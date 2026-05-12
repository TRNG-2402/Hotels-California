import { api } from "./api";
import type { Hotel } from "../types/Hotel";

export const hotelService = {
    async getAllHotels(): Promise<Hotel[]> {

        const response = await api.get<Hotel[]>('Hotel');
        return response.data;
    },

    async deleteById(id: number): Promise<void> {
        await api.delete(`/Hotel/${id}`)
    }
}