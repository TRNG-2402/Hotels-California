import { api } from "./api";
import type { Reservation } from "../types/Reservation";

export const reservationService = {
    async getAllReservations(): Promise<Reservation[]>
    {
        const response = await api.get<Reservation[]>('/Reservation');
        return response.data;
    },

    async getReservationsByMemberId(memberId: number): Promise<Reservation[]>
    {
        const response = await api.get<Reservation[]>(`/Reservation/reservation/member/${memberId}`);
        return response.data;
    },

    async getReservationsByHotelId(hotelId: number): Promise<Reservation[]>
    {
        const response = await api.get<Reservation[]>(`/Reservation/reservation/hotel/${hotelId}`);
        return response.data;
    },

    async createReservation(data: {
        memberId: number;
        roomId: number;
        hotelId: number;
        checkInTime: string;
        checkOutTime: string;
        driversLicense: string;
        email: string;
        phoneNumber: string;
    })
    {
        const response = await api.post("/Reservation", data);
        return response.data;
    }
}
