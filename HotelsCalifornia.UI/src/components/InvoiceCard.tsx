import type { Invoice } from "../types/Invoice"
import { Link } from "react-router-dom"

interface InvoiceCardProps
{
    invoice: Invoice
}

export default function InvoiceCard({ invoice }: InvoiceCardProps)
{
    return (
        <div className={StyleSheet.card}>
            <ul />

            <p>{invoice.isPaid}</p>
        </div>
    )
}
