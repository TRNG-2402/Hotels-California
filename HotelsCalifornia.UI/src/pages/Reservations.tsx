import { useState } from "react";
import type { Reservation } from "../types/Reservation";
import styles from "../styles/Reservations.module.css";

export default function Reservations() {

  const [reservation, setReservation] = useState<Reservation>({
    reservationId: 0,
    memberId: 0,
    roomId: 0,
    hotelId: 0,
    checkInTime: new Date(),
    checkOutTime: new Date(),
    driversLicense: "",
    email: "",
    phoneNumber: "",
    isCanceled: false
  });

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement>
  ) => {
    const { name, value, type, checked } = e.target;

    let updatedValue: any;

    if (type === "checkbox") {
      updatedValue = checked;
    }
    else if (
      name === "memberId" ||
      name === "roomId" ||
      name === "hotelId"
    ) {
      updatedValue = Number(value);
    }
    else if (
      name === "checkInTime" ||
      name === "checkOutTime"
    ) {
      updatedValue = new Date(value);
    }
    else {
      updatedValue = value;
    }

    setReservation((prev) => ({
      ...prev,
      [name]: updatedValue,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    console.log("Reservation Submitted:", reservation);

    // axios.post(...)
  };

  return (
    <div className={styles.reservationContainer}>
      <div className={styles.reservationCard}>

        <h1 className={styles.title}>
          Hotel Reservation
        </h1>

        <form onSubmit={handleSubmit}>

          <div className={styles.formGroup}>
            <label>Hotel ID</label>
            <input
              type="number"
              name="hotelId"
              placeholder="Enter hotel ID"
              onChange={handleChange}
            />
          </div>

          <div className={styles.formGroup}>
            <label>Check In Date</label>
            <input
              type="datetime-local"
              name="checkInTime"
              onChange={handleChange}
            />
          </div>

          <div className={styles.formGroup}>
            <label>Check Out Date</label>
            <input
              type="datetime-local"
              name="checkOutTime"
              onChange={handleChange}
            />
          </div>

          <div className={styles.formGroup}>
            <label>Driver License</label>
            <input
              type="text"
              name="driversLicense"
              placeholder="Enter driver license"
              onChange={handleChange}
            />
          </div>

          <div className={styles.formGroup}>
            <label>Email</label>
            <input
              type="email"
              name="email"
              placeholder="Enter customer email"
              onChange={handleChange}
            />
          </div>

          <div className={styles.formGroup}>
            <label>Phone Number</label>
            <input
              type="text"
              name="phoneNumber"
              placeholder="Enter phone number"
              onChange={handleChange}
            />
          </div>

          <div className={styles.checkboxGroup}>
            <input
              type="checkbox"
              name="isCanceled"
              onChange={handleChange}
            />

            <label>
              Reservation Cancelled
            </label>
          </div>

          <button
            type="submit"
            className={styles.submitButton}
          >
            Create Reservation
          </button>

        </form>
      </div>
    </div>
  );
}