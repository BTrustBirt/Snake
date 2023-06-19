using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class that needs to be implemented by any object that interacts with the snake's head.
/// These objects should be created as prefabs and placed in the Grid spawnObjeck
/// </summary>
public abstract class ActionColision : MonoBehaviour
{

    protected GameMenager gameMenager;

    public ParticleSystem particleSystem;

    // Get reference to the GameMenager and add the object to the grid
    public void GetRef(GameMenager gameRef)
    {
        gameMenager = gameRef;
        gameMenager.Grid.AddToGrid(new Vector2Int((int)transform.position.x, (int)transform.position.y), this.gameObject);

        // Stop the particle system if it exists
        if (particleSystem != null)
        {
            particleSystem.Stop();
        }
    }

    // Abstract method to define the action upon collision
    public abstract void Use();

    // Play the particle system effect and destroy the object after a delay
    protected void EffectObject()
    {
        particleSystem.Play();
        Invoke("destroyObject", gameMenager.SnakeMovement.Speed);
    }

    // Destroy the game object
    private void destroyObject()
    {
        Destroy(gameObject);
    }
}
