using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldConstants : MonoBehaviour, IWorldConstants
{
    public static IWorldConstants Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private float roadPassability;
    public float RoadPassability { get { return roadPassability; } }
}
