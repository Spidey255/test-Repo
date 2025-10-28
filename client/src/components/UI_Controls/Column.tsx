import React from "react";
import type { UIElement } from "../../constants/types";

const Column: React.FC<{
  element: UIElement;
  children?: React.ReactNode;
}> = ({ element, children }) => {
  if (element.ElementName === "UI_GridColumns") return null;

  return <div className={`${element.Css} ${element.ColumnCss}`}>{children}</div>;
};

export default Column;
