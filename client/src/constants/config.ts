const BASE_URL = "http://210.18.135.72:5011";

export const config = {
  BASE_URL,
  SAVE_WIDGET_URL: `${BASE_URL}/api/iProofServicesHub/SaveWidgetInstanceData`,

  SUPPORTED_IMAGE_FORMAT: ["jpeg", "jpg", "png", "svg", "webp"],
  SUPPORTED_VIDEO_FORMAT: ["mp4", "webp", "ogg"],
  SUPPORTED_PDF_FORMAT: ["pdf"],
  SUPPORTED_DOCUMENT_FORMAT: ["docx"],
  SUPPORTED_SPREADSHEET_FORMAT: ["xlsx", "xls"],

  DataControlIds: [1, 2, 3, 4, 5, 6, 7, 8, 13, 14, 66, 67, 68, 76],
};
