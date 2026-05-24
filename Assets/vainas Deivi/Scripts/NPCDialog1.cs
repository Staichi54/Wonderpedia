using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
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
        for (int i = 0; i < dialogLinesNPC.Length; i++)
        {
            dialogLinesNPC[i] = QuitarTildes(dialogLinesNPC[i]);
        }
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

    private string QuitarTildes(string input)
    {
        input = input.Replace('á', 'a');
        input = input.Replace('é', 'e');
        input = input.Replace('í', 'i');
        input = input.Replace('ó', 'o');
        input = input.Replace('ú', 'u');
        input = input.Replace('Á', 'A');
        input = input.Replace('É', 'E');
        input = input.Replace('Í', 'I');
        input = input.Replace('Ó', 'O');
        input = input.Replace('Ú', 'U');
        input = input.Replace('¿', ' ');
        input = input.Replace('¡', ' ');
        return input;
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
            dialogoCompletado = true; // Marcar el di�logo como completado cuando se muestra la �ltima l�nea
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

        // Verificar si el texto es "..." despu�s de completar la escritura de la l�nea
        if (Textazo.text == "$")
        {
            Textazo.text = "";
            EndDialogue();
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
