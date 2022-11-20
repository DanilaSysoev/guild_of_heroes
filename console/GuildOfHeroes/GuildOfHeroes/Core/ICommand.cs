using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes.Core
{
    public interface ICommand
    {
        void Execute(ICommandExecuter commandProcessor);
    }
}
