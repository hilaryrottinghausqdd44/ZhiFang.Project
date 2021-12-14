/**
 * 报告删除面板
 * @author Jcall
 * @version 2018-09-03
 */
Ext.define("Shell.class.manage.delete.Panel", {
    extend: 'Ext.panel.Panel',
    bodyPadding:5,
    
    deleteFileUrl: "/ServiceWCF/ReportFormService.svc/DeleteReportPDFFile",
    
    
    initComponent: function () {
        var me = this;
        me.dockedItems = [{
        	xtype:'toolbar',
        	itemId:'toolbar',
        	dock:'top',
        	items:[{
                xtype: 'datefield',
                fieldLabel: '文件日期',
                itemId:'startDate',
                labelWidth: 60,
                width: 160,
                format:'Y-m-d'
            },{
                xtype: 'datefield',
                itemId: 'endDate',
                fieldLabel: '-',
                labelWidth: 1,
                width:100,
                format:'Y-m-d'
            },{
                style: 'margin-left: 10px;',
                xtype:"button",
                iconCls:'button-del',
                text: '删除',
                handler: function () {
                    me.onDeleteFileClick();
                }
            },{
                style: 'margin-left: 10px;',
                xtype:"button",
                iconCls:'button-add',
                text: '生成电子签名',
                handler: function () {
                    me.CreatePUser();
                }
            }]
        }];
        
        me.html = '<div style="border:1px solid #169ada;border-radius:5px;color:#169ada;text-align:center;margin:10px;padding:10px;font-size:14px;">请选择需要删除文件的时间范围</div>';
        
        me.callParent(arguments);
    },
    //文件删除
    onDeleteFileClick:function() {
    	var me = this,
    		toolbar = me.getComponent("toolbar")
    		startDate = toolbar.getComponent("startDate").value,
        	endDate = toolbar.getComponent("endDate").value;
        	
        startDate = Shell.util.Date.toString(startDate,true);
        endDate = Shell.util.Date.toString(endDate,true);
        
        if (startDate == null ||endDate ==null) {
            Shell.util.Msg.showWarning("请输入时间范围");
            return;
        }
    	
        me.onDeleteFilesByDate(startDate,endDate,me.onShowInfo);
    },
    //根据时间段删除文件
    onDeleteFilesByDate:function(startDate,endDate,callback){
    	var me = this,
    		url = Shell.util.Path.rootPath + me.deleteFileUrl;
    		
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: url,
            method: 'POST',
            params: Ext.JSON.encode({
                startDate: startDate,
                endDate: endDate
            }),
            success: function (response, options) {
                var data = Ext.JSON.decode(response.responseText);
                if (data.success) {
                    var list = Ext.JSON.decode(data.ResultDataValue) || [];
                    callback(list,me);
                }else{
                	me.onShowError(data.ErrorInfo);
                }
            }
        });
    },
    //显示删除信息
    onShowInfo:function(list,m){
    	var me = m,
    		len = list.length,
    		folderCount = 0,
    		fileCount = 0,
    		html = [];
    		
    	for(var i=0;i<len;i++){
    		folderCount++;
    		html.push('<div>文件夹' + list[i].Folder + '</div>');
    		
    		var files = list[i].file,
    			fLen = files.length;
    		for (var j=0;j<fLen;j++){
    			fileCount++;
				html.push('<div>文件' + files[j] + '</div>');
			}
    	}
    	
    	html.unshift('<div><b>共删除 <span style="color:red;">' + folderCount + '</span> 个文件夹, <span style="color:red;">' + fileCount + '</span> 个文件</b></div>')
    	
    	me.update(html.join(''));
    },
    //显示错误信息
    onShowError:function(value){
    	var me = this,
    		html = [];
    		
    	html.push('<div style="padding:10px;color:red;text-align:center;font-size:14px;font-weight:bold;">错误信息</div>');
    	html.push('<div style="">' + value + '</div>');
    	console.log(me);
    	me.update(html.join(''));
    },
    //批量生成电子签名
    CreatePUser:function(){
    	var me = this,
    		url = Shell.util.Path.rootPath + "/ServiceWCF/ReportFormService.svc/GetCreatePUserESignature";
    		
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            url: url,
            method: 'GET',
            //params: Ext.JSON.encode({ }),
            success: function (response) {
            	var data = Ext.JSON.decode(response.responseText);
            
                
                if (data.success) {
                   	Shell.util.Msg.showInfo("生成成功！");
                }else{
                	Shell.util.Msg.showError("生成失败！")
                }
            }
        });
    }
});