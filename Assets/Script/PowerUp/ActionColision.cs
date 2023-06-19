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

        if (particleSystem != null)
        {
            particleSystem.Stop();
        }
    }

    public abstract void Use();

    protected void EffectObject()
    {
        particleSystem.Play();
        Invoke("destroyObject", gameMenager.SnakeMovement.Speed);
    }

    private void destroyObject()
    {
        Destroy(gameObject);
    }
}
