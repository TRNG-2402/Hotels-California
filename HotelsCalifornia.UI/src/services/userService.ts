import { api } from "./api";
import type { Admin, Manager, Member, NewAdminDTO, NewManagerDTO, NewMemberDTO, UpdateUserDTO } from "../types/User";




export const userService = {
    async postNewManager(newManager: NewManagerDTO): Promise<Manager> {
        const response = await api.post<Manager>(`User/Manager`, newManager);
        return response.data;
    },

    async postNewMember(newMember: NewMemberDTO): Promise<Member> {
        const response = await api.post<Member>(`User/Member`, newMember);
        return response.data;
    },

    async postNewAdmin(newAdmin: NewAdminDTO): Promise<Admin> {
        const response = await api.post<Admin>(`User/Admin`, newAdmin);
        return response.data;
    }, 
    async updateUser(updatedUser: UpdateUserDTO): Promise<Manager> {
        const response = await api.put<Manager>(`/User`, updatedUser);
        return response.data;
    },
}