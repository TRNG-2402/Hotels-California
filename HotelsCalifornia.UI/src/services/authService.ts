import { api } from "./api";
import type { LoginResponse, LoginRequest } from "../types/Auth";

export const authService = {
    async Login(creds: LoginRequest): Promise<LoginResponse>
    {
        //TODO Change address for auth?
        const response = await api.post<LoginResponse>('/Auth/login', creds);
        return response.data;
    }
}

