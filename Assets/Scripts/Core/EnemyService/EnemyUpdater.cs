namespace Core.EnemyService
{
    internal class EnemyUpdater
    {
        private readonly RespownSystem _respownSystem;

        public EnemyUpdater(RespownSystem respownSystem)
        {
            _respownSystem = respownSystem;
        }

        public void FixedExecute(float deltaTime)
        {
            for (int i = 0; i < _respownSystem.ActiveEnemies.Count; i++)
            {
                _respownSystem.ActiveEnemies[i].FixedExecute(deltaTime);
            }
        }
    }
}