using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Animal
{
    protected override void AnimalManager()
    {
        Debug.Log($"{Name} buzzes.");
    }

    public void Fly()
    {
        Debug.Log($"{Name} is flying high in the air.");
    }

}
