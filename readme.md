# Health&Med Patient Service - Hackathon Project

## Overview

This project is part of the Health&Med Hackathon, focused on providing services for patient registration, authentication, and medical consultation scheduling. The Patient Service is built in .NET Core and enables patients to register, authenticate, and book consultations with doctors.

### Main Objective
To provide an API for patients to securely register, log in, and book appointments with available doctors.

### Key Features
- Patient registration with required fields
- Patient authentication using JWT
- Search for available doctors
- Book medical consultations

## Functional Requirements
1. **Patient Registration**
   - Patients can sign up by providing the following required fields: Name, CPF, Email, and Password.
   
2. **Patient Authentication**
   - Patients can log in using their Email and Password, with JWT tokens managing authentication.

3. **Doctor Search**
   - Patients can search for doctors based on availability and other criteria.

4. **Appointment Booking**
   - Patients can book a medical consultation by selecting an available time slot for a doctor.

## Non-Functional Requirements
1. **Security**
   - Authentication is securely handled using JWT tokens.
   
2. **Concurrency**
   - The system ensures that only one patient can book a given time slot at any moment, preventing double-bookings.

## Tech Stack
- **.NET Core** for backend service
- **RabbitMQ** for sending appointment notifications
- **PostgreSQL** for database management
- **Docker** for containerization and deployment
- **Entity Framework Core** for data access
- **FluentValidation** for input validation
- **MediatR** for managing requests and command/query separation
- **JWT** for secure authentication

## Features

### 1. Patient Registration
Patients can register by providing their basic information (Name, CPF, Email, Password). Data is securely stored in PostgreSQL.

### 2. Patient Authentication
Patients can log in using their credentials (Email, Password). JWT tokens are used to secure subsequent API requests.

### 3. Doctor Search
Patients can search for doctors based on availability. The API returns a list of doctors with available time slots for booking.

### 4. Appointment Booking
Patients can book an available consultation time with a doctor. The system ensures that each slot can only be booked once, preventing double-booking conflicts.

### 5. RabbitMQ Integration
Once a consultation is booked, the service sends a message to RabbitMQ to notify the doctor about the appointment.

## Setup & Installation

### Prerequisites
- .NET 8 SDK
- Docker & Docker Compose
- RabbitMQ
- PostgreSQL

### Steps to Run

1. **Clone the repository:**
   ```bash
   git clone https://github.com/hackathon-POSTECH/Patient.git
   ```

2. **Set up environment variables:**
   Create a `.env` file based on the provided `.env.example` file, and configure the RabbitMQ and PostgreSQL settings.

3. **Run the application using Docker Compose:**
   ```bash
   docker-compose up --build
   ```

4. **Database migration:**
   After the containers are up, run the database migrations:
   ```bash
   dotnet ef database update
   ```

5. **Access the API:**
   The API will be accessible at `http://localhost:5000`.

## CI/CD Pipeline
The project integrates with a CI/CD pipeline to automate tests, builds, and deployments using tools like GitHub Actions.

## Testing
Unit tests cover key functionalities such as patient registration, authentication, doctor search, and appointment booking.

Run the tests:
```bash
dotnet test
```

## Contributors
- Health&Med Hackathon Team (FIAP SOAT students)

## License
This project is licensed under the MIT License.
