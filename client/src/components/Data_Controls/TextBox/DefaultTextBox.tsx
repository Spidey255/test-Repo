import React from "react";
import type { UIElement } from "../../../constants/types";
import { useGeneralStore } from "../../../store/useStore";
import { resusableOnChange } from "../../reusable/onChange";

const DefaultTextBox: React.FC<{ element: UIElement; isGrid?: boolean }> = ({
  element,
  isGrid,
}) => {
  function onChange(e: any) {
    setState(element.ElementName, e.target.value);
    resusableOnChange(element);
  }

  const state = useGeneralStore(
    (store) => store.state[element.ElementName]?.["value"]
  );
  const setState = useGeneralStore((store) => store.setState);

  const controlId = element.ElementName || element.UIElementid;

  return (
    <div
      className={!Boolean(isGrid) ? element.ColumnCss : undefined}
      data-root={`root_${element.ElementName}`}
    >
      <div id={`${element.ElementName}`}>
        <div className="form-group">
          {!Boolean(isGrid) && Boolean(element["ShowCaption"]) ? (
            <label className="form-label" htmlFor={controlId}>
              {element?.DCaption}
            </label>
          ) : null}

          <span id={`man_${controlId}`} className="text-danger"></span>
          <div
            id={`mc_${element.ElementName}`}
            className="controls"
            data-container={`container_${element.ElementName}`}
          >
            <input
              // className={`${element.Css || "form-control"}`}
              className="form-control input-xs"
              type="text"
              autoComplete="off"
              data-toggle="popover"
              data-trigger="hover"
              placeholder={element["DCaption"]}
              value={typeof state === "string" ? state : ""}
              onChange={
                (e) => onChange(e)
                // setState(element.ElementName, e.target.value)
              }
              onClick={(e) => e.stopPropagation()}
              id={controlId}
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export default DefaultTextBox;
