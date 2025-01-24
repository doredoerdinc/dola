using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DevExpress.ExpressApp.DC;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;

using LgsLib.Base.PermissionPolicy;
using DevExpress.Persistent.Validation;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Drawing;
using LgsLib.Base;
using DevExpress.ExpressApp.Editors;

namespace dola.Module
{

    public enum EnumDivionRate
    {
        None = 0,
        MaxVehicleCapacity = 2,
        Piece = 1,
        On = 10,
        Yüz = 100,
        Bin = 1000,

    }

    public enum EnumRotationType
    { 
         Fifo=0,
         Lifo=1 
    }

    public enum EnumPriceCalculateType
    {
        LastTariffPrice = 0,
        FirstUnitPrice = 1,
        CurrentUnitPrice = 2
    }
    public enum EnumPriceType
    {
        TL = 0,
        Dolar = 1,
        Euro = 2
    }

    public enum EnumItemTrackingType
    {
        None = 0,
        ExpireDate = 1,
        Batch = 2,
        Serial=3
    }

    public enum EnumAccountTransactionType
    {
        None = 0,
        Expense = -1,
        Income = 1
    }

    public enum EnumLocationFunctionType
    {
        None = 0,
        Pick = 100,
        Stock = 200,
        Ramp = 200,
        OperationStation=300
    }

    public enum EnumLHollSide
    {
        None=0,
        Left = 1,
        Right = 2, 
    }

    public enum EnumAccountType
    {
        None = 0,
        PurchVehicle = 100,
        SalesOrder = 200,
        DriverExpense = 300,
    }
}
