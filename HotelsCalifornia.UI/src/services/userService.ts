import { api } from "./api";
import type { Manager, NewManagerDTO, UpdateUserDTO } from "../types/User";



export const userService = {
    async postNewManager(newManager: NewManagerDTO): Promise<Manager> {
        const response = await api.post<Manager>(`User/Manager`, newManager);
        return response.data;
    },

    async updateUser(updatedUser: UpdateUserDTO): Promise<Manager> {
        const response = await api.put<Manager>(`/User`, updatedUser);
        return response.data;
    },
}