using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubNode : MonoBehaviour
{
    public string id;
    public NodeCtr parentNodeCtr;
    private bool IsFullScreen;
    private bool IsCurrenOnDisplay;

    public RectTransform fuScreenTrans;
    public Animator[] animators;
    public Animator SubNodeAnimator;

    public delegate void showDefault();
    public delegate void showFullScreen();
    public delegate void moveAway();
    public delegate void moveIn();

    public event showDefault ShowDefaultEvent;
    public event showFullScreen ShowFullScreenEvent;
    public event moveAway MoveAwayEvent;
    public event moveIn MoveInEvent;


    // Start is called before the first frame update
    void Awake()
    {
        SetFullScreenNodePosition();

        ShowDefaultEvent += fun_ShowDefaultEvent;

        ShowFullScreenEvent += fun_ShowFullScreenEvent;

        MoveAwayEvent += fun_MoveAwayEvent;

        MoveInEvent += fun_MoveInEvent;
    }

    public void Invoke_ShowDefaultEvent() {
        ShowDefaultEvent.Invoke();
    }

    public void Invoke_ShowFullScreenEvent()
    {
        ShowFullScreenEvent.Invoke();
    }

    public void Invoke_MoveAwayEvent()
    {
        MoveAwayEvent.Invoke();
    }

    public void Invoke_MoveInEvent()
    {
        MoveInEvent.Invoke();
    }



    private void fun_MoveInEvent() {
        Debug.Log(id.ToString() + "MoveIn");
            MoveSubNodeIn();
        HideFullScreen();
        ShowDefault();
    }

    private void fun_MoveAwayEvent() {
        Debug.Log(id.ToString() + "MoveOut");

            MoveSubNodeOut();

        HideDefault();
        HideFullScreen();
    }

    private void fun_ShowFullScreenEvent() {
        Debug.Log(id.ToString() + "FullScreen");

            ShowFullScreen();
            HideDefault();


    }

    private void fun_ShowDefaultEvent() {
        Debug.Log(id.ToString() +"ShowDefault");
       // Debug.Log(id.ToString() + "Try to ShowDefault: 是否全屏" + GetIsFullScreen().ToString());

           // Debug.Log(id.ToString()+"执行回到默认");
            HideFullScreen();

        ShowDefault();

    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    Invoke_ShowDefaultEvent();

        //}

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Invoke_ShowFullScreenEvent();
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    Invoke_MoveAwayEvent();
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    Invoke_MoveInEvent();
        //}
    }

    public bool GetIsFullScreen()
    {
        return IsFullScreen;
    }

    public void SetIsFullScreen(bool b)
    {
        IsFullScreen = b;
    }

    public bool GetIsCurrenOnDisplay()
    {
        return IsCurrenOnDisplay;
    }

    public void SetIsCurrenOnDisplay(bool b)
    {
        IsCurrenOnDisplay = b;
    }

    public void SetFullScreenNodePosition() {
      float x =   -(ValueSheet.reslution.x / 2 + parentNodeCtr.GetComponent<RectTransform>().localPosition.x);
        float y = -ValueSheet.reslution.y / 2;

        fuScreenTrans.localPosition = new Vector3(x, y, 0);
    }


    private void ShowFullScreen() {
        animators[1].SetBool("ShowFullScreen",true);
        SetIsFullScreen(true);

    }

    private void HideFullScreen() {
        animators[1].SetBool("ShowFullScreen",false);
        SetIsFullScreen(false);
    }

    private void ShowDefault() {
        animators[0].SetBool("HideDefault",false);
    }

    private void HideDefault() {
        animators[0].SetBool("HideDefault",true);

    }

    private void MoveSubNodeIn() {
        SetIsCurrenOnDisplay(true);
        SubNodeAnimator.SetBool("IsMoveIn", true);
    }

    private void MoveSubNodeOut() {
        SetIsCurrenOnDisplay(false);
        SubNodeAnimator.SetBool("IsMoveIn", false);

    }
}
