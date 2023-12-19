using Enums;

namespace Core.EnemyService.Patterns.PatternVariants
{
    internal sealed class FinalliBossPattern : AbstractEnemyPattern
    {
        private EnemyType _enemyType;

        public FinalliBossPattern(EnemyType enemyType)
        {
            _enemyType = enemyType;
        }


        public override void OnStart()
        {
            while (_respownSystem.ActiveEnemies.Count > 0)
                _respownSystem.ActiveEnemies[0].Remove();
            _respownSystem.Respown(_enemyType);
        }
    }
}