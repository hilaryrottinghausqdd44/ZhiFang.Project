/**
 * 还款审核
 * @author liangyl	
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.business.prepayment.oneaudit.Grid', {
	extend: 'Shell.class.wfm.business.prepayment.basic.Grid',
	title: '还款审核',
	/**默认员工类型*/
	defaultUserType: 'ApplyManID',
	/**默认显示状态*/
	defaultStatusValue: '',
	defaultWhere: '(prepayment.Status=2 or prepayment.Status=4)',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.on({
			itemdblclick: function(view, record) {
				var id = record.get(me.PKField);
				var Status = record.get('PRepayment_Status');
				if(Status == 2) {
					me.openEditForm(id);
				} else {
					me.openShowForm(id);
				}
			}
		});
	},
		/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PRepaymentStatus',function(){
			if(!JShell.System.ClassDict.PRepaymentStatus){
    			JShell.Msg.error('未获取到还款状态，请刷新列表');
    			return;
    		}
          
    	});
    	me.defaultWhere = 'prepayment.Status=4';
		me.load(null, true, autoSelect);
	},
	openEditForm: function(id) {
		var me = this;
		JShell.Win.open('Shell.class.wfm.business.prepayment.oneaudit.EditPanel', {
			PK: id,
			SUB_WIN_NO: '2',
			listeners: {
				save: function(win, id) {
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
		data.push(['', '=全部=', 'font-weight:bold;color:#0A0A0A;text-align:center']);
		for(var i in StatusList) {
			var obj = StatusList[i];
			if(obj.Id != 1 && obj.Id != 3) {
				var style = ['font-weight:bold;text-align:center'];
				if(obj.BGColor) {
					style.push('color:' + obj.BGColor);
				}
				data.push([obj.Id, obj.Name, style.join(';')]);
			}
		}
		return data;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PRepaymentStatus',function(){
			if(!JShell.System.ClassDict.PRepaymentStatus){
    			JShell.Msg.error('未获取到还款状态，请刷新列表');
    			return;
    		}
            var StatusID = me.getComponent('buttonsToolbar').getComponent('StatusID');
			var List=JShell.System.ClassDict.PRepaymentStatus;
			
			if(StatusID.store.data.items.length==0){
			     StatusID.loadData(me.getStatusData(List));
			     StatusID.setValue(me.defaultStatusValue);
			}
			me.load(null, true, autoSelect);
    	});
	}
});