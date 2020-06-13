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
    public Transform contentPanel;
    public ModScrollList otherList;
    public TMPro.TextMeshProUGUI additionalText;
    public SimpleObjectPool modObjectPool;

    void Start()
    {
        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        AddButtons();
    }
    private void AddButtons()
    {
        for (int i = 0; i < modList.Count; i++)
        {
            IModObject item = modList[i];
            GameObject newPanel = modObjectPool.GetObject();
            newPanel.transform.SetParent(contentPanel);

            AddButton sampleButton = newPanel.GetComponent<AddButton>();
            sampleButton.Setup(item, this);
        }
    }
}
