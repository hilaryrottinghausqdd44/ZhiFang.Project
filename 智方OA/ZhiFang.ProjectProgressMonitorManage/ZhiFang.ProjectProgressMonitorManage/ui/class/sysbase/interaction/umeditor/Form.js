/**
 * 互动信息
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.interaction.umeditor.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.UMeditor'
	],
	title: '互动信息',
	width: 600,
	height: 400,

	/**新增服务地址*/
	addUrl: '',
	/**附件对象名*/
	objectName: '',
	/**附件关联对象名*/
	fObejctName: '',
	/**附件关联对象主键*/
	fObjectValue: '',
	/**交流关联对象是否ID,@author Jcall,@version 2016-08-19*/
	fObjectIsID: false,

	/** 每个组件的默认属性*/
	layout: 'fit',
	bodyPadding: 1,
	/**内容自动显示*/
	autoScroll: false,
	/**表单的默认状态*/
	formtype: 'add',
	/**启用表单状态初始化*/
	openFormType: true,
	/**显示成功信息*/
	showSuccessInfo: false,
	autoHeight : true,//高度自适应
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
//		me.on({
//			resize:function(form,width,height,oldWidth,oldHeight,eOpts ){
//				var content = me.getComponent('Content');
//				content.setHeight(height);
//				//console.log("height:"+content.getHeight());
//			}
//		});
	},
	initComponent: function() {
		var me = this;
		me.buttonToolbarItems = ['->', {
			xtype: 'button',
			text: '提交',
			iconCls: 'button-save',
			tooltip: '提交数据',
			handler: function() {
				me.onSaveClick();
			}
		}];
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		var height = me.height;
		height = (height < 40 ? 40 : height);
		//项目名称
		items.push({
			xtype: 'textarea',
			name: 'Content',
			itemId: 'Content',
			//margin: '2px 2px 2px 2px',
			xtype: 'umeditor',
			width: '100%',
			height:height,
			autoScroll: true,
			border: false,
			emptyText: '等待输入...'
		});

		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		if(!values.Content || !values.Content.trim()) {
			return null;
		}
		values.Content = values.Content.replace(/\\/g, '&#92');
		var entity = {
			Contents: values.Content,
			IsUse: true
		};
		//发送人
		entity.SenderID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		entity.SenderName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		//附件关联对象主键
		//		entity[me.fObejctName] = {
		//			Id:me.fObjectValue,
		//			DataTimeStamp:[0,0,0,0,0,0,0,0]
		//		};

		/**交流关联对象是否ID,@author Jcall,@version 2016-08-19*/
		if(me.fObjectIsID) {
			entity[me.fObejctName + 'ID'] = me.fObjectValue;
		} else {
			entity[me.fObejctName] = {
				Id: me.fObjectValue,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			};
		}

		return { entity: entity };
	}
});