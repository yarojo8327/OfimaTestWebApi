﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ofima.TechnicalTest.Infraestructure;

#nullable disable

namespace Ofima.TechnicalTest.Infraestructure.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20230507003910_initialMigration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ofima.TechnicalTest.Infraestructure.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DatePlayed")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlayerOneId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerTwoId")
                        .HasColumnType("int");

                    b.Property<int?>("WinnerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerOneId");

                    b.HasIndex("PlayerTwoId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Ofima.TechnicalTest.Infraestructure.Models.GameMove", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("MoveId")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("RoundNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("MoveId");

                    b.HasIndex("PlayerId");

                    b.ToTable("GameMoves");
                });

            modelBuilder.Entity("Ofima.TechnicalTest.Infraestructure.Models.GameRule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("MovePlayerOneId")
                        .HasColumnType("int");

                    b.Property<int>("MovePlayerTwoId")
                        .HasColumnType("int");

                    b.Property<bool>("Tie")
                        .HasColumnType("bit");

                    b.Property<string>("Winner")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("MovePlayerOneId");

                    b.HasIndex("MovePlayerTwoId");

                    b.ToTable("GameRules");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MovePlayerOneId = 1,
                            MovePlayerTwoId = 3,
                            Tie = false,
                            Winner = "PlayerOne"
                        },
                        new
                        {
                            Id = 2,
                            MovePlayerOneId = 1,
                            MovePlayerTwoId = 2,
                            Tie = false,
                            Winner = "PlayerOne"
                        },
                        new
                        {
                            Id = 3,
                            MovePlayerOneId = 2,
                            MovePlayerTwoId = 1,
                            Tie = false,
                            Winner = "PlayerOne"
                        },
                        new
                        {
                            Id = 4,
                            MovePlayerOneId = 2,
                            MovePlayerTwoId = 3,
                            Tie = false,
                            Winner = "PlayerTwo"
                        },
                        new
                        {
                            Id = 5,
                            MovePlayerOneId = 3,
                            MovePlayerTwoId = 1,
                            Tie = false,
                            Winner = "PlayerTwo"
                        },
                        new
                        {
                            Id = 6,
                            MovePlayerOneId = 3,
                            MovePlayerTwoId = 2,
                            Tie = false,
                            Winner = "PlayerTwo"
                        },
                        new
                        {
                            Id = 7,
                            MovePlayerOneId = 1,
                            MovePlayerTwoId = 1,
                            Tie = true,
                            Winner = "Tie"
                        },
                        new
                        {
                            Id = 8,
                            MovePlayerOneId = 2,
                            MovePlayerTwoId = 2,
                            Tie = true,
                            Winner = "Tie"
                        },
                        new
                        {
                            Id = 9,
                            MovePlayerOneId = 3,
                            MovePlayerTwoId = 3,
                            Tie = true,
                            Winner = "Tie"
                        });
                });

            modelBuilder.Entity("Ofima.TechnicalTest.Infraestructure.Models.Move", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MoveName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Moves");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MoveName = "Piedra"
                        },
                        new
                        {
                            Id = 2,
                            MoveName = "Papel"
                        },
                        new
                        {
                            Id = 3,
                            MoveName = "Tijera"
                        });
                });

            modelBuilder.Entity("Ofima.TechnicalTest.Infraestructure.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Names")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Ofima.TechnicalTest.Infraestructure.Models.Game", b =>
                {
                    b.HasOne("Ofima.TechnicalTest.Infraestructure.Models.Player", "Player")
                        .WithMany("GamesOne")
                        .HasForeignKey("PlayerOneId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ofima.TechnicalTest.Infraestructure.Models.Player", "PlayerTwo")
                        .WithMany("GamesTwo")
                        .HasForeignKey("PlayerTwoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ofima.TechnicalTest.Infraestructure.Models.Player", "PlayerWinner")
                        .WithMany("GamesWinner")
                        .HasForeignKey("WinnerId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Player");

                    b.Navigation("PlayerTwo");

                    b.Navigation("PlayerWinner");
                });

            modelBuilder.Entity("Ofima.TechnicalTest.Infraestructure.Models.GameMove", b =>
                {
                    b.HasOne("Ofima.TechnicalTest.Infraestructure.Models.Game", "Game")
                        .WithMany("GameMoves")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ofima.TechnicalTest.Infraestructure.Models.Move", "Move")
                        .WithMany("GameMoves")
                        .HasForeignKey("MoveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ofima.TechnicalTest.Infraestructure.Models.Player", "Player")
                        .WithMany("GameMoves")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Move");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Ofima.TechnicalTest.Infraestructure.Models.GameRule", b =>
                {
                    b.HasOne("Ofima.TechnicalTest.Infraestructure.Models.Move", "PlayerOneMove")
                        .WithMany("GameRulesOne")
                        .HasForeignKey("MovePlayerOneId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Ofima.TechnicalTest.Infraestructure.Models.Move", "PlayerTwoMove")
                        .WithMany("GameRulesTwo")
                        .HasForeignKey("MovePlayerTwoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("PlayerOneMove");

                    b.Navigation("PlayerTwoMove");
                });

            modelBuilder.Entity("Ofima.TechnicalTest.Infraestructure.Models.Game", b =>
                {
                    b.Navigation("GameMoves");
                });

            modelBuilder.Entity("Ofima.TechnicalTest.Infraestructure.Models.Move", b =>
                {
                    b.Navigation("GameMoves");

                    b.Navigation("GameRulesOne");

                    b.Navigation("GameRulesTwo");
                });

            modelBuilder.Entity("Ofima.TechnicalTest.Infraestructure.Models.Player", b =>
                {
                    b.Navigation("GameMoves");

                    b.Navigation("GamesOne");

                    b.Navigation("GamesTwo");

                    b.Navigation("GamesWinner");
                });
#pragma warning restore 612, 618
        }
    }
}
