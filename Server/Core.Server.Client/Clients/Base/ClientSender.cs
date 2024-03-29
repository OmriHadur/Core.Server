﻿using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Server.Client.Clients
{
    public class ClientSender<TResource>
        : ClientBase<TResource>
        where TResource : Resource
    {
        protected Task<ActionResult<TResource>> SendPatch(string urlSubfix, object content)
        {
            return SendMethod<TResource>(urlSubfix, HttpMethod.Patch, content);
        }

        protected Task<ActionResult> SendHead(string urlSubfix)
        {
            return SendMethod(urlSubfix, HttpMethod.Head);
        }

        protected Task<ActionResult<TResource>> SendGet()
        {
            return SendMethod<TResource>(HttpMethod.Get);
        }

        protected Task<ActionResult<IEnumerable<TResource>>> SendGetMany()
        {
            return SendMethod<IEnumerable<TResource>>(HttpMethod.Get);
        }

        protected Task<ActionResult<IEnumerable<TResource>>> SendGetMany(string urlSubfix)
        {
            return SendMethod<IEnumerable<TResource>>(HttpMethod.Get, urlSubfix);
        }

        protected Task<ActionResult<TResource>> SendGet(string urlSubfix)
        {
            return SendMethod<TResource>(HttpMethod.Get, urlSubfix);
        }

        protected Task<ActionResult> SendDelete(string urlSubfix)
        {
            return SendMethod(urlSubfix, HttpMethod.Delete);
        }

        protected Task<ActionResult<IEnumerable<string>>> SendDeleteMany(string urlSubfix, object content)
        {
            return SendMethod<IEnumerable<string>>(urlSubfix, HttpMethod.Delete, content);
        }

        protected Task<ActionResult<IEnumerable<TResource>>> SentPostMany(object content)
        {
            return SendMethod<IEnumerable<TResource>>(HttpMethod.Post, content);
        }

        protected Task<ActionResult<IEnumerable<TResource>>> SentPostMany(string urlSubfix, object content)
        {
            return SendMethod<IEnumerable<TResource>>(urlSubfix, HttpMethod.Post, content);
        }

        protected Task<ActionResult<TResource>> SentPost(object content)
        {
            return SendMethod<TResource>(HttpMethod.Post, content);
        }

        protected Task<ActionResult<TResource>> SentPost(string urlSubfix, object content)
        {
            return SendMethod<TResource>(urlSubfix, HttpMethod.Post, content);
        }

        protected Task<ActionResult> SentPostNoResource(string urlSubfix, object content)
        {
            return SendMethod(urlSubfix, HttpMethod.Post, content);
        }

        protected Task<ActionResult<TResource>> SentPut(object content)
        {
            return SendMethod<TResource>(HttpMethod.Put, content);
        }

        protected Task<ActionResult<TResource>> SentPut(string urlSubfix, object content)
        {
            return SendMethod<TResource>(urlSubfix, HttpMethod.Put, content);
        }

        protected Task<ActionResult<IEnumerable<TResource>>> SentPutMany(string urlSubfix, object content)
        {
            return SendMethod<IEnumerable<TResource>>(urlSubfix, HttpMethod.Put, content);
        }
    }
}
