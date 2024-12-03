using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Food : MonoBehaviour,
UIMainScene.IUIInfoContent
{
    public string name; // name property
    public float nutritionAmount;

    public float boostAmount; // Speed boost amount
    public float boostDuration; // Speed boost duration in seconds

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
        set{}
    }

    protected abstract void FoodManager(); // Abstract method for specific animal sounds
  

    public virtual string GetName()
    {
        return Name;
    }

    public virtual string GetData()
    {
        return $"Nutrition: {NutritionAmount}, Boost: {boostAmount}x for {boostDuration}s";
    }

    // Handle collision with animals    // Handle collision with animals
    private void OnTriggerEnter(Collider other)
    {
        var animal = other.GetComponentInParent<Animal>();
        if (animal != null)
        {
            animal.StartSpeedBoost(boostAmount, boostDuration); // Trigger the boost
            Debug.Log($"Boost Applied: {boostAmount}x for {boostDuration}s to {animal.Name}");
            Destroy(gameObject); // Destroy food after consumption
        }
    }

}
