using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WWWImageContrl : MonoBehaviour
{
    public Image images;


    public void OepnSLTClick()
    {
        GameObject.Find("MainMenuContrl").GetComponent<MainMenuContrl>().OepnCurrentSelectImg2(images);
    }
}