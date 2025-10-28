import React from "react";
import dayjs from "dayjs";
import type { UIElement } from "@/constants/types";
import { useGeneralStore } from "@/store/useStore";
import { resusableOnChange } from "../../reusable/onChange";

const DefaultDateTimePicker: React.FC<{
  element: UIElement;
  isGrid?: boolean;
}> = ({ element, isGrid }) => {
  function onchange(e: any) {
    setState(element.ElementName, e.target.value);
    resusableOnChange(element);
  }

  const state = useGeneralStore(
    (store) => store.state[element.ElementName]?.["value"]
  );
  const setState = useGeneralStore((store) => store.setState);

  const controlId = element.ElementName || element.UIElementid;

  return (
    <div className={!Boolean(isGrid) ? element.ColumnCss : undefined}>
      <div
        id={`${element.ElementName}`}
        onClick={() => "LayoutControlSelected(event,this.id)"}
      >
        <div className="form-group">
          {!Boolean(isGrid) && Boolean(element["ShowCaption"]) ? (
            <label className="form-label" htmlFor={controlId}>
              {element.DCaption}
            </label>
          ) : null}

          <span id={`man_${controlId}`} className="text-danger"></span>

          <div
            className="input-xs"
            id={`dtp_${controlId} `}
            data-toggle="popover"
            data-placement="auto"
            data-trigger="hover"
            data-content={`${element.DToolTip}`}
          >
            <input
              className="form-control input-xs"
              id={controlId}
              type="date"
              value={state ? dayjs(state.toString()).format("YYYY-MM-DD") : ""}
              onChange={(e) => onchange(e)}
              onClick={(e) => e.stopPropagation()}
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export default DefaultDateTimePicker;
