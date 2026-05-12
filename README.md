# Hotels-California
## Overview
This website is used for booking hotel rooms for multiple hotels, as well as access for managers to view information about their particular hotel.

## Table Structures
### Hotel
- **Hotel Id (PK)**
- Name 
- Description
- Address

### Invoice
- **Invoice ID (PK)**
- Member ID (FK, optional)
- Reservation ID (FK, one fee to many reservations)
- IsPaid

### InvoiceLineItem
- **Invoice Line Item ID (PK)**
- Invoice ID (FK)
- Amount
- Description

### Reservation
- **ReservationId (PK)**
- MemberId (FK, optional)
- RoomId (FK)
- HotelId (FK)
- CheckInTime
- CheckOutTime
- DriversLicense
- Email
- PhoneNumber
- IsCanceled
- Member
- Room

### Room
- **PK: Composite key (RoomNumber, HotelID)**
- RoomNumber
- HotelId (FK)
- DailyRate
- NumBeds
- Description

### User
- **Id (PK)**
- Username
- PasswordHash

### Member
- **MemberId (PK)**
- Username
- PasswordHash
- InBLOCKLIST
- Membername
- Reservations
- LicenseNumber (Driver’s License)
- Email
- PhoneNumber

### Manager
- **ManagerId (PK)**
- HotelId (FK)
- Username
- PasswordHash

### Admin
- **AdminId (PK)**
- Username
- PasswordHash

## Currently Working Features
- CRUD Hotels
- CRUD Invoices
- CRUD InvoiceLineItem
- CRUD Reservation
- CRUD RoomService
- CRUD Users
- Validate reservation information
- Login System (Authentication & Authorization)
- User-friendly GUI

## Concepts applied on this project
### Overall
- 3-tier architecture (Controller, Service, Data)
- Dependency Injection
- Authentication and Authorization
- Middleware (GlobalExceptionHandler)
- Exception Throwing and Handling
- Promise/Async/Await
- DTOs
- Single Page Application (Using React)
