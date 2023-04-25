using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;
    [SerializeField] private Text CherriesText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpeedBoost"))
        {
            Destroy(collision.gameObject);
            cherries++;
            Debug.Log("PowerUps :" + cherries);
            CherriesText.text = " PowerUps : " + cherries;
        }
        if (collision.gameObject.CompareTag("JumpBoost"))
        {
            Destroy(collision.gameObject);
            cherries++;
            Debug.Log("PowerUps :" + cherries);
            CherriesText.text = " PowerUps : " + cherries;
        }
        if (collision.gameObject.CompareTag("DamageBoost"))
        {
            Destroy(collision.gameObject);
            cherries++;
            Debug.Log("PowerUps :" + cherries);
            CherriesText.text = " PowerUps : " + cherries;
        }
    }
}
 