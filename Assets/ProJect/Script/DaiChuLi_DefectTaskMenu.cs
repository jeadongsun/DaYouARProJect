using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;
public class DaiChuLi_DefectTaskMenu : MonoBehaviour
{
   public Text Proce_RWBH;

   public Text Proce_QXLX;

   public GameObject[] Proce_JJState;

   public Text Proce_RWMS;

   public GameObject WanChengBtn_Icon;

   public GameObject WeiWanChengBtn_Icon;

   public InputField Proce_Input;

   public string id;

   public GameObject MoreBtn;

   public GameObject MoreMenu;

   public Text MoreMenuText;

   public bool isMoreState = false;

   public GameObject Content;
   
   private MainMenuContrl MainMenuContrl;

   private UnityWebRequestContrl _webRequestContrl;

   public Text Proce_SBDX;
   private void Start()
   {
      MainMenuContrl = GameObject.Find("MainMenuContrl").GetComponent<MainMenuContrl>();
      _webRequestContrl = GameObject.Find("UnityWebRequestContrl").GetComponent<UnityWebRequestContrl>();
   }
   public void WanChengBtnClick()
   {
      WanChengBtn_Icon.SetActive(true);
      WeiWanChengBtn_Icon.SetActive(false);
   }

   public void WeiWanChengBtnClick()
   {
      WanChengBtn_Icon.SetActive(false);
      WeiWanChengBtn_Icon.SetActive(true);
   }

   public void MoreBtnClick()
   {
      if (isMoreState == false)
      {
         isMoreState = true;
         MoreMenu.SetActive(true);
      }
      else
      {
         isMoreState = false;
         MoreMenu.SetActive(false);
      }
   }
   public void PhotoBtnClick()
   {
      // GameObject obj = GameObject.Instantiate(MainMenuContrl.currentRawImg_Prefab, transform.position, transform.rotation);
      // obj.transform.SetParent(Content.transform);
      // obj.transform.localScale = new Vector3(1, 1, 1);
      //   
      // GameManager.LoadImage(obj.GetComponent<currentRawImg>().currentImg);
      // obj.GetComponent<currentRawImg>().imgUrl = (Texture2D)obj.GetComponent<currentRawImg>().currentImg.texture;
      //   
      // StartCoroutine(_webRequestContrl.UnityWebRequestGetImage(GameManager.Sprite2Bytes(obj.GetComponent<currentRawImg>().currentImg),
      //    data =>
      //    {
      //       Debug.Log("上传图片：" + data);
      //       JsonData jd = JsonMapper.ToObject(data);
      //       if (jd["code"].ToString() == "0")
      //       {
      //          JsonData jd2 = jd["data"];
      //          GameManager.imageUrl.Add(jd2["url"].ToString());
      //          obj.GetComponent<currentRawImg>().currentUrl = jd2["url"].ToString();
      //          //string str = string.Join(",", GameManager.imageUrl.ToArray());
      //       }
      //    }));
      
      NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
         {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
               // 此Action为选取图片后的回调，返回一个Texture2D 
               Texture2D texture = NativeGallery.LoadImageAtPath(path, -1);
               if (texture == null)
               {
                  Debug.Log("Couldn't load texture from " + path);
                  return;
               }
               Debug.Log(texture.name);
        
               GameObject obj = GameObject.Instantiate(MainMenuContrl.currentRawImg_Prefab, transform.position, transform.rotation);
               obj.transform.SetParent(Content.transform);
               obj.transform.localScale = new Vector3(1, 1, 1);
        
               obj.GetComponent<currentRawImg>().currentImg.texture = texture;
                    
               StartCoroutine(_webRequestContrl.UnityWebRequestGetImage(GameManager.DeCompress(texture).EncodeToPNG(),
                  data =>
                  {
                     Debug.Log("上传图片：" + data);
                     JsonData jd = JsonMapper.ToObject(data);
                     if (jd["code"].ToString() == "0")
                     {
                        JsonData jd2 = jd["data"];
                        GameManager.imageUrl.Add(jd2["url"].ToString());
                        obj.GetComponent<currentRawImg>().currentUrl = jd2["url"].ToString();
                        obj.GetComponent<currentRawImg>().TipTop.SetActive(false);
                     }
                  }));
            }
         }
      );
   }
}

