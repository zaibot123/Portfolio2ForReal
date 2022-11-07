using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace DataLayer
{
    public class IMDBcontext : DbContext
    {
        const string ConnectionString = "host=localhost;db=imdb;uid=postgres;pwd=1234";

        //public DbSet<Category>? Categories { get; set; }
        public DbSet<Casting>? Casting { get; set; }
        public DbSet<Titles>? Titles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.UseNpgsql(ConnectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Casting>().ToTable("casting");
            modelBuilder.Entity<Casting>().HasKey(x => new { x.TitleId, x.ProfId, x.Ordering });

            modelBuilder.Entity<Casting>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Casting>().Property(x => x.ProfId).HasColumnName("prof_id");
            modelBuilder.Entity<Casting>().Property(x => x.Ordering).HasColumnName("ordering");
            modelBuilder.Entity<Casting>().Property(x => x.JobCategory).HasColumnName("job_category");
            modelBuilder.Entity<Casting>().Property(x => x.Job).HasColumnName("job");
            modelBuilder.Entity<Casting>().Property(x => x.Characters).HasColumnName("characters");


            modelBuilder.Entity<Titles>().ToTable("title");
            modelBuilder.Entity<Titles>().HasKey(x => new { x.TitleId});

            modelBuilder.Entity<Titles>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<Titles>().Property(x => x.TitleName).HasColumnName("title_name");
            modelBuilder.Entity<Titles>().Property(x => x.TitleType).HasColumnName("title_type");
            modelBuilder.Entity<Titles>().Property(x => x.Poster).HasColumnName("poster");
            modelBuilder.Entity<Titles>().Property(x => x.TitlePlot).HasColumnName("title_plot");
            modelBuilder.Entity<Titles>().Property(x => x.StartYear).HasColumnName("start_year");
            modelBuilder.Entity<Titles>().Property(x => x.EndYear).HasColumnName("end_year");
            modelBuilder.Entity<Titles>().Property(x => x.Runtime).HasColumnName("runtime");
            modelBuilder.Entity<Titles>().Property(x => x.IsAdult).HasColumnName("is_adult");
            modelBuilder.Entity<Titles>().Property(x => x.NrRatings).HasColumnName("nr_ratings");
            modelBuilder.Entity<Titles>().Property(x => x.AvgRating).HasColumnName("avg_ratong");






        }
    }
}
