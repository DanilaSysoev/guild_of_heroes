using ConsoleExtension.Service;
using GuildOfHeroes.Entities;
using System;

namespace GuildOfHeroes.ConsoleInterface.UIElements
{
    class GuildMasterDescriptionUIElement
    {
        private GuildMaster guildMaster;
        public GuildMaster GuildMaster
        {
            get
            {
                return guildMaster;
            }
            set
            {
                guildMaster = value;
                SetupWidgets();
            }
        }

        public GuildMasterDescriptionUIElement(Rectangle area)
        {

        }

        public void Draw()
        {

        }

        private void SetupWidgets()
        {
            throw new NotImplementedException();
        }
    }
}
