using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MonthItemClickContrl : MonoBehaviour
{
    public MainMenuContrl MenuContrl;

    public string Month;

    public int MonthIndex;
    
    public void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClicks);

        MenuContrl = GameObject.Find("MainMenuContrl").GetComponent<MainMenuContrl>();
    }

    void OnClicks()
    {
        foreach (var item in MenuContrl.MonthItem)
        {
            item.GetComponent<Image>().DOFade(0, 0);
        }
        MenuContrl.MonthItem[MonthIndex].GetComponent<Image>().DOFade(1, 0);
        
        foreach (var item in MenuContrl.MonthItem_Text)
        {
            item.GetComponent<Text>().DOColor(new Color(102f / 255f, 102f / 255f, 102f / 255f, 255f / 255f), 0);
        }
        MenuContrl.MonthItem_Text[MonthIndex].GetComponent<Text>().DOColor(new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f), 0);
        
        MenuContrl.CalendarContrlMenu_XJ.SetActive(false);
        MenuContrl.isCalendarContrlMenuState_XJ = false;
        
        string date = MenuContrl.currentYear.ToString() + "-" + Month;
        Debug.Log("日历当前时间:" + date);
        MenuContrl.TitleTime_XJ.text = date;
        MenuContrl.MainMenuBtnItemContrl(2,"9",date);
    }
}
