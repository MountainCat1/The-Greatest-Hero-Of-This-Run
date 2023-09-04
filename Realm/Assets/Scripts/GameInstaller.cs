﻿using Zenject;

public class GameInstaller : MonoInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IInputManager>().To<InputManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IGameManager>().To<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ITileMapData>().To<TileMapData>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ITileSelector>().To<TileSelector>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IBuildingController>().To<BuildingController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ITurnManager>().To<TurnManager>().FromComponentInHierarchy().AsSingle();
        
        Container.Bind<IGameState>().To<GameState>().AsSingle();
        Container.Bind<IGameStateManager>().To<GameStateMutationManager>().AsSingle();
        Container.Bind<IPlayerController>().To<PlayerController>().AsSingle();
    }      
}