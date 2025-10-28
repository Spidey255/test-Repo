import React from "react";
import type { UIElement } from "../../constants/types";

const RightPane: React.FC<{
  element: UIElement;
  children?: React.ReactNode;
}> = ({ element, children }) => {
  return (
    <>
      <div
        id={element.Id}
        className={`${element.ColumnCss} ${element.Css}`}
        style={{
          fontWeight: "normal",
          fontStyle: "normal",
          textDecoration: "none",
        }}
      >
        <ul className="icons-list">
          <li>
            <a data-action="collapse" className="collapse2" />
          </li>
        </ul>
        {children}
      </div>
      <a className="heading-elements-toggle">
        <i className="icon-menu"></i>
      </a>
    </>
  );
};

export default RightPane;
