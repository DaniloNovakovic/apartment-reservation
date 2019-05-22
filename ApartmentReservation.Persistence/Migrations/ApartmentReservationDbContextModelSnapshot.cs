﻿// <auto-generated />
using System;
using ApartmentReservation.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApartmentReservation.Persistence.Migrations
{
    [DbContext(typeof(ApartmentReservationDbContext))]
    partial class ApartmentReservationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Address", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CityName");

                    b.Property<string>("PostalCode");

                    b.Property<string>("StreetName");

                    b.Property<string>("StreetNumber");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Administrator", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Amenity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApartmentId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.ToTable("Amenities");
                });

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Apartment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityState");

                    b.Property<int>("ApartmentType");

                    b.Property<DateTime>("CheckInTime");

                    b.Property<DateTime>("CheckOutTime");

                    b.Property<string>("GuestId");

                    b.Property<string>("HostId");

                    b.Property<string>("LocationId");

                    b.Property<int>("NumberOfGuests");

                    b.Property<int>("NumberOfRooms");

                    b.Property<double>("PricePerNight");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("HostId");

                    b.HasIndex("LocationId");

                    b.ToTable("Apartments");
                });

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApartmentId");

                    b.Property<string>("GuestId");

                    b.Property<byte>("Rating");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("GuestId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Guest", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Host", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Hosts");
                });

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Location", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressId");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApartmentId");

                    b.Property<string>("GuestId");

                    b.Property<int>("NumberOfNightsRented");

                    b.Property<DateTime>("ReservationStartDate");

                    b.Property<int>("ReservationState");

                    b.Property<double>("TotalCost");

                    b.HasKey("Id");

                    b.HasIndex("ApartmentId");

                    b.HasIndex("GuestId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Amenity", b =>
                {
                    b.HasOne("ApartmentReservation.Domain.Entities.Apartment")
                        .WithMany("Amenities")
                        .HasForeignKey("ApartmentId");
                });

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Apartment", b =>
                {
                    b.HasOne("ApartmentReservation.Domain.Entities.Guest")
                        .WithMany("RentedApartments")
                        .HasForeignKey("GuestId");

                    b.HasOne("ApartmentReservation.Domain.Entities.Host", "Host")
                        .WithMany("ApartmentsForRental")
                        .HasForeignKey("HostId");

                    b.HasOne("ApartmentReservation.Domain.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Comment", b =>
                {
                    b.HasOne("ApartmentReservation.Domain.Entities.Apartment", "Apartment")
                        .WithMany("Comments")
                        .HasForeignKey("ApartmentId");

                    b.HasOne("ApartmentReservation.Domain.Entities.Guest", "Guest")
                        .WithMany()
                        .HasForeignKey("GuestId");
                });

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Location", b =>
                {
                    b.HasOne("ApartmentReservation.Domain.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("ApartmentReservation.Domain.Entities.Reservation", b =>
                {
                    b.HasOne("ApartmentReservation.Domain.Entities.Apartment", "Apartment")
                        .WithMany("Reservations")
                        .HasForeignKey("ApartmentId");

                    b.HasOne("ApartmentReservation.Domain.Entities.Guest", "Guest")
                        .WithMany("Reservations")
                        .HasForeignKey("GuestId");
                });
#pragma warning restore 612, 618
        }
    }
}
