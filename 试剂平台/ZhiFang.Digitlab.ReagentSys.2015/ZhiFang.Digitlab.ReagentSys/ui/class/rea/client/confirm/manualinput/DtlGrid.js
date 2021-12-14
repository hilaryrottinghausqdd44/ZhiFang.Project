/**
 * 客户端手工录入验收
 * @author longfc
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.confirm.manualinput.DtlGrid', {
	extend: 'Shell.class.rea.client.confirm.basic.DtlGrid',
	title: '验货单明细列表',

	OTYPE: "manualinput",
	/**是否可编辑*/
	canEdit: false,

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
			hidden:true,
			hideable: false,
			sortable: false,
			items: [{
				getClass: function(v, meta, record) {
					var status = record.get("BmsCenSaleDtlConfirm_Status");
					var inCount = record.get("BmsCenSaleDtlConfirm_InCount");
					if(!inCount) inCount = 0;
					if(status == "0") {						
						meta.tdAttr = 'data-qtip="<b>删除</b>"';
						return 'button-del hand';
					} else {
						return '';
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
		
		var tempStatus = me.StatusList;
		items.push({
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
	/**按钮的启用或或禁用*/
	setButtonsDisabled: function(disabled) {
		var me = this;
		me.setBtnDisabled("btnAddScanCode", disabled);
		me.setBtnDisabled("btnAddManualInput", disabled);
	}
});