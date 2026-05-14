import { api } from "./api";
import type { Invoice, NewInvoice, UpdateInvoice } from "../types/Invoice";

export const invoiceService = {
    async getAllInvoices(): Promise<Invoice[]>
    {
        const resposne = await api.get<Invoice[]>('/Invoice');
        return resposne.data
    },
    async getInvoiceById(id: number): Promise<Invoice>
    {
        const response = await api.get<Invoice>(`/Invoice/${id}`)
        return response.data
    },
    async getInvoicesByMemberId(id: number): Promise<Invoice[]>
    {
        const response = await api.get<Invoice[]>(`/Invoice/Members/${id}`)
        return response.data
    },
    async createInvoice(newInvoice: NewInvoice): Promise<Invoice>
    {
        const response = await api.post<Invoice>(`/Invoice`, newInvoice)
        return response.data
    },
    async updateInvoice(updateInvoice: UpdateInvoice): Promise<void>
    {
        await api.patch(`/Invoice`, updateInvoice)
    }



}

