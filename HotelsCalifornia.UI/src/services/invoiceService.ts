import { api } from "./api";
import type { Invoice } from "../types/Invoice";

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
    }
    // async createInvoice(invoice: Invoice): Promise<Invoice>
    // {
    //     const response = await api.post<Invoice>(`/Invoice`)
    // }



}

