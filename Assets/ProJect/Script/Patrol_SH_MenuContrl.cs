using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class Patrol_SH_MenuContrl : MonoBehaviour
{
    public Text PatrolIns_GDBH;

    public Text PatrolIns_Title;

    public Text PatrolIns_XSRY;

    public Text PatrolIns_XSRQ;

    public Text PatrolIns_GDMS;

    public Text PatrolIns_CLRQ;

    public Text PatrolIns_CJR;

    public Text PatrolIns_CJSJ;

    public GameObject MoreBtn;
    
    public bool isMoreState = false;

    public GameObject MoreMenu;

    public Text MoreMenuText;

    public void Open_MoreBtnClick()
    {
        if (isMoreState == false)
        {
            MoreMenu.SetActive(true);
            isMoreState = true;
        }
        else
        {
            MoreMenu.SetActive(false);
            isMoreState = false;
        }
    }
}
