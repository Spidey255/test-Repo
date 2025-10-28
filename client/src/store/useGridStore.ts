import { create } from "zustand";
import { devtools } from "zustand/middleware";
import type { ServiceElementData, UIElement } from "./../constants/types";

type RecordState<T> = Record<string, T>;

type State = {
  gridHeader: Record<string, UIElement[]>;
  gridDynamicState: Record<string, ServiceElementData[]>;
  gridSearchText: RecordState<string>;
  gridLoadingState: RecordState<boolean>;
  pagination: Record<
    string,
    {
      currentPage: number;
      countPerPage: number;
      totalItems: number;
    }
  >;
  selectedRow: RecordState<string[]>;
  gridElementMapper: RecordState<{ uiElementId: string; elementName: string }>;
};

type Action = {
  setGridHeader: (gridHeader: Record<string, UIElement[]>) => void;
  setGridDynamicState: (key: string, value: ServiceElementData[]) => void;
  updateGridState: (key: string, value: ServiceElementData[]) => void;
  setGridSearchText: (key: string, value: string) => void;
  setGridLoadingState: (key: string, value: boolean) => void;

  setPagination: (
    key: string,
    currentPage: number,
    countPerPage: number,
    totalItems: number
  ) => void;
  setCurrentPage: (key: string, page: number) => void;
  nextPage: (key: string) => void;
  prevPage: (key: string) => void;
  setCountPerPage: (key: string, count: number) => void;
  setTotalItems: (key: string, totalItems: number) => void;

  firstPage: (key: string) => void;
  lastPage: (key: string) => void;

  setSelectedRow: (key: string, rowId: string) => void;

  setGridElementMapper: (
    value: RecordState<{ uiElementId: string; elementName: string }>
  ) => void;

  resetAllGridState: () => void;
};

export const useGridStore = create<State & Action>()(
  devtools(
    (set) => ({
      gridHeader: {},
      setGridHeader: (gridHeader) => set(() => ({ gridHeader })),

      gridDynamicState: {},
      setGridDynamicState: (key, value) =>
        set((state) => ({
          gridDynamicState: { ...state.gridDynamicState, [key]: value },
        })),

      updateGridState: (key, value) =>
        set((state) => {
          const newGridState = {
            ...state.gridDynamicState,
            [key]: state.gridDynamicState[key]
              ? [...state.gridDynamicState[key], ...value] // Append to existing array
              : value, // Set value if key doesn't exist
          };
          const totalItems = newGridState[key]?.length || 0; // Calculate total items for the updated grid
          const currentPage = Math.ceil(
            totalItems / state.pagination[key]?.countPerPage
          );
          return {
            gridDynamicState: newGridState,
            pagination: {
              ...state.pagination,
              [key]: {
                ...state.pagination[key],
                totalItems,
                currentPage,
              },
            },
          };
        }),

      gridLoadingState: {},
      setGridLoadingState: (key, value) =>
        set((state) => ({
          gridLoadingState: { ...state.gridLoadingState, [key]: value },
        })),

      gridSearchText: {},
      setGridSearchText: (key, value) =>
        set((state) => ({
          gridSearchText: { ...state.gridSearchText, [key]: value },
        })),

      pagination: {}, // Initialize empty pagination object
      setPagination: (key, currentPage, countPerPage, totalItems) =>
        set((state) => ({
          pagination: {
            ...state.pagination,
            [key]: { currentPage, countPerPage, totalItems },
          },
        })),
      setCurrentPage: (key, page) =>
        set((state) => ({
          pagination: {
            ...state.pagination,
            [key]: {
              ...state.pagination[key],
              currentPage: page,
            },
          },
        })),
      nextPage: (key) =>
        set((state) => ({
          pagination: {
            ...state.pagination,
            [key]: {
              ...state.pagination[key],
              currentPage: Math.min(
                state.pagination[key]?.currentPage + 1 || 1,
                Math.ceil(
                  state.pagination[key]?.totalItems /
                    state.pagination[key]?.countPerPage
                ) || 1
              ),
            },
          },
        })),
      prevPage: (key) =>
        set((state) => ({
          pagination: {
            ...state.pagination,
            [key]: {
              ...state.pagination[key],
              currentPage: Math.max(
                1,
                (state.pagination[key]?.currentPage || 1) - 1
              ),
            },
          },
        })),
      setCountPerPage: (key, count) =>
        set((state) => ({
          pagination: {
            ...state.pagination,
            [key]: {
              ...state.pagination[key],
              countPerPage: count,
              currentPage: 1,
            },
          },
        })),
      setTotalItems: (key, totalItems) =>
        set((state) => ({
          pagination: {
            ...state.pagination,
            [key]: {
              ...state.pagination[key],
              totalItems,
            },
          },
        })),
      firstPage: (key) =>
        set((state) => ({
          pagination: {
            ...state.pagination,
            [key]: {
              ...state.pagination[key],
              currentPage: 1,
            },
          },
        })),
      lastPage: (key) =>
        set((state) => ({
          pagination: {
            ...state.pagination,
            [key]: {
              ...state.pagination[key],
              currentPage: Math.ceil(
                state.pagination[key]?.totalItems /
                  state.pagination[key]?.countPerPage
              ),
            },
          },
        })),

      selectedRow: {},
      setSelectedRow: (key: string, rowId: string) =>
        set((state) => ({
          selectedRow: {
            [key]: state.selectedRow[key]
              ? state.selectedRow[key].includes(rowId)
                ? state.selectedRow[key].filter((id) => id !== rowId)
                : [...state.selectedRow[key], rowId]
              : [rowId],
          },
        })),

      gridElementMapper: {},
      setGridElementMapper: (value) =>
        set(() => ({
          gridElementMapper: value,
        })),

      resetAllGridState: () =>
        set(() => ({
          gridHeader: {},
          gridDynamicState: {},
          gridLoadingState: {},
          gridSearchText: {},
          pagination: {},
          selectedRow: {},
          gridElementMapper: {},
        })),
    }),
    { name: "GridStore" }
  )
);
