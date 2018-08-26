using System;
using UnityEngine;
using UnityEngine.Networking;


public class TankManager : NetworkBehaviour
{
    // This class is to manage various settings on a tank.
    // It works with the GameManager class to control how the tanks behave
    // and whether or not players have control of their tank in the 
    // different phases of the game.

    [HideInInspector] public string m_PlayerName;
    [HideInInspector] public int m_PlayerNumber;            // This specifies which player this the manager for.
    [HideInInspector] public string m_ColoredPlayerText;    // A string that represents the player with their number colored to match their tank.
    [HideInInspector] public GameObject m_Instance;                          // A reference to the instance of the tank when it is created.
    [HideInInspector] public int m_Wins;                    // The number of wins this player has so far.


    private Transform m_SpawnPoint;                          // The position and direction the tank will have when it spawns.
    private TankMovement m_Movement;                        // Reference to tank's movement script, used to disable and enable control.
    private TankShooting m_Shooting;                        // Reference to tank's shooting script, used to disable and enable control.
    private GameObject m_CanvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.
    


    // Use this for initialization
    void Start()
    {
        m_Instance = gameObject;

        m_Movement = m_Instance.GetComponent<TankMovement>();
        m_Shooting = m_Instance.GetComponent<TankShooting>();
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

        // Set the player numbers to be consistent across the scripts.
        m_Movement.m_PlayerNumber = m_PlayerNumber;
        m_Shooting.m_PlayerNumber = m_PlayerNumber;

        // Create a string using the correct color that says 'PLAYER 1' etc based on the tank's color and the player's number.
        var m_PlayerColor = m_Instance.GetComponent<TankColor>().color;
        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">" + m_PlayerName + " </color>";
    }

    // Used during the phases of the game where the player shouldn't be able to control their tank.
    [ClientRpc]
    public void RpcDisableControl()
    {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;

        m_CanvasGameObject.SetActive(false);
    }

    // Used during the phases of the game where the player should be able to control their tank.
    [ClientRpc]
    public void RpcEnableControl()
    {
        m_Movement.enabled = true;
        m_Shooting.enabled = true;

        m_CanvasGameObject.SetActive(true);
    }


    // Used at the start of each round to put the tank into it's default state.
    [ClientRpc]
    public void RpcReset()
    {
        print("Resting");
        print("m_Instance iD: " + m_Instance.GetInstanceID());
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }

    [ClientRpc]
    public void RpcSpawnPoint(Vector3 position, Quaternion rotation) {
        m_SpawnPoint = new GameObject().transform;
        m_SpawnPoint.SetPositionAndRotation(position, rotation);
    }

}
