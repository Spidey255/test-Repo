"use client";

import React, { useCallback, useEffect } from "react";

import { config } from "@/constants/config";
import type {
  IAppResponse,
  IGlobalStateValues,
  ServiceElementData,
  UIElement,
} from "@/constants/types";
import { accessMandatory } from "@/helpers/utils";
import { useGridStore } from "@/store/useGridStore";
import { useGeneralStore } from "@/store/useStore";
import { useUserStore } from "@/store/useUserStore";
import { getFormOnLoadData } from "@/helpers/axiosHelper";

interface IStoreProps {
  data: UIElement[];
}

const Store: React.FC<IStoreProps> = ({ data }) => {
  // const state = useGeneralStore((store) => store.state);
  // console.log(state);
  const setInitialState = useGeneralStore((store) => store.setInitialState);
  const setUIElementState = useGeneralStore((store) => store.setUIElementState);
  const setGridHeader = useGridStore((store) => store.setGridHeader);
  const setPagination = useGridStore((store) => store.setPagination);
  const setGridLoadingState = useGridStore(
    (store) => store.setGridLoadingState
  );
  const setGridDynamicState = useGridStore(
    (store) => store.setGridDynamicState
  );
  const setGridElementMapper = useGridStore(
    (store) => store.setGridElementMapper
  );
  const slotId = useUserStore((store) => store.slotId);

  const handleGetFormLoadData = useCallback(async () => {
    if (!data.length || !slotId) return;

    try {
      const formLoadDataApi = data.filter(
        (f) => f["Action"]?.toLowerCase() === "formonload"
      );
      if (!formLoadDataApi.length) return;

      const { Rows: formLoadData } = await getFormOnLoadData<IAppResponse>(
        slotId,
        formLoadDataApi[0]
      );

      // GLOBAL DATA CONTROLS STATE INITIALIZE ON LOAD
      const inputs: Record<string, IGlobalStateValues> = data
        .filter((f) => config.DataControlIds.includes(f.ControlId))
        .reduce((acc, curr) => {
          const existData = formLoadData?.find(
            (f) =>
              f.ElementName?.toLowerCase() ===
              curr["ElementName"]?.toLowerCase()
          );

          return {
            ...acc,
            [curr.ElementName]: {
              value: existData
                ? existData.Value
                : String(curr["CurrValue"] || ""),
              type: curr.ControlType,
              required: curr?.ElementControlProperty?.some((s) =>
                accessMandatory(s)
              ),
              isVisible: existData?.Visible !== "false",
              EDT: existData?.EDT || curr["EDT"] || "",
            },
          };
        }, {});

      // GLOBAL UI CONTROLS STATE INITIALIZE ON LOAD
      const uiControls: Record<string, ServiceElementData> = data
        .filter((f) => !config.DataControlIds.includes(f.ControlId))
        .reduce((acc, curr) => {
          const existData = formLoadData?.find(
            (f) =>
              f.ElementName?.toLowerCase() ===
              curr["ElementName"]?.toLowerCase()
          );

          return {
            ...acc,
            [curr.ElementName]: {
              visible: existData?.Visible !== "false",
              ElementName: curr.ElementName,
              ElementId: curr.ElementId,
              EDT: existData?.EDT,
            },
          };
        }, {});

      // GRID LOGICS
      const gridColumns = data.filter(
        (f) => f.ElementName === "UI_GridColumns"
      );

      const gridColumnsValue: Record<string, UIElement[]> = {};

      gridColumns.forEach((f) => {
        const matchedGridUIColumns = data.filter(
          (d) => d.UIElementid === f.UIElementid
        );

        gridColumnsValue[f.ParentElementId || ""] = matchedGridUIColumns.filter(
          (f) => f["ElementName"] !== "UI_GridColumns"
        );
      });

      setGridHeader(gridColumnsValue);

      // GRID FORM ON LOAD LOGICS
      const gridData = data.filter((f) => f.ControlId === 11);

      const gridElementMapper: Record<
        string,
        { uiElementId: string; elementName: string }
      > = {};

      gridData.forEach((m) => {
        const existData = formLoadData?.find(
          (f) =>
            f?.ElementName?.toLowerCase() === m["ElementName"]?.toLowerCase()
        );

        if (existData && existData["Child"]) {
          setGridDynamicState(m["UIElementid"], existData?.Child);

          existData.Child?.map((row) => {
            row.Child?.forEach((rowData) => {
              const elementData = data.find(
                (d) => d.ElementName === rowData.ElementName
              );

              inputs[`${m.ElementName}+${row.RwId}+${rowData.ElementName}`] = {
                value: (typeof rowData["Value"] !== "object"
                  ? rowData["Value"]
                    ? rowData["Value"]
                    : ""
                  : "") as string,
                EDT: rowData?.EDT || elementData?.EDT,
              };
            });
          });
        }

        setGridLoadingState(m["UIElementid"], false);
        setPagination(
          m["UIElementid"],
          1,
          m["RowsPerPage"] || 5,
          existData?.Child?.length || 0
        );
        gridElementMapper[m["ElementName"]] = {
          elementName: m.ElementName,
          uiElementId: m.UIElementid,
        };
      });

      setInitialState(inputs);
      setUIElementState(uiControls);
      setGridElementMapper(gridElementMapper);
    } catch (error) {
      console.log(error);
    }
  }, [
    data,
    slotId,
    setGridHeader,
    setInitialState,
    setUIElementState,
    setGridElementMapper,
    setGridLoadingState,
    setPagination,
    setGridDynamicState,
  ]);

  useEffect(() => {
    handleGetFormLoadData();
  }, [handleGetFormLoadData]);

  return null;
};

export default Store;
