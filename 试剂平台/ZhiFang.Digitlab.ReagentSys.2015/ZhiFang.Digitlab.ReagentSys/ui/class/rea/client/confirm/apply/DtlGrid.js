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
		me.addEvents('onAddDt', 'onAddScanCodeDt','onFullScreenClick');
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
					var status = record.get("BmsCenSaleDtlConfirm_Status");
					var inCount = record.get("BmsCenSaleDtlConfirm_InCount");
					if(!inCount) inCount = 0;
					//不等于已入库或者已入库数量>0
					if(status == "4" || parseInt(inCount) > 0) {
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
		var items = [{
			text:'全屏',
			itemId:'launchFullscreen',
			iconCls:'button-arrow-out',
			handler:function(){
				me.onFullScreenClick();
			}
		}, '-', 'refresh'];
		items.push( '-', {
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
		items.push('-',{
			fieldLabel: '状态',
			labelWidth: 40,
			width: 125,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'DtlConfirmStatus',
			data: tempStatus,
			value:"",
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		});
		//查询框信息
		me.searchInfo = {
			width: 200,
			emptyText: '产品名称/产品批号',
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
		return me.callParent(arguments);;
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
				where.push(me.getSearchWhere(value));
			}
		}
		where.push("bmscensaledtlconfirm.ReaGoodsID is not null");
		return where.join(" and ");
	},
	/**获取供货验收总单状态参数*/
	getParams: function() {
		var me = this,
			params = {};
		params = {
			"jsonpara": [{
				"classname": "BmsCenSaleDtlConfirmStatus",
				"classnamespace": "ZhiFang.Digitlab.Entity.ReagentSys"
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
					if(data.value[0].BmsCenSaleDtlConfirmStatus.length > 0) {
						me.StatusList.push(["", '全部', 'font-weight:bold;color:black;text-align:center;']); //, 'font-weight:bold;text-align:center;'
						Ext.Array.each(data.value[0].BmsCenSaleDtlConfirmStatus, function(obj, index) {
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
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if(len == 0) return;
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for(var i = 0; i < len; i++) {
			me.updateOneInfo(i, records[i]);
		}
	},
	/**获取单个的修改封装信息*/
	getOneUpdateInfo: function(record) {
		var me = this;
		var id = record.get(me.PKField);
		var entity = {
			Id: id,
			GoodsUnit: record.get("BmsCenSaleDtlConfirm_GoodsUnit"),
			UnitMemo: record.get("BmsCenSaleDtlConfirm_UnitMemo"),
			GoodsNo: record.get("BmsCenSaleDtlConfirm_GoodsNo"),
			UnitMemo: record.get("BmsCenSaleDtlConfirm_UnitMemo"),
			BiddingNo: record.get("BmsCenSaleDtlConfirm_BiddingNo"),
			LotNo: record.get("BmsCenSaleDtlConfirm_LotNo"),
			ProdGoodsNo: record.get("BmsCenSaleDtlConfirm_ProdGoodsNo"),
			ApproveDocNo: record.get("BmsCenSaleDtlConfirm_ApproveDocNo"),
			RegisterNo: record.get("BmsCenSaleDtlConfirm_RegisterNo"),
			AcceptMemo: record.get("BmsCenSaleDtlConfirm_AcceptMemo")
		};
		var ProdDate = record.get("BmsCenSaleDtlConfirm_ProdDate");
		var InvalidDate = record.get("BmsCenSaleDtlConfirm_InvalidDate");
		var RegisterInvalidDate = record.get("BmsCenSaleDtlConfirm_RegisterInvalidDate");

		if(ProdDate) entity.ProdDate = JShell.Date.toServerDate(ProdDate);
		if(InvalidDate) entity.InvalidDate = JShell.Date.toServerDate(InvalidDate);
		if(RegisterInvalidDate) entity.RegisterInvalidDate = JShell.Date.toServerDate(RegisterInvalidDate);

		var TaxRate = record.get("BmsCenSaleDtlConfirm_TaxRate");
		if(TaxRate) entity.TaxRate = TaxRate;
		return entity;
	},
	getUpdateFields: function(record) {
		var me = this;
		var fields = [
			'Id', 'GoodsUnit', 'UnitMemo', 'GoodsNo', 'BiddingNo', 'LotNo', 'ProdGoodsNo', 'ApproveDocNo', 'RegisterNo', 'AcceptMemo', 'TaxRate'
		];
		fields.push("ProdDate", "InvalidDate", "RegisterInvalidDate");
		//fields.push("GoodsQty", "Price", "AcceptCount", "RefuseCount", "SumTotal");
		return fields.join(',');
	},
	/**修改单个*/
	updateOneInfo: function(index, record) {
		var me = this,
			url = JShell.System.Path.getRootUrl(me.editUrl);
		var entity = me.getOneUpdateInfo(record);
		var params = JShell.JSON.encode({
			entity: entity,
			fields: me.getUpdateFields()
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
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
						JShell.Msg.error(me.saveErrorCount + '条数据发生错误!');
					}
				}
			});
		}, 100 * index);
	},
	setBtnDisabled: function(com, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(com);
			if(btn) btn.setDisabled(disabled);
		}
	},
	/**按钮的启用或或禁用*/
	setButtonsDisabled: function(disabled) {
		var me = this;
		me.setBtnDisabled("btnAddScanCode", disabled);
		me.setBtnDisabled("btnAddManualInput", disabled);
	}
});