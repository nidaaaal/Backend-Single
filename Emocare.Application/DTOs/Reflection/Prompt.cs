

namespace Emocare.Application.DTOs.Reflection
{
    #region request
    public class DailyJournalDto
    {
        public string prompt { get; set; } = string.Empty;
        public string mood { get; set; } = string.Empty;
    }

    #endregion

    #region response
    public class DailyResponseDto
    {
        public string Reflection { get; set; } = string.Empty;
        public string Mood { get; set; } = string.Empty;
        public string Entry { get; set; } = string.Empty;
        public DateTime Date { get; set; }  
    }

    #endregion response

}
