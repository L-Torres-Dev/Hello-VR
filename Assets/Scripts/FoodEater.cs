using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FoodEater : MonoBehaviour
{

    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socketInteractor;

    public void EatFood()
    {
        var currentFood = socketInteractor.interactablesHovered[0];
        Destroy(currentFood.transform.gameObject);
    }
}
