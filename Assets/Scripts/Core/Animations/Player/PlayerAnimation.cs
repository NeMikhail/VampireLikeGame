using UnityEngine;

namespace Core.Animations.Player
{
    public class PlayerAnimation
    {
        private Animator _animator;
        private static readonly int _animationSpeed = Animator.StringToHash("Speed");

        public PlayerAnimation(Animator animator)
        {
            _animator = animator;
        }
        public void Move(float speed)
        {
            _animator.SetFloat(_animationSpeed, speed);
        }
    }
}