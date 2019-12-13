using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace RGJ{
public class INIT001_Initialize : SerializedMonoBehaviour
{
    [SerializeField, BoxGroup("Settings")] private bool initOnSceneStart = true;
    [SerializeField, ReadOnly] public List<IInitSingletons>   singletonInits = new List<IInitSingletons>();
    //[SerializeField, ReadOnly] public List<ISaveable>         saveableInits  = new List<ISaveable>();
    //[SerializeField, ReadOnly] public List<IStoreable>        storeableInits = new List<IStoreable>();
    [SerializeField, ReadOnly] public List<IInitSelf>         selfInits      = new List<IInitSelf>();
    [SerializeField, ReadOnly] public List<IInitDependencies> dependentInits = new List<IInitDependencies>();

    private void Awake()
    {
        gameObject.SetActive(false); //wird der Initializer hier inaktiv gesetzt, wird kein Awake, Start, OnEnable der Kinder aufgerufen. Nichtmal OnDisable!
        //Man könnte also hier die Hierarchy inaktiv setzen, dann alles initialisieren (bei Spielstart) und dann erst zu einem späteren Zeitpunkt aktivieren.
        
        if(initOnSceneStart) InitializeAllChildren();
    }

    [Button, DisableInEditorMode]
    public void InitializeAllChildren()
    {
        ClearLists();

        GetSingletons(transform);
        InitSingletons();

        //GetSaveables(transform);
        //InitSaveables();

        //GetStoreables(transform);
        //InitStoreables();

        GetSelfAndDependent(transform);
        InitSelfAndDepentents();

        //System.GC.Collect(); //Force Garbage Collection

        gameObject.SetActive(true); //Initialization is finished --> activate Scene
    }

    //--- Init Singletons ---
    #region Init Singletons
    private void GetSingletons(Transform _root)
    {
        IInitSingletons[] _singleton = _root.GetComponents<IInitSingletons>();
        singletonInits.AddRange(_singleton);

        foreach(Transform t in _root)
        {
            if(t == _root) continue;  //make sure you don't initialize the existing transform
            GetSingletons(t);        //initialize this Transform's children recursively
        }
    }
    private void InitSingletons()
    {
        foreach(IInitSingletons _singleton in singletonInits)
        {
            _singleton.InitSingleton();
        }
    }
    #endregion
    //--- ---

    /*
    //--- Init Saveables ---
    #region Init Saveables
    private void GetSaveables(Transform _root)
    {
        ISaveable[] _saveable  = _root.GetComponents<ISaveable>();
        saveableInits.AddRange(_saveable);

        foreach(Transform t in _root)
        {
            if(t == _root) continue;  //make sure you don't initialize the existing transform
            GetSaveables(t);         //initialize this Transform's children recursively
        }
    }    
    private void InitSaveables()
    {
        foreach(ISaveable _saveable in saveableInits)
        {
            _saveable.InitSaveable();
        }
    }
    #endregion
    //--- ---

    //--- Init Storeables ---
    #region Init Storeables
    private void GetStoreables(Transform _root)
    {
        IStoreable[] _storeable = _root.GetComponents<IStoreable>();
        storeableInits.AddRange(_storeable);

        foreach(Transform t in _root)
        {
            if(t == _root) continue;  //make sure you don't initialize the existing transform
            GetStoreables(t);         //initialize this Transform's children recursively
        }
    }
    private void InitStoreables()
    {
        foreach(IStoreable _storeable in storeableInits)
        {
            _storeable.InitStoreable();
        }
    }
    #endregion
    //--- ---
    */

    //--- Init Self and Dependents ---
    #region Init Self and Dependents
    private void GetSelfAndDependent(Transform _root)
    {
        
        IInitSelf[]         _self      = _root.GetComponents<IInitSelf>();
        IInitDependencies[] _dependent = _root.GetComponents<IInitDependencies>();

        selfInits.AddRange(_self);
        dependentInits.AddRange(_dependent);

        foreach(Transform t in _root)
        {
            if(t == _root) continue;  //make sure you don't initialize the existing transform
            GetSelfAndDependent(t);  //initialize this Transform's children recursively
        }
    }
    private void InitSelfAndDepentents()
    {
        
        foreach(IInitSelf _self in selfInits)
        {
            _self.InitSelf();
        }
        foreach(IInitDependencies _dependent in dependentInits)
        {
            _dependent.InitDependencies();
        }
    }
    #endregion
    //--- ---

    private void ClearLists()
    {
        singletonInits.Clear();
        selfInits.Clear();
        dependentInits.Clear();
        //saveableInits.Clear();
        //storeableInits.Clear();
    }
}
}