

export interface Reservation
{
    reservationId: number,
    memberId: number,
    roomId: number,
    hotelId: number,
    checkInTime: Date,
    checkOutTime: Date,
    driversLicense: string,
    email: string,
    phoneNumber: string,
    isCanceled: boolean
}