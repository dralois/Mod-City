using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Item
{
    public string itemName;
    public string description;

}

public class ModScrollList : MonoBehaviour
{
    public List<IModObject> modList;
    public bool isActiveList;
    public Transform contentPanel;
    public ModScrollList otherList;
    public TMPro.TextMeshProUGUI additionalText;
    public SimpleObjectPool modObjectPool;

    void Start()
    {
        for (int i = modList.Count - 1; i >= 0; i--)
        {
            if(isActiveList)
            {
                if (!modList[i].IsActivated)
                    modList.RemoveAt(i);
            }else
            {
                if (modList[i].IsActivated)
                    modList.RemoveAt(i);
            }
        }
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        RemoveButtons();
        AddButtons();
    }

    private void AddButtons()
    {
        foreach (var mod in modList)
        {
            if (isActiveList && mod.IsActivated)
            {
                GameObject newPanel = modObjectPool.GetObject();
                newPanel.transform.SetParent(contentPanel);

                AddButton sampleButton = newPanel.GetComponent<AddButton>();
                sampleButton.Setup(mod, this);
            }
            else if (!isActiveList && !mod.IsActivated)
            {
                GameObject newPanel = modObjectPool.GetObject();
                newPanel.transform.SetParent(contentPanel);

                AddButton sampleButton = newPanel.GetComponent<AddButton>();
                sampleButton.Setup(mod, this);
            }
        }
    }

    public void TryTransferMod(IModObject mod)
    {
        AddMod(mod, otherList);
        RemoveMod(mod, this);
        RefreshDisplay();
        otherList.RefreshDisplay();
    }

    private void AddMod(IModObject mod, ModScrollList modList)
    {
        modList.modList.Add(mod);
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            modObjectPool.ReturnObject(toRemove);
        }

    }

    private void RemoveMod(IModObject mod, ModScrollList modList)
    {
        for (int i = modList.modList.Count - 1; i >= 0; i--)
        {
            if (modList.modList[i] == mod)
                modList.modList.RemoveAt(i);
        }
    }
}
