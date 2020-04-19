using AutoMapper;
using BlogEFModels;
using BlogModelsDTO;
using System.Collections.Generic;

namespace BlogDAL
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            
            CreateMap<BlogPost, Post>().IncludeAllDerived()
                .ForMember(dest => dest.Blogtitle, opt => opt.MapFrom(src => src.title))
                .ForMember(dest => dest.Blogdescription, opt => opt.MapFrom(src => src.description))
                .ForMember(dest => dest.Blogbody, opt => opt.MapFrom(src => src.body))
                .ForMember(dest => dest.Taglist, opt => opt.MapFrom(src => string.Join(",",src.taglist)))
                .ReverseMap();
            //CreateMap<Post, BlogPost>()
            //    .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Blogtitle))
            //    .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Blogdescription))
            //    .ForMember(dest => dest.body, opt => opt.MapFrom(src => src.Blogbody))
            //    .ForMember(dest => string.Join(",", dest.taglist), opt => opt.MapFrom(src => src.Taglist)).ReverseMap();
            CreateMap<TagList, List<Tag>>().ReverseMap();
            CreateMap<Tag, string>().ConstructUsing(r => r.TagName).ReverseMap();
        }
    }
}
