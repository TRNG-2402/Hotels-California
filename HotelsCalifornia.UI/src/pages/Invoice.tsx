import { useEffect, useState } from "react"
import type { Invoice } from "../types/Invoice";
import { invoiceService } from "../services/invoiceService";
import { useAuth } from "../context/AuthContext";
import EmptyState from "../components/EmptyState";
import InvoiceCard from "../components/InvoiceCard";
import { reservationService } from "../services/reservationService";
import type { Reservation } from "../types/Reservation";
import type { NewInvoiceLineItem } from "../types/InvoiceLineItem";
import { invoiceLineItemService } from "../services/invoiceLineItemService";


export default function Invoice() 
{
    const { user } = useAuth();
    const [invoiceList, setInvoiceList] = useState<Invoice[]>([]);
    const [reservationList, setReservationList] = useState<Reservation[]>([]);
    const [reservationIdList, setReservationIdList] = useState<number[]>([]);
    const [isLoading, setIsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const [unpaid, setUnpaid] = useState(false);
    const [invoiceId, setInvoiceId] = useState<number>(-1);
    const [amount, setAmount] = useState<number>(0);
    const [description, setDescription] = useState("");

    const inputStyle = {
        width: "100%",
        padding: "10px",
        marginTop: "5px",
        marginBottom: "15px"
    };

    const buttonStyle = {
        width: "100%",
        padding: "12px",
        backgroundColor: "#aa3bff",
        color: "white",
        borderRadius: "6px",
        cursor: "pointer"
    };

    useEffect(() => 
    {
        if (user === null)
        {
            <></>

        } else
        {
            if (user.role === "Manager")
            {
                invoiceService.getAllInvoices()
                    .then((data) => setInvoiceList(data))
                    .catch((err) => setError(err.message ?? 'Failed to load Invoice'))
                    .finally(() => setIsLoading(false));
                reservationService.getReservationsByHotelId(user!.hotelId!)
                    .then((data) => setReservationList(data))
                    .catch((err) => setError(err.message ?? 'Failed to load Invoice'))
                    .finally(() => setIsLoading(false));
                setReservationIdList(reservationList.map(p => p.reservationId))
                setInvoiceList(invoiceList.filter((p) => reservationIdList.includes(p.reservationId)))
            } else
            {
                invoiceService.getInvoicesByMemberId(user.userId)
                    .then((data) => setInvoiceList(data))
                    .catch((err) => setError(err.message ?? 'Failed to load Invoice'))
                    .finally(() => setIsLoading(false))
            }
        }

    }, [])

    if (isLoading) return <main><p>Loading invoices. . . </p></main>
    if (error) return <main><p style={{ color: 'red' }}>{error}</p></main>
    const filtered = invoiceList.filter((p) => p.isPaid === false)

    function toggleUnpaid()
    {
        setUnpaid(!unpaid)
    };

    const clearForm = () =>
    {
        setInvoiceId(0);
        setAmount(0);
        setDescription("");
    };

    const handleSubmit = async (
        e: React.FormEvent<HTMLFormElement>
    ) =>
    {
        e.preventDefault();

        setError(null);
        setIsLoading(true);

        try
        {
            const newInvoiceLineItem: NewInvoiceLineItem = {
                invoiceId,
                amount,
                description
            };

            await invoiceLineItemService.createInvoiceLineItem(newInvoiceLineItem);
            clearForm();
        }

        catch (err: any)
        {
            setError(
                err.response?.data?.message ||
                "Creation failed"
            );
        } finally
        {
            setIsLoading(false);
        };
    }

    return (

        <main>
            {
                !unpaid ? (
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
                                    <>

                                        <InvoiceCard key={p.id} invoice={p} />
                                        {
                                            user?.role === "Manager" ? (
                                                <>
                                                    <form onSubmit={handleSubmit}>
                                                        <label>Amount</label>
                                                        <input
                                                            type="text"
                                                            step="0.01"
                                                            placeholder="0"
                                                            value={amount}
                                                            onChange={(e) => { setAmount(Number(e.target.value)); setInvoiceId(p.id) }}
                                                            required
                                                            style={inputStyle}
                                                        />
                                                        <label>Description</label>
                                                        <input
                                                            type="text"
                                                            step="0.01"
                                                            placeholder="Surcharge reason"
                                                            value={description}
                                                            onChange={(e) => setDescription(e.target.value)}
                                                            required
                                                            style={inputStyle}
                                                        />

                                                        <button
                                                            type="submit"
                                                            disabled={isLoading}
                                                            style={buttonStyle}
                                                        >
                                                            Post Surcharge
                                                        </button>
                                                    </form>
                                                </>
                                            ) : (
                                                <>

                                                </>
                                            )
                                        }
                                    </>
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
                                {invoiceList.map((p) => (
                                    <InvoiceCard key={p.id} invoice={p} />
                                ))}

                            </section>
                            <button onClick={toggleUnpaid}>View unpaid Invoices only</button>
                        </>
                    )
                )

            }



        </main>
    )
}
