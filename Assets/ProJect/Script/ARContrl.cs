using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using LitJson;
using DG.Tweening;
using UnityEngine.UIElements;

public class ARContrl : MonoBehaviour
{
    public MainMenuContrl MenuContrl;
    public GameObject WorkOrderMenu;

    public UnityWebRequestContrl _webRequestContrl;

    public GameObject AlertMenuItemPrefab;

    public Sprite[] AlertMoreMenu_IconItem;

    //工单编号
    public Text WoMenu_GDBH;

    //用户名称
    public Text WoMenu_YHMC;

    //工单描述
    public Text WoMenu_GDMS;

    //处理人员
    public Text WoMenu_CLRY;

    //处理日期
    public Text WoMenu_CLRQ;

    //创建人
    public Text WoMenu_CJR;

    //创建日期
    public Text WoMenu_CJSJ;

    public GameObject WorkOrderMenu_Content;

    public GameObject WorkOrderMenuList_Prefab;

    public Sprite[] WorkOrderMenuList_IconState;

    public Text SaveTipMenuText;

    public GameObject SaveTipMenu;

    public GameObject PatrolInspection_BackMenu;

    public InputField PatrolInspection_BackMenu_Text;

    public GameObject currentRawImgPrefab;

    public string currentWorkId;
    
    public List<WorkOrderInfoArray> WorkOrderInfoArrayList = new List<WorkOrderInfoArray>();

    public Camera m_Camera;
    private void Awake()
    {
    
        GameManager.imageUrl2.Clear();
        _webRequestContrl = GameObject.Find("UnityWebRequestContrl").GetComponent<UnityWebRequestContrl>();

        // string pwd = PlayerPrefs.GetString("UserName") + "@@" + PlayerPrefs.GetString("UserPwd");
        // StartCoroutine(_webRequestContrl.WebRequestLoginContrl(PlayerPrefs.GetString("UserName"), pwd, data =>
        // {
        //     if (data == "error")
        //     {
        //
        //     }
        //     else
        //     {
        //         Debug.Log("登入成功");
        //         JsonData jd = JsonMapper.ToObject(data);
        //         GameManager.userToken = jd["access_token"].ToString();
        //     }
        // })); 
    }

    private void Start()
    {
        if (GameObject.Find("MainMenuContrl"))
        {
            MenuContrl = GameObject.Find("MainMenuContrl").GetComponent<MainMenuContrl>();
        }
    }

    //关闭 工单界面
    public void CloseWorkOrderMenu()
    {
        foreach (Transform item in WorkOrderMenu_Content.transform)
        {
            Destroy(item.gameObject);
        }
        
        Resources.UnloadUnusedAssets();
        WorkOrderMenu.SetActive(false);
        GameManager.imageUrl2.Clear();
    }

    //返回 主页
    public void BackMainMenuBtnClick()
    {
        MenuContrl.Canvas.SetActive(true);
        MenuContrl.MainCamera.SetActive(true);
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("ARScene"));
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
            case 4:
                SaveTipMenuText.text = "回退订单";
                break;
            case 5:
                SaveTipMenuText.text = "保存到相册";
                break;
        }

        SaveTipMenu.GetComponent<RectTransform>().DOAnchorPosY(-140, 0.5f).SetEase(Ease.Linear);

        yield return new WaitForSeconds(2.5f);

        SaveTipMenu.GetComponent<RectTransform>().DOAnchorPosY(1500, 0f);
    }

    //打开 回退工单界面
    public void Open_PatrolInspection_BackMenu()
    {
        PatrolInspection_BackMenu.SetActive(true);
    }

    //关闭 回退工单界面
    public void Close_PatrolInspection_BackMenu()
    {
        PatrolInspection_BackMenu.SetActive(false);
        PatrolInspection_BackMenu_Text.text = "";
    }
    
    //提交 回退工单
    public void PatrolInspection_BackSubmitBtn()
    {
        JsonData data = new JsonData();
        data["id"] = currentWorkId;
        data["reason"] = PatrolInspection_BackMenu_Text.text;
        Debug.Log(data.ToJson());
        StartCoroutine(_webRequestContrl.UnityWebRequestPost("devopsWorkOrder/rollbackWorkOrder",data, data =>
        {
            Debug.Log("回退订单:" + data);
            JsonData jd = JsonMapper.ToObject(data);
            if (jd["code"].ToString() == "0")
            {
                Close_PatrolInspection_BackMenu();
                CloseWorkOrderMenu();
                StartCoroutine(Open_SaveTipMenu(4));
            }
        }));
    }

    //提交 界面
    public void PatrolIns_SubmitBtnClick()
    {
        PatroIns_SaveAndSubmitContrl(2);
    }

    //保存 界面
    public void PatrolIns_SaveBtnClick()
    {
        PatroIns_SaveAndSubmitContrl(1);
    }

    public void PatroIns_SaveAndSubmitContrl(int index)
    {
        bool isReturn = false;

        foreach (Transform item in WorkOrderMenu_Content.transform)
        {
            if (item.GetComponent<WorkOrderMenuListContrl>())
            {
                if (item.GetComponent<WorkOrderMenuListContrl>().WeiWanCheng_Icon.activeSelf == false && item.GetComponent<WorkOrderMenuListContrl>().WanCheng_Icon.activeSelf == false)
                { 
                    Debug.Log("缺少 完成状态");
                    isReturn = true;
                }

                if (item.GetComponent<WorkOrderMenuListContrl>().WoMenu_InputField.text == "")
                {
                    Debug.Log("缺少 描述");
                    isReturn = true;
                }
            }
        }
        
        if (GameManager.imageUrl2.Count == 0)
        {
            Debug.Log("缺少 图片");
            isReturn = true;
        }
        if (isReturn == true)
        {
            StartCoroutine(Open_SaveTipMenu(3));
            isReturn = false;
            return;
        }

        foreach (Transform item in WorkOrderMenu_Content.transform)
        {
            if (item.GetComponent<WorkOrderMenuListContrl>())
            {
                WorkOrderInfoArray info = new WorkOrderInfoArray();
                info.id = item.GetComponent<WorkOrderMenuListContrl>().id;
                info.handleDesc = item.GetComponent<WorkOrderMenuListContrl>().WoMenu_InputField.text;
                info.handleAttach = string.Join(",", GameManager.imageUrl2.ToArray());
                if (item.GetComponent<WorkOrderMenuListContrl>().WanCheng_Icon.activeSelf == true)
                {
                    info.handleResult = "完成";
                }
                else if (item.GetComponent<WorkOrderMenuListContrl>().WeiWanCheng_Icon.activeSelf == true)
                {
                    info.handleResult = "未完成";
                }
                WorkOrderInfoArrayList.Add(info);
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
            WorkOrderInfoData workOrderInfoData = new WorkOrderInfoData(index, currentWorkId, sWorkOrderInfoArrays);
            string json = JsonMapper.ToJson(workOrderInfoData);
            json = Regex.Unescape(json);
            Debug.Log("当前组成的json:" + json);
            
            StartCoroutine(_webRequestContrl.UnityWebRequestPost("devopsWorkOrder/submitAppTask", json, data =>
            {
                JsonData jd = JsonMapper.ToObject(data);
                if (jd["code"].ToString() == "0")
                {
                    if (index == 2)
                    {
                        Debug.Log("提交工单 成功");
                        StartCoroutine(Open_SaveTipMenu(2));
                        CloseWorkOrderMenu();
                    }
                    else
                    {
                        Debug.Log("保存订单 成功");
                        StartCoroutine(Open_SaveTipMenu(1));
                    }
                }
            }));
            WorkOrderInfoArrayList.Clear();
            GameManager.imageUrl2.Clear();
        }
    }
        
    //截图 保存相册
    public void SavePhotoBtnClick()
    {
        string imageName = GetTimeStampSecond().ToString();

        Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
         ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
        ss.Apply();
        NativeGallery.SaveImageToGallery(ss, "AR巡检", imageName+".png");
        Destroy( ss );
        StartCoroutine(Open_SaveTipMenu(5));
    }
    
    public long GetTimeStampSecond()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        try
        {
            return Convert.ToInt64(ts.TotalSeconds);
        }
        catch (Exception ex)
        {
            Debug.Log($"GetTimeStampSecond Error = {ex}");
            return 0;
        }
    }
}
