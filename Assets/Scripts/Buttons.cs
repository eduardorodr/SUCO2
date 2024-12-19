using UnityEngine;
using UnityEngine.SceneManagement; // Import necessário para gerenciar cenas

public class Buttons : MonoBehaviour
{

    [SerializeField] private GameObject help;
    [SerializeField] private GameObject main;

    public void StartBtn(){
        SceneManager.LoadScene("Room"); // Muda para a cena chamada "Level1"
    }

    public void HelpBtn(){
        help.SetActive(true);
    }

    public void ExitHelpBtn(){
        help.SetActive(false);
    }

    public void ExitBtn(){
        Application.Quit();
    }

}
