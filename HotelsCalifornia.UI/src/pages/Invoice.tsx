import { useEffect, useState } from "react"
import type { Invoice } from "../types/Invoice";
import { invoiceService } from "../services/invoiceService";
import { useAuth } from "../context/AuthContext";
import EmptyState from "../components/EmptyState";
import InvoiceCard from "../components/InvoiceCard";


export default function Invoice() 
{
    const { user } = useAuth();
    const [invoiceList, setInvoiceList] = useState<Invoice[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [unpaid, setUnpaid] = useState(false);

    useEffect(() => 
    {
        user === null ? (
            <></>

        ) : (
            user.role === "Admin" ? (
                invoiceService.getAllInvoices()
                    .then((data) => setInvoiceList(data))
                    .catch((err) => setError(err.message ?? 'Failed to load Invoice'))
                    .finally(() => setIsLoading(false))
            ) : (
                invoiceService.getInvoicesByMemberId(user.userId)
                    .then((data) => setInvoiceList(data))
                    .catch((err) => setError(err.message ?? 'Failed to load Invoice'))
                    .finally(() => setIsLoading(false))
            )
        )

    }, [])

    if (isLoading) return <main><p>Loading products. . . </p></main>
    if (error) return <main><p style={{ color: 'red' }}>{error}</p></main>
    const filtered = invoiceList.filter((p) => p.isPaid === unpaid)

    function toggleUnpaid()
    {
        setUnpaid(!unpaid)
    }

    return (
        <main>
            {
                unpaid ? (
                    filtered.length === 0 ? (
                        invoiceList.length === 0 ? (
                            <EmptyState message={'You have no invoices'} />
                        ) : (
                            <>
                                <EmptyState message={'You have no outstanding invoices'} />
                                <button onClick={toggleUnpaid}>View all Invoices</button>
                            </>
                        )
                    ) : (
                        <>
                            <section>
                                {filtered.map((p) => (
                                    <InvoiceCard key={p.invoiceId} invoice={p} />
                                ))}

                            </section>
                            <button onClick={toggleUnpaid}>View all Invoices</button>
                        </>
                    )

                ) : (
                    invoiceList.length === 0 ? (
                        <EmptyState message={'You have no invoices'} />

                    ) : (
                        <>
                            <section>
                                {filtered.map((p) => (
                                    <InvoiceCard key={p.invoiceId} invoice={p} />
                                ))}

                            </section>
                            <button onClick={toggleUnpaid}>View unpaid only</button>
                        </>
                    )
                )

            }



        </main>
    )
}
