using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //public static string Url = "http://61.241.70.147:8888/console/";
    public static string Url = "http://116.62.124.41:8888/console/";

    public static string userToken = "";
    
    public static string LoginUrl = "http://116.62.124.41:8888/token";

    public static string Url2 = "http://116.62.124.41:8888/console/newkngf/kngfStationFiles/saveOssAttach";
    
    //当前 工单ID
    public static string devopsWorkOrderId = "";

    public static GameObject currentRWObj;

    public static List<string> imageUrl = new List<string>();
    
    public static List<string> imageUrl2 = new List<string>();

    public static int imageIndex = 0;

    public static bool isARScene = false;

    public static bool currentDevice = false;

    //Http url请求 拼接字符串
    public static string DictionaryTostr(Dictionary<string, string> param)
    {
        StringBuilder builder = new StringBuilder();
        foreach (var entry in param)
        {
            if (string.IsNullOrWhiteSpace(Convert.ToString(entry.Key)) || null == entry.Value)
            {
                continue;
            }
            builder.Append(Convert.ToString(entry.Key) + "=" + Convert.ToString(entry.Value) + "&");
        }
        return builder.ToString().Substring(0, builder.ToString().LastIndexOf("&")); ;
    }
    
    public static string DictionaryTostr2(Dictionary<string, byte[]> param)
    {
        StringBuilder builder = new StringBuilder();
        foreach (var entry in param)
        {
            if (string.IsNullOrWhiteSpace(Convert.ToString(entry.Key)) || null == entry.Value)
            {
                continue;
            }
            builder.Append(Convert.ToString(entry.Key) + "=" + entry.Value + "&");
        }
        return builder.ToString().Substring(0, builder.ToString().LastIndexOf("&")); ;
    }

    
    //Text 省略号
    public static void SetTextWithEllipsis(Text textComponent, string value, int characterVisibleCount)
    {
 
        var updatedText = value;
 
        // 判断是否需要过长显示省略号
        if (value.Length > characterVisibleCount)
        {
            updatedText = value.Substring(0, characterVisibleCount - 1);
            updatedText += "…";
        }
 
        // update text
        textComponent.text = updatedText;
    }
    
    //读取图片
    public static void LoadImage(RawImage image)
    {
        string _path = Application.dataPath + "/StreamingAssets/" + "test.jpg";//获取地址

        FileStream _fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read);//使用流数据读取

        byte[] _buffur = new byte[_fileStream.Length];

        _fileStream.Read(_buffur, 0, _buffur.Length);//转换成字节流数据

        Texture2D _texture2d = new Texture2D(220, 220);//设置宽高
        
        _texture2d.LoadImage(_buffur);//将流数据转换成Texture2D

        image.texture = _texture2d;
    }
    
    public static byte[] Sprite2Bytes(RawImage sprite)
    {
        Texture2D tex = sprite.texture as Texture2D;
        return tex.EncodeToPNG();
    }
    
    public static Texture2D DeCompress(Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
            source.width,
            source.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;
        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }
}
