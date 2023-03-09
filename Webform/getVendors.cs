using LinHong.Lib.Model;
using LinHong.Lib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// getVendors 的摘要描述
/// </summary>
public class getVendors
{
    protected RequisitionsService db = new RequisitionsService();
    public getVendors()
    {
        
    }

    public List<SW_VENDOR1> GetVendors()
    {
        //var data = db.getVendor();
        return data;
    }
}