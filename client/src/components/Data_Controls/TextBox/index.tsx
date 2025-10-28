import LabelTextBox from "./LabelTextBox";
import React from "react";
import type { UIElement } from "../../../constants/types";
import DefaultTextBox from "./DefaultTextBox";

const TextBox: React.FC<{
  element: UIElement;
  isGrid?: boolean;
}> = ({ element, isGrid }) => {
  switch (element.RenderType) {
  case 1:
      return <DefaultTextBox element={element} isGrid={isGrid} />;
case 2:
 return <LabelTextBox element={element} isGrid={isGrid} />;
 default:
 return null; 
    
  }
};

export default TextBox;