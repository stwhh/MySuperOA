using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class PositionMap : EntityTypeConfiguration<Position>
    {
        public PositionMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.PositionCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PositionName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Addtion1)
                .HasMaxLength(50);

            this.Property(t => t.Addtion2)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Position");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.PositionCode).HasColumnName("PositionCode");
            this.Property(t => t.PositionName).HasColumnName("PositionName");
            this.Property(t => t.Addtion1).HasColumnName("Addtion1");
            this.Property(t => t.Addtion2).HasColumnName("Addtion2");
        }
    }
}
