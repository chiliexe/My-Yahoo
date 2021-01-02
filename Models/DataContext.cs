
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MyYahoo.Models
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DataContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<Question> questions {get; set;}
        public DbSet<Answer> answers {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string conn = configuration.GetConnectionString("mysql");
            builder.UseMySql(conn);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //MODEL QUESTION
            builder.Entity<Question>(entity => {
                entity.ToTable("question");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Description).HasColumnType("TEXT");

            });

            //MODEL ANSWER
            builder.Entity<Answer>(entity => {
                entity.ToTable("answer");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Content).HasColumnType("TEXT");
                entity.Property(e => e.Content).IsRequired();

                entity.HasOne(e => e.question).WithMany(e => e.Answers);
            });
                
        }
    }
}