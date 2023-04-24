using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using System;
using System.Text;
using UnityEngine.UI;
public class UnityWebRequestContrl : MonoBehaviour
{

    //private string userName = "knjtadmin";
    //private string pwd = "knjtadmin@@00000000";
    public IEnumerator WebRequestLoginContrl(string username,string password,System.Action<string> str)
    {
        //WWWForm form = new WWWForm();
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();

       // form.AddField("grant_type", "password");
        //form.AddField("username", userName);
       // form.AddField("password", pwd);
       // form.AddField("scope", "all");
       form.Add(new MultipartFormDataSection("grant_type","password"));
       form.Add(new MultipartFormDataSection("username",username));
       form.Add(new MultipartFormDataSection("password",password));
       form.Add(new MultipartFormDataSection("scope","all"));
        

        using (UnityWebRequest www = UnityWebRequest.Post(GameManager.LoginUrl, form))
        { 
            www.SetRequestHeader("Authorization","Basic aGFycnk6MTIzNDU2");
            //www.SetRequestHeader("Content-Type","application/json;charset=UTF-8");
            yield return www.SendWebRequest();

            
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                str("error");
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                str(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator WebRquestGetContrl(string thisURL,string parameter,System.Action<string> data)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(GameManager.Url + thisURL + parameter, "GET"))
        {
            webRequest.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Authorization",  "bearer " + GameManager.userToken);//请求头文件内容
            yield return webRequest.Send();
            if (webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                data(webRequest.downloadHandler.text);
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
    }
    public IEnumerator UnityWebRequestPost(string url, JsonData data,Action<string> onPostCallback)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(GameManager.Url + url, "POST"))
        {
            byte[] postBytes = System.Text.Encoding.Default.GetBytes(data.ToJson());
            webRequest.uploadHandler = new UploadHandlerRaw(postBytes);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json"); 
            webRequest.SetRequestHeader("Authorization",  "bearer " + GameManager.userToken);
            yield return webRequest.SendWebRequest();
            string response = null;
            if (webRequest.isNetworkError || webRequest.isHttpError )
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                onPostCallback(webRequest.downloadHandler.text);
            }
        }
    }

    public IEnumerator UnityWebRequestGetImage(byte[] bt, Action<string> onPostCallback)
    {
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", bt);
        WWW www = new WWW(GameManager.Url2, form);
        yield return www;
        if (www.isDone && www.error == null)
        {
            onPostCallback(www.text);
        }
        else
        {
            Debug.Log(www.error);
        }
    }
    
    public IEnumerator LoadImage(string url,GameObject obj)
    {
        using var request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.result);
            Debug.Log(request.error);
        }
        else
        {
            var texture = DownloadHandlerTexture.GetContent(request);
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
          
            obj.GetComponent<Image>().sprite = sprite;
        }
    }
}
