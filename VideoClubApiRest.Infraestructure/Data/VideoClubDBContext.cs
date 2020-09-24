using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VideoClubApiRest.Core.Entities;

namespace VideoClubApiRest.Infraestructure.Data
{
    public partial class VideoClubDBContext : DbContext
    {
        public VideoClubDBContext()
        {
        }

        public VideoClubDBContext(DbContextOptions<VideoClubDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Rents> Rents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rents>(entity =>
            {
                entity.HasKey(e => e.RentId)
                    .HasName("PK__RENTS__03FC6F35EC2FD6D4");

                entity.ToTable("RENTS");

                entity.Property(e => e.RentId)
                    .HasColumnName("rent_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClientId)
                    .HasColumnName("client_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Detailssatus)
                    .HasColumnName("detailssatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Detailsuntil)
                    .HasColumnName("detailsuntil")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ObjectId)
                    .HasColumnName("object_id")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
 
        }

    }
}
