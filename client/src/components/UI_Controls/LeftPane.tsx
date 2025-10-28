import React from "react";
import type { UIElement } from "../../constants/types";

const LeftPane: React.FC<{
  element: UIElement;
  children?: React.ReactNode;
}> = ({ element, children }) => {
  return (
    <div
      className={`${element.ColumnCss} ${element.Css}`}
      style={{
        fontWeight: "normal",
        fontStyle: "normal",
        textDecoration: "none",
      }}
    >
      {children}
    </div>
  );
};

export default LeftPane;
