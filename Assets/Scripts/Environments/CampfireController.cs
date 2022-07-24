using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireController : MonoBehaviour
{
    public bool lit;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Lit", lit);
    }
}
