using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeCtr : MonoBehaviour
{
    public int ID;

    public NodeCtr Pervious;

    public NodeCtr Next;

    public List<SubNode> subNodes = new List<SubNode>();

    public Animator animator;

    public delegate void OnDisplay(int SubNodeID);
    public event OnDisplay OnDisplayEvent;

    public delegate void OnDisplayFullScreen(int SubNodeID);
    public event OnDisplayFullScreen OnDisplayFullScreenEvent;

    public delegate void OnDisplayFinished();
    public event OnDisplayFinished OnDisplayFinishedEvent;

    private bool IsCurrenOnDisplay;

    int currentSubNodeDisplayID = 0;


    private void Awake()
    {
        OnDisplayEvent += fun_OnDisplayEvent;
        OnDisplayFullScreenEvent += fun_OnDisplayFullScreenEvent;
        OnDisplayFinishedEvent += fun_OnDisplayFinishedEvent;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < subNodes.Count; i++)
        {
            if (i == 0)
            {
                subNodes[i].Invoke_MoveInEvent();

            }
            else {
                subNodes[i].Invoke_MoveAwayEvent();

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            invoke_OnDisplayEvent(0);

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            invoke_OnDisplayFullScreenEvent(0);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            invoke_OnDisplayFinishedEvent();
        }

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    invoke_OnDisplayEvent(1);

        //}

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    invoke_OnDisplayFullScreenEvent(1);
        //}


    }

    public void invoke_OnDisplayEvent(int subNodeId)
    {

       OnDisplayEvent.Invoke(subNodeId);
    }

    public void invoke_OnDisplayFullScreenEvent(int subNodeId)
    {
        OnDisplayFullScreenEvent.Invoke(subNodeId);
    }

    public void invoke_OnDisplayFinishedEvent() {
        OnDisplayFinishedEvent.Invoke();
    }


    private void fun_OnDisplayFinishedEvent() {
        for (int i = 0; i < subNodes.Count; i++)
        {
            if (i == 0) {
                subNodes[i].Invoke_MoveInEvent();
            }
            else{
                subNodes[i].Invoke_MoveAwayEvent();
            }
        }

        MoveBackDefault();
    }

    private void fun_OnDisplayFullScreenEvent(int subNodeId) {
        subNodes[subNodeId].Invoke_ShowFullScreenEvent();
    }


    private void fun_OnDisplayEvent(int subNodeId) {
        for (int i = 0; i < subNodes.Count; i++)
        {
            if (i == subNodeId)
            {

                subNodes[i].Invoke_MoveInEvent();

            }
            else {
                if (currentSubNodeDisplayID != subNodeId)
                {
                    subNodes[i].Invoke_MoveAwayEvent();
                    currentSubNodeDisplayID = subNodeId;
                }

            }
        }
        subNodes[subNodeId].Invoke_ShowDefaultEvent();

        if (Pervious != null)
        {
            Pervious.MoveLeft();
        }

        if (Next != null) {
            Next.MoveBackDefault();
        }

    }


    private void MoveLeft() {
        animator.SetBool("IsMoveLeft", true);
    }

    private void MoveBackDefault() {
        animator.SetBool("IsMoveLeft", false);

    }

    public bool GetIsCurrenOnDisplay()
    {
        return IsCurrenOnDisplay;
    }

    public void SetIsCurrenOnDisplay(bool b)
    {
        IsCurrenOnDisplay = b;
    }

    // current DisplayID
    public bool ShoildIMoveLeft(int id) {

        if (id > ID)
        {
            return true;
        }
        else {
          return  false;
        }


    }
}
