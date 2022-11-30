using GuildOfHeroes.ConsoleInterface.Base;
using GuildOfHeroes.Core;
using GuildOfHeroes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes.ConsoleInterface.Screens
{
    class SelectGuildMasterScreen : ICommandExecuter, IDrawManager
    {
        private SelectionList<GuildMaster> guildMasters;

        public SelectGuildMasterScreen(int width, int height)
        {

        }

        public void Draw(World world)
        {
            throw new NotImplementedException();
        }

        public void ExecuteDownCommand()
        {
            throw new NotImplementedException();
        }

        public void ExecuteLeftCommand()
        {
            throw new NotImplementedException();
        }

        public void ExecuteRightCommand()
        {
            throw new NotImplementedException();
        }

        public void ExecuteSelectCommand()
        {
            throw new NotImplementedException();
        }

        public void ExecuteUpCommand()
        {
            throw new NotImplementedException();
        }

        public void Setup()
        {
            throw new NotImplementedException();
        }
    }
}
