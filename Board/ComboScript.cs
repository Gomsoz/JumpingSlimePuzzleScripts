using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboScript : MonoBehaviour
{
    public Action OnCombo = null;
    private enum ComboState
    {
        x1,
        x2,
        x3,
        x4,
        x5
    } ComboState state = ComboState.x1;

    public Slider comboSlider;
    public Text comboText;
    public Text comboBonusText;

    private string comboString;

    public int comboCnt;
    public int maxCombo;

    public int comboBonus;

    float comboTime;
    float minVal;
    float maxVal;
    float comboSpeed;
    float currentVal;

    bool isCombo;
    
    // Start is called before the first frame update
    void Start()
    {
        ComboInit();
    }

    void Update()
    {
        if(isCombo == true && currentVal > 0)
            OngingCombo();
    }

    public void ComboInit()
    {
        comboBonus = 1;

        comboCnt = 0;
        minVal = comboSlider.minValue;
        maxVal = comboSlider.maxValue;
        currentVal = maxVal;
        comboTime = 3;

        comboSpeed = (maxVal - minVal) / comboTime;

        isCombo = false;
    }

    public void StartCombo()
    {
        // 콤보가 진행중인지 확인
        if (isCombo)
        {
            // 콤보 카운트를 증가시킨다.
            ComboCount();
            // 콤보 카운트를 토대로 보너스가 변경되었는지 확인한다.
            if (CheckComboBonus())
            {
                ChangeComboText();
                Managers.Sound.Play($"Sounds/ComboSound", 1f, Defines.SoundType.ComboSound);
            }
            comboString = string.Format("{0,3} Combo!", comboCnt);
            comboText.text = comboString;
            isCombo = false;
        }
        currentVal = maxVal;
        isCombo = true;
    }

    public void OngingCombo()
    {
        // 콤보 시간을 관리하는 함수
        // Update 문에서 슬라이더의 값을 계속 변경시켜 준다.
        currentVal = currentVal - (comboSpeed * Time.deltaTime);
        // 현재 남은 시간(currentValue)이 0이면 콤보를 취소시킨다.
        if(currentVal < 0)
        {
            comboCnt = 0;
            comboText.text = null;
            if (CheckComboBonus())
                ChangeComboText();
        }
        comboSlider.value = currentVal;
    }

    public void ComboCount()
    {
        // 콤보를 증가시키고 최대 콤보를 저장한다.
        // 남은 시간(currentValue)이 0이면 콤보를 0으로 만든다.
        if (currentVal > 0)
        {
            comboCnt++;
            if(comboCnt > maxCombo)
                maxCombo = comboCnt;
        }
        else if (currentVal < 0)
            comboCnt = 0;
    }

    public bool CheckComboBonus()
    {
        ComboState tempState = state;

        if (comboCnt >= 40)
            state = ComboState.x5;
        else if (comboCnt >= 30)
            state = ComboState.x4;
        else if (comboCnt >= 20)
            state = ComboState.x3;
        else if (comboCnt >= 10)
            state = ComboState.x2;
        else if (comboCnt < 10)
            state = ComboState.x1;

        if (tempState == state)
            return false;

        switch (state)
        {
            case ComboState.x2:
                comboBonus = 2;
                break;
            case ComboState.x3:
                comboBonus = 3;
                break;
            case ComboState.x4:
                comboBonus = 4;
                break;
            case ComboState.x5:
                comboBonus = 5;
                break;
            default:
                comboBonus = 1;
                break;
        }
        return true;
    }

    public void ChangeComboText()
    {
        switch (state)
        {
            case ComboState.x2:
                comboBonusText.text = "x2";
                comboBonusText.fontSize = 150;
                comboBonusText.color = Color.green;
                break;
            case ComboState.x3:
                comboBonusText.text = "x3";
                comboBonusText.fontSize = 200;
                comboBonusText.color = Color.yellow;
                break;
            case ComboState.x4:
                comboBonusText.text = "x4";
                comboBonusText.fontSize = 250;
                comboBonusText.color = Color.magenta;
                break;
            case ComboState.x5:
                comboBonusText.text = "x5";
                comboBonusText.fontSize = 300;
                comboBonusText.color = Color.red;
                break;
            default:
                comboBonusText.text = null;
                break;
        }
    }
}
