/**
 * Created with JetBrains WebStorm.
 * User: xiongyy
 * Date: 13-5-31
 * Time: 上午9:57
 * To change this template use File | Settings | File Templates.
 */
Ext.define('InputForm.actionBtn',{
    extend:'Ext.panel.Panel',
    alias: 'widget.actionbtntbar',
    layout: 'absolute',
    id:'extForm',
    frame:true,
    border:false,
    tbar:[
        { xtype: 'button', text: '新增(N)',iconCls:'btnadd',handler:addrequestform},//addrequestform
        { xtype: 'button', text: '保存(S)',iconCls:'icon-save' ,handler:''},//saveRequestForm
        { xtype: 'button', text: '删除(D)',iconCls:'btndelete',handler:'' },//deleterequestform
        { xtype: 'button', text: '打印申请单(P)',iconCls:'icon-print' },
        { xtype: 'button', text: '查找申请单(F)',iconCls:'btnselceform' },
        { xtype: 'button', text: '开展检验项目浏览(L)',iconCls:'btnshowtestitem',handler:showItem},
        { xtype: 'button', text: '帮助(H)' ,iconCls:'btnhelp'}
    ]
    /*,
    items:[
        {xtype:'label',text:'检验医嘱申请',cls:'labelStyle',margin:'5 5 5 5'},
        {xtype:'label',text:'',x:150,y:10,id:'actionBth_lblState',cls:'labelStyle_state'}
    ]*/
});
function deleterequestform(){
    var grid=Ext.getCmp('RequestformGrid');
    var data=grid.getView().getSelectionModel().getSelection();
    if(data.length==0){
        Ext.MessageBox.show({
            title:'提示',
            msg:'请先选择您要操作的行!'
        });
        return;
    }else{
        Ext.Msg.confirm('请确认','是否确定要删除数据？',function(button,text){
            if (button == "yes"){
                var id = "";
                var rowdex=data[0].index;
                Ext.Array.each(data, function(record) {
                    //如果删除的是幻影数据，则id就不传递到后台了，直接在前台删除即可data[0].get('MEPTOrderForm_Id')==""
                    var MEPTOrderForm_Id=record.get('MEPTOrderForm_Id');
                    if(MEPTOrderForm_Id!=""){
                        id=MEPTOrderForm_Id;
                        Ext.Ajax.request({
                            url:getRootPath()+"/MEPTService.svc/MEPT_UDTO_DelMEPTOrderForm?id="+id,
//                    params : {
//                       id:id
//                    },
                            timeout:2000,
                            method : 'GET',
                            success : function(response, opts){
                                alert(response.responseText);
                                var success = Ext.decode(response.responseText).success;
                                if (success) {
                                    Ext.Array.each(data, function(record) {
                                        grid.getStore().remove(record);
                                    });
                                    if(grid.getStore().getAt(rowdex)){
                                        grid.getSelectionModel().select(rowdex,true);//选中删除数据的下一行
                                    }
                                    else{
                                        grid.getSelectionModel().select(0,true);//选中第一行
                                    }
                                }else{
                                    Ext.MessageBox.show({
                                        title : "提示",
                                        msg : "数据删除失败!"
                                    })
                                }
                            }
                        });
                    }else{
                        Ext.MessageBox.show({
                            title:'提示',
                            msg:'该行不能删除!'
                        });
                        return;
                    }
                });
            }
        })
    }
}
//点击添加按钮时，添加申请单信息
function addrequestform(){
    /*var lbl=Ext.getCmp('actionBth_lblState');
    lbl.setText('新增状态');
    //重置表单
    var form=Ext.getCmp('patientsform_form');
    form.form.reset();
    //重置已输入项目信息
    var inputgrid=Ext.getCmp('inputitem_grid');
    inputgrid.store.removeAll(false);
    //重置tabpanel,快速输入信息模块
    var tabpanel=Ext.getCmp('ItemSelect_myTabpanle');
    tabpanel.getActiveTab().items.items[0].getStore().reload();
    var requestformgrid=Ext.getCmp('RequestformGrid').getStore();
    var FormStore=Ext.getCmp('RequestformGrid').getSelectionModel().getSelection();
    if(FormStore[0].data.MEPTOrderForm_Id!="")
    {
        requestformgrid.insert(requestformgrid.getCount(),new MEPT_OrderForm({}));
        Ext.getCmp('RequestformGrid').getSelectionModel().select(requestformgrid.getCount()-1,true);//选中第一行
        Ext.getCmp('RequestformGrid').getView().focusRow(requestformgrid.getCount()-1);//获取焦点
    }*/
    
     var frm=Ext.getCmp('frmCompany');
     //var value=frm.getComponent('BLaboratory_IsUse');
     alert('获取是否使用值:'+frm.title);
}
function saveAddRequestForm(){
    var PatNo=Ext.getCmp('txtPatNo').value;
    var CName=Ext.getCmp('txtOrderFormCName').value;
    var Age=Ext.getCmp('txtOrderForm_Age').value;


    var Bed=Ext.getCmp('txtBed').value;
    var Ward=Ext.getCmp('txtward').value;
    var OrderFormDoctor=Ext.getCmp('txtOrderFormDoctor').value;
    var CertificateNumber=Ext.getCmp('txtCertificateNumber').value;
    var Telephone=Ext.getCmp('txtTelephone').value;
    var SerialNo=Ext.getCmp('txtSerialNo').value;
    var PatientID=Ext.getCmp('txtPatientID').value;
    var Address=Ext.getCmp('txtAddress').value;

    //下拉框

    var TestType=Ext.getCmp('txtTestType');
    var Dept=Ext.getCmp('txtDept');
    var Sex=Ext.getCmp('txtBSexName');
    var AgeUnit=Ext.getCmp('txtAgeUnit');
    var sampletype=Ext.getCmp('sampletype');
    var BNationality=Ext.getCmp('txtBNationality');
    var SickType=Ext.getCmp('txtSickType');

    var ItemGrid=Ext.getCmp('inputitem_grid').getStore();
    var data=ItemGrid.data.items;
    var itemstr="[";
    for(var i in data)
    {
       /* var id=data[i].data.MEPTOrderItem_Id;
        var DataTimeStamp="";
      */
        var ItemID=data[i].data.MEPTOrderItem_ItemAllItem_Id;
        var itemDataTimeStamp='['+data[i].data.MEPTOrderItem_ItemAllItem_DataTimeStamp+']';
        itemstr+="{ItemAllItem:{Id:'"+ItemID+"',DataTimeStamp:"+itemDataTimeStamp+
            "}},";

    }if(itemstr.length>2){
        itemstr=itemstr.substr(0,itemstr.length-1);
    }
    itemstr=Ext.JSON.decode(itemstr+"]");
    var hrDept=compantValueIsExist(Dept,'HRDept_DataTimeStamp');
    var objTestType=compantValueIsExist(TestType,'BTestType_DataTimeStamp');
    var objSickType=compantValueIsExist(SickType,'BSickType_DataTimeStamp');
    var objsampletype=compantValueIsExist(sampletype,'BSampleType_DataTimeStamp');
    var objSex=compantValueIsExist(Sex,'BSex_DataTimeStamp');
    var objageUnit=compantValueIsExist(AgeUnit,'BAgeUnit_DataTimeStamp');
//    for(var i in ItemGrid)
//    {
//        alert(ItemGrid[i]);
//    }
//    alert(ItemGrid);
    var  patientInfo={
        PatientID:PatientID,
        PatNo:PatNo,
        CName:CName,
        BSex:objSex,
        Telephone:Telephone,
        Address:Address,
        CertificateNumber:CertificateNumber
    };

    Ext.Ajax.defaultPostHeader = 'application/json';
    Ext.Ajax.request({
        async:false,//非异步
        url:getRootPath()+"/SingleTableService.svc/ST_UDTO_AddBPatientInfo",
        params:Ext.JSON.encode({entity:patientInfo}),
        method:'POST',
        timeout:2000,
        success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                var MEPT_OrderForm={
                    PatNo:PatNo,
                    CName:CName,
                    Age:Age,
                    AgeUnitID:AgeUnit,
                    PatientID:PatientID,
                    PatientKeyID:result.ResultDataValue,
                    SampleTypeID:sampletype,
                    DeptID:Dept,//.getValue()
                    TestTypeID:TestType,  ///.getValue()
                    HRDept:hrDept,//.Id
                    BSampleType:objsampletype,//.Id
                    BSickType:objSickType,  //.Id
                    BPatientInfo:{
                        Id:result.ResultDataValue,
                        DataTimeStamp:''
                    },
                    BTestType:objTestType,  //.Id
                    BAgeUnit:objageUnit,    //.Id
                    SerialNo:SerialNo,
                    Bed:Bed,
                    DiseaseRoomID:Ward,
                    Doctor:OrderFormDoctor//,
                    //MEPTOrderItemList:itemstr
                };
                Ext.Ajax.defaultPostHeader = 'application/json';
                Ext.Ajax.request({
                    async:false,//非异步
                    url:getRootPath()+"/MEPTService.svc/MEPT_UDTO_AddMEPTOrderForm",
                    params:Ext.JSON.encode({entity:MEPT_OrderForm}),
                    method:'POST',
                    timeout:2000,
                    success:function(response,opts){
                        var result = Ext.JSON.decode(response.responseText);
                        if(result.success){
                            Ext.Msg.alert('提示','操作成功！');
                            Ext.getCmp('RequestformGrid').store.load();
                        }
                        else{
                            Ext.Msg.alert('提示','操作采样组失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                        }
                    },
                    failure : function(response,options){
                        Ext.Msg.alert('提示','服务请求失败！');
                    }
                });
            }
            else{
                Ext.Msg.alert('提示','操作病人信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
            }
        },
        failure : function(response,options){
            Ext.Msg.alert('提示','服务请求失败！');
        }
    });
}
function saveUpdateRequestForm(){
    var PatNo=Ext.getCmp('txtPatNo').value;
    var CName=Ext.getCmp('txtOrderFormCName').value;
    var Age=Ext.getCmp('txtOrderForm_Age').value;


    var Bed=Ext.getCmp('txtBed').value;
    var Ward=Ext.getCmp('txtward').value;
    var OrderFormDoctor=Ext.getCmp('txtOrderFormDoctor').value;
    var CertificateNumber=Ext.getCmp('txtCertificateNumber').value;
    var Telephone=Ext.getCmp('txtTelephone').value;
    var SerialNo=Ext.getCmp('txtSerialNo').value;
    var PatientID=Ext.getCmp('txtPatientID').value;
    var Address=Ext.getCmp('txtAddress').value;

    //下拉框

    var TestType=Ext.getCmp('txtTestType');
    var Dept=Ext.getCmp('txtDept');
    var Sex=Ext.getCmp('txtBSexName');
    var AgeUnit=Ext.getCmp('txtAgeUnit');
    var sampletype=Ext.getCmp('sampletype');
    var BNationality=Ext.getCmp('txtBNationality');
    var SickType=Ext.getCmp('txtSickType');

    var ItemGrid=Ext.getCmp('inputitem_grid').getStore();
    var formGrid=Ext.getCmp('RequestformGrid').getSelectionModel().getSelection();
    var index=formGrid[0].index;
    var st=formGrid[0].raw.MEPTOrderForm_BPatientInfo_DataTimeStamp;
    var patidkey=formGrid[0].raw.MEPTOrderForm_BPatientInfo_Id;
    var orderformId=formGrid[0].raw.MEPTOrderForm_Id;
    var orderformdatetimeStamp=formGrid[0].raw.MEPTOrderForm_DataTimeStamp;
    var patientinfostamp='['+st+']';
    patientinfostamp=Ext.JSON.decode(patientinfostamp);
    var RequestformGrid=Ext.getCmp('RequestformGrid');
    var data=ItemGrid.data.items;
    var itemstr="[";
    for(var i in data)
    {
        var id=data[i].data.MEPTOrderItem_Id;
        var DataTimeStamp="";

        if(data[i].data.MEPTOrderItem_DataTimeStamp)
        {
            DataTimeStamp = '['+data[i].data.MEPTOrderItem_DataTimeStamp+']';
        }
        var ItemID=data[i].data.MEPTOrderItem_ItemAllItem_Id;
        var itemDataTimeStamp='['+data[i].data.MEPTOrderItem_ItemAllItem_DataTimeStamp+']';
        if(id!="")
        {
            itemstr+="{ItemAllItem:{Id:'"+ItemID+"',DataTimeStamp:"+itemDataTimeStamp+
                "},DataTimeStamp:"+DataTimeStamp+",Id:'"+id+"'},";
        }
        else
        {
            itemstr+="{ItemAllItem:{Id:'"+ItemID+"',DataTimeStamp:"+itemDataTimeStamp+
                "}},";
        }

    }if(itemstr.length>2){
        itemstr=itemstr.substr(0,itemstr.length-1);
    }
    itemstr=Ext.JSON.decode(itemstr+"]");
    var hrDept=compantValueIsExist(Dept,'HRDept_DataTimeStamp');
    var objTestType=compantValueIsExist(TestType,'BTestType_DataTimeStamp');
    var objSickType=compantValueIsExist(SickType,'BSickType_DataTimeStamp');
    var objsampletype=compantValueIsExist(sampletype,'BSampleType_DataTimeStamp');
    var objSex=compantValueIsExist(Sex,'BSex_DataTimeStamp');
    var objageUnit=compantValueIsExist(AgeUnit,'BAgeUnit_DataTimeStamp');
    var  patientInfo={
        PatientID:PatientID,
        Id:patidkey,
        PatNo:PatNo,
        CName:CName,
        BSex:objSex,
//        B_Nationality:{
//            Id:BNationality,
//            DataTimeStamp:[0,0,0,0,0,0,220,81]
//        },
        Telephone:Telephone,
        Address:Address,
        CertificateNumber:CertificateNumber,
        DataTimeStamp:patientinfostamp
    };
    Ext.Ajax.defaultPostHeader = 'application/json';
    Ext.Ajax.request({
        async:false,//非异步
        url:getRootPath()+"/SingleTableService.svc/ST_UDTO_UpdateBPatientInfo",
        params:Ext.JSON.encode({entity:patientInfo}),
        method:'POST',
        timeout:2000,
        success:function(response,opts){
            var result = Ext.JSON.decode(response.responseText);
            if(result.success){
                var MEPT_OrderForm={
                    PatNo:PatNo,
                    CName:CName,
                    Age:Age,
                    Id:orderformId,
                    PatientID:PatientID,
                    DataTimeStamp:Ext.JSON.decode('['+orderformdatetimeStamp+']'),
                    HRDept:hrDept,
                    BSampleType:objsampletype,
                    BSickType:objSickType,
                    BPatientInfo:{
                        Id:patidkey,
                        DataTimeStamp:patientinfostamp
                    },
                    BAgeUnit:objageUnit,
                    BTestType:objTestType,
                    SerialNo:SerialNo,
                    Bed:Bed,
                    DiseaseRoomID:Ward,
                    Doctor:OrderFormDoctor,
                    MEPTOrderItemList:itemstr
                };
                Ext.Ajax.defaultPostHeader = 'application/json';
                Ext.Ajax.request({
                    async:false,//非异步
                    url:getRootPath()+"/MEPTService.svc/MEPT_UDTO_UpdateMEPTOrderForm",
                    params:Ext.JSON.encode({entity:MEPT_OrderForm}),
                    method:'POST',
                    timeout:2000,
                    success:function(response,opts){
                        var result = Ext.JSON.decode(response.responseText);
                        if(result.success){
                            Ext.Msg.alert('提示','操作成功！');
                            RequestformGrid.getStore().reload(10);
                            RequestformGrid.getSelectionModel().select(index,true);
//                            var record= RequestformGrid.getStore().data.items[index];
//                            record.load(10,{
//                                callback: function(record, operation) {
//                                    RequestformGrid.getSelectionModel().select(index,true);
//                                }
//                            });

//                            Ext.getCmp('patientsform_form').reload(10);
                        }
                        else{
                            Ext.Msg.alert('提示','修改医嘱单失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
                        }
                    },
                    failure : function(response,options){
                        Ext.Msg.alert('提示','服务请求失败！');
                    }
                });
            }
            else{
                Ext.Msg.alert('提示','操作病人信息失败！错误信息【<b style="color:red">'+ result.ErrorInfo +"</b>】");
            }
        },
        failure : function(response,options){
            Ext.Msg.alert('提示','服务请求失败！');
        }
    });
}
function saveRequestForm(){
    var stateform=Ext.getCmp('actionBth_lblState').text;
    if(stateform=='新增状态'){
        saveAddRequestForm();
    }else if(stateform=='修改数据'){
        saveUpdateRequestForm();
    }
}
//获取下拉框键子对值
function compantValueIsExist(com,fieldStamp){
    var v=com.getRawValue();
    var obj=null;
    if(v==null||v==""){
        obj=null;
    }else{
        var datatimeStamp=null;
        var Id=null;
        if(fieldStamp=='HRDept_DataTimeStamp'){
            if(com.displayTplData!=null)
            {
                Id=com.displayTplData[0].HRDept_Id;
                datatimeStamp=Ext.JSON.decode('['+com.displayTplData[0].HRDept_DataTimeStamp+']');
            }
        }
        else if(fieldStamp=='BSampleType_DataTimeStamp'){
            if(com.displayTplData!=null)
            {
                Id=com.displayTplData[0].BSampleType_Id;
                datatimeStamp=Ext.JSON.decode('['+com.displayTplData[0].BSampleType_DataTimeStamp+']');
            }
        }
        else if(fieldStamp=='BSickType_DataTimeStamp'){
            if(com.displayTplData!=null)
            {
                Id=com.displayTplData[0].BSickType_Id;
                datatimeStamp=Ext.JSON.decode('['+com.displayTplData[0].BSickType_DataTimeStamp+']');
            }
        }
        else if(fieldStamp=='BTestType_DataTimeStamp'){
            if(com.displayTplData!=null)
            {
                Id=com.displayTplData[0].BTestType_Id;
                datatimeStamp=Ext.JSON.decode('['+com.displayTplData[0].BTestType_DataTimeStamp+']');
            }
        }
        else if(fieldStamp=='BSex_DataTimeStamp'){
            if(com.displayTplData!=null)
            {
                Id=com.displayTplData[0].BSex_Id;
                datatimeStamp=Ext.JSON.decode('['+com.displayTplData[0].BSex_DataTimeStamp+']');
            }
        }
        else if(fieldStamp=='BAgeUnit_DataTimeStamp'){
            if(com.displayTplData!=null)
            {
                Id=com.displayTplData[0].BAgerUnit_Id;
                datatimeStamp=Ext.JSON.decode('['+com.displayTplData[0].BAgeUnit_DataTimeStamp+']');
            }
        }
        if(Id!="")
        {
            obj={
                Id:parseInt(Id)
                ,DataTimeStamp:datatimeStamp
            }
        }
    }
    return obj;
}
//function compantValueIsExist(com,fieldStamp){
//    var v=com.value;
//    var obj=null;
//    if(v==null||v==""){
//        obj=null;
//    }else{
//        var datatimeStamp=null;
//        if(fieldStamp=='HRDept_DataTimeStamp'){
//            datatimeStamp=Ext.JSON.decode('['+com.displayTplData[0].HRDept_DataTimeStamp+']');}
//        else if(fieldStamp=='BSampleType_DataTimeStamp'){
//            datatimeStamp=Ext.JSON.decode('['+com.displayTplData[0].BSampleType_DataTimeStamp+']');
//        }
//        else if(fieldStamp=='BSickType_DataTimeStamp'){
//            datatimeStamp=Ext.JSON.decode('['+com.displayTplData[0].BSickType_DataTimeStamp+']');
//        }
//        else if(fieldStamp=='BTestType_DataTimeStamp'){
//            datatimeStamp=Ext.JSON.decode('['+com.displayTplData[0].BTestType_DataTimeStamp+']');
//        }
//        else if(fieldStamp=='BSex_DataTimeStamp'){
//            datatimeStamp=Ext.JSON.decode('['+com.displayTplData[0].BSex_DataTimeStamp+']');
//        }
//        else if(fieldStamp=='BAgeUnit_DataTimeStamp'){
//            datatimeStamp=Ext.JSON.decode('['+com.displayTplData[0].BAgeUnit_DataTimeStamp+']');
//        }
//        obj={
//            Id:parseInt(v)
//            ,DataTimeStamp:datatimeStamp
//        }
//    }
//    return obj;
//}
function showItem(){
    var modelForm = Ext.create('MEPT_SearchItem',{
        modal:true,//模态
        title:'开展检验项目浏览',
        floating:true,//漂浮
        closable:true,//有关闭按钮
        draggable:true,//可移动
        isWindow:true//窗口打开
    }).show();
}