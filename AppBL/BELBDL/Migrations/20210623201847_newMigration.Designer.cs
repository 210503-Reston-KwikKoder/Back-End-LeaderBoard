﻿// <auto-generated />
using BELBDL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BELBDL.Migrations
{
    [DbContext(typeof(BELBDBContext))]
    [Migration("20210623201847_newMigration")]
    partial class newMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "6.0.0-preview.5.21301.9")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("BELBModels.Category", b =>
                {
                    b.Property<int>("CId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Name")
                        .HasColumnType("integer");

                    b.HasKey("CId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BELBModels.LeaderBoard", b =>
                {
                    b.Property<string>("AuthId")
                        .HasColumnType("text");

                    b.Property<int>("CatID")
                        .HasColumnType("integer");

                    b.Property<double>("AverageAcc")
                        .HasColumnType("double precision");

                    b.Property<double>("AverageWPM")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("AuthId", "CatID");

                    b.ToTable("LeaderBoards");
                });
#pragma warning restore 612, 618
        }
    }
}