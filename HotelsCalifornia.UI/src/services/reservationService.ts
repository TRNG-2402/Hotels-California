import { api } from "./api";
import type { Reservation } from "../types/Reservation";

export const roomService = {
    async getAllReservations(): Promise<Reservation[]>
    {
        const response = await api.get<Reservation[]>('/Reservation');
        return response.data;
    }
}