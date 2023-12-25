using DG.Tweening;
using Lean.Pool;
using System;
using UnityEngine;
using Zenject;

public class MergeSystem : GameSystem
{
    public event Action OnMergeEvent;

    [Inject] private readonly MergeConfigData _mergeConfigData;

    public override void OnAwake()
    {
        _game.MergingSingal.AddListener(Merge);
    }

    private void DisablePhysicToAnimal(Animal target)
    {
        target.Collider.enabled = false;
        target.Rigidbody.isKinematic = true;
    }

    private void Merge(Animal collision, Animal target)
    {
        DisablePhysicToAnimal(collision);
        DisablePhysicToAnimal(target);
        ScalingAnimation(target.transform);
        ScalingAnimation(collision.transform);
        MergingAnimaion(collision, target);
    }

    private void ScalingAnimation(Transform target)
    {
        target.DOScale(0, _mergeConfigData.MergeScalingDuration).SetEase(Ease.InBack);
    }

    private void MergingAnimaion(Animal collision, Animal target)
    {
        Vector3 centerByTwoAnimal = (collision.transform.position + target.transform.position) / 2;

        target.transform.DOMove(centerByTwoAnimal, _mergeConfigData.MergeMoveDuration).SetEase(Ease.OutBack);
        collision.transform.DOMove(centerByTwoAnimal, _mergeConfigData.MergeMoveDuration).SetEase(Ease.OutBack).OnComplete(() =>
        {
            BackToPool(collision);
            BackToPool(target);

            int nextEvolutionStageID = collision.ID + 1;
            bool canEvolution = nextEvolutionStageID < _game.EvolutionStages.Count;
            int rewardScore = canEvolution ? _mergeConfigData.RewardNormalScore : _mergeConfigData.RewardLastEvolutionScore;

            if (canEvolution)
            {
                EvolutionStage evolution = _game.EvolutionStages[nextEvolutionStageID];
                var evolutionAnimal = LeanPool.Spawn(evolution.Animal, centerByTwoAnimal, Quaternion.Euler(0, 180, 0));

                evolutionAnimal.Initialize(nextEvolutionStageID);
                evolutionAnimal.ResetToOrigin();

                evolutionAnimal.Collider.isTrigger = false;
                evolutionAnimal.Rigidbody.isKinematic = false;
            }

            LeanPool.Spawn(_mergeConfigData.MergeEffect, centerByTwoAnimal + _mergeConfigData.MergeEffectOffset, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_mergeConfigData.MereSoundEffect, centerByTwoAnimal);

            AddScorePoint(rewardScore);

            OnMergeEvent?.Invoke();
        });
    }

    private void BackToPool(Animal animal)
    {
        LeanPool.Despawn(animal);
    }

    private void AddScorePoint(int value)
    {
        _game.Score += value;
        _save.RecordScore = _game.Score > _save.RecordScore ? _game.Score : _save.RecordScore;
    }
}
