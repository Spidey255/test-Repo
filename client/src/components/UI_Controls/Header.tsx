import React, { JSX } from "react";
import type { UIElement } from "../../constants/types";

const Header: React.FC<{ element: UIElement }> = ({ element }) => {
  const Tag = (element?.HeadingType || "h1") as keyof JSX.IntrinsicElements;

  return (
    <Tag
      id={element.ElementId || element.UIElementid || ""}
      className={`${element?.ColumnCss || ""} ${element?.Css || ""}`}
    >
      {element?.UCaption || ""}
      {element.ElementId}
    </Tag>
  );
};

export default Header;
