/**
 * 报销表单
 * @author liangyl	
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.expenseaccount.apply.Form', {
	extend: 'Shell.class.wfm.business.expenseaccount.basic.Form',
	title: '报销表单',
	formtype: "add",
	layout: {
		type: 'table',
		columns: 3 //每行有几列
	},
	/**每个组件的默认属性*/
	defaults: {
		labelWidth: 80,
		width: 220,
		labelAlign: 'right'
	},
	//报销单状态
	Status: 1,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.changeAmount();
	},
	//自动计算报销金额
	changeAmount: function() {
		var me = this;
		//报销金额
		var PExpenseAccounAmount = me.getComponent('PExpenseAccount_PExpenseAccounAmount');
		//出差天数
		var DayCount = me.getComponent('PExpenseAccount_DayCount');
		//交通费
		var Transport = me.getComponent('PExpenseAccount_Transport');
		//市内车费
		var CityTransport = me.getComponent('PExpenseAccount_CityTransport');
		//住宿费
		var HotelRates = me.getComponent('PExpenseAccount_HotelRates');
		//餐费补贴
		var Meals = me.getComponent('PExpenseAccount_Meals');
		//招待费
		var EntertainsCosts = me.getComponent('PExpenseAccount_EntertainsCosts');
		//通讯费
		var CommunicationCosts = me.getComponent('PExpenseAccount_CommunicationCosts');
		//其它费用
		var OtherCosts = me.getComponent('PExpenseAccount_OtherCosts');
		Transport.on({
			blur: function(com, The, eOpts) {
				var count = me.getPExpenseAccounAmount(Transport, CityTransport, HotelRates, Meals, EntertainsCosts, CommunicationCosts, OtherCosts);
				PExpenseAccounAmount.setValue(count);
			}
		});
		CityTransport.on({
			blur: function(com, The, eOpts) {
				var count = me.getPExpenseAccounAmount(Transport, CityTransport, HotelRates, Meals, EntertainsCosts, CommunicationCosts, OtherCosts);
				PExpenseAccounAmount.setValue(count);
			}
		});
		HotelRates.on({
			blur: function(com, The, eOpts) {
				var count = me.getPExpenseAccounAmount(Transport, CityTransport, HotelRates, Meals, EntertainsCosts, CommunicationCosts, OtherCosts);
				PExpenseAccounAmount.setValue(count);
			}
		});
		Meals.on({
			blur: function(com, The, eOpts) {
				var count = me.getPExpenseAccounAmount(Transport, CityTransport, HotelRates, Meals, EntertainsCosts, CommunicationCosts, OtherCosts);
				PExpenseAccounAmount.setValue(count);
			}
		});
		EntertainsCosts.on({
			blur: function(com, The, eOpts) {
				var count = me.getPExpenseAccounAmount(Transport, CityTransport, HotelRates, Meals, EntertainsCosts, CommunicationCosts, OtherCosts);
				PExpenseAccounAmount.setValue(count);
			}
		});
		CommunicationCosts.on({
			blur: function(com, The, eOpts) {
				var count = me.getPExpenseAccounAmount(Transport, CityTransport, HotelRates, Meals, EntertainsCosts, CommunicationCosts, OtherCosts);
				PExpenseAccounAmount.setValue(count);
			}
		});
		OtherCosts.on({
			blur: function(com, The, eOpts) {
				var count = me.getPExpenseAccounAmount(Transport, CityTransport, HotelRates, Meals, EntertainsCosts, CommunicationCosts, OtherCosts);
				PExpenseAccounAmount.setValue(count);
			}
		});
	},

	/**合计报销金额*/
	getPExpenseAccounAmount: function(Transport, CityTransport, HotelRates, Meals, EntertainsCosts, CommunicationCosts, OtherCosts) {
		var me = this;
		var Count = 0;
		//交通费
		var TransportValue = parseInt(Transport.getValue());
		//市内车费
		var CityTransportValue = parseInt(CityTransport.getValue());
		//住宿费
		var HotelRatesValue = parseInt(HotelRates.getValue());
		//餐费补贴
		var MealsValue = parseInt(Meals.getValue());
		//招待费
		var EntertainsCostsValue = parseInt(EntertainsCosts.getValue());
		//通讯费
		var CommunicationCostsValue = parseInt(CommunicationCosts.getValue());
		//其他费
		var OtherCostsValue = parseInt(OtherCosts.getValue());
		Count = 0;
		if(TransportValue) {
			Count = Count + TransportValue;
		}
		if(CityTransportValue) {
			Count = Count + CityTransportValue;
		}
		if(HotelRatesValue) {
			Count = Count + HotelRatesValue;
		}
		if(MealsValue) {
			Count = Count + MealsValue;
		}
		if(EntertainsCostsValue) {
			Count = Count + EntertainsCostsValue;
		}
		if(CommunicationCostsValue) {
			Count = Count + CommunicationCostsValue;
		}
		if(OtherCostsValue) {
			Count = Count + OtherCostsValue;
		}
		return Count;
	},
	/**@overwrite 获取列表布局组件内容*/
	getTableLayoutItems: function() {
		var me = this,
			items = [];
		//所属公司
		me.PExpenseAccount_ComponeName.colspan = 1;
		me.PExpenseAccount_ComponeName.width = me.defaults.width * me.PExpenseAccount_ComponeName.colspan;
		items.push(me.PExpenseAccount_ComponeName);
		//		//项目类别
		//		me.PExpenseAccount_ProjectTypeName.colspan = 1;
		//		me.PExpenseAccount_ProjectTypeName.width = me.defaults.width * me.PExpenseAccount_ProjectTypeName.colspan;
		//		items.push(me.PExpenseAccount_ProjectTypeName);
		//核算年
		me.PExpenseAccount_AccountingDate.colspan = 1;
		me.PExpenseAccount_AccountingDate.width = me.defaults.width * me.PExpenseAccount_AccountingDate.colspan;
		items.push(me.PExpenseAccount_AccountingDate);

		//出差天数
		me.PExpenseAccount_DayCount.colspan = 1;
		me.PExpenseAccount_DayCount.width = me.defaults.width * me.PExpenseAccount_DayCount.colspan;
		items.push(me.PExpenseAccount_DayCount);

		//所属部门
		me.PExpenseAccount_DeptName.colspan = 1;
		me.PExpenseAccount_DeptName.width = me.defaults.width * me.PExpenseAccount_DeptName.colspan;
		items.push(me.PExpenseAccount_DeptName);
		//交通费
		me.PExpenseAccount_Transport.colspan = 1;
		me.PExpenseAccount_Transport.width = me.defaults.width * me.PExpenseAccount_Transport.colspan;
		items.push(me.PExpenseAccount_Transport);

		me.PExpenseAccount_Meals.colspan = 1;
		me.PExpenseAccount_Meals.width = me.defaults.width * me.PExpenseAccount_Meals.colspan;
		items.push(me.PExpenseAccount_Meals);
		//核算单位
		me.PExpenseAccount_AccountingDeptName.colspan = 1;
		me.PExpenseAccount_AccountingDeptName.width = me.defaults.width * me.PExpenseAccount_AccountingDeptName.colspan;
		items.push(me.PExpenseAccount_AccountingDeptName);

		//市内交通费
		me.PExpenseAccount_CityTransport.colspan = 1;
		me.PExpenseAccount_CityTransport.width = me.defaults.width * me.PExpenseAccount_CityTransport.colspan;
		items.push(me.PExpenseAccount_CityTransport);
		//通讯费
		me.PExpenseAccount_CommunicationCosts.colspan = 1;
		me.PExpenseAccount_CommunicationCosts.width = me.defaults.width * me.PExpenseAccount_CommunicationCosts.colspan;
		items.push(me.PExpenseAccount_CommunicationCosts);

		//项目类别
		me.PExpenseAccount_ProjectTypeName.colspan = 1;
		me.PExpenseAccount_ProjectTypeName.width = me.defaults.width * me.PExpenseAccount_ProjectTypeName.colspan;
		items.push(me.PExpenseAccount_ProjectTypeName);
		// 招待费
		me.PExpenseAccount_EntertainsCosts.colspan = 1;
		me.PExpenseAccount_EntertainsCosts.width = me.defaults.width * me.PExpenseAccount_EntertainsCosts.colspan;
		items.push(me.PExpenseAccount_EntertainsCosts);
		//其他费用
		me.PExpenseAccount_OtherCosts.colspan = 1;
		me.PExpenseAccount_OtherCosts.width = me.defaults.width * me.PExpenseAccount_OtherCosts.colspan;
		items.push(me.PExpenseAccount_OtherCosts);

		//项目名称
		me.PExpenseAccount_ClientName.colspan = 1;
		me.PExpenseAccount_ClientName.width = me.defaults.width * me.PExpenseAccount_ClientName.colspan;
		items.push(me.PExpenseAccount_ClientName);
		//住宿费
		me.PExpenseAccount_HotelRates.colspan = 1;
		me.PExpenseAccount_HotelRates.width = me.defaults.width * me.PExpenseAccount_HotelRates.colspan;
		items.push(me.PExpenseAccount_HotelRates);

		//报销金额
		me.PExpenseAccount_PExpenseAccounAmount.colspan = 2;
		me.PExpenseAccount_PExpenseAccounAmount.width = me.defaults.width * 1;
		me.PExpenseAccount_PExpenseAccounAmount.readOnly=true;
		me.PExpenseAccount_PExpenseAccounAmount.locked=true;
		items.push(me.PExpenseAccount_PExpenseAccounAmount);

		//报销单说明
		me.PExpenseAccount_PExpenseAccounMemo.colspan = 3;
		me.PExpenseAccount_PExpenseAccounMemo.width = me.defaults.width * me.PExpenseAccount_PExpenseAccounMemo.colspan;
		items.push(me.PExpenseAccount_PExpenseAccounMemo);
		return items;
	},
	/**保存按钮点击处理方法*/
	onSave: function(isSubmit) {
		var me = this,
			values = me.getForm().getValues();
		if(!me.getForm().isValid()) {
			me.fireEvent('isValid', me);
			return;
		}
		if(isSubmit) { //提交
			//申请状态
			me.Status = 2;
			me.onSaveClick();
		} else { //暂存
			//暂存状态
			me.Status = 1;
			me.onSaveClick();
		}
	},
	/**@overwrite 获取新增的数据*/
	getAddParams: function() {
		var me = this,
			values = me.getForm().getValues();
		var entity = {
			IsUse: true, //是否使用
			PExpenseAccounAmount: values.PExpenseAccount_PExpenseAccounAmount,
			DayCount: values.PExpenseAccount_DayCount,
			Transport: values.PExpenseAccount_Transport,
			CityTransport: values.PExpenseAccount_CityTransport,
			HotelRates: values.PExpenseAccount_HotelRates,
			Meals: values.PExpenseAccount_Meals,
			EntertainsCosts: values.PExpenseAccount_EntertainsCosts,
			CommunicationCosts: values.PExpenseAccount_CommunicationCosts,
			OtherCosts: values.PExpenseAccount_OtherCosts
				//			PExpenseAccountNo: values.PExpenseAccount_PExpenseAccountNo
		};
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
		//默认员工ID
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		//默认员工名称
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		//申请人
		if(userId) {
			entity.ApplyManID = userId;
			entity.ApplyMan = userName;
		}
		//申请时间
		var Sysdate = JcallShell.System.Date.getDate();
		var ApplyDate = JcallShell.Date.toString(Sysdate);
		if(ApplyDate) {
			entity.ApplyDate = JShell.Date.toServerDate(ApplyDate);
		}
		//状态
		entity.Status = me.Status;
		//报销单说明
		if(values.PExpenseAccount_PExpenseAccounMemo) {
			entity.PExpenseAccounMemo = values.PExpenseAccount_PExpenseAccounMemo.replace(/\\/g, '&#92');
		}
		//		entity.OperationMemo = '报销申请';
		return {
			entity: entity
		};
	},
	/**@overwrite 获取修改的数据*/
	getEditParams: function() {
		var me = this,
			values = me.getForm().getValues(),
			entity = me.getAddParams();
		var fields = ['Id,ComponeID,ProjectTypeID,ClientID,DeptID,AccountingDeptID,ComponeName,AccountingDate,DayCount,DeptName,Transport,Meals,AccountingDeptName,CityTransport,CommunicationCosts,ProjectTypeName,EntertainsCosts,OtherCosts,ClientName,HotelRates,PExpenseAccounAmount,PExpenseAccounMemo,Status'];		entity.fields = fields.join(',');
		entity.entity.Id = values.PExpenseAccount_Id;
		return entity;
	},
	/**@overwrite 重置按钮点击处理方法*/
	onResetClick: function() {
		var me = this;
		if(!me.PK) {
			this.getForm().reset();
			var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID);
			var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTNAME);
			var DeptName = me.getComponent('PExpenseAccount_DeptName');
			var DeptID = me.getComponent('PExpenseAccount_DeptID');
			if(DeptName) {
				DeptName.setValue(userName);
				DeptID.setValue(userId);
			}
			var AccountingDeptName = me.getComponent('PExpenseAccount_AccountingDeptName');
			var AccountingDeptID = me.getComponent('PExpenseAccount_AccountingDeptID');
			if(AccountingDeptName) {
				AccountingDeptName.setValue(userName);
				AccountingDeptID.setValue(userId);
			}
		} else {
			me.getForm().setValues(me.lastData);
		}
	}
});