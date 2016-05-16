namespace WebApiApp.Services
{
    using AutoMapper;
    using WebApi.Data.Repositories;

    public class BaseService
    {
        protected WebApiData data;
        protected IMapper mapper;

        public BaseService(WebApiData data, IMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }
    }
}