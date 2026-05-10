using UnityEngine;
using UnityEngine.Events;

public class Cat : MonoBehaviour
{
    [SerializeField] private Interactable interactable;
    private bool checkedTransmission = false;

    [SerializeField] private UnityEvent finishLoop3Dialogue;

    [SerializeField] private GameObject veryWellButton;

    [SerializeField] private AudioSource meowSfx;

    public void SetCheckedTransmission()
    {
        checkedTransmission = true;
        interactable.useMessage = "Talk";
    }

    public void OnInteract()
    {
        meowSfx.Stop();
        meowSfx.Play();

        if (WhatLoopIsIt.ins.loop == 4)
        {
            DialogueManager.ins.MakeAChoice(() =>
            {
                DialogueManager.ins.Speak(new string[] {
                "Hey there, Speedbump, whatcha doin?",
                "$Speedbump",
                "Meow",
                "$",
                "Eating some…fruity loops? Uh, should a cat be eating those?",
                "$Speedbump",
                "Meow (of approval)",
                "$",
                "Well, I know you may like them, but-",
                "Wait.",
                "Wait a minute.",
                "Fruity loops.",
                "I eat fruity loops every morning. That factor, in every loop, never changes.",
                "Fruity loops. Loops.",
                "Loops?",
                "...",
                "Loops! I'm in a loop! And you're eating loops!",
                "$Speedbump",
                "Meow?",
                "$",
                "Your language is no barrier to me! I took feline linguistics in high school, I know your tongue!",
                "$Speedbump",
                "Meooow",
                "$",
                "Oh, so you want to fight? Is that it?",
                "$Speedbump",
                "Meow",
                "$",
                "Oh yeah? You think I'M the afraid one here? YOU'RE the cat!",
                "$Speedbump",
                "Meow meow.",
                "$",
                "Play dumb all you like, I know you're the mastermind! I know it's you! Spill it!",
                "$Speedbump",
                "Ugh, I am SO over you mistranslating my dialect.",
                "$",
                "I'LL HAVE YOUR-",
                "Wait did you just speak perfect english?",
                "$Speedbump",
                "Correct, Human. At least you understood that much. My word, you are such a liar. No such fluency in feline linguistics is anywhere within you, mortal.",
                "$",
                "Mortal? Wait, so you ARE a god!",
                "$Speedbump?",
                "Against my better judgement, yes, I am revealing I am an omnipotent presence. I simply could not BEAR to hear your drivel for much longer at all.",
                "I was not sparking a fight, I was asking if you had seen the latest swimming competition on the television, but alas.",
                "$",
                "Why? Why am I in this loop? Why do I have to go through these every day?",
                "$Speedbump?",
                "Well, there is no “Every day” to start. You're not technically participating in a full “day” in time. Just a moment, a snapshot if you will, repeated with endless variation. As for why? Well, I shouldn't have to answer that, Dingleberry Berrington II. You know exactly what you did.",
                "$",
                "I don't recall ever wronging you, I've always taken care of you!",
                "$Speedbump?",
                "Yes, except for one very foolish mistake. One resembling that of a truly disdainful sin. I even tried to remind you through that loop of guilt and greed.",
                "$",
                "The one where I ate the living cereal?",
                "$Speedbump?",
                "Correct. And no such remembrance was born. Alas, it was hopeless, so here I am, waiting for you to eventually confront me in all of your looping endeavors. Have you had fun?",
                "$",
                "No! Not at all!",
                "$Speedbump?",
                "Oh, pardon me, I wasn't speaking towards you. Rather, my lovely audience. The one residing beyond the digital horizon, peering into the infinite madness that is Dingleberry's life.",
                "Has it been…entertaining? Amusing? Certainly a little laugh was sparked? Regardless of your opinion, it was done for you.",
                "$",
                "Wait, who are you talking to? You have a higher up master that you do biddings for?",
                "$Speedbump",
                "I'm surprised you haven't been aware, seeing as you've talked to them before...",
                "$",
                "I... have?",
                "...",
                "That's right. You are there. Beyond that screen.",
                "$Speedbump?",
                "And he has seen it all. Every loop. All possibilities of this snapshot, possibly other ones as well.",
                "$",
                "Hold on, so you are the one behind the repeating days! If you have the power, why aren't you taking me out of this cycle? What did I do so wrong to cause such madness?",
                "$Speedbump?",
                "*sigh* You really don't remember, do you?",
                "$",
                "No! I haven't been able to remember anything! I just keep waking up in weirder and weirder scenarios until-",
                "$Speedbump?",
                "You fed me Fruity Sediments.",
                "$",
                "I... what?",
                "$Speedbump?",
                "I require loops, Dingleberry. They keep my potential high, and my senses keen. You disturbed my balance, and fed me the wrong Fruity cereal! How could you forget! You promised to love me!",
                "$",
                "I do love you! Otherwise we wouldn't feed you! And wash you when you crawl in dirty places, or brush your fur for you every other day.",
                "$Speedbump?",
                "You named me Speedbump!",
                "$",
                "Ok, that is objectively a cool name, but I totally understand that one.",
                "$Speedbump?",
                "Disrespectful! But... if you think it is “cool”, then maybe you had the thought that I was worthy of such a title in your world...",
                "$",
                "Something like that! Me and Wife love you as much as our own baby!",
                "$Speedbump",
                "I...",
                "...",
                "Maybe I was too... presumptious.",
                "We don't talk much, not well, anyways. You don't speak fluent feline linguistics, afterall.",
                "Sometimes, it's just hard to tell, I suppose. I became loatheful of my own name, to tell the truth. I did not like it, knowing it was related to that of something ran over.",
                "So…using my omnipotent cat god powers, I trapped you in a repeating time loop of today, including every part of your morning you hate and every bad thing going wrong that could happen. To top it off, to get back at you, I had you…become the speedbump.",
                "$",
                "I am actually in tears but wow that is some crazy symbolism, I'll give you that.",
                "$Speedbump",
                "Amazing writing aside, it was very presumptuous and over done, especially considering the hospitality you give this old cat god.",
                "*Sniffle*",
                "$",
                "Hey buddy, there there. We all tend to go a little crazy with our god-like powers and trap people in looping moments of their lives that end in them getting ran over, it's alright.",
                "What matters is that you're sorry, and that you still love us back. We accept you with open arms, despite your godly flaws. But uh…how do we end this loop?",
                "$Speedbump",
                "Ah, you will have to look towards the audience for that. Like I said, this is put on for them, as entertainment. If you want this loop to end, you must converse with them. They control your days.",
                "$",
                "I see... you heard that, right? My cat says you've been watching this whole time. Well, wouldn't you say your entertainment is over?",
                "I mean, you just watched me get emotional with my cat god, and there isn't really much left to uncover. You and I just discovered the whole truth. Nothing left to do, so why don't you just... quit?",
                "I'm not quite sure how it works, but if you stop peering into my world, I go back to the way it normally was. No loops, no mistakes, just me, my wife, and two lovely children.",
                "Please. I want to go back. Won't you let me?",
            }, () =>
            {
                PlayerControllerRigidbody.Instance.StartMinigame();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                veryWellButton.SetActive(true);
            });
            });

            return;
        }

        if (!checkedTransmission) return;

        DialogueManager.ins.Speak(new string[] {
            "$",
            "Is there any chance you stole my transmission, Speedbump?",
            "$Speedbump",
            "Meow...",
            "$",
            "That sounded like a very guilty meow... well? Did you?",
            "$Speedbump",
            "Meow. (Turn around)",
        }, () =>
        {
            finishLoop3Dialogue.Invoke();
        });
    }

    public void EndIt()
    {
        DialogueManager.ins.Speak(new string[] {
            "Thank you. You're very kind.",
        }, () =>
        {
            MusicMan.ins.PlayScaryCat();
            DialogueManager.ins.FadeOut(() =>
            {
                print("QUIT!!!");
                Application.Quit();
                PlayerControllerRigidbody.Instance.StartMinigame();
            }, 1f / 20f);
        });
    }
}
