using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IPauseSystem
{
    public abstract void Pause();
    public abstract void Unpause();

    public abstract void SetPauseEvents();

}
