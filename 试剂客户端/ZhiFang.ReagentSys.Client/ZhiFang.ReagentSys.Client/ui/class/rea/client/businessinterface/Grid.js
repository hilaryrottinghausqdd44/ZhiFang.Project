/**
 * 业务接口配置
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.businessinterface.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox'
	],
	
	title: '业务接口配置列表',
	width: 800,
	height: 500,
	
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBusinessInterfaceByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBusinessInterface',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBusinessInterfaceByField',
	
	/**默认加载数据*/
	defaultLoad: true,
	/**接口类型Key*/
	InterfaceType: "ReaBusinessInterfaceType",
	
	/**用户UI配置Key*/
	userUIKey: 'businessinterface.Grid',
	/**用户UI配置Name*/
	userUIName: "业务接口配置列表",
	
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.InterfaceType, false, false, null);

		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: 'NewsGridEditing'
		}); //自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBusinessInterface_InterfaceType',
			text: '接口类型',
			sortable: false,
			width: 100,
			editor: {
				fieldLabel: '',
				xtype: 'uxSimpleComboBox',
				hasStyle: true,
				data: JShell.REA.StatusList.Status[me.InterfaceType].List,
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						var records = me.getSelectionModel().getSelection();
						if(records.length != 1) {
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
						records[0].set('ReaBusinessInterface_InterfaceType', newValue);
						me.getView().refresh();
					}
				}
			},
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.InterfaceType].Enum != null)
					v = JShell.REA.StatusList.Status[me.InterfaceType].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.InterfaceType].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.InterfaceType].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.InterfaceType].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.InterfaceType].FColor[value];
				var style = 'font-weight:bold;';
				if(bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if(fColor) {
					style = style + "color:" + fColor + ";";
				}
				if(v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaBusinessInterface_CName',
			text: '接口名称',
			width: 150,
			editor: {}
		}, {
			dataIndex: 'ReaBusinessInterface_URL',
			text: '调用URL入口',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaBusinessInterface_AppKey',
			text: 'AppKey',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaBusinessInterface_AppPassword',
			text: 'AppPassword',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaBusinessInterface_ReaServerLabcCode',
			text: '实验室平台机构码',
			width: 110,
			editor: {}
		}, {
			dataIndex: 'ReaBusinessInterface_ZX1',
			text: '专项1',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaBusinessInterface_ZX2',
			text: '专项2',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaBusinessInterface_ZX3',
			text: '专项3',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaBusinessInterface_Visible',
			text: '启用',
			width: 50,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true
			}
		}, {
			dataIndex: 'ReaBusinessInterface_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-', 'add', 'edit', 'del', '-', 'save'];
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '接口名称',
			fields: ['reabusinessinterface.CName']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.fireEvent('addclick', me);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		me.fireEvent('editclick', me);
	},
	/**保存*/
	onSaveClick: function() {
		var me = this,
			records = me.store.data.items;

		var isError = false;
		var changedRecords = me.store.getModifiedRecords(), //获取修改过的行记录
			len = changedRecords.length;

		if(len == 0) {
			JShell.Msg.alert("没有变更，不需要保存！");
			return;
		}
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for(var i = 0; i < len; i++) {
			me.updateInfo(i, changedRecords[i]);
		}
	},
	/**修改信息*/
	updateInfo: function(i, record) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var entity = {
			Id: record.get('ReaBusinessInterface_Id'),
			Visible: record.get('ReaBusinessInterface_Visible') ? 1 : 0,
			ZX3: record.get('ReaBusinessInterface_ZX3'),
			ZX2: record.get('ReaBusinessInterface_ZX2'),
			ZX1: record.get('ReaBusinessInterface_ZX1'),
			ReaServerLabcCode: record.get('ReaBusinessInterface_ReaServerLabcCode'),
			AppPassword: record.get('ReaBusinessInterface_AppPassword'),
			AppKey: record.get('ReaBusinessInterface_AppKey'),
			URL: record.get('ReaBusinessInterface_URL'),
			CName: record.get('ReaBusinessInterface_CName'),
			InterfaceType: record.get('ReaBusinessInterface_InterfaceType')
		};

		var fields = 'Id,Visible,ZX3,ZX2,ZX1,ReaServerLabcCode,' +
			'AppPassword,AppKey,URL,CName,InterfaceType';
		var params = Ext.JSON.encode({
			entity: entity,
			fields: fields
		});
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.saveCount++;
				if(record) {
					record.set(me.DelField, true);
					record.commit();
				}
			} else {
				me.saveErrorCount++;
				if(record) {
					record.set(me.DelField, false);
					record.commit();
				}
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength) {
				me.hideMask(); //隐藏遮罩层
				if(me.saveErrorCount == 0) {
					me.onSearch();
				} else {
					JShell.Msg.error("保存信息有误！");
				}
			}
		}, false);
	}
});