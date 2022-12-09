﻿// <auto-generated />
using System;
using Dagable.DataAccess.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dagable.DataAccess.Migrations.Migrations
{
    [DbContext(typeof(DagableDbContext))]
    partial class DagableDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Dagable.DataAccess.Migrations.DbModels.Edge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("CommTime")
                        .HasColumnType("double");

                    b.Property<int>("GraphId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCritical")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("NodeFrom")
                        .HasColumnType("int");

                    b.Property<int>("NodeTo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GraphId");

                    b.HasIndex("NodeFrom");

                    b.HasIndex("NodeTo");

                    b.ToTable("Edge", "Dagable");
                });

            modelBuilder.Entity("Dagable.DataAccess.Migrations.DbModels.Graph", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Graph", "Dagable");
                });

            modelBuilder.Entity("Dagable.DataAccess.Migrations.DbModels.Node", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("CompTime")
                        .HasColumnType("double");

                    b.Property<int>("GraphId")
                        .HasColumnType("int");

                    b.Property<string>("IsCritical")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("GraphId");

                    b.ToTable("Node", "Dagable");
                });

            modelBuilder.Entity("Dagable.DataAccess.Migrations.DbModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("User", "Dagable");
                });

            modelBuilder.Entity("Dagable.DataAccess.Migrations.DbModels.UserSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsVerticalLayout")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("NodeColor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("NodeStyle")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserSettings", "Dagable");
                });

            modelBuilder.Entity("Dagable.DataAccess.Migrations.DbModels.Edge", b =>
                {
                    b.HasOne("Dagable.DataAccess.Migrations.DbModels.Graph", "Graph")
                        .WithMany("Edges")
                        .HasForeignKey("GraphId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dagable.DataAccess.Migrations.DbModels.Node", "From")
                        .WithMany()
                        .HasForeignKey("NodeFrom")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dagable.DataAccess.Migrations.DbModels.Node", "To")
                        .WithMany()
                        .HasForeignKey("NodeTo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("From");

                    b.Navigation("Graph");

                    b.Navigation("To");
                });

            modelBuilder.Entity("Dagable.DataAccess.Migrations.DbModels.Graph", b =>
                {
                    b.HasOne("Dagable.DataAccess.Migrations.DbModels.User", "User")
                        .WithMany("Graphs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dagable.DataAccess.Migrations.DbModels.Node", b =>
                {
                    b.HasOne("Dagable.DataAccess.Migrations.DbModels.Graph", "Graph")
                        .WithMany("Nodes")
                        .HasForeignKey("GraphId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Graph");
                });

            modelBuilder.Entity("Dagable.DataAccess.Migrations.DbModels.UserSettings", b =>
                {
                    b.HasOne("Dagable.DataAccess.Migrations.DbModels.User", "User")
                        .WithOne("Settings")
                        .HasForeignKey("Dagable.DataAccess.Migrations.DbModels.UserSettings", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dagable.DataAccess.Migrations.DbModels.Graph", b =>
                {
                    b.Navigation("Edges");

                    b.Navigation("Nodes");
                });

            modelBuilder.Entity("Dagable.DataAccess.Migrations.DbModels.User", b =>
                {
                    b.Navigation("Graphs");

                    b.Navigation("Settings")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
