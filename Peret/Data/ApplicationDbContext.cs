using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Peret.Models;

namespace Peret.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<UserCampaign> UserCampaigns { get; set; }
        public DbSet<ReadingProgress> ReadingProgress { get; set; }
        public DbSet<StudyNote> StudyNotes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
    }
}