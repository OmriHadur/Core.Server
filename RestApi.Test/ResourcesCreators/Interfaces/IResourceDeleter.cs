using RestApi.Client.Results;
using System;
using System.Collections.Generic;

namespace RestApi.Tests.ResourceCreators.Interfaces
{
    public interface IResourceDeleter
    {
        ActionResult Delete(string id);
    }
}
