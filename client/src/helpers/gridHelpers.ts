import { v4 as uuidV4 } from "uuid";

import type {
  IFormLoadApiResponseRowChild,
  UIElement,
} from "../constants/types";

export const getValidColumns = (gridInfo?: UIElement[]) => {
  return !gridInfo
    ? []
    : gridInfo?.filter(
        (f) =>
          f["Width"] === undefined &&
          !f.ElementName.includes("_Sequence") &&
          !f.ElementName.includes("_RowId")
      );
};

export const addRow = (
  tableHeaders: UIElement[]
): IFormLoadApiResponseRowChild => {
  const child = tableHeaders.map((head) => ({
    elementId: head.ElementId,
    value: "",
    ect: null,
    seq: 0,
    chk: null,
    visible: false,
    baCol: null,
    fCol: null,
    dCss: null,
    dVl: null,
    rwId: null,
    peId: null,
    child: null,
    purl: null,
    man: false,
    redirectType: null,
    comboSelectedValue: null,
    rType: 0,
    brCl: null,
  }));

  return {
    elementId: null,
    value: null,
    ect: null,
    seq: 1,
    chk: null,
    visible: false,
    baCol: null,
    fCol: null,
    dCss: null,
    dVl: null,
    rwId: uuidV4(),
    peId: null,
    child: child,
    purl: null,
    man: false,
    redirectType: null,
    comboSelectedValue: null,
    rType: 0,
    brCl: null,
  };
};
