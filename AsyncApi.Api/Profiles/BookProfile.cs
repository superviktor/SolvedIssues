using AsyncApi.Api.Dtos;
using AsyncApi.Api.Entities;
using AutoMapper;

namespace AsyncApi.Api.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>().ForMember(dto => dto.Info, opt => opt.MapFrom(b => $"{b.Id}<=>{b.Title}"));
        }
    }
}