using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handle all the control code, so detecting when the users click on a unit or building and selecting those
/// If a unit is selected it will give the order to go to the clicked point.
/// </summary>
public class UserControl : MonoBehaviour
{

    // Camera Movement
    public Camera GameCamera;
    public float PanSpeed = 5.0f;

    // Selection Control
    private Animal m_Selected = null;
    private Food f_Selected = null;

    // Marker Properties 
    public GameObject animalMarker; // Prefab for the animalMarker
    public GameObject foodMarker;
    public GameObject destinationMarker;


    // Audio clips for shared actions
    public AudioClip selectSound;
    public AudioClip foodEatenSound;
    private AudioSource audioSource;


    public InfoPopup InfoPopup;

    private void Start()
    {
        animalMarker.SetActive(false);
        destinationMarker.SetActive(false);
        foodMarker.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        GameCamera.transform.position = GameCamera.transform.position + new Vector3(move.y, 0, -move.x) * PanSpeed * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
        else if (m_Selected != null && Input.GetMouseButtonDown(1))
        {//right click give order to the unit
            HandleAction();
        }
        AnimalMarkerHandling();
        FoodMarkerHandling();
    }
    
    public void HandleSelection()
    {
            var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //the collider could be children of the unit, so we make sure to check in the parent
                var animal = hit.collider.GetComponentInParent<Animal>();
                var food = hit.collider.GetComponentInParent<Food>();

                if (animal != null)
                {
                    m_Selected = animal;
                    f_Selected = null;
                    PlaySound(selectSound); // Play selection sound
                    Debug.Log("Animal Selected");
                }
                else if (food != null)
                {
                    f_Selected = food;
                    m_Selected = null; // Deselect the current animal
                    PlaySound(selectSound); // Same sound for food selection
                    Debug.Log("Food Selected");

                }else{
                    m_Selected = null; // Clear selection if neither animal nor food
                    f_Selected = null;
                }

                
                // If no animal is selected, deactivate the destination marker
                if (m_Selected == null)
                {
                    animalMarker.SetActive(false);
                    destinationMarker.SetActive(false);
                }

                //check if the hit object have a IUIInfoContent to display in the UI
                //if there is none, this will be null, so this will hid the panel if it was displayed
                var uiInfo = hit.collider.GetComponentInParent<UIMainScene.IUIInfoContent>();
                UIMainScene.Instance.SetNewInfoContent(uiInfo);    
                UIMainScene.Instance.ShowInfoPopup(); // Show the popup
            }
    }

    public void HandleAction(){
            var ray = GameCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("Hit");
            if (Physics.Raycast(ray, out hit))
            {
                m_Selected.GoTo(hit.point);
                // Activate and position the destination marker
                destinationMarker.SetActive(true);
                destinationMarker.transform.position = new Vector3(hit.point.x,0.5f,hit.point.z);

            }
    }
    
    // Handle displaying the animalMarker above the unit that is currently selected (or hiding it if no unit is selected)
    public void AnimalMarkerHandling()
    {
        if (m_Selected == null && animalMarker.activeInHierarchy)
        {
            animalMarker.SetActive(false);
            animalMarker.transform.SetParent(null);
            Debug.Log("Object Marker Disaper");
        }
        else if (m_Selected != null && animalMarker.transform.parent != m_Selected.transform)
        {
            animalMarker.SetActive(true);
            animalMarker.transform.SetParent(m_Selected.transform, false);
            animalMarker.transform.localPosition = Vector3.zero;
        }    
    }

    public void FoodMarkerHandling()
    {
        if (f_Selected == null && foodMarker.activeInHierarchy)
        {
            foodMarker.SetActive(false);
            foodMarker.transform.SetParent(null);
            Debug.Log("Object Marker Disaper");
        }
        else if (f_Selected != null && f_Selected.transform.parent != f_Selected.transform)
        {
            foodMarker.SetActive(true);
            foodMarker.transform.SetParent(f_Selected.transform, false);
            foodMarker.transform.localPosition = Vector3.zero;
        }    
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void NotifyFoodEaten()
    {
        PlaySound(foodEatenSound); // Play food eaten sound
    }

}
