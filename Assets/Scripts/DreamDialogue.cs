using UnityEngine;
using UnityEngine.SceneManagement;

public class DreamDialogue : MonoBehaviour
{
    void Start()
    {
        DialogueManager.ins.Speak(new string[] {
            "AH! Ok, woah woah WOAH. I definitely just got hit by an actual, very real car!",
            "What is going on? Am I in a dream right now?",
            "I certainly don't live near a cool void like this, so I must be dreaming!",
            "But if that wasn't a dream, and this is, then it was real? But, I got hit by a car?",
            "I mean, I should be hurting pretty bad right now, but I feel fine. Hmm...",
            "...",
            ".........",
            "Wait.",
            "Have I been repeating the same day?",
            "Come to think of it, I never checked the date, just the time... and it has always been exactly 7:27 am when I wake up.",
            "I must be! I bet I'm going to wake up at the exact same time again!",
            "And I'll have a headache, and my dang wife will complain about my super awesome party I threw the night before!",
            "But why?",
            "What is causing this? I haven't really angered any ancient Gods recently, so maybe... it's someone in the loops.",
            "Is my wife hiding secret unknown powers from me?",
            "She was... or is, or... will be mad at me about the party, so maybe she is using her hidden evil witch powers...",
            "OR it could be that mindless driver who keeps front-ending my beautiful, 1998 Silver Sedan, running off of a roaring V8 Unity engine...",
            "OR even... my own baby...",
            "What?",
            "Don't look at me like that, player. These kinds of twists happen in movies sometimes, you never know!",
            "Anyways, I guess I will just wake up and see what happens...",
        }, () =>
        {
            SceneManager.LoadScene("Loop4");
        });
    }
}
