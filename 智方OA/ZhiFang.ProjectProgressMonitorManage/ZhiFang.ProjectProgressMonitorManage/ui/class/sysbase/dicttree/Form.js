/**
 * 树类型表单
 * @author 
 * @version 2016-06-22
 */
Ext.define('Shell.class.sysbase.dicttree.Form', {
	extend: 'Shell.ux.form.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],

	title: '类型树信息',
	width: 530,
	height: 240,
	/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
	IDS: "",
	/**获取树的最大层级数*/
	LEVEL: "",
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/UDTO_SearchBDictTreeById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ProjectProgressMonitorManageService.svc/UDTO_AddBDictTree',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/UDTO_UpdateBDictTreeByField',

	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,

	/** 每个组件的默认属性*/
	defaults: {
		width: 240,
		labelWidth: 85,
		labelAlign: 'right'
	},
	/**机构ID*/
	LabId: 0,
	/**上级机构ID*/
	ParentID: 0,
	/**上级机构名称*/
	ParentName: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);

	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			x: 10,
			y: 10,
			fieldLabel: '名称',
			name: 'BDictTree_CName',
			itemId: 'BDictTree_CName',
			emptyText: '必填项',
			allowBlank: false
		}, {
			x: 10,
			y: 35,
			fieldLabel: '快捷码',
			itemId: 'BDictTree_Shortcode',
			name: 'BDictTree_Shortcode',
			emptyText: '',
			allowBlank: true
		}, {
			x: 100,
			y: 60,
			boxLabel: '是否使用',
			name: 'BDictTree_IsUse',
			labelWidth: 85,
			labelAlign: 'right',
			xtype: 'checkbox',
			checked: true
		});
		items.push({
			x: 250,
			y: 10,
			fieldLabel: '简称',
			name: 'BDictTree_SName'
		}, {
			x: 250,
			y: 35,
			fieldLabel: '显示次序',
			name: 'BDictTree_DispOrder',
			xtype: 'numberfield',
			value: 0,
			allowBlank: false
		}, {
			x: 250,
			y: 60,
			fieldLabel: '上级节点',
			emptyText: '必填项',
			enableKeyEvents: false,
			editable: false,
			allowBlank: false,
			itemId: 'BDictTree_ParentName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			value: me.ParentName,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.sysbase.dicttree.CheckTree', {
					resizable: false,
					selectId: me.ParentID, //默认选中节点ID
					hideNodeId: me.PK, //默认隐藏节点ID
					treeShortcodeWhere: me.treeShortcodeWhere,
					IDS: me.IDS,
					/**获取树的最大层级数*/
					LEVEL: me.LEVEL,
					listeners: {
						accept: function(p, record) {
							me.onParentModuleAccept(record);
							p.close();
						}
					}
				}).show();
			}
		}, {
			fieldLabel: '上级机构主键ID',
			hidden: true,
			value: me.ParentID,
			name: 'BDictTree_ParentID',
			itemId: 'BDictTree_ParentID'
		});
		items.push({
			x: 10,
			y: 85,
			fieldLabel: '备注',
			width: 480,
			height: 85,
			name: 'BDictTree_Memo',
			xtype: 'textarea'
		});

		items.push({
			fieldLabel: '主键ID',
			name: 'BDictTree_Id',
			hidden: true
		}, {
			fieldLabel: '时间戳',
			name: 'BDictTree_DataTimeStamp',
			hidden: true
		});

		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			CName: values.BDictTree_CName,
			Shortcode: values.BDictTree_Shortcode,
			IsUse: values.BDictTree_IsUse ? 1 : 0,
			SName: values.BDictTree_SName,
			DispOrder: values.BDictTree_DispOrder,
			Memo: values.BDictTree_Memo,
			CreatorID: JShell.System.Cookie.get(JShell.System.Cookie.map.USERID),
			CreatorName: JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME),
			ParentID: values.BDictTree_ParentID
		};

		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			fields = me.getStoreFields(),
			entity = me.getAddParams(),
			fieldsArr = [];

		for(var i in fields) {
			var arr = fields[i].split('_');
			if(arr.length > 2) continue;
			if(arr[1] != undefined) {
				fieldsArr.push(arr[1]);
			}
		}
		entity.fields = fieldsArr.join(',');

		entity.entity.Id = values.BDictTree_Id;
		entity.entity.DataTimeStamp = values.BDictTree_DataTimeStamp.split(',')
		return entity;
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		return data;
	},
	/**选择上级机构*/
	onParentModuleAccept: function(record) {
		var me = this,
			ParentID = me.getComponent('BDictTree_ParentID'),
			ParentName = me.getComponent('BDictTree_ParentName');

		if(record.get('tid') == 0) {
			JShell.Msg.error('不能选择根节点');
			return;
		}
		ParentID.setValue(record.get('tid'));
		ParentName.setValue(record.get('text') || '');
	}
});