using System.Collections.Generic;
using Zenject;

public class InitializeAnimaEvolutionSystem : GameSystem
{
    [Inject] private readonly AnimalConfigData _animalConfigData;

    public override void OnAwake()
    {
        InitializeAnimalEvolution();
    }

    private void InitializeAnimalEvolution()
    {
        _game.EvolutionStages = new Dictionary<int, EvolutionStage>();

        for (int i = 0; i < _animalConfigData.EvolutionStage.Length; i++)
        {
            var stage = _animalConfigData.EvolutionStage[i];
            stage.Animal.Initialize(i);
            _game.EvolutionStages.Add(i, stage);
        }
    }
}
