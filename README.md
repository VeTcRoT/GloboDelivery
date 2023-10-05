# GloboDelivery Logistics Website

## Overview

GloboDelivery is a logistics website designed to provide users with an easy and efficient way to browse available vans, make payments for transportation services, and track the status of their deliveries. This README file is intended to guide through the key features, technologies used, and the overall structure of the GloboDelivery project.

## Functionality

### User

#### 1. Van Browsing
- Users can browse available vans and view details such as capacity, availability, and pricing.
- Van listings are dynamically updated as new vans become available.

#### 2. Transportation Payment
- Users can make payments for transportation services through integrated payment systems, ensuring secure and convenient transactions.

#### 3. Transportation Status Tracking
- Users can track the status of their transportation.
- Status updates will be visible in user account.

### Managers

#### 1. Van Management
- Managers can add new vans to the system, specifying details like capacity, availability, and pricing.
- They can also edit or remove existing van listings.

#### 2. Delivery Status Updates
- Managers have the ability to update the status of deliveries, providing users with real-time information on their shipments.

#### 3. User Communication
- Managers can communicate with users through the integrated live chat functionality.

## Features

- **Live Chat Integration**: GloboDelivery uses SignalR to provide real-time chat functionality between users and managers, ensuring efficient communication and quick responses to inquiries.

- **Payment System Integration**: Seamlessly integrate a payment system to allow users to make secure and convenient transactions for the transportation services.

- **Address Validation Service**: Utilize the Smarty address validation service to ensure accurate and reliable address information, reducing delivery errors.

## Technologies Used

- **Frontend**: HTML, CSS, JavaScript

- **Backend**: C#, ASP.NET Core 6, SignalR, Entity Framework Core

- **Database**: MSSQL

## Getting Started

1. Clone the repository to your local machine.

   ```bash
   git clone https://github.com/VeTcRoT/GloboDelivery.git
   ```

2. Set up your development environment. Make sure you have the necessary tools and packages installed.

3. Configure the project settings and dependencies. You may need to create configuration files for sensitive information like API keys and database connection strings.

4. Install the required packages using your package manager of choice (e.g., npm, yarn, NuGet).

5. Run the application locally for development and testing.

   ```bash
   dotnet run
   ```

6. Access the application in your web browser at `http://localhost:port`.

## Acknowledgments

- [SignalR Documentation](https://docs.microsoft.com/en-us/aspnet/core/signalr/introduction?view=aspnetcore-6.0)
- [Smarty Address Validation Service](https://www.smartystreets.com/docs)
