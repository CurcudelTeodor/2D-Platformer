using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;

    [SerializeField] private Text cherriesText;
    private Animator anim;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {   
            anim = collision.gameObject.GetComponent<Animator>();
            anim.SetTrigger("collected");
            cherries++;
            cherriesText.text = "Cherries: " + cherries;
        }
    }

}
