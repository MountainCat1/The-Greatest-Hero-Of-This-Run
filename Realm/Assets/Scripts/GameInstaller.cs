﻿using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInputManager>().To<InputManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ILevelManager>().To<LevelManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ITileMapData>().To<TileMapData>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ITileSelector>().To<TileSelector>().FromComponentInHierarchy().AsSingle();
    }      
}