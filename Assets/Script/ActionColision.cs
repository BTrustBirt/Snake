using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionColision : MonoBehaviour
{

    protected GameMenager gameMenager;

    public ParticleSystem particleSystem;
    
    public void GetRef(GameMenager gameRef)
    {
        gameMenager = gameRef;
        gameMenager.Grid.AddToGrid(new Vector2Int((int)transform.position.x, (int)transform.position.y), this.gameObject);
        particleSystem.Stop();
    }

    public abstract void Use();

    protected void EffectObject()
    {
        particleSystem.Play();
        Invoke("destroyObject", 1f);
    }

    private void destroyObject()
    {
        Destroy(gameObject);
    }
}
