using Emocare.Domain.Entities.Journal;


namespace Emocare.Domain.Interfaces.Repositories.Chat
{
    public interface IJournalEntryRepository : IRepository<JournalEntry>
    {
        Task<bool> CheckDone(Guid id);
        Task<JournalEntry> TodayReflection(Guid id);
        Task<IEnumerable<JournalEntry>> LastWeek(Guid id);

    }
}
