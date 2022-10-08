using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleExtension.Widgets
{
    public interface IChild
    {
        IPanel Parent { get; }
    }
}
