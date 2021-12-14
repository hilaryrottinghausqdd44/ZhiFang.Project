/**
 * @author GHX
 * @version 2019-10-17
 */
Ext.define('Shell.class.opd.edit.EditAPP', {
    extend: 'Ext.tab.Panel',
    records:'',
    selecttype:'',
    cookie:'',
    initComponent: function () {
        var me = this;
        me.items = me.createItems();
        me.dockedItems = me.createDockedItems();
        me.callParent(arguments);
    },
    
    createDockedItems: function () {
            var me = this;
            var tooblar = Ext.create('Ext.toolbar.Toolbar', {
                width: 100,
                items: [{
                    xtype: 'button', text: '保存',
                    iconCls: 'button-save',
                    listeners: {
                        click: function () {
                            var isok = me.InsertColumnsTempale();
                            if(isok){
                            	Shell.util.Msg.showInfo("保存成功!");
                            	me.fireEvent("updatesave",me.records);
                            }else{
                            	Shell.util.Msg.showError("保存失败请查看日志!");
                            }
                        }
                    }
                }]
            });
            return [tooblar];
    },

    createItems:function () {
        var me = this;
        var url = "";
        var sectiontype = me.records.SectionType;
        if(sectiontype == 1  || sectiontype == 3 ){
        	url = "Shell.class.opd.edit.ReportItemEdit";
        	me.selecttype = 'ReportItemFull';
        }else  if(sectiontype == 2  || sectiontype == 4|| sectiontype == 7){
        	url = "Shell.class.opd.edit.ReportMicroEdit";
        	me.selecttype = 'ReportMicroFull';
        }else if(sectiontype == 5  || sectiontype == 6 || sectiontype == 8 || sectiontype == 9){
        	url = "Shell.class.opd.edit.ReportMarrowEdit";
        	me.selecttype = 'ReportMarrowFull';
        }
        
        me.ReportFormFull = Ext.create("Shell.class.opd.edit.ReportFormEdit", {
        	height:500,
        	records:me.records,
            title: 'ReportFormFull',
            itemId:'ReportFormFull'
        });
        me.ReportItemEdit = Ext.create(url, {
        	height:500,
        	records:me.records,
            title: me.selecttype,
            itemId:me.selecttype
        });
        return [me.ReportFormFull,me.ReportItemEdit];
    },
    InsertColumnsTempale: function () {
        var me = this;
        var rs = "";
        var rs2 = "";
        var reportFormFullList = [];
        var otherList = [];
        var ReportFormFullstore = me.getComponent("ReportFormFull").getStore().data.items;
		var othertore = me.getComponent(me.selecttype).getStore().data.items;        
        var url = "";
        for(var i = 0;i<ReportFormFullstore.length;i++){
        	reportFormFullList.push(ReportFormFullstore[i].data);
        }
        
        for(var i = 0;i<othertore.length;i++){
        	otherList.push(othertore[i].data);
        }
        //更新ReportFormFull表
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/UpdateReportFormFull",
            async: false,
            method: 'POST',
            params: Ext.encode({ "models": reportFormFullList }),
            success: function (response, options) {
                rs = Ext.JSON.decode(response.responseText);
            }
        });
        
        if(me.selecttype == "ReportItemFull"){
        	url = Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/UpdateReportItemFull";
        }else if(me.selecttype == "ReportMicroFull"){
	        url= Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/UpdateReportMicroFull";
        }else if(me.selecttype == "ReportMarrowFull"){
	        url= Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/UpdateReportMarrowFull";
        }
        if(otherList.length>0){
        	 //更新子表
	        Ext.Ajax.defaultPostHeader = 'application/json';
	        Ext.Ajax.request({
	            url: url,
	            async: false,
	            method: 'POST',
	            params: Ext.encode({ "models": otherList }),
	            success: function (response, options) {
	                rs2 = Ext.JSON.decode(response.responseText);
	            }
	        });
        }
       
        
        //增加操作日志表
        var scopertion = [];
        for(var i=0;i<reportFormFullList.length;i++){
        	scopertion.push({
        		"BobjectID":reportFormFullList[i].ReportPublicationID,
        		"Memo":"修改ReportFormFull表数据",
        		"DispOrder":1,
        		"IsUse":1,
        		"CreatorID":Number(me.cookie.UserNo),
        		"CreatorName":me.cookie.UserCName
        	});
        }
        for(var i=0;i<otherList.length;i++){
        	scopertion.push({
        		"BobjectID":otherList[i].ReportPublicationID,
        		"Memo":"修改"+me.selecttype+"表数据",
        		"DispOrder":1,
        		"IsUse":1,
        		"CreatorID":Number(me.cookie.UserNo),
        		"CreatorName":me.cookie.UserCName
        	});
        }
        var rs3 = "";
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/AddSC_Operation",
            async: false,
            method: 'POST',
            params: Ext.encode({ "models": scopertion }),
            success: function (response, options) {
                rs3 = Ext.JSON.decode(response.responseText);
            }
        });
        var isok = false;
        if(rs.success  && rs3.success){
        	if(rs2 != "" && rs2.success){
        		isok = true;
        	}else if(rs2 != "" && rs2.success==false){
        		isok = false;
        	}else{
        		isok = true;
        	}
        }
        return isok;
    }
});