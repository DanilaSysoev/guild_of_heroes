using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class SelectAction : IInputAction
    {
        public IGameDrawState ApplyThis(IGameDrawState currentState)
        {
            return currentState.ApplySelectAction(this);
        }

        private SelectAction() { }


        public static readonly SelectAction 
        Instance = new SelectAction();
    }
}
