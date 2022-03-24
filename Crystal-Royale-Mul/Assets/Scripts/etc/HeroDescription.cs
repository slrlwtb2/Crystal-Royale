using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroDescription : MonoBehaviour
{
    [SerializeField]
    private GameObject hname;

    [SerializeField]
    private GameObject hskill1;

    [SerializeField]
    private GameObject hskill2;

    [SerializeField]
    private GameObject heroes;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (heroes.GetComponent<CharacterSelection>().characters[heroes.GetComponent<CharacterSelection>().selectedCharacter].name=="Sword")
        {
            hname.GetComponent<Text>().text = "Sword";
            hskill1.GetComponent<Text>().text = "1.Steriod: Increased Movement Speed";
            hskill2.GetComponent<Text>().text = "2.Big slash: Strike wave of a sword in the direction of the player's mouse, dealing massive damage";


        }
        if (heroes.GetComponent<CharacterSelection>().characters[heroes.GetComponent<CharacterSelection>().selectedCharacter].name == "Blade")
        {
            hname.GetComponent<Text>().text = "Blade";
            hskill1.GetComponent<Text>().text = "1.Berserk:  Dashes forward then increases his attack speed immensely";
            hskill2.GetComponent<Text>().text = "2.Spin blade: Spins his sword in a radius around him, dealing massive damage";
        }
        if (heroes.GetComponent<CharacterSelection>().characters[heroes.GetComponent<CharacterSelection>().selectedCharacter].name == "Bow")
        {
            hname.GetComponent<Text>().text = "Bow";
            hskill1.GetComponent<Text>().text = "1.Dodge: roll backward from the current position";
            hskill2.GetComponent<Text>().text = "2.Fire burst: increased attack speed";
        }
        if (heroes.GetComponent<CharacterSelection>().characters[heroes.GetComponent<CharacterSelection>().selectedCharacter].name == "Fire")
        {
            hname.GetComponent<Text>().text = "Fire";
            hskill1.GetComponent<Text>().text = "1.Direct flame: Release a high-speed fireball. inflict damage on enemies";
            hskill2.GetComponent<Text>().text = "2.Meteor strike: Releases a meteor when it hits the ground, creating a large area of ​​damage";
        }
        if (heroes.GetComponent<CharacterSelection>().characters[heroes.GetComponent<CharacterSelection>().selectedCharacter].name == "Monkey")
        {
            hname.GetComponent<Text>().text = "Monkey";
            hskill1.GetComponent<Text>().text = "1.Roll: roll forward";
            hskill2.GetComponent<Text>().text = "2.Jump: jump in the air. If use in combination with Roll, it can travel longer distances";
        }
        if (heroes.GetComponent<CharacterSelection>().characters[heroes.GetComponent<CharacterSelection>().selectedCharacter].name == "Paladin")
        {
            hname.GetComponent<Text>().text = "Paladin";
            hskill1.GetComponent<Text>().text = "1.Steriod: Increased Movement Speed";
            hskill2.GetComponent<Text>().text = "2.Hammer down: Slams the ground dealing damage and makes enemies unable to move";
        }
        if (heroes.GetComponent<CharacterSelection>().characters[heroes.GetComponent<CharacterSelection>().selectedCharacter].name == "Sky")
        {
            hname.GetComponent<Text>().text = "Sky";
            hskill1.GetComponent<Text>().text = "1.Shooting star: release a large number of star meteors";
            hskill2.GetComponent<Text>().text = "2.Sky strike: Massively increases the movement speed";
        }
    }
}
