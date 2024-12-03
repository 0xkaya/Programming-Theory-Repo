using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : Food
{
    public override string GetData()
    {
        string drinkName = ($"Nutrition Amount | \n {NutritionAmount} :/");
        return drinkName;
    }
    protected override void FoodManager()
    {
        Debug.Log("Pizza has been eaten!");
    }

    

}
