using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RGJ{
public class Music : MonoBehaviour
{
    
    //--- Singleton Behaviour ---
    #region Singleton
    private static Music instance_;
    public static Music Instance
        => instance_ == null ? FindObjectOfType<Music>() : instance_;

    public void Awake()
    {
        if (instance_ == null)
            instance_ = this;
        else
            Destroy(gameObject);
    }
    #endregion
    //--- ---
    
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
}