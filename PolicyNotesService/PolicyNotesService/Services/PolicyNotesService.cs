using PolicyNotesService.Models;
using PolicyNotesService.Repositories;

namespace PolicyNotesService.Services
{
    public class PolicyNotesService : IPolicyNotesService
    {
        private readonly IPolicyNoteRepository _repository;

        public PolicyNotesService(IPolicyNoteRepository repository)
        {
            _repository = repository;
        }

        public async Task<PolicyNote> AddNoteAsync(PolicyNoteCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.PolicyNumber))
                throw new ArgumentException("PolicyNumber is required", nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Note))
                throw new ArgumentException("Note is required", nameof(dto));

            var entity = new PolicyNote
            {
                PolicyNumber = dto.PolicyNumber.Trim(),
                Note = dto.Note.Trim()
            };

            return await _repository.AddAsync(entity);
        }

        public Task<List<PolicyNote>> GetAllNotesAsync()
            => _repository.GetAllAsync();

        public Task<PolicyNote?> GetNoteByIdAsync(int id)
            => _repository.GetByIdAsync(id);
    }
}
