import type { Invoice } from "../types/Invoice"
import { Link } from "react-router-dom";
import { useEffect, useState } from "react";
import type { InvoiceLineItem } from "../types/InvoiceLineItem";
import { invoiceLineItemService } from "../services/invoiceLineItemService";
import EmptyState from "./EmptyState";
import InvoiceLineItemCard from "./InvoiceLineItemCard";

interface InvoiceCardProps
{
    invoice: Invoice;
}

export default function InvoiceCard({ invoice }: InvoiceCardProps)
{
    const [invoiceLineItemsList, setInvoiceLineItemsList] = useState<InvoiceLineItem[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const total = invoiceLineItemsList.reduce((total, currItem) =>
    {
        return total + currItem.amount;
    }, 0);

    useEffect(() =>
    {
        invoiceLineItemService.getAllInvoiceLineItemsByInvoiceId(invoice.invoiceId)
            .then((data) => setInvoiceLineItemsList(data))
            .catch((err) => setError(err.message ?? 'Failed to load InvoiceLineItems'))
            .finally(() => setIsLoading(false))
    }, [])

    if (isLoading) return <main><p>Loading invoice . . . </p></main>
    if (error) return <main><p style={{ color: 'red' }}>{error}</p></main>

    return (
        <main>
            {
                invoiceLineItemsList.length === 0 ? (
                    <EmptyState message={`No charges for "${invoice.invoiceId}"`} />
                ) : (

                    <section>
                        {invoiceLineItemsList.map((p) => (
                            <InvoiceLineItemCard key={p.invoiceLineItemId} invoiceLineItem={p} />
                        ))}
                    </section>
                )
            }
            <p>Total: <strong>${total.toFixed(2)}</strong></p>
        </main>
    )
}
