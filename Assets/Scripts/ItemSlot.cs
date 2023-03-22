using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image m_icon;

    [SerializeField]
    private TextMeshProUGUI m_label;

    // [SerializableField]
    // private GameObject m_stackObj;

    [SerializeField]
    private TextMeshProUGUI m_stackLabel;

    public void Set(InventoryItem item) {
        m_icon.sprite = item.data.sprite;
        m_label.text = item.data.type;

        // if(item.stackSize <= 1) {
        //     m_stackObj.SetActive(false);
        //     return;
        // }

        m_stackLabel.text = item.stackSize.ToString();

    }

}
