using Microsoft.EntityFrameworkCore;
using WebBTL1.Models;
using WebBTL1.Seed;

namespace WebBTL1.Data
{
	public class ApplicationDbContext :DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<Employee?> Employees { get; set; }
		public DbSet<Ethnic> Ethnices { get; set;}
		public DbSet<Job> Jobs { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Commune> Communes { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ethnic>().HasData(
                new SeedEthnics().EthnicGroups
            );
            modelBuilder.Entity<Job>().HasData(
				new SeedJobs().Jobs
           );
			modelBuilder.Entity<Province>(entity =>
			{
				entity.HasData(new SeedAdministrativeCountrySubdivision().Provinces);
				entity.ToTable("Province");
				entity.Property(e => e.Id).HasColumnName("id");
			});

			modelBuilder.Entity<District>(entity =>
			{
				entity.HasData(new SeedAdministrativeCountrySubdivision().Districts);
				entity.ToTable("District");
				entity.Property(e => e.Id).HasColumnName("id");
				entity.Property(e => e.ProvinceId).HasColumnName("ProvinceId");
				entity.HasOne(e => e.Province)
						.WithMany(x => x.Districts)
						.HasForeignKey(e => e.ProvinceId)
						.OnDelete(DeleteBehavior.Cascade)
						.HasConstraintName("fk_province_id");

			});

			modelBuilder.Entity<Commune>(entity =>
			{
				entity.HasData(new SeedAdministrativeCountrySubdivision().Communes);
				entity.ToTable("Commune");
				entity.HasOne(e => e.District)
						.WithMany(x => x.Communes)
						.HasForeignKey(d => d.DistrictId)
						.OnDelete(DeleteBehavior.Cascade)
						.HasConstraintName("fk_district_id");
            });
        }
	}
}
