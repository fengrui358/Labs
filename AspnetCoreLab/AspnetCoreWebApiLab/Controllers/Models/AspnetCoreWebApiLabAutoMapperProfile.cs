using AspnetCoreWebApiLab.Entities;
using AutoMapper;

namespace AspnetCoreWebApiLab.Controllers.Models
{
    public class AspnetCoreWebApiLabAutoMapperProfile : Profile
    {
        public AspnetCoreWebApiLabAutoMapperProfile()
        {
            CreateMap<CreateOrUpdateTodoItemDto, TodoItem>();
        }
    }
}
