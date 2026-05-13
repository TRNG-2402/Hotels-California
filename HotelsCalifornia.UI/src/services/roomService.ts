import { api } from "./api";
import type { Room } from "../types/Room";

export const roomService = {
    async getAllHotels(): Promise<Room[]>
    {
        const response = await api.get<Room[]>('Room');
        return response.data;
    },
    async getRoomsByHotelId(hotelId: string)
    {
        const response = await api.get(`/api/Room/room/hotel/${hotelId}`);
        return response.data;
    }
}