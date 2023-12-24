using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MergeSystem : GameSystem
{
    public event Action OnMergeEvent;

    [Inject] private readonly AnimalEvolutionConfigData _animalEvolutionConfigData;

    private Dictionary<int, EvolutionStage> EvolutionStages;

    public override void OnAwake()
    {
        InitializeAnimalEvolution();

        _game.MergingSingal.AddListener(Merge);
    }

    private void InitializeAnimalEvolution()
    {
        EvolutionStages = new Dictionary<int, EvolutionStage>();

        for (int i = 0; i < _animalEvolutionConfigData.EvolutionStage.Length; i++)
        {
            var stage = _animalEvolutionConfigData.EvolutionStage[i];
            stage.Animal.Initialize(i);
            EvolutionStages.Add(i, stage);
        }
    }

    private void DisablePhysicToAnimal(Animal collision, Animal target)
    {
        collision.Collider.enabled = false;
        target.Collider.enabled = false;
        collision.Rigidbody.isKinematic = true;
        target.Rigidbody.isKinematic = true;
    }

    private void Merge(Animal collision, Animal target)
    {
        DisablePhysicToAnimal(collision, target);

        int nextEvolutionStageID = collision.ID + 1;
        if (nextEvolutionStageID >= _animalEvolutionConfigData.EvolutionStage.Length)
        {
            //Explosion
            collision.gameObject.SetActive(false);
            target.gameObject.SetActive(false);
            return;
        }

        EvolutionStage evolution = EvolutionStages[nextEvolutionStageID];
        Vector3 centerByTwoAnimal = (collision.transform.position + target.transform.position) / 2;

        target.transform.DOScale(0, .4f).SetEase(Ease.InBack);
        collision.transform.DOScale(0, .4f).SetEase(Ease.InBack);
        target.transform.DOMove(centerByTwoAnimal, .3f).SetEase(Ease.OutBack);
        collision.transform.DOMove(centerByTwoAnimal, .3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            collision.gameObject.SetActive(false);
            target.gameObject.SetActive(false);

            var evolutionAnimal = Instantiate(evolution.Animal, centerByTwoAnimal, Quaternion.Euler(0, 180, 0));

            evolutionAnimal.Initialize(nextEvolutionStageID);
            evolutionAnimal.Collider.isTrigger = false;
            evolutionAnimal.Rigidbody.isKinematic = false;

            _game.Score += 50;
            _save.RecordScore = _game.Score > _save.RecordScore ? _game.Score : _save.RecordScore;

            OnMergeEvent?.Invoke();
        });
    }
}
