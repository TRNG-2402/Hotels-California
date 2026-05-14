import type { Invoice } from "../types/Invoice"

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
