using System;
using System.Collections.Generic;

namespace SaveSystem.Core
{
    [Serializable]
    public class EntityTable<T>
    {
        public string uniqueId;
        public List<T> entities;

        public EntityTable(string uniqueId, List<T> entities)
        {
            this.uniqueId = uniqueId;
            this.entities = entities;
        }

    }
}