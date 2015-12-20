using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class AnnounceMap : EntityTypeConfiguration<Announce>
    {
        public AnnounceMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.AnnounceCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.AnnounceTypeId)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.AnnounceTitle)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.AnnounceContent)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CreateUserCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ModifyUserCode)
                .HasMaxLength(20);

            this.Property(t => t.Addtion1)
                .HasMaxLength(50);

            this.Property(t => t.Addtion2)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Announce");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.AnnounceCode).HasColumnName("AnnounceCode");
            this.Property(t => t.AnnounceTypeId).HasColumnName("AnnounceTypeId");
            this.Property(t => t.AnnounceTitle).HasColumnName("AnnounceTitle");
            this.Property(t => t.AnnounceContent).HasColumnName("AnnounceContent");
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
