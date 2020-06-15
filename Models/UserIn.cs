using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2084___A1.Models
{
    public partial class UserIn
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        public int ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        [InverseProperty(nameof(ProductIn.UserIn))]
        public virtual ProductIn Item { get; set; }
    }
}
