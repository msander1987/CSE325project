using Microsoft.EntityFrameworkCore;
using Peret.Data;
using Peret.Models;

namespace Peret.Services
{
    public class StudyService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public StudyService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        // gets or cretares the campaign
        public async Task<UserCampaign> GetOrCreateCampaignAsync(string userId, string bookSetId, string defaultBook)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var campaign = await db.UserCampaigns
                .FirstOrDefaultAsync(c => c.UserId == userId && c.BookSetId == bookSetId);

            if (campaign == null)
            {
                campaign = new UserCampaign
                {
                    UserId = userId,
                    BookSetId = bookSetId,
                    CurrentCampaign = 1,
                    LastBook = defaultBook,
                    LastChapter = 1
                };
                db.UserCampaigns.Add(campaign);
                await db.SaveChangesAsync();
            }

            return campaign;
        }

        // Set user reading bookmark
        public async Task UpdateBookmarkAsync(string userId, string bookSetId, string nextBook, int nextChapter)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var campaign = await db.UserCampaigns
                .FirstOrDefaultAsync(c => c.UserId == userId && c.BookSetId == bookSetId);

            if (campaign != null)
            {
                campaign.LastBook = nextBook;
                campaign.LastChapter = nextChapter;
                await db.SaveChangesAsync();
            }
        }

        // Starts a new campaign and set book at the beginning
        public async Task<UserCampaign> StartNewCampaignAsync(string userId, string bookSetId, string firstBook)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var campaign = await db.UserCampaigns
                .FirstOrDefaultAsync(c => c.UserId == userId && c.BookSetId == bookSetId);

            if (campaign == null)
            {
                campaign = new UserCampaign
                {
                    UserId = userId,
                    BookSetId = bookSetId,
                    CurrentCampaign = 1,
                    LastBook = firstBook,
                    LastChapter = 1
                };
                db.UserCampaigns.Add(campaign);
            }
            else
            {
                campaign.CurrentCampaign++;
                campaign.LastBook = firstBook;
                campaign.LastChapter = 1;
            }

            await db.SaveChangesAsync();
            return campaign;
        }

        // reading progress
        public async Task<bool> IsChapterReadAsync(string userId, string bookSetId, int campaignNumber, string book, int chapter)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.ReadingProgress.AnyAsync(p =>
                p.UserId == userId &&
                p.BookSetId == bookSetId &&
                p.CampaignNumber == campaignNumber &&
                p.Book == book &&
                p.Chapter == chapter);
        }

        public async Task MarkChapterAsReadAsync(string userId, string bookSetId, int campaignNumber, string book, int chapter)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            bool exists = await db.ReadingProgress.AnyAsync(p =>
                p.UserId == userId &&
                p.BookSetId == bookSetId &&
                p.CampaignNumber == campaignNumber &&
                p.Book == book &&
                p.Chapter == chapter);

            if (!exists)
            {
                db.ReadingProgress.Add(new ReadingProgress
                {
                    UserId = userId,
                    BookSetId = bookSetId,
                    CampaignNumber = campaignNumber,
                    Book = book,
                    Chapter = chapter,
                    ReadAt = DateTime.UtcNow
                });
                await db.SaveChangesAsync();
            }
        }

        // notes
        public async Task<List<StudyNote>> GetNotesForChapterAsync(string userId, string bookSetId, int campaignNumber, string book, int chapter)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.StudyNotes
                .Where(n => n.UserId == userId &&
                            n.BookSetId == bookSetId &&
                            n.CampaignNumber == campaignNumber &&
                            n.Book == book &&
                            n.Chapter == chapter)
                .OrderBy(n => n.Verse ?? 0)
                .ThenByDescending(n => n.CreatedAt)
                .ToListAsync();
        }

        public async Task AddNoteAsync(StudyNote note)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            note.CreatedAt = DateTime.UtcNow;
            db.StudyNotes.Add(note);
            await db.SaveChangesAsync();
        }

        public async Task UpdateNoteAsync(StudyNote updatedNote)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var existing = await db.StudyNotes
                .FirstOrDefaultAsync(n => n.Id == updatedNote.Id && n.UserId == updatedNote.UserId);

            if (existing != null)
            {
                existing.Verse = updatedNote.Verse;
                existing.Content = updatedNote.Content;
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteNoteAsync(int noteId, string userId)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var note = await db.StudyNotes
                .FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId);

            if (note != null)
            {
                db.StudyNotes.Remove(note);
                await db.SaveChangesAsync();
            }
        }

        // Quizz questions
        public async Task<int> GetQuestionCountForChapterAsync(string userId, string bookSetId, int campaignNumber, string book, int chapter)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            return await db.QuizQuestions.CountAsync(q =>
                q.CreatedByUserId == userId &&
                q.BookSetId == bookSetId &&
                q.CampaignNumber == campaignNumber &&
                q.Book == book &&
                q.Chapter == chapter);
        }

        public async Task AddQuizQuestionAsync(QuizQuestion question)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            question.CreatedAt = DateTime.UtcNow;
            db.QuizQuestions.Add(question);
            await db.SaveChangesAsync();
        }
    }
}