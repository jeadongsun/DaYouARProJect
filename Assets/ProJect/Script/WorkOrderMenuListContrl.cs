using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class WorkOrderMenuListContrl : MonoBehaviour
{
    //设备对象
    public Text WoMenu_SBDX;
    
    //缺陷类型
    public Text WoMenu_QXLX;
    
    //缺陷描述
    public Text WoMenu_QXMS;
    
    //处理结果 完成
    public GameObject WanCheng_Icon;
    
    //处理结果 未完成
    public GameObject WeiWanCheng_Icon;

    public InputField WoMenu_InputField;

    public Text WoMenu_RWBH;

    public Text WoMenu_State_Text;

    public Image WoMenu_State_Image;

    public GameObject PhotoContent;

    private ARContrl _arContrl;

    private UnityWebRequestContrl _unityWebRequestContrl;

    public string id;

    private void Start()
    {
        _arContrl = GameObject.Find("ARContrl").GetComponent<ARContrl>();
        _unityWebRequestContrl = GameObject.Find("UnityWebRequestContrl").GetComponent<UnityWebRequestContrl>();
    }

    public void WanChengBtn()
    {
        WanCheng_Icon.SetActive(true);
        WeiWanCheng_Icon.SetActive(false);
    }


    public void WeiWanChengBtn()
    {
        WanCheng_Icon.SetActive(false);
        WeiWanCheng_Icon.SetActive(true);
    }
    
    
    public void PhotoBtnClick()
    {
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
        
                    GameObject obj = GameObject.Instantiate(_arContrl.currentRawImgPrefab, transform.position, transform.rotation);
                    obj.transform.SetParent(PhotoContent.transform);
                    obj.transform.localScale = new Vector3(1, 1, 1);
        
                    obj.GetComponent<currentRawImg>().currentImg.texture = texture;
                    
                    StartCoroutine(_unityWebRequestContrl.UnityWebRequestGetImage(GameManager.DeCompress(texture).EncodeToPNG(),
                        data =>
                        {
                            Debug.Log("上传图片：" + data);
                            JsonData jd = JsonMapper.ToObject(data);
                            if (jd["code"].ToString() == "0")
                            {
                                JsonData jd2 = jd["data"];
                                GameManager.imageUrl2.Add(jd2["url"].ToString());
                                obj.GetComponent<currentRawImg>().currentUrl = jd2["url"].ToString();
                                obj.GetComponent<currentRawImg>().TipTop.SetActive(false);
                            }
                        }));
                }
            }
        );
    }
    
}
