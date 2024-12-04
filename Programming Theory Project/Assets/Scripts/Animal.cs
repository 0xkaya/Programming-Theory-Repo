using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Animal : MonoBehaviour, UIMainScene.IUIInfoContent // Abstract class for high-level abstraction
{

    protected abstract void AnimalManager(); // Abstract method for animal class

    [SerializeField]
    private string name; // name property
    public float speed; // speed property
    public string message; // message property


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

    public float Speed
    {
        get => speed;
        set
        {
            if (value < 0)
                throw new ArgumentException("Speed cannot be negative!");
            speed = value;
        }
    }

    public string Message
    {
        get => message;
        set{ 
        }
    }

    // Boost Properties
    private Coroutine activeSpeedBoostCoroutine; // Store the current speed boost coroutine
    private float originalSpeed; // Store the original speed

    // Eaten Food Details
    private List<string> consumedFoods = new List<string>(); // List of consumed foods
    private float totalNutrition; // Track total nutrition consumed

    // Navigation Mesh
    protected NavMeshAgent m_Agent;
    // Animotor
    private Animator animator; // Reference to the child object's Animator
    private ParticleSystem dirtParticle;
    public bool isMoving; // To track the movement state

    protected void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = Speed;
        m_Agent.acceleration = 999;
        m_Agent.angularSpeed = 999;      
        originalSpeed = Speed;

        // Get the Animator and Particle component from the child objects
        animator = GetComponentInChildren<Animator>();
        dirtParticle = GetComponentInChildren<ParticleSystem>();
    }

    // Common behavior (polymorphism through virtual method)
    // Command animal to move to a position
    public virtual void GoTo(Vector3 position)
    {
        m_Agent.SetDestination(position);
        m_Agent.isStopped = false;

    // Handle movement start
        MonitorMovement();
    }

    // Function to monitor movement changes
    private void MonitorMovement()
    {
        if (m_Agent.remainingDistance > 0.1f && !m_Agent.isStopped) // If agent is moving
        {
            if (!isMoving) // If movement just started
            {
                isMoving = true;
                OnMovementStarted();
            }
        }
        else // If the agent has stopped
        {
            if (isMoving) // If movement just stopped
            {
                isMoving = false;
                OnMovementStopped();
            }
        }
    }

     // Called when the animal starts moving
    private void OnMovementStarted()
    {
        if (animator != null)
        {
            animator.SetFloat("Speed_f", 0.75f); // Set animator parameter
        }

        if (dirtParticle != null && !dirtParticle.isPlaying)
        {
            dirtParticle.Play(); // Start particle effect
        }
    }

        // Called when the animal stops moving
    private void OnMovementStopped()
    {
        if (animator != null)
        {
            animator.SetFloat("Speed_f", 0.0f); // Reset animator parameter
        }

        if (dirtParticle != null && dirtParticle.isPlaying)
        {
            dirtParticle.Stop(); // Stop particle effect
        }
    }

    // Check movement status when NavMeshAgent stops or changes destination
    private void LateUpdate()
    {
        MonitorMovement();
    }


    public virtual string GetName()
    {
        return Name;
    }

    public virtual string GetData()
    {
        string foodList = string.Join(", ", consumedFoods);
        foodList = string.IsNullOrEmpty(foodList) ? "None" : foodList; // Handle empty list
        return $"{Message} \n Speed : {Speed}\n \n Foods Eaten: {foodList} \n \n Nutrition Amount: {totalNutrition:F1}";
    }

    public void AddConsumedFood(string foodName,float nutrition)
    {
        consumedFoods.Add(foodName); // Add food name to the list
        totalNutrition += nutrition; // Update total nutrition
        UpdateUI();
    }
    public void StartSpeedBoost(float boostMultiplier, float duration)
    {
    // Stop the active coroutine if one is running
        if (activeSpeedBoostCoroutine != null)
        {
            StopCoroutine(activeSpeedBoostCoroutine);
        }       
        activeSpeedBoostCoroutine = StartCoroutine(SpeedBoostCoroutine(boostMultiplier, duration));
    }

    private IEnumerator SpeedBoostCoroutine(float boostMultiplier, float duration)
    {
        //Debug.Log($"Speed Boost Active | Time: {Time.time}");
        speed = speed * boostMultiplier;
        m_Agent.speed = speed; // Apply Boost
        UpdateUI();
        yield return new WaitForSeconds(duration);
        speed = originalSpeed;
        m_Agent.speed = speed; // Finish Boost 
        //UpdateUI();
        activeSpeedBoostCoroutine  = null;
        //Debug.Log($"Speed Boost Ended | Time: {Time.time}");
    }

    public void UpdateUI(){
        var uiInfo = GetComponentInParent<UIMainScene.IUIInfoContent>();
        UIMainScene.Instance.SetNewInfoContent(uiInfo);
    }


    /*
    private IEnumerator MonitorMovement()
    {
        while (m_Agent.remainingDistance > m_Agent.stoppingDistance)
        {
            
            yield return null; // Wait until the next frame
        }

        // Stop movement and reset Speed_f when the destination is reached
        m_Agent.isStopped = true;
        if (animator != null)
        {
            animator.SetFloat("Speed_f", 0f);
        }

        // Optionally, stop the particle system if no movement
        if (dirtParticle != null && dirtParticle.isPlaying)
        {
            dirtParticle.Stop(); // Stop the particle system
        }
    }*/
    /*
    public virtual void Move()
    {
        transform.Translate(Vector3.forward*Time.deltaTime*verticalInput*speed);
        transform.Translate(Vector3.right*Time.deltaTime*horizontalInput*speed);
    }

    public virtual void InputHandle(){
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }*/
}
