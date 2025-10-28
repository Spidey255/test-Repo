import React from "react";
import type { UIElement } from "@/constants/types";
import { useGeneralStore } from "@/store/useStore";
import { resusableOnChange } from "../../reusable/onChange";

const DefaultNumericTextBox: React.FC<{
  element: UIElement;
  isGrid?: boolean;
}> = ({ element, isGrid }) => {
  const state = useGeneralStore(
    (store) => store.state[element.ElementName]?.["value"]
  );
  const setState = useGeneralStore((store) => store.setState);
  const controlId = element.ElementName || element.UIElementid;

  const onChange = (event: any, element: UIElement) => {
    setState(element.ElementName, event);
    if (element.Action == "OnChange" && element.BindingDetail) {
      resusableOnChange(element);
    }
  };

  return (
    <div className={!Boolean(isGrid) ? element.ColumnCss : undefined}>
      <div id={controlId}>
        <div className="form-group">
          {!Boolean(isGrid) && Boolean(element["ShowCaption"]) ? (
            <label className="form-label" htmlFor={controlId}>
              {element?.DCaption}
            </label>
          ) : null}

          <span
            id={`man_${element.ElementName}`}
            className="text-danger"
          ></span>

          <span className="help">{element?.DHelpText}</span>

          <div className="controls" id={`dtx_${element.ElementName}`}>
            <input
              name={element.ElementName}
              type="number"
              className="form-control input-xs"
              tabIndex={element.TabIndex}
              data-toggle="popover"
              data-trigger="hover"
              data-content="{ToolTip}"
              value={typeof state === "string" ? state : ""}
              onChange={(e) => onChange(e.target.value, element)}
              onClick={(e) => e.stopPropagation()}
              id={controlId}
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export default DefaultNumericTextBox;
