using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPropertyEvidence.Models {

    [Table("rooms")]
    public class RoomModel {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("room_name")]
        public string RoomName { get; set; } = null!;

        [Required]
        [Column("responsible_person_id")]
        public int ResponsiblePersonId { get; set; }

        [ForeignKey(nameof(ResponsiblePersonId))]
        public PeopleModel ResponsiblePerson { get; set; } = null!;

        public ICollection<ItemModel> Items { get; set; } = new List<ItemModel>();
    }
}