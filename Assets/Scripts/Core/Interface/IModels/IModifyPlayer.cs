namespace Core.Interface.IModels
{
    public interface IModifyPlayer
    {
        public void ModifySpeed(float modifier);
        public void ModifyMaxHealth(float modifier);
        public void ModifyArmor(float modifier);
        public void ModifyRegeneration(float modifier);
        public void ModifyExpirienceMultiplier(float modifier);
        public void ModifyExpiriencePickupRange(float modifier);
        public void ModifyRevivesCount(float modifier);
    }
}