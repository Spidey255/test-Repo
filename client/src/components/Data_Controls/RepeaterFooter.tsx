import React from "react";


import type { UIElement
 } from "../../constants/types";
 import { useGridStore } from "../../store/useGridStore";
 import { useGeneralStore } from "../../store/useStore";
const RepeaterGridFooter: React.FC<{ element: UIElement }> = ({ element }) => {
  const pagination = useGridStore(
    (store) => store.pagination?.[String(element["ParentElementId"])]
  );
  const uiState = useGeneralStore(
    (store) => store.uiElementState?.[String(element?.TempElementId)]
  );
  const setCountPerPage = useGridStore((store) => store.setCountPerPage);
  const setCurrentPage = useGridStore((store) => store.setCurrentPage);
  const nextPage = useGridStore((store) => store.nextPage);
  const prevPage = useGridStore((store) => store.prevPage);
  const firstPage = useGridStore((store) => store.firstPage);
  const lastPage = useGridStore((store) => store.lastPage);

  const start = (pagination?.currentPage - 1) * pagination?.countPerPage + 1;
  const end = pagination?.currentPage * pagination?.countPerPage;

  if (uiState?.["visible"] === false) return null;

  return (
    <div className="datatable-footer">
      <div className="col-md-12 col-xs-12 col-sm-12 pt-10 pb-10">
        <div className="pull-left fullContainer grid_page_left">
          <ul className="pagination bootpag pagination-xs">
            <li>
              <label
                className="pr-10"
                style={{ marginBottom: "0", lineHeight: "22px" }}
              >
                Show Rows
              </label>
            </li>
            <li>
              <select
                className="form-control  select-size-xs"
                style={{}}
                value={pagination?.countPerPage || 5}
                onChange={(e) =>
                  element["ParentElementId"] &&
                  setCountPerPage(
                    element["ParentElementId"],
                    Number(e.target.value)
                  )
                }
              >
                {[
                  "5",
                  "10",
                  "25",
                  "50",
                  "100",
                  "200",
                  "300",
                  "500",
                  "600",
                  "700",
                  "800",
                  "900",
                  "1000",
                ].map((count, index) => (
                  <option key={index.toString()} value={count}>
                    {count}
                  </option>
                ))}
              </select>
            </li>
            <li className="pl-5">
              <label className="label bg-slate-400" id="lbl_">
                {!pagination ? (
                  "No Records Found"
                ) : (
                  <>
                    {start} - {Math.min(end, pagination?.totalItems)} of{" "}
                    {pagination?.totalItems || 0} records
                  </>
                )}
              </label>
            </li>
          </ul>
        </div>

        <div className="pull-right fullContainer grid_page_right" style={{}}>
          <ul className="pagination bootpag Counts pagination-xs pull-left">
            <li>
              <label
                className="pr-10"
                style={{ marginBottom: 0, lineHeight: "25px" }}
              >
                Page
              </label>
            </li>

            <li>
              <input
                type="number"
                min={1}
                className="input-xs text-center form-control"
                style={{ width: "35px", height: "25px" }}
                value={String(pagination?.currentPage || 1)}
                onChange={(e) =>
                  element["ParentElementId"] &&
                  setCurrentPage(
                    element["ParentElementId"],
                    Number(e.target.value)
                  )
                }
              />
            </li>

            <li>
              <label className="pl-5 pr-5" style={{ marginBottom: 0 }}></label>
            </li>
          </ul>

          <ul className="pagination bootpag pagination-xs pull-left">
            <li>
              <a
                className="btn btn-icon"
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="First"
                onClick={() =>
                  element["ParentElementId"] &&
                  firstPage(element["ParentElementId"])
                }
              >
                <i className="fa double-left">
                  <svg width="27" height="24" viewBox="0 0 27 24">
                    <path
                      d="M12 24 0 12 12 0l2.1 2.1L4.2 12l9.9 9.9L12 24Zm12.65 0-12-12 12-12 2.1 2.1-9.9 9.9 9.9 9.9-2.1 2.1Z"
                      fill="#000"
                    />
                  </svg>
                </i>
              </a>
            </li>

            <li>
              <a
                className="btn btn-icon"
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="Previous"
                onClick={() =>
                  element["ParentElementId"] &&
                  prevPage(element["ParentElementId"])
                }
              >
                <i className="fa faleft">
                  <svg width="14" height="24" viewBox="0 0 14 24">
                    <path
                      d="M11.667 23.333 0 11.667 11.667 0 13.3 1.663 3.296 11.666 13.3 21.67l-1.633 1.662Z"
                      fill="#000"
                    />
                  </svg>
                </i>
              </a>
            </li>

            <li>
              <a
                className="btn btn-icon"
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="Next"
                onClick={() =>
                  element["ParentElementId"] &&
                  nextPage(element["ParentElementId"])
                }
              >
                <i className="fa faright">
                  <svg width="14" height="24" viewBox="0 0 14 24">
                    <path
                      d="M1.633 23.333 13.3 11.667 1.633 0 0 1.663l10.004 10.004L0 21.67l1.633 1.662Z"
                      fill="#000"
                    />
                  </svg>
                </i>
              </a>
            </li>

            <li>
              <a
                className="btn btn-icon"
                type="button"
                data-toggle="tooltip"
                data-placement="top"
                title="Last"
                onClick={() =>
                  element["ParentElementId"] &&
                  lastPage(element["ParentElementId"])
                }
              >
                <i className="fa faright">
                  <svg width="28" height="24" viewBox="0 0 28 24">
                    <path
                      d="m15.3 24 12-12-12-12-2.1 2.1 9.9 9.9-9.9 9.9 2.1 2.1ZM2.65 24l12-12-12-12-2.1 2.1 9.9 9.9-9.9 9.9 2.1 2.1Z"
                      fill="#000"
                    />
                  </svg>
                </i>
              </a>
            </li>
          </ul>
        </div>
      </div>
    </div>
  );
};

export default RepeaterGridFooter;
