using Supyrb;
using System;
public class GameOverCheckerSystem : GameSystem
{
    public override void OnAwake()
    {
        _game.OnAnimalDeselectedSingal.AddListener(CheckAnimalPosition);   
    }

    private void CheckAnimalPosition()
    {

    }
}
