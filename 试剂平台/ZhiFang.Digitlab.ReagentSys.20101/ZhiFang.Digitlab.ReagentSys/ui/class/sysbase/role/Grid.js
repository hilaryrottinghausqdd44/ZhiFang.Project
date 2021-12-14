/**
 * 角色列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.role.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	
	title: '角色列表 ',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/RBACService.svc/RBAC_UDTO_UpdateRBACRoleByField',
	/**删除数据服务路径*/
	delUrl:'/RBACService.svc/RBAC_UDTO_DelRBACRole ',
  	
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	
	/**默认加载*/
	defaultLoad: true,
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
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:true,
	/**是否启用修改按钮*/
	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel:true,
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**查询栏参数设置*/
	searchToolbarConfig:{},
	
	defaultOrderBy:[{property:'RBACRole_DispOrder',direction:'ASC'}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.on({
			itemdblclick:function(view,record){
				me.onEditClick();
			}
		});
	},
	initComponent: function() {
		var me = this;
		
		//查询框信息
		me.searchInfo = {width:220,emptyText:'角色名称',isLike:true,fields:['rbacrole.CName']};
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var me = this;
		var columns = [{
			text:'角色名称',dataIndex:'RBACRole_CName',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'系统代码',dataIndex:'RBACRole_UseCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'标准代码',dataIndex:'RBACRole_StandCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'开发商代码',dataIndex:'RBACRole_DeveCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'RBACRole_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'显示次序',dataIndex:'RBACRole_DispOrder',width:60,
			sortable:false,menuDisabled:true,defaultRenderer:true,type:'int'
		},{
			text:'模块ID',dataIndex:'RBACRole_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'RBACRole_DataTimeStamp',hidden:true,hideable:false
		}];
		
		return columns;
	},
	onSaveClick:function(){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
			
		if(len == 0) return;
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			var id = rec.get(me.PKField);
			var IsUse = rec.get('RBACRole_IsUse');
			me.updateOneByIsUse(i,id,IsUse);
		}
	},
	updateOneByIsUse:function(index,id,IsUse){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var params = Ext.JSON.encode({
			entity:{
				Id:id,
				IsUse:IsUse
			},
			fields:'Id,IsUse'
		});
		
		setTimeout(function(){
			JShell.Server.post(url,params,function(data){
				var record = me.store.findRecord(me.PKField,id);
				if(data.success){
					if(record){record.set(me.DelField,true);record.commit();}
					me.saveCount++;
				}else{
					me.saveErrorCount++;
					if(record){record.set(me.DelField,false);record.commit();}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength){
					me.hideMask();//隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		},100 * index);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		me.fireEvent('addclick',me);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		me.fireEvent('editclick',me,records[0]);
	}
});