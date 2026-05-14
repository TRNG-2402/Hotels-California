
export interface Invoice
{
    invoiceId: number,
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