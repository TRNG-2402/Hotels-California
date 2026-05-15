
export interface Invoice
{
    id: number,
    memberId: number,
    reservationId: number,
    isPaid: boolean
}
export interface NewInvoice
{
    memberId: number,
    reservationId: number,
    isPaid: boolean
}
export interface UpdateInvoice
{
    Id: number,
    IsPaid: boolean
}