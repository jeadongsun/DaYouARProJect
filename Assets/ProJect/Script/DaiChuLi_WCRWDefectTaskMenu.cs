using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaiChuLi_WCRWDefectTaskMenu : MonoBehaviour
{
   public Text Proce_RWBH;

   public Text Proce_QXLX;

   public Text Proce_JJCD;

   public Text Proce_RWMS;

   public Text Proce_CLJG;

   public Text Proce_CLSM;

   public GameObject MoreMenuBtn;

   public GameObject MoreMenu;

   public Text MoreMenuText;

   public bool isMoreMenuState;
   
   public GameObject Content;

   public Text Proce_SBDX;

   public void MoreMenBtnClick()
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
