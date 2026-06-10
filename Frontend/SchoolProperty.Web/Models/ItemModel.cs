using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPropertyEvidence.Models {

    [Table("items")]
    public class ItemModel {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("item_name")]
        public string ItemName { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(CategoryModel.Items))]
        public CategoryModel Category { get; set; } = null!;

        [Required]
        [Column("room_id")]
        public int RoomId { get; set; }

        [ForeignKey(nameof(RoomId))]
        [InverseProperty(nameof(RoomModel.Items))]
        public RoomModel Room { get; set; } = null!;

        [Required]
        [Column("is_active")]
        public bool IsActive { get; set; }
    }
}