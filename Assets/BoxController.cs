using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxController : MonoBehaviour
{
   
    public Color colorToStartAs;
    [SerializeField]
    Color colorToChangeTo;
    [SerializeField]
    Color colorForWrong;

    [SerializeField]
    WordManager wordManager;
    List<string> temporaryWord = new List<string>();
    TMP_Text boxText;
    int downInt;
    int upInt;

    bool hasPushed;
    bool playerIsInBox;
    public bool isLocked;
    bool hasLocked;
    string tempLetter;

    public LockedCheck lockedLetter;


    [HideInInspector]
    PlayerController pC;

    private void Awake()
    {
        boxText = gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = colorToStartAs;
    }

    private void Start()
    {
        pC = FindObjectOfType<PlayerController>();
        wordManager = FindObjectOfType<WordManager>();
    }

    public void PopulateBox(List<string> wordForBox)
    {
        if (!isLocked)
        {
            temporaryWord.Clear();
            if(temporaryWord.Count > 0)
            {
                for(int i = 0; i > temporaryWord.Count; i++)
                {
                    if (temporaryWord[i] != null)
                    {
                        temporaryWord.RemoveAt(i);
                    }
                }
            }
            for (int i = 0; i < wordForBox.Count; i++)
            {
                if (wordForBox[i] != null)
                {
                    temporaryWord.Add(wordForBox[i]);
                    //Debug.Log("temporary word is " + temporaryWord[i]);
                   
                }
            }

            upInt = temporaryWord.Count - 1;
            if (boxText.text == tempLetter)
            {
                boxText.text = temporaryWord[upInt - 2];
            }

        }
    }

    private void Update()
    {
        if (playerIsInBox) 
        { 

            if (pC.IsPushingUpBrick() && !hasPushed && !isLocked)
            {
                hasPushed = true;
                PlayerPushBoxUp();
            }
            else if (pC.IsPushingBrickDown() && !hasPushed && !isLocked)
            {
                hasPushed = true;
                PlayerPushBoxDown();

            }

            if (pC.IsLockingLetter() && !hasLocked)
            {
                if (!isLocked)
                {
                    hasLocked = true;
                    isLocked = true;
                    lockedLetter.lettersLocked += 1;
                    gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = colorToChangeTo;
                    StartCoroutine(ResetLock());
                }
                else if (isLocked)
                {
                    hasLocked = true;
                    isLocked = false;
                    if(lockedLetter.lettersLocked == 5)
                    {
                        wordManager.ResumeRoutine();
                    }
                    lockedLetter.lettersLocked -= 1;
                    wordManager.hasUpdated = false;
                   
                    gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = colorToStartAs;
                    
                    StartCoroutine(ResetLock());
                }
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsInBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerIsInBox = false;
        }
    }

    public void PlayerPushBoxUp()
    {
        Debug.Log("trying to push up");

        if (temporaryWord.Count > 0)
        {
            
            boxText.text = temporaryWord[upInt];
            tempLetter = temporaryWord[upInt];
            if (upInt != 0)
            {
                upInt -= 1;
            }
            else if (upInt == 0)
            {
                upInt = temporaryWord.Count - 1;
            }
            Debug.Log("pushing up");
        }
        StartCoroutine(ResetPush());



    }
    public void PlayerPushBoxDown()
    {
        Debug.Log("trying to push down");
        if (temporaryWord.Count > 0)
        {
            boxText.text = temporaryWord[downInt];
            tempLetter = temporaryWord[downInt];
            if (downInt != temporaryWord.Count -1)
            {
                downInt += 1;
            }
            else if (downInt >= temporaryWord.Count - 1)
            {
                downInt = 0;
            }
            Debug.Log("pushing down");
        }
        StartCoroutine(ResetPush());
    }

    public void ResetColor()
    {
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = colorToStartAs;
    }
    public void WrongAnswer()
    {
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().color = colorForWrong;
    }
    public void ResetTheBrick()
    {
        tempLetter = "";
        isLocked = false;
       if(temporaryWord.Count > 0)
        {
            for(int i = 0; i < temporaryWord.Count; i++)
            {
                if (temporaryWord[i] != null)
                {
                    temporaryWord.RemoveAt(i);
                }
               
            }
            temporaryWord.Clear();
            hasPushed = false;
            hasLocked = false;
        }
    }


    IEnumerator ResetPush()
    {
        yield return new WaitForSeconds(.25f);
        hasPushed = false;
        yield break;
    }

    public IEnumerator ResetLock()
    {
        yield return new WaitForSeconds(.25f);
        hasLocked = false;
        yield break;
    }
}
