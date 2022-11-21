using System.Collections.Generic;

namespace GuildOfHeroes.Entities.Service
{
    public class AttributeCollection
    {
        private Dictionary<string, object> attributes;

        public AttributeCollection()
        {
            attributes = new Dictionary<string, object>();
        }

        public void Add<T>(string name, T value)
        {
            attributes.Add(name, value);
        }

        public T Get<T>(string name)
        {
            return (T)attributes[name];
        }
    }
}
