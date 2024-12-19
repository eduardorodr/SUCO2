using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class DoorInteraction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.Space; // Tecla para interagir com a porta
    private bool playerInZone = false;        // Controle se o jogador está na área
    private bool dialogosConcluidos = false;  // Verifica se os diálogos foram concluídos

    public void ConcluirDialogos() {
        dialogosConcluidos = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            playerInZone = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            playerInZone = false;
        }
    }

    void Update() {
        if (playerInZone && Input.GetKeyDown(interactionKey)) {
            InteragirComPorta();
        }
    }

    void InteragirComPorta() {
        if (dialogosConcluidos) {
            SceneManager.LoadScene("City");
        } else {
            //
        }
    }
}