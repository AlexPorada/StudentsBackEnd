﻿// <auto-generated />
using System;
using ClassStudent.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClassStudent.DAL.Migrations
{
    [DbContext(typeof(StudentRoomContext))]
    [Migration("20181130114346_ReferenceTeacherStudentWithTagTable")]
    partial class ReferenceTeacherStudentWithTagTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ClassStudent.DAL.Entity.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("ClassStudent.DAL.Entity.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int?>("RoomId");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ClassStudent.DAL.Entity.StudentTeacherTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StudentId");

                    b.Property<int>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TeacherId");

                    b.ToTable("StudentTeacherTags");
                });

            modelBuilder.Entity("ClassStudent.DAL.Entity.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int?>("RoomId");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("RoomId")
                        .IsUnique()
                        .HasFilter("[RoomId] IS NOT NULL");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("ClassStudent.DAL.Entity.Student", b =>
                {
                    b.HasOne("ClassStudent.DAL.Entity.Room", "Room")
                        .WithMany("Students")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("ClassStudent.DAL.Entity.StudentTeacherTag", b =>
                {
                    b.HasOne("ClassStudent.DAL.Entity.Student", "Student")
                        .WithMany("StudentTeacherTags")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ClassStudent.DAL.Entity.Teacher", "Teacher")
                        .WithMany("StudentTeacherTags")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ClassStudent.DAL.Entity.Teacher", b =>
                {
                    b.HasOne("ClassStudent.DAL.Entity.Room", "Room")
                        .WithOne("Teacher")
                        .HasForeignKey("ClassStudent.DAL.Entity.Teacher", "RoomId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}