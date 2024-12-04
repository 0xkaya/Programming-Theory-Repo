using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : Food
{

    /*
    public override string GetData()
    {
        string argentina = "Argentino Style | " + NutritionAmount +" Calories";
        return argentina;
    }*/

    protected override void FoodManager(){
        Debug.Log("Pizza has been eaten!");
    }
}
