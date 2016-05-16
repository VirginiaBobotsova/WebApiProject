namespace WebApiApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using AutoMapper;
    using WebApi.Common;
    using WebApi.Data.Repositories;
    using WebApi.Models;
    using WebApiApp.Models;
    using WebApiApp.Services.Interfaces;

    public class PaintingService : BaseService, IPaintingService
    {
        public PaintingService(WebApiData data, IMapper mapper) : base(data, mapper)
        {
        }

        public IEnumerable<PaintingViewModel> GetAllPaintings()
        {
            var paintings = this.data.PaintingRepository.Get()
                .Include(i => i.Exibition)
                .OrderBy(i => i.Id)
                .ToList();

            return this.GetMappedPaintings(paintings);
        }

        public PaintingViewModel Add(string userId, PaintingBindingModel model)
        {
            var user = this.data.UserRepository.GetById(userId);
            if (!user.isAdmin)
            {
                throw new InvalidOperationException(Constants.NotAdmin);
            }

            var painting = mapper.Map<PaintingBindingModel, Painting>(model);

            var paintingExibition = this.data.ExibitionRepository.GetById(painting.Exibition.Id)
                .Include(p => p.Paintings)
                .FirstOrDefault();
            if (paintingExibition == null)
            {
                throw new ArgumentException(Constants.UnexistingExibitionErrorMessage);
            }

            var exibitionPaintingsCount = paintingExibition.Paintings.Count;

            this.data.PaintingRepository.Insert(painting);
            this.data.Save();

            return this.GetMappedPainting(painting);
        }

        public PaintingViewModel Edit(string userId, int id, PaintingBindingModel model)
        {
            var painting = this.data.PaintingRepository.GetById(id)
                .Include(i => i.Exibition)
                .FirstOrDefault();
            if (painting == null)
            {
                throw new ArgumentException(Constants.UnexistingpPaintingErrorMessage);
            }

            var user = this.data.UserRepository.GetById(userId);
            if (!user.isAdmin)
            {
                throw new InvalidOperationException(Constants.NotAdmin);
            }

            painting.Title = model.Title;
            painting.Description = model.Description;
            painting.Img = model.Image;
            painting.Year = model.Year;
            painting.Exibition = this.data.ExibitionRepository.Get(e => e.Name == model.Exibition).FirstOrDefault();

            this.data.Save();

            return this.GetMappedPainting(painting);
        }

       
       

        public IEnumerable<PaintingViewModel> GetExibitionPaintings(int exibitionId)
        {
            var paintings = this.data.PaintingRepository.Get(i => i.Exibition.Id == exibitionId)
                .Include(i => i.Exibition)
                .ToList();

            return this.GetMappedPaintings(paintings);
        }

        public PaintingViewModel GetById(int id)
        {
            var painting = this.data.PaintingRepository.GetById(id)
                .Include(i => i.Exibition)
                .FirstOrDefault();
            if (painting == null)
            {
                throw new ArgumentException(Constants.UnexistingpPaintingErrorMessage);
            }

            return this.GetMappedPainting(painting);
        }

        private IEnumerable<PaintingViewModel> GetMappedPaintings(List<Painting> paintings)
        {
            var mappedPaintings = this.mapper.Map<ICollection<Painting>, ICollection<PaintingViewModel>>(paintings);
           
            return mappedPaintings;
        }

        private PaintingViewModel GetMappedPainting(Painting painting)
        {
            return GetMappedPaintings(new List<Painting>() { painting }).First();
        }
    }
}