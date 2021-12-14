/**
 * 员工列表
 * @author liangyl
 * @version 2016-08-12
 */
Ext.define('Shell.class.qms.equip.templet.user.EmpGrid', {
	extend: 'Shell.class.qms.equip.templet.user.EmpbasicGrid',
    title:'员工选择列表',
    width:270,
    height:300,
    
    /**根据部门ID查询模式*/
    DeptTypeModel:true,
    /**部门ID*/
    DeptId:null,
	
	/**获取数据服务路径*/
	selectUrl:'/RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true',
	/**根据部门ID获取数据服务路径*/
	selectUrl2:'/RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?isPlanish=true',
	
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
		
//		me.defaultWhere = me.defaultWhere || '';
//		
//		if(me.defaultWhere){
//			me.defaultWhere = '(' + me.defaultWhere + ') and ';
//		}
//		me.defaultWhere += 'hremployee.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:120,emptyText:'名称',isLike:true,
			fields:['hremployee.CName']};
			
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
	initButtonToolbarItems:function(){
		var me = this;
		me.callParent(arguments);
		
		me.buttonToolbarItems.splice(1,0,{
			xtype:'checkbox',
			boxLabel:'本部门',
			itemId:'onlyShowDept',
			checked:!me.DeptTypeModel,
			listeners:{
				change:function(field,newValue,oldValue){
					me.changeShowType(newValue);
				}
			}
		});
		if(me.IsbtnAccept==true){
			me.buttonToolbarItems.push('accept');
		}
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
	}
});