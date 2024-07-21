using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider healthSlider;
    Animator animator;

    void Start()
    {
        healthSlider = GetComponent<Slider>();
        animator = GetComponent<Animator>();

        healthSlider.value = healthSlider.maxValue;
    }


    //Establece el valor máximo del slider
    public void ChangeHealthMax(float MaxHealth)
    {
        healthSlider.maxValue = MaxHealth;
    }

    //Cambia el valor del slider por el actual
    public void ChangeActualHealth(float ActualHealth)
    {
        healthSlider.value = ActualHealth;
        animator.SetTrigger("TakeDamage");
    }

    //El valor inicial y maximo del slider es igual a la cantidad de vida actual
    public void StartHealth(float ActualHealth)
    {
        ChangeActualHealth(ActualHealth);
        ChangeHealthMax(ActualHealth);
    }
}

