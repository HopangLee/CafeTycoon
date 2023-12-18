using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class PointerDownUpHandler : Button
{
    private SteamManager steamManager;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        steamManager?.onPressedButton();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        steamManager?.onReleasedButton();
    }

    public void setSteamManager(SteamManager _s)
    {
        if (steamManager == null) steamManager = _s;
    }
}
