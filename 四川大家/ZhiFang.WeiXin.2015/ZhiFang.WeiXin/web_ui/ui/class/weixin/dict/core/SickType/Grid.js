/**
 * 就诊类型字典表
 * @author guozhaojing
 * @version 2018-03-27
 */
Ext.define('Shell.class.weixin.dict.core.SickType.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 800,
	height: 500,
	PKField: 'SickType_Id',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchSickTypeByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_DelSickType',
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**默认加载*/
	defaultLoad: true,

/**复制按钮点击次数*/
    copyTims:0,
    
	/**查询栏参数设置*/
	searchToolbarConfig: {},
	
	searchInfo: {
		width: 120,
			emptyText: '编码/名称',
			isLike: true,
			itemId: 'search',
			fields: ['sicktype.CName',"sicktype.Id"]
	},

	initComponent: function() {
		var me = this;
		//  	//查询框信息
		//		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
		//			fields:['sicktype.CName']};
		me.columns = me.createGColumns();
		me.callParent(arguments);
	},

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
		//me.showSearch();
	},

	//创建列的方法名名不能为createColumns，因为继承的共方法中有这个方法名如果重写则没有序号列
	createGColumns: function() {
		var me = this;
		var columns = [{
			text: '编码',
			dataIndex: 'SickType_Id',
			width: 100,
			isKey: true,
			hideable: false
		}, {
			text: '中文名称',
			dataIndex: 'SickType_CName',
			width: 100,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '简码',
			dataIndex: 'SickType_ShortCode',
			width: 100,
			sortable: true,
			menuDisabled: true,
			defaultRenderer: true
		}];
		return columns;
	},

	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		items.push(me.createDefaultButtonToolbarItems());
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		return items;
	},

	createDefaultButtonToolbarItems: function() {
		var me = this;
		
		var items = [
			{
				text: '复制',
				tooltip: '复制',
				iconCls: 'button-copy',
				name: 'btnCopy ',
				itemId: 'btnCopy',
				handler: function() {}
			},
			{
				fieldLabel: '编码id',
				hidden: true,
				xtype: 'textfield',
				name: 'SelectClienteleId',
				itemId: 'SelectClienteleId'
			},
			{
				fieldLabel: '',
				xtype: 'uxCheckTrigger',
				emptyText: '实验室',
				width: 300,
				labelSeparator: '',
				hidden: true,
				labelWidth: 55,
				labelAlign: 'right',
				name: 'SelectClienteleName',
				itemId: 'SelectClienteleName',
				className: 'Shell.class.weixin.dict.core.SickType.CheckGrid',
				classConfig: {
					title: '实验室选择',
					/**是否单选*/
					checkOne: false
				},
				listeners: {
					check: function(p, record) {
						me.onClienteleAccept(record);
						p.close();
					}
				}
			},
			{
				text: '保存',
				tooltip: '保存',
				iconCls: 'button-save',
				itemId: 'btnSave',
				hidden: true,
				handler: function() {
					var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
					var SelectClienteleId = buttonsToolbar2.getComponent('SelectClienteleId');
					var SelectClienteleName = buttonsToolbar2.getComponent('SelectClienteleName');
					var Arr = [];
					var strVal = SelectClienteleId.getValue();
					if(strVal) {
						Arr = strVal.split(",")
					}
					var ItemNoList = me.getLabTestItem();
					if(Arr.length == 0) {
						JShell.Msg.error('请选择目标实验室');
						return;
					}
					var isall = false;
					var msg = '确定把当前勾选的数据复制到目标实验室吗?';
					if(ItemNoList.length == 0) {
						isall = true;
						msg = '确定把当前中心表所有数据复制到目标实验室吗?'
					}
					JShell.Msg.confirm({
						msg: msg
					}, function(but) {
						if(but == "ok") {

							me.onCopyClick(Arr, ItemNoList, isall);
						}
					});
				}
			}
		];
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},

	/*复制到某个实验室的选择*/
	onClienteleAccept: function(record) {
		var me = this;
		var bottomToolbar = me.getComponent('buttonsToolbar2');
		var SelectClienteleId = bottomToolbar.getComponent('SelectClienteleId');
		var SelectClienteleName = bottomToolbar.getComponent('SelectClienteleName');
		var idArry = [];
		var nameArry = [];

		if(record) {
			for(var i = 0; i < record.length; i++) {
				var id = record[i] ? record[i].get('CLIENTELE_Id') : '';
				var name = record[i] ? record[i].get('CLIENTELE_CNAME') : '';
				idArry.push(id);
				nameArry.push(name);
			}
		}
		SelectClienteleId.setValue(idArry);
		SelectClienteleName.setValue(nameArry);
	},

	getLabTestItem: function() {
		var me = this;
		var Id = '',
			Arr = [];
		var records = me.getSelectionModel().getSelection();
		console.log(records);
		if(records.length > 0) {
			for(var i = 0; i < records.length; i++) {
				Id = records[i] ? records[i].get('SickType_Id') : '';
				if(Id) Arr.push(Id);
			}
		}
		return Arr;
	},

	onCopyClick: function(LabCodeList, ItemNoList, isall) {
		var me = this;
		JShell.Win.open('Shell.class.weixin.dict.core.SickType.MsgFrom', {
			resizable: false,
			formtype: 'add',
			maximizable: false, //是否带最大化功能
			LabCodeList: LabCodeList,
			ItemNoList: ItemNoList,
			IsAll: isall,
			listeners: {
				save: function(p, entity) {
					p.close();
				}
			}
		}).show();
	},

	/**初始化监听*/
	initFilterListeners: function() {
		var me = this;
		var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
		var btnSave = buttonsToolbar2.getComponent('btnSave');
		var SelectClienteleName = buttonsToolbar2.getComponent('SelectClienteleName');
		var btnCopy = buttonsToolbar2.getComponent('btnCopy');

		btnCopy.on({
			click: function(btn, e) {
				if(Number(me.copyTims) == 0) {
					me.copyTims = Number(me.copyTims) + 1;
					SelectClienteleName.show();
					btnSave.show();
				} else {
					me.copyTims = 0;
					SelectClienteleName.hide();
					btnSave.hide();
				}
			}
		});
	},
});