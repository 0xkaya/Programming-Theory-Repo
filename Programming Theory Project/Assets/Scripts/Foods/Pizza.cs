using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : Food
{
    public override string GetData()
    {
        string pizzarita = "Italiano | " + NutritionAmount +" Calories";
        return pizzarita;
    }

    protected override void FoodManager(){
        Debug.Log("Pizza has been eaten!");
    }
}
