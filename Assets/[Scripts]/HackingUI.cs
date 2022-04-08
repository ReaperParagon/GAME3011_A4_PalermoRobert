using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HackingUI : MonoBehaviour
{
    public List<string> difficultyLevelNames = new List<string>();
    public TextMeshProUGUI difficultyText;
    public List<TextMeshProUGUI> playerSkillTexts = new List<TextMeshProUGUI>();
    public Slider difficultySlider;

    [SerializeField]
    private PlayerSkill playerSkill;

    private void Start()
    {
        SetHackingSkill(playerSkill.HackingLevel);
        difficultySlider.value = playerSkill.HackingLevel;
    }

    private void OnEnable()
    {
        HackingEvents.MiniGameStart += SetDifficultyText;
    }

    private void OnDisable()
    {
        HackingEvents.MiniGameStart -= SetDifficultyText;
    }

    public void SetDifficultyText(DifficultyLevel dl, PlayerSkill ps)
    {
        difficultyText.text = difficultyLevelNames[(int)dl];
    }

    public void SetHackingSkill(float ps)
    {
        playerSkill.HackingLevel = Mathf.FloorToInt(ps);

        for (int i = 0; i < playerSkillTexts.Count; i++)
        {
            playerSkillTexts[i].text = playerSkill.HackingLevel.ToString();
        }
    }
}
