/**
 * 报销一审列表
 * @author liangyl	
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.expenseaccount.oneaudit.Grid',{
    extend: 'Shell.class.wfm.business.expenseaccount.basic.Grid',
    
    title:'报销一审列表',
    /**默认员工类型*/
	defaultUserType:'',
	/**默认显示状态*/
	defaultStatusValue:'',
		/**不显示暂存状态数据*/
	defaultWhere:'',
	/**员工类型列表*/
	UserTypeList: [
		['', '不过滤'],
		['ApplyManID', '申请人'],
		['TwoReviewManID', '二审人'],
		['ThreeReviewManID', '三审人'],
		['FourReviewManID', '四审人'],
		['PayManID', '打款负责人'],
		['ReceiveManID', '领款人']
	],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick:function(view,record){
				var id = record.get(me.PKField);
				var Status = record.get('PExpenseAccount_Status');
				//申请
				var isApply=2;
				//二审退回
				var isTwoAuditBack =6;
				if(Status==isApply || Status==isTwoAuditBack){
					me.openEditForm(id);
				}else{
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
	        me.defaultWhere='(pexpenseaccount.Status !=1 and pexpenseaccount.ReviewManID='+UserId+' and pexpenseaccount.IsUse=1)';	
	        me.load(null, true, autoSelect);
	   	});
   		
	},
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.expenseaccount.oneaudit.EditPanel', {
			PK:id,
			SUB_WIN_NO: '2',
			listeners: {
				save: function(win,id) {
					win.close();
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
			if(obj.Id != 1){
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