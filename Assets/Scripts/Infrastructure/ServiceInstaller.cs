using Extensions;
using Infrastructure;
using Infrastructure.States;
using Services;
using Services.AssetManagement;
using Services.Data;
using Services.Factories;
using Services.Unity;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class ServiceInstaller : MonoInstaller, IInitializable
{
    [SerializeField] private CoroutineRunner coroutineRunner;


    public override void Start()
    {
        base.Start();
        Initialize();
    }

    public void Initialize()
    {
        Container.Resolve<GameStateMachine>()
            .With(stateMachine => stateMachine.CreateStates())
            .Enter<BootstrapState>();
    }
    
    public override void InstallBindings()
    {
        BindServices();
        BindFactories();

        Container.Bind<GameStateMachine>().AsSingle().NonLazy();
    }

    private void BindServices()
    {
        Container.Bind<ICoroutineRunner>().To<CoroutineRunner>().FromInstance(coroutineRunner).AsSingle();
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
        Container.Bind<IUnitSpawner>().To<UnitSpawner>().AsSingle();
        Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        Container.Bind<IInputService>().To<InputService>().AsSingle();
    }

    private void BindFactories()
    {
        Container.Bind<IPrefabFactory>().To<PrefabFactory>().AsSingle();
        Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
    }
}