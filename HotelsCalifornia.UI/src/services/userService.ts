import { api } from "./api";
import type { Manager, NewManagerDTO } from "../types/User";



export const userService = {
    async postNewManager(newManager: NewManagerDTO): Promise<Manager> {
        const response = await api.post<Manager>(`User/Manager`, newManager);
        return response.data;
    },
}