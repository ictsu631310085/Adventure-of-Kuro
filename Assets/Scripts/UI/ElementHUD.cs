using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementHUD : MonoBehaviour
{
    public Image currentElementImage;
    public Image leftElementImage;
    public Image rightElementImage;

    public Sprite ignisIcon;
    public Sprite terraIcon;
    public Sprite ventusIcon;

    public void UpdateElementHUD(EnumHolder.Element playerElement)
    {
        switch (playerElement)
        {
            case EnumHolder.Element.Ignis:
                currentElementImage.sprite = ignisIcon;
                leftElementImage.sprite = ventusIcon;
                rightElementImage.sprite = terraIcon;
                break;
            case EnumHolder.Element.Terra:
                currentElementImage.sprite = terraIcon;
                leftElementImage.sprite = ignisIcon;
                rightElementImage.sprite = ventusIcon;
                break;
            case EnumHolder.Element.Ventus:
                currentElementImage.sprite = ventusIcon;
                leftElementImage.sprite = terraIcon;
                rightElementImage.sprite = ignisIcon;
                break;
        }
        currentElementImage.SetNativeSize();
    }
}
