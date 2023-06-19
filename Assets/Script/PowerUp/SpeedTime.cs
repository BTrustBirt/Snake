using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTime : ActionColision
{
    public override void Use()
    {
        if(gameMenager.SnakeMovement.Speed < 0.3f)
        {
            EffectObject();
        }
        else
        {
            gameMenager.SnakeMovement.Speed -= 0.2f;
        }

        EffectObject();
    }
}
