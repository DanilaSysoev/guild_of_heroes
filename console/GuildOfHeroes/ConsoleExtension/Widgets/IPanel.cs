﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleExtension.Widgets
{
    public interface IPanel
    {
        int Line { get; set; }
        int Column { get; set; }

        int Width { get; set; }
        int Height { get; set; }
    }
}