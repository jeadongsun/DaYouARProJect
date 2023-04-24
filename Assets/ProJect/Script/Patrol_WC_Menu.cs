using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Patrol_WC_Menu : MonoBehaviour
{
    public Text PatrolIns_GDBH;

    public Text PatrolIns_Title;

    public Text PatrolIns_XSRY;

    public Text PatrolIns_XSRQ;

    public Text PatrolIns_GDMS;

    public Text PatrolIns_CLRQ;

    public Text PatrolIns_CJR;

    public Text PatrolIns_CJSJ;

    public Text PatrolIns_SHR;

    public Text PatrolIns_SHSJ;

    public GameObject MoreBtn;

    public GameObject MoreMenu;

    public Text MoreBtnText;

    public bool isMoreMenu = false;

    public void MoreMenuClick()
    {
        if (isMoreMenu == false)
        {
            isMoreMenu = true;
            MoreMenu.SetActive(true);
        }
        else
        {
            isMoreMenu = false;
            MoreMenu.SetActive(false);
        }
    }
}
