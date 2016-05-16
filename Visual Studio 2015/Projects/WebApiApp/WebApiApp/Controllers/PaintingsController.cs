namespace WebApiApp.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using WebApi.Data;
    using WebApi.Models;
    using WebApiApp.Models;

    public class PaintingsController : ApiController
    {
        public PaintingsController()
        {
            this.Data = new ApplicationDbContext();
        }

        public ApplicationDbContext Data { get; set; }

        //GET: paintings/
        [HttpGet]
        [Route("paintings")]
        public IHttpActionResult GetAllPaintings()
        {
            var paintings = this.Data.Paintings
                .OrderByDescending(p=>p.Id)
                .Select(p => new PaintingViewModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Year = p.Year,
                    Description = p.Description,
                    Image = p.Img,
                    Exibition = p.Exibition.Name ?? null

                });

            return this.Ok(paintings);
        }

        //POST: paintings
        [HttpPost]
        [Route("paintings")]
        public IHttpActionResult AddNewPainting(AddPaintingBindingModel model)
        {
            var userId = this.User.Identity.GetUserId();
            ApplicationUser user = null;

            if (userId != null)
            {
                user = this.Data.Users.FirstOrDefault(u => u.Id == userId);
            }

            if (user == null && userId != null)
            {
                return this.BadRequest();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (model == null)
            {
                return this.BadRequest("Model can not be null");
            }

            var painting = new Painting()
            {
                Title = model.Title,
                Description = model.Description ?? null,
                Img = model.Img,
                Year = model.Year ?? null,
                Exibition = this.Data.Exibitions.FirstOrDefault(e=>e.Name == model.Exibition) ?? null

            };

            this.Data.Paintings.Add(painting);
            this.Data.SaveChanges();

            return CreatedAtRoute(
                "DefaultApi",
                new
                {
                    id = painting.Id
                },
                new
                {
                    Message = "Painting is added successfully"
                });
        }
    }
}
