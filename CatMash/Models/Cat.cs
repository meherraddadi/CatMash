using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CatMash.Models
{
    [Table("Cat")]
    public class Cat
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Nom Du Chat")]
        public string Cat_Name { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date_Cre { get; set; }
        public byte[] Cat_picture { get; set; }
        [Display(Name = "Image Du Chat")]
        public string Picture_Path {get;set;}
        
        public int Rate { get; set; }
        [Display(Name = "Score")]
        public double Score { get; set; }

    }
}
