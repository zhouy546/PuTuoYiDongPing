using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCtr : MonoBehaviour
{
    public List<NodeCtr> nodeCtrs = new List<NodeCtr>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToTarget(string para) {

      string[] m_para=  para.Split('_');

        int NodeIndex = int.Parse(m_para[0]);

        int SubNodeIndex = int.Parse(m_para[1]);

        bool IsShowFullScreen = false;
        if (m_para[2] == "true")
        {
            IsShowFullScreen = true;
        }
        else {
            IsShowFullScreen = false;
        }


        for (int i = 0; i < nodeCtrs.Count; i++)
        {
            if (i == NodeIndex)
            {
                if (IsShowFullScreen)
                {
                    nodeCtrs[i].invoke_OnDisplayFullScreenEvent(SubNodeIndex);

                }
                else
                {
                    nodeCtrs[i].invoke_OnDisplayEvent(SubNodeIndex);

                }
            }
            else
            {
                nodeCtrs[i].invoke_OnDisplayFinishedEvent();
            }
        }
    }
}
