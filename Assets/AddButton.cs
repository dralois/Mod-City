using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AddButton : MonoBehaviour
{
    public Button button;
    public TMPro.TextMeshProUGUI title;
    public TMPro.TextMeshProUGUI description;
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
        description.text = $"{item.Author} created a mod with the version {item.Version} but will it work?";
        icon.sprite = item.Icon;
        scrollList = currentScrollList;
    }

    public void HandleClick()
    {
        if (scrollList.isActiveList && item.ModDisable())
        {
            scrollList.TryTransferMod(item);
        }
        else if (!scrollList.isActiveList && item.ModEnable())
        {
            scrollList.TryTransferMod(item);
        }
    }
}
