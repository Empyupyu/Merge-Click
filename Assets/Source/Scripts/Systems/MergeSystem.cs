using DG.Tweening;
using ModestTree;
using System.Linq;
using UnityEngine;
using Zenject;
public class MergeSystem : GameSystem
{
    [Inject] private readonly AnimalEvolutionConfigData _animalEvolutionConfigData;

    public override void OnAwake()
    {
        _game.MergingSingal.AddListener(Merge);
    }

    private void Merge(Animal collision, Animal target)
    {
        collision.Collider.enabled = false;
        target.Collider.enabled = false;
        collision.Rigidbody.isKinematic = true;
        target.Rigidbody.isKinematic = true;

        EvolutionStage evolution = _animalEvolutionConfigData.EvolutionStage.FirstOrDefault(e => e.AnimalType.Equals(collision.AnimalType));
        int nextEvolutionStageID = _animalEvolutionConfigData.EvolutionStage.IndexOf(evolution) + 1;

        if (nextEvolutionStageID >= _animalEvolutionConfigData.EvolutionStage.Length) return;

        evolution = _animalEvolutionConfigData.EvolutionStage[nextEvolutionStageID];
        Vector3 centerByTwoAnimal = (collision.transform.position + target.transform.position) / 2;

        target.transform.DOScale(0, .4f).SetEase(Ease.InBack);
        collision.transform.DOScale(0, .4f).SetEase(Ease.InBack);
        target.transform.DOMove(centerByTwoAnimal, .3f).SetEase(Ease.OutBack);
        collision.transform.DOMove(centerByTwoAnimal, .3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            collision.gameObject.SetActive(false);
            target.gameObject.SetActive(false);

            var evolutionAnimal = Instantiate(evolution.Animal, centerByTwoAnimal, Quaternion.Euler(0, 180, 0));

            evolutionAnimal.Collider.isTrigger = false;
            evolutionAnimal.Rigidbody.isKinematic = false;
        });
    }
}
