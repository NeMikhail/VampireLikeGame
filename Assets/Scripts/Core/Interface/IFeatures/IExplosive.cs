namespace Core.Interface.IFeatures
{
    internal interface IExplosive
    {
        public float Radius { get; set; }
        public void Explode();
        public float Damage { get; set; }
    }
}