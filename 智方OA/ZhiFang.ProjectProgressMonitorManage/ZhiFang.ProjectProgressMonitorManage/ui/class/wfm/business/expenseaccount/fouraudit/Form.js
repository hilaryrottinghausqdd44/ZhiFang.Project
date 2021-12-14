/**
 * 四审
 * @author liangyl	
 * @version 2016-10-10
 */
Ext.define('Shell.class.wfm.business.expenseaccount.fouraudit.Form', {
	extend: 'Shell.class.wfm.business.expenseaccount.basic.Form',
	title: '报销四审',
	/**四审通过状态*/
	OverStatus: 9,
	/**四审退回状态*/
	BackStatus: 10,
	IsSpecially: false,
	width: 500,
	height: 210,
	/**处理意见Id和name*/
	ReviewInfo: 'PExpenseAccounAmount_FourReviewInfo',
	/**布局方式*/
	layout: {
		type: 'table',
		columns: 2 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 70,
		width: 220,
		labelAlign: 'right'
	},
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否重置按钮*/
	hasReset: true,
	/**启用表单状态初始化*/
	openFormType: true,
	PK: '',
	Status: '',
	/**@overwrite 获取列表布局组件内容*/
	getTableLayoutItems: function() {
		var me = this,
			items = [];
		//科目1
		me.PExpenseAccount_OneLevelItemName.colspan = 1;
		me.PExpenseAccount_OneLevelItemName.width = me.defaults.width * me.PExpenseAccount_OneLevelItemName.colspan;
		items.push(me.PExpenseAccount_OneLevelItemName);
		//科目2
		me.PExpenseAccount_TwoLevelItemName.colspan = 1;
		me.PExpenseAccount_TwoLevelItemName.width = me.defaults.width * me.PExpenseAccount_TwoLevelItemName.colspan;
		items.push(me.PExpenseAccount_TwoLevelItemName);
        //处理意见
		me.ReviewInfo.colspan = 2;
		me.ReviewInfo.width = me.defaults.width * me.ReviewInfo.colspan;
		items.push(me.ReviewInfo);

		return items;
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var Status = '';
		if(me.isSubmit) {
			Status = me.OverStatus;
		} else {
			//特殊审批退回三审
			if(me.IsSpecially || me.IsSpecially == 'true') {
				Status = me.BackStatus;
			} else {
				Status = 6;
			}
		}
		var entity = {
			Status: Status
		};
		if(values.PExpenseAccount_OneLevelItemID) {
			entity.OneLevelItemID = values.PExpenseAccount_OneLevelItemID;
			entity.OneLevelItemName = values.PExpenseAccount_OneLevelItemName;
		}
		if(values.PExpenseAccount_TwoLevelItemID) {
			entity.TwoLevelItemID = values.PExpenseAccount_TwoLevelItemID;
			entity.TwoLevelItemName = values.PExpenseAccount_TwoLevelItemName;
		}
		if(values.PExpenseAccounAmount_FourReviewInfo) {
			entity.FourReviewInfo = values.PExpenseAccounAmount_FourReviewInfo.replace(/\\/g, '&#92');
			entity.OperationMemo = values.PExpenseAccounAmount_FourReviewInfo.replace(/\\/g, '&#92');
		}
		if(me.PK) {
			entity.Id = me.PK;
		}
		//登录员工
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		//登录员工名称
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId) {
			entity.FourReviewManID = userId;
		}
		if(userName) {
			entity.FourReviewMan = userName;
		}
		var Sysdate = JcallShell.System.Date.getDate();
		var ReviewDate = JcallShell.Date.toString(Sysdate);
		var ReviewDateStr = JShell.Date.toServerDate(ReviewDate);
		if(ReviewDateStr) {
			entity.FourReviewDate = ReviewDateStr;
		}
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = ['Id,Status,OneLevelItemID,TwoLevelItemID,OneLevelItemName,TwoLevelItemName'];
		entity.fields = fields.join(',');
		return entity;
	}
});