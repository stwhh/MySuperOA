using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class PermissonMap : EntityTypeConfiguration<Permisson>
    {
        public PermissonMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.PermCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PermName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PermUrl)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.PermType)
                .HasMaxLength(20);

            this.Property(t => t.ParaentCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PermIcon)
                .HasMaxLength(50);

            this.Property(t => t.CreateUserCode)
                .HasMaxLength(20);

            this.Property(t => t.ModifyUserCode)
                .HasMaxLength(20);

            this.Property(t => t.Addtion1)
                .HasMaxLength(50);

            this.Property(t => t.Addtion2)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Permisson");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.PermCode).HasColumnName("PermCode");
            this.Property(t => t.PermName).HasColumnName("PermName");
            this.Property(t => t.PermUrl).HasColumnName("PermUrl");
            this.Property(t => t.PermSeq).HasColumnName("PermSeq");
            this.Property(t => t.PermType).HasColumnName("PermType");
            this.Property(t => t.ParaentCode).HasColumnName("ParaentCode");
            this.Property(t => t.PermIcon).HasColumnName("PermIcon");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.CreateUserCode).HasColumnName("CreateUserCode");
            this.Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            this.Property(t => t.ModifyUserCode).HasColumnName("ModifyUserCode");
            this.Property(t => t.Addtion1).HasColumnName("Addtion1");
            this.Property(t => t.Addtion2).HasColumnName("Addtion2");
        }
    }
}
