
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalsConfigData", menuName = "Configs/AnimalsConfig")]
public class AnimalConfigData : ScriptableObject
{
    [field: SerializeField] public int AnimalsInPreview { get; private set; }
    [field: SerializeField] public Vector3 AnimalsInPreviewScale { get; private set; }
    [field: SerializeField] public List<Animal> Animals { get; private set; }
}
