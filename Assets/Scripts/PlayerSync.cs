using UnityEngine;
using UnityEngine.Networking;

public class PlayerSync : NetworkBehaviour
{
    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetButtonDown("Sync"))
        {
            CmdSync();
        }
    }


    [Command]
    void CmdSync()
    {
        RpcSync();
    }


    [ClientRpc]
    void RpcSync()
    {
        Logger.Event("Syncing");
        LoudSyncingSound();
    }


    void LoudSyncingSound()
    {
        audioSource.Play();
    }
}
