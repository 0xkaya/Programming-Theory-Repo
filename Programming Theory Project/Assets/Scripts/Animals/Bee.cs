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

    public override string GetData()
    {
        string Bee =$"Speed:{Speed}\n \n {Message}";
        return Bee;
    }
}
