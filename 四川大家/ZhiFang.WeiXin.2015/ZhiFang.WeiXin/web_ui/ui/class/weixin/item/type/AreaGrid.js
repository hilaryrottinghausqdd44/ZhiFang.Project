/**
 * 根据区域选择项目分类树
 * @author liangyl	
 * @version 2017-03-03
 */
Ext.define('Shell.class.weixin.item.type.AreaGrid', {
	extend: 'Shell.class.weixin.hospital.area.Grid',
	title: '区域列表 ',
	
	/**默认加载*/
	defaultLoad: true,
	/**是否启用刷新按钮*/
	hasRefresh:true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	
		me.addAllData();
	},
	/**添加默认行（全部)*/
	addAllData:function(){
		var me = this;
		var obj={ClientEleArea_AreaCName:'全部',ClientEleArea_Id:''};
	    me.store.insert(0,obj);
	    me.getSelectionModel().select(0);
	},
	/**清空数据,禁用功能按钮*/
	clearData: function() {
		var me = this;
		me.disableControl(); //禁用 所有的操作功能
		me.store.removeAll(); //清空数据
		me.addAllData();
	},
	
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var result = {},
			list = [],
			arr = [],
			obj = {};
	    obj={ClientEleArea_AreaCName:'全部',ClientEleArea_Id:''};
		if(data.value){
			list=data.value.list;
			list.splice(0,0,obj);
		}else{
			list=[];
			list.push(obj);
		}
	    result.list =  list;
		return result;
	}
});