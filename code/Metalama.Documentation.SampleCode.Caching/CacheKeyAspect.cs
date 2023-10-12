// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Patterns.Caching.Aspects;
using System;
using System.Collections.Generic;

namespace Doc.CacheKeyAspect
{
    public abstract class Entity
    {
        protected Entity( string kind, int id )
        {
            this.Kind = kind;
            this.Id = id;
        }

        [CacheKey]
        public string Kind { get; }

        [CacheKey]
        public int Id { get; }

        public string? Description { get; set; }
    }

    public class EntityService
    {
        [Cache]
        public IEnumerable<Entity> GetRelatedEntities( Entity entity ) => throw new NotImplementedException();
    }
}