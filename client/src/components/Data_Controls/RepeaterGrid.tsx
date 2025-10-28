import React, { useMemo, type ReactNode } from "react";
import _ from "lodash";
import type { ServiceElementData, UIElement } from "../../constants/types";
import { useGridStore } from "../../store/useGridStore";
import { useGeneralStore } from "../../store/useStore";

const RepeaterGrid: React.FC<{ element: UIElement; children: ReactNode }> = ({
  element,
  children,
}) => {
  const uiState = useGeneralStore(
    (store) => store.uiElementState?.[element.ElementId]
  );
  const gridData = useGridStore(
    (store) => store.gridDynamicState?.[element?.UIElementid]
  );
  const gridSearchText = useGridStore(
    (store) => store.gridSearchText?.[element.UIElementid!]
  );
  const gridLoadingState = useGridStore(
    (store) => store.gridLoadingState?.[element.UIElementid]
  );
  const pagination = useGridStore(
    (store) => store.pagination?.[String(element?.UIElementid)]
  );

  const data = useMemo(() => {
    if (!gridData || !gridData?.length) return;

    return gridData;
  }, [gridData]);

  // Filter data based on search text
  const filteredData = useMemo(() => {
    if (!data || !data?.length) return;

    if (!gridSearchText) return data;

    const values = useGeneralStore.getState().state;

    // Filter keys containing `element.Id`
    const filteredCurrenttGridValues = Object.entries(values).filter(([key]) =>
      key.includes(element.ElementName.toString() + "+")
    );

    const filteredValues = filteredCurrenttGridValues.filter((f) => {
      return f[1].value
        .toString()
        .toLowerCase()
        .includes(gridSearchText?.toLowerCase());
    });

    const rowIds = _.uniq(filteredValues.map((s) => s[0].split("+")[1]));

    return data.filter((f) => {
      return rowIds.includes(f.rwId!);
    });
  }, [data, element.ElementName, gridSearchText]);

  // Paginate the filtered data
  const paginatedData = useMemo(() => {
    const startIndex = (pagination?.currentPage - 1) * pagination?.countPerPage;
    const endIndex = startIndex + pagination?.countPerPage;
    return filteredData?.slice(startIndex, endIndex) || [];
  }, [filteredData, pagination]);

  if (uiState?.["visible"] === false) return null;

  // Recursively clone children and inject props
  const cloneChildrenWithProps = (
    children: ReactNode,
    rowData: ServiceElementData
  ): ReactNode => {
    return React.Children.map(children, (child) => {
      if (!React.isValidElement(child)) {
        return child;
      }

      const props = {
        rowId: rowData.RwId,
        gridName: element.ElementName,
      };

      // Check if child has element prop with ElementName
      const childProps = child.props as {
        children?: ReactNode;
        element?: { ElementName?: string };
      };

      // Override element prop if it exists with ElementName
      let updatedProps: Record<string, any> = { ...props };
      if (childProps.element?.ElementName) {
        updatedProps = {
          ...props,
          element: {
            ...childProps.element,
            ElementName: `${element.ElementName}+${rowData.RwId}+${childProps.element.ElementName}`,
          },
        };
      }

      // If child has children, recursively clone
      if (childProps.children) {
        return React.cloneElement(child, {
          ...updatedProps,
          children: cloneChildrenWithProps(childProps.children, rowData),
        } as any);
      }

      // No children, just clone with props
      return React.cloneElement(child, updatedProps as any);
    });
  };

  return (
    <div className={`row Rep-Row ${element.Css}`}>
      {gridLoadingState === undefined ? (
        <div>loading...</div>
      ) : gridLoadingState !== false ? (
        <div>loading...</div>
      ) : gridSearchText && !paginatedData.length ? (
        <div>No Search found</div>
      ) : !paginatedData?.length ? (
        <div>No data</div>
      ) : (
        paginatedData?.map((rowData) => (
          <React.Fragment key={rowData.RwId}>
            {cloneChildrenWithProps(children, rowData)}
          </React.Fragment>
        ))
      )}
    </div>
  );
};

export default RepeaterGrid;
