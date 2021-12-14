/**
 * 送检单位选择列表
 * @author longfc
 * @version 2016-05-17
 */
Ext.define('Shell.class.pki.dunitdealer.CheckGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '送检单位选择列表',
	width: 270,
	height: 500,
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**默认选中*/
	autoSelect: false,
	/**是否单选*/
	checkOne: true,
	/**是否带确认按钮*/
	hasAcceptButton: true,
	oldRecords: [],
	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchBLaboratoryByHQL?isPlanish=true',
	plugins: Ext.create('Ext.grid.plugin.CellEditing', {
		clicksToEdit: 1
	}),
	/**@overwrite加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.oldRecords = records;
		me.enableControl(); //启用所有的操作功能

		if (me.errorInfo) {
			var error = me.errorFormat.replace(/{msg}/, me.errorInfo);
			me.getView().update(error);
			me.errorInfo = null;
		} else {
			if (!records || records.length <= 0) {
				var msg = me.msgFormat.replace(/{msg}/, JShell.Server.NO_DATA);
				me.getView().update(msg);
			}
		}

		if (!records || records.length <= 0) {
			me.fireEvent('nodata', me);
			return;
		}
		//默认选中处理
		me.doAutoSelect(records, me.autoSelect);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('checkchange');
		//自定义按钮功能栏
		me.buttonToolbarItems = [];
		//查询框信息
		me.searchInfo = me.searchInfo || {
			width: '100%',
			emptyText: '送检单位',
			isLike: true,
			fields: ['blaboratory.CName']
		};
		//自定义按钮功能栏
		me.buttonToolbarItems.push({
			type: 'search',
			info: me.searchInfo
		}, '->', {
			xtype: 'checkbox',
			boxLabel: '只显示已维护送检单位',
			checked: false,
			name: 'checkbox',
			itemId: 'checkbox',
			handler: function(com, newValue, oldValue, eOpts) {
				me.fireEvent('checkchange', com, newValue, oldValue, eOpts);
			}
		});

		me.multiSelect = true;
		me.selType = 'checkboxmodel';
		me.cboUrl = JShell.System.Path.ROOT + '/BaseService.svc/ST_UDTO_SearchBBillingUnitByHQL?isPlanish=true&fields=BBillingUnit_DataTimeStamp,BBillingUnit_Id,BBillingUnit_Name'; //cboStore

		var lists = [];
		JShell.Server.get(me.cboUrl, function(data) {
			lists = data.value.list;
		}, false);
		me.cboStore = new Ext.data.Store({
			fields: ['BBillingUnit_Id', 'BBillingUnit_Name', 'BBillingUnit_DataTimeStamp'],
			data: lists
		});

		//数据列
		me.columns = [{
			dataIndex: 'BLaboratory_CName',
			text: '送检单位名称',
			flex: 1,//width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'BLaboratory_BBillingUnit_Name',
			text: '默认开票方名称',
			hidden: true,
			width: 110,
			defaultRenderer: true
		}, {
			dataIndex: 'BLaboratory_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BLaboratory_DataTimeStamp',
			text: '时间戳',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'BLaboratory_BBillingUnit_Id',
			text: '默认开票方',
			flex: 1,
			//width: 160,
			hidden: true,
			hideable: false,
			renderer: function(value, metaData, record, rowIndex, colIndex, store, view) {
				return record.get("BLaboratory_BBillingUnit_Name");
			},
			editor: new Ext.form.field.ComboBox({
				mode: 'local',
				editable: false,
				typeAhead: false,
				forceSelection: true,
				queryMode: 'local',
				displayField: 'BBillingUnit_Name',
				valueField: 'BBillingUnit_Id',
				store: me.cboStore,
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						var record = com.ownerCt.editingPlugin.context.record;
						var NameValue = "",
							DataTimeStampValue = "";
						if (newValue && newValue != "") {
							var index = com.store.find("BBillingUnit_Id", newValue);
							var rec = com.getStore().getAt(index);
							if (rec) {
								NameValue = rec.get("BBillingUnit_Name");
								DataTimeStampValue = rec.get("BBillingUnit_DataTimeStamp");
							}
						}
						if (record) {
							record.set("BLaboratory_BBillingUnit_Name", NameValue);
							record.set("BLaboratory_BBillingUnit_DataTimeStamp", DataTimeStampValue);
						}
					},
					select: function(com, records, eOpts) {

					}
				}
			})
		}, {
			dataIndex: 'BLaboratory_BBillingUnit_DataTimeStamp',
			text: '默认开票方时间戳',
			hidden: true,
			hideable: false
		}];
		me.callParent(arguments);
	}
});