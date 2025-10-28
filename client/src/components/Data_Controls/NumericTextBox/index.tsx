import LabelNumericTextBox from "./LabelNumericTextBox";
import React from "react";
import type { UIElement } from "../../../constants/types";
import DefaultNumericTextBox from "./DefaultNumericTextBox";

const NumericTextBox: React.FC<{
  element: UIElement;
  isGrid?: boolean;
}> = ({ element, isGrid }) => {
  switch (element.RenderType) {
  case 15:
      return <DefaultNumericTextBox element={element} isGrid={isGrid} />;
case 16:
 return <LabelNumericTextBox element={element} isGrid={isGrid} />;
 default:
 return null; 
    
  }
};

export default NumericTextBox;