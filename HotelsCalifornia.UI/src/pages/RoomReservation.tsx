import { useParams, useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import { roomService } from "../services/roomService";
import { reservationService } from "../services/reservationService";
import { useAuth } from "../context/AuthContext";
import type { Room } from "../types/Room";
import type { Reservation } from "../types/Reservation";
import type { Invoice, NewInvoice } from "../types/Invoice";
import { invoiceService } from "../services/invoiceService";
import type { NewInvoiceLineItem } from "../types/InvoiceLineItem";
import { invoiceLineItemService } from "../services/invoiceLineItemService";

export default function RoomReservation()
{
    const { roomId } = useParams();
    const { user } = useAuth();

    const navigate = useNavigate();

    const [room, setRoom] = useState<Room | null>(null);
    const [checkInTime, setCheckInTime] = useState("");
    const [checkOutTime, setCheckOutTime] = useState("");
    const [driversLicense, setDriversLicense] = useState("");
    const [email, setEmail] = useState("");
    const [phoneNumber, setPhoneNumber] = useState("");
    const [error, setError] = useState<string | null>(null);

    useEffect(() =>
    {
        roomService.getRoomById(Number(roomId))
            .then(setRoom)
            .catch(() => setError("Failed to load room info"));
    }, [roomId]);

    const handleSubmit = async (e: React.SubmitEvent) =>
    {
        e.preventDefault();

        if (!room || !user) return;

        try
        {
            let reservation: Reservation = await reservationService.createReservation({
                memberId: user.userId,
                roomId: room.id,
                hotelId: room.hotelId,
                checkInTime,
                checkOutTime,
                driversLicense,
                email,
                phoneNumber
            });
            let newInvoice: NewInvoice = {
                memberId: user.userId,
                reservationId: reservation.reservationId,
                isPaid: false
            }
            let invoice: Invoice = await invoiceService.createInvoice(newInvoice)

            let newInvoiceLineItem: NewInvoiceLineItem = {
                invoiceId: invoice.invoiceId,
                amount: 150,
                description: "Price of room reservation"
            }
            await invoiceLineItemService.createInvoiceLineItem(newInvoiceLineItem)



            alert("Reservation created!");
            navigate(-1);
        } catch (err: any)
        {
            setError(err.response?.data?.message ?? "Failed to create reservation");
        }
    };

    if (!room) return <p>Loading room...</p>;

    return (
        <main>
            <h2>Reserve Room {room.roomNumber}</h2>

            {error && <p style={{ color: "red" }}>{error}</p>}

            <form onSubmit={handleSubmit}>
                <label>
                    Check In:
                    <input
                        type="datetime-local"
                        value={checkInTime}
                        onChange={(e) => setCheckInTime(e.target.value)}
                    />
                </label>

                <label>
                    Check Out:
                    <input
                        type="datetime-local"
                        value={checkOutTime}
                        onChange={(e) => setCheckOutTime(e.target.value)}
                    />
                </label>

                <label>
                    Driver's License:
                    <input
                        type="text"
                        value={driversLicense}
                        onChange={(e) => setDriversLicense(e.target.value)}
                    />
                </label>

                <label>
                    Email:
                    <input
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                    />
                </label>

                <label>
                    Phone Number:
                    <input
                        type="tel"
                        value={phoneNumber}
                        onChange={(e) => setPhoneNumber(e.target.value)}
                    />
                </label>

                <button type="submit">Reserve</button>
            </form>
        </main>
    );
}
