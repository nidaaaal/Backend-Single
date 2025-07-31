

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
        public string reflection { get; set; } = string.Empty;
        public string mood { get; set; } = string.Empty;
        public DateTime date { get; set; }  
    }

    #endregion response

}
