using GameScripts.Extensions;
using GameScripts.Infrastructure.States;
using GameScripts.Services.AssetManagement;
using GameScripts.Services.Data;
using GameScripts.Services.Factories;
using GameScripts.Services.InputService;
using GameScripts.Services.UnitSpawner;
using GameScripts.Services.Unity;
using UnityEngine;
using Zenject;

namespace GameScripts.Infrastructure
{
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
}