/**
 * 医生奖金结算
 * @author longfc
 * @version 2017-02-27
 */
Ext.define('Shell.class.weixin.ordersys.settlement.doctorbonus.basic.App', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '医生奖金结算',
	GridCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.Grid',
	BonusGridCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.BonusGrid',
	TabPanelCalss: 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.TabPanel',
	header: false,

	/**医生奖金结算申请信息*/
	applyInfo: null,
	Status: null,
	OperationMemo: null,
	/**审核通过不通过服务地址*/
	editUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonusFormAndDetails',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.Grid.on({
			itemclick: function(v, record) {
				me.loadDataOfTabPanel(record);
			},
			select: function(RowModel, record) {
				me.loadDataOfTabPanel(record);
			},
			nodata: function(p) {
				me.TabPanel.clearData();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.GridCalss = me.GridCalss || 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.Grid';
		me.BonusGridCalss = me.BonusGridCalss || 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.BonusGrid';
		me.TabPanelCalss = me.TabPanelCalss || 'Shell.class.weixin.ordersys.settlement.doctorbonus.basic.TabPanel';
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createItems: function() {
		var me = this;
		me.Grid = Ext.create(me.GridCalss, {
			region: 'west',
			width: 425,
			header: true,
			split: true,
			collapsible: false,
			itemId: 'Grid',
			title: '医生奖金结算单'
		});
		me.TabPanel = Ext.create(me.TabPanelCalss, {
			region: 'center',
			title: '医生奖金结算记录信息',
			header: false,
			split: true,
			collapsible: true,
			BonusGridCalss: me.BonusGridCalss,
			itemId: 'TabPanel'
		});
		return [me.Grid, me.TabPanel];
	},
	/*左列表的事件监听联动右区域**/
	loadDataOfTabPanel: function(record) {
		var me = this;
		JShell.Action.delay(function() {
			var id = "";
			if(record != null) {
				id = record.get('Id');
				me.TabPanel.PK = id;
				me.TabPanel.loadTabPanel();
			}
		}, null, 500);
	},
	checkSave: function() {
		var me = this;
		var isSave = true;
		var msg = "";
		if(me.applyInfo == null) {
			isSave = false;
			msg = msg + "结算数据为空!<br />";
		}
		if(isSave == true && me.applyInfo.OSDoctorBonusForm == null) {
			isSave = false;
			msg = msg + "结算数据为空!<br />";
		}
		var count1 = me.TabPanel.BonusGrid.store.getCount();
		if(isSave == true && count1 < 1) {
			isSave = false;
			msg = msg + "结算数据的医生奖金记录为空!<br />";
		}
		if(isSave == false) {
			JShell.Msg.alert(msg, null, 2000);
		}
		return isSave;
	},
	getSaveParams: function(status, operationMemo) {
		var me = this;
		var bonusList = [];
		me.TabPanel.BonusGrid.store.each(function(record) {
			record = me.dealWithData(record);
			if(me.Status != null) record.data.Status = me.Status;
			bonusList.push(record.data);
		});
		me.applyInfo.OSDoctorBonusForm.Id = me.PK;
		me.applyInfo.OSDoctorBonusForm.Status = me.Status;
		me.applyInfo.OperationMemo = me.OperationMemo;
		me.applyInfo.OSDoctorBonusList = bonusList;
	},
	dealWithData: function(record) {
		var me = this;
		if(record.data.OSDoctorBonusFormID == "") record.data.OSDoctorBonusFormID = null;
		if(record.data.DoctorAccountID == "") record.data.DoctorAccountID = null;
		if(record.data.WeiXinUserID == "") record.data.WeiXinUserID = null;
		if(record.data.BankAccount == "") record.data.BankAccount = null;
		if(record.data.BankID == "") record.data.BankID = null;
		if(record.data.PaymentMethod == "") record.data.PaymentMethod = null;
		return record;
	},
	setapplyInfo: function(record) {
		var me = this;
		var entity = record.data;
		me.applyInfo = {
			IsSettlement: true,
			BonusFormRound: record.get('BonusFormRound'),
			OSDoctorBonusForm: entity,
			OperationMemo: '',
			OSDoctorBonusList: null
		};
		delete me.applyInfo.OSDoctorBonusForm.DataTimeStamp;
		delete me.applyInfo.OSDoctorBonusForm.DataAddTime;
		delete me.applyInfo.OSDoctorBonusForm.DataUpdateTime;
		delete me.applyInfo.OSDoctorBonusForm.DispOrder;
		delete me.applyInfo.OSDoctorBonusForm.StatusName;
		delete me.applyInfo.OSDoctorBonusForm.BonusApplytTime;

		delete me.applyInfo.OSDoctorBonusForm.BonusOneReviewFinishTime;
		delete me.applyInfo.OSDoctorBonusForm.BonusOneReviewStartTime;
		delete me.applyInfo.OSDoctorBonusForm.BonusOneReviewManID;

		delete me.applyInfo.OSDoctorBonusForm.BonusTwoReviewFinishTime;
		delete me.applyInfo.OSDoctorBonusForm.BonusTwoReviewStartTime;
		delete me.applyInfo.OSDoctorBonusForm.BonusTwoReviewManID;

		delete me.applyInfo.OSDoctorBonusForm.BonusThreeReviewFinishTime;
		delete me.applyInfo.OSDoctorBonusForm.BonusThreeReviewStartTime;
		delete me.applyInfo.OSDoctorBonusForm.BonusThreeReviewManID;

	},
	/**审核通过不通过的公共处理*/
	updateStatus: function(record) {
		var me = this;
		me.setapplyInfo(record);
		me.PK = record.get(me.Grid.PKField);
		var isSave = me.checkSave();
		if(isSave == true) {
			me.getSaveParams();
			var params = {
				entity: me.applyInfo,
				fields: "Id,Status"
			};
			params = Ext.JSON.encode(params);
			me.showMask("数据提交保存中...");
			var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
			JShell.Server.post(url, params, function(data) {
				me.hideMask(); //隐藏遮罩层
				if(data.success) {
					JShell.Msg.alert(me.OperationMemo + "操作成功", null, 1000);
					me.Grid.onSearch();
				} else {
					JShell.Msg.error(me.OperationMemo + "操作失败!<br />" + data.msg);
				}
			});
		}
	}
});