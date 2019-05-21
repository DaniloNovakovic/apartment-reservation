# ApartmentReservation

Apartment Reservation is a sample application built using ASP.NET Core and Entity Framework Core. 

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or 2019](https://www.visualstudio.com/downloads/)
* [.NET Core SDK 2.1](https://www.microsoft.com/net/download/dotnet-core/2.1)

### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. At the root directory, restore required packages by running:
     ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build
     ```
  4. Next, within the `ApartmentReservation.WebUI\ClientApp` directory, launch the front end by running:
     ```
     npm start
     ```
  5. Once the front end has started, within the `Northwind.WebUI` directory, launch the back end by running:
     ```
	 dotnet run
	 ```

## Technologies
* .NET Core 2.1
* ASP.NET Core 2.1
* Entity Framework Core 2.1

