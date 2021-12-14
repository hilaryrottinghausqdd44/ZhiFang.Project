/**
 * 客户端授权变更
 * @author longfc
 * @version 2019-01-14
 */
Ext.define('Shell.class.rea.client.attachment.UploadPanel',{
    extend:'Ext.form.Panel',   
    requires:[
		'Shell.ux.form.field.CheckTrigger'
    ],
	
    title:'客户端授权变更',
    width:400,
    height:150,
    
    /**文件上传服务*/
   	url:'/ReaManageService.svc/ST_UDTO_UploadAuthorizationFileOfClient',
	
   	/**空白提示*/
    fileEmptyText:'文件',
    /**显示成功信息*/
	showSuccessInfo:true,
	
	/**导入类型*/
	formType:'',	
	ERROR_UNIQUE_KEY_INFO:JShell.Server.Status.ERROR_UNIQUE_KEY,
	
	bodyPadding:10,
    layout:'anchor',
    /**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
    	labelWidth:60,
    	labelAlign:'right',
    },
    /*厂商*/
	ProdOrg: 'ProdOrg',
	  /**机构信息*/
    CenOrg:{Id:'',Name:'',readOnly:false},
	
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	//初始化检索监听
		me.initFilterListeners();
    },
    initComponent:function(){
    	var me = this;
    	me.addEvents('save');
    	
    	if(me.formType == 'Comp'){
    		me.defaults.labelWidth = 70;
    	}else if(me.formType == 'Prod'){
    		me.height = me.height - 60;
    	}
    	
    	me.items = me.items || me.createItems();
    	me.dockedItems = me.dockedItems || me.createDockedItems();
    	
    	me.callParent(arguments);
    },
    /**创建内部组件*/
    createItems:function(){
    	var me = this,
    		items = [];
 		//文件
    	items.push({
    		xtype:'filefield',allowBlank:false,emptyText:me.fileEmptyText,
    		buttonConfig:{iconCls:'button-search',text:'选择'},
			name:'File',itemId:'File',fieldLabel: '文件'
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
   	
   	   	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	
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
        		if(me.showSuccessInfo){
        			JShell.Msg.alert(JShell.All.SUCCESS_TEXT);
        		}
        		me.fireEvent('save',me);
            	
            },
            failure:function(form,action){
				var msg = action.result.ErrorInfo;
				//上传的文件内容错误
				if(msg == 'Error001'){
					JShell.Msg.confirm({
						icon:Ext.Msg.ERROR,
						msg:'上传的文件内容存在问题，是否下载错误详细信息文件？'
					},function(btn){
						if(btn != 'ok') return;
						//下载错误文件
						me.downloadErrorFile(action.result.ResultDataValue);
					});
				}else{
					msg = msg ? msg : '文件上传失败！';
					JShell.Msg.error(msg);
				}
			}
        });
    },
    /**下载错误详细信息文件*/
    downloadErrorFile:function(fileName){
    	var me = this,
    		url = JShell.System.Path.ROOT + '/ReaManageService.svc/RS_UDTO_DownLoadExcel';
		url += '?downFileName=错误详细信息文件&operateType=0&fileName=' + fileName;
		window.open(url);
    }
});