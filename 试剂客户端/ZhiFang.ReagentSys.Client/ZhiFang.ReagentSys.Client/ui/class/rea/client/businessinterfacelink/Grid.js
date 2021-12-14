/**
 * 业务接口关系配置信息
 * @author longfc	
 * @version 2018-11-19
 */
Ext.define('Shell.class.rea.client.businessinterfacelink.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox'
	],
	title: '业务接口关系配置信息列表',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBusinessInterfaceLinkByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBusinessInterfaceLink',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBusinessInterfaceLinkByField',
	/**默认加载数据*/
	defaultLoad: true,
	/**业务类型Key*/
	BusinessType: "ReaBusinessType",
	//业务接口ID
	BusinessID: null,
	//业务接口名称
	BusinessCName: null,

	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.BusinessType, false, false, null);

		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId: 'NewsGridEditing'
		}); //自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBusinessInterfaceLink_BusinessType',
			text: '业务类型',
			sortable: false,
			width: 100,
			editor: {
				fieldLabel: '',
				xtype: 'uxSimpleComboBox',
				hasStyle: true,
				data: JShell.REA.StatusList.Status[me.BusinessType].List,
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						var records = me.getSelectionModel().getSelection();
						if(records.length != 1) {
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
						records[0].set('ReaBusinessInterfaceLink_BusinessType', newValue);
						me.getView().refresh();
					}
				}
			},
			renderer: function(value, meta) {
				var v = value;
				if(JShell.REA.StatusList.Status[me.BusinessType].Enum != null)
					v = JShell.REA.StatusList.Status[me.BusinessType].Enum[value];
				var bColor = "";
				if(JShell.REA.StatusList.Status[me.BusinessType].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.BusinessType].BGColor[value];
				var fColor = "";
				if(JShell.REA.StatusList.Status[me.BusinessType].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.BusinessType].FColor[value];
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
			dataIndex: 'ReaBusinessInterfaceLink_BusinessCName',
			text: '所属接口',
			hidden: true,
			width: 150
		}, {
			dataIndex: 'ReaBusinessInterfaceLink_BusinessId',
			text: '业务接口ID',
			hidden: true
		}, {
			dataIndex: 'ReaBusinessInterfaceLink_CompanyName',
			text: '供应商',
			width: 100
		}, {
			dataIndex: 'ReaBusinessInterfaceLink_ZX1',
			text: '专项1',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaBusinessInterfaceLink_ZX2',
			text: '专项2',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaBusinessInterfaceLink_ZX3',
			text: '专项3',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaBusinessInterfaceLink_Visible',
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
			dataIndex: 'ReaBusinessInterfaceLink_Id',
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
			fields: ['ReaBusinessInterfaceLink.CName']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		if(!me.BusinessID) {
			var msg = me.msgFormat.replace(/{msg}/, "获取所属业务接口参数值(BusinessID)为空!");
			me.getView().update(msg);
			return false;
		}
		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var internalWhere = "";
		if(me.BusinessID) internalWhere = "reabusinessinterfacelink.BusinessID=" + me.BusinessID;
		return "";
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		var url = me.callParent(arguments);
		return url;
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
			Id: record.get('ReaBusinessInterfaceLink_Id'),
			Visible: record.get('ReaBusinessInterfaceLink_Visible') ? 1 : 0,
			ZX3: record.get('ReaBusinessInterfaceLink_ZX3'),
			ZX2: record.get('ReaBusinessInterfaceLink_ZX2'),
			ZX1: record.get('ReaBusinessInterfaceLink_ZX1'),
			BusinessType: record.get('ReaBusinessInterfaceLink_BusinessType')
		};

		var fields = 'Id,Visible,ZX3,ZX2,ZX1,BusinessType';
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