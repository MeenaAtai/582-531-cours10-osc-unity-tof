using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;


public class NewBehaviourScript : MonoBehaviour
{
    public extOSC.OSCReceiver oscReceiver;
    public static float Proportion(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return Mathf.Clamp(((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin), outputMin, outputMax);
    }

    void TraiterOscAngle(OSCMessage message)
    {
        // Si le message n'a pas d'argument ou l'argument n'est pas un Int on l'ignore
        if (message.Values.Count == 0)
        {
            Debug.Log("No value in OSC message");
            return;
        }

        if (message.Values[0].Type != OSCValueType.Int)
        {
            Debug.Log("Value in message is not an Int");
            return;
        }

        // Récupérer la valeur de l’angle depuis le message OSC
        int value = message.Values[0].IntValue;

        // EXEMPLE : utiliser la valeur pour appliquer une rotation
        // Adapter proportionnellement la valeur reçue
        float angle = Proportion(value, 0, 4095, -180, 180);
        // Appliquer la rotation à l’objet
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        oscReceiver.Bind("/angle", TraiterOscAngle);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
