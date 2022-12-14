using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WordManager : MonoBehaviour
{
    [SerializeField]
    Word[] levelOneWords;
    [SerializeField]
    Word[] levelTwoWords;
    [SerializeField]
    Word[] levelThreeWords;
    [SerializeField]
    Word[] levelFourWords;
    [SerializeField]
    GameObject[] scrambleSlots;
    [SerializeField]
    Image[] timerImages;
    [SerializeField]
    Color colorToChangeTime;
    [SerializeField]
    Color colorToStartTime;
    [SerializeField]
    LockedCheck lockedLetter;
    [SerializeField]
    LockedBool lockedBool;
    [SerializeField]
    GameObject[] levelOneEnemyPool;
    [SerializeField]
    Transform[] spawnPointEnemy;
    [SerializeField]
    TextMeshProUGUI textForLevel;
    IEnumerator pause;
    List<Word> levelWords = new List<Word>();
    List<string> scrambledWord = new List<string>();
    List<string> letters = new List<string>();
    List<string> finalWord = new List<string>();
    List<string> lastLetter = new List<string>();
    List<string> finishedWords = new List<string>();
    public int currentLevel;
    bool hasUpdatedLevelOne;
    bool hasUpdatedlevelTwo;
    public int letterPassed;
    public bool hasCheckedFinalWord;
    string finalString;
    public bool hasUpdated;
    bool hasUpdatedLevel;
    int storedRandomInt;
    int counterForEnemy;
    private void Awake()
    {
        lockedLetter.lettersLocked = 0;
    }
    private void Start()
    {
        currentLevel = 1;
        if (currentLevel == 1)
        {
            if (levelWords != null && !hasUpdatedLevelOne)
            {
                hasUpdatedLevelOne = true;
                for (int i = 0; i < levelOneWords.Length; i++)
                {
                    if (levelOneWords[i] != null)
                    {
                        levelWords.Add(levelOneWords[i]);
                    }
                }
            }
        }
        textForLevel.text = "Level 1-"+currentLevel.ToString() + " Foods";
        int randomRange = Random.Range(0, levelOneWords.Length);
        storedRandomInt = randomRange;
        finalString = levelWords[randomRange].wordToUse;
        SplitWord(finalString);
       
    }

    private void Update()
    { 
      //  Debug.Log(letterPassed.ToString());
        if (lockedLetter.lettersLocked == 5)
        {
            if (!hasUpdated)
            {
                hasUpdated = true;
                StopCoroutine(pause);
                if (scrambleSlots.Length > 0)
                {
                    if (finalWord.Count > 0)
                    {
                        //for (int i = 0; i < scrambleSlots.Length; i++)
                        //{
                        //    Debug.Log("letter is " + finalWord[i]);
                        //    Debug.Log("your letter is " + scrambleSlots[i].transform.GetChild(0).GetComponent<TMP_Text>().text);
                        //    if (finalWord[i] == scrambleSlots[i].transform.GetChild(0).GetComponent<TMP_Text>().text)
                        //    {
                        //        letterPassed += 1;
                        //    }

                        //}
                        string[] wordToUse = new string[finalString.Length];
                        for (int y = 0; y < finalString.Length; y++)
                        {
                            wordToUse[y] = System.Convert.ToString(finalString[y]);
                            Debug.Log(wordToUse[y].ToString());


                            if (scrambleSlots[y] != null)
                            {
                                if (scrambleSlots[y].transform.GetChild(0).GetComponent<TMP_Text>().text == wordToUse[y])
                                {
                                    letterPassed++;
                                }
                            }
                        }

                        if (letterPassed == 5)
                        {
                            finishedWords.Add(finalString);
                            scrambleSlots[0].transform.GetChild(0).GetComponent<TMP_Text>().text = "g";
                            scrambleSlots[1].transform.GetChild(0).GetComponent<TMP_Text>().text = "r";
                            scrambleSlots[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "e";
                            scrambleSlots[3].transform.GetChild(0).GetComponent<TMP_Text>().text = "a";
                            scrambleSlots[4].transform.GetChild(0).GetComponent<TMP_Text>().text = "t";
                            StartCoroutine(NewLevel());
                           
                        }
                        else if(letterPassed != 5)
                        {
                            scrambleSlots[0].transform.GetChild(0).GetComponent<TMP_Text>().text = "w";
                            scrambleSlots[1].transform.GetChild(0).GetComponent<TMP_Text>().text = "r";
                            scrambleSlots[2].transform.GetChild(0).GetComponent<TMP_Text>().text = "o";
                            scrambleSlots[3].transform.GetChild(0).GetComponent<TMP_Text>().text = "n";
                            scrambleSlots[4].transform.GetChild(0).GetComponent<TMP_Text>().text = "g";

                            // Debug.Log("you have the wrong word");
                            for (int d = 0; d < scrambleSlots.Length; d++)
                            {
                                letterPassed -= 1;
                                
                                scrambleSlots[d].GetComponent<BoxController>().WrongAnswer();

                            }
                            StartCoroutine(ResetBricks());
                           
                        }
                    }
                }
            }
        }
     
      
    }

    public void SplitWord(string wordForScramble)
    {
        if (pause != null)
        {
            StopCoroutine(pause);
        }

        if (currentLevel > 1 && !hasUpdatedLevel && currentLevel <= 4)
        {
            hasUpdatedLevel = true;

            int randomRange = Random.Range(0, levelOneWords.Length);
            storedRandomInt = randomRange;
            wordForScramble = levelWords[randomRange].wordToUse;
            finalString = wordForScramble;
            if (finishedWords.Count > 0)
            {
                for (int l = 0; l < finishedWords.Count; l++)
                {
                    if (finishedWords[l] != null)
                    {
                        if (finishedWords[l] == finalString)
                        {
                            hasUpdatedLevel = false;
                            SplitWord(finalString);

                            break;
                        }

                    }


                }

            }
        }

        else if (currentLevel >= 5 && !hasUpdatedLevel)
        {
            hasUpdatedLevel = true;

            int randomRange = Random.Range(0, levelTwoWords.Length);
            storedRandomInt = randomRange;
            wordForScramble = levelWords[randomRange].wordToUse;
            finalString = wordForScramble;
            if (finishedWords.Count > 0)
            {
                for (int l = 0; l < finishedWords.Count; l++)
                {
                    if (finishedWords[l] != null)
                    {
                        if (finishedWords[l] == finalString)
                        {
                            hasUpdatedLevel = false;
                            SplitWord(finalString);

                            break;
                        }

                    }


                }

            }
        }
            hasUpdatedLevel = true;
      

        for (int i = 0; i < timerImages.Length; i++)
        {
            timerImages[i].color = colorToStartTime;
        }
        if (finalWord.Count > 0)
        {
            for (int i = 0; i < finalWord.Count; i++)
            {
                finalWord.RemoveAt(i);
            }
        }
        letters = new List<string>();
       
        string storeThisWord = wordForScramble;
       
       
        
        string[] wordToUse = new string[wordForScramble.Length];
        for (int y = 0; y < wordForScramble.Length; y++)
        {
            wordToUse[y] = System.Convert.ToString(wordForScramble[y]);
            Debug.Log(wordToUse[y].ToString());

            letters.Add(wordToUse[y]);
            finalWord.Add(wordToUse[y]);
           
        }
       
        ScrambleWord(storeThisWord);
     
    }

    void ScrambleWord(string wordForScramble)
    {
        List<string> tempWord = new List<string>();

        
        for (int i = 0; i < wordForScramble.Length; i++)
        {

            if (letters.Count == 0)
            {
                letters.Clear();

            }
            else
            {
                int randomRange = Random.Range(0, letters.Count);
                tempWord.Add(letters[randomRange]);
                letters.RemoveAt(randomRange);
            }
            Debug.Log("temp word is" + tempWord.Count);

        }


        if (tempWord.Count > 0)
        {
            string tempString = "";
            for (int p = 0; p < tempWord.Count; p++)
            {
                tempString += tempWord[p];
            }
            Debug.Log("the string is " + tempString + "and the scrambled word is " + wordForScramble);
            if (tempString == wordForScramble)
            {
              Debug.Log("Rescramble");
                SplitWord(wordForScramble);
                return;
            }
            else
            {
                if (scrambleSlots.Length > 0)
                   
                {
                    for (int d = 0; d < scrambleSlots.Length; d++)
                    {

                        if (!scrambleSlots[d].GetComponent<BoxController>().isLocked)
                        {
                            scrambleSlots[d].transform.GetChild(0).GetComponent<TMP_Text>().text = tempWord[d];
                        }
                        
                       
                        scrambleSlots[d].GetComponent<BoxController>().PopulateBox(tempWord);
                    }

                }
            }


            pause = TimerForScramble(finalString);
            
            StartCoroutine(pause);
            

        }
    }

    public void ResumeRoutine()
    {
        StartCoroutine(pause);
    }

    void CheckEnemyLevel()
    {
        int wayToSend = 0;
        int randomNumber = Random.Range(0, 1);
        Debug.Log("Random number is" + randomNumber.ToString() + "direction number is" + wayToSend.ToString());
        if(randomNumber == 0)
        {
            wayToSend = -1;
        }
        else if(randomNumber == 1)
        {
            wayToSend = 1;
        }
        if (counterForEnemy < 3)
        {
            GameObject newEnemy = Instantiate(levelOneEnemyPool[0], spawnPointEnemy[randomNumber]);
            Debug.Log(wayToSend.ToString());
            newEnemy.GetComponent<EnemyWalker>().DirectionToSend(wayToSend, true);
            return;
        }
    }
    IEnumerator ResetBricks()
    {
        letterPassed = 0;
        hasUpdated = false;
        lockedLetter.lettersLocked = 0;
        yield return new WaitForSeconds(1f);

        for (int d = 0; d < scrambleSlots.Length; d++)
        {
            lockedLetter.lettersLocked = 0;
            scrambleSlots[d].GetComponent<BoxController>().isLocked = false;
            scrambleSlots[d].GetComponent<BoxController>().ResetLock();
            scrambleSlots[d].GetComponent<BoxController>().ResetColor();
           
        }
        
        SplitWord(finalString);
       

        yield break;

    }

    IEnumerator NewLevel()
    {
        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        for(int i =0; i < enemys.Length; i++)
        {
            if (enemys[i] != null)
            {
                Destroy(enemys[i], .1f);
            }
        }
        counterForEnemy = 0;
        currentLevel += 1;
        letterPassed = 0;
      
        yield return new WaitForSeconds(1f);

        for (int d = 0; d < scrambleSlots.Length; d++)
        {
            lockedLetter.lettersLocked = 0;
            scrambleSlots[d].GetComponent<BoxController>().isLocked = false;
            scrambleSlots[d].GetComponent<BoxController>().ResetColor();
            scrambleSlots[d].GetComponent<BoxController>().ResetTheBrick();
        }
        
        if(currentLevel <= 4)
        {
            textForLevel.text = "Level 1-" + currentLevel.ToString() + " Foods";
        }
        if (currentLevel >= 5 && levelWords != null && !hasUpdatedlevelTwo)
        {
            hasUpdatedlevelTwo = true;
            textForLevel.text = "Level 2-"+ currentLevel.ToString() + " Tools And Supplies";
            if(levelWords.Count > 0)
            {
                levelWords.Clear();
            }
          
            
            for (int i = 0; i < levelTwoWords.Length; i++)
            {
                if (levelTwoWords[i] != null)
                {
                    
                    levelWords.Add(levelTwoWords[i]);
                }
            }
            int randomRange = Random.Range(0, levelTwoWords.Length);
            storedRandomInt = randomRange;
            finalString = levelWords[randomRange].wordToUse;
        }
        if(currentLevel >= 5 && hasUpdatedlevelTwo)
        {
            textForLevel.text = "Level 2-" + currentLevel.ToString() + " Tools And Supplies";
        }

        hasUpdated = false;
        lockedLetter.lettersLocked = 0;
        hasUpdatedLevel = false;
        StopCoroutine(TimerForScramble(finalString));
        SplitWord(finalString);


        yield break;

    }

    IEnumerator TimerForScramble(string finalstring)
    {
        yield return new WaitForSeconds(.1f);
        timerImages[9].color = colorToChangeTime;
        yield return new WaitForSeconds(1f);
        timerImages[8].color = colorToChangeTime;
        yield return new WaitForSeconds(1f);
        timerImages[7].color = colorToChangeTime;
        yield return new WaitForSeconds(1f);
        timerImages[6].color = colorToChangeTime;
        yield return new WaitForSeconds(1f);
        timerImages[5].color = colorToChangeTime;
        yield return new WaitForSeconds(1f);
        timerImages[4].color = colorToChangeTime;
        yield return new WaitForSeconds(1f);
        timerImages[3].color = colorToChangeTime;
        yield return new WaitForSeconds(1f);
        timerImages[2].color = colorToChangeTime;
        yield return new WaitForSeconds(1f);
        timerImages[1].color = colorToChangeTime;
        yield return new WaitForSeconds(1f);
        timerImages[0].color = colorToChangeTime;
        yield return new WaitForSeconds(1f);
        counterForEnemy += 1;
        CheckEnemyLevel();
        SplitWord(finalstring);
        yield break;
    }

 




   

}


