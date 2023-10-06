using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEntryUI : MonoBehaviour
{
    #region Events

    public event Action<LevelConfig> OnSelected;

    #endregion

    [SerializeField] private TextMeshProUGUI levelNameText;
    [SerializeField] private TextMeshProUGUI levelDescriptionText;
    [SerializeField] private Image levelImage;
    [SerializeField] private Image lockImage;
    [SerializeField] private Color lockedColor;

    private LevelConfig _levelConfig;

    public void Initialize(LevelConfig levelConfig, bool unlocked)
    {
        _levelConfig = levelConfig;
        
        levelNameText.text = levelConfig.LevelName;
        levelDescriptionText.text = levelConfig.LevelDescription;
        levelImage.sprite = levelConfig.LevelImage;
        
        lockImage.gameObject.SetActive(!unlocked);
        if(!unlocked)
            levelImage.color = lockedColor;
    }

    public void Select()
    {
        OnSelected?.Invoke(_levelConfig);
    }
}    