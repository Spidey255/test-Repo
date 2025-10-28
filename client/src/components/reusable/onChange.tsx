import { useGridStore } from "@/store/useGridStore";
import { useGeneralStore } from "../../store/useStore";
import { useUserStore } from "../../store/useUserStore";
import axiosHelper from "../../helpers/axiosHelper";
import type {
  IBindingData,
  IActionParams,
  IAppResponse,
  IGlobalStateValues,
  UIElement,
  TValue,
} from "../../constants/types";

export async function resusableOnChange(element: UIElement) {
  if (element.Action == "OnChange" && element.BindingDetail) {
    try {
      const slotId = useUserStore.getState().slotId;

      if (!element["BindingDetail"])
        return console.log("No BindingDetail Found");

      const bindingDetails = JSON.parse(
        element["BindingDetail"]
      ) as IBindingData[];

      if (!bindingDetails.length)
        return console.log("No Content in BindingDetail");

      const details =
        bindingDetails.length === 1
          ? bindingDetails[0]
          : bindingDetails.find((f) => f["Action"] === "OnChange");

      if (!details) return [];

      const url = details.Endpoint;
      const method = details.HttpVerb;

      const params = JSON.parse(details["Params"]) as IActionParams[];
      let elementIds = params.map((p) => p.ElementName);

      if (element.ElementName.includes("+")) {
        const [id, rwId] = element.ElementName.split("+");
        const rowId = `${id}+${rwId}`;
        elementIds = params.map((p) => `${rowId}+${p.ElementName}`);
      }

      const fetchedStateValues = elementIds.reduce((acc, id) => {
        const ElementName = element.ElementName.includes("+")
          ? id.split("+")[2]
          : id;
        acc[ElementName] =
          useGeneralStore.getState().state[id]?.["value"] || "";
        return acc;
      }, {} as Record<string, TValue | undefined>);

      const stateParams = Object.entries(fetchedStateValues).map(
        ([id, value]) => ({
          ElementName: id,
          Value: value,
        })
      );

      const postData = {
        SlotId: slotId,
        ControlId: element["ElementName"].includes("+")
          ? element["ElementName"].split("+")[2]
          : element["ElementName"],
        PackageProcessMapId: element.PackageProcessMapId,
        ProcessActivityMapId: element.ProcessActivityMapId,
        FormVersionId: element.FormVersionId,
        ViewPort: 4,
        Action: element?.Action,
        JsxFileName: "",
        JsxFileVersion: "",
        Params: stateParams,
      };

      const data = await axiosHelper<IAppResponse>(url, method, postData);

      if (!data["rows"])
        return console.log(
          "No rows in response in ACTION BUTTON " + element.Id
        );

      // REDIRECTION LOGICS
      const redirectionInfo = data.rows.filter((row) =>
        Boolean(row["redirectType"])
      );

      if (redirectionInfo.length) {
        if (redirectionInfo[0].redirectType === 1) {
          return; // Handle redirection logic here
        }
        // Handle other types of redirection if needed
      }

      // Update UI CONTROLS
      const updateDataInfo = data.rows.filter(
        (row) => !Boolean(row["redirectType"])
      );

      const updateState: { [key: string]: IGlobalStateValues } = {};

      updateDataInfo
        .filter((f) => f["child"] === null)
        .forEach((row) => {
          if (row["elementName"]) {
            // Dynamically update state
            updateState[row.elementName] = { value: row.value! };
          }
        });

      // GRID STATE UPDATE
      data.rows
        .filter((f) => f["child"] && f["child"].length)
        .forEach((m) => {
          if (m["elementName"]) {
            const elementMapper =
              useGridStore.getState().gridElementMapper[m.elementName];

            m.child?.map((row) => {
              row.child?.forEach((rowData) => {
                // Dynamically update grid state based on element structure
                updateState[
                  `${elementMapper.elementName}+${row.rwId}+${rowData.elementName}`
                ] = {
                  value: (typeof rowData["value"] !== "object"
                    ? rowData["value"]
                    : "") as string,
                };
              });
            });

            const { setGridDynamicState, setPagination } =
              useGridStore.getState();
            // Call dynamic grid state update
            setGridDynamicState(elementMapper.uiElementId, m?.child!);
            setPagination(
              elementMapper.uiElementId,
              1,
              5,
              m?.child?.length || 0
            );
          }
        });

      // Update the general store dynamically
      useGeneralStore.getState().updateInitialState(updateState);
    } catch (error) {
      console.log(error);
      alert("Something went wrong");
    }
  }
}
