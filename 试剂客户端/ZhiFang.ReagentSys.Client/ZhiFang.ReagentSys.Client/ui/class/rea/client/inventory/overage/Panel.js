/**
 * 盘库管理--盘盈入库
 * @author longfc
 * @version 2019-01-18
 */
Ext.define('Shell.class.rea.client.inventory.overage.Panel', {
	extend: 'Shell.ux.panel.AppPanel',

	title: '盘盈入库',

	/**新增/编辑/查看*/
	formtype: 'show',
	/**盘库单Id*/
	PK: null,
	/**当前盘库单选择行*/
	checkRecord: null,
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/ST_UDTO_AddReaBmsInDocAndDtlOfCheckDocID',
	//按钮是否可点击
	BUTTON_CAN_CLICK:true,
	saveText:"保存中",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.onListeners();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.DtlGrid = Ext.create('Shell.class.rea.client.inventory.overage.DtlGrid', {
			header: false,
			itemId: 'DtlGrid',
			region: 'center',
			defaultLoad: false
		});
		me.DocForm = Ext.create('Shell.class.rea.client.inventory.overage.DocForm', {
			header: false,
			itemId: 'DocForm',
			region: 'north',
			height: 155,
			split: true,
			collapsible: true,
			collapseMode: 'mini'
		});
		var appInfos = [me.DtlGrid, me.DocForm];
		return appInfos;
	},
	onListeners: function() {
		var me = this;
		me.DtlGrid.on({
			onChangeSumTotal: function(grid, totalPrice) {
				me.DocForm.setTotalPrice(totalPrice);
			}
		});
		me.DocForm.on({
			onConfirmClick: function(form, params) {
				me.onConfirm(params);
			}
		});
	},
	nodata: function() {
		var me = this;
		var me = this;
		me.PK = null;
		me.formtype = "show";
		me.checkRecord = null;

		me.DocForm.PK = null;
		me.DocForm.formtype = "show";
		me.DocForm.isShow();
		me.DocForm.getForm().reset();
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = null;
		me.DtlGrid.canEdit = true;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.store.removeAll();
		me.DtlGrid.disableControl();
	},
	clearData: function() {
		var me = this;
		me.nodata();
	},
	isAdd: function(record) {
		var me = this;
		var id = record.get("ReaBmsCheckDoc_Id");
		me.PK = id;
		me.formtype = "add";

		me.DocForm.PK = id;
		me.DocForm.isEdit(id);
		me.DocForm.formtype = "add";

		me.DtlGrid.PK = id;
		me.DtlGrid.canEdit = true;
		me.DtlGrid.formtype = "add";
		me.DtlGrid.onSearch();
	},
	/**主订单联动明细及表单*/
	isShow: function(record) {
		var me = this;
		var id = record.get("ReaBmsCheckDoc_Id");
		me.PK = id;
		me.formtype = "show";

		me.DocForm.PK = id;
		me.DocForm.isShow(id);
		me.DocForm.getComponent('buttonsToolbar').hide();

		me.DtlGrid.PK = id;
		me.DtlGrid.canEdit = false;
		me.DtlGrid.formtype = "show";
		me.DtlGrid.onSearch();
	},
	loadData: function(record) {
		var me = this;
		if(!record) {
			me.clearData();
		} else {
			me.checkRecord = record;
			var checkResult = record.get("ReaBmsCheckDoc_BmsCheckResult");
			//已盘盈,已盘盈已盘亏
			if(checkResult == "2" || checkResult == "4")
				me.isShow(record);
			else
				me.isAdd(record);
		}
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		}
	},
	onConfirm: function() {
		var me = this;
		if(!me.DocForm.getForm().isValid()) return;
		
		if(!me.DocForm.getForm().isValid()) return;
		var docEntity = me.formtype == 'add' ? me.DocForm.getAddParams() : me.DocForm.getEditParams();

		if(!docEntity.entity) {
			JShell.Msg.alert("获取封装验入库信息为空", null, 2000);
			return;
		}
		if(!me.DtlGrid.validatorSave()) return;
		var entity = docEntity.entity;
		//确认入库
		entity.Status = "2";
		var dtlInfo = me.DtlGrid.getSaveDtlAll();
		entity.TotalPrice = dtlInfo.TotalPrice;

		var params = {
			"checkDocID": me.PK,
			"codeScanningMode": "mixing",
			"inDoc": entity,
			"dtAddList": dtlInfo.dtAddList
		};
		if(!params.dtAddList) params.dtAddList = [];

		params = Ext.JSON.encode(params);
		if(!params) {
			JShell.Msg.alert("封装入库信息出错,不能保存!", null, 2000);
			return;
		}

		var url = me.addUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		me.showMask(me.saveText); //显示遮罩层
		me.BUTTON_CAN_CLICK = false; //不可点击
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			me.BUTTON_CAN_CLICK = true;
			if(data.success) {
				me.fireEvent('save', me, me.PK);
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.DocForm.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	}
});