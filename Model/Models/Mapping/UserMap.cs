using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.UserCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.RealName)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.UserPwd)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Sex)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Phone)
                .IsRequired()
                .HasMaxLength(11);

            this.Property(t => t.QQ)
                .HasMaxLength(20);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Address)
                .HasMaxLength(100);

            this.Property(t => t.PositionCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.DepartmentCode)
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
            this.ToTable("User");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.UserCode).HasColumnName("UserCode");
            this.Property(t => t.RealName).HasColumnName("RealName");
            this.Property(t => t.UserPwd).HasColumnName("UserPwd");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.Age).HasColumnName("Age");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.QQ).HasColumnName("QQ");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.EntryDate).HasColumnName("EntryDate");
            this.Property(t => t.PositionCode).HasColumnName("PositionCode");
            this.Property(t => t.DepartmentCode).HasColumnName("DepartmentCode");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.CreateUserCode).HasColumnName("CreateUserCode");
            this.Property(t => t.ModifyTime).HasColumnName("ModifyTime");
            this.Property(t => t.ModifyUserCode).HasColumnName("ModifyUserCode");
            this.Property(t => t.Addtion1).HasColumnName("Addtion1");
            this.Property(t => t.Addtion2).HasColumnName("Addtion2");
        }
    }
}
