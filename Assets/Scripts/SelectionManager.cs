using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour {

    [SerializeField] private GameObject leftCard;
    [SerializeField] private GameObject rightCard;
    [SerializeField] private EventSystem eventSys;

    [SerializeField] private GameObject[] weaponPairs;
    [SerializeField] private GameObject[] mapPairs;
    [SerializeField] private List<GameObject> cardPairs;

    private Text instruction;

    private int choicesMade = 0;
    private int maxChoices = 6;

    private float direction = 0f;
    private CardChoice currentCards;
    private System.Random random;

    public int player;

    // Use this for initialization
    void Start () {


        random = new System.Random();
        player = random.Next(1, 3); //Aloittava pelaaja valitaan satunnaisesti

        instruction = GameObject.Find("Instruction").GetComponent<Text>();
        instruction.text = "Player " + player + " choose your weapon";

        int weaponChoiceN = random.Next(0, weaponPairs.Length);
        currentCards = weaponPairs[weaponChoiceN].GetComponent("CardChoice") as CardChoice;

        leftCard.GetComponentInChildren<Text>().text = currentCards.choice1;
        rightCard.GetComponentInChildren<Text>().text = currentCards.choice2;

        eventSys.SetSelectedGameObject(leftCard);
    }
	
	// Update is called once per frame
	void Update () {
        direction = Input.GetAxis("Horizontal-" + player);

        if (Input.GetButtonDown("SelectCard-" + player))
        {
            GameObject selected = eventSys.currentSelectedGameObject;
            GameObject unselected;
            if (selected == leftCard) unselected = rightCard;
            else unselected = leftCard;
            
            if(choicesMade == 0)
            {
                GameObject.Find("Stats-" + player).GetComponent<PlayerStats>().weapon = selected.GetComponentInChildren<Text>().text;
                player = nextPlayer();
                GameObject.Find("Stats-" + player).GetComponent<PlayerStats>().weapon = unselected.GetComponentInChildren<Text>().text;

                choicesMade += 1;

                int mapChoiceN = random.Next(0, mapPairs.Length);
                currentCards = mapPairs[mapChoiceN].GetComponent("CardChoice") as CardChoice;

                instruction.text = "Player " + player + " choose the arena";

                leftCard.GetComponentInChildren<Text>().text = currentCards.choice1;
                rightCard.GetComponentInChildren<Text>().text = currentCards.choice2;

                eventSys.SetSelectedGameObject(leftCard);
            }
            else if(choicesMade == 1)
            {
                GameObject.Find("Stats-" + player).GetComponent<PlayerStats>().map = selected.GetComponentInChildren<Text>().text;
                player = nextPlayer();
                GameObject.Find("Stats-" + player).GetComponent<PlayerStats>().map = selected.GetComponentInChildren<Text>().text;

                choicesMade += 1;
                getNewCards();
            }
            else
            {
                PlayerStats leftP;
                PlayerStats rightP;
                if (selected == leftCard)
                {
                    leftP = GameObject.Find("Stats-" + player).GetComponent<PlayerStats>();
                    player = nextPlayer();
                    rightP = GameObject.Find("Stats-" + player).GetComponent<PlayerStats>();
                }
                else
                {
                    rightP = GameObject.Find("Stats-" + player).GetComponent<PlayerStats>();
                    player = nextPlayer();
                    leftP = GameObject.Find("Stats-" + player).GetComponent<PlayerStats>();
                }
                
                switch (currentCards.gameObject.name)
                {
                    case "SpeedHp":
                        leftP.speedMod *= 1.5f;
                        leftP.hpMod *= 0.5f;
                        rightP.speedMod *= 2.0f;
                        rightP.hpMod *= 2.0f;
                        break;
                    case "SizeDamage":
                        leftP.sizeMod *= 1.25f;
                        leftP.damageMod *= 1.25f;
                        rightP.sizeMod *= 0.75f;
                        rightP.damageMod *= 0.75f;
                        break;
                    case "HeavyFloaty":
                        leftP.fallMult = 5f;
                        rightP.fallMult = 0.1f;
                        break;
                    case "Invert":
                        leftP.invertVertical = true;
                        rightP.invertHorizontal = true;
                        break;
                    case "LifeJump":
                        leftP.hpMod *= 2.0f;
                        leftP.hasNoJump = true;
                        rightP.hasDoubleJump = true;
                        break;
                    case "JumpDash":
                        leftP.hasDoubleJump = true;
                        rightP.hasDash = true;
                        break;
                    case "FireDamage":
                        leftP.fireRateMod *= 0.5f;
                        leftP.damageMod *= 0.5f;
                        rightP.fireRateMod *= 2.0f;
                        rightP.damageMod *= 2.0f;
                        break;
                    case "AccuracySpeed":
                        leftP.accuracyMod *= 1.5f;
                        rightP.speedMod *= 1.5f;
                        break;
                    case "Beauty":
                        leftP.hasOuterBeauty = true;
                        rightP.hasInnerBeauty = true;
                        break;
                    case "SmartDumb":
                        leftP.isSmart = true;
                        rightP.isDumb = true;
                        break;
                    default:
                        Console.WriteLine("En tiedä mitä valitsit :(");
                        break;
                }

                choicesMade += 1;
                if(choicesMade < maxChoices)
                {
                    getNewCards();
                }
                else
                {
					switch(GameObject.Find("Stats-" + player).GetComponent<PlayerStats>().map)
					{
						case "Open":
							SceneManager.LoadScene("Map1");
							break;
						case "Less platforms but more walls":
							SceneManager.LoadScene("Map2");
							break;
						case "Maze":
							SceneManager.LoadScene("map3");
							break;
						case "More platforms but less walls":
							SceneManager.LoadScene("map4");
							break;
						default:
							SceneManager.LoadScene(0);
							break;
					}
                }
            }
            
        }

        if (direction < 0) eventSys.SetSelectedGameObject(leftCard);
        else if (direction > 0) eventSys.SetSelectedGameObject(rightCard);

        
    }

    private int nextPlayer()
    {
        if (player == 1) return 2;
        else return 1;
    }

    private void getNewCards()
    {
        instruction.text = "Player " + player + " make your choice";

        int choiceN = random.Next(0, cardPairs.Count);

        currentCards = cardPairs.ElementAt(choiceN).GetComponent("CardChoice") as CardChoice;

        cardPairs.RemoveAt(choiceN);

        leftCard.GetComponentInChildren<Text>().text = currentCards.choice1;
        rightCard.GetComponentInChildren<Text>().text = currentCards.choice2;

        eventSys.SetSelectedGameObject(leftCard);
    }
}
