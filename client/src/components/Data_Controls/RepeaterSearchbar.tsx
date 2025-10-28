import React from "react";

import type{ UIElement } from "../../constants/types";
import { useGridStore } from "../../store/useGridStore";
import { useGeneralStore } from "../../store/useStore";
const RepeaterGridSearchBar: React.FC<{ element: UIElement }> = ({
  element,
}) => {
  const gridSearchText = useGridStore(
    (store) => store.gridSearchText?.[element.ParentElementId!]
  );
  const setGridSearchText = useGridStore((store) => store.setGridSearchText);
  const uiState = useGeneralStore(
    (store) => store.uiElementState?.[String(element?.TempElementId)]
  );

  if (uiState?.["visible"] === false || !element?.Css?.includes("in")) {
    return null;
  }

  return (
    <div
      onClick={() => "LayoutControlSelected(event,this.id);"}
      className={element.Css}
      style={{ textDecoration: "none" }}
    >
      <div className="pt-10 mr-15 ml-15">
        <div className="row">
          <div className="col-md-5  col-sm-5 col-xs-12">
            <ul className="icons-list navbarpad">
              <li className="dropdown">
                <a
                  id="dd_ad178b25-717b-adb8-3943-35eb95d35765"
                  type="button"
                  className="dropdown-toggle"
                  data-toggle="dropdown"
                  aria-expanded="false"
                >
                  <i className="iconmenu7">
                    <svg width="18" height="16" viewBox="0 0 18 16" fill="none">
                      <path
                        d="m13 5 3 3-3 3M1 1h14M1 8h9m-9 7h14"
                        stroke="#000"
                        strokeWidth="2"
                        strokeLinecap="round"
                      ></path>
                    </svg>
                  </i>
                </a>
                <ul className="dropdown-menu dropdown-menu-right">
                  <li style={{ display: "none" }}>
                    <a
                      id="btnRefresh_ad178b25-717b-adb8-3943-35eb95d35765"
                      type="button"
                      data-toggle="tooltip"
                      data-placement="top"
                      title="Refresh"
                      onClick={() =>
                        "OnRefreshGrid('ad178b25-717b-adb8-3943-35eb95d35765','RefreshGrid',false);"
                      }
                    >
                      <i className="icon-loop3"></i> Refresh
                    </a>
                  </li>
                  <li>
                    <a
                      id="btnPrintPDF_ad178b25-717b-adb8-3943-35eb95d35765"
                      type="button"
                      data-toggle="tooltip"
                      data-placement="top"
                      title="Download PDF"
                      onClick={() =>
                        "PrintPDF('ad178b25-717b-adb8-3943-35eb95d35765');"
                      }
                    >
                      <i className="icon-printer"></i> Download PDF
                    </a>
                  </li>
                  <li style={{ display: "none" }}>
                    <a
                      id="btnPrintAllPDF_ad178b25-717b-adb8-3943-35eb95d35765"
                      type="button"
                      data-toggle="tooltip"
                      data-placement="top"
                      title="Download All PDF"
                      onClick={() =>
                        "OnGridAction('ad178b25-717b-adb8-3943-35eb95d35765','PrintAllPDF');"
                      }
                    >
                      <i className="icon-printer"></i> Download PDF (All rows)
                    </a>
                  </li>
                  <li>
                    <a
                      id="btnPrintExcel_ad178b25-717b-adb8-3943-35eb95d35765"
                      type="button"
                      data-toggle="tooltip"
                      data-placement="top"
                      title="Download EXCEL"
                      onClick={() =>
                        "PrintEXCEL('ad178b25-717b-adb8-3943-35eb95d35765');"
                      }
                    >
                      <i className="icon-printer"></i> Download EXCEL
                    </a>
                  </li>
                  <li style={{ display: "none" }}>
                    <a
                      id="btnPrintAllExcel_ad178b25-717b-adb8-3943-35eb95d35765"
                      type="button"
                      data-toggle="tooltip"
                      data-placement="top"
                      title="Download All Excel"
                      onClick={() =>
                        "OnGridAction('ad178b25-717b-adb8-3943-35eb95d35765','PrintAllExcel');"
                      }
                    >
                      <i className="icon-printer"></i> Download Excel(All rows)
                    </a>
                  </li>
                </ul>
              </li>
              <li>
                <a
                  className="btn btn-default"
                  data-toggle="tooltip"
                  data-placement="top"
                  title="Refresh"
                  onClick={() =>
                    "OnRefreshGrid('ad178b25-717b-adb8-3943-35eb95d35765','RefreshGrid',false);"
                  }
                >
                  <i className="fa fa-refresh" style={{ margin: 0 }}></i>
                </a>
              </li>
              <li>
                <div className="form-group">
                  <div className="multi-select-full R-dropdown">
                    <select
                      id="cmb_ad178b25-717b-adb8-3943-35eb95d35765"
                      className=""
                      style={{ display: "none" }}
                    >
                      <option value="Name1">Name</option>
                      <option value="Role1">Designation</option>
                      <option value="DOB">DOB</option>
                      <option value="MobileNumber">Mobile Number</option>
                      <option value="Mail1">Email Id</option>
                    </select>
                    <div className="btn-group">
                      <button
                        type="button"
                        className="multiselect dropdown-toggle btn btn-default btn-Custom-xs"
                        data-toggle="dropdown"
                        title="None selected"
                      >
                        <span className="multiselect-selected-text">
                          None selected
                        </span>
                        <b className="caret"></b>
                      </button>
                      <ul className="multiselect-container dropdown-menu">
                        <li
                          className="multiselect-item multiselect-filter"
                          value="0"
                        >
                          <i className="serch-icon">
                            <svg width="24" height="24" viewBox="0 0 24 24">
                              <g clipPath="url(#a)">
                                <path
                                  d="m23.707 22.294-5.97-5.97a10.016 10.016 0 1 0-1.413 1.415l5.969 5.969a1 1 0 0 0 1.414-1.414ZM10 18a8 8 0 1 1 8-8 8.009 8.009 0 0 1-8 8Z"
                                  fill="#374957"
                                ></path>
                              </g>
                              <defs>
                                <clipPath id="a">
                                  <path fill="#fff" d="M0 0h24v24H0z"></path>
                                </clipPath>
                              </defs>
                            </svg>
                          </i>
                          <input
                            className="form-control"
                            type="text"
                            placeholder="Search"
                          />
                        </li>
                        <li>
                          <a tabIndex={0}>
                            <label className="checkbox">
                              <input type="checkbox" value="Name1" /> Name
                            </label>
                          </a>
                        </li>
                        <li>
                          <a tabIndex={0}>
                            <label className="checkbox">
                              <input type="checkbox" value="Role1" />
                              Designation
                            </label>
                          </a>
                        </li>
                        <li>
                          <a tabIndex={0}>
                            <label className="checkbox">
                              <input type="checkbox" value="DOB" />
                              DOB
                            </label>
                          </a>
                        </li>
                        <li>
                          <a tabIndex={0}>
                            <label className="checkbox">
                              <input type="checkbox" value="MobileNumber" />
                              Mobile Number
                            </label>
                          </a>
                        </li>
                        <li>
                          <a tabIndex={0}>
                            <label className="checkbox">
                              <input type="checkbox" value="Mail1" /> Email Id
                            </label>
                          </a>
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              </li>
              <li>
                <div className="input-group-btn">
                  <button
                    className="btn btn-info btn-icon"
                    type="button"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Search"
                    onClick={() =>
                      "OnRefreshGrid('ad178b25-717b-adb8-3943-35eb95d35765','SearchGrid',true);"
                    }
                  >
                    <i className="fasearch" style={{ margin: 0 }}>
                      <svg width="24" height="24" viewBox="0 0 24 24">
                        <g clipPath="url(#a)">
                          <path
                            d="m23.707 22.294-5.97-5.97a10.016 10.016 0 1 0-1.413 1.415l5.969 5.969a1 1 0 0 0 1.414-1.414ZM10 18a8 8 0 1 1 8-8 8.009 8.009 0 0 1-8 8Z"
                            fill="#374957"
                          ></path>
                        </g>
                        <defs>
                          <clipPath id="a">
                            <path fill="#fff" d="M0 0h24v24H0z"></path>
                          </clipPath>
                        </defs>
                      </svg>
                    </i>
                  </button>
                </div>
              </li>
            </ul>
          </div>
          <div className="col-md-6  col-sm-6 col-xs-12">
            <div className="input-group input-group-xs R-search">
              <input
                id="txt_ad178b25-717b-adb8-3943-35eb95d35765"
                className="form-control input-xs"
                placeholder="Search..."
                type="search"
                value={gridSearchText || ""}
                onChange={(e) =>
                  setGridSearchText(element["ParentElementId"]!, e.target.value)
                }
                onKeyDown={() => {
                  //  "OnSearchGrid(e,'ad178b25-717b-adb8-3943-35eb95d35765');"
                }}
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default RepeaterGridSearchBar;
