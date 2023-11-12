using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarModels
{
    public class Vehicle
    {
        
        [Required]
        public DateTime deliveryDate { get; set; }
        [Required]
        public Finantiation finantiation { get; set; }
    }
}
