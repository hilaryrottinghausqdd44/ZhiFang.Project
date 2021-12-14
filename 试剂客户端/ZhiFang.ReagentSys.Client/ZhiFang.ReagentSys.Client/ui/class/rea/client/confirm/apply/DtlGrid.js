/**
 * 客户端验收验货单明细列表
 * @author longfc
 * @version 2017-12-01
 */
Ext.define('Shell.class.rea.client.confirm.apply.DtlGrid', {
	extend: 'Shell.class.rea.client.confirm.basic.DtlGrid',
	
	title: '验货单明细列表',
	/**录入:apply/审核:check*/
	OTYPE: "apply",
	/**是否可编辑*/
	canEdit: true,
	/**是否多选行*/
	checkOne: false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.getStatusListData();
		me.addEvents('onAddDt', 'onAddScanCodeDt');
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(3, 0, {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			items: [{
				getClass: function(v, meta, record) {
					var status = record.get("ReaBmsCenSaleDtlConfirm_Status");
					var inCount = record.get("ReaBmsCenSaleDtlConfirm_InCount");
					if(!inCount) inCount = 0;
					//不等于已入库或者已入库数量>0
					if(status == "4" || parseFloat(inCount) > 0) {
						return '';
					} else {
						meta.tdAttr = 'data-qtip="<b>删除</b>"';
						return 'button-del hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					JShell.Msg.confirm({
						title: '<div style="text-align:center;">删除操作确认</div>',
						msg: '请确认是否删除?',
						closable: true
					}, function(but, text) {
						if(but != "ok") return;
						var rec = grid.getStore().getAt(rowIndex);
						me.deleteOne(rec);
					});
				}
			}]
		});
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = ['refresh'];
		items.push('-', {
			iconCls: 'button-add',
			itemId: "btnAddScanCode",
			text: '扫码新增',
			tooltip: '扫码新增',
			handler: function() {
				me.onAddScanCodeDtClick();
			}
		}, {
			iconCls: 'button-add',
			itemId: "btnAddManualInput",
			text: '手工新增',
			tooltip: '手工新增(无码)',
			handler: function() {
				me.onAddDtClick();
			}
		});
		var tempStatus = me.StatusList;
		items.push('-', {
			fieldLabel: '状态',
			labelWidth: 40,
			width: 125,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DtlConfirmStatus',
			data: tempStatus,
			value: "",
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		//查询框信息
		me.searchInfo = {
			width: 200,
			emptyText: '货品名称/批号',
			itemId: 'search',
			isLike: true,
			fields: ['bmscensaledtlconfirm.ReaGoodsName', 'bmscensaledtlconfirm.LotNo']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		//items.push('-', 'save');
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		me.internalWhere = me.getInternalWhere();
		return me.callParent(arguments);
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');
		var status = buttonsToolbar.getComponent('DtlConfirmStatus');
		var where = [];
		if(status) {
			var value = status.getValue();
			if(value) {
				where.push("bmscensaledtlconfirm.Status=" + value);
			}
		}
		if(search) {
			var value = search.getValue();
			if(value) {
				var searchHql = me.getSearchWhere(value);
				if(searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
		//where.push("bmscensaledtlconfirm.ReaGoodsID is not null");
		return where.join(" and ");
	},
	/**获取供货验收总单状态参数*/
	getParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "ReaBmsCenSaleDtlConfirmStatus",
				"classnamespace": "ZhiFang.Entity.ReagentSys.Client"
			}]
		};
		return params;
	},
	/**获取状态信息*/
	getStatusListData: function(callback) {
		var me = this;
		if(me.StatusList.length > 0) return;
		var params = {},
			url = JShell.System.Path.getRootUrl(JcallShell.System.ClassDict._classDicListUrl);
		params = Ext.encode(me.getParams());
		me.StatusList = [];
		me.StatusEnum = {};
		me.StatusFColorEnum = {};
		me.StatusBGColorEnum = {};
		var tempArr = [];
		JcallShell.Server.post(url, params, function(data) {
			if(data.success) {
				if(data.value) {
					if(data.value[0].ReaBmsCenSaleDtlConfirmStatus.length > 0) {
						me.StatusList.push(["", '请选择', 'font-weight:bold;color:black;text-align:center;']); //, 'font-weight:bold;text-align:center;'
						Ext.Array.each(data.value[0].ReaBmsCenSaleDtlConfirmStatus, function(obj, index) {
							var style = ['font-weight:bold;text-align:center;'];
							if(obj.FontColor) {
								//style.push('color:' + obj.FontColor);
								me.StatusFColorEnum[obj.Id] = obj.FontColor;
							}
							if(obj.BGColor) {
								style.push('color:' + obj.BGColor); //background-
								me.StatusBGColorEnum[obj.Id] = obj.BGColor;
							}
							me.StatusEnum[obj.Id] = obj.Name;
							tempArr = [obj.Id, obj.Name, style.join(';')];
							me.StatusList.push(tempArr);
						});
					}
				}
			}
		}, false);
	},

	/**@description 新增按钮点击处理方法*/
	onAddDtClick: function() {
		var me = this;
		me.fireEvent('onAddDt');
	},
	/**@description 扫码录入新增按钮点击处理方法*/
	onAddScanCodeDtClick: function() {
		var me = this;
		me.fireEvent('onAddScanCodeDt');
	},
	/**@description 删除按钮点击处理方法*/
	onDeleteDtClick: function() {
		var me = this;
	},
	setBtnDisabled: function(com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if(btn) btn.setDisabled(disabled);
		}
	}
});