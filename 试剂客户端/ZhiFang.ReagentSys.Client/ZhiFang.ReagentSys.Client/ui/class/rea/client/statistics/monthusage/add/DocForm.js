/**
 * 出库使用量统计
 * @author longfc
 * @version 2018-10-24
 */
Ext.define('Shell.class.rea.client.statistics.monthusage.add.DocForm', {
	extend: 'Shell.class.rea.client.statistics.monthusage.basic.DocForm',

	title: '出库使用量统计',
	formtype: 'show',
	width: 680,
	height: 155,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaMonthUsageStatisticsDocById?isPlanish=true',
	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/RS_UDTO_AddReaMonthUsageStatisticsDoc',
	/**修改服务地址*/
	editUrl: '',
	buttonDock: "top",
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	hasPrint:false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,

	/**月结最小年份*/
	minYearValue: 2018,
	/**月结最小年份*/
	minYearValue: 2018,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if(items.length == 0) {
			if(me.hasSave) items.push("save");
			if(me.hasReset) items.push('reset');
		}
		if(items.length == 0) return null;

		return Ext.create('Shell.ux.toolbar.Button', {
			dock: me.buttonDock,
			itemId: 'buttonsToolbar',
			items: items,
			hidden: true
		});
	},
	/****@description清空库房货架选择*/
	clearDeptName: function() {
		var me = this;
		var DeptName = me.getComponent('ReaMonthUsageStatisticsDoc_DeptName');
		var DeptID = me.getComponent('ReaMonthUsageStatisticsDoc_DeptID');
		DeptID.setValue("");
		DeptName.setValue("");
	},
	/**@description 打印月结单*/
	onPrintClick: function() {
		var me = this;
		if(!me.PK) {
			JShell.Msg.error("请先保存打印月结单后,再打印月结单!");
			return;
		}
		var url = JShell.System.Path.getRootUrl("/ReaManageService.svc/RS_UDTO_GetQtyMonthBalanceAndDtlOfPdf");
		url += '?operateType=1&id=' + me.PK;
		window.open(url);
	},
	/**保存按钮点击处理方法*/
	onSaveClick: function() {
		var me = this;
		var values = me.getForm().getValues();
		if(!me.getForm().isValid()) return;

		var params = me.formtype == 'add' ? me.getAddParams() : me.getEditParams();
		if(!params) {
			JShell.Msg.error("封装提交信息错!");
			return;
		}
		var labCName=JcallShell.REA.System.CENORG_NAME;
		if(!labCName)labCName="";
		params.labCName=labCName;
		params = JcallShell.JSON.encode(params);

		me.showMask(me.saveText); //显示遮罩层
		var url = me.addUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				id = me.formtype == 'add' ? data.value.id : id;
				id += '';
				me.fireEvent('save', me, id);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();

		var entity = {
			Id: -1,
			DocNo: values.ReaMonthUsageStatisticsDoc_DocNo,
			Round: values.ReaMonthUsageStatisticsDoc_Round,
			CreaterName: values.ReaMonthUsageStatisticsDoc_CreaterName,
			DeptName: values.ReaMonthUsageStatisticsDoc_DeptName
		};
		entity.Memo = values.ReaMonthUsageStatisticsDoc_Memo.replace(/\\/g, '&#92');
		entity.Memo = entity.Memo.replace(/[\r\n]/g, '<br />');

		if(values.ReaMonthUsageStatisticsDoc_TypeID) entity.TypeID = values.ReaMonthUsageStatisticsDoc_TypeID;
		if(values.ReaMonthUsageStatisticsDoc_RoundTypeId) entity.RoundTypeId = values.ReaMonthUsageStatisticsDoc_RoundTypeId;
		if(values.ReaMonthUsageStatisticsDoc_DeptID) entity.DeptID = values.ReaMonthUsageStatisticsDoc_DeptID;

		if(values.ReaMonthUsageStatisticsDoc_CreaterID) entity.CreaterID = values.ReaMonthUsageStatisticsDoc_CreaterID;
		if(values.ReaMonthUsageStatisticsDoc_RoundTypeId) entity.RoundTypeId = values.ReaMonthUsageStatisticsDoc_RoundTypeId;
		
		if(values.ReaMonthUsageStatisticsDoc_StartDate) entity.StartDate = JShell.Date.toServerDate(values.ReaMonthUsageStatisticsDoc_StartDate);
		if(values.ReaMonthUsageStatisticsDoc_EndDate) entity.EndDate = JShell.Date.toServerDate(values.ReaMonthUsageStatisticsDoc_EndDate);

		return {
			entity: entity
		};
	},
	isAdd: function() {
		var me = this;
		me.callParent(arguments);
		var TypeID = me.getComponent('ReaMonthUsageStatisticsDoc_TypeID');
		//按使用量
		TypeID.setValue("1");
		
		var RoundTypeId = me.getComponent('ReaMonthUsageStatisticsDoc_RoundTypeId');
		//按自然月
		RoundTypeId.setValue("1");
		
		var sysdate = JcallShell.System.Date.getDate();	
		var roundValue = Ext.util.Format.date(sysdate, "Y-m")
		var round = me.getComponent('ReaMonthUsageStatisticsDoc_Round');		
		//round.setValue(roundValue);
		
		me.onTypeChange("1");
		me.onRoundTypeChange("1");
	}
});