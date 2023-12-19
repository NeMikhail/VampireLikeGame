using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Core.Interface;

namespace UI
{
    internal sealed class DamagePopupView : MonoBehaviour, IPoolObject<DamagePopupView>
    {
        private const float ACTIVE_TIME = 0.7f;
        private const float MOVE_SPEED = 0.2f;
    
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Color _normalDamageColor;
        [SerializeField] private Color _criticalDamageColor;
        [SerializeField] private Color _playerHealColor;
        [SerializeField] private Material _plainPreset;
        [SerializeField] private Material _dropShadowPreset;

        public event Action<DamagePopupView> OnSpawnObject;
        public event Action<DamagePopupView> OnDeSpawnObject;
        
        public bool IsActive { get; set; }
        public TextMeshProUGUI Text { get => _text; set => _text = value; }
        public Color NormalDamageColor { get => _normalDamageColor; }
        public Color CriticalDamageColor { get => _criticalDamageColor; }
        public Color PlayerHealColor { get => _playerHealColor; }
        public Material DropShadowPreset { get => _dropShadowPreset; }


        public void OnEnable()
        {
            StartCoroutine(FadeAway());
            OnSpawnObject?.Invoke(this);
        }

        public void OnDisable()
        {
            StopCoroutine(FadeAway());
            OnDeSpawnObject?.Invoke(this);
            _text.alpha = 1.0f;
            _text.fontSharedMaterial = _plainPreset;
        }

        private IEnumerator FadeAway()
        {
            float currentTime = 0;
            while (currentTime < ACTIVE_TIME)
            {
                if (Time.timeScale != 0)
                {
                    transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + Vector2.up,
                        MOVE_SPEED);
                    _text.alpha -= Time.deltaTime / ACTIVE_TIME;
                    currentTime += Time.deltaTime;
                }
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}