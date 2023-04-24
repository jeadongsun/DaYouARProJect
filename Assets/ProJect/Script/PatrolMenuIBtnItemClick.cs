using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;

public class PatrolMenuIBtnItemClick : MonoBehaviour
{

    public string id;

    public MainMenuContrl MenuContrl;

    public Text PartrolNum;

    public Text PartrolNameValue;

    public Text PartrolTimeValue;

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
                MenuContrl.PatrolMenu_TitleText.text = "巡视工单详情";
                Debug.Log("获取 工单基本信息接口:" + data);
                JsonData jd = JsonMapper.ToObject(data);
                JsonData jd2 = jd["data"];
                if (jd["code"].ToString() == "0")
                {
                    GameObject obj = GameObject.Instantiate(MenuContrl.PatrolMessageMenu, transform.position, transform.rotation);
                    obj.transform.SetParent(MenuContrl.PatrolProceMenu_Content.transform);
                    obj.transform.localScale = new Vector3(1, 1, 1);
                    
                    obj.GetComponent<PatrolProceItemContrl>().PatrolIns_Title.text = jd2["ceCustName"].ToString();
                    obj.GetComponent<PatrolProceItemContrl>().PatrolIns_GDMS.text = jd2["workOrderDesc"].ToString();
                    obj.GetComponent<PatrolProceItemContrl>().PatrolIns_GDBH.text = jd2["workOrderNo"].ToString();
                    obj.GetComponent<PatrolProceItemContrl>().PatrolIns_XSRY.text = jd2["workOrderContacter"].ToString();
                    obj.GetComponent<PatrolProceItemContrl>().PatrolIns_CJR.text = jd2["creator"].ToString();
                    obj.GetComponent<PatrolProceItemContrl>().PatrolIns_XSRQ.text = jd2["workOrderDate"].ToString();
                    obj.GetComponent<PatrolProceItemContrl>().PatrolIns_CJSJ.text = jd2["gmtCreate"].ToString();
                    obj.GetComponent<PatrolProceItemContrl>().PatrolIns_GDMS2 = jd2["workOrderDesc"].ToString();
                    if (jd2["handleDesc"] != null)
                    {
                        obj.GetComponent<PatrolProceItemContrl>().PatrolIns_SHBTG.gameObject.SetActive(true);
                        obj.GetComponent<PatrolProceItemContrl>().PatrolIns_SHBTG.text = jd2["handleDesc"].ToString();
                        obj.GetComponent<PatrolProceItemContrl>().PatrolIns_GoBackBtn.SetActive(false);
                    }
                    else
                    {
                        obj.GetComponent<PatrolProceItemContrl>().PatrolIns_SHBTG.text = "后台没有数据";
                    }

                    if (obj.GetComponent<PatrolProceItemContrl>().PatrolIns_GDMS.text.Length > 10)
                    {
                        obj.GetComponent<PatrolProceItemContrl>().MoreGDMSBtn.SetActive(true);
                        obj.GetComponent<PatrolProceItemContrl>().PatrolIns_GDMS.gameObject.GetComponent<Button>().enabled = true;
                        obj.GetComponent<PatrolProceItemContrl>().MoreMenuText.text = jd2["workOrderDesc"].ToString();
                        GameManager.SetTextWithEllipsis(obj.GetComponent<PatrolProceItemContrl>().PatrolIns_GDMS,obj.GetComponent<PatrolProceItemContrl>().PatrolIns_GDMS.text,10);
                    }
                    else
                    {
                        obj.GetComponent<PatrolProceItemContrl>().MoreGDMSBtn.SetActive(false);
                        obj.GetComponent<PatrolProceItemContrl>().PatrolIns_GDMS.gameObject.GetComponent<Button>().enabled = false;
                    }
                    
                }
                
                StartCoroutine(MenuContrl.WebRequestContrl.WebRquestGetContrl("devopsWorkOrder/listTask", "?" + GameManager.DictionaryTostr(str),
                    data =>
                    {
                        Debug.Log("获取 工单基本信息下面的任务列表 接口:" + data);
                        JsonData jd = JsonMapper.ToObject(data);
                        if (jd["code"].ToString() == "0")
                        {
                            for (int i = 0; i < jd["data"].Count; i++)
                            {
                                GameObject obj2 = GameObject.Instantiate(MenuContrl.PatrolProceItemPrefab, transform.position, transform.rotation);
                                obj2.transform.SetParent(MenuContrl.PatrolProceMenu_Content.transform);
                                obj2.transform.localScale = new Vector3(1, 1, 1);

                                obj2.GetComponent<PatrolProceInfo>().Proce_RWBH.text = jd["data"][i]["taskNo"].ToString();
                                obj2.GetComponent<PatrolProceInfo>().Proce_RWLX.text = jd["data"][i]["taskSubtypeName"].ToString();
                                obj2.GetComponent<PatrolProceInfo>().Proce_RWMS.text = jd["data"][i]["taskDesc"].ToString();
                                obj2.GetComponent<PatrolProceInfo>().id = jd["data"][i]["id"].ToString();
                                GameManager.currentRWObj = obj2;


                                if (((IDictionary)jd["data"][i]).Contains("handleDesc"))
                                {
                                    if (jd["data"][i]["handleDesc"].ToString() != "")
                                    {
                                        obj2.GetComponent<PatrolProceInfo>().Proce_Input.text = jd["data"][i]["handleDesc"].ToString();
                                    }
                                }
                                if (obj2.GetComponent<PatrolProceInfo>().Proce_RWMS.text.Length > 15)
                                {
                                    GameManager.SetTextWithEllipsis(obj2.GetComponent<PatrolProceInfo>().Proce_RWMS,obj2.GetComponent<PatrolProceInfo>().Proce_RWMS.text,15);
                                    obj2.GetComponent<PatrolProceInfo>().MoreBtn.SetActive(true);
                                    obj2.GetComponent<PatrolProceInfo>().MoreMenuText.text =  jd["data"][i]["taskDesc"].ToString();
                                }
                                else
                                {
                                    obj2.GetComponent<PatrolProceInfo>().Proce_RWMS.text = jd["data"][i]["taskDesc"].ToString();
                                }
                            }
                        }
                        MenuContrl.DaiChuLi_ScrollView.SetActive(false);
                        MenuContrl.PatrolInspectionMenu.SetActive(true);
                        MenuContrl.PatrolProceMenu_Content.transform.parent.gameObject.transform.parent.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                    }));
            }));
    }
}

