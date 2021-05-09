using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject prompt;
    bool hasCollided = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            hasCollided = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //hasCollided = false;
    }

    public void OnGUI()
    {
        if (hasCollided)
        {
            prompt.GetComponentInChildren<TextMeshProUGUI>().text = "Press Enter to Fight Boss";
            prompt.SetActive(true); 
        }
        else { prompt.SetActive(false); }
    }

}
