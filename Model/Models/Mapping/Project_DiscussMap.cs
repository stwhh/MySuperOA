using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class Project_DiscussMap : EntityTypeConfiguration<Project_Discuss>
    {
        public Project_DiscussMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.ProjCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.ProjComConent)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.CreateUserCode)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CreateUserImage)
                .HasMaxLength(50);

            this.Property(t => t.Addtion1)
                .HasMaxLength(50);

            this.Property(t => t.Addtion2)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Project_Discuss");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.ProjCode).HasColumnName("ProjCode");
            this.Property(t => t.ProjComConent).HasColumnName("ProjComConent");
            this.Property(t => t.CreateTime).HasColumnName("CreateTime");
            this.Property(t => t.CreateUserCode).HasColumnName("CreateUserCode");
            this.Property(t => t.CreateUserImage).HasColumnName("CreateUserImage");
            this.Property(t => t.Addtion1).HasColumnName("Addtion1");
            this.Property(t => t.Addtion2).HasColumnName("Addtion2");
        }
    }
}
