using GuildOfHeroes.Entities.Service;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GuildOfHeroes.Entities
{
    public class GuildMaster : ObjectWithName
    {
        public Race Race { get; private set; }
        public Class Class { get; private set; }
        public string History { get; private set; }

        public int SameRaceLoyaltyModifier { get; private set; }
        public int SameClassLoyaltyModifier { get; private set; }

        public IReadOnlyDictionary<Race, int>
        RaceLoyaltyModifiers
        { get; private set; }
        public IReadOnlyDictionary<Class, int>
        ClassLoyaltyModifiers
        { get; private set; }

        private GuildMaster(
            string name,
            Race race,
            Class clas,
            string history,
            int sameRaceLoyaltyModifier,
            int sameClassLoyaltyModifier,
            IReadOnlyDictionary<Race, int> raceLoyaltyModifiers,
            IReadOnlyDictionary<Class, int> classLoyaltyModifiers
        )
            : base(name)
        {
            Race = race;
            Class = clas;
            History = history;
            SameRaceLoyaltyModifier = sameRaceLoyaltyModifier;
            SameClassLoyaltyModifier = sameClassLoyaltyModifier;
            RaceLoyaltyModifiers = raceLoyaltyModifiers;
            ClassLoyaltyModifiers = classLoyaltyModifiers;
        }


        public static GuildMaster Selected { get; private set; }

        public static void SelectMaster(string name)
        {
            Selected = guildMasters[name];
        }

        public static void Load()
        {
            JArray guildMastersData = null;
            using (var reader = new StreamReader("Data/GuildMasters.json"))
                guildMastersData = JArray.Parse(reader.ReadToEnd());

            guildMasters = JsonBuilder.BuildKeyValueDictionary(
                guildMastersData,
                nameData => nameData.Value<string>("name"),
                BuildGuildMaster
            );
        }

        public static GuildMaster Get(string name)
        {
            return guildMasters[name];
        }
        public static List<string> GetNames()
        {
            return guildMasters.Keys.ToList();
        }
        public static List<GuildMaster> GetAll()
        {
            return guildMasters.Values.ToList();
        }

        private static GuildMaster BuildGuildMaster(JToken guildMasterData)
        {
            return new GuildMaster(
                guildMasterData.Value<string>("name"),
                Race.Get(guildMasterData.Value<string>("race")),
                Class.Get(guildMasterData.Value<string>("class")),
                guildMasterData.Value<string>("history"),
                guildMasterData.Value<int>("sameRaceLoyalty"),
                guildMasterData.Value<int>("sameClassLoyalty"),
                JsonBuilder.BuildKeyValueDictionary(
                    guildMasterData["racesLoyalties"],
                    nameData => Race.Get(nameData.Value<string>("name")),
                    token => token.Value<int>("value")
                ),
                JsonBuilder.BuildKeyValueDictionary(
                    guildMasterData["classesLoyalties"],
                    nameData => Class.Get(nameData.Value<string>("name")),
                    token => token.Value<int>("value")
                )
            );
        }

        private static Dictionary<string, GuildMaster> guildMasters;
    }
}
