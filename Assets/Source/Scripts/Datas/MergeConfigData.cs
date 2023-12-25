using UnityEngine;

[CreateAssetMenu(fileName = "MergeConfigData", menuName = "Configs/MergeConfig")]
public class MergeConfigData : ScriptableObject
{
    [field: SerializeField] public ParticleSystem MergeEffect { get; private set; }
    [field: SerializeField] public float MergeMoveDuration { get; private set; }
    [field: SerializeField] public float MergeScalingDuration { get; private set; }
    [field: SerializeField] public int RewardNormalScore { get; private set; }
    [field: SerializeField] public int RewardLastEvolutionScore { get; private set; }
}
