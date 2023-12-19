using System;

namespace Core.EnemyService.Patterns
{
    public interface IExecutablePattern
    {
        public event Action<IExecutablePattern> OnComplet;
        public void OnExecute();
    }
}