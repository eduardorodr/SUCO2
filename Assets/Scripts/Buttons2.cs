using UnityEngine;
using UnityEngine.SceneManagement; // Import necessário para gerenciar cenas

public class Buttons2 : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject room;

    public void QuitGame(){
        SceneManager.LoadScene("Menu"); // Muda para a cena chamada "Menu"
    }

    public void Return(){
        pause.SetActive(false);
        room.GetComponent<AudioSource>().Play();
    }

}