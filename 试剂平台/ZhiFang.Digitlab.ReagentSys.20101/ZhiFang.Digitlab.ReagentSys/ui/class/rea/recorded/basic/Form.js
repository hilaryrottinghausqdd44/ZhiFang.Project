/**
 * 入账管理表单-基础
 * @author liangyl
 * @version 2017-06-02
 */
Ext.define('Shell.class.rea.recorded.basic.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '入账管理表单-基础',

	width: 240,
	height: 300,

	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchBmsAccountInputById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/SingleTableService.svc/ST_UDTO_AddBmsAccountInput',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdateBmsAccountInput',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,

	/**内容周围距离*/
	bodyPadding: '10px 10px 0 10px',
	/**布局方式*/
	layout: 'anchor',
	/** 每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelWidth: 55,
		labelAlign: 'right'
	},
	/***表单的默认状态,add(新增)edit(修改)show(查看)*/
	formtype: 'add',
	/**机构Id*/
	CENORG_ID: null,
	/**机构名称*/
	CENORG_NAME: null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '主键ID',
			name: 'BmsAccountInput_Id',
			hidden: true,
			type: 'key'
		});
		items.push({
			fieldLabel: '名称',
			name: 'BmsAccountInput_CName',
			emptyText: '必填项',
			allowBlank: false
		});
		items.push({
			xtype: 'textarea',
			fieldLabel: '备注',
			name: 'BmsAccountInput_Comment',
			itemId: 'BmsAccountInput_Comment',
			height: 60
		});
		items.push({
			fieldLabel: '总金额',
			name: 'BmsAccountInput_TotalPrice',
			itemId: 'BmsAccountInput_TotalPrice',
			readOnly: true,
			locked: true,
			fieldStyle: 'border-color: #ffffff; background-image: none;'
		});
		return items;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		data.BmsCenSaleDoc_OperDate = JShell.Date.getDate(data.BmsCenSaleDoc_OperDate);
		return data;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			CName: values.BmsAccountInput_CName,
			Comment: values.BmsAccountInput_Comment,
			IsUse: 1,
			TotalPrice: values.BmsAccountInput_TotalPrice
		};

		if(me.CENORG_ID) {
			entity.Lab = {
				Id: me.CENORG_ID,
				DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0]
			}
		}
		if(me.CENORG_NAME) {
			entity.LabName = me.CENORG_NAME;
		}
		//如果操作人员不存在不让保存
		if(JShell.System.Cookie.get(JShell.System.Cookie.map.USERID)) {
			entity.UserID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		}
		if(JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME)) {
			entity.UserName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		}

		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();

		var fields = [
			'Id', 'CName', 'Comment', 'TotalPrice'
		];

		entity.fields = fields.join(',');

		entity.entity.Id = values.BmsCenSaleDoc_Id;
		return entity;
	},
	isAdd: function() {
		var me = this;

		me.getComponent('buttonsToolbar').hide();
		me.setReadOnly(false);

		me.formtype = 'add';
		me.PK = '';
		me.changeTitle(); //标题更改
		me.enableControl(); //启用所有的操作功能
		me.onResetClick();
	}
});