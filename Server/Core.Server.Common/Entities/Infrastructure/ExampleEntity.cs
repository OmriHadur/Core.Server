using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Common.Entities
{
    public class ExampleEntity : OwnedEntity
    {
        [Required]
        [Range(0, 1000)]
        public int? Value { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        public List<ExampleChildEntity> ChildEntities { get; set; }

        public ExampleEntity()
        {
            ChildEntities = new List<ExampleChildEntity>();
        }
    }
}
