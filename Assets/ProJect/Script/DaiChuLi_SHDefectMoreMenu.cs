using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DaiChuLi_SHDefectMoreMenu : MonoBehaviour
{
    public Text PatrolIns_GDBH;

    public Text PatrolIns_Title;

    public Text PatrolIns_GDMS;

    public Text PatrolIns_CLRY;

    public Text PatrolIns_CJR;

    public Text PatrolIns_CLRQ;

    public Text PatrolIns_CJSJ;

    public Text PatrolIns_CLSJ;

    public GameObject MoreMenuBtn;

    public GameObject MoreMenu;

    public Text MoreMenuText;

    public bool isMoreMenuState = false;

    public void MoreMenuBtnClick()
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
