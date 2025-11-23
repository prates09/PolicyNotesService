using Microsoft.EntityFrameworkCore;
using PolicyNotesService.Data;
using PolicyNotesService.Models;
using PolicyNotesService.Repositories;
using PolicyNotesService.Services;
using Xunit;

namespace PolicyNotesService.Tests.UnitTests
{
    public class PolicyNotesServiceUnitTests
    {
        
        private Services.PolicyNotesService CreateService()
        {
            var options = new DbContextOptionsBuilder<PolicyNotesDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new PolicyNotesDbContext(options);
            var repo = new PolicyNoteRepository(context);

            return new Services.PolicyNotesService(repo);
        }

        [Fact]
        public async Task AddNoteAsync_AddsNoteAndAssignsId()
        {
            
            var service = CreateService();
            var dto = new PolicyNoteCreateDto
            {
                PolicyNumber = "POL-100",
                Note = "Unit test note"
            };

            
            var created = await service.AddNoteAsync(dto);

            
            Assert.NotNull(created);
            Assert.True(created.Id > 0);
            Assert.Equal("POL-100", created.PolicyNumber);
            Assert.Equal("Unit test note", created.Note);
        }

        [Fact]
        public async Task GetAllNotesAsync_ReturnsAllAddedNotes()
        {
            
            var service = CreateService();

            await service.AddNoteAsync(new PolicyNoteCreateDto
            {
                PolicyNumber = "POL-1",
                Note = "First note"
            });

            await service.AddNoteAsync(new PolicyNoteCreateDto
            {
                PolicyNumber = "POL-2",
                Note = "Second note"
            });

            
            var notes = await service.GetAllNotesAsync();

            
            Assert.Equal(2, notes.Count);
            Assert.Contains(notes, n => n.PolicyNumber == "POL-1" && n.Note == "First note");
            Assert.Contains(notes, n => n.PolicyNumber == "POL-2" && n.Note == "Second note");
        }
    }
}
