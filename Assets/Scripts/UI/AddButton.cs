﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddButton : MonoBehaviour
{
    public Button button;
    public TMPro.TextMeshProUGUI title;
    public TMPro.TextMeshProUGUI description;
    public TMPro.TextMeshProUGUI version;
    public Image icon;

    private IModObject item;
    private ModScrollList scrollList;

    void Start()
    {
        button.onClick.AddListener(HandleClick);
    }

    public void Setup(IModObject currentMod, ModScrollList currentScrollList)
    {
        item = currentMod;
        item.ModLoad();
        title.text = item.Name;
        description.text = $"{item.Author}";
        version.text = $"Version: {item.Version.ToString()}";
        icon.sprite = item.Icon;
        scrollList = currentScrollList;
        if (scrollList.isActiveList)
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "-";
        else

            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "+";
    }

    public void HandleClick()
    {
        if (scrollList.isActiveList)
        {
            if (item.ModDeactivate())
            {
                button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "-";
                scrollList.TryTransferMod(item);
            }
            else
                scrollList.WarnIncompatible();
        }
        else if (!scrollList.isActiveList)
        {
            if (item.ModActivate())
            {
                button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "+";
                scrollList.TryTransferMod(item);
            }
            else
                scrollList.WarnIncompatible();
        }
    }
}
