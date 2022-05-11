using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour
{
    //public static string staticId;
    //public static string changedStaticId;
    public static bool isMobile;

    public InputField name;
    public static string namestring;
    public InputField email;
    public static string emailstr;
    public InputField city;
    public static string citystr;

    public GameObject selection;
    public Animator anim;
    public Text alreadyPlayed;
    public GameObject focusCopy;

    public Text dataentry;

    /*readonly string exist = "https://www.wearnpaymemorygame.com/wearnpay/sqlconnect/getEmpexist.php";
    readonly string hasPlayed = "https://www.wearnpaymemorygame.com/wearnpay/sqlconnect/getHasplayed.php";
    readonly string register = "https://www.wearnpaymemorygame.com/wearnpay/sqlconnect/registerUser.php";*/

    public string nameCheck;
    private void Start()
    {
        anim = selection.GetComponent<Animator>();


        //anim = GetComponent<Animator>();
    }
    public void SubmitPressed()
    {
        namestring = name.text.ToString();
        emailstr = email.text.ToString();
        citystr = city.text.ToString();
        SceneManager.LoadScene(1);

        //staticId = employeeId.text.ToString();
        //AlteredID();

        //string check = employeeId.text.ToString();

        //StartCoroutine("PlayerExist");
        /*if (check.Contains("@"))
        {
            staticId = employeeId.text.ToString();
            StartCoroutine("CheckHasPlayed");
        }
        else
        {
            alreadyPlayed.enabled = true;
            alreadyPlayed.text = "Please enter correct employeeId.";
            Invoke("DisableText", 2f);
        }*/
    }

    void DisableText()
    {
        alreadyPlayed.enabled = false;
    }

    /*IEnumerator PlayerExist()
    {
        List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
        wwwForm.Add(new MultipartFormDataSection("empcode", changedStaticId));

        UnityWebRequest www = UnityWebRequest.Post(exist, wwwForm);

        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);

        }
        else
        {
            Debug.Log("PlayerExist Value " + www.downloadHandler.text);

            string str = www.downloadHandler.text;
            //string mytext = dataentry.text.ToString();

            if (str == "0")
            {
                StartCoroutine("CheckHasPlayed");
                //StartCoroutine("CallSubmit");
            }
            else
            {
                StartCoroutine("RegisterUser");
                *//*alreadyPlayed.enabled = true;
                alreadyPlayed.text = "Sorry, you have already played.";
                Invoke("DisableText", 2f);*//*
            }

        }
    }*/

    /*private void AlteredID()
    {
        changedStaticId = "xxxx" + staticId.Substring(staticId.Length - 4);
        Debug.Log(changedStaticId);
    }

        IEnumerator CheckHasPlayed()
        {
            List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();
            wwwForm.Add(new MultipartFormDataSection("empcode", changedStaticId));

            UnityWebRequest www = UnityWebRequest.Post(hasPlayed, wwwForm);

            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);

            }
            else
            {
                Debug.Log("HasPlayed Value " + www.downloadHandler.text);

                string str = www.downloadHandler.text;
                //string mytext = dataentry.text.ToString();

                if (str == "0" || str == "1" || str == "2")
                {
                    SceneManager.LoadScene(1);
                    //StartCoroutine("CallSubmit");
                }

                else
                {
                    alreadyPlayed.enabled = true;
                    alreadyPlayed.text = "Sorry, you have already played.";
                    Invoke("DisableText", 2f);
                }

            }

        }*/



        /*IEnumerator RegisterUser()
        {
            List<IMultipartFormSection> wwwForm = new List<IMultipartFormSection>();

            wwwForm.Add(new MultipartFormDataSection("username", name.text.ToString()));
            wwwForm.Add(new MultipartFormDataSection("empcode", staticId));
            wwwForm.Add(new MultipartFormDataSection("time", "0"));
            wwwForm.Add(new MultipartFormDataSection("hasplayed", "0"));

        UnityWebRequest www = UnityWebRequest.Post(register, wwwForm);

            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);

            }
            else
            {
                Debug.Log("Register User Value " + www.downloadHandler.text);

                string str = www.downloadHandler.text;
                //string mytext = dataentry.text.ToString();

                if (str == "0")
                {
                SceneManager.LoadScene(1);
                }
                else
                {
                    Debug.Log("Save Failed ");
                }
            }
        }*/

        public void PC()
        {
            focusCopy.SetActive(false);
            anim.Play("up");
        }

        public void Mobile()
        {
            isMobile = true;
            focusCopy.SetActive(true);
            anim.Play("up");
        } 

    
}