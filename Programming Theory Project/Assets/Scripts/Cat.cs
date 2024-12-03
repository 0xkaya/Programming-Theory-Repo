using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : Animal
{
    public override void Talk()
    {
        Console.WriteLine($"{Name} says Meow!");
    }

    public void Jump()
    {
        Console.WriteLine($"{Name} jumps gracefully.");
    }
}
