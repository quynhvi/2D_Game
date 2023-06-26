using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwap : MonoBehaviour
{
    public Transform character;
    public List<Transform> possibleCharacters;
    public int whichCharacter;
    public ParticleSystem m_ParticleSystem;
    public CameraFollow cam;

    void Start()
    {
        if (possibleCharacters.Count > 0)
        {
            whichCharacter = 0;
            character = possibleCharacters[whichCharacter];
            character.GetComponent<PlayerMovement>().enabled = true;

            m_ParticleSystem.transform.position = character.position;
            m_ParticleSystem.Play();

            for (int i = 1; i < possibleCharacters.Count; i++)
            {
                possibleCharacters[i].GetComponent<PlayerMovement>().enabled = false;
            }

            cam.SetTarget(character);
        }
        Swap();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (whichCharacter == 0)
            {
                whichCharacter = possibleCharacters.Count - 1;
            }
            else
            {
                whichCharacter -= 1;
            }
            Swap();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (whichCharacter == possibleCharacters.Count - 1)
            {
                whichCharacter = 0;
            }
            else
            {
                whichCharacter += 1;
            }
            Swap();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToCharacter(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToCharacter(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchToCharacter(2);
        }
    }

    void SwitchToCharacter(int index)
    {
        if (index >= 0 && index < possibleCharacters.Count)
        {
            character.GetComponent<PlayerMovement>().enabled = false;
            character = possibleCharacters[index];
            character.GetComponent<PlayerMovement>().enabled = true;

            m_ParticleSystem.transform.position = character.position; // spawn at player
            m_ParticleSystem.Play();

            // Update the currentPlayerLayer in the Platform script
            var platformObjects = GameObject.FindGameObjectsWithTag("Platform");
            foreach (var platformObject in platformObjects)
            {
                var platformScript = platformObject.GetComponent<Platform>();
                platformScript.currentPlayerLayer = character.gameObject.layer;
            }

            cam.SetTarget(character); // Update the camera's target to the newly selected character
        }
    }

    public void Swap()
    {
        character.GetComponent<PlayerMovement>().enabled = false;
        character = possibleCharacters[whichCharacter];
        character.GetComponent<PlayerMovement>().enabled = true;

        m_ParticleSystem.transform.position = character.position;
        m_ParticleSystem.Play();

        cam.SetTarget(character);

        for (int i = 0; i < possibleCharacters.Count; i++)
        {
            if (i != whichCharacter)
            {
                possibleCharacters[i].GetComponent<PlayerMovement>().enabled = false;
            }
        }
    }
}