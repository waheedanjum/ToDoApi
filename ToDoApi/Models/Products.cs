using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApi.Models
{
    public class Products
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set;  }

        [Required]
        [Key]
        public int ProductCode { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [DefaultValue(0.0)]
        public decimal Price { get; set; }

    }
}
