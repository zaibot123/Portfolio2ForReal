using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace DataLayer
{
    public class IMDBcontext : DbContext

        //TEEEEST
    {
        const string ConnectionString = "host=localhost;db=imdb;uid=postgres;pwd=1234";
        public DbSet<Casting>? Casting { get; set; }
        public DbSet<HasProfession>? Profession { get; set; }
        public DbSet<Bookmark>? Bookmark { get; set; }
        public DbSet<Password>? Password { get; set; }
        public DbSet<Titles>? Titles { get; set; }
        public DbSet<Professionals>? Professionals { get; set; }
        public DbSet<ActorsModel>? ActorsModel { get; set; }
        public DbSet<SearchResult>? SearchResult { get; set; }
        // public DbSet<BookmarkModels>? BookmarkModels { get; set; }
        public DbSet<TitlesModel>? TitlesModel { get; set; }
        public DbSet<WordModel>? WordModel { get; set; }

        public DbSet<ProfessionalsPageModel> ProfessionalsPageModels { get; set; }
        public DbSet<MoviePageModel> MoviePageModel { get; set; }



        public DbSet<UserModel>? UserModels { get; set; }


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
            modelBuilder.Entity<Titles>().Property(x => x.AvgRating).HasColumnName("avg_rating");

            modelBuilder.Entity<SearchResult>().HasNoKey();
            modelBuilder.Entity<SearchResult>().Property(x => x.Title).HasColumnName("title");
            modelBuilder.Entity<SearchResult>().Property(x => x.Plot).HasColumnName("plot");
            modelBuilder.Entity<SearchResult>().Property(x => x.Character).HasColumnName("characters");
            modelBuilder.Entity<SearchResult>().Property(x => x.ActorNames).HasColumnName("profname");

            modelBuilder.Entity<TitlesModel>().HasNoKey();
            modelBuilder.Entity<TitlesModel>().Property(x => x.TitleName).HasColumnName("title");
            modelBuilder.Entity<TitlesModel>().Property(x => x.Poster).HasColumnName("poster");
            
            modelBuilder.Entity<Professionals>().ToTable("professionals");
            modelBuilder.Entity<Professionals>().HasKey(x => new { x.ProfId});
            modelBuilder.Entity<Professionals>().Property(x => x.ProfId).HasColumnName("prof_id");
            modelBuilder.Entity<Professionals>().Property(x => x.ProfName).HasColumnName("prof_name");
            modelBuilder.Entity<Professionals>().Property(x => x.BirthYear).HasColumnName("birth_year");
            modelBuilder.Entity<Professionals>().Property(x => x.DeathYear).HasColumnName("death_year");
            modelBuilder.Entity<Professionals>().Property(x => x.ProfRating).HasColumnName("prof_rating");

            modelBuilder.Entity<WordModel>().HasNoKey();
            modelBuilder.Entity<WordModel>().Property(x => x.Word).HasColumnName("words");
            modelBuilder.Entity<WordModel>().Property(x => x.Frequency).HasColumnName("c_count");

            modelBuilder.Entity<Password>().ToTable("password");
            modelBuilder.Entity<Password>().HasKey(x => new { x.UserName });

            modelBuilder.Entity<Password>().Property(x => x.UserName).HasColumnName("username");
            modelBuilder.Entity<Password>().Property(x => x.HashedPassword).HasColumnName("hashed_password");
            modelBuilder.Entity<Password>().Property(x => x.Salt).HasColumnName("salt");


            modelBuilder.Entity<HasProfession>().ToTable("has_profession");
            modelBuilder.Entity<HasProfession>().HasKey(x => new { x.ProfId, x.Profession});
            modelBuilder.Entity<HasProfession>().Property(x => x.ProfId).HasColumnName("prof_id");
            modelBuilder.Entity<HasProfession>().Property(x => x.Profession).HasColumnName("profession");


            modelBuilder.Entity<Bookmark>().ToTable("bookmark");
            modelBuilder.Entity<Bookmark>().HasKey(x => new { x.UserName, x.TitleId });
            modelBuilder.Entity<Bookmark>().Property(x => x.UserName).HasColumnName("username");
            modelBuilder.Entity<Bookmark>().Property(x => x.TitleId).HasColumnName("title_id");


            modelBuilder.Entity<UserModel>().ToTable("users");
            modelBuilder.Entity<UserModel>().HasKey(x => new { x.UserName});
            modelBuilder.Entity<UserModel>().Property(x => x.UserName).HasColumnName("username");
            modelBuilder.Entity<UserModel>().Property(x => x.Photo).HasColumnName("picture");
            modelBuilder.Entity<UserModel>().Property(x => x.Bio).HasColumnName("user_bio");
            modelBuilder.Entity<UserModel>().Property(x => x.Email).HasColumnName("email");

            modelBuilder.Entity<ActorsModel>().HasNoKey();
            modelBuilder.Entity<ActorsModel>().Property(x => x.ActorName).HasColumnName("prof_name");
            modelBuilder.Entity<ActorsModel>().Property(x => x.BirthYear).HasColumnName("birth_year");
            modelBuilder.Entity<ActorsModel>().Property(x => x.DeathYear).HasColumnName("death_year");

            modelBuilder.Entity<MoviePageModel>().HasNoKey();
            modelBuilder.Entity<MoviePageModel>().Property(x => x.TitleName).HasColumnName("title_name");
            modelBuilder.Entity<MoviePageModel>().Property(x => x.TitleType).HasColumnName("title_type");
            modelBuilder.Entity<MoviePageModel>().Property(x => x.Poster).HasColumnName("poster");
            modelBuilder.Entity<MoviePageModel>().Property(x => x.TitlePlot).HasColumnName("title_plot");
            modelBuilder.Entity<MoviePageModel>().Property(x => x.StartYear).HasColumnName("start_year");
            modelBuilder.Entity<MoviePageModel>().Property(x => x.EndYear).HasColumnName("end_year");
            modelBuilder.Entity<MoviePageModel>().Property(x => x.Runtime).HasColumnName("runtime");
            modelBuilder.Entity<MoviePageModel>().Property(x => x.IsAdult).HasColumnName("is_adult");
            modelBuilder.Entity<MoviePageModel>().Property(x => x.NrRatings).HasColumnName("nr_ratings");
            modelBuilder.Entity<MoviePageModel>().Property(x => x.AvgRating).HasColumnName("avg_rating");

            modelBuilder.Entity<ProfessionalsPageModel>().HasNoKey();
            modelBuilder.Entity<ProfessionalsPageModel>().Property(x => x.ProfName).HasColumnName("prof_name");
            modelBuilder.Entity<ProfessionalsPageModel>().Property(x => x.BirthYear).HasColumnName("birth_year");
            modelBuilder.Entity<ProfessionalsPageModel>().Property(x => x.DeathYear).HasColumnName("death_year");
            modelBuilder.Entity<ProfessionalsPageModel>().Property(x => x.ProfRating).HasColumnName("prof_rating");

        }
    }
}
