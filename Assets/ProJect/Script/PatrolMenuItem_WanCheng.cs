using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;
public class PatrolMenuItem_WanCheng : MonoBehaviour
{
    public string id;

    public MainMenuContrl MenuContrl;

    public Text PartrolNum;

    public Text PartrolNameValue;

    public Text PartrolTimeValue;
    
    public Text PartrolTimeValue_CJRQ;
    
    public void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClicks);

        MenuContrl = GameObject.Find("MainMenuContrl").GetComponent<MainMenuContrl>();
    }

    void OnClicks()
    {
        MenuContrl.currentUIIndex = 2;
        GameManager.devopsWorkOrderId = id;
        
        Dictionary<string, string> str = new Dictionary<string, string>();
        str.Add("id", id);
        
        StartCoroutine(MenuContrl.WebRequestContrl.WebRquestGetContrl("devopsWorkOrder/get", "?" + GameManager.DictionaryTostr(str),
            data =>
            {
                MenuContrl.PatrolMenu_TitleText.text = "巡视工单详情";
                Debug.Log("获取 工单基本信息接口:" + data);
                JsonData jd = JsonMapper.ToObject(data);
                JsonData jd2 = jd["data"];
                if (jd["code"].ToString() == "0")
                {
                    GameObject obj = GameObject.Instantiate(MenuContrl.Patrol_WC_Menu, transform.position, transform.rotation);
                    obj.transform.SetParent(MenuContrl.PatrolInspectionMenu_XunShi_WanCheng_Content.transform);
                    obj.transform.localScale = new Vector3(1, 1, 1);
                    
                    obj.GetComponent<Patrol_WC_Menu>().PatrolIns_Title.text = jd2["ceCustName"].ToString();
                    obj.GetComponent<Patrol_WC_Menu>().PatrolIns_GDMS.text = jd2["workOrderDesc"].ToString();
                    obj.GetComponent<Patrol_WC_Menu>().PatrolIns_GDBH.text = jd2["workOrderNo"].ToString();
                    obj.GetComponent<Patrol_WC_Menu>().PatrolIns_XSRY.text = jd2["workOrderContacter"].ToString();
                    obj.GetComponent<Patrol_WC_Menu>().PatrolIns_CJR.text = jd2["creator"].ToString();
                    obj.GetComponent<Patrol_WC_Menu>().PatrolIns_XSRQ.text = jd2["workOrderDate"].ToString();
                    obj.GetComponent<Patrol_WC_Menu>().PatrolIns_CJSJ.text = jd2["gmtCreate"].ToString();

                    if (obj.GetComponent<Patrol_WC_Menu>().PatrolIns_GDMS.text.Length > 10)
                    {
                        obj.GetComponent<Patrol_WC_Menu>().MoreBtn.SetActive(true);
                        obj.GetComponent<Patrol_WC_Menu>().MoreBtnText.text = jd2["workOrderDesc"].ToString();
                        GameManager.SetTextWithEllipsis(obj.GetComponent<Patrol_WC_Menu>().PatrolIns_GDMS,obj.GetComponent<Patrol_WC_Menu>().PatrolIns_GDMS.text,10);
                    }
                    obj.GetComponent<Patrol_WC_Menu>().PatrolIns_CLRQ.text = jd2["submitTime"].ToString();
                    obj.GetComponent<Patrol_WC_Menu>().PatrolIns_SHR.text = jd2["handler"].ToString();
                    obj.GetComponent<Patrol_WC_Menu>().PatrolIns_SHSJ.text  = jd2["handleTime"].ToString();
                }
                
                StartCoroutine(MenuContrl.WebRequestContrl.WebRquestGetContrl("devopsWorkOrder/listTask", "?" + GameManager.DictionaryTostr(str),
                    data =>
                    {
                        Debug.Log("获取 待审核 工单任务列表 接口:" + data);
                        JsonData jd = JsonMapper.ToObject(data);
                        if (jd["code"].ToString() == "0")
                        {
                            for (int i = 0; i < jd["data"].Count; i++)
                            {
                                GameObject obj2 = GameObject.Instantiate(MenuContrl.Patrol_SHRW_Item, transform.position, transform.rotation);
                                obj2.transform.SetParent(MenuContrl.PatrolInspectionMenu_XunShi_WanCheng_Content.transform);
                                obj2.transform.localScale = new Vector3(1, 1, 1);

                                obj2.GetComponent<Patrol_SHRW_ItemContrl>().Proce_RWBH.text = jd["data"][i]["taskNo"].ToString();
                                obj2.GetComponent<Patrol_SHRW_ItemContrl>().Proce_RWLX.text = jd["data"][i]["taskSubtypeName"].ToString();
                                obj2.GetComponent<Patrol_SHRW_ItemContrl>().Proce_MS.text = jd["data"][i]["taskDesc"].ToString();
                                obj2.GetComponent<Patrol_SHRW_ItemContrl>().Proce_CLSM.text = jd["data"][i]["handleDesc"].ToString();

                                if (obj2.GetComponent<Patrol_SHRW_ItemContrl>().Proce_MS.text.Length > 15)
                                {
                                    GameManager.SetTextWithEllipsis(obj2.GetComponent<Patrol_SHRW_ItemContrl>().Proce_MS,obj2.GetComponent<Patrol_SHRW_ItemContrl>().Proce_MS.text,15);
                                    obj2.GetComponent<Patrol_SHRW_ItemContrl>().MoreBtn.SetActive(true);
                                    obj2.GetComponent<Patrol_SHRW_ItemContrl>().MoreMenuText.text = jd["data"][i]["taskDesc"].ToString();
                                }
                                
                                if (jd["data"][i]["handleAttach"].ToString() != "" || jd["data"][i]["handleAttach"].ToString() != "test" || jd["data"][i]["handleAttach"].ToString() != null)
                                {
                                    string[] dataStr = jd["data"][i]["handleAttach"].ToString().Split(',');
                                    for (int j = 0; j < dataStr.Length; j++)
                                    {
                                        GameObject objImage = GameObject.Instantiate(MenuContrl.WWWImage_Prefab,
                                            transform.position, transform.rotation);
                                           
                                        objImage.transform.SetParent(obj2.GetComponent<Patrol_SHRW_ItemContrl>().Content.transform);
                                        objImage.transform.localScale = new Vector3(1, 1, 1);
                                        StartCoroutine(MenuContrl.WebRequestContrl.LoadImage(dataStr[j],objImage));
                                    }
                                }
                            }
                        } 
                        MenuContrl.CalendarMenu.transform.localScale = new Vector3(0, 0, 0);
                        MenuContrl.CalendarContrlMenu_XJ.SetActive(false);
                        MenuContrl.isCalendarContrlMenuState_XJ = false;
                        MenuContrl.PatrolInspectionMenu_XunShi_WanCheng.SetActive(true);
                        //MenuContrl.DaiChuLi_ScrollView_WanCheng.SetActive(false);
                        MenuContrl.DaiChuLi_ScrollView_WanCheng.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                        MenuContrl.PatrolInspectionMenu_XunShi_WanCheng_Content.transform.parent.gameObject.transform
                            .parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                    }));
            }));
    }
}
