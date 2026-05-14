import { api } from "./api";
import type { Hotel, NewHotelDTO } from "../types/Hotel";

export const hotelService = {
    async getAllHotels(): Promise<Hotel[]> {

        const response = await api.get<Hotel[]>('Hotel');
        return response.data;
    },

    async getHotelById(id: number): Promise<Hotel> {

        const response = await api.get<Hotel>(`/Hotel/id/${id}`);
        return response.data;
    },

    async postNewHotel(newHotel: NewHotelDTO): Promise<Hotel> {

        const response = await api.post<Hotel>(`Hotel`, newHotel);
        return response.data;
    },

    async deleteById(id: number): Promise<void> {
        await api.delete(`/Hotel/${id}`)
    }
}
