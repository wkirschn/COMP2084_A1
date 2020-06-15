using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2084___A1.Models
{
    public partial class ProductIn
    {
        public ProductIn()
        {
            UserIn = new HashSet<UserIn>();
        }

        [Key]
        public int ItemId { get; set; }
        [Required]
        [StringLength(50)]
        public string Material { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        [Required]
        [StringLength(255)]
        public string Photo { get; set; }
        public int EcoScore { get; set; }

        [InverseProperty("Item")]
        public virtual ICollection<UserIn> UserIn { get; set; }
    }
}
