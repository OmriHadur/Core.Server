using Core.Server.Client.Results;
using System;
using System.Collections.Generic;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourceDeleter
    {
        ActionResult Delete(string id);
    }
}
