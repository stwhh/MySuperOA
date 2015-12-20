using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.RoleCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.RoleName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.RoleContent)
                .HasMaxLength(100);

            this.Property(t => t.CreateUserCode)
                .HasMaxLength(20);

            this.Property(t => t.ModifyUserCode)
                .HasMaxLength(20);

            this.Property(t => t.Addtion1)
                .HasMaxLength(50);

            this.Property(t => t.Addtion2)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Role");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.RoleCode).HasColumnName("RoleCode");
            this.Property(t => t.RoleName).HasColumnName("RoleName");
            this.Property(t => t.RoleContent).HasColumnName("RoleContent");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.CreateUserCode).HasColumnName("CreateUserCode");
            this.Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            this.Property(t => t.ModifyUserCode).HasColumnName("ModifyUserCode");
            this.Property(t => t.Addtion1).HasColumnName("Addtion1");
            this.Property(t => t.Addtion2).HasColumnName("Addtion2");
        }
    }
}
