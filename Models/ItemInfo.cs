using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2084___A1.Models
{
    public partial class ItemInfo
    {
        [Key]
        [Column("ItemID")]
        public int ItemId { get; set; }
        [Key]
        [Column("userName")]
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }
        public int EcoScoreId { get; set; }

        [ForeignKey(nameof(EcoScoreId))]
        [InverseProperty(nameof(EcoScoreTally.ItemInfo))]
        public virtual EcoScoreTally EcoScore { get; set; }
    }
}
