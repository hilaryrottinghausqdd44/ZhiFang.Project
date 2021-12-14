Ext.onReady(function () {
    Ext.QuickTips.init();//初始化后就会激活提示功能
    var serverUrl = Shell.util.Path.rootPath +
        '/ServiceWCF/ReportFormService.svc/SelectReport?fields=ReportFormID,FormNo,' +
		'SAMPLENO,SECTIONNO,CNAME,CLIENTNO,SectionType,RECEIVEDATE,PRINTTIMES,ItemName,PATNO,Serialno' + 
        '&page=1&limit=100';
    
    function doIt(filePath){
    	if(!filePath){
    		Shell.util.Msg.error('请选择一个Excel文件！');
    		return;
    	}
    	
	    var oXL = null;
	    try{
	    	//创建Excel.Application对象
	        oXL = new ActiveXObject("Excel.application");
	    }catch(err){
	        Shell.util.Msg.error('错误信息：' + err);
    		return;
	    }
	    
	    var SHELL_ID = 1;//读取第2个表
	    var ROW_START = 3;//从第3行开始读取
	    
	    var oWB = oXL.Workbooks.open(filePath);
	    oWB.worksheets(SHELL_ID).select();
	    var oSheet = oWB.ActiveSheet;
	    var colCount = oXL.Worksheets(SHELL_ID).UsedRange.Cells.Rows.Count ;
	
		var list = [];
		var strArr = [];
	    for(var i=ROW_START;i<=colCount;i++){
	    	list.push({
	    		name1:oSheet.Cells(i,1).value,
	    		name2:oSheet.Cells(i,2).value,
	    		name3:oSheet.Cells(i,3).value,
	    		name4:oSheet.Cells(i,4).value
	    	});
	    	strArr.push(
	    		oSheet.Cells(i,1).value + ' ; ' + 
	    		oSheet.Cells(i,2).value + ' ; ' + 
	    		oSheet.Cells(i,3).value + ' ; ' + 
	    		oSheet.Cells(i,4).value
	    	);
	    }
		
	    oXL.Quit();
	    CollectGarbage();
	    
	    alert(strArr.join('</br>'));
    }
    
    
    //总体布局
    var viewport = Ext.create('Ext.container.Viewport', {
        layout: 'fit',
        bodyPadding: 10,
        items: [{
            xtype:'panel',
            title: '根据EXCEL保存产品',
            tbar: [{
            	xtype:'filefield',fieldLabel:'程序文件',
				allowBlank:false,width:600,
				labelAlign:'right',labelWidth:60,
				emptyText:'请选择EXCEL文件',
				buttonConfig:{iconCls:'search-img-16',text:'选择'},
				name:'CodeFile',itemId:'CodeFile',
            },{
                xtype: 'button',
                text: '开始执行',
                handler: function () {doIt(this.ownerCt.getComponent('CodeFile').getValue());}
            }]
        }]
    });
});