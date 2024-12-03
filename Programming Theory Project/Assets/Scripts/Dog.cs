using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : Animal
{
    public override void Talk()
    {
        Console.WriteLine($"{Name} says Woof!");
    }

    public void Jump()
    {
        Console.WriteLine($"{Name} jumps excitedly.");
    }
}
