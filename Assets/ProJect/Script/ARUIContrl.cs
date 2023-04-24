using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using LitJson;

public class ARUIContrl : MonoBehaviour
{
   public GameObject UIBg;

   public GameObject[] MenuItems;

   public GameObject CurvMenu;

   public GameObject CurvMenuBg;

   public GameObject CurvMenuCloseBtn;

   public ARContrl ARContrls;

   //设备运行状态
   public Text EquStateText;

   //温度
   public Text TemperatureText;

   //告警信息
   public Text AlertText;

   //设备名称
   public Text WO_SBMC_TextValue;

   //设备编号
   public Text WO_SBBH_TextValue;

   //关联工单
   public Text WO_GLDH_TextValue;

   //日发电量
   public Text WO_RCFD_TextValue;

   //累计发电量
   public Text WO_LJFD_TextValue;

   //实时功率
   public Text WO_SSGL_TextValue;

   //告警 图标
   public Image AlertIcon;

   //告警4种图标
   public Sprite[] AlertIconItem;

   //运行正常 图标
   public Image EquipmentIcon;

   //运行正常 2种图标
   public Sprite[] EquipmentIconItem;
   
   //更多告警信息
   public GameObject AlertMoreMenu;
   
   //更多告警信息 背景
   public GameObject AlertMoreMenu_Bg1;

   public GameObject AlertMoreMenuItem;

   public GameObject AlertMoreMenu_Content;

   public string dwoId;

   public string workOrderNo;

   public GameObject CLGDBtn;

   private void Start()
   {
      InitUI();

      ARContrls = GameObject.Find("ARContrl").GetComponent<ARContrl>();


      if (GameManager.isARScene == true)
      {
         CLGDBtn.SetActive(true);
      }
      else
      {
         CLGDBtn.SetActive(false);
      }
   }


   public void InitUI()
   {
      UIBg.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 620), 0, false);
      UIBg.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-494, 0), 0, false);

      foreach (var item in MenuItems)
      {
         item.GetComponent<CanvasScaler>().transform.DOScale(new Vector3(0, 0, 0), 0);
      }

      CurvMenuBg.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 620), 0, false);
      CurvMenuBg.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-501, 0), 0, false);

   }

   public IEnumerator PlayUIAni()
   {
      UIBg.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1006, 620), 0.5f, false).SetEase(Ease.Linear);
      UIBg.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f, false).SetEase(Ease.Linear)
         .OnComplete(
            delegate { });
      foreach (var item in MenuItems)
      {
         yield return new WaitForSeconds(0.1f);
         item.GetComponent<CanvasScaler>().transform.DOScale(new Vector3(1, 1, 1), 0.5f);
      }


      Dictionary<string, string> str = new Dictionary<string, string>();
      str.Add("ceDevId", "213704245111685123");

      StartCoroutine(ARContrls._webRequestContrl.WebRquestGetContrl("mr/getAREquipmentMsg",
         "?" + GameManager.DictionaryTostr(str), data =>
         {
            Debug.Log("获取设备基本信息:" + data);

            JsonData jd = JsonMapper.ToObject(data);
            JsonData jd2 = jd["data"];

            if (jd["code"].ToString() == "0")
            {
               EquStateText.text = jd2["status"].ToString();
               switch (jd2["status"].ToString())
               {
                  case "运行正常":
                     EquipmentIcon.sprite = EquipmentIconItem[0];
                     EquStateText.color = new Color(24 / 255f, 144 / 255f, 255 / 255f, 255f / 255f);
                     break;
                  case "离线状态":
                     EquipmentIcon.sprite = EquipmentIconItem[1];
                     EquStateText.color = new Color(137 / 255f, 151 / 255f, 176 / 255f, 255f / 255f);
                     break;
               }

               TemperatureText.text = "温度: " + jd2["genWattTempivt"].ToString() + "°C";
               AlertText.text = jd2["graveLevel"].ToString();
               switch (jd2["graveLevel"].ToString())
               {
                  case "严重告警":
                     AlertIcon.sprite = AlertIconItem[0];
                     AlertText.GetComponent<Text>().color = new Color(255f / 255f, 93f / 255f, 93f / 255f, 255f / 255f);
                     break;
                  case "轻微告警":
                     AlertIcon.sprite = AlertIconItem[1];
                     AlertText.GetComponent<Text>().color = new Color(127 / 255f, 209 / 255f, 91 / 255f, 255f / 255f);
                     break;
                  case "普通告警":
                     AlertIcon.sprite = AlertIconItem[3];
                     AlertText.GetComponent<Text>().color =
                        new Color(250f / 255f, 173f / 255f, 20f / 255f, 255f / 255f);
                     break;
                  case "未告警":
                     AlertIcon.sprite = AlertIconItem[2];
                     AlertText.GetComponent<Text>().color = new Color(137 / 255f, 151 / 255f, 176 / 255f, 255f / 255f);
                     break;
               }

               WO_SBMC_TextValue.text = jd2["ceResName"].ToString();
               WO_SBBH_TextValue.text = jd2["deviceId"].ToString();
               WO_GLDH_TextValue.text = jd2["workOrderNo"].ToString();
               WO_RCFD_TextValue.text = jd2["genWattPaetCd"].ToString();
               WO_LJFD_TextValue.text = jd2["genWattPaet"].ToString();
               WO_SSGL_TextValue.text = jd2["genWattAcpt"].ToString();
               
               dwoId = jd2["dwoId"].ToString();
               workOrderNo = jd2["workOrderNo"].ToString();
            }
         }));
   }
   
   //打开 历史告警信息列表
   public void Open_AlertMoreMenu()
   {
      
      Dictionary<string, string> str = new Dictionary<string, string>();
      str.Add("ceDevId", "213704245111685123");
      
      StartCoroutine(ARContrls._webRequestContrl.WebRquestGetContrl("mr/getSoeResponses",
         "?" + GameManager.DictionaryTostr(str), data =>
         {
            Debug.Log("获取告警信息列表:" + data);

            JsonData jd = JsonMapper.ToObject(data);
            JsonData jd2 = jd["data"];

            if (jd["code"].ToString() == "0")
            {
               for (int i = 0; i < jd2.Count; i++)
               {
                  GameObject obj = GameObject.Instantiate(ARContrls.AlertMenuItemPrefab, transform.position,
                     transform.rotation);
                  
                  obj.transform.SetParent(AlertMoreMenu_Content.transform);
                  obj.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

                  obj.GetComponent<AlertMenuItemContrl>().AlertName.text = jd2[i]["soeTitle"].ToString();
                  
                  obj.GetComponent<AlertMenuItemContrl>().AlertXQ.text = jd2[i]["soeDesc"].ToString();
                  obj.GetComponent<AlertMenuItemContrl>().AlertTime.text = jd2[i]["soeGenTime"].ToString();

                  switch (jd2[i]["graveLevel"].ToString())
                  {
                     case "普通告警":
                        obj.GetComponent<AlertMenuItemContrl>().AlertLevel.text = "普通";
                        obj.GetComponent<AlertMenuItemContrl>().AlertLevelIcon.sprite =
                           ARContrls.AlertMoreMenu_IconItem[1];
                        
                        obj.GetComponent<AlertMenuItemContrl>().AlertLevel.color = new Color(250/255f,173/255f,20/255f,255/255);
                        break;
                     case "严重告警":
                        obj.GetComponent<AlertMenuItemContrl>().AlertLevel.text = "严重";
                        obj.GetComponent<AlertMenuItemContrl>().AlertLevelIcon.sprite =
                           ARContrls.AlertMoreMenu_IconItem[0];

                        obj.GetComponent<AlertMenuItemContrl>().AlertLevel.color = new Color(255/255,77/255,79/255,255/255);
                        break;
                     case "轻微告警":
                        obj.GetComponent<AlertMenuItemContrl>().AlertLevel.text = "轻微";
                        obj.GetComponent<AlertMenuItemContrl>().AlertLevelIcon.sprite =
                           ARContrls.AlertMoreMenu_IconItem[2];
                        
                        obj.GetComponent<AlertMenuItemContrl>().AlertLevel.color = new Color(82/255f,196/255f,26/255f,255/255);
                        break;
                  }
               }
            }
         }));
      
      StartCoroutine(CloseMenuAni(data =>
      {
         AlertMoreMenu.SetActive(true);
         
         AlertMoreMenu_Bg1.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1008, 620), 0.5f, false);
         AlertMoreMenu_Bg1.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f, false).OnComplete(delegate
         {
            AlertMoreMenuItem.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.Linear);
         });
      }));
   }
   //关闭 历史告警信息列表
   public void Close_AlertMoreMenu()
   {
      foreach (Transform item in AlertMoreMenu_Content.transform)
      {
         Destroy(item.gameObject);
         Resources.UnloadUnusedAssets();
      }
      
      AlertMoreMenuItem.transform.DOScale(new Vector3(0, 0, 0), 0.3f).SetEase(Ease.Linear);
      AlertMoreMenu_Bg1.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 620), 0.5f, false);
      AlertMoreMenu_Bg1.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-501, 0), 0.5f, false).OnComplete(delegate
      {
         StartCoroutine("PlayUIAni");
      });
   }

   //打开 曲线图
   public void Open_CurvMenu()
   {
      // StartCoroutine(CloseMenuAni(data =>
      // {
      //    CurvMenu.SetActive(true);
      //    CurvMenuBg.GetComponent<RectTransform>().DOSizeDelta(new Vector2(1008, 620), 0.5f, false);
      //    CurvMenuBg.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 0.5f, false).OnComplete(delegate
      //    {
      //       CurvMenuCloseBtn.SetActive(true);
      //    });
      // }));
      //
      // Dictionary<string, string> str = new Dictionary<string, string>();
      // str.Add("ceDevId", "213704245111685123");
      //
      // StartCoroutine(ARContrls._webRequestContrl.WebRquestGetContrl("mr/getPowerCurve",
      //    "?" + GameManager.DictionaryTostr(str), data =>
      //    {  
      //       Debug.Log("获取总有功功率曲线:" + data);
      //       
      //       JsonData jd = JsonMapper.ToObject(data);
      //       JsonData jd2 = jd["data"];
      //
      //    }));
   }

   //关闭 曲线图
   public void CloseOpen_CurvMenu()
   {
      CurvMenuCloseBtn.SetActive(false);
      CurvMenuBg.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 620), 0.5f, false);
      CurvMenuBg.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-501, 0), 0.5f, false).OnComplete(delegate
      {
         StartCoroutine("PlayUIAni");
      });
   }

   //关闭 主面板动画
   public IEnumerator CloseMenuAni(System.Action<string> str)
   {
      foreach (var item in MenuItems)
      {
         yield return new WaitForSeconds(0.1f);
         item.GetComponent<CanvasScaler>().transform.DOScale(new Vector3(0f, 0, 0), 0.5f);
      }

      yield return new WaitForSeconds(0.2f);

      UIBg.GetComponent<RectTransform>().DOSizeDelta(new Vector2(0, 620), 0.5f, false).SetEase(Ease.Linear);
      UIBg.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-494, 0), 0.5f, false).SetEase(Ease.Linear).OnComplete(
         delegate { });

      yield return new WaitForSeconds(0.5f);
      str("end");
   }

   //打开 工单界面 
   public void Open_WorkOrderMenu()
   {
      Dictionary<string, string> str = new Dictionary<string, string>();
      str.Add("id",dwoId);

      StartCoroutine(ARContrls._webRequestContrl.WebRquestGetContrl("devopsWorkOrder/get",
         "?" + GameManager.DictionaryTostr(str), data =>
         {
            Debug.Log("工单基本信息:" + data);
            JsonData jd = JsonMapper.ToObject(data);
            JsonData jd2 = jd["data"];
            if (jd["code"].ToString() == "0")
            {
               Debug.Log(jd["data"].ToString());
               ARContrls.WoMenu_GDBH.text = jd2["workOrderNo"].ToString();
               ARContrls.WoMenu_YHMC.text = jd2["ceCustName"].ToString();
               ARContrls.WoMenu_GDMS.text = jd2["workOrderDesc"].ToString();
               ARContrls.WoMenu_CLRY.text = jd2["workOrderContacter"].ToString();
               ARContrls.WoMenu_CLRQ.text = jd2["submitTime"].ToString();
               ARContrls.WoMenu_CJR.text = jd2["creator"].ToString();
               ARContrls.WoMenu_CJSJ.text = jd2["gmtCreate"].ToString();
               
               ARContrls.WorkOrderMenu.SetActive(true);
               
               
               Dictionary<string, string> str2= new Dictionary<string, string>();
               str2.Add("workNo", workOrderNo);

               ARContrls.currentWorkId = dwoId;

               StartCoroutine(ARContrls._webRequestContrl.WebRquestGetContrl("devopsWorkOrder/listTaskByWorkNo",
                  "?" + GameManager.DictionaryTostr(str2),
                  data =>
                  {
                     Debug.Log("获取工单任务列表:" + data);
                     JsonData jd = JsonMapper.ToObject(data);
                     if (jd["code"].ToString() == "0")
                     {
                        for (int i = 0; i < jd["data"].Count; i++)
                        {
                           GameObject obj2 = GameObject.Instantiate(ARContrls.WorkOrderMenuList_Prefab, transform.position, transform.rotation);
                           obj2.transform.SetParent(ARContrls.WorkOrderMenu_Content.transform);
                           obj2.transform.localScale = new Vector3(1, 1, 1);
                           obj2.transform.localPosition = new Vector3(0, 0, 0);
                           obj2.transform.localEulerAngles = new Vector3(0, 0, 0);
                           
                           obj2.GetComponent<WorkOrderMenuListContrl>().WoMenu_SBDX.text = jd["data"][i]["ceDeviceName"].ToString();
                           obj2.GetComponent<WorkOrderMenuListContrl>().WoMenu_QXLX.text = jd["data"][i]["taskSubtypeName"].ToString();
                           obj2.GetComponent<WorkOrderMenuListContrl>().WoMenu_QXMS.text = jd["data"][i]["taskDesc"].ToString();
                           obj2.GetComponent<WorkOrderMenuListContrl>().WoMenu_RWBH.text = jd["data"][i]["taskNo"].ToString();
                           obj2.GetComponent<WorkOrderMenuListContrl>().WoMenu_State_Text.text = jd["data"][i]["taskLevelName"].ToString();
                           obj2.GetComponent<WorkOrderMenuListContrl>().id =  jd["data"][i]["id"].ToString();
                           if (((IDictionary)jd["data"][i]).Contains("handleDesc"))
                           {
                              obj2.GetComponent<WorkOrderMenuListContrl>().WoMenu_InputField.text = jd["data"][i]["handleDesc"].ToString();
                           }
                           if (((IDictionary)jd["data"][i]).Contains("handleResult"))
                           {
                              switch (jd["data"][i]["handleResult"].ToString())
                              {
                                 case  "完成":
                                    obj2.GetComponent<WorkOrderMenuListContrl>().WanChengBtn();
                                    break;
                                 case  "未完成":
                                    obj2.GetComponent<WorkOrderMenuListContrl>().WeiWanChengBtn();
                                    break;
                              }
                             
                           }
                           
                           switch (jd["data"][i]["taskLevelName"].ToString())
                           {
                              case "紧急":
                                 obj2.GetComponent<WorkOrderMenuListContrl>().WoMenu_State_Text.color = new Color(255/255,93/255f,93/255f,255/255);
                                 obj2.GetComponent<WorkOrderMenuListContrl>().WoMenu_State_Image.sprite =
                                    ARContrls.WorkOrderMenuList_IconState[1];
                                 break;
                              case "重要":
                                 obj2.GetComponent<WorkOrderMenuListContrl>().WoMenu_State_Text.color = new Color(250/255f,173/255f,20/255f,255/255);
                                 obj2.GetComponent<WorkOrderMenuListContrl>().WoMenu_State_Image.sprite =
                                    ARContrls.WorkOrderMenuList_IconState[2];
                                 break;
                              case "一般":
                                 obj2.GetComponent<WorkOrderMenuListContrl>().WoMenu_State_Text.color = new Color(82/255f,196/255f,26/255f,255/255);
                                 obj2.GetComponent<WorkOrderMenuListContrl>().WoMenu_State_Image.sprite =
                                    ARContrls.WorkOrderMenuList_IconState[0];
                                 break;
                           }
                        }
                     }
                  }));
            }
         }));
   }


   public void Update()
   {
      if (Input.GetKeyDown(KeyCode.A))
      {
         StartCoroutine("PlayUIAni");
      }

      if (Input.GetKeyDown(KeyCode.B))
      {
         InitUI();
      }
   }
}
