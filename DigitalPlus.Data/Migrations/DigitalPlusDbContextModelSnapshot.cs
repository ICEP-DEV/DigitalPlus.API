﻿// <auto-generated />
using System;
using DigitalPlus.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DigitalPlus.Data.Migrations
{
    [DbContext(typeof(DigitalPlusDbContext))]
    partial class DigitalPlusDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DigitalPlus.API.Model.Administrator", b =>
                {
                    b.Property<int>("Admin_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Admin_Id"));

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Admin_Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("MenteeId")
                        .HasColumnType("int");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("AppointmentId");

                    b.HasIndex("MenteeId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.AssignKey", b =>
                {
                    b.Property<int>("KeyId")
                        .HasColumnType("int");

                    b.Property<int>("MentorId")
                        .HasColumnType("int");

                    b.HasKey("KeyId");

                    b.ToTable("AssignKeys");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.AssignMod", b =>
                {
                    b.Property<int>("AssignModId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssignModId"));

                    b.Property<int>("MentorId")
                        .HasColumnType("int");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.HasKey("AssignModId");

                    b.HasIndex("MentorId");

                    b.ToTable("AssignMods");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Course", b =>
                {
                    b.Property<int>("Course_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Course_Id"));

                    b.Property<string>("Course_Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Course_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Department_Id")
                        .HasColumnType("int");

                    b.HasKey("Course_Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Department", b =>
                {
                    b.Property<int>("Department_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Department_Id"));

                    b.Property<string>("Department_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Department_Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Key", b =>
                {
                    b.Property<int>("KeyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KeyId"));

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("KeyId");

                    b.ToTable("Keys");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Mentee", b =>
                {
                    b.Property<int>("Mentee_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Mentee_Id"));

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Semester")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Mentee_Id");

                    b.ToTable("Mentees");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Mentor", b =>
                {
                    b.Property<int>("MentorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MentorId"));

                    b.Property<bool>("Activated")
                        .HasColumnType("bit");

                    b.Property<int>("Available")
                        .HasColumnType("int");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MentorId");

                    b.ToTable("Mentors");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Module", b =>
                {
                    b.Property<int>("Module_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Module_Id"));

                    b.Property<int?>("Course_Id")
                        .HasColumnType("int");

                    b.Property<string>("Module_Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Module_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Module_Id");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Register", b =>
                {
                    b.Property<int>("Register_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Register_Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MenteeId")
                        .HasColumnType("int");

                    b.Property<int>("MentorId")
                        .HasColumnType("int");

                    b.Property<double>("Rating")
                        .HasColumnType("float");

                    b.Property<int>("ReportId")
                        .HasColumnType("int");

                    b.Property<string>("Signature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Register_Id");

                    b.HasIndex("MenteeId");

                    b.ToTable("Registers");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduleId"));

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<int?>("AdministratorAdmin_Id")
                        .HasColumnType("int");

                    b.Property<string>("DaysOfTheWeek")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MentorId")
                        .HasColumnType("int");

                    b.Property<string>("TimeSlot")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ScheduleId");

                    b.HasIndex("AdministratorAdmin_Id");

                    b.HasIndex("MentorId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Appointment", b =>
                {
                    b.HasOne("DigitalPlus.API.Model.Mentee", null)
                        .WithMany("Appointments")
                        .HasForeignKey("MenteeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalPlus.API.Model.AssignKey", b =>
                {
                    b.HasOne("DigitalPlus.API.Model.Key", "Key")
                        .WithOne("AssignKey")
                        .HasForeignKey("DigitalPlus.API.Model.AssignKey", "KeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Key");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.AssignMod", b =>
                {
                    b.HasOne("DigitalPlus.API.Model.Mentor", null)
                        .WithMany("AssignMods")
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Register", b =>
                {
                    b.HasOne("DigitalPlus.API.Model.Mentee", null)
                        .WithMany("Registers")
                        .HasForeignKey("MenteeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Schedule", b =>
                {
                    b.HasOne("DigitalPlus.API.Model.Administrator", null)
                        .WithMany("Schedules")
                        .HasForeignKey("AdministratorAdmin_Id");

                    b.HasOne("DigitalPlus.API.Model.Mentor", null)
                        .WithMany("Schedules")
                        .HasForeignKey("MentorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Administrator", b =>
                {
                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Key", b =>
                {
                    b.Navigation("AssignKey")
                        .IsRequired();
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Mentee", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Registers");
                });

            modelBuilder.Entity("DigitalPlus.API.Model.Mentor", b =>
                {
                    b.Navigation("AssignMods");

                    b.Navigation("Schedules");
                });
#pragma warning restore 612, 618
        }
    }
}
