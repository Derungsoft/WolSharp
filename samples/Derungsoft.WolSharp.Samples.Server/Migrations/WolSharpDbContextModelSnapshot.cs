﻿// <auto-generated />
using Derungsoft.WolSharp.Samples.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Derungsoft.WolSharp.Samples.Server.Migrations
{
    [DbContext(typeof(WolSharpDbContext))]
    partial class WolSharpDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("Derungsoft.WolSharp.Samples.Server.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("MacAddress");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Device");
                });
#pragma warning restore 612, 618
        }
    }
}
