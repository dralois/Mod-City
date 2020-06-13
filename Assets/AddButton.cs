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
    
    public void ActivateMod()
    {
        item.ModEnable();

        // Debug.Log("activate Mod");
    }
}
