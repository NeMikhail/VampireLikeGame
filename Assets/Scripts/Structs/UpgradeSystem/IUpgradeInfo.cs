using UnityEngine;

namespace Structs.UpgradeSystem
{
    internal interface IUpgradeInfo
    {
        public string Caption { get; set; }
        public Sprite Icon { get; set; }
        public string Description { get; set; }
        public float Probability { get; set; }
    }   
}