﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Workshop.Domain.DataBase;

namespace Workshop.Domain.Migrations
{
    [DbContext(typeof(WorkshopContext))]
    [Migration("20171214133055_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Workshop.Domain.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("HouseNumber");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Street");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Workshop.Domain.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("Role");

                    b.Property<string>("Usersame");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Workshop.Domain.Models.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmployeeId");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("UsersAccounts");
                });

            modelBuilder.Entity("Workshop.Domain.Models.WorkshopObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<int>("Model");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Objects");
                });

            modelBuilder.Entity("Workshop.Domain.Models.WorkshopProblem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BeginDate");

                    b.Property<DateTime>("Deadline");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("ObjectId");

                    b.Property<string>("ProblemDescription");

                    b.Property<string>("ResultDescription");

                    b.HasKey("Id");

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("Workshop.Domain.Models.WorkshopTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BeginDate");

                    b.Property<DateTime>("Deadline");

                    b.Property<int>("EmployyId");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("ProblemId");

                    b.Property<string>("ResultDescription");

                    b.Property<string>("TaskDescription");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
