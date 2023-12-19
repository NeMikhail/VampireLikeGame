
using Core.Interface.IPresenters;

namespace Core.Interface.UnityLifecycle
{
    public interface IFixedExecute : IPresenter
    {
        void FixedExecute(float fixedDeltaTime);
    }
}