using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class Role_PermissonMap : EntityTypeConfiguration<Role_Permisson>
    {
        public Role_PermissonMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.RoleCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.PermCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CreateUserCode)
                .HasMaxLength(20);

            this.Property(t => t.ModifyUserCode)
                .HasMaxLength(20);

            this.Property(t => t.Addtion1)
                .HasMaxLength(50);

            this.Property(t => t.Addtion2)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Role_Permisson");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.RoleCode).HasColumnName("RoleCode");
            this.Property(t => t.PermCode).HasColumnName("PermCode");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.CreateUserCode).HasColumnName("CreateUserCode");
            this.Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            this.Property(t => t.ModifyUserCode).HasColumnName("ModifyUserCode");
            this.Property(t => t.Addtion1).HasColumnName("Addtion1");
            this.Property(t => t.Addtion2).HasColumnName("Addtion2");
        }
    }
}
