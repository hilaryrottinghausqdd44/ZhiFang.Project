/**
 * 员工财务账户信息
 * @author longfc
 * @version 2016-11-18
 */
Ext.define('Shell.class.sysbase.user.pempfinanceaccount.show.Form', {
	extend: 'Shell.ux.form.Panel',
	title: '员工财务账户信息',

	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPEmpFinanceAccountByHQL?isPlanish=true',
	bodyPadding: "2px",
	/**布局方式*/
	layout: 'anchor',
	/**每个组件的默认属性*/
	defaults: {
		anchor: '100%',
		labelStyle: 'font-size:15px;font-weight:bold;color:#00F',
		fieldStyle: 'background:none;border:0;border-bottom:0px;font-size:15px;font-weight:bold;color:#00F;',
		labelWidth: 75,
		labelAlign: 'right'
	},
	/**启用表单状态初始化*/
	openFormType: true,
	/**显示成功信息*/
	showSuccessInfo: false,
	/*是否管理员应用**/
	isManage: false,
	/**默认加载数据*/
	defaultLoad: false,
	EmpID:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(me.defaultLoad){
			me.isShow(me.EmpID);
		}
	},
	initComponent: function() {
		var me = this;
		me.buttonToolbarItems = ['->', 'save', 'reset'];
		me.callParent(arguments);
	},
	/**@overwrite 创建内部组件*/
	createItems: function() {
		var me = this,
			items = [];
		items.push({
			fieldLabel: '单笔上限',
			xtype: 'numberfield',
			readOnly: true,
			locked: true,
			minValue: 0,
			allowDecimals: true,
			decimalPrecision: 2,
			name: 'PEmpFinanceAccount_OneceLoanUpperAmount',
			itemId: 'PEmpFinanceAccount_OneceLoanUpperAmount'
		}, {
			fieldLabel: '借款上限',
			xtype: 'numberfield',
			readOnly: true,
			locked: true,
			minValue: 0,
			allowDecimals: true,
			decimalPrecision: 2,
			name: 'PEmpFinanceAccount_LoanUpperAmount',
			itemId: 'PEmpFinanceAccount_LoanUpperAmount'
		}, {
			fieldLabel: '借款总额',
			xtype: 'numberfield',

			readOnly: true,
			locked: true,
			minValue: 0,
			allowDecimals: true,
			decimalPrecision: 2,
			name: 'PEmpFinanceAccount_LoanAmount',
			itemId: 'PEmpFinanceAccount_LoanAmount'
		}, {
			fieldLabel: '待还额度',
			xtype: 'numberfield',
			readOnly: true,
			locked: true,
			minValue: 0,
			allowDecimals: true,
			decimalPrecision: 2,
			name: 'PEmpFinanceAccount_UnRepaymentAmount',
			itemId: 'PEmpFinanceAccount_UnRepaymentAmount'
		}, {
			fieldLabel: '还款总额',
			xtype: 'numberfield',
			readOnly: true,
			locked: true,
			minValue: 0,
			allowDecimals: true,
			decimalPrecision: 2,
			name: 'PEmpFinanceAccount_RepaymentAmount',
			itemId: 'PEmpFinanceAccount_RepaymentAmount'
		});
		return items;
	},
	/**根据主键ID加载数据*/
	load: function(empID) {
		var me = this,
			url = me.selectUrl,
			collapsed = me.getCollapsed();
		me.EmpID=empID;
		if(me.EmpID == null || me.EmpID == "")
		return;
			//me.EmpID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || "";

		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed) {
			me.isCollapsed = true;
			return;
		}

		me.showMask(me.loadingText); //显示遮罩层
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		url += (url.indexOf('?') == -1 ? "?" : "&") + "where=(pempfinanceaccount.IsUse=1 and pempfinanceaccount.EmpID=" + me.EmpID + ")";
		url += '&fields=' + me.getStoreFields().join(',');
		var objValues = {
			PEmpFinanceAccount_OneceLoanUpperAmount: '',
			PEmpFinanceAccount_LoanUpperAmount: '',
			PEmpFinanceAccount_LoanAmount: '',
			PEmpFinanceAccount_UnRepaymentAmount: '',
			PEmpFinanceAccount_RepaymentAmount:''
		};
		JShell.Server.get(url, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				if(data.value) {
					var list = (data.value || {}).list || [];
					if(list && list.length > 0)
						objValues = list[0];
					data.value = JShell.Server.Mapping(objValues);
					me.lastData = me.changeResult(objValues);
					me.getForm().setValues(data.value);
				}
			} else {
				me.getForm().setValues(objValues);
				JShell.Msg.error(data.msg);
			}
			me.fireEvent('load', me, data);
		});
	}
});