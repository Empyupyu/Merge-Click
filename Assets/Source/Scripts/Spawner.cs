using UnityEngine;
using Zenject;
public class Spawner : GameSystem
{
    [Inject] private readonly AnimalConfigData _animalConfigData;

    public override void OnAwake()
    {
        print("_gameData" + _game);

        print("_animalConfigData" + _animalConfigData);

        Spawn();
    }

    private void Spawn()
    {
        var animals = _animalConfigData.Animals;
        var animalPrefab = animals[Random.Range(0, animals.Count)];

        var newAnimal = Instantiate(animalPrefab, new Vector3(0, 2f, 0), Quaternion.identity);
        _game.CurrentAnimal = newAnimal;
    }
}
