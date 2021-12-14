/**
 * 模块列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.module.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	
	title: '模块列表 ',
	width: 800,
	height: 500,
	
	/**模块ID*/
	ModuleId:'',
	/**模块名称*/
	ModuleName:'',
	
  	/**获取数据服务路径*/
	selectUrl:'/RBACService.svc/RBAC_UDTO_SearchRBACModuleByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/RBACService.svc/RBAC_UDTO_UpdateRBACModuleByField',
	/**删除数据服务路径*/
	delUrl:'/RBACService.svc/RBAC_UDTO_DelRBACModule',
  	
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
	
	defaultOrderBy:[{property:'RBACModule_DispOrder',direction:'ASC'}],

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
		me.searchInfo = {width:220,emptyText:'模块名称',isLike:true,fields:['rbacmodule.CName']};
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var me = this;
		var columns = [{
			text:'图标',dataIndex:'RBACModule_PicFile',width:35,
			sortable:false,menuDisabled:true,align:'center',
			renderer:function(value,meta){
				var v = value;
		        if(value){
		        	v = JShell.System.Path.getModuleIconPathBySize(16) + '/' + v;
		        	v = '<img src="' + v + '"></img>';
		        }
		        return v;
		    }
		},{
			text:'模块名称',dataIndex:'RBACModule_CName',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'系统代码',dataIndex:'RBACModule_UseCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'标准代码',dataIndex:'RBACModule_StandCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'开发商代码',dataIndex:'RBACModule_DeveCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'RBACModule_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'显示次序',dataIndex:'RBACModule_DispOrder',width:60,
			sortable:false,menuDisabled:true,defaultRenderer:true,type:'int'
		},{
			text:'入口地址',dataIndex:'RBACModule_Url',width:200,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'模块ID',dataIndex:'RBACModule_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'RBACModule_DataTimeStamp',hidden:true,hideable:false
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
			var IsUse = rec.get('RBACModule_IsUse');
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
		me.openModuleForm();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		var id = records[0].get(me.PKField);
		me.openModuleForm(id);
	},
	/**打开表单*/
	openModuleForm:function(id){
		var me = this;
		var config = {
			ParentID:me.ModuleId,//上级模块ID
			ParentName:me.ModuleName,//上级模块名称
			showSuccessInfo:false,
			resizable:false,
			formtype:'add',
			listeners:{
				save:function(win){
					me.onSearch();
					win.close();
				}
			}
		};
		if(id){
			config.formtype = 'edit';
			config.PK = id;
		}
		JShell.Win.open('Shell.class.sysbase.module.Form',config).show();
	},
	/**根据上级模块ID加载数据*/
	loadByParentId:function(id,name){
		var me = this;
		me.ModuleId = id;
		me.ModuleName = name;
		me.defaultWhere = 'rbacmodule.ParentID=' + id;
		me.onSearch();
	}
});