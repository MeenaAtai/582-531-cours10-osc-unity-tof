using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OscPalette : MonoBehaviour
{
    public extOSC.OSCReceiver oscReceiver;
    public static float Proportion(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return Mathf.Clamp(((value - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin), outputMin, outputMax);
    }
    void TraiterOscMesure(OSCMessage message)
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

        if (value > 450)
        {
            transform.position = new Vector3(-6, 0, 0);
        }
        else
        {
            // EXEMPLE : utiliser la valeur pour appliquer une rotation
            // Adapter proportionnellement la valeur reçue
            float mesure = Proportion(value, 0, 400, -4, 4);
            // Appliquer la rotation à l’objet
            transform.position = new Vector3(-6, mesure, 0);

        }

    }
    // Start is called before the first frame update
    void Start()
    {
        oscReceiver.Bind("/mesure", TraiterOscMesure);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
