using UnityEngine;
using UnityEngine.SceneManagement;

public class CarCrasher : MonoBehaviour
{
    public void NextLoop()
    {
        int loop = WhatLoopIsIt.ins.loop;

        if (loop == 1)
        {
            DialogueManager.ins.CutToBlack();
            SceneManager.LoadScene("Loop2");
        }
        else if (loop == 2)
        {
            DialogueManager.ins.CutToBlack();
            SceneManager.LoadScene("Loop3");
        }
        else if (loop == 3)
        {
            DialogueManager.ins.CutToBlack();
            SceneManager.LoadScene("Dream");
        }
    }

    public void CarEnding()
    {
        DialogueManager.ins.Speak(new string[] {
            "(It worked?!)",
            "(Gotta play it cool...)",
            "Hey",
            "$Car Driver",
            "Hey?",
            "$",
            "You're the guy who keeps head-on ramming my car every morning!",
            "$Car Driver",
            "I am? Are you sure?",
            "$",
            "Yes, positive! It hurts every time, and very much feels like a car! Why are you doing it, why are you keeping me in this loop?",
            "$Car Driver",
            "Dude, I have no clue what you are talking about. And I don't have time, I have to get to work in exactly a minute!",
            "$",
            "Well so do I, but you keep hitting me! Where do you even work, the bad driving factory?",
            "$Car Driver",
            "Woah dude, NOT cool. I have an actual important job, that you probably wouldn't even understand the value of.",
            "Coffee N' Co., we make only the finest joe for all our loyal customers!",
            "$",
            "Wait, that's where I work! Why are you driving this way?",
            "$Car Driver",
            "Why are YOU driving this way? The building's that way!",
            "$",
            "Yeah ok buddy, what cubicle number do you work in then, hmm? Loser number 800?",
            "$Car Driver",
            "Good ole' 727, buckaroo. It has a warm seat calling my name, mainly from my boss's coffee stains...",
            "$",
            "Wait... I work in cubicle 727...",
            "$Car Driver",
            "No you don't? What next, you're gonna tell me your boss throws coffee at you too for being late? That there's cats on the moon? Or that you drive a-",
            "$",
            "1998 Silver Sedan with a V8 Unity engine?",
            "$Car Driver",
            "Wait a minute...",
            "$",
            "Does your wife-",
            "$Car Driver",
            "Complain about the parties I throw during the week even though I have work the next day, and therefore is the reason I'm late to work everyday?",
            "$",
            "...",
            "Say... what's your name?",
            "$Car Driver",
            "Dingleberry."
        }, () =>
        {
            DialogueManager.ins.FadeOut(() =>
            {
                DialogueManager.ins.Speak(new string[] {
                    "I can't believe it. A clone of myself has been the reason I reset the loop each time.",
                    "But, that doesn't really answer any of my questions, and just adds a ton more!",
                    "Why doesn't he loop? Or is his loop a different set of events?",
                    "Why are there two me's??? Is there even more?",
                    "Does one of them actually fulfill our dream of becoming a professional diver on a swim team? Does each have the same wife? Does my wife have clones?",
                    "Oh no, too many questions, brain overload! Other me, quick, car!",
                }, () => SceneManager.LoadScene("SampleScene"));
            });
        });
    }
}
