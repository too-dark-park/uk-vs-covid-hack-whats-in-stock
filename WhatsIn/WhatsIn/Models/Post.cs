using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatsIn.Models
{
    public class Post
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ImageFileName { get; set; }

        public DateTime PostedUtc { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
