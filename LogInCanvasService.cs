using TMPro;
using UnityEngine;

public class LogInCanvasService : MonoBehaviour
{
    LogInRepo _logIn;
    LogInRepo_Json _logInJson;
    [SerializeField] private TMP_InputField username_Log;
    [SerializeField] private TMP_InputField password_Log;

    [SerializeField] TMP_InputField username_Reg;
    [SerializeField] TMP_InputField password_Reg;

    private void Awake()
    {
        _logIn = new LogInRepo();
        _logInJson = new LogInRepo_Json();
        username_Log.text = string.Empty;
        password_Log.text = string.Empty;
        username_Reg.text = string.Empty;
        password_Reg.text = string.Empty;
    }
    public void ExitBtn()
    {
        Application.Quit();
    }

    public void LoginClick()
    {
        if (ValidateInput(username_Log.text, password_Log.text))
        {
            LoginBtn(username_Log.text, password_Log.text);
        }
        else
        {
            Login_jSON_Btn(username_Log, password_Log);
        }
    }

    public void RegisterClick()
    {
        //if (ValidateInput(username_Reg.text, password_Reg.text))
        //{
        //    RegBtn(username_Reg.text, password_Reg.text);
        //}
        //else
        {
            Reg_JSON_Btn(username_Reg.text, password_Reg.text);
        }
    }

    /// <summary>
    /// bai toan log in su dung file txt don thuan
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public void LoginBtn(string username, string password)
    {

        if (_logIn.ValidatedLogin(username, password))
        {
            Debug.Log("Login successful.");

        }
        else
        {
            Debug.LogError("Invalid username or password.");
        }

    }

    /// <summary>
    /// bai toan dang nhap bang cach doc file json luu data
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public void Login_jSON_Btn(TMP_InputField username, TMP_InputField password)
    {

        if (_logInJson.ValidatedLogin(username.text, password.text))
        {
            Debug.Log("Login Json successful.");

        }
        else
        {
            Debug.LogError("Invalid username or password. Json");
        }

    }


    public bool ValidateInput(string username, string pass)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(pass))
        {
            Debug.LogError("Username or password is empty.");
            return false;
        }
        return true;
    }

    /// <summary>
    /// bai toan dang ky, luu gia tri dang tri vao trong file txt
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public void RegBtn(string username, string password)
    {

        if (_logIn.Register(username, password))
        {
            Debug.Log("Registration successful.");
        }
        else
        {
            Debug.LogError("Registration failed.");
        }

    }

    /// <summary>
    /// bai toan dang ky, luu gia tri dang tri vao trong file json
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    public void Reg_JSON_Btn(string username, string password)
    {

        if (_logInJson.Register(username, password))
        {
            Debug.Log("Registration Json successful.");
        }
        else
        {
            Debug.LogError("Registration Json failed.");
        }

    }
}
