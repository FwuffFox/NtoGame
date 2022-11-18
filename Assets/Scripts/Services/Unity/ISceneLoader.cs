using System;

namespace Services.Unity
{
    public interface ISceneLoader
    {
        void LoadAsync(string sceneName, Action onLoaded);
        void LoadStraight(string sceneName);
    }
}