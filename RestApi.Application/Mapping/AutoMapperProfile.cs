using AutoMapper;
using RestApi.Common.Mapping;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace RestApi.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(IUnityContainer unityContainer)
        {
            var mappers = unityContainer.ResolveAll<IResourceMapper>();
            foreach (var mapper in mappers)
                mapper.AddMapping(this);             
        }
    }
}