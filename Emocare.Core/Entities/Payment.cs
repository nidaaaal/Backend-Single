using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Enums;
using Emocare.Domain.Enums.Auth;

namespace Emocare.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Users? User { get; set; } 
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod  { get; set; }
        public DateTime PaidAt { get; set; }
        public Status Status  { get; set; } = Status.Pending;   
        public string TransactionId { get; set; } = "";
    }
}
