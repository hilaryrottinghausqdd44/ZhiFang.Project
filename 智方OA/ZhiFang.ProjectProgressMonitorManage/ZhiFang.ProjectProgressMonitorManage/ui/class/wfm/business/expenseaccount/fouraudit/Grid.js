/**
 * 报销四审列表
 * @author liangyl	
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.expenseaccount.fouraudit.Grid', {
	extend: 'Shell.class.wfm.business.expenseaccount.basic.Grid',
	title: '报销四审列表',
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
				var IsSpecially = record.get('PExpenseAccount_IsSpecially');
				//三审通过
				var isApply = 7;
				if(Status == isApply || (Status == 5 && IsSpecially == 'false')) {
					me.openEditForm(id, IsSpecially);
				} else {
					me.openShowForm(id);
				}
			}
		});
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
		    me.defaultWhere= '((pexpenseaccount.FourReviewManID=' + UserId +') or (pexpenseaccount.FourReviewManID=null and IsSpecially=1 and pexpenseaccount.Status=7) or (pexpenseaccount.FourReviewManID=null and IsSpecially=0 and pexpenseaccount.Status=5)) and (pexpenseaccount.IsUse=1)';
	        me.load(null, true, autoSelect);
    	});
		
	},
	openEditForm: function(id, IsSpecially) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.expenseaccount.fouraudit.EditPanel', {
			PK: id,
			SUB_WIN_NO: '8',
			IsSpecially: IsSpecially,
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
		data.push(['', '=全部=', 'font-weight:bold;color:#303030;text-align:center']);
		for(var i in StatusList) {
			var obj = StatusList[i];
			if(obj.Id != 1 && obj.Id != 2 && obj.Id != 4 && obj.Id != 3 && obj.Id != 6 && obj.Id != 8 && obj.Id != 10) {
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