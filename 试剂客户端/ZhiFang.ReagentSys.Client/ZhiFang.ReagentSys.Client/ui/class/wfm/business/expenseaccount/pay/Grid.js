/**
 * 报销单检查并打款
 * @author liangyl
 * @version 2016-10-12
 */
Ext.define('Shell.class.wfm.business.expenseaccount.pay.Grid', {
	extend: 'Shell.class.wfm.business.expenseaccount.basic.Grid',
	title: '报销单检查并打款',
	/**默认员工类型*/
	defaultUserType: '',
	/**默认显示状态*/
	defaultStatusValue: '',
	/**不显示暂存状态数据*/
	defaultWhere: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var id = record.get(me.PKField);
				var Status = record.get('PExpenseAccount_Status');
				var PExpenseAccounAmount = record.get('PExpenseAccount_PExpenseAccounAmount');
				//二审通过
				var isApply = 9;
				if(Status == isApply) {
					me.openEditForm(id,PExpenseAccounAmount);
				} else {
					me.openShowForm(id);
				}
			}
		});
	},
	initComponent: function() {
		var me = this;
		var UserId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || '';
		me.defaultWhere = '(pexpenseaccount.Status =9 and pexpenseaccount.PayManID=null and pexpenseaccount.IsUse=1)' + " or (pexpenseaccount.PayManID=" + UserId + " and  pexpenseaccount.IsUse=1)";
		me.callParent(arguments);
	},
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PExpenseAccountStatus',function(){
			if(!JShell.System.ClassDict.PExpenseAccountStatus){
    			JShell.Msg.error('未获取到报销状态，请刷新列表');
    			return;
    		}
			var StatusID = me.getComponent('buttonsToolbar').getComponent('StatusID');
			var List=JShell.System.ClassDict.PExpenseAccountStatus;
			if(StatusID.store.data.items.length==0){
			     StatusID.loadData(me.getStatusData(List));
			     StatusID.setValue(me.defaultStatusValue);
			}
			var UserId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || '';
			me.defaultWhere = '(pexpenseaccount.Status =9 and pexpenseaccount.PayManID=null and pexpenseaccount.IsUse=1)' + " or (pexpenseaccount.PayManID=" + UserId + " and  pexpenseaccount.IsUse=1)";
	        me.load(null, true, autoSelect);
		});
	},
	openEditForm: function(id,PExpenseAccounAmount) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.expenseaccount.pay.EditPanel', {
			PK: id,
			SUB_WIN_NO: '10',
			PExpenseAccounAmount:PExpenseAccounAmount,
			listeners: {
				save: function(p, id) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**获取状态列表*/
	getStatusData: function(StatusList) {
		var me = this,
			data = [];
		data.push(['', '=全部=', 'font-weight:bold;text-align:center']);
		for(var i in StatusList) {
			var obj = StatusList[i];
			if(obj.Id == 9  || obj.Id == 11) {
				var style = ['font-weight:bold;text-align:center'];
				if(obj.BGColor) {
					style.push('color:' + obj.BGColor);
				}
				data.push([obj.Id, obj.Name, style.join(';')]);
			}
		}
		return data;
	}
});