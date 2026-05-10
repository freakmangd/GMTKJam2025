using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Wife : MonoBehaviour
{
    private float losTimer;
    private const float losTimerMax = 0.2f;

    public DialogueList[] dialogues;

    enum State
    {
        kitchen,
        check_on_baby,
    }

    private State state = State.kitchen;

    public LayerMask playerLayer;

    public UnityEvent runToBabyStart;

    void Update()
    {
        losTimer -= Time.deltaTime;

        if (state == State.kitchen && losTimer < 0f)
        {
            losTimer = losTimerMax;

            if (Physics.Raycast(transform.position, (PlayerControllerRigidbody.Instance.transform.position - transform.position).normalized, out RaycastHit hit, 20f))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    transform.forward = (hit.transform.position - transform.position).normalized;
                    state = State.check_on_baby;

                    if (WhatLoopIsIt.ins.loop < 4)
                    {
                        DialogueManager.ins.Speak(dialogues[WhatLoopIsIt.ins.loop - 1].dialogue, runToBabyStart);
                    }
                    else
                    {
                        DialogueManager.ins.Speak(new string[] {
                            "$Wife",
                            "Hey honeybunchems, you sure did sleep-in this morning.",
                            "You usually have work in the next three minutes, you better grab your cereal and go. Wouldn't want that dang boss to give you a coffee shower again, would you?",
                            "Hey... is everything alright? You look real... serious.",
                            "Like you're about to confront someone about something beyond my silly comprehension.",
                            "$",
                            "Yes honey, I am fine. I will be eating my cereal now. Thank you.",
                            "$Wife",
                            "A-alright then... have a good day, honey...",
                        }, runToBabyStart);
                    }
                }
            }
        }
    }

    public void Interact()
    {
        DialogueManager dm = DialogueManager.ins;

        dm.MakeAChoice(() =>
        {
            dm.Speak(new string[] {
                "Honeysuckle, we need to talk.",
                "$Wife",
                "Oh no, am I the person that your confronting gaze is drawn towards?",
                "$",
                "That is correct, honeyflower. And it is very serious, so I need you to listen very closely to me and fully take it in, alright?",
                "$Wife",
                "I'm a bit worried, but go ahead honeybear.",
                "$",
                "Ok, so basically, I have been waking up on the same day, everyday. Always exactly at 7:27 am, always having a headache from the night before. And everyday, I'm always late for work, rushing out the door.",
                "You always say I slept late, and partied too hard the night before. You always suggest I eat cereal before I go. You always say I forgot to take out the trash, and then go tend to the baby. Every. Single. Time. Crazy, right?",
                "Well get this. After I leave to go to work, a speeding car hits me head on, and I wake up like the previous day was a dream.",
                "I can't tell if I'm even dreaming right now, but I just had a dream inside a dream, which I don't think is possible.",
                "So just be honest with me.",
                "Are you... an all-seeing spell-casting time-bending gravity-manipulating earth-shattering demi-god/entire god in disguise trapping me in an endless loop of time not-so coincidentally lining up with the day after I threw a huge party that may or may not have really peeved you off?",
                "$Wife",
                "...",
                ".....",
                "We're getting a divorce.",
                "$",
                "WHAT?!",
                "$Wife",
                "Dingleberry, I just can't keep doing this. This is the maybe the 50th time you've tried telling me that your life is going in a loop, and it just gets old after awhile.",
                "$",
                "Huh? But this is-",
                "$Wife",
                "The first time you're telling me this? Yeah, you say that every time. It isn't. Have you ever thought that maybe, just maybe, your old habits never die?",
                "I mean, you party ALL the time, and NEVER clean up the mess EVER. Even on days you have work the next morning, you INSIST on celebrating like it's the end of the world.",
                "And why does Tony have no problem sleeping in our trash can every night?",
                "Then you complain that you're gonna be late, like you didn't do it to yourself. Maybe your boss throws coffee at you because his employee can't ever make it to work on time ONCE.",
                "$",
                "Honey, I-",
                "$Wife",
                "No, Dingleberry. We are through!"
            }, () =>
            {
                DialogueManager.ins.FadeOut(() =>
                {
                    dm.Speak(new string[] {
                        "Well dang, my wife just divorced me over accusing her of being an all-seeing spell-casting time-bending gravity-manipulating earth-shattering demi-god/entire god in disguise trapping me in an endless loop of time not-so coincidentally lining up with the day after I threw a huge party that may or may not have really peeved her off.",
                        "What's next, cats on the moon?",
                        "In any case, it probably isn't my wife.",
                        "But luckily, the loops will continue, I will receive another car to the dome, and I can retry my bid.",
                        "I'm glad, because I do really love my wife.",
                    }, () =>
                    {
                        SceneManager.LoadScene("SampleScene");
                    });
                });
            });
        });
    }
}
