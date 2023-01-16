using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace DataLayer
{
    public class IMDBcontext : DbContext

     
    {
        const string ConnectionString = "host=cit.ruc.dk;db=cit03;uid=cit03;pwd=tAtc9jnYtgip;";

        public DbSet<Bookmark>? Bookmark { get; set; }
        public DbSet<Password>? Password { get; set; }
        public DbSet<SimpleProfessionals> SimpleProfessionals { get; set; }
        public DbSet<PopularMovies> PopularMovies { get; set; }
        public DbSet<Characters> Characters { get; set; }
        public DbSet<Profession> Profession { get; set; }
        public DbSet<TitleName> TitleNames { get; set; }
        public DbSet<SearchHistory> searchHistories { get; set; }
        public DbSet<Titles>? Titles { get; set; }
        public DbSet<Professionals>? Professionals { get; set; }
        public DbSet<Word>? WordModel { get; set; }
        public DbSet<RatingHistory>? RatingHistory { get; set; }
        public DbSet<User>? User { get; set; }
        public DbSet<HasGenre> HasGenre { get; set; }
        public DbSet<TitleSimilarModel>? TitlesSimilarModels { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.UseNpgsql(ConnectionString);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
            modelBuilder.Entity<Titles>().Property(x => x.AvgRating).HasColumnName("avg_rating");

            modelBuilder.Entity<Professionals>().ToTable("professionals");
            modelBuilder.Entity<Professionals>().HasKey(x => new { x.ProfId});
            modelBuilder.Entity<Professionals>().Property(x => x.ProfId).HasColumnName("prof_id");
            modelBuilder.Entity<Professionals>().Property(x => x.ProfName).HasColumnName("prof_name");
            modelBuilder.Entity<Professionals>().Property(x => x.BirthYear).HasColumnName("birth_year");
            modelBuilder.Entity<Professionals>().Property(x => x.DeathYear).HasColumnName("death_year");
            modelBuilder.Entity<Professionals>().Property(x => x.ProfRating).HasColumnName("prof_rating");

            modelBuilder.Entity<Word>().HasNoKey();
            modelBuilder.Entity<Word>().Property(x => x.KeyWord).HasColumnName("words");
            modelBuilder.Entity<Word>().Property(x => x.Frequency).HasColumnName("c_count");

            modelBuilder.Entity<SimpleProfessionals>().HasNoKey();
            modelBuilder.Entity<SimpleProfessionals>().Property(x => x.Name).HasColumnName("prof_name");
            modelBuilder.Entity<SimpleProfessionals>().Property(x => x.ProfId).HasColumnName("prof_id");

            modelBuilder.Entity<Password>().ToTable("password");
            modelBuilder.Entity<Password>().HasKey(x => new { x.UserName });    
            modelBuilder.Entity<Password>().Property(x => x.UserName).HasColumnName("username");
            modelBuilder.Entity<Password>().Property(x => x.HashedPassword).HasColumnName("hashed_password");
            modelBuilder.Entity<Password>().Property(x => x.Salt).HasColumnName("salt");

            modelBuilder.Entity<Bookmark>().ToTable("bookmark");
            modelBuilder.Entity<Bookmark>().HasKey(x => new { x.UserName, x.TitleId });
            modelBuilder.Entity<Bookmark>().Property(x => x.UserName).HasColumnName("username");
            modelBuilder.Entity<Bookmark>().Property(x => x.TitleId).HasColumnName("title_id");

            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().HasKey(x => new { x.UserName});
            modelBuilder.Entity<User>().Property(x => x.UserName).HasColumnName("username");
            modelBuilder.Entity<User>().Property(x => x.Photo).HasColumnName("photo");
            modelBuilder.Entity<User>().Property(x => x.Bio).HasColumnName("bio");
            modelBuilder.Entity<User>().Property(x => x.Email).HasColumnName("email");

            modelBuilder.Entity<RatingHistory>().ToTable("rating_history");
            modelBuilder.Entity<RatingHistory>().HasNoKey();
            modelBuilder.Entity<RatingHistory>().Property(x => x.Rating).HasColumnName("rating");
            modelBuilder.Entity<RatingHistory>().Property(x => x.UserName).HasColumnName("username");
            modelBuilder.Entity<RatingHistory>().Property(x => x.TitleId).HasColumnName("title_id");
            modelBuilder.Entity<RatingHistory>().Property(x => x.TitleName).HasColumnName("title_name");
            modelBuilder.Entity<RatingHistory>().Property(x => x.Poster).HasColumnName("poster");

            modelBuilder.Entity<TitleSimilarModel>().HasNoKey();
            modelBuilder.Entity<TitleSimilarModel>().Property(x => x.ID).HasColumnName("title_id");
            modelBuilder.Entity<TitleSimilarModel>().Property(x => x.Name).HasColumnName("title_name");
            modelBuilder.Entity<TitleSimilarModel>().Property(x => x.Poster).HasColumnName("poster");

            modelBuilder.Entity<PopularMovies>().HasNoKey();
            modelBuilder.Entity<PopularMovies>().Property(x => x.ID).HasColumnName("title_id");
            modelBuilder.Entity<PopularMovies>().Property(x => x.Name).HasColumnName("title_name");
            modelBuilder.Entity<PopularMovies>().Property(x => x.Poster).HasColumnName("poster");
            modelBuilder.Entity<PopularMovies>().Property(x => x.AvgRating).HasColumnName("avg_rating");

            modelBuilder.Entity<HasGenre>().HasNoKey();
            modelBuilder.Entity<HasGenre>().ToTable("has_genre");
            modelBuilder.Entity<HasGenre>().Property(x => x.Genre).HasColumnName("genre");

            modelBuilder.Entity<Characters>().HasNoKey();
            modelBuilder.Entity<Characters>().Property(x => x.Character).HasColumnName("characters"); 
            
            modelBuilder.Entity<TitleName>().HasNoKey();
            modelBuilder.Entity<TitleName>().Property(x => x.TitleNames).HasColumnName("title_name");

            modelBuilder.Entity<SearchHistory>().HasNoKey();
            modelBuilder.Entity<SearchHistory>().Property(x => x.Search).HasColumnName("search_string");

            modelBuilder.Entity<Profession>().ToTable("has_profession");
            modelBuilder.Entity<Profession>().HasKey(x => new { x.ProfId, x.Professions });
            modelBuilder.Entity<Profession>().Property(x => x.ProfId).HasColumnName("prof_id");
            modelBuilder.Entity<Profession>().Property(x => x.Professions).HasColumnName("profession");
        }
    }
}
