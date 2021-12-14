/**
 * 文件上传面板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.excel.FileUpdatePanel',{
    extend:'Ext.form.Panel',
    title:'文件上传',
    
    width:400,
    height:150,
    
    /**文件上传服务*/
   	url:'/StatService.svc/ST_UDTO_UploadExcelBaseData',
   	//url:JShell.System.Path.DEFAULT_ERROR_URL_ASPX,
   	/**空白提示*/
    fileEmptyText:'',
    /**显示成功信息*/
	showSuccessInfo:true,
	/**表名*/
	TableName:'',
	
	ERROR_UNIQUE_KEY_INFO:JShell.Server.Status.ERROR_UNIQUE_KEY,
	
	bodyPadding:10,
    layout:'anchor',
    /**每个组件的默认属性*/
    defaults:{anchor:'100%'},
    
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    },
    initComponent:function(){
    	var me = this;
    	me.addEvents('save');
    	
    	if(me.TableName){
    		me.items = me.items || me.createItems();
    		me.dockedItems = me.dockedItems || me.createDockedItems();
    	}else{
    		me.html = '<div style="text-align:center;font-weight:bold;color:red;margin-top:20px;">请配置TableName属性</div>';
    	}
    	
    	me.callParent(arguments);
    },
    /**创建内部组件*/
    createItems:function(){
    	var me = this,
    		items = [];
    		
    	items.push({
    		xtype:'textfield',name:'TableName',itemId:'TableName',
    		value:me.TableName,hidden:true
    	});
    	items.push({
    		xtype:'filefield',allowBlank:false,emptyText:me.fileEmptyText,
    		buttonConfig:{iconCls:'button-search',text:'选择'},
			name:'File',itemId:'File'
    	});
    	
    	return items;
    },
    /**创建挂靠*/
    createDockedItems:function(){
    	var me = this,
    		dockedItems = [];
		
		dockedItems.push(Ext.create('Shell.ux.toolbar.Button',{
			dock:'bottom',
			items:['->','accept','cancel']
		}));
    		
    	return dockedItems;
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
            	if(action.result.success){
            		if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT);
            		me.fireEvent('save',me,action);
            	}else{
            		var msg = action.result.ErrorInfo;
            		var index = msg.indexOf('UNIQUE KEY');
					if(index != -1){
						msg = me.ERROR_UNIQUE_KEY_INFO;
					}
            		JShell.Msg.error(msg);
            	}
            },
            failure:function(form,action){
				var msg = action.result.ErrorInfo;
            		var index = msg.indexOf('UNIQUE KEY');
					if(index != -1){
						msg = me.ERROR_UNIQUE_KEY_INFO;
					}
            		JShell.Msg.error(msg);
			}
        });
    }
});