import React from "react";
import { useNavigate } from "react-router-dom";

import type {
  IActionParams,
  IBindingData,
  IGlobalStateValues,
  IAppResponse,
  TValue,
  UIElement,
} from "../../constants/types";
import { useGeneralStore } from "../../store/useStore";
import axiosHelper from "../../helpers/axiosHelper";
import { useUserStore } from "../../store/useUserStore";
import { useGridStore } from "../../store/useGridStore";
import { getISpaceAbsoluteUrl } from "../../helpers/utils";

const ActionButton: React.FC<{ element: UIElement }> = ({ element }) => {
  const navigate = useNavigate();
  const slotId = useUserStore((store) => store.slotId);
  const setPagination = useGridStore((store) => store.setPagination);
  const setGridDynamicState = useGridStore(
    (store) => store.setGridDynamicState
  );
  const updateUIElementState = useGeneralStore(
    (store) => store.updateUIElementState
  );
  const state = useGeneralStore(
    (store) => store.state[element.ElementName]?.["value"]
  );

  const handleOnClick = async () => {
    try {
      if (!element["BindingDetail"])
        return console.log("No BindingDetail Found");

      const bindingDetails = JSON.parse(
        element["BindingDetail"]
      ) as IBindingData[];

      if (!bindingDetails.length)
        return console.log("No Content in BindingDetail");

      const details = bindingDetails[0];
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
        const elementId = element.ElementName.includes("+")
          ? id.split("+")[2]
          : id;
        acc[elementId] = useGeneralStore.getState().state[id]?.["value"] || "";
        return acc;
      }, {} as Record<string, TValue | undefined>);

      const stateParams = Object.entries(fetchedStateValues).map(
        ([id, value]) => ({
          ElementName: id,
          Value: value,
        })
      );
      console.log(stateParams);

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
        // "FormInstanceId": "12BA0DDF-87B5-4541-A9F4-E15BCDBDE780",
        // "WidgetId": "fd60752d-b019-6b5e-4c99-fb75c38f59e9",
        JsxFileName: "",
        JsxFileVersion: "",
        Params: stateParams,
      };

      const data = await axiosHelper<IAppResponse>(url, method, postData);

      if (!data["rows"]) {
        return console.log(
          "No rows in response in ACTION BUTTON " + element.Id
        );
      }

      // REDIRECTION LOGICS
      const redirectionInfo = data.rows.filter((row) =>
        Boolean(row["redirectType"])
      );

      if (redirectionInfo.length) {
        if (redirectionInfo[0].redirectType === 1) {
          window.location.href = `${getISpaceAbsoluteUrl(
            element["VersionName"],
            redirectionInfo[0].value! as string
          )}?SlId=${slotId}`;

          return;
        }
        if (redirectionInfo[0].redirectType === 2) navigate("/");
      }

      //  UPDATE UI CONTROLS
      const updateDataInfo = data.rows.filter(
        (row) => !Boolean(row["redirectType"])
      );

      const updateState: { [key: string]: IGlobalStateValues } = {};

      updateDataInfo
        .filter((f) => f["child"] === null)
        .forEach((row) => {
          if (row["elementId"])
            updateState[row.elementId] = { value: row.value! };
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
                updateState[
                  `${elementMapper.elementName}+${row.rwId}+${rowData.elementName}`
                ] = {
                  value: (typeof rowData["value"] !== "object"
                    ? rowData["value"]
                      ? rowData["value"]
                      : ""
                    : "") as string,
                };
              });
            });

            // eslint-disable-next-line @typescript-eslint/no-non-null-asserted-optional-chain
            setGridDynamicState(elementMapper.uiElementId, m?.child!);
            setPagination(
              elementMapper.uiElementId,
              1,
              5,
              m?.child?.length || 0
            );
          }
        });

      useGeneralStore.getState().updateInitialState(updateState);

      updateDataInfo
        .filter((f) => f["child"] === null)
        .forEach((m) => {
          if (m["elementName"]) updateUIElementState(m["elementName"], m);
        });
    } catch (error) {
      console.log(error);
      alert("something went wrong");
    }
  };

  return (
    <div
      id={element.ElementId || element.UIElementid}
      onClick={(e) => {
        e.stopPropagation();
        handleOnClick();
      }}
      className={`${element.ColumnCss}`}
    >
      <div className="form-group">
        <div className="controls">
          <span title={element.ElementName}>
            <a
              data-popup="popover"
              data-placement="bottom"
              data-trigger="hover"
              data-content=""
              id={`btn_${element.ElementId}`}
              data-name={`btn_${element.ElementId}`}
              className={element.Css}
              data-original-title=""
              title={element?.DCaption}
            >
              {state || element?.DCaption}
            </a>
          </span>
        </div>
      </div>
    </div>
  );
};

export default ActionButton;
