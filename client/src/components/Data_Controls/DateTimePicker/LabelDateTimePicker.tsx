import React from "react";
import dayjs from "dayjs";
import type { UIElement } from "@/constants/types";
import { useGeneralStore } from "@/store/useStore";

const LabelDateTimePicker: React.FC<{
  element: UIElement;
  isGrid?: boolean;
}> = ({ element, isGrid }) => {
  const state = useGeneralStore(
    (store) => store.state[element.ElementName]?.["value"]
  );

  return (
    <div className={!Boolean(isGrid) ? element.ColumnCss : undefined}>
      <div
        id={`${element.ElementName}`}
        onClick={() => "LayoutControlSelected(event,this.id)"}
      >
        <div className="form-group">
          {!Boolean(isGrid) && Boolean(element["ShowCaption"]) ? (
            <label
              id={`lbl_${element.ElementName}`}
              className="form-label"
              htmlFor={element.ElementName}
            >
              {element.DCaption}
            </label>
          ) : null}

          <span
            id={`man_${element.ElementName}`}
            className="text-danger"
          ></span>

          <div
            className="input-xs"
            id={`dtp_${element.ElementName} `}
            data-toggle="popover"
            data-placement="auto"
            data-trigger="hover"
            data-content={element.DToolTip}
          >
            <span id={`spn_${element.ElementName}`} className={element.Css}>
              {state ? dayjs(state.toString()).format("YYYY-MM-DD") : ""}
            </span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LabelDateTimePicker;
