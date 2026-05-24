using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    public SpriteRenderer[] images; // Arreglo que contiene las imágenes que se desplazarán
    public GameObject[] imagenes;
    public Animator[] animators;
    public Animation botDerAnim, botIzqAnim;
    public Button leftButton; // Botón izquierdo
    public Button rightButton; // Botón derecho
    [SerializeField] private int currentIndex = 0 ; // Índice de la imagen actualmente mostrada
    private bool isAnimating = false; // Variable para controlar si se está ejecutando una animación
    public int distancia;
    public float delay;
    int puntos;

    void Start()
    {
        //puntos = PlayerPrefs.GetInt("mates");
        animators[0].SetBool("estaCentro", true);
        leftButton.onClick.AddListener(OnLeftButtonClick);
        rightButton.onClick.AddListener(OnRightButtonClick);
    }

    void Update()
    {
        //Debug.Log(puntos);
        if (!isAnimating) // Solo permitir presionar las teclas si no se está ejecutando una animación
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnRightButtonClick();
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OnLeftButtonClick();
            }
        }
    }

    void OnRightButtonClick()
    {
        if (!isAnimating) // Evitar iniciar una nueva animación si ya está en curso una
        {
            isAnimating = true; // Marcar que se está ejecutando una animación
            switch (currentIndex)
            {
                case 0:
                    animators[currentIndex].SetBool("estaCentro", false);
                    LeanTween.moveX(images[currentIndex].gameObject, distancia, 0.5f)
                    .setDelay(1f)
                    .setEase(LeanTweenType.easeInOutQuad);
                    currentIndex = 1;
                    Invoke("hola", delay);
                    LeanTween.moveX(images[currentIndex].gameObject, 0, 0.5f)
                    .setDelay(1f)
                    .setEase(LeanTweenType.easeInOutQuad)
                    .setOnComplete(AnimationComplete);
                    break;
                case 1:
                    animators[currentIndex].SetBool("estaCentro", false);
                    LeanTween.moveX(images[currentIndex].gameObject, distancia, 0.5f)
                    .setDelay(1f)
                    .setEase(LeanTweenType.easeInOutQuad);
                    currentIndex = 2;
                    Invoke("hola", delay);
                    LeanTween.moveX(images[currentIndex].gameObject, 0, 0.5f)
                    .setDelay(1f)
                    .setEase(LeanTweenType.easeInOutQuad)
                    .setOnComplete(AnimationComplete);
                    break;
                case 2:
                    animators[currentIndex].SetBool("estaCentro", false);
                    LeanTween.moveX(images[currentIndex].gameObject, distancia, 0.5f)
                    .setDelay(1f)
                    .setEase(LeanTweenType.easeInOutQuad);
                    currentIndex = 0;
                    Invoke("hola", delay);
                    LeanTween.moveX(images[currentIndex].gameObject, 0, 0.5f)
                    .setDelay(1f)
                    .setEase(LeanTweenType.easeInOutQuad)
                    .setOnComplete(AnimationComplete);
                    break;
            }
            float posX = -distancia;
            float posY = imagenes[currentIndex].transform.position.y;
            Vector2 newPos = new Vector2(posX, posY);
            imagenes[currentIndex].transform.position = newPos;
        }
    }

    void OnLeftButtonClick()
    {
        if (!isAnimating) // Evitar iniciar una nueva animación si ya está en curso una
        {
            isAnimating = true; // Marcar que se está ejecutando una animación
            switch (currentIndex)
            {
                case 0:
                    animators[currentIndex].SetBool("estaCentro", false);
                    LeanTween.moveX(images[currentIndex].gameObject, -distancia, 0.5f)
                    .setDelay(1f)
                    .setEase(LeanTweenType.easeInOutQuad);
                    currentIndex = 2;
                    Invoke("hola", delay);
                    LeanTween.moveX(images[currentIndex].gameObject, 0, 0.5f)
                    .setDelay(1f)
                    .setEase(LeanTweenType.easeInOutQuad)
                    .setOnComplete(AnimationComplete);
                    break;
                case 1:
                    animators[currentIndex].SetBool("estaCentro", false);
                    LeanTween.moveX(images[currentIndex].gameObject, -distancia, 0.5f)
                    .setDelay(1f)
                    .setEase(LeanTweenType.easeInOutQuad);
                    currentIndex = 0;
                    Invoke("hola", delay);
                    LeanTween.moveX(images[currentIndex].gameObject, 0, 0.5f)
                    .setDelay(1f)
                    .setEase(LeanTweenType.easeInOutQuad)
                    .setOnComplete(AnimationComplete);
                    break;
                case 2:
                    animators[currentIndex].SetBool("estaCentro", false);
                    LeanTween.moveX(images[currentIndex].gameObject, -distancia, 0.5f)
                    .setDelay(1f)
                    .setEase(LeanTweenType.easeInOutQuad);
                    currentIndex = 1;
                    Invoke("hola", delay);
                    LeanTween.moveX(images[currentIndex].gameObject, 0, 0.5f)
                    .setDelay(1f)
                    .setEase(LeanTweenType.easeInOutQuad)
                    .setOnComplete(AnimationComplete);
                    break;
            }
            float posX = distancia;
            float posY = imagenes[currentIndex].transform.position.y;
            Vector2 newPos = new Vector2(posX, posY);
            imagenes[currentIndex].transform.position = newPos;
        }
    }

    void hola(){
        animators[currentIndex].SetBool("estaCentro", true);
    }

    void AnimationComplete()
    {
        isAnimating = false; // Marcar que la animación ha terminado
    }
}