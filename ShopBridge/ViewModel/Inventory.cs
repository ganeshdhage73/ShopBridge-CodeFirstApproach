using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.ViewModel
{
    public class Inventory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int InventoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; } 

        [Required]
        public int CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ModifiedOn { get; set; }

        public int ModifiedBy { get; set; }

        [Required]
        [StringLength(50)]
        public string IPAddress { get; set; }
    }
}
