using AutoMapper;
using Core.Server.Common.Mapping;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Core.Server.Application.Mapping
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