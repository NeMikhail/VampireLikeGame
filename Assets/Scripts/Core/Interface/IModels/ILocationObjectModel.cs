using System;

namespace Core.Interface.IModels
{
    public interface ILocationObjectModel : IModel
    {
        public event Action OnBreaking;
        float Health { get; }
        void TakeDamage(float damage);
    }
}