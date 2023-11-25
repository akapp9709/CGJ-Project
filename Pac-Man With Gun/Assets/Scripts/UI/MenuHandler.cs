using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> hiddenObjects;

    public void OpenMenu(GameObject menuItem)
    {
        menuItem.SetActive(true);
        menuItem.GetComponent<MenuHandler>().HideObjects();
    }

    public void HideObjects()
    {
        foreach (var obj in hiddenObjects)
        {
            obj.SetActive(false);
        }
    }
}
