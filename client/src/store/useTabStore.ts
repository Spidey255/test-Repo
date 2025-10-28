import { create } from "zustand";
import { devtools } from "zustand/middleware";
import type { UIElement } from "./../constants/types";

type State = {
  tabHeader: Record<string, UIElement[]>;
  activeTab: Record<string, string>;
};

type Action = {
  setTabHeader: (tabHeader: Record<string, UIElement[]>) => void;
  setActiveTab: (key: string, value: string) => void;
};

export const useTabStore = create<State & Action>()(
  devtools(
    (set) => ({
      tabHeader: {},
      setTabHeader: (tabHeader) => set(() => ({ tabHeader })),

      activeTab: {},
      setActiveTab: (key, value) =>
        set((store) => ({ activeTab: { ...store.activeTab, [key]: value } })),
    }),
    { name: "TabStore" }
  )
);
