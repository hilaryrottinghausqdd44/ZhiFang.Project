/**
 * 字典类型列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.dicttype.SimpleGrid',{
    extend: 'Shell.ux.grid.Panel',
    
    title:'字典类型列表',
    width: 205,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/BaseService.svc/ST_UDTO_SearchFDictTypeByHQL?isPlanish=true',
  	
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:50,
	
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: false,
	
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**查询栏参数设置*/
	searchToolbarConfig:{},
	
	defaultOrderBy:[{property:'FDictType_DispOrder',direction:'ASC'}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:220,emptyText:'编码/名称',isLike:true,
			fields:['fdicttype.DictTypeCode','fdicttype.CName']};
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'编码',dataIndex:'FDictType_DictTypeCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'名称',dataIndex:'FDictType_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
//		},{
//			text:'次序',dataIndex:'FDictType_DispOrder',width:100,
//			defaultRenderer:true,align:'center',type:'int'
		},{
			text:'主键ID',dataIndex:'FDictType_Id',isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var isAdmin = (JShell.System.ADMIN_CAN_LOGIN == 
			JShell.System.Cookie.get(JShell.System.Cookie.map.ACCOUNTNAME));
		
		//超级管理员登录时不过滤数据
		if(isAdmin) return data;
		
		var me = this,
			list = data.list || [],
			len = list.length,
			ParameterIndex = null;
			
		for(var i=0;i<len;i++){
			if(list[i].FDictType_DictTypeCode == 'BParameter'){//系统参数
				ParameterIndex = i;
				break;
			}
		}
		
		if(ParameterIndex != null){
			data.list = list.splice(i,1);
		}
			
		return data;
	}
});