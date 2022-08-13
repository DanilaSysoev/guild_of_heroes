using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Economy", menuName = "World/SettlementEconomy")]
public class SettlementEconomy : ScriptableObject, ISettlementEconomy
{
    [SerializeField]
    private float intakeVolume;
    public float IntakeVolume { get { return intakeVolume; } }

    [SerializeField]
    private float priceCoefficient;
    public float PriceCoefficient { get { return priceCoefficient; } }
}
