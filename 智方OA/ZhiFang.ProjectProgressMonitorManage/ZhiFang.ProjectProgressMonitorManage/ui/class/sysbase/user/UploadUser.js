/**
 * 用户上传面板
 * @author Jcall
 * @version 2018-04-09
 */
Ext.define('Shell.class.sysbase.user.UploadUser',{
    extend:'Ext.form.Panel',
	requires:['Shell.ux.toolbar.Button'],
    title:'用户上传',
    width:400,
    height:120,
    
    /***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype:'add',
    /**文件上传服务*/
   	url:'/RBACService.svc/RBAC_RJ_UploadHRDeptEmpByExcel',
   	//下载文件服务地址
   	downLoadExcelUrl:'/RBACService.svc/RBAC_RJ_DownLoadExcel',
   	/**空白提示*/
    fileEmptyText:'Excel格式文件',
    /**显示成功信息*/
	showSuccessInfo:true,
	
	bodyPadding:10,
    layout:'anchor',
    /**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
    	labelWidth:60,
    	labelAlign:'right',
    },
	
	/**部门ID*/
	DeptID: null,
    
    initComponent:function(){
    	var me = this;
    	
    	me.dockedItems = [{
    		xtype:'uxButtontoolbar',
    		itemId:'buttonsToolbar',
			dock:'bottom',
			items:['->','accept','cancel']
		}];
    	me.items = me.createItems();
    	me.callParent(arguments);
    },
    /**创建内部组件*/
    createItems:function(){
    	var me = this,
    		items = [];
    	
 		//文件
    	items.push({
    		xtype:'textfield',fieldLabel: '部门ID',
			name: 'DeptID',itemId: 'DeptID',
			hidden: true,value:me.DeptID
    	},{
    		xtype:'filefield',allowBlank:false,emptyText:me.fileEmptyText,
    		buttonConfig:{iconCls:'button-search',text:'选择'},
			name:'File',itemId:'File',fieldLabel: '用户文件'
    	});
    	
    	return items;
   },
   	/**点击取消按钮处理*/
    onCancelClick:function(){
    	this.close();
    },
   	/**点击确定按钮处理*/
    onAcceptClick:function(){
    	var me = this;
		if (!me.getForm().isValid()) return;
		
		var values = me.getForm().getValues();
		
		var url = (me.url.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.url;
        me.getForm().submit({
            url:url,
            waitMsg:JShell.Server.SAVE_TEXT,
            success:function (form,action) {
            	var msg = action.result.ErrorInfo + '</b>是否需要下载处理结果文件？';
        		JShell.Msg.confirm({
					icon:Ext.Msg.INFO,
					msg:msg
				},function(btn){
					if(btn == 'ok'){
						//下载错误文件
						me.downloadErrorFile(action.result.ResultDataValue);
					}
					me.fireEvent('save',me);
				});
            },
            failure:function(form,action){
            	var msg = action.result.ErrorInfo + '</b>是否需要下载处理结果文件？';
				JShell.Msg.confirm({
					icon:Ext.Msg.INFO,
					msg:msg
				},function(btn){
					if(btn != 'ok') return;
					//下载错误文件
					me.downloadErrorFile(action.result.ResultDataValue);
				});
			}
        });
    },
    /**下载错误详细信息文件*/
    downloadErrorFile:function(fileName){
    	var me = this,
    		url = JShell.System.Path.ROOT + me.downLoadExcelUrl;
		
		url += '?downFileName=错误详细信息文件&operateType=0&fileName=' + fileName;
		window.open(url);
    }
});