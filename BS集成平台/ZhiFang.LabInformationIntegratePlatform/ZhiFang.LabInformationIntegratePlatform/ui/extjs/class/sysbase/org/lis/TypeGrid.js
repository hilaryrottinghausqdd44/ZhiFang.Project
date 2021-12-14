/**
 * 部门身份列表
 * @author Jcall	
 * @version 2019-10-15
 */
Ext.define('Shell.class.sysbase.org.lis.TypeGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	
	title: '部门LIS身份 ',
	width: 600,
	height: 260,
	
	/**获取所有身份枚举*/
	getTypeListUrl:'/ServerWCF/CommonService.svc/GetClassDic',
	//获取系统列表
	getSystemListUrl:'/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL',
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL',
	/**新增服务地址*/
	addUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddHRDeptIdentity',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_DelHRDeptIdentity',
  	
  	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect:false,
	/**显示成功信息*/
	showSuccessInfo:false,
	/**消息框消失时间*/
	hideTimes:3000,
	
	/**默认加载*/
	defaultLoad:false,
	/**带分页栏*/
	hasPagingtoolbar:false,
	
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl:true,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: true,
	
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用保存按钮*/
	hasSave:true,
	
	//部门枚举码
	DEPT_ENUM_CODE:'DeptSystemType',
	//检验之星系统编码
	SYSTEM_CODE:'ZF_LAB_START',
	//检验之星系统ID
	SYSTEM_ID:null,
	//检验之星系统名称
	SYSTEM_NAME:null,
	//检验之星系统地址
	SYSTEM_HOST:null,
	
	//所有身份枚举列表
	ALL_TYPE_LIST:null,
	//身份关系列表
	LINK_LIST:null,
	
	//机构ID
	DeptId:'',
	//机构名字
	DeptName:'',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.onLoadSystemList(function(){
			me.onLoadTypeList(function(){
				me.initGridData();
				me.onSearch();
			});
		});
	},
	initComponent: function() {
		var me = this;
		//me.title += ' - ' + me.DeptName;
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'ID',dataIndex:'Id',isKey:true,hidden:true,hideable:false
		},{
			text:'机构身份ID',dataIndex:'IdenTypeID',width:150,
			hidden:true,hideable:false
		},{
			text:'机构身份名称',dataIndex:'TSysName',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'机构类型编码',dataIndex:'TSysCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'勾选',dataIndex:'IsCheck',
			width:40,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
			text:'所属系统ID',dataIndex:'SystemID',width:150,
			hidden:true,hideable:false
		},{
			text:'所属系统名称',dataIndex:'SystemName',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'所属系统编码',dataIndex:'SystemCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		
		return columns;
	},
	//获取系统列表
	onLoadSystemList:function(callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.getSystemListUrl + 
				"?fields=IntergrateSystemSet_Id,IntergrateSystemSet_SystemName,IntergrateSystemSet_SystemHost" +
				"&where=intergratesystemset.SystemCode='" + me.SYSTEM_CODE + "'";
			
		JShell.Server.get(url,function(data){
			if(data.success){
				var list = (data.value || {}).list || [];
				if(list.length == 1){
					me.SYSTEM_ID = list[0].Id;
					me.SYSTEM_NAME = list[0].SystemName;
					me.SYSTEM_HOST = list[0].SystemHost;
					callback();
				}else{
					//JShell.Msg.error('系统编码为' + me.SYSTEM_CODE + '的系统地存在多条信息，请维护好再使用本功能！');
					var error = me.errorFormat.replace(/{msg}/,'系统编码为' + me.SYSTEM_CODE + '的系统地存在多条信息，请维护好再使用本功能！');
					me.getView().update(error);
				}
			}else{
				//JShell.Msg.error(data.msg);
				var error = me.errorFormat.replace(/{msg}/,data.msg);
				me.getView().update(error);
			}
		});
	},
	//获取所有身份枚举列表
	onLoadTypeList:function(callback){
		var me = this,
			url = JShell.System.Path.LOCAL + '/' + me.SYSTEM_HOST + me.getTypeListUrl;
		url += '?classname=' + me.DEPT_ENUM_CODE;
			
		if(!me.SYSTEM_HOST){
			//JShell.Msg.error('系统编码为' + me.SYSTEM_CODE + '的系统地址不存在，请维护好再使用本功能！');
			var error = me.errorFormat.replace(/{msg}/,'系统编码为' + me.SYSTEM_CODE + '的系统地址不存在，请维护好再使用本功能！');
			me.getView().update(error);
			return;
		}
			
		JShell.Server.get(url,function(data){
			if(data.success){
				me.ALL_TYPE_LIST = data.value || [];
				callback();
			}else{
				//JShell.Msg.error(data.msg);
				var error = me.errorFormat.replace(/{msg}/,'本系统没有配置部门身份枚举！');
				me.getView().update(error);
			}
		});
	},
	//获取身份关系列表
	onLoadLinkList:function(callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl;
		url += "?fields=HRDeptIdentity_Id,HRDeptIdentity_TSysCode" +
		"&where=hrdeptidentity.HRDept.Id=" + me.DeptId +
		" and hrdeptidentity.SystemCode='" + me.SYSTEM_CODE + "'";
			
		JShell.Server.get(url,function(data){
			if(data.success){
				me.LINK_LIST = (data.value || {}).list || [];
				callback();
			}else{
				//JShell.Msg.error(data.msg);
				var error = me.errorFormat.replace(/{msg}/,data.msg);
				me.getView().update(error);
			}
		});
	},
	//查询数据
	onSearch: function(){
		var me = this;
		me.onLoadLinkList(function(){
			me.initGridCheck();
			me.enableControl();
		});
	},
	//初始化列表勾选
	initGridCheck:function(){
		var me = this,
			linkList = me.LINK_LIST;
			
		me.store.each(function(record){
			record.set({Id:'',IsCheck:false});
			record.commit();
		});
		
		for(var i in linkList){
			var record = me.store.findRecord('TSysCode',linkList[i].TSysCode);
			record.set({
				IsCheck:true,
				Id:linkList[i].Id
			});
			record.commit();
		}
	},
	//初始化列表数据
	initGridData:function(){
		var me = this,
			typeList = me.ALL_TYPE_LIST,
			list = [];
		
		for(var i in typeList){
			list.push({
				Id:'',
				IsCheck:false,
				IdenTypeID:typeList[i].Id,
				TSysCode:typeList[i].Code,
				TSysName:typeList[i].Name,
				SystemID:me.SYSTEM_ID,
				SystemCode:me.SYSTEM_CODE,
				SystemName:me.SYSTEM_NAME
			});
		}
		
		me.store.loadData(list);
	},
	
	//保存处理
	onSaveClick:function(){
		var me = this,
			records = me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
			
		if(len == 0) return;
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			var id = rec.get(me.PKField);
			if(id){
				me.onDelOne(id);
			}else{
				me.onAddOne(rec);
			}
		}
	},
	//保存一条关系
	onAddOne:function(record){
		var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;
			
		var entity = {
			IsUse:true,
			IdenTypeID:record.get('IdenTypeID'),
			TSysCode:record.get('TSysCode'),
			TSysName:record.get('TSysName'),
			SystemID:me.SYSTEM_ID,
			SystemCode:me.SYSTEM_CODE,
			SystemName:me.SYSTEM_NAME,
			HRDept:{Id:me.DeptId,DataTimeStamp:[0,0,0,0,0,0,0,0]}
		};
		var params = Ext.JSON.encode({entity:entity});
		
		var TSysCode = record.get('TSysCode');
		JShell.Server.post(url,params,function(data){
			if(data.success){
				me.saveCount++;
				var rec= me.store.findRecord('TSysCode',TSysCode);
				rec.set({Id:data.value.id});
				rec.commit();
			}else{
				me.saveErrorCount++;
				JShell.Msg.error(data.msg);
			}
			me.onSaveOver();
		});
	},
	//删除一条关系
	onDelOne:function(id){
		var me = this,
			url = JShell.System.Path.ROOT + me.delUrl + '?id=' + id;
			
		JShell.Server.get(url,function(data){
			if(data.success){
				me.saveCount++;
				var record = me.store.findRecord('Id',id);
				record.set({Id:''});
				record.commit();
			}else{
				me.saveErrorCount++;
				JShell.Msg.error(data.msg);
			}
			me.onSaveOver();
		});
	},
	//保存结束
	onSaveOver:function(){
		var me = this;
		if (me.saveCount + me.saveErrorCount == me.saveLength) {
			me.hideMask();//隐藏遮罩层
			if (me.saveErrorCount > 0){
				JShell.Msg.error('存在失败信息，请重新保存一次！');
			}
		}
	}
});