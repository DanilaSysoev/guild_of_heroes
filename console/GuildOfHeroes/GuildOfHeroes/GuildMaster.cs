using GuildOfHeroes.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class GuildMaster : ObjectWithName
    {
        public Race Race { get; private set; }
        public Class Class { get; private set; }
        public string Hystory { get; private set; }

        public int SameRaceLoyaltyModifier { get; private set; }
        public int SameClassLoyaltyModifier { get; private set; }

        public IReadOnlyDictionary<Race, int>
        RaceLoyaltyModifiers { get; private set; }
        public IReadOnlyDictionary<Class, int> 
        ClassLoyaltyModifiers { get; private set; }

        private GuildMaster(string name)
            : base(name)
        {
            SameClassLoyaltyModifier = 0;
            SameRaceLoyaltyModifier = 0;            
        }


        public static GuildMaster Selected { get; private set; }

        public static void SelectMaster(string name)
        {
            Selected = guildMasters[name];
        }

        public static void Load()
        {
            using (StreamReader reader = new StreamReader("Data/GuildMasters.txt"))
            {
                var guildMastersTexts =
                    reader.ReadToEnd().Split(
                        new string[] { "\r\n\r\n" },
                        StringSplitOptions.RemoveEmptyEntries
                    );

                guildMasters = new Dictionary<string, GuildMaster>();
                foreach (var text in guildMastersTexts)
                {
                    var guildMaster = FromText(text);
                    guildMasters.Add(guildMaster.Name, guildMaster);
                }
            }
        }

        private static GuildMaster FromText(string text)
        {
            var tokens = text.Split(
                new string[] { "\r\n" },
                StringSplitOptions.RemoveEmptyEntries
            );

            var attributes = new AttributeCollection();
            attributes.Add("classloyalty", new Dictionary<Class, int>());
            attributes.Add("raceloyalty", new Dictionary<Race, int>());
            foreach (var attributeDeskription in tokens)
                ParseAttribute(attributeDeskription, attributes);

            return BuildGuildMasterFromAttributes(attributes);

        }

        private static GuildMaster BuildGuildMasterFromAttributes(AttributeCollection attributes)
        {
            var guildMaster = new GuildMaster(
                attributes.Get<string>("name")
            );

            guildMaster.SameClassLoyaltyModifier =
                attributes.Get<int>("sameclassloyalty");
            guildMaster.SameRaceLoyaltyModifier =
                attributes.Get<int>("sameraceloyalty");
            guildMaster.Hystory =
                attributes.Get<string>("history");
            guildMaster.Race =
                attributes.Get<Race>("race");
            guildMaster.Class =
                attributes.Get<Class>("class");
            guildMaster.RaceLoyaltyModifiers =
                attributes.Get<Dictionary<Race, int>>("raceloyalty");
            guildMaster.ClassLoyaltyModifiers =
                attributes.Get<Dictionary<Class, int>>("classloyalty");


            return guildMaster;
        }

        private static void ParseAttribute(
            string attributeDeskription,
            AttributeCollection attributes
        )
        {
            var keyValue = attributeDeskription.Split(':');
            switch (keyValue[0].Trim().ToLower())
            {
                case "name":
                    attributes.Add("name", keyValue[1].Trim());
                    break;
                case "race":
                    attributes.Add("race", Race.Get(keyValue[1].Trim()));
                    break;
                case "class":
                    attributes.Add("class", Class.Get(keyValue[1].Trim()));
                    break;
                case "sameclassloyalty":
                    attributes.Add(
                        "sameclassloyalty", 
                        int.Parse(keyValue[1])
                    );
                    break;
                case "sameraceloyalty":
                    attributes.Add(
                        "sameraceloyalty",
                        int.Parse(keyValue[1])
                    );
                    break;
                case "classloyalty":
                    var nameValue = keyValue[1].Trim().Split(',');
                    attributes.Get<Dictionary<Class, int>>("classloyalty").
                        Add(
                            Class.Get(nameValue[0].Trim()),
                            int.Parse(nameValue[1])
                        );
                    break;
                case "raceloyalty":
                    nameValue = keyValue[1].Trim().Split(',');
                    attributes.Get<Dictionary<Race, int>>("raceloyalty").
                        Add(
                            Race.Get(nameValue[0].Trim()),
                            int.Parse(nameValue[1])
                        );
                    break;
                case "history":
                    attributes.Add("history", keyValue[1].Trim());
                    break;
            }
        }

        private static Dictionary<string, GuildMaster> guildMasters;
    }
}
