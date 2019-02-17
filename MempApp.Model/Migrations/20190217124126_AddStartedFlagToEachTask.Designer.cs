﻿// <auto-generated />
using System;
using MempApp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MempApp.Model.Migrations
{
    [DbContext(typeof(MemoAppContext))]
    [Migration("20190217124126_AddStartedFlagToEachTask")]
    partial class AddStartedFlagToEachTask
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity("MempApp.Model.EachTask", b =>
                {
                    b.Property<string>("EachTaskId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CompleteFlag");

                    b.Property<string>("Content");

                    b.Property<DateTimeOffset>("DeadLine");

                    b.Property<string>("ParentEachTaskId");

                    b.Property<DateTimeOffset>("PlanDate");

                    b.Property<DateTimeOffset>("RegisteredDate");

                    b.Property<bool>("StartedFlag");

                    b.Property<string>("Type");

                    b.HasKey("EachTaskId");

                    b.ToTable("EachTasks");
                });

            modelBuilder.Entity("MempApp.Model.Memo", b =>
                {
                    b.Property<string>("MemoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTimeOffset>("CreateTime");

                    b.Property<string>("EachTaskId");

                    b.Property<DateTimeOffset>("UpdateTime");

                    b.HasKey("MemoId");

                    b.HasIndex("EachTaskId");

                    b.ToTable("Memos");
                });

            modelBuilder.Entity("MempApp.Model.TimeInfo", b =>
                {
                    b.Property<string>("TimeInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EachTaskId");

                    b.Property<DateTimeOffset>("Start");

                    b.Property<DateTimeOffset>("Stop");

                    b.HasKey("TimeInfoId");

                    b.HasIndex("EachTaskId");

                    b.ToTable("TimeInfos");
                });

            modelBuilder.Entity("MempApp.Model.Memo", b =>
                {
                    b.HasOne("MempApp.Model.EachTask", "EachTask")
                        .WithMany()
                        .HasForeignKey("EachTaskId");
                });

            modelBuilder.Entity("MempApp.Model.TimeInfo", b =>
                {
                    b.HasOne("MempApp.Model.EachTask", "EachTask")
                        .WithMany()
                        .HasForeignKey("EachTaskId");
                });
#pragma warning restore 612, 618
        }
    }
}
