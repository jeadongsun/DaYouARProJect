using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PatrolProceItemContrl : MonoBehaviour
{
    public Text PatrolIns_Title;

    public Text PatrolIns_GDBH;

    public Text PatrolIns_GDMS;

    public Text PatrolIns_XSRY;

    public Text PatrolIns_CJR;

    public Text PatrolIns_XSRQ;

    public Text PatrolIns_CJSJ;

    public GameObject MoreGDMSBtn;

    public string PatrolIns_GDMS2;

    private MainMenuContrl _mainMenuContrl;

    public bool isMoreBtn = false;

    public Text PatrolIns_SHBTG;

    public GameObject MoreMenu;

    public Text MoreMenuText;

    public GameObject PatrolIns_GoBackBtn;
    private void Start()
    {
        _mainMenuContrl = GameObject.Find("MainMenuContrl").GetComponent<MainMenuContrl>();
    }

    //工单描述 查看更多文字
    public void MoreGDMSBtnClick()
    {
        if (isMoreBtn == false)
        {
            MoreMenu.SetActive(true);
            isMoreBtn = true;
        }
        else
        {
            MoreMenu.SetActive(false);
            isMoreBtn = false;
        }
    }
    
    //打开 巡视界面 回退订单
    public void Open_PatrolInspection_BackMenu()
    {
        _mainMenuContrl.PatrolInspection_BackMenu.SetActive(true);
    }

}
