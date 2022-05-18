using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivePoints : MonoBehaviour
{
    private int livePoints = 5;
    [SerializeField] private Text livePointsText;

    private void Start()
    {
        livePointsText.text = "Live Points: " + livePoints;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Cherry"))
        {   
            Destroy(col.gameObject);
            ++livePoints;
            livePointsText.text = "Live Points: " + livePoints;
        }
    }
}
