
import axiosHelper from "./axiosHelper";
import { transformArray } from "./utils";
import type{ UIElement } from "../constants/types";
export const fetchJSONData = async (
  baseUrl: string,
  mainPathUrl: string,
  slotId: string
) => {
  const jsonData = await axiosHelper<UIElement[]>(mainPathUrl, "GET");

  const modifiedData = jsonData.map(async (f) => {
    if (f.ControlId === 5) {
      const jsonBindingData = f["BindingDetail"]
        ? JSON.parse(f.BindingDetail)
        : [];
      if (jsonBindingData.length) {
        const url = jsonBindingData?.[0]?.Endpoint;

        try {
          const comboboxData = await axiosHelper<[]>(
            url,
            "POST",
            JSON.stringify({
              SlotId: slotId,
              ControlId: f.ElementId,
              PackageProcessMapId: f.PackageProcessMapId,
              ProcessActivityMapId: f.ProcessActivityMapId,
              FormVersionId: f.FormVersionId,
              ViewPort: 4,
              JsxFileName: "",
              JsxFileVersion: "",
              Params: [{ ElementId: f.ElementId }],
            }),
            {
              "Content-Type": "application/json",
            }
          );

          if (comboboxData) {
            f.List = transformArray(comboboxData);
          }
          // eslint-disable-next-line @typescript-eslint/no-unused-vars
        } catch (error) {
          console.log("ERROR ON COMBOBOX SERVER");
          f.List = [];
        }
      }

      return f;
    }

    if (f.ControlId === 11 && f["GM"] === "Repeater") {
      try {
        const repeaterGridData = await axiosHelper<UIElement[]>(
          `${baseUrl}/${f.ElementId}.json`,
          "GET"
        );

        f.RepeaterGridData = repeaterGridData;
        // eslint-disable-next-line @typescript-eslint/no-unused-vars
      } catch (error) {
        console.log("ERROR ON REPEATER GRID SERVER");
        f.RepeaterGridData = [];
      }
    }

    return f;
  });

  const resolvedJsonData = await Promise.all(modifiedData);

  const result = filterJsonData(resolvedJsonData);

  return result;
};

const filterJsonData = (data: UIElement[]) => {
  const gridColumns = data.filter((f) => f.ElementName === "UI_GridColumns");

  const filteredJsonData = data.filter((f) => {
    return !gridColumns.some(
      (s) =>
        s.ElementName !== "UI_GridColumns" && s.UIElementid === f.UIElementid
    );
  });

  // TABS LOGIC
  const tabs = filteredJsonData.filter((f) => f.ControlId === 19); // TAB CONTROL ID

  tabs.forEach((m) => {
    const tabsPane = data.filter(
      (f) =>
        f.ControlId === 20 &&
        f.ParentElementId?.toLocaleLowerCase() === m.UIElementid.toLowerCase()
    );

    if (tabsPane.length) {
      const tabPaneIndex = filteredJsonData.findIndex(
        (f) => f.Id === tabsPane[0].Id
      );
      if (tabPaneIndex !== -1) filteredJsonData[tabPaneIndex].ActiveTab = true;
    }

    const tabIndex = filteredJsonData.findIndex((f) => f.Id === m.Id);

    if (tabIndex !== -1) filteredJsonData[tabIndex].TabHeaders = tabsPane;
  });

  // GRID LOGIC
  const grid = filteredJsonData.filter((f) => f.ControlId === 11); // GRID CONTROL ID

  grid.forEach((m) => {
    const gridElements = filteredJsonData.filter(
      (f) =>
        f.ParentElementId?.toLocaleLowerCase() === m.UIElementid.toLowerCase()
    );

    gridElements.forEach((el) => {
      const gridElementIndex = filteredJsonData.findIndex(
        (f) => f.Id === el.Id
      );

      if (gridElementIndex !== -1)
        filteredJsonData[gridElementIndex]["TempElementId"] = m.ElementId;
    });
  });

  return { data: filteredJsonData };
};
