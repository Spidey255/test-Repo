import LabelDateTimePicker from "./LabelDateTimePicker";
import React from "react";
import type { UIElement } from "../../../constants/types";
import DefaultDateTimePicker from "./DefaultDateTimePicker";

const DateTimePicker: React.FC<{
  element: UIElement;
  isGrid?: boolean;
}> = ({ element, isGrid }) => {
  switch (element.RenderType) {
  case 9:
      return <DefaultDateTimePicker element={element} isGrid={isGrid} />;
case 10:
 return <LabelDateTimePicker element={element} isGrid={isGrid} />;
 default:
 return null; 
    
  }
};

export default DateTimePicker;