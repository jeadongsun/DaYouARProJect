using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.UI;

public class PatrolProceInfo : MonoBehaviour
{
    public Text Proce_RWBH;

    public Text Proce_RWLX;

    public Text Proce_RWMS;

    public InputField Proce_Input;

    public string id;

    public GameObject MoreBtn;

    public GameObject MoreMenu;

    public Text MoreMenuText;

    private bool isMoreMenu = false;

    public GameObject Proce_PhtoMenu_Content;

    private MainMenuContrl MainMenuContrl;

    private UnityWebRequestContrl _webRequestContrl;
    private void Start()
    {
        MainMenuContrl = GameObject.Find("MainMenuContrl").GetComponent<MainMenuContrl>();
        _webRequestContrl = GameObject.Find("UnityWebRequestContrl").GetComponent<UnityWebRequestContrl>();
    }

    public void MoreMenuContrl()
    {
        if (isMoreMenu == false)
        {
            isMoreMenu = true;
            MoreMenu.SetActive(true);
        }
        else
        {
            isMoreMenu = false;
            MoreMenu.SetActive(false);
        }
    }

    public void PhotoBtnClick()
    {
        // GameObject obj = GameObject.Instantiate(MainMenuContrl.currentRawImg_Prefab, transform.position, transform.rotation);
        // obj.transform.SetParent(Proce_PhtoMenu_Content.transform);
        // obj.transform.localScale = new Vector3(1, 1, 1);
        //
        // GameManager.LoadImage(obj.GetComponent<currentRawImg>().currentImg);
        // obj.GetComponent<currentRawImg>().imgUrl = (Texture2D)obj.GetComponent<currentRawImg>().currentImg.texture;
        // Debug.Log("开始上传图片");
        // StartCoroutine(_webRequestContrl.UnityWebRequestGetImage(GameManager.Sprite2Bytes(obj.GetComponent<currentRawImg>().currentImg),
        //     data =>
        //     {
        //         Debug.Log("上传图片：" + data);
        //         JsonData jd = JsonMapper.ToObject(data);
        //         if (jd["code"].ToString() == "0")
        //         {
        //             JsonData jd2 = jd["data"];
        //             GameManager.imageUrl.Add(jd2["url"].ToString());
        //             obj.GetComponent<currentRawImg>().currentUrl = jd2["url"].ToString();
        //             //string str = string.Join(",", GameManager.imageUrl.ToArray());
        //         }
        //     }));
        
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
                    obj.transform.SetParent(Proce_PhtoMenu_Content.transform);
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
