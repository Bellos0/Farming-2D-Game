using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class ButtonFunc : MonoBehaviour, IUIbutton
{

    public abstract void action1();


    public abstract void action2();


    public abstract void action3();

    public abstract void action4();


    public abstract void action5();


    public void Exit2MainScreen()
    {
        SceneManager.LoadScene("MainScreen");
    }


}
