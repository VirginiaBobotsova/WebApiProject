namespace WebApi.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WebApi.Models.Interfaces;

    public class Exibition : IDentificatable
    {
        public Exibition()
        {
            this.Paintings = new HashSet<Painting>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Img { get; set; }

        public string GalleryId { get; set; }

        public virtual Gallery Gallery { get; set; }

        public virtual ICollection<Painting> Paintings { get; set; }
    }
}
