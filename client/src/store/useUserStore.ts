import { create } from "zustand";

type State = {
  slotId: string | null;
  isAuthenticating: boolean;
  error: string | null;
};

type Action = {
  setSlotId: (slotId: string) => void;
  setIsAuthenticated: (value: boolean) => void;
  setError: (error: string | null) => void;
  logout: () => void;
};

export const useUserStore = create<State & Action>((set) => ({
  slotId:
    "092DE9179-9423-494E-803D-7BC1E031F810C3BEA3AF-C9B7-4DEA-AE35-EA1C626191C00314393C-29B4-4E60-8D11-595EDAAAC42F00",
  isAuthenticating: true,
  error: null,

  setSlotId: (slotId) => set({ slotId }),
  setIsAuthenticated: (value) => set({ isAuthenticating: value }),
  setError: (error) => set({ error }),
  logout: () => {
    localStorage.removeItem("accessToken");
    set({ slotId: null, isAuthenticating: false, error: null });
  },
}));
