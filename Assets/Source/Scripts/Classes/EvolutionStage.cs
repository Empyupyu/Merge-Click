using System;
using UnityEngine;

[Serializable]
public class EvolutionStage 
{
    [field: SerializeField] public AnimalType AnimalType { get; private set; }
    [field: SerializeField] public Animal Animal { get; private set; }
}