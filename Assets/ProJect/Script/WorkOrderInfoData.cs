using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkOrderInfoData
{
    public int type;

    public string devopsWorkOrderId;

    public WorkOrderInfoArray[] list;

    public WorkOrderInfoData()
    {
        
    }

    public WorkOrderInfoData(int ctype,string cdevopsWorkOrderId,WorkOrderInfoArray[] clist)
    {
        type = ctype;
        devopsWorkOrderId = cdevopsWorkOrderId;
        list = clist;
    }
}
