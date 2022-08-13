using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSettlementType", menuName = "World/SettlementType")]
public class SettlementType : ScriptableObject, ISettlementType
{
    [SerializeField]
    private string settlementTypeName;
    public string SettlementTypeName { get { return settlementTypeName; } }

    [SerializeField]
    private int size;
    public int Size { get { return size; } }

    [SerializeField]
    private SettlementEconomy settlementEconomy;
    public ISettlementEconomy SettlementEconomy { get { return settlementEconomy; } }
}
