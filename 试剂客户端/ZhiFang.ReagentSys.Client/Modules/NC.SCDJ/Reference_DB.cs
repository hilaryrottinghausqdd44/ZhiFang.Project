//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.8810
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

// 
// Assembly WebServiceStudio Version = 2.0.50727.8810
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("WebServiceStudio", "0.0.0.0")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name = "TranOrderMkServiceSOAP11Binding", Namespace = "http://ser.mk.pub.itf.nc/TranOrderMkService")]
public partial class TranOrderMkService : System.Web.Services.Protocols.SoapHttpClientProtocol
{

    /// <remarks/>
    public TranOrderMkService()
    {
        this.Url = "http://172.16.8.8:9099/uapws/service/nc.itf.pub.mk.ser.TranOrderMkService";
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:doCreateTranOrder", RequestNamespace = "http://ser.mk.pub.itf.nc/TranOrderMkService", ResponseNamespace = "http://ser.mk.pub.itf.nc/TranOrderMkService", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
    public string doCreateTranOrder([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)] string @string)
    {
        object[] results = this.Invoke("doCreateTranOrder", new object[] {
                    @string});
        return ((string)(results[0]));
    }

    /// <remarks/>
    public System.IAsyncResult BegindoCreateTranOrder(string @string, System.AsyncCallback callback, object asyncState)
    {
        return this.BeginInvoke("doCreateTranOrder", new object[] {
                    @string}, callback, asyncState);
    }

    /// <remarks/>
    public string EnddoCreateTranOrder(System.IAsyncResult asyncResult)
    {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
}
