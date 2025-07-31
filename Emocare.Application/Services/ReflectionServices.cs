using Emocare.Application.Interfaces;
using Emocare.Domain.Entities.Journal;
using Emocare.Domain.Enums.AiChat;
using Emocare.Domain.Interfaces.Helper.AiChat;
using Emocare.Domain.Interfaces.Repositories.Chat;
using Emocare.Shared.Helpers.Api;
using System.Text;


namespace Emocare.Application.Services
{
    public class ReflectionServices: IReflectionServices
    {
        private readonly IOpenRouterStreamService _openRouterStream;
        private readonly IJournalEntryRepository _journalEntry;
        private readonly IUserFinder _userFinder;

        public ReflectionServices(IOpenRouterStreamService openRouterStream, IJournalEntryRepository journalEntry, IUserFinder userFinder)
        {
            _openRouterStream = openRouterStream;
            _journalEntry = journalEntry;
            _userFinder = userFinder;
        }

        public async Task<ApiResponse<string>> GetReflection(string prompt, string mood)
        {

            Guid id = _userFinder.GetId();

            if (!await _journalEntry.CheckDone(id))
            {

                Mood usermood = Enum.Parse<Mood>(mood);

                StringBuilder Response = new StringBuilder();
                JournalMode journal = JournalMode.Journal;
                await foreach (var word in _openRouterStream.StreamChatAsync(prompt, journal, mood))
                {
                    Console.WriteLine("word :", word);
                    Response.Append(word);
                }
                string reflection = Response.ToString();

                var data = new JournalEntry
                {
                    UserId = id,
                    Date = DateTime.Now,
                    Entry = prompt,
                    Mode = journal,
                    Mood = usermood,
                    AIReflection = reflection
                };
                await _journalEntry.Add(data);
                return ResponseBuilder.Success(reflection, "Response Successful for AIReflection", "GetReflection");

            }
            var res = await _journalEntry.TodayReflection(id) ?? throw new NotFoundException("user Not Found On the Corresponding id");

            return ResponseBuilder.Success(res.AIReflection, "Response Already Updated", "GetReflection");
        }

        public async Task<ApiResponse<string>?> DailyReflection()
        {
            Guid id = _userFinder.GetId();
            var res = await _journalEntry.TodayReflection(id) ?? throw new NotFoundException("user Not Found On the Corresponding id");
            if (res.Date.Date != DateTime.Now.Date)
                return ResponseBuilder.Fail<string>("Get new Reflection", "DailyReflection", 404);
            return ResponseBuilder.Success(res.AIReflection, "DailyReflection  Fetched", "GetReflection");
        }

        public async Task<ApiResponse<IEnumerable<JournalEntry?>>> LastWeekDailyReflection()
        {
            Guid id = _userFinder.GetId();
            var response = await _journalEntry.LastWeek(id) ?? throw new NotFoundException("No Reflection Found on Corresponding Id");
            return ResponseBuilder.Success(response, "DailyReflection  Fetched", "GetReflection");
        }
    }
}
