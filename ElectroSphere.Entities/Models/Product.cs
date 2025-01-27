using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectroSphere.Entities.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
