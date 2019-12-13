//(c) copyright by Martin M. Klöckener
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInitSingletons
{
    void InitSingleton();
}

public interface IInitSelf
{
    void InitSelf();
}

public interface IInitDependencies
{
    void InitDependencies();
}
