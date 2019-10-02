using ClassStudent.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassStudent.DAL.Context
{
    public class StudentRoomContext : DbContext
    {
        private readonly DbContextOptions<StudentRoomContext> _dbContextOptions; 
       public  DbSet<Student> Students { get; set; }
       public  DbSet<Room> Rooms { get; set; }
       public  DbSet<Teacher> Teachers {get; set; }

        public DbSet<StudentTeacherTag> StudentTeacherTags { get; set; }
        public StudentRoomContext(DbContextOptions<StudentRoomContext> options) : base(options)
        {
            _dbContextOptions = options;
        }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Students)
                .WithOne(s => s.Room)
                .HasForeignKey(s => s.RoomId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Room>()
                .HasOne(r => r.Teacher)
                .WithOne(t => t.Room)
                .HasForeignKey<Teacher>(t => t.RoomId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.StudentTeacherTags)
                .WithOne(st => st.Student)
                .HasForeignKey(st => st.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.StudentTeacherTags)
                .WithOne(st => st.Teacher)
                .HasForeignKey(st => st.TeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
