using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class DaiChuLi_DefectMoreMenu : MonoBehaviour
{
    public Text PatrolIns_Title;

    public Text PatrolIns_GDBH;

    public Text PatrolIns_GDMS;

    public Text PatrolIns_CLRY;

    public Text PatrolIns_CJR;

    public Text PatrolIns_CLRQ;

    public Text PatrolIns_CJSJ;

    public GameObject MoreGDMSBtn;

    public string PatrolIns_GDMS2;

    private MainMenuContrl _mainMenuContrl;

    public bool isMoreBtn = false;

    public Text PatrolIns_SHBTG;

    public string id = "";

    public GameObject MoreMenu;

    public Text MoreMenuText;

    public GameObject PatrolIns_GoBackBtn;
    private void Start()
    {
        _mainMenuContrl = GameObject.Find("MainMenuContrl").GetComponent<MainMenuContrl>();
    }
    
    public void Open_PatrolInspection_BackMenu()
    {
        _mainMenuContrl.PatrolInspection_BackMenu.SetActive(true);
    }

    public void MoreMenuBtnClick()
    {
        if (isMoreBtn == false)
        {
            isMoreBtn = true;
            MoreMenu.SetActive(true);
        }
        else
        {
            isMoreBtn = false;
            MoreMenu.SetActive(false);
        }
    }
}
