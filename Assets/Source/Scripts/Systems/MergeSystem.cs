using DG.Tweening;
using System;
using UnityEngine;
public class MergeSystem : GameSystem
{
    public event Action OnMergeEvent;

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

        int nextEvolutionStageID = collision.ID + 1;
        if (nextEvolutionStageID >= _game.EvolutionStages.Count)
        {
            //Explosion
            AddScorePoint(250);
            BackToPool(collision);
            BackToPool(target);
            OnMergeEvent?.Invoke();
            return;
        }

        ScalingAnimation(target.transform);
        ScalingAnimation(collision.transform);

        Vector3 centerByTwoAnimal = (collision.transform.position + target.transform.position) / 2;
        target.transform.DOMove(centerByTwoAnimal, .3f).SetEase(Ease.OutBack);
        collision.transform.DOMove(centerByTwoAnimal, .3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            BackToPool(collision);
            BackToPool(target);

            EvolutionStage evolution = _game.EvolutionStages[nextEvolutionStageID];
            var evolutionAnimal = Instantiate(evolution.Animal, centerByTwoAnimal, Quaternion.Euler(0, 180, 0));

            evolutionAnimal.Initialize(nextEvolutionStageID);
            evolutionAnimal.Collider.isTrigger = false;
            evolutionAnimal.Rigidbody.isKinematic = false;

            AddScorePoint(50); 
            OnMergeEvent?.Invoke();
        });
    }

    private void ScalingAnimation(Transform target)
    {
        target.DOScale(0, .4f).SetEase(Ease.InBack);
    }

    private void BackToPool(Animal animal)
    {
        animal.gameObject.SetActive(false);
    }

    private void AddScorePoint(int value)
    {
        _game.Score += value;
        _save.RecordScore = _game.Score > _save.RecordScore ? _game.Score : _save.RecordScore;
    }
}
