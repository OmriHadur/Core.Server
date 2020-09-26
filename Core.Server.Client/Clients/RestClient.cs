﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Shared.Resources;

namespace Core.Server.Client.Clients
{
    public abstract class RestClient<TCreateResource, TResource> :
        ClientBase, 
        IRestClient<TCreateResource, TResource>
        where TCreateResource : CreateResource
        where TResource : Resource
    {
        protected RestClient(string apiRoute) : 
            base(apiRoute)
        {
        }

        public Task<ActionResult<IEnumerable<TResource>>> Get()
        {
            return GetAsync<IEnumerable<TResource>>(ApiUrl);
        }

        public Task<ActionResult<TResource>> Get(string id)
        {
            return GetAsync<TResource>(ApiUrl + id);
        }

        public Task<ActionResult<TResource>> Create(TCreateResource resource)
        {
            return PostAsync<TResource>(ApiUrl, resource);
        }

        public Task<ActionResult<TResource>> Update(string id, TCreateResource resource)
        {
            return PutAsync<TResource>(ApiUrl + id, resource);
        }

        public Task<ActionResult<TResource>> Delete(string id)
        {
            return DeleteAsync<TResource>(ApiUrl + id);
        }
    }
}