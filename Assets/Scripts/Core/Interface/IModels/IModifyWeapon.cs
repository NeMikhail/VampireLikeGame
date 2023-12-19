namespace Core.Interface.IModels
{
    public interface IModifyWeapon
    {
        public void ModifyDamage(float modifier);
        public void ModifyArea(float modifier);
        public void ModifyProjectileSpeed(float modifier);
        public void ModifyCooldown(float modifier);
        public void ModifyEffectDuration(float modifier);
        public void ModifyProjectilesAdditional(int modifier);
    }
}