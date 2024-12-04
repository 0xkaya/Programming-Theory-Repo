using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : Food
{
    /*
    public override string GetData()
    {
        return $"Nutrition: {NutritionAmount}, \n \n Boost: {boostAmount}x for {boostDuration}s";
    }*/
    protected override void FoodManager()
    {
        Debug.Log("Pizza has been eaten!");
    }

    

}
