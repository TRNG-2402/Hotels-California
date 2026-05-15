import { api } from "./api";
import type { InvoiceLineItem, NewInvoiceLineItem } from "../types/InvoiceLineItem";

export const invoiceLineItemService = {
    async getAllInvoiceLineItems(): Promise<InvoiceLineItem[]>
    {
        const response = await api.get<InvoiceLineItem[]>('/InvoiceLineItem')
        return response.data
    },
    async getAllInvoiceLineItemsByInvoiceId(invoiceId: number): Promise<InvoiceLineItem[]>
    {
        const response = await api.get<InvoiceLineItem[]>(`/InvoiceLineItem/${invoiceId}`)
        return response.data
    },
    async getInvoiceLineItemById(invoiceId: number, id: number): Promise<InvoiceLineItem>
    {
        const response = await api.get<InvoiceLineItem>(`/InvoiceLineItem/${invoiceId}/${id}`)
        return response.data
    },
    async createInvoiceLineItem(newInvoiceLineItem: NewInvoiceLineItem): Promise<InvoiceLineItem>
    {
        const response = await api.post<InvoiceLineItem>(`/InvoiceLineItem`, newInvoiceLineItem)
        return response.data
    }
}