using UnityEngine;
using Core.Interface;
using Core.Interface.IPresenters;
using Core.ResourceLoader;
using Infrastructure.PoolSystems.Pool;
using UI;
using System;

namespace MVP.Presenters.WeaponPresenters
{
    internal sealed class DamagePopupPresenter : IInitialisation
    {
        private const float DAMAGE_FONT_SIZE = 38;
        private const float HEAL_FONT_SIZE = 58;
        private const float PLAYER_DAMAGE_FONT_SIZE = 48;
        private const float HEAL_POPUP_X_POS = 10;
        private const float HEAL_POPUP_Y_POS = 3;
        private readonly IViewProvider _viewProvider;
        private readonly Canvas _mainCanvas;
        private DamagePopupView _popupView;
        private bool _poolParentChanged;

        public DamagePopupPresenter(IViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
            _mainCanvas = _viewProvider.GetBattleScreen().GetComponentInParent<Canvas>();
        }


        public void Initialisation()
        {
            _popupView = ResourceLoadManager.GetPrefabComponentOrGameObject<DamagePopupView>("DamagePopup");
        }

        public void CreateDamagePopup(Transform transform, float damage, PopUpType popUpType)
        {
            if (!_poolParentChanged)
                SetPoolParent();

            DamagePopupView popupView = GamePool.DamagePopup.Pool.Spawn(_popupView.gameObject);
            Vector2 canvasPosition = Camera.main.WorldToScreenPoint(transform.position);
            popupView.transform.position = canvasPosition;
            if (popUpType == PopUpType.PlayerHeal || popUpType == PopUpType.PlayerDamage)
                damage = RoundDamageValue(damage);
            popupView.Text.text = damage.ToString();
            popupView.Text.fontSize = DAMAGE_FONT_SIZE;

            switch (popUpType)
            {
                case PopUpType.EnemyCriticalDamage:
                    popupView.Text.color = popupView.CriticalDamageColor;
                    break;
                case PopUpType.EnemyNormalDamage:
                    popupView.Text.color = popupView.NormalDamageColor;
                    break;
                case PopUpType.PlayerHeal:
                    popupView.Text.color = popupView.PlayerHealColor;
                    popupView.Text.fontSize = HEAL_FONT_SIZE;
                    popupView.transform.position += new Vector3(HEAL_POPUP_X_POS, HEAL_POPUP_Y_POS, 0);
                    popupView.Text.fontSharedMaterial = popupView.DropShadowPreset;
                    break;
                default:
                    popupView.Text.text = (damage * -1).ToString();
                    popupView.Text.color = Color.red;
                    popupView.Text.fontSize = PLAYER_DAMAGE_FONT_SIZE;
                    popupView.transform.position += new Vector3(HEAL_POPUP_X_POS, HEAL_POPUP_Y_POS, 0);
                    popupView.Text.fontSharedMaterial = popupView.DropShadowPreset;
                    break;
            }
        }

        private float RoundDamageValue(float damage)
        {
            if (damage % 0.1f != 0)
            {
                damage = (float)Math.Round(damage, 1);
                if (damage == 0)
                    damage = 0.1f;
            }

            return damage;
        }

        private void SetPoolParent()
        {
            GamePool.DamagePopup.Pool.Root.SetParent(_mainCanvas.transform);
            GamePool.DamagePopup.Pool.Root.SetAsFirstSibling();
            _poolParentChanged = true;
        }
    }
}