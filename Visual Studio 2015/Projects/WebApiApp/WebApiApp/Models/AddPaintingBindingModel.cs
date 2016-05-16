namespace WebApiApp.Models
{
    using System.ComponentModel.DataAnnotations;
    using WebApi.Models;

    public class AddPaintingBindingModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Img { get; set; }

        public string Year { get; set; }

        public string Exibition { get; set; }
    }
}