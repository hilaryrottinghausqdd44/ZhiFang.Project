/**
 * 移库总单
 * @author liangyl
 * @version 2017-12-14
 */
Ext.define('Shell.class.rea.client.transfer.accept.DocGrid', {
	extend: 'Shell.class.rea.client.transfer.DocGrid',
	searchStatusValue:null,
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsTransferDoc',
	/**修改服务地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDocByField',
	
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems =  [];
		buttonToolbarItems.push('refresh','-');
		buttonToolbarItems.push({text:'新增移库',tooltip:'新增移库',iconCls:'button-add',
		    itemId:'Add',
			handler: function() {
				me.onAddClick();
			}
		},{text:'确认移库',tooltip:'确认移库',iconCls:'button-accept',
		    itemId:'btnAdd',
			handler: function() {
			   me.onCheckAddClick();
			}
		});
		return buttonToolbarItems;
	},
	removeSomeStatusList:function(){
		var me = this;
		var tempList = JShell.JSON.decode(JShell.JSON.encode(JShell.REA.StatusList.Status[me.ReaBmsTransferDocStatus].List));
		var removeArr = [];
		
		if(me.TYPE=='2' || me.TYPE=='3' ){
			//暂存,已申请
			if(tempList[1]) removeArr.push(tempList[1]);
			if(tempList[2]) removeArr.push(tempList[2]);
		}else{
			if(tempList[1]) removeArr.push(tempList[1]);
			if(tempList[2]) removeArr.push(tempList[2]);
			if(tempList[3]) removeArr.push(tempList[3]);
			if(tempList[4]) removeArr.push(tempList[4]);
			if(tempList[5]) removeArr.push(tempList[5]);
		}
		Ext.Array.each(removeArr, function(name, index, countriesItSelf) {
			Ext.Array.remove(tempList, removeArr[index]);
		});
		
		me.searchStatusValue=tempList;
		return tempList;
	},
	/**综合查询*/
	onGridSearch:function(){
		var me = this;
		JShell.Action.delay(function(){
			me.onSearch();
		},100);
	}
});