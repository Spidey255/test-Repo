import React from "react";
import type{ UIElement } from "../../constants/types";

const PanelBody: React.FC<{
  element: UIElement;
  children?: React.ReactNode;
}> = ({ element, children }) => {
  return <div className={`${element.Css}`}>{children}</div>;
};

export default PanelBody;
