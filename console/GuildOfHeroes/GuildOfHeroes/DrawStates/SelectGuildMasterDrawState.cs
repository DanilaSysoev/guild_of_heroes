using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildOfHeroes
{
    public class SelectGuildMasterDrawState : DrawStateBasedOnSelectionUnit
    {
        private const int DRAW_LIST_TOP_OFFSET = 2;
        private const int DRAW_LIST_LEFT_OFFSET = 2;

        public SelectGuildMasterDrawState(
            List<List<ISelectionUnit>> selectionUnits
        ) : base(selectionUnits)
        {
        }

        public override void Draw()
        {
            Console.Clear();

            DrawGuildMasterList();
            DrawSelectedGuildMasterAttributes();
        }

        private void DrawGuildMasterList()
        {
        }

        private void DrawSelectedGuildMasterAttributes()
        {
        }

    }
}
