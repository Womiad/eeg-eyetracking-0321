using UnityEngine;
using UnityEngine.UI;

public class ButtonDisableWhenConfirm : MonoBehaviour
{
    public GameObject UI_Confirm;

    Button btn;
    bool prevValue;
    bool isConfirming = false;

    private void Awake()
    {
        btn = this.GetComponent<Button>();
    }

    private void Start()
    {
        prevValue = btn.interactable;
    }

    private void Update()
    {
        // if (UI_Confirm.activeSelf == false && isConfirming)
        // {
        //     isConfirming = false;
        //     btn.interactable = prevValue;
        // }
        // else if (UI_Confirm.activeSelf == true && !isConfirming)
        // {
        //     isConfirming = true;
        //     prevValue = btn.interactable;
        //     btn.interactable = false;
        // }
    }
}
