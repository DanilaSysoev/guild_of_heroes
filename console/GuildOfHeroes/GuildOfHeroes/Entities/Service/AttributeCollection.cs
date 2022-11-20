using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes.Service
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
