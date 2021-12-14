/**
 * 合同查询列表
 * @author Jcall
 * @version 2016-11-14
 */
Ext.define('Shell.class.wfm.business.contract.search.Grid', {
    extend: 'Shell.class.wfm.business.contract.basic.SearchGrid',

    title: '合同查询列表',
	
    /**是否启用刷新按钮*/
    hasRefresh: true,
    /**是否启用查询框*/
    hasSearch: true,
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		me.onPContractTotal();
		JShell.System.ClassDict.init('ZhiFang.Entity.ProjectProgressMonitorManage','PContractStatus',function(){
			if(!JShell.System.ClassDict.PContractStatus){
    			JShell.Msg.error('未获取到合同状态，请刷新列表');
    			return;
    		}
			var StatusID = me.getComponent('buttonsToolbar').getComponent('StatusID');
			var List=JShell.System.ClassDict.PContractStatus;
			
			if(StatusID.store.data.items.length==0){
			     StatusID.loadData(me.getStatusData(List));
			     StatusID.setValue(me.defaultStatusValue);
			}
			//客户属于自己
			me.getClientIds(function(ids){
				ids = ids.length > 0 ? ids : ['-1'];
				var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
				me.defaultWhere = 'pcontract.PClientID in(' + ids.join(',') + ') or pcontract.PrincipalID='+userId +' or pcontract.ApplyManID='+userId;
				me.load(null, true, autoSelect);
			});
    	});
	}
});