using UnityEngine;

namespace Core.Animations.Enemy
{
    public class EnemyAnimation
    {
        private Animator _animator;
        private static readonly int _animationSpeed = Animator.StringToHash("Speed");

        public EnemyAnimation(Animator animator)
        {
            _animator = animator;
        }
        
        public void Move(float speed)
        {
            _animator.SetFloat(_animationSpeed, speed);
        }
    }
}