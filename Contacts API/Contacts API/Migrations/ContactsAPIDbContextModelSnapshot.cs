﻿// <auto-generated />
using System;
using Contacts_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ContactsAPI.Migrations
{
    [DbContext(typeof(ContactsAPIDbContext))]
    partial class ContactsAPIDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Contacts_API.Models.Account", b =>
                {
                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Login", "Password");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Contacts_API.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountLogin")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AccountPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Phone")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AccountLogin", "AccountPassword");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Contacts_API.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<Guid>("ContactId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Contacts_API.Models.Contact", b =>
                {
                    b.HasOne("Contacts_API.Models.Account", "Account")
                        .WithMany("Contacts")
                        .HasForeignKey("AccountLogin", "AccountPassword")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Contacts_API.Models.Item", b =>
                {
                    b.HasOne("Contacts_API.Models.Contact", "Contact")
                        .WithMany("Items")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Contacts_API.Models.Account", b =>
                {
                    b.Navigation("Contacts");
                });

            modelBuilder.Entity("Contacts_API.Models.Contact", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
