using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Animal
{
    protected override void AnimalManager()
    {
        Debug.Log($"{Name} says Woof!");
    }

    public void Jump()
    {
        Debug.Log($"{Name} jumps excitedly.");
    }
}
