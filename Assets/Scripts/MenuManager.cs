using System;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    Dictionary<Type, Menu> menus = new Dictionary<Type, Menu>();
    private Menu currentMenu;
    public static MenuManager instance;

    private void OnEnable()
    {
        Debug.Log("in on enable");
        if (instance != null)
        {
            Debug.LogError("Duplicate of MenuManager");
        }
        
        instance = this;
        DontDestroyOnLoad(instance);
    }

    private void Awake()
    {
        var menuComponents = GetComponentsInChildren<Menu>();

        foreach (var component in menuComponents)
        {
            menus[component.GetType()] = component;
            component.gameObject.SetActive(false);
        }
    }

    public void ChangeMenu<T>() where T : Menu
    {
        if (currentMenu)
        {
            currentMenu.gameObject.SetActive(false);
        }

        currentMenu = menus[typeof(T)]; 
        currentMenu.gameObject.SetActive(true);
        
    }
}
