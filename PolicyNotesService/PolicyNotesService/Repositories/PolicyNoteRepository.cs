using Microsoft.EntityFrameworkCore;
using PolicyNotesService.Data;
using PolicyNotesService.Models;

namespace PolicyNotesService.Repositories
{
    public class PolicyNoteRepository : IPolicyNoteRepository
    {
        private readonly PolicyNotesDbContext _db;

        public PolicyNoteRepository(PolicyNotesDbContext db)
        {
            _db = db;
        }

        public async Task<PolicyNote> AddAsync(PolicyNote note)
        {
            _db.PolicyNotes.Add(note);
            await _db.SaveChangesAsync();
            return note;
        }

        public Task<List<PolicyNote>> GetAllAsync()
        {
            return _db.PolicyNotes
                      .AsNoTracking()
                      .OrderBy(n => n.Id)
                      .ToListAsync();
        }

        public Task<PolicyNote?> GetByIdAsync(int id)
        {
            return _db.PolicyNotes
                      .AsNoTracking()
                      .FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}
