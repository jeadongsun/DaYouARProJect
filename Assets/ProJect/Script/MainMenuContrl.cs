using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using LitJson;

public class MainMenuContrl : MonoBehaviour
{

    public UnityWebRequestContrl WebRequestContrl;

    //巡视界面
    public GameObject PatrolMenu;

    //首页 按钮
    public GameObject[] MainMenuBtnItem;
    
    //巡视界面 左边菜单栏 bg 动画
    public GameObject PatrolMenuBtnItem;

    //巡视界面 工单 列表
    public GameObject DaiChuLi_ScrollView;

    //巡视界面 左边菜单栏
    public GameObject[] PatrolMenuBtnItems;
    
    //巡视界面 左边菜单栏 文字
    public Text[] PatrolMenuTextItems;
    
    //巡视界面 工单列表点击 详情页 
    public GameObject PatrolInspectionMenu;
    
    //巡视界面 工单列表 Content
    public GameObject DaiChuLi_ScrollView_Content;
    
    //巡视界面 工单列表 Prefab
    public GameObject PatrolMenuItem;
    
    //巡视界面 详情 Content
    public GameObject PatrolProceMenu_Content;
    
    //巡视界面 详情 Prefab 1
    public GameObject PatrolMessageMenu;
    
    //巡视界面 详情 Prefab 2
    public GameObject PatrolProceItemPrefab;
    
    //巡视界面 详情 更多文字
    public GameObject MoreGDMSMenu;
    
    //巡视界面 详情 更多文字
    public Text MoreGDMSText;

    //巡视界面 退单 界面
    public GameObject PatrolInspection_BackMenu;
    
    //巡视 界面 退单 文本框
    public InputField PatrolInspectionBackInput;
    
    //巡视 界面 任务列表 List
    public List<WorkOrderInfoArray> WorkOrderInfoArrayList = new List<WorkOrderInfoArray>();
    
    //巡视 界面 待审核 列表 Prefab
    public GameObject PatrolReviewItem;

    public GameObject PatrolInspectionMenu2;

    public GameObject PatrolInspectionMenu2_Content;
    
    //巡视 界面 待审核 详情页 Prefab
    public GameObject Patrol_SH_Menu;
    
    //巡视 界面 待审核 详情页 任务列表 Prefab
    public GameObject Patrol_SHRW_Item;

    //巡视工单 已完成 列表
    public GameObject DaiChuLi_ScrollView_WanCheng;
    
    //巡视工单 已完成 列表 Content
    public GameObject DaiChuLi_ScrollView_WanCheng_Content;
    
    //巡视工单 已完成 列表 Content
    public GameObject PatrolMenuItem_WanCheng_Prefab;

    public int currentUIIndex = 0;
    
    //巡视工单 已完成 任务列表 Prefab
    public GameObject Patrol_WC_Menu;
    
    //巡视工单 已完成 任务列表 GameObject
    public GameObject PatrolInspectionMenu_XunShi_WanCheng;
    
    //巡视工单 已完成 任务列表 Content
    public GameObject PatrolInspectionMenu_XunShi_WanCheng_Content;
    
    //缺陷工单 待处理 列表 Prefab
    public GameObject DaiChuLi_DefectMenuItem_Prefab;
    
    //缺陷工单 待审核 列表 Prefab
    public GameObject DaiShenHe_DefectMenuItem_Prefab;
    
    //缺陷工单 已完成 列表 Prefab
    public GameObject WanCheng_DefectMenuItem_Prefab;

    public string UIState = "";
    
    //缺陷工单 待处理 工单详情列表 Prefab
    public GameObject DaiChuLi_DefectMoreMenu_Prefab;
    
    //缺陷工单 待处理 工单子列表 Prefab
    public GameObject DaiChuLi_DefectTaskMenu_Prefab;
    
    //缺陷工单 待审核 工单详情列表 Prefab
    public GameObject DaiChuLi_SHDefectMoreMenu_Prefab;
    
    //缺陷工单 待审核 工单子列表 Prefab
    public GameObject DaiChuLi_SHRWDefectTaskMenu_Prefab;
    
    //缺陷工单 已完成 工单详情列表 Prefab
    public GameObject DaiChuLi_WCDefectMoreMenu_Prefab;
    
    //缺陷工单 已完成 工单子列表 Prefab
    public GameObject DaiChuLi_WCRWDefectTaskMenu_Prefab;

    public GameObject Canvas;

    public GameObject MainCamera;

    public GameObject UserBtn;
    
    //个人中心界面
    public GameObject UserMenu;
    
    //个人中心 用户ID Text
    public Text UserID;
    
    //个人中心 用户昵称 Text
    public Text UserName;
    
    //个人中心 用户手机 Text
    public Text UserPhoneNum;

    //二级界面 标题
    public Text PatrolMenu_TitleText;
    
    //保存成功 提示界面
    public GameObject SaveTipMenu;

    public Text SaveTipMenuText;

    public Text TitleTime_XJ;

    public GameObject CalendarContrlMenu_XJ;
    
    public bool isCalendarContrlMenuState_XJ = false;

    public GameObject[] MonthItem;

    public GameObject[] MonthItem_Text;

    public Text YearText;

    public int currentYear;

    public GameObject CalendarMenu;

    public GameObject currentRawImg_Prefab;

    public GameObject WWWImage_Prefab;
    
    //暂无信息
    public GameObject NoDataMenu;

    public GameObject CurrentSelectImg;

    public GameObject currentImg;
    
    public GameObject currentImg2;
    
    private void Awake()
    {
        foreach (var item in MainMenuBtnItem)
        {
            item.transform.DOScale(new Vector3(0,0,0),0f);
        }
        
        PatrolMenuBtnItem.GetComponent<RectTransform>().DOAnchorPosX(-746, 0f, false);

        DaiChuLi_ScrollView.GetComponent<RectTransform>().DOAnchorPosX(2013, 0f, false);

        UserBtn.GetComponent<Text>().DOFade(0, 0);

        YearText.text = System.DateTime.Now.Year.ToString() + "年";

        currentYear = System.DateTime.Now.Year;

        if (GameManager.currentDevice == true)
        {
            Canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.5f;
        } 
    }

    private IEnumerator Start()
    {
        foreach (var item in MainMenuBtnItem)
        {
            yield return new WaitForSeconds(0.1f);
            item.transform.DOScale(new Vector3(1,1,1),0.5f).SetEase(Ease.InQuad);
        }
        
        UserBtn.GetComponent<Text>().DOFade(1, 0.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
           Debug.Log(GameManager.imageUrl.Count);
        }
    }

    //打开 巡视界面
    public void Open_PatrolMenuClick()
    {
        UIState = "巡视";
        PatrolMenu.SetActive(true);
        
        PatrolMenuBtnItem.GetComponent<RectTransform>().DOAnchorPosX(-225, 0.5f, false);
        
        DaiChuLi_ScrollView.GetComponent<RectTransform>().DOAnchorPosX(210, 0.5f, false);

        PatrolMenu_DaiChuLiBtnClick();

        PatrolMenu_TitleText.text = "巡视工单";
    }
    
    //打开 缺陷界面
    public void Open_DefectMenuClick()
    {
        UIState = "缺陷";
        PatrolMenu.SetActive(true);
        
        PatrolMenuBtnItem.GetComponent<RectTransform>().DOAnchorPosX(-225, 0.5f, false);
        
        DaiChuLi_ScrollView.GetComponent<RectTransform>().DOAnchorPosX(210, 0.5f, false);

        PatrolMenu_DaiChuLiBtnClick();

        PatrolMenu_TitleText.text = "缺陷工单";
    }

    //关闭 巡视界面 回到首页
    public void Close_PatrolMenuClick()
    {
        PatrolMenu.SetActive(false);
        
        PatrolMenuBtnItem.GetComponent<RectTransform>().DOAnchorPosX(-746, 0f, false);
        DaiChuLi_ScrollView.GetComponent<RectTransform>().DOAnchorPosX(2013, 0f, false);
        
        PatrolInspectionMenu.SetActive(false);
        
        DaiChuLi_ScrollView_WanCheng.SetActive(false);
        DaiChuLi_ScrollView.SetActive(false);
        
        CalendarMenu.SetActive(false);
    }
    
    //巡视界面 待处理
    public void PatrolMenu_DaiChuLiBtnClick()
    {
        MainMenuBtnItemContrl(0,"0");
        CalendarMenu.SetActive(false);
    }
    //巡视界面 待审核
    public void PatrolMenu_DaiShenHeBtnClick()
    {
        MainMenuBtnItemContrl(1,"1");
        CalendarMenu.SetActive(false);
    }
    //巡视界面 已完成
    public void PatrolMenu_YiWanChengBtnClick()
    {
        MainMenuBtnItemContrl(2,"9");

        InitCalendarMenuContrl();
    }

    //巡视界面 菜单控制
    public void MainMenuBtnItemContrl(int index, string ListType,string date = null)
    {
        foreach (Transform item in DaiChuLi_ScrollView_Content.transform)
        {
            Destroy(item.gameObject);
        }

        foreach (Transform item in DaiChuLi_ScrollView_WanCheng_Content.transform)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in PatrolMenuBtnItems)
        {
            item.GetComponent<Image>().enabled = false;
        }

        PatrolMenuBtnItems[index].GetComponent<Image>().enabled = true;

        foreach (var item in PatrolMenuTextItems)
        {
            item.GetComponent<Text>().DOColor(new Color(0 / 255f, 0 / 255f, 0 / 255f, 255 / 255f), 0.3f);
        }

        PatrolMenuTextItems[index].GetComponent<Text>().DOColor(new Color(255 / 255f, 255 / 255f, 255 / 255f, 255f / 255f), 0.3f);

        Resources.UnloadUnusedAssets();

        Close_PatrolInspectionMenu();

        DaiChuLi_ScrollView.SetActive(false);
        DaiChuLi_ScrollView_WanCheng.SetActive(false);
        //巡视 接口 操作
        switch (UIState)
        {
            case "巡视":
                Dictionary<string, string> str = new Dictionary<string, string>();
                str.Add("workOrderType", "1");
                str.Add("workOrderHandleStatus", ListType);
                str.Add("pageNumber", "1");
                str.Add("pageSize", "100");
                if (date != null)
                {
                    str.Add("date",date);
                }
                StartCoroutine(WebRequestContrl.WebRquestGetContrl("devopsWorkOrder/listApp",
                    "?" + GameManager.DictionaryTostr(str),
                    data =>
                    {
                        Debug.Log("获取巡视工单接口：" + data);
                        JsonData jd = JsonMapper.ToObject(data);
                        JsonData jd2 = jd["data"];
                        if (jd["code"].ToString() == "0")
                        {
                            if (jd2["list"].Count == 0)
                            {
                                NoDataMenu.SetActive(true);
                            }
                            else
                            {
                                NoDataMenu.SetActive(false);
                            }
                            for (int i = 0; i < jd2["list"].Count; i++)
                            {
                                switch (ListType)
                                {
                                    case "0":
                                        GameObject obj = GameObject.Instantiate(PatrolMenuItem, transform.position,
                                            transform.rotation);
                                        obj.transform.SetParent(DaiChuLi_ScrollView_Content.transform);
                                        obj.transform.localScale = new Vector3(1, 1, 1);

                                        obj.GetComponent<PatrolMenuIBtnItemClick>().PartrolNum.text = jd2["list"][i]["workOrderNo"].ToString();
                                        obj.GetComponent<PatrolMenuIBtnItemClick>().PartrolNameValue.text = jd2["list"][i]["ceCustName"].ToString();
                                        obj.GetComponent<PatrolMenuIBtnItemClick>().PartrolTimeValue.text = jd2["list"][i]["workOrderDate"].ToString();
                                        obj.GetComponent<PatrolMenuIBtnItemClick>().id = jd2["list"][i]["id"].ToString();

                                        DaiChuLi_ScrollView.SetActive(true);

                                        DaiChuLi_ScrollView_Content.transform.parent.gameObject.transform.parent
                                            .GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                                        break;
                                    case "1":
                                        GameObject obj2 = GameObject.Instantiate(PatrolReviewItem, transform.position,
                                            transform.rotation);
                                        obj2.transform.SetParent(DaiChuLi_ScrollView_Content.transform);
                                        obj2.transform.localScale = new Vector3(1, 1, 1);

                                        obj2.GetComponent<PatrolReviewItemContrl>().PartrolNum.text =
                                            jd2["list"][i]["workOrderNo"].ToString();
                                        obj2.GetComponent<PatrolReviewItemContrl>().PartrolNameValue.text =
                                            jd2["list"][i]["ceCustName"].ToString();
                                        obj2.GetComponent<PatrolReviewItemContrl>().PartrolTimeValue.text =
                                            jd2["list"][i]["workOrderDate"].ToString();
                                        obj2.GetComponent<PatrolReviewItemContrl>().id =
                                            jd2["list"][i]["id"].ToString();

                                        DaiChuLi_ScrollView.SetActive(true);
                                        DaiChuLi_ScrollView_Content.transform.parent.gameObject.transform.parent
                                            .GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                                        break;
                                    case "9":

                                        GameObject obj3 = GameObject.Instantiate(PatrolMenuItem_WanCheng_Prefab,
                                            transform.position, transform.rotation);
                                        obj3.transform.SetParent(DaiChuLi_ScrollView_WanCheng_Content.transform);
                                        obj3.transform.localScale = new Vector3(1, 1, 1);

                                        obj3.GetComponent<PatrolMenuItem_WanCheng>().PartrolNum.text = jd2["list"][i]["workOrderNo"].ToString();
                                        obj3.GetComponent<PatrolMenuItem_WanCheng>().PartrolNameValue.text = jd2["list"][i]["ceCustName"].ToString();
                                        obj3.GetComponent<PatrolMenuItem_WanCheng>().PartrolTimeValue.text = jd2["list"][i]["handleTime"].ToString();
                                        obj3.GetComponent<PatrolMenuItem_WanCheng>().id = jd2["list"][i]["id"].ToString();
                                        obj3.GetComponent<PatrolMenuItem_WanCheng>().PartrolTimeValue_CJRQ.text = jd2["list"][i]["gmtCreate"].ToString();

                                        DaiChuLi_ScrollView_WanCheng.SetActive(true);
                                        DaiChuLi_ScrollView_WanCheng_Content.transform.parent.gameObject.transform.parent
                                            .GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
                                        break;
                                }
                            }
                        }
                    }));
                break;
            case "缺陷":
               Dictionary<string, string> str2 = new Dictionary<string, string>();
               str2.Add("workOrderType", "2");
               str2.Add("workOrderHandleStatus", ListType);
               str2.Add("pageNumber", "1");
               str2.Add("pageSize", "100");
               if (date != null)
               {
                   str2.Add("date",date);
               }
               StartCoroutine(WebRequestContrl.WebRquestGetContrl("devopsWorkOrder/listApp",
                    "?" + GameManager.DictionaryTostr(str2),
                    data =>
                    {
                        
                        Debug.Log("获取缺陷工单接口：" + data);
                        JsonData jd = JsonMapper.ToObject(data);
                        JsonData jd2 = jd["data"];
                        if (jd["code"].ToString() == "0")
                        {
                            if (jd2["list"].Count == 0)
                            {
                                NoDataMenu.SetActive(true);
                            }
                            else
                            {
                                NoDataMenu.SetActive(false);
                            }
                            for (int i = 0; i < jd2["list"].Count; i++)
                            {
                                switch (ListType)
                                {
                                    case "0":
                                        GameObject obj = GameObject.Instantiate(DaiChuLi_DefectMenuItem_Prefab, transform.position, transform.rotation);
                                        obj.transform.SetParent(DaiChuLi_ScrollView_Content.transform);
                                        obj.transform.localScale = new Vector3(1, 1, 1);

                                        obj.GetComponent<DaiChuLi_DefectMenuItem>().PartrolNum.text = jd2["list"][i]["workOrderNo"].ToString();
                                        obj.GetComponent<DaiChuLi_DefectMenuItem>().PartrolNameValue.text = jd2["list"][i]["ceCustName"].ToString();
                                        obj.GetComponent<DaiChuLi_DefectMenuItem>().PartrolTimeValue.text = jd2["list"][i]["workOrderDate"].ToString();
                                        obj.GetComponent<DaiChuLi_DefectMenuItem>().id = jd2["list"][i]["id"].ToString();

                                        DaiChuLi_ScrollView.SetActive(true);
                                        break;
                                    case "1":
                                        GameObject obj2 = GameObject.Instantiate(DaiShenHe_DefectMenuItem_Prefab, transform.position, transform.rotation);
                                        obj2.transform.SetParent(DaiChuLi_ScrollView_Content.transform);
                                        obj2.transform.localScale = new Vector3(1, 1, 1);

                                        obj2.GetComponent<DaiShenHe_DefectMenuItem>().PartrolNum.text = jd2["list"][i]["workOrderNo"].ToString();
                                        obj2.GetComponent<DaiShenHe_DefectMenuItem>().PartrolNameValue.text = jd2["list"][i]["ceCustName"].ToString();
                                        obj2.GetComponent<DaiShenHe_DefectMenuItem>().PartrolTimeValue.text = jd2["list"][i]["workOrderDate"].ToString();
                                        obj2.GetComponent<DaiShenHe_DefectMenuItem>().id = jd2["list"][i]["id"].ToString();

                                        DaiChuLi_ScrollView.SetActive(true);
                                        break;
                                    case "9":

                                        GameObject obj3 = GameObject.Instantiate(WanCheng_DefectMenuItem_Prefab, transform.position, transform.rotation);
                                        obj3.transform.SetParent(DaiChuLi_ScrollView_WanCheng_Content.transform);
                                        obj3.transform.localScale = new Vector3(1, 1, 1);

                                        obj3.GetComponent<WanCheng_DefectMenuItem>().PartrolNum.text = jd2["list"][i]["workOrderNo"].ToString();
                                        obj3.GetComponent<WanCheng_DefectMenuItem>().PartrolNameValue.text = jd2["list"][i]["ceCustName"].ToString();
                                        obj3.GetComponent<WanCheng_DefectMenuItem>().PartrolTimeValue.text = jd2["list"][i]["handleTime"].ToString();
                                        obj3.GetComponent<WanCheng_DefectMenuItem>().id = jd2["list"][i]["id"].ToString();
                                        obj3.GetComponent<WanCheng_DefectMenuItem>().PartrolTimeValue_CJRQ.text = jd2["list"][i]["gmtCreate"].ToString();

                                        DaiChuLi_ScrollView_WanCheng.SetActive(true);
                                        break;
                                }
                            }
                        }
                    }));
                break;
        }
    }

    //打开 巡视界面 工单界面
    public void Open_PatrolInspectionMenu()
    {
        //PatrolInspectionMenu.SetActive(true);
        //DaiChuLi_ScrollView.SetActive(false);
    }
    
    //关闭 巡视界面 工单界面
    public void Close_PatrolInspectionMenu()
    {
        PatrolInspectionMenu.SetActive(false);
        PatrolInspectionMenu2.SetActive(false);
        PatrolInspectionMenu_XunShi_WanCheng.SetActive(false);
        GameManager.imageUrl.Clear();
        GameManager.imageIndex = 0;
        DaiChuLi_ScrollView.transform.localScale = new Vector3(1, 1, 1);
        DaiChuLi_ScrollView_WanCheng.transform.localScale = new Vector3(1, 1, 1);
        switch (currentUIIndex)
        {
            case 1:
                DaiChuLi_ScrollView.SetActive(true);
                break;
            
            case 2:
                DaiChuLi_ScrollView_WanCheng.SetActive(true);
                break;
        }

        if (CalendarMenu.transform.localScale.x == 0)
        {
            CalendarMenu.transform.localScale = new Vector3(1, 1, 1);
        }
        foreach (Transform item in PatrolProceMenu_Content.transform)
        {
            Destroy(item.gameObject);
        }
        
        foreach (Transform item in PatrolInspectionMenu2_Content.transform)
        {
            Destroy(item.gameObject);
        }
        
        foreach (Transform item in PatrolInspectionMenu_XunShi_WanCheng_Content.transform)
        {
            Destroy(item.gameObject);
        }
        Resources.UnloadUnusedAssets();

        switch (UIState)
        {
            case  "巡视":
                PatrolMenu_TitleText.text = "巡视工单";
                break;
            case  "缺陷":
                PatrolMenu_TitleText.text = "缺陷工单";
                break;
        }
    }
    
    //巡视界面 详情 下拉框监听
    public void PatrolProceMenuScroViewOnChange()
    {
        // MoreGDMSMenu.SetActive(false);
        // XS_DSH_MoreGDMSMenu.SetActive(false);
        // foreach (Transform item in PatrolProceMenu_Content.transform)
        // {
        //     if (item.GetComponent<PatrolProceItemContrl>())
        //     {
        //         item.GetComponent<PatrolProceItemContrl>().isMoreBtn = false;
        //     }
        // }
        // foreach (Transform item in PatrolInspectionMenu2_Content.transform)
        // {
        //     if (item.GetComponent<Patrol_SH_MenuContrl>())
        //     {
        //         item.GetComponent<Patrol_SH_MenuContrl>().isMoreState = false;
        //     }
        // }
    }
        
    //确认提交 巡视界面 回退订单
    public void PatrolInspectionSubmitBtnClick()
    {
        JsonData data = new JsonData();
        data["id"] = GameManager.devopsWorkOrderId;
        data["reason"] = PatrolInspectionBackInput.text;
        Debug.Log(data.ToJson());
        StartCoroutine(WebRequestContrl.UnityWebRequestPost("devopsWorkOrder/rollbackWorkOrder",data, data =>
        {
            Debug.Log("确认提交-巡视界面-回退订单:" + data);
            JsonData jd = JsonMapper.ToObject(data);
            if (jd["code"].ToString() == "0")
            {
                Close_PatrolInspection_BackMenu();
                Close_PatrolInspectionMenu();

                foreach (Transform item in DaiChuLi_ScrollView_Content.transform)
                {
                    if (item.GetComponent<PatrolMenuIBtnItemClick>())
                    {
                        if (item.GetComponent<PatrolMenuIBtnItemClick>().id == GameManager.devopsWorkOrderId)
                        {
                           Destroy(item.gameObject);
                        }
                    }
                    
                }
                foreach (Transform item in DaiChuLi_ScrollView_Content.transform)
                {
                    if (item.GetComponent<DaiChuLi_DefectMenuItem>())
                    {
                        Debug.Log(GameManager.devopsWorkOrderId);
                        if (item.GetComponent<DaiChuLi_DefectMenuItem>().id == GameManager.devopsWorkOrderId)
                        {
                            Destroy(item.gameObject);
                        }
                    }
                }
            }
        }));
    }
    //关闭 巡视界面 回退订单
    public void Close_PatrolInspection_BackMenu()
    {
        PatrolInspection_BackMenu.SetActive(false);
        PatrolInspectionBackInput.text = "";
    }
    
    //提交 巡视工单 界面
    public void PatrolIns_SubmitBtnClick()
    {
        PatroIns_SaveAndSubmitContrl(2);
    }
    
    //保存 巡视工单 任务列表 界面
    public void PatrolIns_SaveBtnClick()
    {
        PatroIns_SaveAndSubmitContrl(1);
    }
    
    //巡视工单 保存 提交 控制
    public void PatroIns_SaveAndSubmitContrl(int index)
    {
        bool isReturn = false;
        foreach (Transform item in PatrolProceMenu_Content.transform)
        {
            if (item.GetComponent<PatrolProceInfo>())
            {
                if (item.GetComponent<PatrolProceInfo>().Proce_Input.text == "")
                {
                    isReturn = true;
                }
            }
            
            if (item.GetComponent<DaiChuLi_DefectTaskMenu>())
            {
                if (item.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_Input.text == "")
                {
                    isReturn = true;
                }

                if (item.GetComponent<DaiChuLi_DefectTaskMenu>().WanChengBtn_Icon.activeSelf == false && item.GetComponent<DaiChuLi_DefectTaskMenu>().WeiWanChengBtn_Icon.activeSelf == false)
                {
                    isReturn = true;
                }
            }
        }
        

        if (GameManager.imageUrl.Count == 0)
        {
            isReturn = true;
        }

        if (isReturn == true)
        {
            StartCoroutine(Open_SaveTipMenu(3));
            isReturn = false;
            return;
        }
        
        if (UIState == "巡视")
        {
            foreach (Transform item in PatrolProceMenu_Content.transform)
            {
                if (item.GetComponent<PatrolProceInfo>())
                {
                    WorkOrderInfoArray info = new WorkOrderInfoArray();
                    info.id = item.GetComponent<PatrolProceInfo>().id;
                    info.handleDesc = item.GetComponent<PatrolProceInfo>().Proce_Input.text;
                    //照片
                    info.handleAttach = string.Join(",", GameManager.imageUrl.ToArray());
                    info.handleResult = "完成";
                    WorkOrderInfoArrayList.Add(info);
                }
            }
            
            WorkOrderInfoArray[] sWorkOrderInfoArrays = new WorkOrderInfoArray[WorkOrderInfoArrayList.Count];
            for (int i = 0; i < WorkOrderInfoArrayList.Count; i++)
            {
                sWorkOrderInfoArrays[i] = new WorkOrderInfoArray();
                sWorkOrderInfoArrays[i].id = WorkOrderInfoArrayList[i].id;
                sWorkOrderInfoArrays[i].handleDesc = WorkOrderInfoArrayList[i].handleDesc;
                sWorkOrderInfoArrays[i].handleAttach = WorkOrderInfoArrayList[i].handleAttach;
                sWorkOrderInfoArrays[i].handleResult = WorkOrderInfoArrayList[i].handleResult;
            }
            
            WorkOrderInfoData workOrderInfoData = new WorkOrderInfoData(index, GameManager.devopsWorkOrderId, sWorkOrderInfoArrays);
            string json = JsonMapper.ToJson(workOrderInfoData);
            json = Regex.Unescape(json);
            
            StartCoroutine(WebRequestContrl.UnityWebRequestPost("devopsWorkOrder/submitAppTask", json, data =>
            {
                Debug.Log("提交 待处理接口：" + data);
                JsonData jd = JsonMapper.ToObject(data);
                if (jd["code"].ToString() == "0")
                {
                    if (index == 2)
                    {
                        Debug.Log("提交工单 成功");
                        Close_PatrolInspection_BackMenu();
                        Close_PatrolInspectionMenu();
                        
                        foreach (Transform item in DaiChuLi_ScrollView_Content.transform)
                        {
                            if (item.GetComponent<PatrolMenuIBtnItemClick>())
                            {
                                if (item.GetComponent<PatrolMenuIBtnItemClick>().id == GameManager.devopsWorkOrderId)
                                {
                                    Destroy(item.gameObject);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("保存工单 成功");
                    }
                    StartCoroutine(Open_SaveTipMenu(index));
                }
            }));
        }
        else if (UIState == "缺陷")
        {
            foreach (Transform item in PatrolProceMenu_Content.transform)
            {
                if (item.GetComponent<DaiChuLi_DefectTaskMenu>())
                {
                    WorkOrderInfoArray info = new WorkOrderInfoArray();
                    info.id = item.GetComponent<DaiChuLi_DefectTaskMenu>().id;
                    info.handleDesc = item.GetComponent<DaiChuLi_DefectTaskMenu>().Proce_Input.text;
                    //照片
                    info.handleAttach = string.Join(",", GameManager.imageUrl.ToArray());
                    if (item.GetComponent<DaiChuLi_DefectTaskMenu>().WanChengBtn_Icon.activeSelf == true)
                    {
                        info.handleResult = "完成";
                    }
                    else if (item.GetComponent<DaiChuLi_DefectTaskMenu>().WeiWanChengBtn_Icon.activeSelf == true)
                    {
                        info.handleResult = "未完成";
                    }
                    WorkOrderInfoArrayList.Add(info);
                }
            }
            WorkOrderInfoArray[] sWorkOrderInfoArrays = new WorkOrderInfoArray[WorkOrderInfoArrayList.Count];
            for (int i = 0; i < WorkOrderInfoArrayList.Count; i++)
            {
                sWorkOrderInfoArrays[i] = new WorkOrderInfoArray();
                sWorkOrderInfoArrays[i].id = WorkOrderInfoArrayList[i].id;
                sWorkOrderInfoArrays[i].handleDesc = WorkOrderInfoArrayList[i].handleDesc;
                sWorkOrderInfoArrays[i].handleAttach = WorkOrderInfoArrayList[i].handleAttach;
                sWorkOrderInfoArrays[i].handleResult = WorkOrderInfoArrayList[i].handleResult;
            }

            WorkOrderInfoData workOrderInfoData = new WorkOrderInfoData(index, GameManager.devopsWorkOrderId, sWorkOrderInfoArrays);
            string json = JsonMapper.ToJson(workOrderInfoData);
            json = Regex.Unescape(json);
            StartCoroutine(WebRequestContrl.UnityWebRequestPost("devopsWorkOrder/submitAppTask", json, data =>
            {
                Debug.Log("提交 待处理接口：" + data);
                JsonData jd = JsonMapper.ToObject(data);
                if (jd["code"].ToString() == "0")
                {
                    if (index == 2)
                    {
                        Debug.Log("提交工单 成功");
                        Close_PatrolInspection_BackMenu();
                        Close_PatrolInspectionMenu();

                        foreach (Transform item in DaiChuLi_ScrollView_Content.transform)
                        {
                            if (item.GetComponent<DaiChuLi_DefectMenuItem>())
                            {
                                if (item.GetComponent<DaiChuLi_DefectMenuItem>().id == GameManager.devopsWorkOrderId)
                                {
                                    Destroy(item.gameObject);
                                }
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("保存工单 成功");
                    }

                    StartCoroutine(Open_SaveTipMenu(index));
                }
            }));
            WorkOrderInfoArrayList.Clear();
        }
    }

    //返回 登录界
    public void QuitBtnClick()
    {
        //PlayerPrefs.SetString("UserName","");
        PlayerPrefs.SetString("UserPwd","");
        SceneManager.LoadScene("LoginMenu");
    }
    
    //进入AR 场景
    public void GoARBtnClick()
    {
        Canvas.SetActive(false);
        MainCamera.SetActive(false);
        GameManager.isARScene = true;
        SceneManager.LoadScene("ARScene", LoadSceneMode.Additive);
    }
    
    //打开 个人中心界面
    public void Open_UserMenu()
    {
        UserMenu.SetActive(true);
        StartCoroutine(WebRequestContrl.WebRquestGetContrl("loadAuthUser",
            "", data =>
            {
                Debug.Log("获取个人中心界面：" + data);
                JsonData jd = JsonMapper.ToObject(data);
                JsonData jd2 = jd["data"];

                UserID.text = jd2["userId"].ToString();
                UserName.text = jd2["userFullname"].ToString();
                UserPhoneNum.text = jd2["userMobile"].ToString();
            })); 
    }
    
    //关闭 个人中心界面
    public void Close_UserMenu()
    {
        UserMenu.SetActive(false);
    }
    
    //退出登录
    public void BackLoginMainMenu()
    {
        //PlayerPrefs.SetString("UserName","");
        PlayerPrefs.SetString("UserPwd","");
        SceneManager.LoadScene("LoginMenu");
    }
    
    //保存 提交 提示界面
    public IEnumerator Open_SaveTipMenu(int index)
    {
        switch (index)
        {
            case 1:
                SaveTipMenuText.text = "保存成功";
                break;
            case 2:
                SaveTipMenuText.text = "提交成功";
                break;
            case 3:
                SaveTipMenuText.text = "缺少必填项";
                break;
        }
        SaveTipMenu.GetComponent<RectTransform>().DOAnchorPosY(615, 0.5f).SetEase(Ease.Linear);
        
       yield return new WaitForSeconds(2.5f);
        
        SaveTipMenu.GetComponent<RectTransform>().DOAnchorPosY(1500, 0f);
    }

    //巡检 已完成 日历
    public void CalendarContrlMenu_XJ_Click()
    {
       
        if (isCalendarContrlMenuState_XJ == false)
        {
            isCalendarContrlMenuState_XJ = true;
            CalendarContrlMenu_XJ.SetActive(true);
        }
        else
        {
            isCalendarContrlMenuState_XJ = false;
            CalendarContrlMenu_XJ.SetActive(false);
        }
    }

    public void PageYearZBtn()
    {
        currentYear = currentYear - 1;
        YearText.text = currentYear.ToString() + "年";
    }

    public void PageYearYBtn()
    {
        currentYear = currentYear + 1;
        YearText.text = currentYear.ToString() + "年";
    }

    public void InitCalendarMenuContrl()
    {
        CalendarMenu.SetActive(true);
        isCalendarContrlMenuState_XJ = false;
        CalendarContrlMenu_XJ.SetActive(false);
        foreach (var item in MonthItem)
        {
            item.GetComponent<Image>().DOFade(0, 0);
        }
        foreach (var item in MonthItem_Text)
        {
            item.GetComponent<Text>().DOColor(new Color(102f / 255f, 102f / 255f, 102f / 255f, 255f / 255f), 0);
        }
        YearText.text = System.DateTime.Now.Year.ToString() + "年";
        currentYear = System.DateTime.Now.Year;
        CalendarMenu.transform.localScale = new Vector3(1, 1, 1);
        TitleTime_XJ.text = "全部";
    }

    //工单 ar界面
    public void ProceARBtnClick()
    {
        Canvas.SetActive(false);
        MainCamera.SetActive(false);
        GameManager.isARScene = false;
        SceneManager.LoadScene("ARScene", LoadSceneMode.Additive);
    }

    //查看 缩略图 1 
    public void OepnCurrentSelectImg(RawImage images)
    {
        CurrentSelectImg.SetActive(true);
        currentImg.GetComponent<RawImage>().texture = images.texture;
        currentImg.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
    }
    //查看 缩略图 2
    public void OepnCurrentSelectImg2(Image images)
    {
        CurrentSelectImg.SetActive(true);
        currentImg2.GetComponent<Image>().sprite = images.sprite;
        currentImg2.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
    }

    public void CloseCurrentSelectImg()
    {
        currentImg.transform.DOScale(new Vector3(0,0,0), 0.5f);
        currentImg2.transform.DOScale(new Vector3(0,0,0), 0.5f);
        CurrentSelectImg.SetActive(false);
    }

    public void CloseMoreMenu()
    {
       
    }
}
