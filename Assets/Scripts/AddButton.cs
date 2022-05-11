using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButton : MonoBehaviour
{
    [SerializeField]
    private Transform gamePanel;

    [SerializeField]
    private GameObject button;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject btn = Instantiate(button);
            btn.name = "" + i;
            btn.transform.SetParent(gamePanel, false);
        }
    }

}
