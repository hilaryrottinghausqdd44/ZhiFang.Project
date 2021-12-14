/**
 * 机构列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.org.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	
	title: '机构列表 ',
	width: 800,
	height: 500,
	
	/**机构ID*/
	OrgId:'',
	/**机构名称*/
	OrgName:'',
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_UpdateHRDeptByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_DelHRDept',
  	
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
	
	defaultOrderBy:[{property:'HRDept_DispOrder',direction:'ASC'}],

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
		me.searchInfo = {width:220,emptyText:'机构名称',isLike:true,fields:['hrdept.CName']};
		//功能按钮栏
		me.buttonToolbarItems = ['refresh','add','edit','del','save',{
			text:'EXCEL导入',tooltip:'EXCEL导入',iconCls:'button-import',
			handler:function(but){
				me.onExcelImportClick();
			}
		},'->', {
			type: 'search',
			info: me.searchInfo
		}];
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var me = this;
		//sortable:false,menuDisabled:true,defaultRenderer:true
		var columns = [{
			text:'部门名称',dataIndex:'HRDept_CName',width:150,
			defaultRenderer:true
		},{
			text:'系统代码',dataIndex:'HRDept_UseCode',width:100,
			defaultRenderer:true
		},{
			text:'标准代码(LIS编码)',dataIndex:'HRDept_StandCode',width:150,
			defaultRenderer:true
		},{
			text:'开发商代码(HIS编码)',dataIndex:'HRDept_DeveCode',width:150,
			defaultRenderer:true
		},{
			text:'部门联系人',dataIndex:'HRDept_Contact',width:100,
			defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'HRDept_IsUse',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'显示次序',dataIndex:'HRDept_DispOrder',width:60,
			defaultRenderer:true,type:'int'
		},{
			text:'模块ID',dataIndex:'HRDept_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'HRDept_DataTimeStamp',hidden:true,hideable:false
		},{
			xtype: 'actioncolumn',
			text: '身份',
			align: 'center',
			width: 40,
			style:'font-weight:bold;color:white;background:green;',
			hideable: false,
			items: [{
				iconCls:'button-config hand',
				tooltip:'部门身份',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.openTypeLinkGrid(rec);
				}
			}]
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
			var IsUse = rec.get('HRDept_IsUse');
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
		me.openOrgForm();
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
		me.openOrgForm(id);
	},
	/**打开表单*/
	openOrgForm:function(id){
		var me = this;
		var config = {
			ParentID:me.OrgId,//上级机构ID
			ParentName:me.OrgName,//上级机构名称
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
		JShell.Win.open('Shell.class.sysbase.org.Form',config).show();
	},
	/**根据上级机构ID加载数据*/
	loadByParentId:function(id,name){
		var me = this;
		me.OrgId = id;
		me.OrgName = name;
		me.defaultWhere = 'hrdept.ParentID=' + id;
		me.onSearch();
	},
	/**打开部门身份选择列表*/
	openTypeLinkGrid:function(record){
		var me = this,
			Id = record.get(me.PKField),
			Name = record.get('HRDept_CName');
		
		JShell.Win.open('Shell.class.sysbase.org.lis.Tab',{
			DeptId:Id,
			DeptName:Name
		}).show();
	},
	//EXCEL导入
	onExcelImportClick:function(){
		var me = this;
		
		JShell.Win.open('Shell.class.sysbase.org.UploadPanel').show();
	}
});