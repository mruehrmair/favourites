using AutoMapper;
using Favourites.API.Models;
using Favourites.Data.Entities;

namespace Favourites.API.Profiles;

// ReSharper disable once UnusedType.Global
public class BookmarkProfile : Profile
{
    public BookmarkProfile()
    {
        CreateMap<Bookmark, BookmarkDto>()
            .ForMember(dest => dest.Tags,
                opt => opt.MapFrom(src => (src.Tags ?? new List<Tag>()).Select(x => x.Name)));
        CreateMap<BookmarkForCreationDto, Bookmark>()
            .ForMember(dest => dest.Tags,
                opt => opt.MapFrom(src => src.Tags.Select(s => new Tag(s)).ToList()))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToLower()));
        CreateMap<BookmarkForUpdateDto, Bookmark>()
            .ForMember(dest => dest.Tags,
                opt => opt.MapFrom(src => src.Tags.Select(s => new Tag(s)).ToList()));
    }
}