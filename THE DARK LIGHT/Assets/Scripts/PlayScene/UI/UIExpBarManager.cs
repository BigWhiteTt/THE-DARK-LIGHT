using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIExpBarManager : MonoBehaviour {
    public static UIExpBarManager Instance;

    public Slider ExpSlider;
    private void Awake()
    {
        Instance = this;
    }
    
    public void UpdateSlider()
    {
        ExpSlider.value = PlayerStatus.Instance.Exp / (float)PlayerStatus.Instance.LevelUpExp;
    }
}
