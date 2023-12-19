using UnityEngine;
using Core.ResourceLoader;
using UI;

namespace Core
{
    internal sealed class ViewFactory
    {
        private Transform _canvasTransform;

        public void CreateMainCanvas()
        {
            GameObject prefab = ResourceLoadManager.
                GetPrefabComponentOrGameObject<GameObject>(ConstantsProvider.CANVAS_PREFAB_NAME);
            GameObject go = GameObject.Instantiate(prefab);
            _canvasTransform = go.transform;
        }

        public UiBattleScreen CreateBattleScreen()
        {
            UiBattleScreen prefab = ResourceLoadManager.
                GetPrefabComponentOrGameObject<UiBattleScreen>(ConstantsProvider.BATTLE_SCREEN_NAME);
            return GameObject.Instantiate(prefab, _canvasTransform);
        }

        public UiMainMenuScreen CreateMainMenuScreen()
        {
            UiMainMenuScreen prefab = ResourceLoadManager.
                GetPrefabComponentOrGameObject<UiMainMenuScreen>(ConstantsProvider.MAIN_MENU_SCREEN_NAME);
            return GameObject.Instantiate(prefab, _canvasTransform);
        }

        public UiUpgradeScreen CreateUpgradeScreen()
        {
            UiUpgradeScreen prefab = ResourceLoadManager.
                GetPrefabComponentOrGameObject<UiUpgradeScreen>(ConstantsProvider.SELECT_UPGRADE_SCREEN_NAME);
            return GameObject.Instantiate(prefab, _canvasTransform);
        }
    }
}