using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public BaseState(GameObject _gameObject)
    {
        this.gameObject = _gameObject;
        this.transform = _gameObject.transform;
    }

    protected GameObject gameObject;
    protected Transform transform;

    public abstract Type Tick();
}