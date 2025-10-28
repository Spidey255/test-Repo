
import { config } from "../constants/config";
import type { TDevice, TElementControlProperty } from "../constants/types";

export const getColumnClassName = (device: TDevice, wrap = 0) => {
  return wrap === 0 ? "" : `col-${device}-${wrap}`;
};

export const accessMandatory = (prop: TElementControlProperty): boolean => {
  if ("Mandatory" in prop) {
    return prop.Mandatory;
  }
  return false;
};

export const transformArray = (array: Record<string, unknown>[]) => {
  return array.map((item) => {
    const keys = Object.keys(item);
    return {
      id: item[keys[0]] as string,
      value: item[keys[1]] as string,
    };
  });
};

export const getExtensionFromBase64 = (mimePart: string): string | null => {
  const mimeType = mimePart.match(/data:(.*?);base64/);

  if (mimeType && mimeType[1]) {
    const mime = mimeType[1] as keyof typeof mimeToExt;

    const mimeToExt: Record<string, string> = {
      // Images
      "image/jpeg": "jpg",
      "image/png": "png",
      "image/gif": "gif",
      "image/bmp": "bmp",
      "image/webp": "webp",
      "image/svg+xml": "svg",
      "image/tiff": "tiff",
      "image/x-icon": "ico",

      // Documents
      "application/pdf": "pdf",
      "text/plain": "txt",
      "application/msword": "doc",
      "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
        "docx",
      "application/vnd.ms-excel": "xls",
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
        "xlsx",
      "application/vnd.ms-powerpoint": "ppt",
      "application/vnd.openxmlformats-officedocument.presentationml.presentation":
        "pptx",
      "application/rtf": "rtf",
      "application/vnd.oasis.opendocument.text": "odt",
      "application/vnd.oasis.opendocument.spreadsheet": "ods",

      // Audio
      "audio/mpeg": "mp3",
      "audio/wav": "wav",
      "audio/ogg": "ogg",
      "audio/webm": "weba",
      "audio/aac": "aac",

      // Video
      "video/mp4": "mp4",
      "video/x-msvideo": "avi",
      "video/x-matroska": "mkv",
      "video/webm": "webm",
      "video/quicktime": "mov",
    };

    return mimeToExt[mime] || null;
  }

  return null;
};

export const downloadFile = (fileName: string, base64: string) => {
  const link = document.createElement("a");
  link.href = base64;
  link.download = fileName;
  link.click();
};

export const getISpaceBaseUrl = (
  versionName: "Mobile" | "Large" | string | undefined = "Large"
) => {
  switch (versionName) {
    case "Mobile":
      return `${config.BASE_URL}/Mobile`;
    default:
      return `${config.BASE_URL}/Default`;
  }
};

export const getISpaceAbsoluteUrl = (
  versionName: string | undefined,
  url: string
) => {
  return getISpaceBaseUrl(versionName) + url.split("~")[1];
};
