/**
 * 员工列表
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.res.empduty.EmpGrid', {
	extend:'Shell.ux.grid.Panel',
    title:'员工列表',
    width:1200,
    height:600,
    /**根据部门ID查询模式*/
    DeptTypeModel:true,
    /**部门ID*/
    DeptId:null,
    /**部门名称*/
    DeptName:null,
    /**部门时间戳*/
    DeptDataTimeStamp:null,
    /**根据部门ID查询模式*/
    DeptTypeModel:true,

	/**获取数据服务路径*/
	selectUrl:'/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true',
	/**根据部门ID获取数据服务路径*/
	selectUrl2:'/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?isPlanish=true',
	
  	/**修改服务地址*/
	editUrl:'/RBACService.svc/RBAC_UDTO_UpdateHREmployeeByField',
	/**删除数据服务路径*/
	delUrl:'/RBACService.svc/RBAC_UDTO_DelHREmployee',
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar:true,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 1000,
	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
//	defaultPageSize:200,
	/**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用查询框*/
	hasSearch:true,
	/**是否存在上级*/
	hasManager:true,
    /**是否单选*/
	checkOne:false,
    /**是否启用新增按钮*/
	hasAdd:false,
	/**是否启用修改按钮*/
	hasEdit:false,
	/**是否启用删除按钮*/
	hasDel:false,
	/**是否启用保存按钮*/
	hasSave:false,
	IsbtnAccept:false,
	initComponent:function(){
		var me = this;
		me.addEvents('addclick');
//		me.defaultWhere = me.defaultWhere || '';
//		
//		if(me.defaultWhere){
//			me.defaultWhere = '(' + me.defaultWhere + ') and ';
//		}
//		me.defaultWhere += '';
		//查询框信息
		me.searchInfo = {width:125,emptyText:'名称',isLike:true,
			itemId:'search',fields:['hremployee.CName']};

		me.buttonToolbarItems = ['refresh','-',{
			xtype:'checkbox',
			boxLabel:'本部门',
			itemId:'onlyShowDept',
			height:22,
			checked:!me.DeptTypeModel,
			listeners:{
				change:function(field,newValue,oldValue){
					me.changeShowType(newValue);
				}
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
		  
		var columns = [{
			text:'员工姓名',dataIndex:'HREmployee_CName',width:100,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'员工代码',dataIndex:'HREmployee_UseCode',width:100,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'隶属部门',dataIndex:'HREmployee_HRDept_CName',width:100,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'主键ID',dataIndex:'HREmployee_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'时间戳',dataIndex:'HREmployee_DataTimeStamp',hidden:true,hideable:false
		}]
		
		return columns;
	},
	loadByDeptId:function(id){
		var me = this;
		me.DeptId = id;
		me.onSearch();
	},
		changeDefaultWhere:function(){
		var me=this;
		//defaultWhere追加上IsUse约束
		if(me.defaultWhere){
			var index = me.defaultWhere.indexOf('hremployee.IsUse=1');
			if(index == -1){
				me.defaultWhere += ' and hremployee.IsUse=1';
			}
		}else{
			me.defaultWhere = 'hremployee.IsUse=1';
		}
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],
			url = '',
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,params = [];
			
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
		}
		
		//改变默认条件
		me.changeDefaultWhere();	
		//根据部门ID查询模式
		if(me.DeptTypeModel){
			url += JShell.System.Path.getUrl(me.selectUrl2);
		}else{
			url += JShell.System.Path.getUrl(me.selectUrl);
		}

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');

		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";

		//根据部门ID查询模式
		if(me.DeptTypeModel){
			url += '&where=id=' + me.DeptId;
			if (where) {
				url += '^' + JShell.String.encode(where);
			}
		}else{
			url += "&where=hremployee.HRDept.Id='" + me.DeptId + "'";
			if (where) {
				url += ' and ' + JShell.String.encode(where);
			}
		}
       
		return url;
	},
	changeShowType:function(value){
		var me = this;
		me.DeptTypeModel = value ? false : true;
		me.onSearch();
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
			var IsUse = rec.get('HREmployee_IsUse');
			me.updateOneByIsUse(i,id,IsUse);
		}
	},
	onAddClick:function(){
		this.fireEvent('addclick',this);
	},
	updateOneByIsUse:function(index,id,IsUse){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
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
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var len = data.list.length;
		for(var i=0;i<len;i++){
			if(data.list[i].HREmployee_IsUse == 'True' || data.list[i].HREmployee_IsUse == 'true'){
				data.list[i].HREmployee_IsUse = true;
			}else{
				data.list[i].HREmployee_IsUse = false;
			}
		}
		return data;
	}
});