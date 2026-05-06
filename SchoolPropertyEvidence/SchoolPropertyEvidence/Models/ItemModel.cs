using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPropertyEvidence.Models {

    [Table("items")]
    public class ItemModel {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("inventory_number")]
        public string InventoryNumber { get; set; } = null!;

        [Required]
        [Column("item_name")]
        public string ItemName { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        [Required]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public CategoryModel Category { get; set; } = null!;

        [Required]
        [Column("room_id")]
        public int RoomId { get; set; }

        [ForeignKey(nameof(RoomId))]
        public RoomModel Room { get; set; } = null!;
    }
}