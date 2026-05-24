using System.Collections;
using UnityEngine;
using TMPro;

public class NPC4Dialog : MonoBehaviour
{
    [SerializeField] private GameObject DialogMark;
    [SerializeField] private GameObject DialogoPanel;
    [SerializeField] private TMP_Text Textazo;

    [SerializeField, TextArea(4, 6)]
    private string[] dialogLinesNPC;

    private PersonaMovi personaMovi;
    public DialogManager dialogManager;

    private bool dialogoCompletado = false;
    private float typingTime = 0.05f;
    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;
    public string cualNpc;

    private bool isTyping = false;

    public bool IsPlayerInRange()
    {
        return isPlayerInRange;
    }

    private void Start()
    {
        personaMovi = FindObjectOfType<PersonaMovi>(); // Obtener la referencia a PersonaMovi
    }

    public void CargarDialogo(int lineIndex)
    {
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown("space") && isPlayerInRange)
        {
            if (!didDialogueStart)
            {
                StartDialogue();
                dialogManager.verificarQnHablo(cualNpc);
            }
            else if (isTyping)
            {
                StopAllCoroutines();
                Textazo.text = dialogLinesNPC[lineIndex]; // Muestra todo el texto de la l�nea actual
                isTyping = false; // Marca como no escribiendo
            }
            else
            {
                NextDialogLine();
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        DialogoPanel.SetActive(true);
        DialogMark.SetActive(false);
        Time.timeScale = 0f;
        lineIndex = dialogManager.yaHablo(cualNpc);
        StartCoroutine(ShowLine());
    }

    private void EndDialogue()
    {
        dialogoCompletado = false;
        didDialogueStart = false;
        DialogoPanel.SetActive(false);
        DialogMark.SetActive(true);
        Time.timeScale = 1f;
        isTyping = false;
    }

    private void NextDialogLine()
    {
        lineIndex++;
        if (lineIndex < dialogLinesNPC.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            EndDialogue(); // Llama a EndDialogue cuando se alcanza la �ltima l�nea del di�logo
        }
    }


    private IEnumerator ShowLine()
    {
        isTyping = true;
        Textazo.text = string.Empty;

        foreach (char ch in dialogLinesNPC[lineIndex])
        {
            Textazo.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        isTyping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            DialogMark.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            DialogMark.SetActive(false);
            didDialogueStart = false;
            Time.timeScale = 1f;
        }
    }
}
