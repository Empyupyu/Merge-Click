﻿
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AnimalsConfigData", menuName = "Configs/AnimalsConfig")]
public class AnimalConfigData : ScriptableObject
{
    [field: SerializeField] public int AnimalsInPreview { get; private set; }
    [field: SerializeField] public float OffsetPerAnimalInRow { get; private set; }
    [field: SerializeField] public float SortingDuration { get; private set; }
    [field: SerializeField] public Vector3 AnimalsInPreviewScale { get; private set; }
    [field: SerializeField] public Vector3 AnimalsInPreviewPosition { get; private set; }
    [field: SerializeField] public EvolutionStage[] EvolutionStage { get; private set; }
    [field: SerializeField] public int MaximumEvolutionStageLevelInPreview { get; private set; }
}
