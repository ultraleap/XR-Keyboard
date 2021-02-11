using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SetChildColourBlocks : MonoBehaviour
{
    public ColorBlock colorBlock;
    // Start is called before the first frame update
    void Start()
    {
        SetColours();
    }
    private void OnEnable()
    {
        GetComponentsInChildren<Button>().ToList().ForEach(b => b.onClick.AddListener(Unselect));
    }

    private void Unselect()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
    private void OnDisable()
    {
        GetComponentsInChildren<Button>().ToList().ForEach(b => b.onClick.RemoveListener(Unselect));
    }

    [Button]
    private void SetColours()
    {
        GetComponentsInChildren<Button>().ToList().ForEach(b => b.colors = colorBlock);

    }
}
