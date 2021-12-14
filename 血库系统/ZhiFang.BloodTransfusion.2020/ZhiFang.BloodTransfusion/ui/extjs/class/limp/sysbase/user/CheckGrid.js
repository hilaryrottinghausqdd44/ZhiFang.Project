/**
 * 员工选择
 * @author longfc
 * @version 2020-03-04
 */
Ext.define('Shell.class.limp.sysbase.user.CheckGrid',{
    extend:'Shell.ux.grid.CheckPanel',
    title:'员工选择列表',
    width:270,
    height:300,
    
    /**根据部门ID查询模式*/
    DeptTypeModel:true,
    /**部门ID*/
    DeptId:null,
	
	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/BloodTransfusionManageService.svc/RBAC_UDTO_SearchHREmployeeByHQL?isPlanish=true',
	/**根据部门ID获取数据服务路径*/
	selectUrl2:'/ServerWCF/BloodTransfusionManageService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?isPlanish=true',
	
    /**是否单选*/
	checkOne:true,
    
	initComponent:function(){
		var me = this;
		
		me.defaultWhere = me.defaultWhere || '';
		
		if(me.defaultWhere){
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += 'hremployee.IsUse=1';
		
		//查询框信息
		me.searchInfo = {width:145,emptyText:'名称',isLike:true,
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
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'员工代码',dataIndex:'HREmployee_UseCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'隶属部门',dataIndex:'HREmployee_HRDept_CName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'隶属部门ID',dataIndex:'HREmployee_HRDept_Id',width:100,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
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
	},
	loadByDeptId:function(id){
		var me = this;
		me.DeptId = id;
		me.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],
			url = '';
			
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