using Core.Server.Common.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.Server.Application.Query.PhraseBuilders
{
    public abstract class QueryPhrasePipelineMapper : IQueryPhrasePipelineMapper
    {
        public abstract int Priory { get; }

        public QueryBase Map(string queryPhrase, IEnumerable<IQueryPhrasePipelineMapper> mappers) 
        {
            if (IsCanMap(queryPhrase))
                return InnerMap(queryPhrase, mappers);
            return CallNext(queryPhrase, mappers);
        }

        public override bool Equals(object obj)
        {
            return GetType().Equals(obj.GetType());
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }

        protected QueryBase CallNext(string queryPhrase, IEnumerable<IQueryPhrasePipelineMapper> mappers)
        {
            if (!mappers.Any())
                return null;
            var next = mappers.First();
            var leftMappers = mappers.Skip(1);
            return next.Map(queryPhrase, leftMappers);
        }


        protected bool IsCanMap(string queryPhrase)
        {
            return Regex.IsMatch(queryPhrase, MappingRegex);
        }

        protected string ToStartUpper(string str)
        {
            return str[0].ToString().ToUpper() + str[1..];
        }

        protected abstract string MappingRegex { get; }

        protected abstract QueryBase InnerMap(string queryPhrase, IEnumerable<IQueryPhrasePipelineMapper> mappers);
    }
}
