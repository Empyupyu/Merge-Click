using DG.Tweening;
using Lean.Pool;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class MergeSystem : GameSystem
{
    public event Action OnMergeEvent;

    [Inject] private readonly MergeConfigData _mergeConfigData;

    private Vector3 _centerBetweenAnimals;
    private bool _canEvolution;
    private Animal _select;
    private Animal _target;

    public override void OnAwake()
    {
        _game.MergingSingal.AddListener(Merge);
    }

    private void DisablePhysicToAnimal(Animal target)
    {
        target.Collider.enabled = false;
        target.Rigidbody.isKinematic = true;
    }

    private async void Merge(Animal select, Animal target)
    {
        _select = select;
        _target = target;

        DisablePhysicToAnimal(_select);
        DisablePhysicToAnimal(_target);
        ScalingAnimation(_target.transform);
        ScalingAnimation(_select.transform);
        GetCenterBetweenAnimals();
        MovingAnimation(_target.transform);
        await MovingAnimation(_select.transform);

        MergeCompleted();
    }

    private void ScalingAnimation(Transform target)
    {
        target.DOScale(0, _mergeConfigData.MergeScalingDuration).SetEase(Ease.InBack);
    }

    private Task MovingAnimation(Transform target)
    {
       return _target.transform.DOMove(_centerBetweenAnimals, _mergeConfigData.MergeMoveDuration).SetEase(Ease.OutBack).AsyncWaitForCompletion();
    }

    private void GetCenterBetweenAnimals()
    {
        _centerBetweenAnimals = (_select.transform.position + _target.transform.position) / 2;
    }

    private void MergeCompleted()
    {
        BackToPool(_select);
        BackToPool(_target);
        CreateNewAnimalStage();
        CreateMergeEffects();
        AddScorePoint();

        OnMergeEvent?.Invoke();
    }

    private void BackToPool(Animal animal)
    {
        LeanPool.Despawn(animal);
    }

    private void CreateNewAnimalStage()
    {
        int nextEvolutionStageID = _select.ID + 1;
        _canEvolution = nextEvolutionStageID < _game.EvolutionStages.Count;

        if (!_canEvolution) return;

        EvolutionStage evolution = _game.EvolutionStages[nextEvolutionStageID];
        var evolutionAnimal = LeanPool.Spawn(evolution.Animal, _centerBetweenAnimals, Quaternion.Euler(0, 180, 0));

        evolutionAnimal.Initialize(nextEvolutionStageID);
        evolutionAnimal.ResetToOrigin();

        evolutionAnimal.Collider.isTrigger = false;
        evolutionAnimal.Rigidbody.isKinematic = false;
    }

    private void CreateMergeEffects()
    {
        LeanPool.Spawn(_mergeConfigData.MergeEffect, _centerBetweenAnimals + _mergeConfigData.MergeEffectOffset, Quaternion.identity);
        AudioSource.PlayClipAtPoint(_mergeConfigData.MereSoundEffect, _centerBetweenAnimals);
    }

    private void AddScorePoint()
    {
        int value = _canEvolution ? _mergeConfigData.RewardNormalScore : _mergeConfigData.RewardLastEvolutionScore;
        _game.Score += value;

        if (_game.Score <= _save.RecordScore) return;

        _save.RecordScore = _game.Score;
        SaveUtility.Instance().Save(_save);
    }
}
