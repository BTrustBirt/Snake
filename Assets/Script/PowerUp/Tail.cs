using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : ActionColision
{
    public override void Use()
    {
        gameMenager.GameOver();

        EffectObject();
    }
}
