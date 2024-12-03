using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Animal : MonoBehaviour, UIMainScene.IUIInfoContent // Abstract class for high-level abstraction
{
    public string name; // name property
    public float speed; // speed property

    private Coroutine activeSpeedBoostCoroutine; // Store the current speed boost coroutine
    private float originalSpeed; // Store the original speed

    public string message;

    private float horizontalInput;
    private float verticalInput;

    public string Message{
        get => message;
        set{ 
        }
    }
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

    // Navigation Mesh
    protected NavMeshAgent m_Agent;

    protected void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.speed = Speed;
        m_Agent.acceleration = 999;
        m_Agent.angularSpeed = 999;      
        originalSpeed = Speed;
    }

    // Common behavior (polymorphism through virtual method)
    public virtual void GoTo(Vector3 position)
    {
        m_Agent.SetDestination(position);
        m_Agent.isStopped = false;
    }
    
    

    protected abstract void AnimalManager(); // Abstract method for animal class

    public virtual string GetName()
    {
        return Name;
    }

    public virtual string GetData()
    {
        return $"Speed: {Speed}";
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
        Debug.Log($"Speed Boost Active | Time: {Time.time}");

        speed = speed * boostMultiplier;
        m_Agent.speed = speed; // Apply Boost
        var uiInfo = GetComponentInParent<UIMainScene.IUIInfoContent>();
        UIMainScene.Instance.SetNewInfoContent(uiInfo);

        yield return new WaitForSeconds(duration);

        speed = originalSpeed;
        m_Agent.speed = speed; // Finish Boost 
        UIMainScene.Instance.SetNewInfoContent(uiInfo);

        activeSpeedBoostCoroutine  = null;
        Debug.Log($"Speed Boost Ended | Time: {Time.time}");
    }

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
