using UnityEngine;

[CreateAssetMenu(fileName = "AnimalEvolutionConfigData", menuName = "Configs/AnimalEvolutionConfig")]
public class AnimalEvolutionConfigData : ScriptableObject
{
    [field: SerializeField] public EvolutionStage[] EvolutionStage { get; private set; }
}
