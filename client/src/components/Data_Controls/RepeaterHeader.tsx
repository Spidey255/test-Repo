/* eslint-disable @typescript-eslint/no-unused-vars */
import React from "react";

import type{ UIElement } from "../../constants/types";
import { useGridStore } from "../../store/useGridStore";
import { getValidColumns } from "../../helpers/gridHelpers";
import { useGeneralStore } from "../../store/useStore";

const RepeaterGridHeader: React.FC<{ element: UIElement }> = ({ element }) => {
  const updateGridState = useGridStore((store) => store.updateGridState);
  const gridHeader = useGridStore(
    (store) => store.gridHeader?.[String(element.ParentElementId)]
  );
  const uiState = useGeneralStore(
    (store) => store.uiElementState?.[String(element?.TempElementId)]
  );

  const tableHeaders = getValidColumns(gridHeader);

  if (uiState?.["visible"] === false) return null;

  return (
    <>
      <div
        id=" "
        data-root="root_"
        // onclick={()=>LayoutControlSelected(event,this.id)}
        data-container="container_"
        data-gridid="hdr_"
        className="panel-heading"
      >
        <span className="text-danger"></span>

        <h6 className="panel-title">
          <span id="spnLOD_"></span>
        </h6>

        <div className="heading-elements">
          <ul className="icons-list navbarpad">
            <li className="">
              <a
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="Add"
                className="btn"
                style={{ display: "none" }}
              >
                <i className="iconplus2">
                  <svg width="24" height="24" viewBox="0 0 24 24" fill="none">
                    <g clipPath="url(#a)" fill="#374957">
                      <path d="M7 0H4a4 4 0 0 0-4 4v3a4 4 0 0 0 4 4h3a4 4 0 0 0 4-4V4a4 4 0 0 0-4-4Zm2 7a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V4a2 2 0 0 1 2-2h3a2 2 0 0 1 2 2v3Zm-2 6H4a4 4 0 0 0-4 4v3a4 4 0 0 0 4 4h3a4 4 0 0 0 4-4v-3a4 4 0 0 0-4-4Zm2 7a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2v-3a2 2 0 0 1 2-2h3a2 2 0 0 1 2 2v3Zm11-7h-3a4 4 0 0 0-4 4v3a4 4 0 0 0 4 4h3a4 4 0 0 0 4-4v-3a4 4 0 0 0-4-4Zm2 7a2 2 0 0 1-2 2h-3a2 2 0 0 1-2-2v-3a2 2 0 0 1 2-2h3a2 2 0 0 1 2 2v3ZM14 7h3v3a1 1 0 0 0 2 0V7h3a1 1 0 1 0 0-2h-3V2a1 1 0 1 0-2 0v3h-3a1 1 0 0 0 0 2Z" />
                    </g>
                    <defs>
                      <clipPath id="a">
                        <path fill="#fff" d="M0 0h24v24H0z" />
                      </clipPath>
                    </defs>
                  </svg>
                </i>
              </a>
            </li>

            <li className="dropdown">
              <a
                type="button"
                className="dropdown-toggle"
                data-toggle="dropdown"
                aria-expanded="false"
                style={{ display: "none" }}
              >
                <i className="iconmenu7">
                  <svg width="18" height="16" viewBox="0 0 18 16" fill="none">
                    <path
                      d="m13 5 3 3-3 3M1 1h14M1 8h9m-9 7h14"
                      stroke="#000"
                      strokeWidth={2}
                      strokeLinecap="round"
                    />
                  </svg>
                </i>
              </a>

              <ul className="dropdown-menu" dropdown-menu-right="true">
                <li>
                  <a
                    type="button"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Refresh"
                  >
                    <i className="icon-loop3"></i> Refresh
                  </a>
                </li>
                <li>
                  <a
                    type="button"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Dowload PDF"
                  >
                    <i className="icon-printer"></i>Dowload PDF
                  </a>
                </li>
                <li>
                  <a
                    type="button"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Dowload All PDF"
                  >
                    <i className="icon-printer"></i>Dowload PDF (All rows)
                  </a>
                </li>
                <li>
                  <a
                    type="button"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Dowload EXCEL"
                  >
                    <i className="icon-printer"></i>Dowload EXCEL
                  </a>
                </li>

                <li>
                  <a
                    type="button"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Dowload All Excel"
                  >
                    <i className="icon-printer"></i>Dowload Excel(All rows)
                  </a>
                </li>
              </ul>
            </li>

            <li>
              <a
                className=""
                // onclick=CollapseSearch(' + + ')
                style={{ display: "none" }}
                title="Search"
              >
                <i className="iconsearch4">
                  <svg width="36" height="37" viewBox="0 0 36 37" fill="none">
                    <path
                      d="M33.8 36.95 20.65 23.8c-1 .867-2.167 1.542-3.5 2.025a12.38 12.38 0 0 1-4.25.725c-3.6 0-6.65-1.25-9.15-3.75S0 17.283 0 13.75C0 10.217 1.25 7.2 3.75 4.7 6.25 2.2 9.283.95 12.85.95c3.533 0 6.542 1.25 9.025 3.75 2.483 2.5 3.725 5.517 3.725 9.05 0 1.433-.233 2.817-.7 4.15a12.694 12.694 0 0 1-2.1 3.75L36 34.75l-2.2 2.2Zm-20.95-13.4c2.7 0 5-.958 6.9-2.875 1.9-1.917 2.85-4.225 2.85-6.925s-.95-5.008-2.85-6.925c-1.9-1.917-4.2-2.875-6.9-2.875-2.733 0-5.058.958-6.975 2.875C3.958 8.742 3 11.05 3 13.75s.958 5.008 2.875 6.925c1.917 1.917 4.242 2.875 6.975 2.875Z"
                      fill="#000"
                    />
                  </svg>
                </i>
              </a>
            </li>

            <li>
              <a
                style={{ position: "relative" }}
                data-action="collapse"
                className="collapse3"
              >
                <i>
                  <svg width="24" height="24" viewBox="0 0 12 12" fill="none">
                    <path
                      d="M9.38 2.62a4.781 4.781 0 1 0-6.76 6.76 4.781 4.781 0 1 0 6.76-6.76Zm-.4 6.361a4.219 4.219 0 1 1-5.97-5.962 4.219 4.219 0 0 1 5.97 5.962ZM7.697 3.928a.272.272 0 0 1 0 .394l-1.5 1.5a.272.272 0 0 1-.394 0l-1.5-1.5a.278.278 0 1 1 .394-.394L6 5.227l1.303-1.299a.272.272 0 0 1 .394 0Zm0 2.625a.272.272 0 0 1 0 .394l-1.5 1.5a.272.272 0 0 1-.394 0l-1.5-1.5a.278.278 0 1 1 .394-.394L6 7.852l1.303-1.299a.272.272 0 0 1 .394 0Z"
                      fill="#000"
                    />
                  </svg>
                </i>
              </a>
            </li>
          </ul>
        </div>
      </div>
      <div className="panel-heading" style={{ display: "none" }}>
        <span className="text-danger"></span>
        <h6 className="panel-title">
          <span id="spnLOD_ "></span>
        </h6>
        <div className="heading-elements">
          <ul className="icons-list navbarpad">
            <li className="">
              <a
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="Add"
                className="btn"
                style={{ display: "none" }}
              >
                <i className="iconplus2">
                  <svg width="24" height="24" viewBox="0 0 24 24" fill="none">
                    <g clipPath="url(#a)" fill="#374957">
                      <path d="M7 0H4a4 4 0 0 0-4 4v3a4 4 0 0 0 4 4h3a4 4 0 0 0 4-4V4a4 4 0 0 0-4-4Zm2 7a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2V4a2 2 0 0 1 2-2h3a2 2 0 0 1 2 2v3Zm-2 6H4a4 4 0 0 0-4 4v3a4 4 0 0 0 4 4h3a4 4 0 0 0 4-4v-3a4 4 0 0 0-4-4Zm2 7a2 2 0 0 1-2 2H4a2 2 0 0 1-2-2v-3a2 2 0 0 1 2-2h3a2 2 0 0 1 2 2v3Zm11-7h-3a4 4 0 0 0-4 4v3a4 4 0 0 0 4 4h3a4 4 0 0 0 4-4v-3a4 4 0 0 0-4-4Zm2 7a2 2 0 0 1-2 2h-3a2 2 0 0 1-2-2v-3a2 2 0 0 1 2-2h3a2 2 0 0 1 2 2v3ZM14 7h3v3a1 1 0 0 0 2 0V7h3a1 1 0 1 0 0-2h-3V2a1 1 0 1 0-2 0v3h-3a1 1 0 0 0 0 2Z" />
                    </g>
                    <defs>
                      <clipPath id="a">
                        <path fill="#fff" d="M0 0h24v24H0z" />
                      </clipPath>
                    </defs>
                  </svg>
                </i>
              </a>
            </li>
            <li className="dropdown">
              <a
                type="button"
                className="dropdown-toggle"
                data-toggle="dropdown"
                aria-expanded="false"
                style={{ display: "none" }}
              >
                <i className="iconmenu7">
                  <svg width="18" height="16" viewBox="0 0 18 16" fill="none">
                    <path
                      d="m13 5 3 3-3 3M1 1h14M1 8h9m-9 7h14"
                      stroke="#000"
                      strokeWidth={2}
                      strokeLinecap="round"
                    />
                  </svg>
                </i>
              </a>
              <ul className="dropdown-menu" dropdown-menu-right="true">
                <li>
                  <a
                    type="button"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Refresh"
                  >
                    <i className="icon-loop3"></i> Refresh
                  </a>
                </li>
                <li>
                  <a
                    type="button"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Dowload PDF"
                  >
                    <i className="icon-printer"></i> Dowload PDF
                  </a>
                </li>

                <li>
                  <a
                    type="button"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Dowload All PDF"
                  >
                    <i className="icon-printer"></i> Dowload PDF (All rows)
                  </a>
                </li>
                <li>
                  <a
                    type="button"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Dowload EXCEL"
                  >
                    <i className="icon-printer"></i> Dowload EXCEL
                  </a>
                </li>

                <li>
                  <a
                    type="button"
                    data-toggle="tooltip"
                    data-placement="top"
                    title="Dowload All Excel"
                  >
                    <i className="icon-printer"></i> Dowload Excel(All rows)
                  </a>
                </li>
              </ul>
            </li>
            <li>
              <a
                className=""
                // onclick=CollapseSearch(' +  + ')
                style={{ display: "none" }}
              >
                <i className="iconsearch4">
                  <svg width="36" height="37" viewBox="0 0 36 37" fill="none">
                    <path
                      d="M33.8 36.95 20.65 23.8c-1 .867-2.167 1.542-3.5 2.025a12.38 12.38 0 0 1-4.25.725c-3.6 0-6.65-1.25-9.15-3.75S0 17.283 0 13.75C0 10.217 1.25 7.2 3.75 4.7 6.25 2.2 9.283.95 12.85.95c3.533 0 6.542 1.25 9.025 3.75 2.483 2.5 3.725 5.517 3.725 9.05 0 1.433-.233 2.817-.7 4.15a12.694 12.694 0 0 1-2.1 3.75L36 34.75l-2.2 2.2Zm-20.95-13.4c2.7 0 5-.958 6.9-2.875 1.9-1.917 2.85-4.225 2.85-6.925s-.95-5.008-2.85-6.925c-1.9-1.917-4.2-2.875-6.9-2.875-2.733 0-5.058.958-6.975 2.875C3.958 8.742 3 11.05 3 13.75s.958 5.008 2.875 6.925c1.917 1.917 4.242 2.875 6.975 2.875Z"
                      fill="#000"
                    />
                  </svg>
                </i>
              </a>
            </li>

            <li>
              <a
                style={{ position: "relative" }}
                data-action="collapse"
                className="collapse3"
              >
                <i>
                  <svg width="24" height="24" viewBox="0 0 12 12" fill="none">
                    <path
                      d="M9.38 2.62a4.781 4.781 0 1 0-6.76 6.76 4.781 4.781 0 1 0 6.76-6.76Zm-.4 6.361a4.219 4.219 0 1 1-5.97-5.962 4.219 4.219 0 0 1 5.97 5.962ZM7.697 3.928a.272.272 0 0 1 0 .394l-1.5 1.5a.272.272 0 0 1-.394 0l-1.5-1.5a.278.278 0 1 1 .394-.394L6 5.227l1.303-1.299a.272.272 0 0 1 .394 0Zm0 2.625a.272.272 0 0 1 0 .394l-1.5 1.5a.272.272 0 0 1-.394 0l-1.5-1.5a.278.278 0 1 1 .394-.394L6 7.852l1.303-1.299a.272.272 0 0 1 .394 0Z"
                      fill="#000"
                    />
                  </svg>
                </i>
              </a>
            </li>
          </ul>
        </div>
      </div>
    </>
  );
};

export default RepeaterGridHeader;
