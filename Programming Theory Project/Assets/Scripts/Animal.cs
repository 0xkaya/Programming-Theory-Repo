using System;
using UnityEngine;

public abstract class Animal : MonoBehaviour // Abstract class for high-level abstraction
{
    private string name; // Encapsulated name property
    private float speed; // Encapsulated speed property

    // Encapsulated properties with validations
    public string Name
    {
        get => name;
        set
        {
            if (value.Length > 20)
                throw new ArgumentException("name is too long!");
            name = value;
        }
    }

    public float Speed
    {
        get => speed;
        set
        {
            if (value < 0)
                throw new ArgumentException("Speed cannot be negative!");
            speed = value;
        }
    }

    // Common behavior (polymorphism through virtual method)
    public virtual void Move()
    {
        Console.WriteLine($"{name} is moving at {Speed} speed.");
    }

    public abstract void Talk(); // Abstract method for specific animal sounds
}
