using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Login bang cach doc file logInfor.txt trong thu muc persistentDataPath cua ung dung, kiem tra username va password co ton tai hay khong
/// </summary>
public class LogInRepo : ILogin
{
    SceneManager sceneManager;
    string filePath = Path.Combine(Application.persistentDataPath, "logInfor.txt"); // tu dong lay duong dan den file logInfor.txt trong thu muc persistentDataPath cua ung dung

    public LogInRepo() { }

    public bool ValidatedLogin(string username, string password)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("khong co thong tin dang nhap");
            return false;
        }

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("Username or password is empty.");
            return false;
        }

        string[] lines = File.ReadAllLines(filePath);

        // duyet tung dong trong file logInfor.txt
        foreach (string line in lines)
        {
            // kiem tra dong nao co toan khoang trang thi bo qua
            if (string.IsNullOrWhiteSpace(line))
                continue;
            string[] part = line.Split('|');
            if (part.Length == 2)
            {
                string storedUsername = part[0].Trim().ToLower();
                string storedPassword = part[1].Trim().ToLower();
                if (username == storedUsername && password == storedPassword)
                {
                    SceneManager.LoadScene("MainScreen");
                    Debug.Log("Login successful.");
                    return true;
                }

            }
        }
        return false;
    }


    public bool Register(string username, string password)
    {
        // kiem tra username va pass da ton tai hay chua
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }

        // kiem tra username da ton tai trong file hay chua
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
            string[] path = line.Split('|');
            if (path.Length == 2)
            {
                if (username.ToLower().Trim() == path[0])
                {
                    Debug.LogError("Username already exists.");
                    return false;
                }
            }
        }
        File.AppendAllText(filePath, $"{username}|{password}" + Environment.NewLine);
        Debug.Log("Registration successful.");
        return true;

    }

}

