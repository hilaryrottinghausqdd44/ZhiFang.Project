/**
 * 验货单明细列表
 * @author liangyL
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.stock.inspection.DtlGrid', {
	extend: 'Shell.class.rea.client.stock.basic.DtlGrid',
	title: '验货单明细列表',

	OTYPE: "manualinput",
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

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtontoolbar: function() {
		var me = this;
		var items = ['refresh','-',{
			name: 'btnAddScanCode',itemId: 'btnAddScanCode',
			emptyText: '条码号扫码',labelSeparator:'',hidden:true,
			labelWidth:0,width: 135,hidden:true,labelAlign: 'right',
		    xtype:'textfield'
	    },{
			xtype: 'button',
			iconCls: 'button-add',
			itemId: "btnAdd",
			text: '新增明细',hidden:false,
			tooltip: '新增明细',
			handler: function() {
				
				me.showForm(null);
			}
		},{
			xtype: 'button',
			iconCls: 'button-edit',
			itemId: "btnEdit",
			text: '修改明细',hidden:false,
			tooltip: '修改明细',
			handler: function() {
				var records = me.getSelectionModel().getSelection();
				if (records.length != 1) {
					JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
					return;
				}
				var id=records[0].get(me.PKField);
				me.showForm(id);
			}
		},{
			xtype: 'button',
			iconCls: 'button-del',
			itemId: "btnDel",
			text: '删除明细',hidden:false,
			tooltip: '删除明细',
			handler: function() {
			
			}
		},{
			xtype: 'button',
			iconCls: 'button-print',
			itemId: "btnSingleCode",
			text: '打印条码',hidden:true,
			tooltip: '打印条码',
			handler: function() {
			
			}
		}];
		//查询框信息
		me.searchInfo = {
			width: 200,
			emptyText: '产品名称/产品批号',
			itemId: 'search',
			isLike: true,
			fields: ['bmscensaledtlconfirm.ReaGoodsName', 'bmscensaledtlconfirm.LotNo']
		};
//		items.push( {
//			type: 'search',
//			info: me.searchInfo
//		});
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
		if(search) {
			var value = search.getValue();
			if(value) {
				where.push(me.getSearchWhere(value));
			}
		}
		return where.join(" and ");
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
			RegisterNo: record.get("BmsCenSaleDtlConfirm_RegisterNo")
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
		//fields.push("GoodsQty", "Price", "AcceptCount", "RefuseCounted", "SumTotal");
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
	},
	/**显示表单*/
	showForm: function(id) {
		var me = this,
			config = {
				resizable: false,
				listeners: {
					load:function(){
						var edit = me.getPlugin('NewsGridEditing'); 
		                edit.cancelEdit();
					},
					close:function(){
						me.onSearch();
					}
				}
			};

		if (id) {
			config.formtype = 'edit';

		} else {
			config.formtype = 'add';
		}
		JShell.Win.open('Shell.class.rea.client.stock.inspection.DtlForm', config).show();
	}
});