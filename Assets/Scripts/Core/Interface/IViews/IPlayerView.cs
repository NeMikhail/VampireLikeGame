using MVP.Views.CollectableViews;
using MVP.Views.PlayerViews;
using UnityEngine;

namespace Core.Interface.IViews
{
    internal interface IPlayerView : IValueSatable, IView
    {
        public GameObject PlayerObject { get; set; }
        public Transform PlayerTransform { get; set; }
        public DetectorPlayerCollision DetectorPlayerCollision { get; }
        public DetectorCollectableCollision DetectorCollactableCollision { get; }
    }
}