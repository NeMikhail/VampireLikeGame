using System.Collections.Generic;
using Core.Interface;
using Core.Interface.IPresenters;
using Core.Interface.UnityLifecycle;

namespace Core
{
    public class Presenters : IPresenter
    {
        private readonly List<IInitialisation> _initializePresenters;
        private readonly List<IExecute> _executePresenters;
        private readonly List<IFixedExecute> _fixedExecutePresenters;
        private readonly List<ICleanUp> _cleanupPresenters;


        public Presenters()
        {
            _initializePresenters = new List<IInitialisation>();
            _executePresenters = new List<IExecute>();
            _fixedExecutePresenters = new List<IFixedExecute>();
            _cleanupPresenters = new List<ICleanUp>();
        }

        public Presenters Add(IPresenter _controller)
        {
            if (_controller is IInitialisation initializeController)
                _initializePresenters.Add(initializeController);

            if (_controller is IExecute executeController)
                _executePresenters.Add(executeController);

            if (_controller is IFixedExecute fixedExecuteController)
                _fixedExecutePresenters.Add(fixedExecuteController);

            if (_controller is ICleanUp cleanUpController)
                _cleanupPresenters.Add(cleanUpController);

            return this;
        }

        public void Initialization()
        {
            for (var i = 0; i < _initializePresenters.Count; ++i)
            {
                _initializePresenters[i].Initialisation();
            }
        }

        public void Execute(float deltaTime)
        {
            for (var index = 0; index < _executePresenters.Count; ++index)
            {
                _executePresenters[index].Execute(deltaTime);
            }
        }

        public void FixedExecute(float fixedDeltaTime)
        {
            for (var index = 0; index < _fixedExecutePresenters.Count; ++index)
            {
                _fixedExecutePresenters[index].FixedExecute(fixedDeltaTime);
            }
        }

        public void Cleanup()
        {
            for (var index = 0; index < _cleanupPresenters.Count; ++index)
            {
                _cleanupPresenters[index].Cleanup();
            }
        }
    }
}