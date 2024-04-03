using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    [SerializeField] private Text cherriesText;
    private Animator anim;

    [SerializeField] private AudioSource collectionSoundEffect;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {   
            collectionSoundEffect.Play();
            anim = collision.gameObject.GetComponent<Animator>();
            anim.SetTrigger("collected");
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
        }
    }

}
