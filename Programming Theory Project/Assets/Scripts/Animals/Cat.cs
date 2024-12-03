using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cat : Animal
{

    protected override void AnimalManager()  // Override a virtual method from parent class
    {
        Debug.Log($"{Name} or pisi pisi says Meow!");
    }
/*
    void Update()
    {
        InputHandle();
        Move();

        if(Input.GetKeyDown(KeyCode.Space)){
            Jump(); 
            Talk();
            GetData();
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            UIMainScene.Instance.HideInfoPopup();
        }
    }*/

    public void Jump()
    {
        var uiInfo = GetComponentInParent<UIMainScene.IUIInfoContent>();
        UIMainScene.Instance.SetNewInfoContent(uiInfo);
        Debug.Log($"{Message} jumps gracefully.");
    }

    public override string GetData()
    {
        string cato =$"Speed:{Speed}\n \n {Message}";
        return cato;
    }
    
}
