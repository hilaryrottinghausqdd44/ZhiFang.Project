/**
 * 入库明细
 * @author liangyl
 * @version 2017-12-05
 */
Ext.define('Shell.class.rea.client.stock.inspection.EditPanel', {
	extend: 'Shell.class.rea.client.stock.basic.EditPanel',

	OTYPE: "manualinput",

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//明细列表 的货品明细添加,删除,数量改变后,需要重新计算总价格并联动更新表单总价格及表单供货方编辑状态处理
		me.DtlGrid.on({
			onFullScreenClick: function() {
				me.fireEvent('onFullScreenClick', me);
			},
			nodata: function(p) {

			},
			onAddDt: function() {
				//新增验收货品前先获取主单基本信息
				var docEntity = me.DocForm.getEditParams();
				
				if(docEntity != null && !docEntity.entity.CompanyID) {
			
					JShell.Msg.alert("请选择供应商后再操作!", null, 1000);
					return;
				}
				me.showDtGridCheck(docEntity);
			}
		});
		//供应商选择改变后
		me.DocForm.on({
			reacompcheck: function(v, record) {
				var ReaCompID = record ? record.get('ReaCenOrg_Id') : '';
				me.DtlGrid.ReaCompID = ReaCompID;
			},
			//表单供货方编辑状态处理
			isEditAfter: function(form) {
				var bo = true;
				if(me.DtlGrid.store.getCount() <= 0) bo = false;
				me.setReaCompNameReadOnly(bo);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.stock.inspection.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			collapsible: false,
			PK: me.PK,
			defaultLoad: false,
			collapsed: false,
			formtype: me.formtype,
			OTYPE: me.OTYPE
		});
		me.DocForm = Ext.create('Shell.class.rea.client.stock.inspection.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			width: me.width,
			height: 130,
			split: false,
			collapsible: false,
			collapsed: false,
			PK: me.PK,
			formtype: me.formtype,
			OTYPE: me.OTYPE
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	isEdit: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsInDoc_Id");
		me.PK = id;
		me.formtype = "edit";
		me.DocForm.formtype = "edit";
		me.DocForm.PK = id;
		me.DocForm.Status = record.get("ReaBmsInDoc_Status");
		me.DocForm.isEdit(id);
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "edit";
		me.DtlGrid.Status = record.get("ReaBmsInDoc_Status");
		me.loadDtlGrid(me.PK);
		me.DtlGrid.setButtonsDisabled(true);
	},
	/**主订单联动明细及表单*/
	isShow: function(record, applyGrid) {
		var me = this;
		var id = record.get("ReaBmsInDoc_Id");
		me.PK = id;
		me.formtype = "show";

		me.DocForm.PK = id;
		me.DocForm.formtype = "show";
		var statusName = "",
			color = "#1c8f36";
		if(applyGrid.StatusEnum != null) statusName = applyGrid.StatusEnum[record.get("ReaBmsInDoc_Status")];
		if(applyGrid.StatusBGColorEnum != null) color = applyGrid.StatusBGColorEnum[record.get("ReaBmsInDoc_Status")];
		statusName = '<b style="color:' + color + ';">' + statusName + '</b> ';
		me.DocForm.StatusName = statusName;
		me.DocForm.isShow(id);

		me.DtlGrid.PK = id;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.Status = record.get("ReaBmsInDoc_Status");

		me.loadDtlGrid(me.PK);
		me.DtlGrid.setButtonsDisabled(false);
	},
	/**@description 供货方是可编辑还是只读处理*/
	setReaCompNameReadOnly: function(bo) {
		var me = this;
		var com = me.DocForm.getComponent('ReaBmsInDoc_CompanyName');
		com.setReadOnly(bo);
	},
	/**@description 显示新增货品明细*/
	showDtGridCheck: function(docEntity) {
		var me = this;
		var config = {
			resizable: true,
			ReaCompID: docEntity.entity.CompanyID,
			DocEntity: docEntity, //供货验收单基本信息
			listeners: {
				close: function(p, eOpts) {
					me.fireEvent('save', me, p.IsRefresh);
				},
				save: function(p, isClose) {
					if(isClose) {
						me.fireEvent('save', me, true);
						p.close();
					}
				}
			}
		};
		config.formtype = 'add';
		var win = JShell.Win.open('Shell.class.rea.client.stock.manualinput.DtlForm', config);
		win.show();
	}
});