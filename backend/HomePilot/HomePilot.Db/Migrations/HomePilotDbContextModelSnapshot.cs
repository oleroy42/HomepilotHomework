﻿// <auto-generated />
using System;
using HomePilot.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HomePilot.Db.Migrations
{
    [DbContext(typeof(HomePilotDbContext))]
    partial class HomePilotDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HomePilot.Db.Model.AmendmentModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("EffectiveDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("LeaseId")
                        .HasColumnType("char(36)");

                    b.Property<int?>("OldRentInCents")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LeaseId");

                    b.ToTable("Amendments");
                });

            modelBuilder.Entity("HomePilot.Db.Model.LeaseModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTimeOffset?>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RentInCents")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Leases");
                });

            modelBuilder.Entity("HomePilot.Db.Model.LeaseTenantModel", b =>
                {
                    b.Property<Guid>("LeaseId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AmendmentEntryId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("AmendmentExitId")
                        .HasColumnType("char(36)");

                    b.HasKey("LeaseId", "TenantId");

                    b.HasIndex("AmendmentEntryId");

                    b.HasIndex("AmendmentExitId");

                    b.HasIndex("TenantId", "LeaseId")
                        .IsUnique();

                    b.ToTable("LeaseTenants");
                });

            modelBuilder.Entity("HomePilot.Db.Model.TenantModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("HomePilot.Db.Model.AmendmentModel", b =>
                {
                    b.HasOne("HomePilot.Db.Model.LeaseModel", "Lease")
                        .WithMany()
                        .HasForeignKey("LeaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lease");
                });

            modelBuilder.Entity("HomePilot.Db.Model.LeaseTenantModel", b =>
                {
                    b.HasOne("HomePilot.Db.Model.AmendmentModel", "AmendmentEntry")
                        .WithMany("Entries")
                        .HasForeignKey("AmendmentEntryId");

                    b.HasOne("HomePilot.Db.Model.AmendmentModel", "AmendmentExit")
                        .WithMany("Exits")
                        .HasForeignKey("AmendmentExitId");

                    b.HasOne("HomePilot.Db.Model.LeaseModel", "Lease")
                        .WithMany()
                        .HasForeignKey("LeaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomePilot.Db.Model.TenantModel", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AmendmentEntry");

                    b.Navigation("AmendmentExit");

                    b.Navigation("Lease");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("HomePilot.Db.Model.AmendmentModel", b =>
                {
                    b.Navigation("Entries");

                    b.Navigation("Exits");
                });
#pragma warning restore 612, 618
        }
    }
}
