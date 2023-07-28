using System.ComponentModel.DataAnnotations;

namespace RestSiteKursova.Models
{
    public class MenuItem
    {
        public int Id { get; set; } 
        [Display(Name= "Страва")]
        public string Name { get; set; }
        [Display(Name= "Опис")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Display(Name= "Ціна")]
        public int Price { get; set; }

        [Display(Name= "IMG URL")]
        public string img { get; set; }

    
    }
}
