using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class ReimMap : EntityTypeConfiguration<Reim>
    {
        public ReimMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ReimCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ReimContent)
                .HasMaxLength(100);

            this.Property(t => t.AirTicket)
                .HasMaxLength(10);

            this.Property(t => t.Train)
                .HasMaxLength(10);

            this.Property(t => t.Bus)
                .HasMaxLength(10);

            this.Property(t => t.Traffic)
                .HasMaxLength(10);

            this.Property(t => t.Accommodation)
                .HasMaxLength(10);

            this.Property(t => t.Bonus)
                .HasMaxLength(10);

            this.Property(t => t.Other)
                .HasMaxLength(10);

            this.Property(t => t.ReimMoney)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.UserCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.TripCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.CreateUserCode)
                .HasMaxLength(20);

            this.Property(t => t.ModifyUserCode)
                .HasMaxLength(20);

            this.Property(t => t.Addtion1)
                .HasMaxLength(50);

            this.Property(t => t.Addtion2)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Reim");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ReimCode).HasColumnName("ReimCode");
            this.Property(t => t.ReimContent).HasColumnName("ReimContent");
            this.Property(t => t.ApplyDate).HasColumnName("ApplyDate");
            this.Property(t => t.AirTicket).HasColumnName("AirTicket");
            this.Property(t => t.Train).HasColumnName("Train");
            this.Property(t => t.Bus).HasColumnName("Bus");
            this.Property(t => t.Traffic).HasColumnName("Traffic");
            this.Property(t => t.Accommodation).HasColumnName("Accommodation");
            this.Property(t => t.Bonus).HasColumnName("Bonus");
            this.Property(t => t.Other).HasColumnName("Other");
            this.Property(t => t.ReimMoney).HasColumnName("ReimMoney");
            this.Property(t => t.UserCode).HasColumnName("UserCode");
            this.Property(t => t.TripCode).HasColumnName("TripCode");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.CreateUserCode).HasColumnName("CreateUserCode");
            this.Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            this.Property(t => t.ModifyUserCode).HasColumnName("ModifyUserCode");
            this.Property(t => t.Addtion1).HasColumnName("Addtion1");
            this.Property(t => t.Addtion2).HasColumnName("Addtion2");
        }
    }
}
