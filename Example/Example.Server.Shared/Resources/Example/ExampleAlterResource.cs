using Core.Server.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Example.Server.Shared.Resources
{
    public class ExampleAlterResource
    {
        [Immutable]
        [MinLength(3)]
        public string Name { get; set; }

        [Range(0, 100)]
        public int? Value { get; set; }
    }
}