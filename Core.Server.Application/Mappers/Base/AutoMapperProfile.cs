using AutoMapper;
using Core.Server.Common.Mappers;
using Unity;

namespace Core.Server.Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(IUnityContainer unityContainer)
        {
            var mappers = unityContainer.ResolveAll<IAutoMapperMapping>();
            foreach (var mapper in mappers)
                mapper.AddAutoMapping(this);             
        }
    }
}