/**
 * 合同选择列表
 * @author liangyl	
 * @version 2015-11-10
 */
Ext.define('Shell.class.wfm.business.receive.preceiveplan.change.CheckGrid', {
	extend: 'Shell.class.wfm.business.receive.preceiveplan.basic.CheckGrid',
	title: '合同选择列表',
	width:500,
	height:400,
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPContractByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'PContract_DispOrder',
		direction: 'ASC'
	}],
	/**是否单选*/
	checkOne: true,

	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		//客户属于自己
		me.getClientIds(function(ids) {
			ids = ids.length > 0 ? ids : ['-1'];
			me.defaultWhere = 'pcontract.IsUse=1 and pcontract.ContractStatus>1 and pcontract.PClientID in(' + ids.join(',') + ')';
			me.load(null, true, autoSelect);
		});
	},
	/**获取当前用户所负责的客户Ids数组*/
	getClientIds: function(callback) {
		var me = this;

		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;
		var fields = ['PSalesManClientLink_PClientID'];
		var where = 'psalesmanclientlink.SalesManID=' + userId

		var url = '/SingleTableService.svc/ST_UDTO_SearchPSalesManClientLinkByHQL';
		url += '?isPlanish=true&fields=' + fields.join(',') + '&where=' + where;
		url = JShell.System.Path.getRootUrl(url);

		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(Ext.typeOf(callback)) {
					var ids = [];
					for(var i in data.value.list) {
						ids.push(data.value.list[i].PSalesManClientLink_PClientID);
					}
					callback(ids);
				}
			} else {
				JShell.Msg.error('客户销售关系获取错误：' + data.msg);
			}
		});
	}
});