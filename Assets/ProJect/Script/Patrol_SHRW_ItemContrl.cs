using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using UnityEngine.Networking;

public class Patrol_SHRW_ItemContrl : MonoBehaviour
{
    public Text Proce_RWBH;

    public Text Proce_RWLX;

    public Text Proce_MS;

    public Text Proce_CLSM;

    public GameObject MoreMenu;

    public Text MoreMenuText;

    public bool isMoreMenuState = false;

    public GameObject MoreBtn;

    public GameObject Content;

    public void MoreBtnClick()
    {
        if (isMoreMenuState == false)
        {
            isMoreMenuState = true;
            MoreMenu.SetActive(true);
        }
        else
        {
            isMoreMenuState = false;
            MoreMenu.SetActive(false);
        }
    }
}
