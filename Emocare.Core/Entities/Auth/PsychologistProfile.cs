
using System.ComponentModel.DataAnnotations;


namespace Emocare.Domain.Entities.Auth
{
    public class PsychologistProfile
    {
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public Users Users { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Specialization { get; set; } = null!;

        [Required, MaxLength(50)]
        public string LicenseNumber { get; set; } = null!;

        [Required]
        public string? LicenseCopy { get; set; } 

        [Range(0, 60)]
        public int? Experience { get; set; }

        [MaxLength(1000)]
        public string? Biography { get; set; }

        [MaxLength(200)]
        public string? Education { get; set; }

        public bool IsApproved { get; set; } = false;
        public DateTime? ApprovedOn {get; set; }

    }



}
