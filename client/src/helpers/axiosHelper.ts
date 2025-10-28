import axios, { type AxiosRequestConfig, type AxiosResponse } from "axios";

import type { UIElement } from "../constants/types";
/**
 * Axios Helper method for making HTTP requests
 * @param url - The endpoint URL
 * @param method - The HTTP method (GET, POST, PUT, DELETE, etc.)
 * @param data - The request body (optional, for methods like POST, PUT)
 * @param headers - Additional headers (optional)
 * @param params - Additional headers (optional)
 * @returns Promise resolving to the response data or rejecting with an error object
 */
const axiosHelper = async <T>(
  url: string,
  method: "GET" | "POST" | "PUT" | "DELETE" | "PATCH" | string,
  data?: unknown,
  headers: Record<string, string> = {},
  params: Record<string, unknown> = {}
): Promise<T> => {
  try {
    const config: AxiosRequestConfig = {
      url,
      method,
      data,
      headers,
      params,
    };

    const response: AxiosResponse<T> = await axios(config);
    return response.data;
  } catch (error: unknown) {
    if (axios.isAxiosError(error)) {
      // Handle Axios-specific errors
      throw {
        message: error.message,
        status: error.response?.status,
        data: error.response?.data || null,
      };
    } else if (error instanceof Error) {
      // Handle generic JavaScript errors
      throw {
        message: error.message,
      };
    } else {
      // Fallback for unexpected errors
      throw {
        message: "An unknown error occurred",
      };
    }
  }
};

export const getFormOnLoadData = async <T>(
  slotId: string,
  element: UIElement
): Promise<T> => {
  const jsonBindingData = JSON.parse(element.BindingDetail || "");
  if (!jsonBindingData.length) throw new Error("No Binding Details Found");

  const url = jsonBindingData[0]?.Endpoint;
  const params = jsonBindingData[0]?.Params;

  const data = await axiosHelper<T>(
    url,
    "POST",
    JSON.stringify({
      SlotId: slotId,
      WidgetId: element.WidgetId,
      ControlId: element.WidgetId,
      PackageProcessMapId: element.PackageProcessMapId,
      ProcessActivityMapId: element.ProcessActivityMapId,
      FormVersionId: element.FormVersionId,
      ViewPort: 4,
      JsxFileName: "",
      JsxFileVersion: "",
      Action: element.Action,
      Params: params || [],
      FormData: [],
    }),
    {
      "Content-Type": "application/json",
    }
  );

  return data;
};

export const getWidgetInstanceData = async <T>(
  slotId: string,
  element: UIElement,
  formInstanceId: string
): Promise<T> => {
  const data = await axiosHelper<T>(
    "http://210.18.135.72:5009/api/iProofServicesHub/GetWidgetInstanceData",
    "POST",
    JSON.stringify({
      SlotId: slotId,
      WidgetId: element["WidgetId"],
      ControlId: element["WidgetId"],
      PackageProcessMapId: element["PackageProcessMapId"],
      ProcessActivityMapId: element["ProcessActivityMapId"],
      FormInstanceId: formInstanceId,
      ViewPort: 4,
      JsxFileName: "",
      JsxFileVersion: "",
      Action: element["Action"],
      Params: [],
      FormData: [],
    }),
    {
      "Content-Type": "application/json",
    }
  );

  return data;
};

export default axiosHelper;
