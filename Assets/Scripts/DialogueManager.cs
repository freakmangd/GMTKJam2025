using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public GameObject textBox;
    public GameObject nameTag;
    public TMP_Text dialogueText;
    public TMP_Text nameTagText;

    public GameObject nextDialogueArrow;

    public Queue<string> dialogueQueue;

    private string typingText;

    public bool talking;
    private bool typing;

    private Action onFinish;

    private InputAction next;

    [SerializeField] private UnityEngine.UI.Image black, explosion;
    [SerializeField] private GameObject choices;

    enum Choice
    {
        none, yes, no
    }

    Choice choice;

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
        if (next.WasReleasedThisDynamicUpdate())
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
    public void Speak(string[] dialogue)
    {
        Speak(dialogue, () => { });
    }

    public void Speak(string[] dialogue, UnityEvent onFinish)
    {
        Speak(dialogue, () => onFinish.Invoke());
    }

    public void Speak(string[] dialogue, Action onFinish)
    {
        nameTag.SetActive(false);
        dialogueText.text = string.Empty;
        this.onFinish = onFinish;

        ToggleTextBox(true);
        dialogueQueue = new Queue<string>(dialogue);

        talking = true;

        NextDialogue();
    }

    public void MakeAChoice(Action onYes)
    {
        Speak(new string[] {
            "Are you sure it's them?"
        }, () =>
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PlayerControllerRigidbody.Instance.StartMinigame();
            choice = Choice.none;
            choices.SetActive(true);

            StartCoroutine(WaitForChoice(() =>
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                PlayerControllerRigidbody.Instance.StopMinigame();
                onYes?.Invoke();
            }, () =>
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                PlayerControllerRigidbody.Instance.StopMinigame();
            }));
        });
    }

    public void ChooseYes()
    {
        choice = Choice.yes;
    }

    public void ChooseNo()
    {
        choice = Choice.no;
    }

    public void NextDialogue(bool add = false)
    {
        if (dialogueQueue.Count == 0)
        {
            FinishedDialogue();
            return;
        }

        string next = dialogueQueue.Dequeue();

        if (next.StartsWith("$"))
        {
            if (next.Length > 1)
            {
                nameTagText.text = next[1..];
                nameTag.SetActive(true);
            }
            else
            {
                nameTag.SetActive(false);
            }

            NextDialogue();
            return;
        }

        nextDialogueArrow.SetActive(false);

        StopAllCoroutines();
        StartCoroutine(TypeDialogue(next, add));
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

    private IEnumerator WaitForChoice(Action onYes, Action onNo)
    {
        while (choice == Choice.none) yield return new WaitForEndOfFrame();
        choices.SetActive(false);

        if (choice == Choice.yes)
        {
            onYes?.Invoke();
        }
        else
        {
            onNo?.Invoke();
        }
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
        black.gameObject.SetActive(true);
        black.color = new Color(0f, 0f, 0f);
    }

    public void Explosion()
    {
        PlayerControllerRigidbody.Instance.StartMinigame();
        explosion.gameObject.SetActive(true);
        Util.ins.DoAfterSeconds(1f, () => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }

    public void FadeIn(Action onFinish)
    {
        black.gameObject.SetActive(true);
        StartCoroutine(FadeInCo(onFinish));
    }

    public void FadeOut(Action onFinish, float timeScale = 1f)
    {
        black.gameObject.SetActive(true);
        StartCoroutine(FadeOutCo(onFinish, timeScale));
    }

    private IEnumerator FadeInCo(Action onFinish)
    {
        PlayerControllerRigidbody.Instance.StartMinigame();
        float a = 1f;

        while (a > 0f)
        {
            black.color = new Color(0f, 0f, 0f, a);
            a -= Time.deltaTime;

            yield return new WaitForSeconds(0.0001f);
        }

        black.gameObject.SetActive(false);
        onFinish?.Invoke();
        PlayerControllerRigidbody.Instance.StopMinigame();
    }
    
    private IEnumerator FadeOutCo(Action onFinish, float timeScale)
    {
        PlayerControllerRigidbody.Instance.StartMinigame();
        float a = 0f;

        while (a < 1f)
        {
            a += Time.deltaTime * timeScale;
            black.color = new Color(0f, 0f, 0f, a);

            yield return new WaitForSeconds(0.0001f);
        }

        onFinish?.Invoke();
        PlayerControllerRigidbody.Instance.StopMinigame();
    }
}
