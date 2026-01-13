using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPlayerController : MonoBehaviour
{
    void Start()
    {
        // Makes cursor visiable and allows the player to move the mouse when in the shop
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
