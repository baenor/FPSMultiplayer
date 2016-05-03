using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KillTextBehaviour : MonoBehaviour {

    string killer;
    string victim;
    string killMessage;

    public Text killText;

    Animator anim;

    float timePassed;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        timePassed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (anim.GetBool("NewDead"))
        {
            timePassed += Time.deltaTime;
            if(timePassed > 2f)
            {
                anim.SetBool("NewDead", false);
                anim.SetBool("Reverse", true);
                timePassed = 0f;
            }
        }
	}

    //PlayerDamage --> OnDestroy()
    public void SetKillerAndVictim(string kName, string vName)
    {
        if(kName == null)
        {
            //suicidio
            victim = vName;
            killMessage = victim + " committed suicide.";
        }
        else
        {
            //omicidio
            killer = vName;
            victim = vName;
            killMessage = killer + " has killed " + victim + ".";
        }

        killText.text = killMessage;

        if (anim.GetBool("NewDead"))
        {
            timePassed = 0f;
        }
        else
        {
            anim.SetBool("Reverse", false);
            anim.SetBool("NewDead", true);
        }
    }
}
