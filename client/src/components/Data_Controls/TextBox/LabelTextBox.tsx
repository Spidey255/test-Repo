import React from "react";
import type{ UIElement } from "../../../constants/types";
import { useGeneralStore } from "../../../store/useStore";
const LabelTextBox: React.FC<{ element: UIElement; isGrid?: boolean }> = ({
  element,
  isGrid,
}) => {
  const state = useGeneralStore(
    (store) => store.state[element.ElementName]?.["value"]
  );

  return (
    <div
      className={!Boolean(isGrid) ? element.ColumnCss : undefined}
      data-root={`root_${element.ElementName}`}
    >
      <div id={`${element.ElementName}`}>
        <div className="form-group">
          {!Boolean(isGrid) && Boolean(element["ShowCaption"]) ? (
            <label className="form-label" htmlFor={element.ElementName}>
              {element?.DCaption}
            </label>
          ) : null}

          <span id={`man_${element.ElementName}`} className="text-danger"></span>

          <div
            id={`mc_${element.ElementName}`}
            className="controls"
            data-container={`container_${element.ElementName}`}
          >
            <span className={element.Css}>{state}</span>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LabelTextBox;
