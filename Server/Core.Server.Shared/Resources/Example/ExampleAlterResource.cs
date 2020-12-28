using Core.Server.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class ExampleAlterResource
    {
        [RequiredOnAlter]
        [Range(0, 100)]
        public int? Value { get; set; }

        [Immutable]
        [MinLength(3)]
        public string Name { get; set; }

    }
}