/**
 * 机构管理
 * @author liangyl
 * @version 2018-05-21
 */
Ext.define('Shell.class.rea.center.Grid', {
 extend:'Shell.ux.grid.Panel',
    requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	
    title:'机构列表',
    width:800,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchCenOrgByHQL?isPlanish=true',
    /**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelCenOrg',
	/**查询角色数据服务路径*/
	selectRoleUrl:'/ReaManageService.svc/ST_UDTO_SearchRBACRoleModuleByLabIDAndSysRoleId?isPlanish=true',
    /**获取角色模块服务*/
    selectRoleModuleUrl:'/RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL',
    /**获取角色模块服务*/
    selectExportUrl:'/ReaManageService.svc/ST_UDTO_SearchExportAuthorizationFileOfPlatform',
    
    /**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**默认加载数据*/
	defaultLoad:true,
	/**排序字段*/
	defaultOrderBy:[{property:'CenOrg_OrgNo',direction:'ASC'}],
	
	hasDel: true,
	//上级机构
    POrgEnum :{},
    POrgList:[],
    /**机构类型*/
    CenOrgTypeEnum:{},
    CenOrgTypeList:[],
     /**显示某个角色分类,根据角色表SName过滤*/
    ROLETYPE:'',
    /**加载数据提示*/
	loadingText:JShell.Server.LOADING_TEXT,
	
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:180,emptyText:'机构名称/机构编号',isLike:true,itemId:'Search',
			fields:['cenorg.CName','cenorg.EName','cenorg.ShortCode','cenorg.OrgNo']
		};
		me.buttonToolbarItems = ['refresh','-',
		{text:'申请',tooltip:'申请',
		iconCls:'button-add',
			handler:function(){
				me.onAddClick();
			}
		},'-','edit','-',{text:'更改授权',tooltip:'更改授权',
		    iconCls:'button-edit',
			handler:function(){
				var records = me.getSelectionModel().getSelection();
				if (records.length == 0) {
					JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
					return;
				}
				var LabID = records[0].get('CenOrg_LabID');
				var DeveCode = records[0].get('CenOrg_DeveCode');
				var CenOrgID=records[0].get('CenOrg_Id');
				var roleId ='';
				var ids='';
				me.getRole(LabID,function(data){
					if(data && data.value){
						var list= data.value.list;
						var len = list.length;
						for(var i= 0; i<len; i++ ){
						    ids+=','+list[i].RBACRoleModule_RBACModule_Id;
						}
						ids=0==ids.indexOf(",")?ids.substr(1):ids;
					}
				});
				me.showEditForm(LabID,ids,roleId,CenOrgID);
			}
		},'-',{
			width:85,boxLabel:'显示禁用',itemId:'IsUser',
		    xtype:'checkbox',checked:false,
		    listeners:{
		    	 change:function(com,  newValue,  oldValue,  eOpts ){
					me.onSearch();
				}
		    }
	    },'->',{
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
			dataIndex: 'CenOrg_OrgNo',text: '机构编号',
			width: 100,type:'int',defaultRenderer: true
		},{
			dataIndex: 'CenOrg_CName',text: '机构名称',
			width: 180,defaultRenderer: true
		},{
			dataIndex: 'CenOrg_EName',text: '英文名',
			width: 100,defaultRenderer: true
		},{
			dataIndex: 'CenOrg_Memo',text: '备注',width: 100,
			renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta);
				return v;
			}
		},{
			dataIndex: 'CenOrg_ServerIP',text: '服务器IP',
			width: 100,defaultRenderer: true
		},{
			dataIndex: 'CenOrg_ServerPort',text: '服务器端口',
			width: 100,defaultRenderer: true
		},{
			dataIndex: 'CenOrg_ShortCode',text: '机构代码',
			width: 100,defaultRenderer: true
		},{
			xtype: 'actioncolumn',text: '初始化授权导出',
			align: 'center',width: 100,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-exp hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var record = grid.getStore().getAt(rowIndex);
			        var labId = record.get('CenOrg_LabID');
					var cenOrgId=record.get('CenOrg_Id');
					me.onExportInfo(1,labId,cenOrgId)
					
				}
			}]
		},{
			xtype: 'actioncolumn',text: '授权变更导出',
			align: 'center',width: 100,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-exp hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var record = grid.getStore().getAt(rowIndex);
			        var labId = record.get('CenOrg_LabID');
					var cenOrgId=record.get('CenOrg_Id');
					me.onExportInfo(2,labId,cenOrgId)
				}
			}]
		},{
			xtype: 'actioncolumn',text: '操作记录',
			align: 'center',width: 70,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-exp hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var record = grid.getStore().getAt(rowIndex);
					var LabID=record.get('CenOrg_LabID');
//					var BobjectID=record.get('CenOrg_LabID');
					me.showSCOperation(LabID);
				}
			}]
		},{
			dataIndex: 'CenOrg_DispOrder',text: '次序',
			width: 50,align:'center',type:'int',
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_Visible',text: '启用',
			width: 50,align:'center',
			type:'bool',isBool:true,defaultRenderer: true
		},{
			dataIndex: 'CenOrg_Id',text: '主键ID',hidden: true,
			hideable: false,isKey: true,defaultRenderer: true
		},{
			dataIndex: 'CenOrg_LabID',text: 'LabID',
			hidden: true,hideable: true,defaultRenderer: true
		}];
		
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		this.showForm(null);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();
		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.showOrgEditForm(records[0].get(me.PKField));
	},
	
	 /**修改机构信息*/
	showOrgEditForm: function(id) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			resizable: false,
			title:'机构信息',
			ROLETYPE:me.ROLETYPE,
			height:330,
			width:680,
			listeners: {
				save: function(p) {
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,2000);
					p.close();
					me.onSearch();
				}
			}
		};
		if(id) {
			config.formtype = 'edit';
			config.PK = id;
		} else {
			config.formtype = 'add';
		}
		JShell.Win.open('Shell.class.rea.center.register.EditForm', config).show();
	},
    /**显示表单*/
	showForm: function(id) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		var config = {
			resizable: false,
			title:'机构注册',
			ROLETYPE:me.ROLETYPE,
			height:height,
			width:maxWidth,
			listeners: {
				save: function(p) {
					JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,2000);
					p.close();
					me.onSearch();
				}
			}
		};
		if(id) {
			config.formtype = 'edit';
			config.PK = id;
		} else {
			config.formtype = 'add';
		}
		JShell.Win.open('Shell.class.rea.center.register.App', config).show();
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			IsUser=buttonsToolbar.getComponent('IsUser').getValue(),
			params = [];
        if(!IsUser){
        	params.push('cenorg.Visible=1');
        }else{
        	params.push('(cenorg.Visible=1 or cenorg.Visible=0)');
        }
		if (Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}
       
		if (params.length > 0) {
			me.internalWhere = params.join(' and ');
		}

		return me.callParent(arguments);
	},
	/**获取查询框内容*/
	getSearchWhere: function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for (var i = 0; i < len; i++) {
			if(i == 'cenorg.OrgNo'){
				if(!isNaN(value)){
					where.push("cenorg.OrgNo=" + value);
				}
				continue;
			}
			if (isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me=this;
		return data;
	},
	showMemoText:function(value, meta){
		var me=this	;
        var val=value.replace(/(^\s*)|(\s*$)/g, ""); 	
		val = val.replace(/\\r\\n/g, "<br />");
        val = val.replace(/\\n/g, "<br />");
		var v = "" + value;
		var index1=v.indexOf("</br>");
		if(index1>0)v=v.substring(0,index1);
		if(v.length > 0)v = (v.length > 20 ? v.substring(0, 20) : v);
		if(value.length>20){
			v= v+"...";
		}
        var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
        meta.tdAttr = 'data-qtip="' + qtipValue + '"';
        return v
	},
	 /**查看授权查询*/
	showForm2: function() {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.69;
		var height = document.body.clientHeight * 0.62;
		var config = {
			resizable: false,
			title:'查看授权',hidden:true,
			height:height,
			width:640
		};
		JShell.Win.open('Shell.class.rea.center.search.App', config).show();
	},
	/**更改授权*/
	showEditForm: function(LabID,RoleModuleID,roleId,CenOrgID) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.78;
		var height = document.body.clientHeight * 0.96;
		var config = {
			resizable: false,
			title:'授权更改',hidden:true,
			height:height,LabID:LabID,CenOrgID:CenOrgID,
			RoleID:roleId,RoleModuleID:RoleModuleID,
			width:380
		};
		JShell.Win.open('Shell.class.rea.center.register.ModuleTree', config).show();
	},
	//获取到角色
	getRole:function(LabID,callback){
		var me=this;
		var url = JShell.System.Path.ROOT + me.selectRoleUrl;
		url += "&fields=RBACRoleModule_RBACModule_Id&labId="+LabID;
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
	/**授权文件从智方平台导出,
	 * fileType:(授权文件类型:1:机构初始化;2:授权变更
	 */
	onExportInfo: function(fileType,labId,cenOrgId) {
		var me=this;
		var url = (me.selectExportUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.selectExportUrl;
	    var where="?labId="+labId+"&cenOrgId="+cenOrgId+'&fileType='+fileType;
        url+=where;
		window.open(url);
	},
	 /**查看操作记录*/
	showSCOperation: function(LabID) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.69;
		var height = document.body.clientHeight * 0.62;
		var config = {
			resizable: false,
			title:'操作记录',hidden:true,
			height:height,PK:LabID,
			LabID:LabID,
			width:640
		};
		JShell.Win.open('Shell.class.rea.center.register.SCOperation', config).show();
	}
});