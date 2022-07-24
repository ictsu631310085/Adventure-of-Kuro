using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLogController : MonoBehaviour
{
    public GameObject tutorial;
    public SpriteRenderer tutorialSprite;

    // Start is called before the first frame update
    void Start()
    {
        tutorial.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        tutorial.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        tutorial.SetActive(false);
    }
}
