using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    [SerializeField]
    BoardManager boardManager;
    [SerializeField]
    BoardGeneratingManager boardGeneratingManager;
    [SerializeField]
    InputManager inputManager;

    public override void InstallBindings()
    {
        Container.Bind<BoardManager>().FromComponentOn(boardManager.gameObject).AsSingle();
        Container.Bind<BoardGeneratingManager>().FromComponentOn(boardGeneratingManager.gameObject).AsSingle();
        Container.Bind<InputManager>().FromComponentOn(inputManager.gameObject).AsSingle();
    }
}
