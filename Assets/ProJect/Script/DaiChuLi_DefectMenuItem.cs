using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using LitJson;
using UnityEngine.UI;
public class DaiChuLi_DefectMenuItem : MonoBehaviour
{
   public string id;

    public MainMenuContrl MenuContrl;

    public Text PartrolNum;

    public Text PartrolNameValue;

    public Text PartrolTimeValue;

    public string workOrderNo;
    public void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClicks);

        MenuContrl = GameObject.Find("MainMenuContrl").GetComponent<MainMenuContrl>();
    }

    void OnClicks()
    {
        MenuContrl.currentUIIndex = 1;
        GameManager.devopsWorkOrderId = id;
        MenuContrl.Open_PatrolInspectionMenu();
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
                    GameObject obj = GameObject.Instantiate(MenuContrl.DaiChuLi_DefectMoreMenu_Prefab, transform.position, transform.rotation);
                    obj.transform.SetParent(MenuContrl.PatrolProceMenu_Content.transform);
                    obj.transform.localScale = new Vector3(1, 1, 1);
                    
                    obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_Title.text = jd2["ceCustName"].ToString();
                    obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_GDMS.text = jd2["workOrderDesc"].ToString();
                    obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_GDBH.text = jd2["workOrderNo"].ToString();
                    workOrderNo = jd2["workOrderNo"].ToString();
                    obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_CLRY.text = jd2["workOrderContacter"].ToString();
                    obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_CJR.text = jd2["creator"].ToString();

                    if (((IDictionary)jd2).Contains("submitTime"))
                    {
                        obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_CLRQ.text = jd2["submitTime"].ToString();
                    }
                    else
                    {
                        obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_CLRQ.text = "后台缺少数据";
                    }

                    obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_CJSJ.text = jd2["gmtCreate"].ToString();
                    obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_GDMS2 = jd2["workOrderDesc"].ToString();

                    if (obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_GDMS.text.Length > 10)
                    {
                        GameManager.SetTextWithEllipsis(obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_GDMS,obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_GDMS.text,10);
                        obj.GetComponent<DaiChuLi_DefectMoreMenu>().MoreGDMSBtn.SetActive(true);
                        obj.GetComponent<DaiChuLi_DefectMoreMenu>().MoreMenuText.text = jd2["workOrderDesc"].ToString();
                    }
                    
                    if (((IDictionary)jd2).Contains("handleDesc"))
                    {
                        if (jd2["handleDesc"] != null)
                        {
                            obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_GoBackBtn.SetActive(false);
                            obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_SHBTG.gameObject.SetActive(true);
                            obj.GetComponent<DaiChuLi_DefectMoreMenu>().PatrolIns_SHBTG.text =
                                jd2["handleDesc"].ToString();
                        }
                    }
                }
                Dictionary<string, string> str2= new Dictionary<string, string>();
                str2.Add("workNo", workOrderNo);
                StartCoroutine(MenuContrl.WebRequestContrl.WebRquestGetContrl("devopsWorkOrder/listTaskByWorkNo", "?" + GameManager.DictionaryTostr(str2),
                    data =>
                    {
                        Debug.Log("获取 工单基本信息下面的任务列表 接口:" + data);
                        JsonData jd = JsonMapper.ToObject(data);
                        if (jd["code"].ToString() == "0")
                        {
                            for (int i = 0; i < jd["data"].Count; i++)
                            {
                                GameObject obj2 = GameObject.Instantiate(MenuContrl.DaiChuLi_DefectTaskMenu_Prefab, transform.position, transform.rotation);
                                obj2.transform.SetParent(MenuContrl.PatrolProceMenu_Content.transform);
                                obj2.transform.localScale = new Vector3(1, 1, 1);

                                obj2.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_RWBH.text = jd["data"][i]["taskNo"].ToString();
                                obj2.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_QXLX.text = jd["data"][i]["taskSubtypeName"].ToString();
                                obj2.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_RWMS.text = jd["data"][i]["taskDesc"].ToString();
                                if (((IDictionary)jd["data"][i]).Contains("ceDeviceName"))
                                {
                                    obj2.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_SBDX.text = jd["data"][i]["ceDeviceName"].ToString();
                                }
                                if (obj2.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_RWMS.text.Length > 15)
                                {
                                    GameManager.SetTextWithEllipsis(obj2.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_RWMS,obj2.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_RWMS.text,15);
                                    obj2.GetComponent<DaiChuLi_DefectTaskMenu>().MoreBtn.SetActive(true);
                                    obj2.GetComponent<DaiChuLi_DefectTaskMenu>().MoreMenuText.text = jd["data"][i]["taskDesc"].ToString();
                                }
                                obj2.GetComponent<DaiChuLi_DefectTaskMenu>().id = jd["data"][i]["id"].ToString();
                                if (((IDictionary)jd["data"][i]).Contains("handleDesc"))
                                {
                                    if (jd["data"][i]["handleDesc"].ToString() != "")
                                    {
                                        obj2.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_Input.text = jd["data"][i]["handleDesc"].ToString();
                                    }
                                }
                             
                                switch (jd["data"][i]["taskLevelName"].ToString())
                                {
                                    case "紧急":
                                        obj2.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_JJState[1].SetActive(true);
                                        break;
                                    case "重要":
                                        obj2.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_JJState[0].SetActive(true);
                                        break;
                                    case "一般":
                                        obj2.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_JJState[2].SetActive(true);
                                        break;
                                }

                                if (((IDictionary)jd["data"][i]).Contains("handleResult"))
                                {
                                    switch (jd["data"][i]["handleResult"].ToString())
                                    {
                                        case "未完成":
                                            obj2.GetComponent<DaiChuLi_DefectTaskMenu>().WeiWanChengBtnClick();
                                            break;
                                        case "完成":
                                            obj2.GetComponent<DaiChuLi_DefectTaskMenu>().WanChengBtnClick();
                                            break;
                                    }
                                }
                            }
                        }
                        MenuContrl.DaiChuLi_ScrollView.SetActive(false);
                        MenuContrl.PatrolInspectionMenu.SetActive(true);
                        MenuContrl.PatrolProceMenu_Content.transform.parent.gameObject.transform.parent
                            .GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                    }));
            }));
    }
}
