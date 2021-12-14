/**
 * 报销一审信息
 * @author liangyl
 * @version 2015-07-27
 */
Ext.define('Shell.class.wfm.business.expenseaccount.oneaudit.EditPanel', {
	extend: 'Shell.class.wfm.business.expenseaccount.basic.EditTabPanel',
	title: '报销审核',
	FormClassName: 'Shell.class.wfm.business.expenseaccount.basic.ContentPanel',
	width: 750,
	height: 420,
	/**ID*/
	PK: null,
	/**一审通过状态*/
	OverStatus: 3,
	/**一审退回状态*/
	BackStatus: 4,
	/**通过文字*/
	OverName: '审核通过',
	/**退回文字*/
	BackName: '退回',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		me.callParent(arguments);
	},
	/**保存按钮点击处理方法*/
	onSave: function(isSubmit, id) {
		var me = this;
		//处理意见
		JShell.Msg.confirm({
			title: '<div style="text-align:center;">处理意见</div>',
			msg: '',
			closable: false,
			multiline: true //多行输入框
		}, function(but, text) {
			if(but != "ok") return;
			me.onSaveClick(isSubmit, id, text);
		});
	},
	onSaveClick: function(isSubmit, id, text, p) {
		var me = this,
			Status = me.OverStatus;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var Sysdate = JcallShell.System.Date.getDate();
		var ReviewDate = JcallShell.Date.toString(Sysdate);
		//一审通过
		if(isSubmit) {
			Status = me.OverStatus;
		} else {
			Status = me.BackStatus;
		}
		var entity = {
			Status: Status,
			OperationMemo: text,
			ReviewInfo: text,
			Id: id
		};		
		var ReviewDateStr = JShell.Date.toServerDate(ReviewDate);
		if(ReviewDateStr) {
			entity.ReviewDate = ReviewDateStr;
		}
		var values = me.Form.getForm().getValues();
		if(values){
			entity.PExpenseAccounAmount=values.PExpenseAccount_PExpenseAccounAmount;
			entity.DayCount=values.PExpenseAccount_DayCount;
			entity.Transport=values.PExpenseAccount_Transport;
			entity.CityTransport=values.PExpenseAccount_CityTransport;
			entity.HotelRates=values.PExpenseAccount_HotelRates;
			entity.Meals=values.PExpenseAccount_Meals;
			entity.EntertainsCosts=values.PExpenseAccount_EntertainsCosts;
			entity.CommunicationCosts=values.PExpenseAccount_CommunicationCosts;
			entity.OtherCosts=values.PExpenseAccount_OtherCosts;
			//公司
			if(values.PExpenseAccount_ComponeName) {
				entity.ComponeID = values.PExpenseAccount_ComponeID;
				entity.ComponeName = values.PExpenseAccount_ComponeName;
			}
			//部门
			if(values.PExpenseAccount_DeptName) {
				entity.DeptID = values.PExpenseAccount_DeptID;
				entity.DeptName = values.PExpenseAccount_DeptName;
			}
			//核算单位
			if(values.PExpenseAccount_AccountingDeptName) {
				entity.AccountingDeptID = values.PExpenseAccount_AccountingDeptID
				entity.AccountingDeptName = values.PExpenseAccount_AccountingDeptName;
			}
			//一级科目
			if(values.PExpenseAccount_OneLevelItemName) {
				entity.OneLevelItemID = values.PExpenseAccount_OneLevelItemID;
				entity.OneLevelItemName = values.PExpenseAccount_OneLevelItemName;
			}
			//二级科目
			if(values.PExpenseAccount_TwoLevelItemName) {
				entity.TwoLevelItemID = values.PExpenseAccount_TwoLevelItemID;
				entity.TwoLevelItemName = values.PExpenseAccount_TwoLevelItemName;
			}
			//项目名称
			if(values.PExpenseAccount_ClientName) {
				entity.ClientID = values.PExpenseAccount_ClientID;
				entity.ClientName = values.PExpenseAccount_ClientName;
			}
			//项目类别
			if(values.PExpenseAccount_ProjectTypeName) {
				entity.ProjectTypeID = values.PExpenseAccount_ProjectTypeID;
				entity.ProjectTypeName = values.PExpenseAccount_ProjectTypeName;
			}
			//核算年份
			if(values.PExpenseAccount_AccountingDate) {
				entity.AccountingDate = values.PExpenseAccount_AccountingDate;
			}
			//报销单说明
			if(values.PExpenseAccount_PExpenseAccounMemo) {
				entity.PExpenseAccounMemo = values.PExpenseAccount_PExpenseAccounMemo.replace(/\\/g, '&#92');
			}			
		}
				
		var fields = ['Id','Status','PExpenseAccounAmount','DayCount','Transport',
			'CityTransport','HotelRates','Meals','EntertainsCosts','CommunicationCosts',
			'OtherCosts','ComponeID','ComponeName','DeptID','DeptName','AccountingDeptID',
			'AccountingDeptName','OneLevelItemID','OneLevelItemName','TwoLevelItemID',
			'TwoLevelItemName','ClientID','ClientName','ProjectTypeID','ProjectTypeName',
			'AccountingDate','PExpenseAccounMemo'];
		fields = fields.join(',');
//		if(values.PInvoice_Id != '') {
//			entity.entity.Id = values.PInvoice_Id;
//		}
		var params = {
			entity: entity,
			fields: fields
		};
		if(!params) return;
		params = Ext.JSON.encode(params);
		JShell.Server.post(url, params, function(data) {
			if(data.success) {
				me.fireEvent('save', me);
				if(me.showSuccessInfo) JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, me.hideTimes);
			} else {
				var msg = data.msg;
				if(msg == JShell.Server.Status.ERROR_UNIQUE_KEY) {
					msg = '有重复';
				}
				JShell.Msg.error(msg);
			}
		}, false);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this;
		var items = me.callParent(arguments);
		me.Form = Ext.create('Shell.class.wfm.business.expenseaccount.apply.Form', {
			title: '报销信息',
			formtype:'edit',
			PK: me.PK,
			itemId: 'Form',
			border: true
		});
		items.splice(0, 0,me.Form);
		return items;
	}
});