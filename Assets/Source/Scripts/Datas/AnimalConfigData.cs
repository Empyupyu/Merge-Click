
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalsConfigData", menuName = "Configs/AnimalsConfig")]
public class AnimalConfigData : ScriptableObject
{
    [field: SerializeField] public List<Animal> Animals { get; private set; }
}
