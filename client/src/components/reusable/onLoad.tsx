import type {
  IBindingData,
  UIElement,
  IDropdownValues,
} from "@/constants/types";
import axiosHelper from "@/helpers/axiosHelper";

export async function onLoad(
  element: UIElement,
  slotId: string | null
): Promise<IDropdownValues[]> {
  try {
    if (element["BindingDetail"]) {
      const bindingData = JSON.parse(
        element["BindingDetail"]
      ) as IBindingData[];

      if (!bindingData.length) return [];

      const details = bindingData.find((f) => f["Action"] === "OnLoad");

      if (!details) return [];

      const url = details.Endpoint;
      const method = details.HttpVerb;

      const postData = {
        SlotId: slotId,
        FormVersionId: element.FormVersionId,
        PackageProcessMapId: element.PackageProcessMapId,
        ProcessActivityMapId: element.ProcessActivityMapId,
        ViewPort: 4,
        Action: details.Action,
        FormInstanceId: "",
        JsxFileName: "",
        JsxFileVersion: "",
        Params: JSON.parse(details.Params),
      };
      const data = await axiosHelper<IDropdownValues[]>(url, method, postData);
      return data || [];
    }

    return [];
  } catch (error) {
    console.log(error);
    return [];
  }
}
