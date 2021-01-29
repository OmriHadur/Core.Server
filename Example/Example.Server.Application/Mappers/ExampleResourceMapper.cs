using Core.Server.Application.Mappers.Base;
using Core.Server.Common.Attributes;
using Core.Server.Common.Mappers;
using Example.Server.Common.Entities;
using Example.Server.Shared.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Example.Server.Application.Mappers
{
    [InjectBoundleWithName]
    public class ExampleResourceMapper
        : ResourceMapper<ExampleResource, ExampleEntity>
    {
        [Dependency]
        public IResourceMapper<ExampleChildResource, ExampleChildEntity> exampleChildResourceMapper;

        public override async Task<IEnumerable<ExampleResource>> Map(IEnumerable<ExampleEntity> entities)
        {
            var resources = new List<ExampleResource>();
            foreach (var entity in entities)
                resources.Add(await Map(entity));
            return resources;
        }

        public override async Task<ExampleResource> Map(ExampleEntity entity)
        {
            var resource = await base.Map(entity);
            var childResources = await exampleChildResourceMapper.Map(entity.ChildEntities);
            resource.ChildResources = childResources.ToArray();
            return resource;
        }
    }
}
