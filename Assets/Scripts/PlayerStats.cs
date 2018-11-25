using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    //Map
    public string map = "";

    //Size, speed. weight
    public float sizeMod = 1f;
    public float speedMod = 1f;
    public float fallMult = 1.6f;

    //Health
    public float hpMod = 1f;

    //Weapon mods
    public string weapon = "";
    public float damageMod = 1f;
    public float fireRateMod = 1f;
    public float rangeMod = 1f;
    public float accuracyMod = 1f;

    //Abilities
    public bool hasDash = false;
    public bool hasDoubleJump = false;
    public bool hasNoJump = false;

    //Invert controls
    public bool invertVertical = false;
    public bool invertHorizontal = false;

    //Cosmetics
    public bool hasInnerBeauty = false;
    public bool hasOuterBeauty = false;
    public bool isSmart = false;
    public bool isDumb = false;

	//Score
	public int score = 0;
}
