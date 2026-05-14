import { api } from "./api";
<<<<<<< HEAD
import type { Admin, Manager, Member, NewAdminDTO, NewManagerDTO, NewMemberDTO } from "../types/User";
=======
import type { Manager, NewManagerDTO, UpdateUserDTO } from "../types/User";
>>>>>>> c78f013a29ecc0ab1c06dcca75ad61f72bc0acad



export const userService = {
    async postNewManager(newManager: NewManagerDTO): Promise<Manager> {
        const response = await api.post<Manager>(`User/Manager`, newManager);
        return response.data;
    },

<<<<<<< HEAD
    async postNewMember(newMember: NewMemberDTO): Promise<Member> {
        const response = await api.post<Member>(`User/Member`, newMember);
        return response.data;
    },

    async postNewAdmin(newAdmin: NewAdminDTO): Promise<Admin> {
        const response = await api.post<Admin>(`User/Admin`, newAdmin);
        return response.data;
    }, 
=======
    async updateUser(updatedUser: UpdateUserDTO): Promise<Manager> {
        const response = await api.put<Manager>(`/User`, updatedUser);
        return response.data;
    },
>>>>>>> c78f013a29ecc0ab1c06dcca75ad61f72bc0acad
}