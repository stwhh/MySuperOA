using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class AnnounceTypeMap : EntityTypeConfiguration<AnnounceType>
    {
        public AnnounceTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.AnnounceTypeId)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.AnnounceTypeName)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("AnnounceType");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.AnnounceTypeId).HasColumnName("AnnounceTypeId");
            this.Property(t => t.AnnounceTypeName).HasColumnName("AnnounceTypeName");
        }
    }
}
