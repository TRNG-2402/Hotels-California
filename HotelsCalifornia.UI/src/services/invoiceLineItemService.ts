import { api } from "./api";
import type { InvoiceLineItem } from "../types/InvoiceLineItem";

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

}