using Enums;
using UnityEngine;

namespace Core.Interface.ICollectable
{
    internal interface ICollectable
    {
        CollectableType CollectableType { get; }
        Transform Transform { get; }
    }
}