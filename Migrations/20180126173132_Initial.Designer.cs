﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Triplann.Data;

namespace thoughtlesseels.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180126173132_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Triplann.Models.Computer", b =>
                {
                    b.Property<int>("ComputerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Available");

                    b.Property<string>("DecomissionedOn");

                    b.Property<int>("Malfunction");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PurchasedOn")
                        .IsRequired();

                    b.HasKey("ComputerId");

                    b.ToTable("Computer");
                });

            modelBuilder.Entity("Triplann.Models.CurrentOrder", b =>
                {
                    b.Property<int>("CurrentOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<int>("PaymentTypeId");

                    b.HasKey("CurrentOrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PaymentTypeId");

                    b.ToTable("CurrentOrder");
                });

            modelBuilder.Entity("Triplann.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedOn")
                        .IsRequired();

                    b.Property<int>("DaysInactive");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Triplann.Models.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Budget");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("DepartmentId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("Triplann.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DepartmentId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Supervisor");

                    b.HasKey("EmployeeId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Triplann.Models.EmployeeComputer", b =>
                {
                    b.Property<int>("EmployeeComputerId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ComputerId");

                    b.Property<int>("EmployeeId");

                    b.Property<DateTime>("EndDate")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("StartDate")
                        .ValueGeneratedOnAddOrUpdate();

                    b.HasKey("EmployeeComputerId");

                    b.HasIndex("ComputerId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeComputer");
                });

            modelBuilder.Entity("Triplann.Models.EmployeeTraining", b =>
                {
                    b.Property<int>("EmployeeTrainingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmployeeId");

                    b.Property<int>("TrainingProgramId");

                    b.HasKey("EmployeeTrainingId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TrainingProgramId");

                    b.ToTable("EmployeeTraining");
                });

            modelBuilder.Entity("Triplann.Models.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountNumber");

                    b.Property<int>("CustomerId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("PaymentTypeId");

                    b.HasIndex("CustomerId");

                    b.ToTable("PaymentType");
                });

            modelBuilder.Entity("Triplann.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.Property<int>("ProductTypeId");

                    b.Property<int>("Quantity");

                    b.HasKey("ProductId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Triplann.Models.ProductOrder", b =>
                {
                    b.Property<int>("ProductOrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CurrentOrderId");

                    b.Property<int>("ProductId");

                    b.HasKey("ProductOrderId");

                    b.HasIndex("CurrentOrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductOrder");
                });

            modelBuilder.Entity("Triplann.Models.ProductType", b =>
                {
                    b.Property<int>("ProductTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired();

                    b.HasKey("ProductTypeId");

                    b.ToTable("ProductType");
                });

            modelBuilder.Entity("Triplann.Models.TrainingProgram", b =>
                {
                    b.Property<int>("TrainingProgramId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EndDate")
                        .IsRequired();

                    b.Property<int>("MaxAttendees");

                    b.Property<DateTime>("StartDate");

                    b.Property<string>("TrainingProgramName")
                        .IsRequired();

                    b.HasKey("TrainingProgramId");

                    b.ToTable("TrainingProgram");
                });

            modelBuilder.Entity("Triplann.Models.CurrentOrder", b =>
                {
                    b.HasOne("Triplann.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Triplann.Models.PaymentType", "PaymentType")
                        .WithMany()
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Triplann.Models.Employee", b =>
                {
                    b.HasOne("Triplann.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Triplann.Models.EmployeeComputer", b =>
                {
                    b.HasOne("Triplann.Models.Computer", "Computer")
                        .WithMany()
                        .HasForeignKey("ComputerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Triplann.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Triplann.Models.EmployeeTraining", b =>
                {
                    b.HasOne("Triplann.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Triplann.Models.TrainingProgram", "TrainingProgram")
                        .WithMany()
                        .HasForeignKey("TrainingProgramId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Triplann.Models.PaymentType", b =>
                {
                    b.HasOne("Triplann.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Triplann.Models.Product", b =>
                {
                    b.HasOne("Triplann.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Triplann.Models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Triplann.Models.ProductOrder", b =>
                {
                    b.HasOne("Triplann.Models.CurrentOrder", "CurrentOrder")
                        .WithMany()
                        .HasForeignKey("CurrentOrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Triplann.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
