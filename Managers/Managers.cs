using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class Managers : MonoBehaviour
{
    private static Managers s_instance;
    public static Managers Instance { get { return s_instance; } }

    #region Property
    private static ResourcesManager _resources = new ResourcesManager();
    private static SoundManager _sound = new SoundManager();
    private static InputManager _input = new InputManager();

    public static ResourcesManager Resources { get { return _resources; } }
    public static SoundManager Sound { get { return _sound; } }
    public static InputManager Input { get { return _input; } }
    #endregion

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        if(s_instance == null)
        {
            GameObject findGo = GameObject.Find("@Managers");
            if(findGo == null)
            {
                findGo = new GameObject { name = "@Managers" };
                findGo.AddComponent<Managers>();
            }

            s_instance = findGo.GetComponent<Managers>();
            DontDestroyOnLoad(findGo);
        }

        _resources = new ResourcesManager();
        _sound = new SoundManager();
        _input = new InputManager();
        Sound.Init();

        Managers.Sound.Play("Sounds/BGM", 1f, Defines.SoundType.Bgm);
    }
}
