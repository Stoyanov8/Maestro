﻿// <auto-generated />
using Maestro.Statistics.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Maestro.Statistics.Migrations
{
    [DbContext(typeof(StatisticsDbContext))]
    [Migration("20200722181921_averageWorkTIme")]
    partial class averageWorkTIme
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Maestro.Statistics.Data.Models.AverageEmployeeWorkTime", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("AverageTime")
                        .HasColumnType("bigint");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AverageWorkTime");
                });
#pragma warning restore 612, 618
        }
    }
}
