﻿// <auto-generated />
using System;
using ASM_SIMS.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASM_SIMS.Migrations
{
    [DbContext(typeof(SimsDataContext))]
    [Migration("20250402150556_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ASM_SIMS.DB.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("Varchar(150)")
                        .HasColumnName("Address");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("Email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("Password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("Varchar(20)")
                        .HasColumnName("Phone");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("Varchar(60)")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ASM_SIMS.DB.Categories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Avatar")
                        .HasColumnType("Varchar(150)")
                        .HasColumnName("Avatar");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("Varchar(255)")
                        .HasColumnName("Description");

                    b.Property<string>("NameCategory")
                        .IsRequired()
                        .HasColumnType("Varchar(60)")
                        .HasColumnName("NameCategory");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("Varchar(20)")
                        .HasColumnName("Status");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ASM_SIMS.DB.ClassRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("Varchar(60)")
                        .HasColumnName("ClassName");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("EndDate");

                    b.Property<string>("Location")
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("Location");

                    b.Property<string>("Schedule")
                        .IsRequired()
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("Schedule");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("StartDate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("Varchar(20)")
                        .HasColumnName("Status");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("TeacherId");

                    b.ToTable("ClassRooms");
                });

            modelBuilder.Entity("ASM_SIMS.DB.Courses", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AvatarCourse")
                        .HasColumnType("Varchar(150)")
                        .HasColumnName("AvatarCourse");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("Varchar(255)")
                        .HasColumnName("Description");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("EndDate");

                    b.Property<string>("NameCourse")
                        .IsRequired()
                        .HasColumnType("Varchar(60)")
                        .HasColumnName("NameCourse");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("StartDate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("Varchar(20)")
                        .HasColumnName("Status");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Vote")
                        .HasColumnType("Integer")
                        .HasColumnName("Vote");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("ASM_SIMS.DB.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("Varchar(150)")
                        .HasColumnName("Address");

                    b.Property<int?>("ClassRoomId")
                        .HasColumnType("int");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("Email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("FullName");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("Varchar(20)")
                        .HasColumnName("Phone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("Varchar(20)")
                        .HasColumnName("Status");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("ClassRoomId");

                    b.HasIndex("CourseId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ASM_SIMS.DB.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("Varchar(150)")
                        .HasColumnName("Address");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("Email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("Varchar(100)")
                        .HasColumnName("FullName");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("Varchar(20)")
                        .HasColumnName("Phone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("Varchar(20)")
                        .HasColumnName("Status");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.HasIndex("CourseId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("ASM_SIMS.DB.ClassRoom", b =>
                {
                    b.HasOne("ASM_SIMS.DB.Courses", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASM_SIMS.DB.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ASM_SIMS.DB.Courses", b =>
                {
                    b.HasOne("ASM_SIMS.DB.Categories", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ASM_SIMS.DB.Student", b =>
                {
                    b.HasOne("ASM_SIMS.DB.Account", "Account")
                        .WithOne()
                        .HasForeignKey("ASM_SIMS.DB.Student", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASM_SIMS.DB.ClassRoom", "ClassRoom")
                        .WithMany()
                        .HasForeignKey("ClassRoomId");

                    b.HasOne("ASM_SIMS.DB.Courses", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.Navigation("Account");

                    b.Navigation("ClassRoom");

                    b.Navigation("Course");
                });

            modelBuilder.Entity("ASM_SIMS.DB.Teacher", b =>
                {
                    b.HasOne("ASM_SIMS.DB.Account", "Account")
                        .WithOne()
                        .HasForeignKey("ASM_SIMS.DB.Teacher", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASM_SIMS.DB.Courses", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId");

                    b.Navigation("Account");

                    b.Navigation("Course");
                });
#pragma warning restore 612, 618
        }
    }
}
