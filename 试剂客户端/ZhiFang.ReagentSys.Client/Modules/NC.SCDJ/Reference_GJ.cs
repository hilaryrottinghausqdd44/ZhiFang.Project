//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.9151
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
// Assembly WebServiceStudio Version = 2.0.50727.9151
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("WebServiceStudio", "0.0.0.0")]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Web.Services.WebServiceBindingAttribute(Name = "IcrkOrderServiceSOAP11Binding", Namespace = "http://itfservice.crkorder.mk.nc/IcrkOrderService")]
public partial class IcrkOrderService : System.Web.Services.Protocols.SoapHttpClientProtocol
{

    /// <remarks/>
    public IcrkOrderService()
    {
        this.Url = "http://172.16.8.8:9099/uapws/service/nc.mk.crkorder.itfservice.IcrkOrderService";
    }

    /// <remarks/>
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:insertCrkOrder", RequestNamespace = "http://itfservice.crkorder.mk.nc/IcrkOrderService", ResponseNamespace = "http://itfservice.crkorder.mk.nc/IcrkOrderService", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
    [return: System.Xml.Serialization.XmlElementAttribute("return", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)]
    public string insertCrkOrder([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = true)] string @string)
    {
        object[] results = this.Invoke("insertCrkOrder", new object[] {
                    @string});
        return ((string)(results[0]));
    }

    /// <remarks/>
    public System.IAsyncResult BegininsertCrkOrder(string @string, System.AsyncCallback callback, object asyncState)
    {
        return this.BeginInvoke("insertCrkOrder", new object[] {
                    @string}, callback, asyncState);
    }

    /// <remarks/>
    public string EndinsertCrkOrder(System.IAsyncResult asyncResult)
    {
        object[] results = this.EndInvoke(asyncResult);
        return ((string)(results[0]));
    }
}
