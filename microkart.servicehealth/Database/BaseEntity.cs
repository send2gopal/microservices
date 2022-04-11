using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microkart.shared.Database
{
    public abstract class BaseEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime CraetedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }

        [Required]
        public string? CreatedBy { get; set; }

        [Required]
        public string? UpdatedBy { get; set; }

    }
}
