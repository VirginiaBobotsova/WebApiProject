namespace WebApiApp.Services.Interfaces
{
    using System.Collections.Generic;
    using WebApiApp.Models;

    public interface IPaintingService
    {
        PaintingViewModel Add(string userId, PaintingBindingModel model);
        PaintingViewModel Edit(string userId, int id, PaintingBindingModel model);
        IEnumerable<PaintingViewModel> GetAllPaintings();
        IEnumerable<PaintingViewModel> GetExibitionPaintings(int exibitionId);
        PaintingViewModel GetById(int id);
    }
}
