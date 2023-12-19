using System.Collections.Generic;
using Core.Interface;
using Core.Interface.IPresenters;
using Core.Interface.UnityLifecycle;

namespace Infrastructure.TimeRemaningService
{
    public sealed class TimeRemainingController : IExecute, IPresenter
    {

        private readonly List<ITimeRemaining> _timeRemainings;

        public TimeRemainingController()
        {
            _timeRemainings = TimeRemainingExtensions.TimeRemainings;
        }

        public void Execute(float deltaTime)
        {
            for (var i = 0; i < _timeRemainings.Count; i++)
            {
                var obj = _timeRemainings[i];
                obj.CurrentTime -= deltaTime;
                if (obj.CurrentTime <= 0.0f)
                {
                    if (!obj.IsRepeating)
                    {
                        obj.RemoveTimeRemaining();
                    }
                    else
                    {
                        obj.CurrentTime = obj.Time;
                    }
                    obj?.Method?.Invoke();
                }
            }
        }

    }
}
