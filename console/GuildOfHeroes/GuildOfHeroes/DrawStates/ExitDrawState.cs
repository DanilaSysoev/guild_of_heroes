using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class ExitDrawState : DrawState
    {
        public static ExitDrawState Instance => instance;

        public override void OnEnter()
        {
            Game.Instance.StopGame();
        }


        private ExitDrawState() { }

        private static ExitDrawState instance = new ExitDrawState();
    }
}
