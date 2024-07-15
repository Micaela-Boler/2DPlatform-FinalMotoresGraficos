using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Collider2D collider;

    [SerializeField] float timeToDestroyGameObject;

    [Header("PLAYER")]
    [SerializeField] protected GameObject player;

    [Header("OTHERS")]
    [SerializeField] public ParticleSystem particulas;
    [SerializeField] AudioSource audio;


    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    protected abstract void ApplyPowerUp();


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ApplyPowerUp();
            
            collider.enabled = false;
            spriteRenderer.enabled = false;

            particulas.Play();
            audio.Play();

            Destroy(gameObject, timeToDestroyGameObject);
        }
    }
}
