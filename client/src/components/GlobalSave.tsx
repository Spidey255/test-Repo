import React from "react";
import { v4 } from "uuid";
import _ from "lodash";
import { useGeneralStore } from "@/store/useStore";
import type { IAppResponse, UIElement } from "@/constants/types";
import axiosHelper from "@/helpers/axiosHelper";
import { useUserStore } from "@/store/useUserStore";
import { config } from "@/constants/config";

interface IGlobalSaveProps {
  onSave?: () => void;
  elements?: UIElement[];
}

const GlobalSave: React.FC<IGlobalSaveProps> = ({ elements }) => {
  const slotId = useUserStore((store) => store.slotId);

  const handleSave = async () => {
    try {
      if (!elements?.length) return;

      const state = useGeneralStore.getState().state;

      const formInstanceId = v4();
      // let redirectUrl: string = "";

      const saveAllPromises = elements.map(async (element) => {
        let FormData: object[];
        let Action = "FormSave";

        if (element.ControlType === "Grid") {
          Action = "GridSave";

          const gridElements = Object.entries(state).filter(
            ([key]) =>
              key.includes("+") && key.startsWith(`${element.ElementName}+`)
          );

          const groupedElements = _.groupBy(
            gridElements,
            ([key]) => key.split("+")[1]
          );

          const gridFormattedData = Object.entries(groupedElements).map(
            ([RwId, rowData], index) => ({
              RwId,
              Seq: index + 1,
              Child: rowData.map(([key, { value }]) => ({
                ElementName: key.split("+")[2],
                Value: value || null,
                EDT: state[key]?.EDT,
              })),
            })
          );

          FormData = [
            {
              ElementId: element.ElementId,
              ElementName: element.ElementName,
              Child: gridFormattedData,
            },
          ];
        } else {
          const formattedData = Object.entries(state)
            .filter(([key]) => !key.includes("+"))
            .map(([ElementName, value]) => ({
              ElementName: ElementName,
              Value: value.value || null,
              EDT: value.EDT,
            }));

          FormData = [
            {
              ElementId: element.ElementId,
              ElementName: element.ElementName,
              Child: formattedData,
            },
          ];
        }

        const postData = {
          SlotId: slotId,
          ControlId: element["ElementName"],
          PackageProcessMapId: element["PackageProcessMapId"],
          ProcessActivityMapId: element["ProcessActivityMapId"],
          ViewPort: 4,
          Action: Action,
          FormInstanceId: formInstanceId,
          WidgetId: element["WidgetId"],
          JsxFileName: "",
          JsxFileVersion: "",
          Params: [],
          FormData: FormData,
        };

        const data = await axiosHelper<IAppResponse>(
          config.SAVE_WIDGET_URL,
          "POST",
          postData
        );
        console.log(data);

        // if (data) {
        //   console.log("Form saved successfully");
        //   redirectUrl = data.redirectURL;
        // }
      });

      await Promise.all(saveAllPromises);

      alert(`Form saved successfully. your form id => ${formInstanceId}`);

      // window.location.href = redirectUrl;
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="global-save">
      <div className="form-group" onClick={handleSave}>
        <div className="controls">
          <span>
            <a
              data-popup="popover"
              data-placement="bottom"
              data-trigger="hover"
              data-content=""
              className={"Dark_Button"}
              data-original-title=""
              title=""
            >
              Save
            </a>
          </span>
        </div>
      </div>
      <div className="form-group">
        <div className="controls">
          <span>
            <a
              data-popup="popover"
              data-placement="bottom"
              data-trigger="hover"
              data-content=""
              className={"Cancel_Button"}
              data-original-title=""
              title=""
            >
              Cancel
            </a>
          </span>
        </div>
      </div>
    </div>
  );
};

export default GlobalSave;
