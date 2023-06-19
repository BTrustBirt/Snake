using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTail : ActionColision
{
    public override void Use()
    {
        gameMenager.Snake.addTail = true;
        EffectObject();
    }
}
