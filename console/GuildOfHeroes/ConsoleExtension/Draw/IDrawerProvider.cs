using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtension.Draw
{
    public interface IDrawerProvider
    {
        IDrawer Get();
    }
}
