

export interface Room
{
    id: number,
    hotelId: number,
    roomNumber: number,
    dailyRate: number,
    numBeds: number,
    description: string
}

export interface NewRoom
{
    hotelId: number,
    roomNumber: number,
    dailyRate: number,
    numBeds: number,
    description: string
}