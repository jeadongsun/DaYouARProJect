using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testPhotoContrl : MonoBehaviour
{
    public RawImage headIcon;


    public void TestClick()
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

                    headIcon.texture = texture;
                }
            }
        );
    }
}
