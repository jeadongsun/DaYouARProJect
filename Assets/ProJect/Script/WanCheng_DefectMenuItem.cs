using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;
public class WanCheng_DefectMenuItem : MonoBehaviour
{
  public string id;

    public MainMenuContrl MenuContrl;

    public Text PartrolNum;

    public Text PartrolNameValue;

    public Text PartrolTimeValue;
    
    public Text PartrolTimeValue_CJRQ;
    
    public string workOrderNo;
    
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
                MenuContrl.PatrolMenu_TitleText.text = "缺陷工单详情";
                Debug.Log("获取 工单基本信息接口:" + data);
                JsonData jd = JsonMapper.ToObject(data);
                JsonData jd2 = jd["data"];
                if (jd["code"].ToString() == "0")
                {
                    GameObject obj = GameObject.Instantiate(MenuContrl.DaiChuLi_WCDefectMoreMenu_Prefab, transform.position, transform.rotation);
                    obj.transform.SetParent(MenuContrl.PatrolInspectionMenu2_Content.transform);
                    obj.transform.localScale = new Vector3(1, 1, 1);
                    
                    obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_Title.text = jd2["ceCustName"].ToString();
                    obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_GDMS.text = jd2["workOrderDesc"].ToString();
                    obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_GDBH.text = jd2["workOrderNo"].ToString();
                   
                    if (((IDictionary)jd2).Contains("submitTime"))
                    {
                        obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_CLSJ.text = jd2["submitTime"].ToString();
                    }
                    else
                    {
                        obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_CLSJ.text = "后台缺少数据";
                    }

                    obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_CJR.text = jd2["creator"].ToString();
                    obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_CLRY.text = jd2["workOrderContacter"].ToString();
                    obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_CJSJ.text = jd2["gmtCreate"].ToString();
                    obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_CLRQ.text = jd2["workOrderDate"].ToString();
                    obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_SHR.text = jd2["handler"].ToString();
                    obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_SHSJ.text = jd2["handleTime"].ToString();
                    workOrderNo = jd2["workOrderNo"].ToString();
                    if (obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_GDMS.text.Length > 10)
                    {
                        GameManager.SetTextWithEllipsis( obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_GDMS, obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().PatrolIns_GDMS.text,10);
                        obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().MoreMenuBtn.SetActive(true);
                        obj.GetComponent<DaiChuLi_WCDefectMoreMenu>().MoreMenuText.text = jd2["workOrderDesc"].ToString();
                    }
                }
                
                Dictionary<string, string> str2= new Dictionary<string, string>();
                str2.Add("workNo", workOrderNo);
                StartCoroutine(MenuContrl.WebRequestContrl.WebRquestGetContrl("devopsWorkOrder/listTaskByWorkNo", "?" + GameManager.DictionaryTostr(str2),
                    data =>
                    {
                        Debug.Log("获取 待审核 工单任务列表 接口:" + data);
                        JsonData jd = JsonMapper.ToObject(data);
                        if (jd["code"].ToString() == "0")
                        {
                            for (int i = 0; i < jd["data"].Count; i++)
                            {
                                GameObject obj2 = GameObject.Instantiate(MenuContrl.DaiChuLi_WCRWDefectTaskMenu_Prefab, transform.position, transform.rotation);
                                obj2.transform.SetParent(MenuContrl.PatrolInspectionMenu2_Content.transform);
                                obj2.transform.localScale = new Vector3(1, 1, 1);
                
                                obj2.GetComponent<DaiChuLi_WCRWDefectTaskMenu>().Proce_RWBH.text = jd["data"][i]["taskNo"].ToString();
                                obj2.GetComponent<DaiChuLi_WCRWDefectTaskMenu>().Proce_QXLX.text = jd["data"][i]["taskSubtypeName"].ToString();
                                obj2.GetComponent<DaiChuLi_WCRWDefectTaskMenu>().Proce_JJCD.text = jd["data"][i]["taskLevelName"].ToString();
                                obj2.GetComponent<DaiChuLi_WCRWDefectTaskMenu>().Proce_CLSM.text = jd["data"][i]["handleDesc"].ToString();
                                obj2.GetComponent<DaiChuLi_WCRWDefectTaskMenu>().Proce_RWMS.text = jd["data"][i]["taskDesc"].ToString();
                                obj2.GetComponent<DaiChuLi_WCRWDefectTaskMenu>().Proce_CLJG.text = jd["data"][i]["handleResult"].ToString();

                                if (((IDictionary)jd["data"][i]).Contains("ceDeviceName"))
                                {
                                    obj2.GetComponent<DaiChuLi_WCRWDefectTaskMenu>().Proce_SBDX.text = jd["data"][i]["ceDeviceName"].ToString();
                                }
                                
                                if (obj2.GetComponent<DaiChuLi_WCRWDefectTaskMenu>().Proce_RWMS.text.Length > 15)
                                {
                                    obj2.GetComponent<DaiChuLi_WCRWDefectTaskMenu>().MoreMenuBtn.SetActive(true);
                                    obj2.GetComponent<DaiChuLi_WCRWDefectTaskMenu>().MoreMenuText.text =  jd["data"][i]["taskDesc"].ToString();
                                    GameManager.SetTextWithEllipsis( obj2.GetComponent<DaiChuLi_WCRWDefectTaskMenu>().Proce_RWMS, obj2.GetComponent<DaiChuLi_WCRWDefectTaskMenu>().Proce_RWMS.text,15);
                                }
                                
                                if (jd["data"][i]["handleAttach"].ToString() != "" || jd["data"][i]["handleAttach"].ToString() != "test" || jd["data"][i]["handleAttach"].ToString() != null)
                                {
                                    string[] dataStr = jd["data"][i]["handleAttach"].ToString().Split(',');

                                    for (int j = 0; j < dataStr.Length; j++)
                                    {
                                        GameObject objImage = GameObject.Instantiate(MenuContrl.WWWImage_Prefab,
                                            transform.position, transform.rotation);
                                           
                                        objImage.transform.SetParent(obj2.GetComponent<DaiChuLi_WCRWDefectTaskMenu>().Content.transform);
                                        objImage.transform.localScale = new Vector3(1, 1, 1);
                                        StartCoroutine(MenuContrl.WebRequestContrl.LoadImage(dataStr[i],objImage));
                                    }
                                }
                            }
                        }

                        MenuContrl.CalendarMenu.transform.localScale = new Vector3(0, 0, 0);
                        MenuContrl.CalendarContrlMenu_XJ.SetActive(false);
                        MenuContrl.isCalendarContrlMenuState_XJ = false;
                MenuContrl.PatrolInspectionMenu2.SetActive(true);
                //MenuContrl.DaiChuLi_ScrollView_WanCheng.SetActive(false);
                MenuContrl.DaiChuLi_ScrollView_WanCheng.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                MenuContrl.PatrolInspectionMenu2_Content.transform.parent.gameObject.transform.parent
                    .GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                    }));
            }));
    }
} 
