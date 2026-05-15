

export interface InvoiceLineItem
{
    invoiceLineItemId: number,
    invoiceId: number,
    amount: number,
    description: string
}

export interface NewInvoiceLineItem
{
    invoiceId: number,
    amount: number,
    description: string
}