import { api } from "./api";
import type { Room } from "../types/Room";

export const roomService = {
    // sends GET to http://localhost:xxxx/api/Room
    async getAllRooms(): Promise<Room[]>
    {
        const response = await api.get<Room[]>('/room');
        return response.data;
    },
    async getRoomById(id: number): Promise<Room> {
    
            const response = await api.get<Room>(`/Room/id/${id}`);
            return response.data;
    },

    async getRoomsByHotelId(hotelId: number): Promise<Room[]>
    {
        const response = await api.get<Room[]>(`Room/room/hotel/${hotelId}`);
        return response.data;
    }
}
