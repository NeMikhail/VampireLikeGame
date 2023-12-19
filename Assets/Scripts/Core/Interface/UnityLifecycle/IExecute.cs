using Core.Interface.IPresenters;

namespace Core.Interface.UnityLifecycle
{
    public interface IExecute : IPresenter
    {
        void Execute(float deltaTime);
    }
}