using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISettlementType
{
    string SettlementTypeName { get; }
    int Size { get; }
    ISettlementEconomy SettlementEconomy { get; }
}
