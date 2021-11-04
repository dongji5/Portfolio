using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UGUITweenRectSize : UGUITweener
{
    [SerializeField] RectTransform rt;
    public Vector2 from;
    public Vector2 to;

    public Vector2 value
    {
        get
        {
            if (rt == null)
            {
                rt = GetComponent<RectTransform>();
            }
            return rt.sizeDelta;
        }
        set
        {
            if (rt == null)
            {
                rt = GetComponent<RectTransform>();
            }
            rt.sizeDelta = value;
        }
    }

    public override void PlayForward()
    {
        base.PlayForward();
    }

    public override void PlayReverse()
    {
        base.PlayReverse();
    }

    public override void PlayToggle()
    {
        base.PlayToggle();
    }

    [ContextMenu("Set 'From' to current value")]
    public override void SetStartToCurrentValue() { from = value; }

    [ContextMenu("Set 'To' to current value")]
    public override void SetEndToCurrentValue() { to = value; }

    protected override void OnUpdate(float t) { value = Vector2.Lerp(from, to, t); }
}
