using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    private  List<GameObject> open = new List<GameObject>();
    private void Start()
    {
        ShowMenu(mainMenu);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
    public void ShowMenu(GameObject m)
    {
        m.SetActive(true);
        open.Add(m);
    }
    public void HideOpen()
    {
        foreach (GameObject o in open)
        {
            o.SetActive(false);
        }
        open.Clear();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
