using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : ActionColision
{
    public override void Use()
    {
        gameMenager.GameOver();

    }
}
