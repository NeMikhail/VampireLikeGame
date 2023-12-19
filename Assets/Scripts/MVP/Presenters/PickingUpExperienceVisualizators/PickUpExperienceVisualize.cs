using UnityEngine;
using Configs.PickingExperienceLists;
using Core.Interface.IPresenters;
using Core.Interface.UnityLifecycle;
using Core.ResourceLoader;
using MVP.Views.PlayerViews;

namespace MVP.Presenters.PickingUpExperienceVisualizators
{
    internal sealed class PickUpExperienceVisualize : IFixedExecute, ICleanUp
    {
        private const float DISTANCE_OF_PICK_UP = 1f;
        private const float FORCE = 8f;

        private PickingExperienceList _experienceList;
        private PlayerView _playerView;

        public PickUpExperienceVisualize(PlayerView playerView)
        {
            _playerView = playerView;
            _experienceList = ResourceLoadManager.GetConfig<PickingExperienceList>();
        }

        public void FixedExecute(float fixedDeltaTime)
        {
            if (_experienceList.experiences.Count == 0)
            {
                return;
            }

            for (int i = 0; i < _experienceList.experiences.Count; i++)
            {
                Vector3 playerTransform = _playerView.PlayerTransform.position;
                Vector3 direction = playerTransform - _experienceList.experiences[i].ExperienceObject.transform.position;
                if (direction.sqrMagnitude <= DISTANCE_OF_PICK_UP)
                {
                    _playerView.DetectorPlayerCollision.ActivatePickUpActions(_experienceList.experiences[i]);
                    _experienceList.experiences[i].Rigidbody.velocity = Vector3.zero;
                    _experienceList.experiences.Remove(_experienceList.experiences[i]);
                    continue;
                }
                _experienceList.experiences[i].Rigidbody.velocity = direction * FORCE;
            }
        }

        public void Cleanup()
        {
            _experienceList.experiences.Clear();
        }
    }
}