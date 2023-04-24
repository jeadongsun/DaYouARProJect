using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginMenuContrl : MonoBehaviour
{
    
    
    public UnityWebRequestContrl WebRequestContrl;

    public InputField InputField_UserName;

    public InputField InputField_Pwd;

    public GameObject SaveStateIcon;

    private bool isSave = false;

    public GameObject ErrorText;

    private bool isPassword = false;

    public GameObject HidePwdBtn;

    public GameObject HidePwdBtn2;

    public GameObject Canvas;

    private void Start()
    {
        Application.targetFrameRate = 60;
        if (PlayerPrefs.GetString("UserName") != "" && PlayerPrefs.GetString("UserPwd") != "")
        {
            InputField_UserName.text = PlayerPrefs.GetString("UserName");
            InputField_Pwd.text = PlayerPrefs.GetString("UserPwd");
            LoginBtnClick();
        }
        
        InputField_UserName.text = PlayerPrefs.GetString("UserName");

        // string iP_genneration = UnityEngine.iOS.Device.generation.ToString();
        // if (iP_genneration.Substring(0, 3) == "iPa")
        // {
        //     Debug.Log("苹果平板");
        // }
        // else
        // {
        //     Canvas.GetComponent<CanvasScaler>().matchWidthOrHeight = 0.5f;
        //     GameManager.currentDevice = true;
        //     Debug.Log("苹果手机");
        // }
        
        
        float physicscreen = Mathf.Sqrt(Screen.width * Screen.width + Screen.height * Screen.height) / Screen.dpi;
        if (physicscreen >= 7f)
        {
            Debug.Log("安卓平板");
        }
        else
        {
            GameManager.currentDevice = true;
            Debug.Log("安卓手机");
        }
    }

    //登入按钮
    public void LoginBtnClick()
    {
        string pwd = InputField_UserName.text + "@@" + InputField_Pwd.text;
       StartCoroutine(WebRequestContrl.WebRequestLoginContrl(InputField_UserName.text,pwd, data =>
       {
           if (data == "error")
           {
               ErrorText.SetActive(true);
           }
           else
           {
               if (isSave == true)
               {
                   PlayerPrefs.SetString("UserName",InputField_UserName.text);
                   PlayerPrefs.SetString("UserPwd",InputField_Pwd.text);
               }
               
               ErrorText.SetActive(false);
               JsonData jd = JsonMapper.ToObject(data);
               GameManager.userToken = jd["access_token"].ToString();
               SceneManager.LoadScene("MainMneu");
           }
       }));

        //SceneManager.LoadScene("MainMneu");
    }


    //是否记住密码 
    public void LoginSaveBtnClick()
    {
        if (isSave == false)
        {
            SaveStateIcon.SetActive(true);
            isSave = true;
        }
        else
        {
            SaveStateIcon.SetActive(false);
            isSave = false;
        }
    }
    
    //隐藏显示密码
    public void PasswordHideContrl()
    {
        if (isPassword == false)
        {
            isPassword = true;
            InputField_Pwd.inputType = InputField.InputType.Standard;
            InputField_Pwd.ForceLabelUpdate();
            HidePwdBtn.SetActive(false);
            HidePwdBtn2.SetActive(true);
        }
        else
        {
            isPassword = false;
            InputField_Pwd.inputType = InputField.InputType.Password;
            InputField_Pwd.ForceLabelUpdate();
            HidePwdBtn.SetActive(true);
            HidePwdBtn2.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            InputField_UserName.text = "hzdy_01";
            InputField_Pwd.text = "00000000";
        }
    }
    
    //账号密码 测试按钮
    public void TestLogin()
    {
        InputField_UserName.text = "hzdy_01";
        InputField_Pwd.text = "00000000";
    }
}
