using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Sprite[] blocks;
    public AudioSource[] blockAudio;

    private void Start()
    {
        BlockInit();
    }

    public void BlockInit()
    {
        GameManager.GameMgr.SuccessBlock += BlockSound;
    }

    public void BlockSound(bool isOk)
    {
        if (isOk)
        {
            Managers.Sound.Play($"Sounds/GoodBlock", 1f, Defines.SoundType.BlockSound);
        }
        else
        {
            Managers.Sound.Play($"Sounds/BadBlock", 1f, Defines.SoundType.BlockSound);
        }
    }
}
