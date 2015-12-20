using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class FileTypeMap : EntityTypeConfiguration<FileType>
    {
        public FileTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.FilesTypeId)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.FilesTypeName)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("FileType");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.FilesTypeId).HasColumnName("FilesTypeId");
            this.Property(t => t.FilesTypeName).HasColumnName("FilesTypeName");
        }
    }
}
