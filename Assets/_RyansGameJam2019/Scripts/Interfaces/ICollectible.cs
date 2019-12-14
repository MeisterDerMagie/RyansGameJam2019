using DefaultNamespace;
using UnityEngine;

namespace RGJ{
public interface ICollectible
{
    SO_Collectibles CollectibleType { get; set; }
    ICollectible Collect();
}
}