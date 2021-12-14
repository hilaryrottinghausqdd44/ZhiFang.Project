/**
 * 附件表单
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.attachment.PrimaryForm', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '附件信息',
	width: 20,
	height: 20,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchAttachmentById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/WorkManageService.svc/WM_UDTO_AttachmentUpLoad',
	/**修改服务地址*/
	//editUrl: '/BaseService.svc/ST_UDTO_UpdateAttachmentByField',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,

	/** 每个组件的默认属性*/
	defaults: {
		width: 200,
		labelWidth: 55,
		labelAlign: 'right'
	},
	/**文件信息*/
	FileInfo: {
		Id: null,
		FileName: null,
		FileSize: 0,
		FileExt: null
	},
	/**附属主体名*/
	PrimaryName:null,
	/**附属主体数据ID*/
	PrimaryID:null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.on({
			boxready:function(){
				me.onCheckFile();
			}
		});
	},
	
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];

		items.push({
			fieldLabel: '附件ID',
			name: 'Id',
			hidden: true
		},{
			fieldLabel: '附属主体名',
			name: 'PrimaryName',
			value:me.PrimaryName,
			hidden: true
		},{
			fieldLabel: '附属主体数据ID',
			name: 'PrimaryID',
			value:me.PrimaryID,
			hidden: true
		},{
			fieldLabel: '是否使用',
			name: 'IsUse',
			value:true,
			hidden: true
		});

		items.push({
			fieldLabel: '文件',
			name: 'File',
			xtype: 'filefield',
			itemId:'File',
			emptyText:'必填项',allowBlank:false,
			hidden:true,
			listeners:{
				change:function(field,value){
					me.onSaveClick();
				}
			}
		});

		return items;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		
		if (!me.getForm().isValid()) return;
		
		var url = me.addUrl;
		url = JShell.System.Path.getUrl(url);
		
		me.submit({
			clientValidation:true,//进行客户端验证
			waitMsg:me.saveText,//提示信息
			waitTitle:JShell.Msg.ALERT_TITLE,//标题
			url:url,//请求的url地址
			success:function(form,action){//加载成功的处理函数
				var value = action.result.ResultDataValue;
				var data = Ext.JSON.decode(value);
				me.fireEvent('save',me,data);
			},
			failure:function(form,action){//加载失败的处理函数
				JShell.Msg.error(JShell.All.FAILURE_TEXT);
			}
		});
	},
	/**更改标题*/
	changeTitle:function(){
		var me = this;
			
		if(!me.FileInfo.FileName){
    		me.setTitle(me.defaultTitle + '-' + JShell.All.ADD); 
    	}else {
    		me.setTitle(me.defaultTitle + '-' + JShell.All.EDIT); 
    	}
	},
	/**选择图标文件*/
	onCheckFile:function(){
		var me = this,
			File = me.getComponent('File'),
			fileInputEl = File['fileInputEl'];
		fileInputEl.dom.click();
	}
});