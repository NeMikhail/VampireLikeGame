using System;
using MVP.Presenters.ChestPresenters;
using MVP.Presenters.ExperiencePresenters;
using MVP.Presenters.GameState;
using MVP.Presenters.WorldGenerator;

namespace Core
{
    public sealed class PresentersProvider
    {
        private LocationGeneratorPresenter _locationGeneratorPresenter;
        private DropExperiencePresenter _dropExperiencePresenter;
        private EndGamePresenter _endGamePresenter;
        private readonly DropChestPresenter _dropChestPresenter;

        internal event Action<PresentersProvider> OnEndGamePresenterSet;

        public LocationGeneratorPresenter LocationGeneratorPresenter
        {
            get => _locationGeneratorPresenter;
            set => _locationGeneratorPresenter = value;
        }
        internal DropExperiencePresenter DropExperiencePresenter
        {
            get => _dropExperiencePresenter;
        }
        internal DropChestPresenter DropChestPresenter
        {
            get => _dropChestPresenter;
        }
        internal EndGamePresenter EndGamePresenter
        {
            get => _endGamePresenter;
            set 
            {
                _endGamePresenter = value;
                OnEndGamePresenterSet?.Invoke(this);
            }
        }

        public PresentersProvider()
        {
            _dropExperiencePresenter = new DropExperiencePresenter();
            _dropChestPresenter = new DropChestPresenter();
        }
    }
}