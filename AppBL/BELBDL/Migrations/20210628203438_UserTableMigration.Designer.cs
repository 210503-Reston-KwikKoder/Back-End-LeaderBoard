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
    [Migration("20210628203438_UserTableMigration")]
    partial class UserTableMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "6.0.0-preview.5.21301.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BELBModels.Category", b =>
                {
                    b.Property<int>("CId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.HasKey("CId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BELBModels.LeaderBoard", b =>
                {
                    b.Property<string>("AuthId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CatID")
                        .HasColumnType("int");

                    b.Property<double>("AverageAcc")
                        .HasColumnType("float");

                    b.Property<double>("AverageWPM")
                        .HasColumnType("float");

                    b.HasKey("AuthId", "CatID");

                    b.ToTable("LeaderBoards");
                });

            modelBuilder.Entity("BELBModels.User", b =>
                {
                    b.Property<string>("AuthId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuthId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
