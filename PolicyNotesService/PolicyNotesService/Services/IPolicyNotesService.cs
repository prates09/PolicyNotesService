using PolicyNotesService.Models;

namespace PolicyNotesService.Services
{
    public interface IPolicyNotesService
    {
        Task<PolicyNote> AddNoteAsync(PolicyNoteCreateDto dto);
        Task<List<PolicyNote>> GetAllNotesAsync();
        Task<PolicyNote?> GetNoteByIdAsync(int id);
    }
}
