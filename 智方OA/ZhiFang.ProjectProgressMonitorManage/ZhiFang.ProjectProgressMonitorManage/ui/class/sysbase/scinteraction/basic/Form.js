/**
 * 互动信息
 * @author longfc
 * @version 2017-03-21
 */
Ext.define('Shell.class.sysbase.scinteraction.basic.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.UMeditor'
	],
	title: '互动信息',
	width: 600,
	height: 120,

	/**新增服务地址*/
	addUrl: '/SystemCommonService.svc/SC_UDTO_AddSCInteractionExtend',
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
	/**话题标志*/
	IsCommunication: false,
	/**话题标题*/
	TopicTitle: "",
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//解决在线编辑器换行出现滚动条后工具栏会被隐藏,需要手工调整高度,工具栏才不会被隐藏
		setTimeout(function() {
			me.setHeight(me.height+1);
		}, 800);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onTopicClick');
		me.buttonToolbarItems = ['->', {
			xtype: 'button',
			text: '提交话题',
			iconCls: 'button-save',
			tooltip: '提交话题',
			itemId: 'btnTopic',
			handler: function() {
				me.fireEvent('onTopicClick', me);
				
				JShell.Msg.confirm({
					title: '<div style="text-align:center;">话题标题</div>',
					msg: '',
					closable: false,
					multiline: true //多行输入框
				}, function(but, text) {
					if(but != "ok") return;
					me.TopicTitle = "" + text;
					if(me.TopicTitle == "") {
						JShell.Msg.alert("话题标题为空");
					} else {
						me.IsCommunication = true;
						me.onSaveClick();
					}
				});

			}
		}, {
			xtype: 'button',
			text: '提交交流',
			itemId: 'btnSaveInteraction',
			iconCls: 'button-save',
			disabled: true,
			tooltip: '提交交流',
			handler: function() {
				me.TopicTitle = "";
				me.IsCommunication = false;
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
		height = (height < 80 ? 80 : height);
		items.push({
			name: 'Content',
			itemId: 'Content',
			border: false,
			xtype: 'umeditor',
			width: '100%',
			height: height,
			emptyText: '等待输入...'
		});
		return items;
	},
	setHeight: function(height) {
		var me = this;
		if(height) height = height < 120 ? 120 : height;
		return me.setSize(undefined, height);
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		if(!values.Content || !values.Content.trim()) {
			return null;
		}
		values.Content = values.Content.replace(/\\/g, '&#92');
		me.TopicTitle = me.TopicTitle.replace(/\\/g, '&#92');
		me.TopicTitle = me.TopicTitle.replace(/[\r\n]/g, '<br />');
		var entity = {
			Contents: values.Content,
			IsUse: true,
			IsCommunication: me.IsCommunication,
			Memo: me.TopicTitle
		};
		//话题的回复交流数默认为0
		if(me.IsCommunication==true){
			entity.ReplyCount=0;
		}
		//发送人
		entity.SenderID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		if(entity.SenderID == "") entity.SenderID = -1;
		entity.SenderName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);

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