using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2084___A1.Models
{
    public partial class EcoScoreTally
    {
        public EcoScoreTally()
        {
            ItemInfo = new HashSet<ItemInfo>();
        }

        [Key]
        public int EcoScoreId { get; set; }
        [Required]
        [StringLength(50)]
        public string Material { get; set; }
        [Required]
        [StringLength(50)]
        public string Removal { get; set; }
        [Required]
        [StringLength(50)]
        public string Reuse { get; set; }

        [InverseProperty("EcoScore")]
        public virtual ICollection<ItemInfo> ItemInfo { get; set; }
    }
}
