namespace WebApi.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using WebApi.Models.Interfaces;

    public class Painting : IDentificatable
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Year { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string Img { get; set; }

        public virtual Exibition Exibition { get; set; }

    }
}
