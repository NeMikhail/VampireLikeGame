namespace Core.ImprovmentSystem.Upgrade
{
    public interface IUpgrade
    {
        public string Name { get; }
        public void Apply();
    }
}