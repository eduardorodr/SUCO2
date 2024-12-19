using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GirlInteraction : MonoBehaviour
{

    [SerializeField] private GameObject diag;

    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject endGame;

    [SerializeField] private GameObject pause;

    [SerializeField] private GameObject op1Btn;
    [SerializeField] private GameObject op2Btn;
    [SerializeField] private GameObject messageTxt;

    private int Hp = 5;

    public CharacterController controller;
    public Animator animator;

    public KeyCode interactionKey = KeyCode.Space; // Tecla de interação
    private bool playerInZone = false;        // Controle se o jogador está na área
    public int currentMessage = 1; // Mensagem inicial
    public int nextMov = 1;

    public Dictionary<int, string> mensagens = new Dictionary<int, string>()
    {

        { 1, "Desculpe te chamar tão tarde, acho que preciso de ajuda..." },
        { 2, "lorem ipsum" },
        { 3, "lorem ipsum" },

        { 4, "Você sabe, já faz um tempo que eu não tenho me sentido eu mesma. As vezes me pergunto se alguma vez sequer eu já senti" },
        { 5, "lorem ipsum" },
        { 6, "lorem ipsum" },

        { 7, "Bom, eu acho que eu precisava de alguém pra conversar, por isso te chamei. Tem sido dificil fazer até as coisas mais básicas, sair da cama é parte mais dificil do meu dia" },
        { 8, "lorem ipsum" },
        { 9, "lorem ipsum" },

        { 10, "Se sair da cama já é dificil, imagine ir ao mercado. Eu acho que não conseguiria fazer isso sozinha" },
        { 11, "lorem ipsum" },
        { 12, "lorem ipsum" },

        { 13, "Você não precisa fazer isso, você é meu amigo, mas eu tenho que andar com as minhas próprias pernas" },
        { 14, "lorem ipsum" },
        { 15, "lorem ipsum" },

        { 16, "No que exatamente sair de casa poderia me fazer bem?" },
        { 17, "lorem ipsum" },
        { 18, "lorem ipsum" },

        { 19, "Você vai mesmo insistir nisso?" },
        { 20, "lorem ipsum" },
        { 21, "lorem ipsum" },

        { 22, "Tudo bem, você está certo... Só me deixa me arrumar, e nós vamos, ok?" },
        { 23, "lorem ipsum" },
        { 24, "lorem ipsum" },

        { 99, "Estou pronta para sair, vamos lá" },

    };

    public Dictionary<int, (string op1, int val1, int next1, string op2, int val2, int next2) > op = new Dictionary <int, (string,int,int,string,int,int)>()
    {
        { 1, ( "A", 1, 2, "B", 0, 3 ) },
        { 4, ( "C", 1, 5, "D", 0, 6 ) },
        { 7, ( "E", 1, 8, "F", 0, 9 ) },
        { 10, ( "G", 1, 11, "H", 0, 12 ) },
        { 13, ( "I", 1, 14, "J", 0, 15 ) },
        { 16, ( "K", 1, 17, "L", 0, 18 ) },
        { 19, ( "M", 0, 20, "N", 0, 21 ) },
        { 22, ( "O", 0, 23, "P", 0, 24 ) },
        { 99, ( "Ok", 0, 99, "...", 0, 99 ) },

        {2, ( "...", 0, 4, "...", 0, 4 ) },
        {3, ( "...", 0, 4, "...", 0, 4 ) },

        {5, ( "...", 0, 7, "...", 0, 7 ) },
        {6, ( "...", 0, 7, "...", 0, 7 ) },

        {8, ( "...", 0, 10, "...", 0, 10 ) },
        {9, ( "...", 0, 10, "...", 0, 10 ) },

        {11, ( "...", 0, 13, "...", 0, 13 ) },
        {12, ( "...", 0, 13, "...", 0, 13 ) },

        {14, ( "...", 0, 16, "...", 0, 16 ) },
        {15, ( "...", 0, 16, "...", 0, 16 ) },

        {17, ( "...", 0, 19, "...", 0, 19 ) },
        {18, ( "...", 0, 19, "...", 0, 19 ) },

        {20, ( "...", 0, 22, "...", 0, 22 ) },
        {21, ( "...", 0, 22, "...", 0, 22 ) },

        {23, ( "...", 0, 99, "...", 0, 99 ) },
        {24, ( "...", 0, 99, "...", 0, 99 ) }

    };

    public Dictionary <int, (float x, float y, Vector3 direction) > mov = new Dictionary <int, (float,float,Vector3)>()
    {
        { 1, ( 333f, -97f, Vector3.forward ) },
        { 2, ( 782f, -93f, Vector3.right ) },
        { 3, ( 782f, 264f, Vector3.forward ) }
    };

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

    void Start(){
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name == "City") Step();
    }

    void Update() {

        if(Hp==0){
            gameOver.SetActive(true);

        }

        if(nextMov==4&&currentMessage==99){
            endGame.SetActive(true);
        }

        if (playerInZone && Input.GetKeyDown(interactionKey)) {
            Interact();
        }

    }

    void Interact()
    {
        if (playerInZone && Input.GetKeyDown(interactionKey))
        {

            if (mensagens.ContainsKey(currentMessage))
            {

                diag.SetActive(true);
                messageTxt.GetComponent<TMPro.TextMeshProUGUI>().text = mensagens[currentMessage];
                op1Btn.GetComponent<TMPro.TextMeshProUGUI>().text = op[currentMessage].op1;
                op2Btn.GetComponent<TMPro.TextMeshProUGUI>().text = op[currentMessage].op2;

                if(currentMessage==99&& SceneManager.GetActiveScene().name == "Room" ){
                    GameObject Door = GameObject.FindWithTag("door");
                    Door.GetComponent<DoorInteraction>().ConcluirDialogos();
                }

            }
        }
    }

    public void Step()
    {
        StartCoroutine(StepRoutine());
    }

    private IEnumerator StepRoutine()
    {
        
        Vector3 targetPosition = new Vector3( mov[nextMov].x, 0, mov[nextMov].y);
        Debug.Log(mov[nextMov].x);
        Debug.Log(mov[nextMov].y);
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(mov[nextMov].direction);

        while (Quaternion.Angle(transform.rotation, lookRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            yield return null;
        }

        animator.SetBool("walking", true);

        while (Vector3.Distance(transform.position, targetPosition) > 3f)
        {
            
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !animator.IsInTransition(0)) {
                animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0f);
            }

            if ( diag.active == true || pause.active == true )
            {
                animator.SetBool("walking", false);
                yield return new WaitUntil(() => diag.active != true && pause.active != true );
                animator.SetBool("walking", true);
            }
            controller.Move(direction * Time.deltaTime * 8);
            yield return null;
        }

        animator.SetBool("walking", false);
        nextMov++;
        if(nextMov<=3) Step();

    }

    public void Op1(){
        diag.SetActive(false);
        currentMessage = op[currentMessage].next1;
        Hp = Hp - op[currentMessage].val1;
    } 

    public void Op2(){
        diag.SetActive(false);
        currentMessage = op[currentMessage].next2; 
        Hp = Hp - op[currentMessage].val2;
    }
}