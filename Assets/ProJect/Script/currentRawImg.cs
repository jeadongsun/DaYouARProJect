using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentRawImg : MonoBehaviour
{
    public RawImage currentImg;

    public Texture2D imgUrl;
    
    public string currentUrl;

    public GameObject TipTop;
    public void DeleteImage()
    {
      //  GameManager.imageUrl.RemoveAt(currentIndex);
        //GameManager.imageIndex--;

        for (int i = 0; i < GameManager.imageUrl.Count; i++)
        {
            if (GameManager.imageUrl[i] == currentUrl)
            {
                GameManager.imageUrl.RemoveAt(i);
            }
        }
        Destroy(gameObject);
    }

    public void OepnSLTClick()
    {
        GameObject.Find("MainMenuContrl").GetComponent<MainMenuContrl>().OepnCurrentSelectImg(currentImg);
    }
}
