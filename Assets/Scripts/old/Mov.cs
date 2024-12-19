using UnityEngine;

public class Mov : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;
    public Vector3 input;
    public Vector3 turn;
    public float speed;
    public float gravity;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject room;

    void Start() {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update() {

        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !animator.IsInTransition(0)) {
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0f);
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            if( pause.active == false){
                pause.SetActive(true);
                room.GetComponent<AudioSource>().Stop();
            }
            else{
                pause.SetActive(false);
                room.GetComponent<AudioSource>().Play();
            }
        }

        input = transform.forward * Input.GetAxis("Vertical");
        if(input != Vector3.zero) {
            animator.SetBool("walking", true);
        }
        else {
            animator.SetBool("walking", false);
        }

        input.y -= gravity * Time.deltaTime;
        turn.Set(0, Input.GetAxis("Horizontal")*0.4f, 0);
        transform.Rotate(turn);

        controller.Move(input * speed * Time.deltaTime);

        if(pause.active==false){
            float mouseY = Input.GetAxis("Mouse Y") * 2f; // Sensibilidade vertical
            float mouseX = Input.GetAxis("Mouse X") * 2f; // Sensibilidade horizontal
            Vector3 currentRotation = Camera.main.transform.localEulerAngles;
            float newRotationX = currentRotation.x - mouseY;
            float newRotationY = currentRotation.y + mouseX;
            newRotationX = (newRotationX > 180) ? newRotationX - 360 : newRotationX;
            newRotationX = Mathf.Clamp(newRotationX, -60f, 60f);
            newRotationY = (newRotationY > 180) ? newRotationY - 360 : newRotationY;
            newRotationY = Mathf.Clamp(newRotationY, -60f, 60f);
            Camera.main.transform.localEulerAngles = new Vector3(newRotationX, newRotationY, currentRotation.z);
        }

    }

}