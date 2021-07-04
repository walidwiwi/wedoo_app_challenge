using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassageHandler : MonoBehaviour
{
    public GameObject msgExampleMachine , msgExampleUser ;
    public GameObject msgImageExemple; 
    public Transform msgsContinerMachine;
    public static MassageHandler instance;
    private string[] repens =
    { 
        "hi im robot" ,
        "im a robot to help",
        "im in Valmascort now",
        "about : 1min",
        "about :",
        "i dont understand what you mean! \n try to ask me quations like \n 'time' \n 'who are you' \n 'where are you'"

    };
    private string[] msg =
    {
        "hi" ,
        "who are you" ,
        "where are you" ,
        "when my order arrive",
        "time",


    };

    public void SendMessage(string msg)
    {

        MakeMsg(2, msg); 
        StartCoroutine( getRepens(msg) ); 
    }

    private void Start()
    {
        instance = this; 
    }
    public IEnumerator getRepens(string qst)
    {
        int indexOdRepen = repens.Length-1;
        

        for (int i = 0; i < msg.Length; i++)
        {
            if(msg[i].Equals(qst , System.StringComparison.OrdinalIgnoreCase))
            {
                indexOdRepen = i;
                break; 
            }
        }

        yield return new WaitForSeconds(.5f);
        
        if(indexOdRepen == 2)
        {
            MakeMsg(1 , repens[indexOdRepen] );
            MakeMsgImage(  exemple_image); 
        }else
            MakeMsg(1, repens[indexOdRepen]);
    }
    public Sprite exemple_image; 
    public void MakeMsgImage(Sprite image)
    {
        GameObject msgInstance;
 
        msgInstance = Instantiate(msgImageExemple, msgsContinerMachine);
 
        Message m = msgInstance.GetComponent<Message>();
        //m.image.sprite = image; 
    }
    public void MakeMsg(int source , string msg)
    {
        GameObject msgInstance; 
        if (source == 1)
        {
            msgInstance = Instantiate(msgExampleMachine, msgsContinerMachine);
        }else
        {
            msgInstance = Instantiate(msgExampleUser, msgsContinerMachine);
        }
        Message m = msgInstance.GetComponent<Message>();
        m.msg = msg; 

    }
}
