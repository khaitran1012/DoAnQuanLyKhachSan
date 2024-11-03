using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Đồ_án_quản_lí_khách_sạn.Models
{
    public partial class Hotels : DbContext
    {
        public Hotels()
            : base("name=Hotels")
        {
        }

        public virtual DbSet<customer> customers { get; set; }
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<Room> rooms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<customer>()
                .Property(e => e.cname)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.nationality)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.dob)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.idproof)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.checkin)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.checkout)
                .IsUnicode(false);

            modelBuilder.Entity<customer>()
                .Property(e => e.chekout)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.ename)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.emailid)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.pass)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.RoomNo)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.RoomType)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.Bed)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .Property(e => e.Booked)
                .IsUnicode(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Customers)
                .WithRequired(e => e.room)
                .WillCascadeOnDelete(false);
        }
    }
}
