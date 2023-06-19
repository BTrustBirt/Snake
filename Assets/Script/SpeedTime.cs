using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTime : ActionColision
{
    public override void Use()
    {
        gameMenager.IngressSpeed();
        EffectObject();
    }
}
