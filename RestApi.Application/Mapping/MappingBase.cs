using AutoMapper;
using RestApi.Common.Entities;
using RestApi.Standard.Shared.Resources;
using System;

namespace RestApi.Application.Mapping
{
    public class MappingBase : Profile
    {
        public void AddMapping<TCreateResource, TResource, TEntity>()
            where TCreateResource : CreateResource
            where TResource : Resource
            where TEntity : Entity, new()
        {
            CreateMap<TCreateResource, TEntity>();
            CreateMap<TResource, TEntity>().ReverseMap();
        }
        public string SizeDisplay(int size,bool IsInGrams)
        {
            var roundedSize = size < 1000 ? size : Math.Round((decimal)size / 1000, 1);
            return LongUom(roundedSize, size, IsInGrams);
        }
        public string ShortUom(decimal valueDisplay, int value, bool inInGrams)
        {
            return $"{valueDisplay} {(inInGrams ? (value > 999 ? "kg" : "g") : "l")}";
        }
        public string LongUom(decimal valueDisplay, int value, bool inInGrams)
        {
            return $"{valueDisplay} {(inInGrams ? (value > 999 ? "Kilo" : "Gram") : "Liter")}";
        }
    }
}
