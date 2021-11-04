using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class UGUITweener : MonoBehaviour
{
    public float duration = 1;
    public float startDelay = 0;
    public bool isForwarded = false;

    public float t = 0;
    public bool isFinished = true;
    public Action OnFinished;

    private WaitForSeconds waitDuration;
    private WaitForSeconds waitDurationHalf;

    private void Awake()
    {
        waitDuration = new WaitForSeconds(duration);
        waitDurationHalf = new WaitForSeconds(duration*0.5f);
    }

    private void Update()
    {
        if (isFinished == false)
        {
            if (t <= duration)
            {
                t += Time.deltaTime;
                //var value = Mathf.Clamp((t / duration) * (isForwarded ? 1 : 0.1f), 0, 1);
                var value = (isForwarded ? (t*2 / duration) : 1f) - (t / duration);
                OnUpdate(value);
            }
            else
            {
                isFinished = true;
                if (OnFinished != null)
                {
                    OnFinished();
                }
            }
        }
    }

    public virtual void PlayForward()
    {
        SetTweener(true);
    }

    public virtual void PlayReverse()
    {
        SetTweener(false);
    }

    public virtual void PlayToggle()
    {
        if(isForwarded)
        {
            PlayReverse();
        }
        else
        {
            PlayForward();
        }
    }

    public virtual IEnumerator PlayPingPong()
    {
        PlayForward();
        yield return waitDurationHalf;
        PlayReverse();

        //while (true)
        //{
        //    PlayForward();
        //    yield return waitDuration;
        //    PlayReverse();
        //}
    }

    private void SetTweener(bool _isForwarded)
    {
        t = 0;
        isFinished = false;
        isForwarded = _isForwarded;
    }

    abstract protected void OnUpdate(float t);

    //[ContextMenu("Set 'From' to current value")]
    public virtual void SetStartToCurrentValue() { }

    //[ContextMenu("Set 'To' to current value")]
    public virtual void SetEndToCurrentValue() { }
}
