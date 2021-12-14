/**
 * 默认模板设置（根据类型）
 * @author longfc
 * @version 2020-03-27
 */
Ext.define('Shell.class.blood.template.DefaultTemplateGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '默认模板设置',
	width: 700,
	height: 350,
	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBTemplateByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBTemplateByField',
	/**默认加载数据*/
	defaultLoad: false,
	/**货品编码*/
	ReaGoodsNo: null,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**带分页按钮栏*/
	hasPagingtoolbar: false,
	/**模板信息类型Key*/
	BTemplateType: "BTemplateType",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.enableControl(true);
	},
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.BTemplateType, true, false, null);
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'BTemplate_CName',
			text: '模板名称',
			width: 160,
			defaultRenderer: true
		}, {
			dataIndex: 'BTemplate_SName',
			text: '简称',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BTemplate_Shortcode',
			text: '快捷码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BTemplate_IsDefault',
			text: '默认模板',
			width: 80,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor: {
				xtype: 'uxBoolComboBox',
				value: true,
				hasStyle: true,
				listeners: {
					change: function(com, newValue, oldValue, eOpts) {
						var records = me.getSelectionModel().getSelection();
						if (records.length != 1) {
							JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
							return;
						}
						me.setIsDefault(records[0].get(me.PKField), newValue);
					}
				}
			}
		}, {
			dataIndex: 'BTemplate_Id',
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
		var items = ['refresh', '-', {
			emptyText: '模板类型',
			xtype: 'uxSimpleComboBox',
			name: 'BTemplateType',
			itemId: 'BTemplateType',
			hasStyle: true,
			data: JShell.REA.StatusList.Status[me.BTemplateType].List,
			listeners: {
				change: function(com, newValue, oldValue, eOpts) {
					me.onSearch();
				}
			},
			width: 100,
			labelWidth: 0
		}, '-', 'save'];

		return items;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			BTemplateType = buttonsToolbar.getComponent('BTemplateType').getValue(),
			params = [];
		me.internalWhere = '';
		if (BTemplateType) {
			params.push("btemplate.TypeID='" + BTemplateType + "'");
		}
		if (params.length > 0) {
			me.internalWhere = params.join(' and ');
		}

		return me.callParent(arguments);
	},
	/**保存*/
	onSaveClick: function() {
		var me = this,
			records = me.store.data.items;
		var isError = false;
		//校验
		var isExect = me.isVerification();
		if (!isExect) return;
		var changedRecords = me.store.getModifiedRecords(),
			len = changedRecords.length;
		if (len == 0) {
			JShell.Msg.alert("没有变更，不需要保存！");
			return;
		}
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			//存在id 编辑
			if (changedRecords[i].get(me.PKField)) {
				me.updateOne(i, changedRecords[i]);
			}
		}
		if (me.saveCount + me.saveErrorCount == me.saveLength) {
			me.hideMask(); //隐藏遮罩层
			if (me.saveErrorCount == 0) {
				me.fireEvent('save', me);
			}
		}
	},
	updateOne: function(i, record) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = {
			entity: {
				Id: record.get('BTemplate_Id'),
				IsDefault: record.get('BTemplate_IsDefault') ? 1 : 0
			}
		};

		params.fields = 'Id,IsDefault';
		var entity = Ext.JSON.encode(params);

		JShell.Server.post(url, entity, function(data) {
			if (data.success) {
				me.saveCount++;
				if (record) {
					record.set(me.DelField, true);
					record.commit();
				}
			} else {
				me.saveErrorCount++;
				if (record) {
					record.set(me.DelField, false);
					record.commit();
				}
			}

		}, false);
	},
	//同一类型只能设置一个最小单位
	setIsDefault: function(id, value) {
		var me = this;
		me.store.each(function(record) {
			if (record.get(me.PKField) == id) {
				record.set('BTemplate_IsDefault', value);
			} else {
				record.set('BTemplate_IsDefault', false);
			}
		});
		me.getView().refresh();
	},
	/**保存前验证*/
	isVerification: function() {
		var me = this,
			records = me.store.data.items,
			isExect = true,
			len = records.length;
		if (len == 0) {
			isExect = false;
			return isExect;
		}
		var arr = [],
			num = 0;
		var msg = '';
		//验证
		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var IsDefault = rec.get('BTemplate_IsDefault');
			if (IsDefault == '1') {
				num += 1;
			}
		}
		if (num > 1) {
			msg += '设置多个默认模板,不能保存<br>';
			isExect = false;
		}
		if (!isExect) {
			JShell.Msg.error(msg);
		}
		return isExect;
	}
});
