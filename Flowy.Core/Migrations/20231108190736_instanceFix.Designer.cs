﻿// <auto-generated />
using System;
using Flowy.Core.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Flowy.Core.Migrations
{
    [DbContext(typeof(FlowyContext))]
    [Migration("20231108190736_instanceFix")]
    partial class instanceFix
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Flowy.Core.Models.Draft", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<long>("IdScope")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Schema")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdScope");

                    b.ToTable("Drafts");
                });

            modelBuilder.Entity("Flowy.Core.Models.DraftTrack", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("EventAt")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("IdDraft")
                        .HasColumnType("bigint");

                    b.Property<string>("Operation")
                        .HasColumnType("longtext");

                    b.Property<string>("SchemaBackup")
                        .HasColumnType("longtext");

                    b.Property<string>("UserIdentifier")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdDraft");

                    b.ToTable("DraftTracks");
                });

            modelBuilder.Entity("Flowy.Core.Models.Process", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("IdScope")
                        .HasColumnType("bigint");

                    b.Property<long>("KeyProcessDefinition")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("IdScope");

                    b.ToTable("Processes");
                });

            modelBuilder.Entity("Flowy.Core.Models.Scope", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<long>("IdTenant")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("IdTenant");

                    b.ToTable("Scopes");
                });

            modelBuilder.Entity("Flowy.Core.Models.Tenant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("Flowy.Core.Models.Draft", b =>
                {
                    b.HasOne("Flowy.Core.Models.Scope", "Scope")
                        .WithMany()
                        .HasForeignKey("IdScope")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scope");
                });

            modelBuilder.Entity("Flowy.Core.Models.DraftTrack", b =>
                {
                    b.HasOne("Flowy.Core.Models.Draft", "Draft")
                        .WithMany()
                        .HasForeignKey("IdDraft")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Draft");
                });

            modelBuilder.Entity("Flowy.Core.Models.Process", b =>
                {
                    b.HasOne("Flowy.Core.Models.Scope", "Scope")
                        .WithMany()
                        .HasForeignKey("IdScope")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Scope");
                });

            modelBuilder.Entity("Flowy.Core.Models.Scope", b =>
                {
                    b.HasOne("Flowy.Core.Models.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("IdTenant")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });
#pragma warning restore 612, 618
        }
    }
}
