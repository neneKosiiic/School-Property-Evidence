using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolPropertyEvidence.Models {

    [Table("categories")]
    public class CategoryModel {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("category_name")]
        public string CategoryName { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        public ICollection<ItemModel> Items { get; set; } = new List<ItemModel>();
    }
}