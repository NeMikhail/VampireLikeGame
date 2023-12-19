using Core.Interface.IExperiences;
using Core.Interface.IPresenters;
using Core.Interface.IViews;
using System;

namespace MVP.Presenters.ExperiencePresenters
{
    internal class PickupExperiencePresenter : IInitialisation, ICleanUp
    {
        private IExperiencePickingUp _experiencePickingUpSphere;
        private ILevelBarConnector _levelBarConnector;
        private DropExperiencePresenter _dropExperiencePresenter;

        public PickupExperiencePresenter(IExperiencePickingUp experiencePickingUpObject, ILevelBarConnector levelBarConnector,
            DropExperiencePresenter dropExperiencePresenter)
        {
            _experiencePickingUpSphere = experiencePickingUpObject;
            _levelBarConnector = levelBarConnector;
            _dropExperiencePresenter = dropExperiencePresenter;
        }

        public void Initialisation()
        {
            _experiencePickingUpSphere.OnPickedSphere += _dropExperiencePresenter.PickupExperience;
            _experiencePickingUpSphere.OnPickingUpExperience += _levelBarConnector.AddValueExperience;
        }

        public void Cleanup()
        {
            _experiencePickingUpSphere.OnPickingUpExperience -= _levelBarConnector.AddValueExperience;
            _experiencePickingUpSphere.OnPickedSphere -= _dropExperiencePresenter.PickupExperience;
        }
    }
}