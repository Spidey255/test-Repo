import { create } from "zustand";
import type { TDevice } from "./../constants/types";

type State = {
  deviceViewport: TDevice;
};

type Action = {
  setDeviceViewport: (deviceViewport: TDevice) => void;
};

export const useDeviceStore = create<State & Action>((set) => ({
  deviceViewport: "xs",
  setDeviceViewport: (deviceViewport: TDevice) => set({ deviceViewport }),
}));
