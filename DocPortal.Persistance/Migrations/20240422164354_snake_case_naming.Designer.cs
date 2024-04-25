﻿// <auto-generated />
using System;
using DocPortal.Persistance.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DocPortal.Persistance.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240422164354_snake_case_naming")]
    partial class snake_case_naming
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DocPortal.Domain.Entities.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("createdAt");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("createdBy");

                    b.Property<int>("DocumentTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("documentTypeId");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("boolean")
                        .HasColumnName("isPrivate");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("integer")
                        .HasColumnName("organizationId");

                    b.Property<DateOnly>("RegisteredDate")
                        .HasColumnType("date")
                        .HasColumnName("registeredDate");

                    b.Property<string>("RegisteredNumber")
                        .IsRequired()
                        .HasMaxLength(63)
                        .HasColumnType("character varying(63)")
                        .HasColumnName("registeredNumber");

                    b.Property<string>("StoragePath")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("storagePath");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("character varying(1023)")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updatedAt");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("updatedBy");

                    b.HasKey("Id");

                    b.HasIndex("DocumentTypeId");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("RegisteredNumber");

                    b.HasIndex("Title");

                    b.ToTable("documents", (string)null);
                });

            modelBuilder.Entity("DocPortal.Domain.Entities.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("character varying(1023)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("document_types", (string)null);
                });

            modelBuilder.Entity("DocPortal.Domain.Entities.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("created_by");

                    b.Property<string>("Details")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("details");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("PhysicalIdentity")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("physical_identity");

                    b.Property<int?>("PrimaryOrganizationId")
                        .HasColumnType("integer")
                        .HasColumnName("primary_organization_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("character varying(1023)")
                        .HasColumnName("title");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryOrganizationId");

                    b.HasIndex("Title");

                    b.ToTable("organizations", (string)null);
                });

            modelBuilder.Entity("DocPortal.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_at");

                    b.Property<int>("CreatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("created_by");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("character varying(127)")
                        .HasColumnName("firstname");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("JobPosition")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("character varying(127)")
                        .HasColumnName("job_position");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("character varying(127)")
                        .HasColumnName("lastname");

                    b.Property<string>("Login")
                        .HasMaxLength(127)
                        .HasColumnType("character varying(127)")
                        .HasColumnName("login");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(63)
                        .HasColumnType("character varying(63)")
                        .HasColumnName("password");

                    b.Property<string>("PhysicalIdentity")
                        .IsRequired()
                        .HasColumnType("char(14)")
                        .HasColumnName("physical_identity");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(31)
                        .HasColumnType("character varying(31)")
                        .HasColumnName("role");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("updated_at");

                    b.Property<int>("UpdatedBy")
                        .HasColumnType("integer")
                        .HasColumnName("updated_by");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("DocPortal.Domain.Entities.UserOrganization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("OrganizationId")
                        .HasColumnType("integer")
                        .HasColumnName("organization_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId");

                    b.ToTable("user_organizations", (string)null);
                });

            modelBuilder.Entity("DocPortal.Domain.Entities.Document", b =>
                {
                    b.HasOne("DocPortal.Domain.Entities.DocumentType", "DocumentType")
                        .WithMany("Documents")
                        .HasForeignKey("DocumentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocPortal.Domain.Entities.Organization", "Organization")
                        .WithMany("Documents")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DocumentType");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("DocPortal.Domain.Entities.Organization", b =>
                {
                    b.HasOne("DocPortal.Domain.Entities.Organization", "PrimaryOrganization")
                        .WithMany("Subordinates")
                        .HasForeignKey("PrimaryOrganizationId");

                    b.Navigation("PrimaryOrganization");
                });

            modelBuilder.Entity("DocPortal.Domain.Entities.UserOrganization", b =>
                {
                    b.HasOne("DocPortal.Domain.Entities.Organization", "AssignedOrganization")
                        .WithMany("AssignedRoles")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocPortal.Domain.Entities.User", "Admin")
                        .WithMany("UserOrganizations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");

                    b.Navigation("AssignedOrganization");
                });

            modelBuilder.Entity("DocPortal.Domain.Entities.DocumentType", b =>
                {
                    b.Navigation("Documents");
                });

            modelBuilder.Entity("DocPortal.Domain.Entities.Organization", b =>
                {
                    b.Navigation("AssignedRoles");

                    b.Navigation("Documents");

                    b.Navigation("Subordinates");
                });

            modelBuilder.Entity("DocPortal.Domain.Entities.User", b =>
                {
                    b.Navigation("UserOrganizations");
                });
#pragma warning restore 612, 618
        }
    }
}
