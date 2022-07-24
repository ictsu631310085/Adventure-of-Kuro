using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMP_HUD : MonoBehaviour
{
    public PlayerData playerData;

    public Image MP_1;
    public Image MP_2;
    public Image MP_3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerData.currentMP)
        {
            case 3:
                MP_1.color = Color.white;
                MP_2.color = Color.white;
                MP_3.color = Color.white;
                break;
            case 2:
                MP_1.color = Color.white;
                MP_2.color = Color.white;
                MP_3.color = Color.black;
                break;
            case 1:
                MP_1.color = Color.white;
                MP_2.color = Color.black;
                MP_3.color = Color.black;
                break;
            case 0:
                MP_1.color = Color.black;
                MP_2.color = Color.black;
                MP_3.color = Color.black;
                break;
        }
    }
}
