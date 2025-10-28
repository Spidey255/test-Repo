import { create } from "zustand";
import type {
  IFormLoadApiResponseRowChild,
  IGlobalStateValues,
} from "../constants/types";
import { devtools } from "zustand/middleware";

type TState = Record<string, IGlobalStateValues>;

type State = {
  state: TState;
  uiElementState: Record<string, Partial<IFormLoadApiResponseRowChild>>;
};

type Action = {
  setInitialState: (initialState: TState) => void;
  updateInitialState: (state: TState) => void;
  setState: (key: string, value: string | boolean) => void;

  setUIElementState: (
    uiElementState: Record<string, Partial<IFormLoadApiResponseRowChild>>
  ) => void;
  updateUIElementState: (
    key: string,
    value: Partial<IFormLoadApiResponseRowChild>
  ) => void;

  resetState: () => void;
  resetUIElementState: () => void;
};

export const useGeneralStore = create<State & Action>()(
  devtools(
    (set) => ({
      state: {},
      setInitialState: (initialState) => set(() => ({ state: initialState })),
      updateInitialState: (updateState) =>
        set((store) => ({ state: { ...store.state, ...updateState } })),

      setState: (key, value) =>
        set((state) => ({
          state: {
            ...state.state,
            [key]: { ...state.state[key], value: value },
          },
        })),

      uiElementState: {},
      setUIElementState: (uiElementState) => set(() => ({ uiElementState })),
      updateUIElementState: (key, value) =>
        set((state) => ({
          uiElementState: {
            ...state.uiElementState,
            [key]: { ...(state.uiElementState[key] || {}), ...value },
          },
        })),

      resetState: () => set(() => ({ state: {} })),
      resetUIElementState: () => set(() => ({ uiElementState: {} })),
    }),
    { name: "GeneralStore" }
  )
);
