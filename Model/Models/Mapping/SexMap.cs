using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class SexMap : EntityTypeConfiguration<Sex>
    {
        public SexMap()
        {
            // Primary Key
            this.HasKey(t => t.SexId);

            // Properties
            this.Property(t => t.SexId)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.SexName)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Sex");
            this.Property(t => t.SexId).HasColumnName("SexId");
            this.Property(t => t.SexName).HasColumnName("SexName");
        }
    }
}
