using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPropertyEvidence.Models {

    [Table("people")]
    public class PeopleModel {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("first_name")]
        public string FirstName { get; set; } = null!;

        [Required]
        [Column("last_name")]
        public string LastName { get; set; } = null!;

        [Required]
        [Column("email")]
        public string Email { get; set; } = null!;

        [Required]
        [Column("password_hash")]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [Column("role")]
        public string Role { get; set; } = null!;

        [Required]
        [Column("is_active")]
        public bool IsActive { get; set; }

        public ICollection<RoomModel> Rooms { get; set; } = new List<RoomModel>();
    }
}
