using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public GameObject textBox;
    public TMP_Text dialogueText;

    public GameObject nextDialogueArrow;

    public Queue<string> dialogueQueue;

    private string typingText;

    public bool talking;
    private bool typing;

    private UnityEvent onFinish;

    private InputAction next;

    [SerializeField] private GameObject black;

    public static DialogueManager ins;

    private void Awake()
    {
        ins = this;
    }

    void Start()
    {
        next = InputSystem.actions.FindAction("Next");
    }

    private void Update()
    {
        if (next.WasPressedThisDynamicUpdate())
        {
            if (typing)
            {
                CompleteText();
            }
            else if (talking)
            {
                NextDialogue();
            }
        }
    }

    public void Speak(string[] dialogue, UnityEvent onFinish)
    {
        dialogueText.text = string.Empty;
        this.onFinish = onFinish;

        ToggleTextBox(true);
        dialogueQueue = new Queue<string>(dialogue);

        talking = true;

        NextDialogue();
    }

    public void NextDialogue(bool add = false)
    {
        if (dialogueQueue.Count == 0)
        {
            FinishedDialogue();
            return;
        }

        nextDialogueArrow.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(TypeDialogue(dialogueQueue.Dequeue(), add));
    }

    private IEnumerator TypeDialogue(string text, bool add)
    {
        typingText = text;
        typing = true;

        if (!add)
            dialogueText.text = string.Empty;

        foreach (char c in text)
        {
            dialogueText.text += c;

            yield return new WaitForSeconds(0.015f);
        }

        typing = false;
        nextDialogueArrow.SetActive(true);
    }

    private void CompleteText()
    {
        typing = false;
        nextDialogueArrow.SetActive(true);

        StopAllCoroutines();
        dialogueText.text = typingText;
    }

    private void FinishedDialogue()
    {
        talking = false;
        textBox.SetActive(false);
        nextDialogueArrow.SetActive(false);
        onFinish?.Invoke();
    }

    public void ToggleTextBox(bool value)
    {
        textBox.SetActive(value);
    }

    public void ClearTextBox()
    {
        dialogueText.text = string.Empty;
    }

    public void CutToBlack()
    {
        black.SetActive(true);
    }
}
