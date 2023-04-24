using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using LitJson;
using UnityEngine.UI;
public class DaiShenHe_DefectMenuItem : MonoBehaviour
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
                    GameObject obj = GameObject.Instantiate(MenuContrl.DaiChuLi_SHDefectMoreMenu_Prefab, transform.position, transform.rotation);
                    obj.transform.SetParent(MenuContrl.PatrolInspectionMenu2_Content.transform);
                    obj.transform.localScale = new Vector3(1, 1, 1);
                    
                    workOrderNo = jd2["workOrderNo"].ToString();
                    obj.GetComponent<DaiChuLi_SHDefectMoreMenu>().PatrolIns_Title.text = jd2["ceCustName"].ToString();
                    obj.GetComponent<DaiChuLi_SHDefectMoreMenu>().PatrolIns_GDMS.text = jd2["workOrderDesc"].ToString();
                    obj.GetComponent<DaiChuLi_SHDefectMoreMenu>().PatrolIns_GDBH.text = jd2["workOrderNo"].ToString();
                    obj.GetComponent<DaiChuLi_SHDefectMoreMenu>().PatrolIns_CLRY.text = jd2["workOrderContacter"].ToString();
                    obj.GetComponent<DaiChuLi_SHDefectMoreMenu>().PatrolIns_CJR.text = jd2["creator"].ToString();
                    obj.GetComponent<DaiChuLi_SHDefectMoreMenu>().PatrolIns_CLRQ.text = jd2["submitTime"].ToString();
                    obj.GetComponent<DaiChuLi_SHDefectMoreMenu>().PatrolIns_CJSJ.text = jd2["gmtCreate"].ToString();
                    obj.GetComponent<DaiChuLi_SHDefectMoreMenu>().PatrolIns_CLSJ.text =  jd2["submitTime"].ToString();

                    if ( obj.GetComponent<DaiChuLi_SHDefectMoreMenu>().PatrolIns_GDMS.text.Length > 10)
                    {
                        GameManager.SetTextWithEllipsis( obj.GetComponent<DaiChuLi_SHDefectMoreMenu>().PatrolIns_GDMS, obj.GetComponent<DaiChuLi_SHDefectMoreMenu>().PatrolIns_GDMS.text,10);
                        obj.GetComponent<DaiChuLi_SHDefectMoreMenu>().MoreMenuBtn.SetActive(true);
                        obj.GetComponent<DaiChuLi_SHDefectMoreMenu>().MoreMenuText.text = jd2["workOrderDesc"].ToString();
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
                                GameObject obj2 = GameObject.Instantiate(MenuContrl.DaiChuLi_SHRWDefectTaskMenu_Prefab, transform.position, transform.rotation);
                                obj2.transform.SetParent(MenuContrl.PatrolInspectionMenu2_Content.transform);
                                obj2.transform.localScale = new Vector3(1, 1, 1);
                
                                obj2.GetComponent<DaiChuLi_SHRWDefectTaskMenu>().Proce_RWBH.text = jd["data"][i]["taskNo"].ToString();
                                obj2.GetComponent<DaiChuLi_SHRWDefectTaskMenu>().Proce_QXLX.text = jd["data"][i]["taskSubtypeName"].ToString();
                                obj2.GetComponent<DaiChuLi_SHRWDefectTaskMenu>().Proce_RWMS.text = jd["data"][i]["taskDesc"].ToString();
                                obj2.GetComponent<DaiChuLi_SHRWDefectTaskMenu>().id = jd["data"][i]["id"].ToString();
                                obj2.GetComponent<DaiChuLi_SHRWDefectTaskMenu>().Proce_JJCD.text = jd["data"][i]["taskLevelName"].ToString();
                                if (((IDictionary)jd["data"][i]).Contains("ceDeviceName"))
                                {
                                    obj2.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_SBDX.text = jd["data"][i]["ceDeviceName"].ToString();
                                }
                                if (((IDictionary)jd["data"][i]).Contains("handleResult"))
                                {
                                    obj2.GetComponent<DaiChuLi_SHRWDefectTaskMenu>().Proce_CLJG.text = jd["data"][i]["handleResult"].ToString();
                                }
                                obj2.GetComponent<DaiChuLi_SHRWDefectTaskMenu>().Proce_CLSM.text = jd["data"][i]["handleDesc"].ToString();

                                if (obj2.GetComponent<DaiChuLi_SHRWDefectTaskMenu>().Proce_RWMS.text.Length > 15)
                                {
                                    GameManager.SetTextWithEllipsis(obj2.GetComponent<DaiChuLi_SHRWDefectTaskMenu>().Proce_RWMS,obj2.GetComponent<DaiChuLi_SHRWDefectTaskMenu>().Proce_RWMS.text,15);
                                    obj2.GetComponent<DaiChuLi_SHRWDefectTaskMenu>().MoreMenuBtn.SetActive(true);
                                    obj2.GetComponent<DaiChuLi_SHRWDefectTaskMenu>().MoreMenuText.text = jd["data"][i]["taskDesc"].ToString();
                                }
                                if (jd["data"][i]["handleAttach"].ToString() != "" || jd["data"][i]["handleAttach"].ToString() != "test" || jd["data"][i]["handleAttach"].ToString() != null)
                                {
                                    string[] dataStr = jd["data"][i]["handleAttach"].ToString().Split(',');

                                    for (int j = 0; j < dataStr.Length; j++)
                                    {
                                        GameObject objImage = GameObject.Instantiate(MenuContrl.WWWImage_Prefab,
                                            transform.position, transform.rotation);
                                           
                                        objImage.transform.SetParent(obj2.GetComponent<DaiChuLi_SHRWDefectTaskMenu>().Content.transform);
                                        objImage.transform.localScale = new Vector3(1, 1, 1);
                                        StartCoroutine(MenuContrl.WebRequestContrl.LoadImage(dataStr[i],objImage));
                                    }
                                }
                            }
                        }
                MenuContrl.PatrolInspectionMenu2.SetActive(true);
                //MenuContrl.DaiChuLi_ScrollView.SetActive(false);
                MenuContrl.DaiChuLi_ScrollView.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
                MenuContrl.PatrolInspectionMenu2_Content.transform.parent.gameObject.transform.parent
                    .GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                    }));
            }));
    }
}
