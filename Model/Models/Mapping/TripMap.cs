using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class TripMap : EntityTypeConfiguration<Trip>
    {
        public TripMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.TripCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.TripContent)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Destination)
                .HasMaxLength(50);

            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.UserCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.IsReim)
                .HasMaxLength(2);

            this.Property(t => t.CreateUserCode)
                .HasMaxLength(20);

            this.Property(t => t.ModifyUserCode)
                .HasMaxLength(20);

            this.Property(t => t.Addtion1)
                .HasMaxLength(50);

            this.Property(t => t.Addtion2)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Trip");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.TripCode).HasColumnName("TripCode");
            this.Property(t => t.TripContent).HasColumnName("TripContent");
            this.Property(t => t.ApplyDate).HasColumnName("ApplyDate");
            this.Property(t => t.BeginDate).HasColumnName("BeginDate");
            this.Property(t => t.EndDate).HasColumnName("EndDate");
            this.Property(t => t.Destination).HasColumnName("Destination");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.UserCode).HasColumnName("UserCode");
            this.Property(t => t.IsReim).HasColumnName("IsReim");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.CreateUserCode).HasColumnName("CreateUserCode");
            this.Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            this.Property(t => t.ModifyUserCode).HasColumnName("ModifyUserCode");
            this.Property(t => t.Addtion1).HasColumnName("Addtion1");
            this.Property(t => t.Addtion2).HasColumnName("Addtion2");
        }
    }
}
