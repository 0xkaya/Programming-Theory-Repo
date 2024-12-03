using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Animal
{
    public override void Talk()
    {
        Console.WriteLine($"{Name} buzzes.");
    }

    public override void Move()
    {
        Console.WriteLine($"{Name} is buzzing around at {Speed} speed.");
    }

    public void Fly()
    {
        Console.WriteLine($"{Name} is flying high in the air.");
    }
}
