﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tic_Tac_Toe.Data;

namespace Tic_Tac_Toe.Migrations
{
    [DbContext(typeof(GamesDBContext))]
    [Migration("20211124050412_ChangedCoordinateNames")]
    partial class ChangedCoordinateNames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Tic_Tac_Toe.Data.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("NumberOfMoves")
                        .HasColumnType("int");

                    b.Property<int?>("Player1Id")
                        .HasColumnType("int");

                    b.Property<int?>("Player2Id")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("GameId");

                    b.HasIndex("Player1Id");

                    b.HasIndex("Player2Id");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            GameId = 1,
                            NumberOfMoves = 2,
                            Player1Id = 1,
                            Player2Id = 2,
                            Status = -1
                        });
                });

            modelBuilder.Entity("Tic_Tac_Toe.Data.Move", b =>
                {
                    b.Property<int>("MoveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ColumnNumber")
                        .HasColumnType("int");

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("RowNumber")
                        .HasColumnType("int");

                    b.HasKey("MoveId");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Moves");

                    b.HasData(
                        new
                        {
                            MoveId = 1,
                            ColumnNumber = 0,
                            GameId = 1,
                            PlayerId = 1,
                            RowNumber = 0
                        },
                        new
                        {
                            MoveId = 2,
                            ColumnNumber = 2,
                            GameId = 1,
                            PlayerId = 2,
                            RowNumber = 1
                        });
                });

            modelBuilder.Entity("Tic_Tac_Toe.Data.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            PlayerId = 1,
                            Name = "Ajay"
                        },
                        new
                        {
                            PlayerId = 2,
                            Name = "Tom"
                        });
                });

            modelBuilder.Entity("Tic_Tac_Toe.Data.Game", b =>
                {
                    b.HasOne("Tic_Tac_Toe.Data.Player", "Player1")
                        .WithMany("Player1Games")
                        .HasForeignKey("Player1Id");

                    b.HasOne("Tic_Tac_Toe.Data.Player", "Player2")
                        .WithMany("Player2Games")
                        .HasForeignKey("Player2Id");

                    b.Navigation("Player1");

                    b.Navigation("Player2");
                });

            modelBuilder.Entity("Tic_Tac_Toe.Data.Move", b =>
                {
                    b.HasOne("Tic_Tac_Toe.Data.Game", "Game")
                        .WithMany("Moves")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tic_Tac_Toe.Data.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Tic_Tac_Toe.Data.Game", b =>
                {
                    b.Navigation("Moves");
                });

            modelBuilder.Entity("Tic_Tac_Toe.Data.Player", b =>
                {
                    b.Navigation("Player1Games");

                    b.Navigation("Player2Games");
                });
#pragma warning restore 612, 618
        }
    }
}
