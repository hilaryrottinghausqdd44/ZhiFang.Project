/**
 * 当前打开的小组信息
 * @author Jcall
 * @version 2020-09-09
 */
Ext.define('Shell.class.basic.data.OpenedSection',{
	extend:'Shell.class.basic.data.Operate',
	
	//小组页签信息包含：小组ID、小组名称
	//例如：{"Id":"1","Name":"生化一组"}
	//新增小组页签
	set:function(info){
		var me = this,
			list = me.getList();
			
		var inList = false;
		for(var i=0;i<list.length;i++){
			if(list[i].Id == info.Id){
				inList = true;
				break;
			}
		}
		if(!inList){
			list.push(info);
		}
		
		me.setList(list);
	},
	//删除小组页签
	remove:function(id){
		var me = this,
			list = me.getList();
			
		for(var i=0;i<list.length;i++){
			if(list[i].Id == id){
				list.splice(i,1);
				i--;
			}
		}
		
		me.setList(list);
	},
	//清空小组页签
	removeAll:function(){
		this.setList([]);
	},
	//保存当前打开的小组页签列表
	setList:function(list){
		var me = this,
			AccountInfo = me._getAccountInfo();
		
		AccountInfo['OpenedSectionList'] = list;
			
		me._setAccountInfo(AccountInfo);
	},
	//获取当前打开的小组页签
	getList:function(){
		var me = this,
			AccountInfo = me._getAccountInfo(),
			list = AccountInfo['OpenedSectionList'] || [];
			
		return list;
	}
});