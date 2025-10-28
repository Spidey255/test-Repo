import React from "react";
import type { UIElement } from "../../constants/types";

const PanelHeader: React.FC<{
  element: UIElement;
  children?: React.ReactNode;
}> = ({ element, children }) => {
  const handleClick = (event: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
    event.stopPropagation();
    console.log("Panel clicked:", element);
    // Replace with actual navigation or selection logic
  };

  const handleCollapse = (
    event: React.MouseEvent<HTMLAnchorElement, MouseEvent>
  ) => {
    event.preventDefault();
    event.stopPropagation();
    console.log("Collapse clicked");
    // Replace with collapse/expand logic
  };

  return (
    <div
      className={`${element.ColumnCss} ${element.Css}`}
      style={{
        borderStyle: "solid",
        fontWeight: "normal",
        fontStyle: "normal",
        textDecoration: "none",
      }}
    >
      {children}
    </div>
  );
};

export default PanelHeader;
