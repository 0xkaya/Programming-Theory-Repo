using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Food : MonoBehaviour,
UIMainScene.IUIInfoContent
{
    protected abstract void FoodManager(); // Abstract method for specific animal sounds
    public string name; // name property
    public float nutritionAmount;

        // Encapsulated properties with validations
    public string Name
    {
        get => name;
        set
        {
            if (value.Length > 20)
                throw new ArgumentException("name is too long!");
            name = value;
        }
    }
    public float NutritionAmount
    {
        get => nutritionAmount;
        private set{}
    }

    // Boost Properties
    public float boostAmount; // Speed boost amount
    public float boostDuration; // Speed boost duration in seconds



    public virtual string GetName()
    {
        return Name;
    }

    public virtual string GetData()
    {
        return $"Nutrition: {NutritionAmount}, \n \n Boost: {boostAmount}x for {boostDuration}s";
    }

    // Handle collision with animals 
    private void OnTriggerEnter(Collider other)
    {
        var animal = other.GetComponentInParent<Animal>();
        if (animal != null)
        {
            animal.StartSpeedBoost(boostAmount, boostDuration); // Trigger the boost
            animal.AddConsumedFood(Name,NutritionAmount); // Add this food to the animal's list
            Debug.Log($"Boost Applied: {boostAmount}x for {boostDuration}s to {animal.Name}");

            // Notify the UserControl to play the "food eaten" sound
            UserControl userControl = FindObjectOfType<UserControl>();
            if (userControl != null)
            {
                userControl.NotifyFoodEaten();
            }
            Destroy(gameObject); // Destroy food after consumption
        }
    }

}
