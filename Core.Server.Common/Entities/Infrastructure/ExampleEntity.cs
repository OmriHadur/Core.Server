using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Common.Entities
{
    public class ExampleEntity : OwnedEntity, IParentEntity
    {
        [Required]
        [Range(0, 1000)]
        public int Value { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public string Mutable { get; set; }

        public List<ExampleChildEntity> ChildEntities { get; set; }

        public ExampleEntity()
        {
            ChildEntities = new List<ExampleChildEntity>();
        }

        public IList<TEntity> GetChildEntitiess<TEntity>() 
            where TEntity : Entity
        {
            if (typeof(TEntity) == typeof(ExampleChildEntity))
                return (ChildEntities as List<TEntity>);
            return null;
        }
    }
}
