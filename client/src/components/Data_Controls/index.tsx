
import React from "react";
import RepeaterHeader from "@/components/Data_Controls/RepeaterHeader";
import RepeaterSearchbar from "@/components/Data_Controls/RepeaterSearchbar";
import RepeaterFooter from "@/components/Data_Controls/RepeaterFooter";
import NumericTextBox from "@/components/Data_Controls/NumericTextBox";
import TextBox from "@/components/Data_Controls/TextBox";
import ActionButton from "@/components/Data_Controls/ActionButton";
import DateTimePicker from "@/components/Data_Controls/DateTimePicker";

export const componentsMap: { [key: string]: React.FC<any> } = {
  66: RepeaterHeader,
  67: RepeaterSearchbar,
  68: RepeaterFooter,
  2: NumericTextBox,
  1: TextBox,
  9: ActionButton,
  7: DateTimePicker,
};
