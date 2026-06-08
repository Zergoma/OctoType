using Microsoft.EntityFrameworkCore;

using OctoType.Domain.Entities;

namespace OctoType.Infrastructure.DbContexts;

public class DactyloDbContext : DbContext
{
    public DbSet<Word> Words => Set<Word>();

    public DbSet<WordAnalysis> WordAnalyses => Set<WordAnalysis>();

    public DactyloDbContext(
        DbContextOptions<DactyloDbContext> options)
        : base(options)
    {
        
    }


    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        
    }

    private static void ConfigureWord(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Word>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Text)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.LanguageCode)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(x => x.OccurrenceCount)
                .HasDefaultValue(1);

            // INDEX
            entity.HasIndex(x => new
            {
                x.Text,
                x.LanguageCode 
            })
            .IsUnique();

            entity.HasIndex(x => x.LanguageCode);
            entity.HasIndex(x => x.Length);
            entity.HasIndex(x => x.OccurrenceCount);
        });
    }

    private static void ConfigureWordAnalysis(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WordAnalysis>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Layout)
                .HasConversion<int>();

            entity.Property(x => x.RowMask)
                .HasConversion<int>();

            entity.Property(x => x.FingerMask)
                .HasConversion<int>();

            // RELATION
            entity.HasOne(x => x.Word)
                .WithMany(w => w.Analyses)
                .HasForeignKey(x => x.WordId)
                .OnDelete(DeleteBehavior.Cascade);

            // INDEXES
            entity.HasIndex(x => x.Layout);

            entity.HasIndex(x => x.WordId);

            entity.HasIndex(x => new
            {
                x.Layout,
                x.RowMask
            });

            entity.HasIndex(x => new
            {
                x.Layout,
                x.FingerMask
            });

            entity.HasIndex(x => new
            {
                x.Layout,
                x.UsesLeftHand,
                x.UsesRightHand
            });

            entity.HasIndex(x => new
            {
                x.WordId,
                x.Layout
            })
            .IsUnique();

        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureWord(modelBuilder);
        ConfigureWordAnalysis(modelBuilder);
    }
}
