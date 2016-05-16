namespace WebApi.Models
{
    using System.ComponentModel.DataAnnotations;
    using WebApi.Models.Interfaces;

    public class Gallery : IDentificatable
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Town { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }
    }
}
