using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes.Service
{
    public static class JsonBuilder
    {
        public static Dictionary<K, V> 
        BuildKeyValueDictionary<K, V>(
            JToken data,
            Func<JToken, K> keyParser,
            Func<JToken, V> valueParser
        )
        {
            Dictionary<K, V> result = new Dictionary<K, V>();
            foreach (var dataUnit in data)
                result.Add(
                    keyParser(dataUnit),
                    valueParser(dataUnit)
                );
            return result;
        }
    }
}
