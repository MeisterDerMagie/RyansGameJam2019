using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ{
public class Piece : MonoBehaviour, ICollectible
{
    [SerializeField, FoldoutGroup("References"), Required] private SO_Collectibles collectibleType;
    
    public SO_Collectibles CollectibleType
    {
        get => collectibleType;
        set => collectibleType = value;
    }

    public ICollectible Collect()
    {
        return this;
    }
}
}