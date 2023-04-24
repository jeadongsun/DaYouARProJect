using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkOrderInfoArray
{
    public string handleDesc;

    public string handleAttach;

    public string handleResult;

    public string id;
    
    public WorkOrderInfoArray()   
    {
    }

    public WorkOrderInfoArray(string chandleDesc,string chandleAttach,string chandlerResult,string cid)
    {
        id = cid;
        handleDesc = chandleDesc;
        handleAttach = chandleAttach;
        handleResult = chandlerResult;
    }
}
