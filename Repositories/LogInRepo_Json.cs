using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LogInRepo_Json : ILogin
{
    string filepath = Path.Combine(Application.persistentDataPath, "logInfor.json");

    public LogInRepo_Json() { }

    public bool Register(string username, string password)
    {
        // tao danh sach tai khoan de luu cac tai khoan da ton tai trong file json
        AccountModels.AccountList accountList = new AccountModels.AccountList();

        if (File.Exists(filepath))
        {
            // doc toan bo cac gia tri duoc luu trng json file
            string jsonSTR = File.ReadAllText(filepath);

            // luu cac gia tri vao trong accountlist de kiem tra co trung username khong
            accountList = JsonUtility.FromJson<AccountModels.AccountList>(jsonSTR);

            // kiem tra username co bi trung khong
            foreach (AccountModels.Account account in accountList.accounts)
            {
                if (username.Trim().ToLower() == account.username.ToLower().Trim())
                {
                    Debug.LogError("Username da ton tai");
                    return false;
                }
            }
        }
        else
        {
            File.Create(filepath).Dispose();
        }


        // tao tai khoan moi
        AccountModels.Account newAccount = new AccountModels.Account();
        newAccount.username = username.Trim().ToLower();
        newAccount.password = password.Trim().ToLower();
        accountList.accounts.Add(newAccount);

        // convert  sang file json
        string _convertJson = JsonUtility.ToJson(accountList, true);

        //5. ghi de chuoi json vao file json da co

        File.WriteAllText(filepath, _convertJson);

        Debug.Log("register thanh cong");
        return true;
    }

    public bool ValidatedLogin(string username, string password)
    {
        if (!File.Exists(filepath))
        {
            Debug.LogError("No login information found.");
            return false;
        }

        // 1. doc toan bo chuoi trong file json
        string jsonSTR = File.ReadAllText(filepath);

        //2. chuyen chuoi json sang object trong C# (deserialize)
        // day toan bo data trong file json vao list accountList trong model cua AccountModels
        AccountModels.AccountList accountList = JsonUtility.FromJson<AccountModels.AccountList>(jsonSTR);
        if (accountList == null)
        {
            Debug.LogError("chua ton tai tai khoan nao duoc luu tren he thong");
            return false;
        }

        // duyet cac phan tu dong accountList.accounts de kiem tra username va password co trung khop hay khong

        foreach (AccountModels.Account account in accountList.accounts)
        {
            if (username == account.username.ToLower().Trim() && password == account.password.ToLower().Trim())
            {
                SceneManager.LoadScene("MainScreen");
                return true;
            }
        }
        Debug.LogError("username hoac password khong dung");
        return true;
    }
}
