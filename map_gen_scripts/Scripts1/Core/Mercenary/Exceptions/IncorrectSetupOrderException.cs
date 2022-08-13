using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncorrectSetupOrderException : Exception
{
    public IncorrectSetupOrderException() :
        base("Order of setup of parameters is incorrect")
    { }
}
