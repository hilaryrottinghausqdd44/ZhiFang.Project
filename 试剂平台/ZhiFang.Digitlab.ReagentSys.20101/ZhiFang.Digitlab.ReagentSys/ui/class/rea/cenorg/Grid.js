/**
 * 机构列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.cenorg.Grid',{
    extend:'Shell.ux.grid.Panel',
    requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
    title:'机构列表',
    width:800,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReagentSysService.svc/ST_UDTO_SearchCenOrgByHQL?isPlanish=true',
    /**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelCenOrg',
	
    /**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:true,
	/**是否启用修改按钮*/
	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel:true,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**默认加载数据*/
	defaultLoad:true,
	/**排序字段*/
	defaultOrderBy:[{property:'CenOrg_OrgNo',direction:'ASC'}],
	
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
    
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//初始化检索监听
		me.initFilterListeners();
		
		var CreateDatabaseButton = me.getComponent('buttonsToolbar').getComponent('CreateDatabaseButton');
		me.on({
			select:function(rowModel,record){
				var OrgTypeName = record.get('CenOrg_CenOrgType_CName');
				if(OrgTypeName == '实验室'){
					CreateDatabaseButton.enable();
				}else{
					CreateDatabaseButton.disable();
				}
			}
		});
	},
	initComponent:function(){
		var me = this;
		//查询框信息
		me.searchInfo = {
			width:180,emptyText:'中文名/英文名/代码/机构编号',isLike:true,itemId:'Search',
			fields:['cenorg.CName','cenorg.EName','cenorg.ShortCode','cenorg.OrgNo']
		};
			
		me.buttonToolbarItems = ['refresh','add','edit','del','-',{
			text:'创建数据库',
			itemId:'CreateDatabaseButton',
			iconCls:'button-config',
			tooltip:'创建实验室数据库',
			disabled:true,
			handler:function(){
				me.onCreateDataBase();
			}
		},'-',{
			fieldLabel: '机构类型',
			width: 180,
			labelWidth: 60,
			itemId: 'CenOrgTypeName',
			xtype: 'uxCheckTrigger',
			className:'Shell.class.rea.cenorg.type.CheckGrid'
		}, {
			fieldLabel:'机构类型主键ID',
			itemId: 'CenOrgTypeId',
			xtype: 'textfield',
			hidden: true
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
			dataIndex: 'CenOrg_OrgNo',
			text: '机构编号',
			width: 60,
			type:'int',
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_CName',
			text: '中文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_EName',
			text: '英文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_CenOrgType_CName',
			text: '机构类型',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_Memo',
			text: '备注',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_ServerIP',
			text: '服务器IP',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_ServerPort',
			text: '服务器端口',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_ShortCode',
			text: '机构代码',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_DispOrder',
			text: '次序',
			width: 50,
			align:'center',
			type:'int',
			defaultRenderer: true
		},{
			dataIndex: 'CenOrg_Visible',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true
		},{
			dataIndex: 'CenOrg_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		this.fireEvent('addclick');
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection();
			
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('editclick',me,records[0].get(me.PKField));
	},
	/**创建数据库*/
	onCreateDataBase:function(){
		var me = this,
			CreateDatabaseButton = me.getComponent('buttonsToolbar').getComponent('CreateDatabaseButton'),
			records = me.getSelectionModel().getSelection();
			
		if(records.length != 1){
			JShell.Msg.error('请选择一行记录进行操作');
			return;
		}
		var record = records[0];
		var OrgTypeName = record.get('CenOrg_CenOrgType_CName');
		if(OrgTypeName != '实验室'){
			CreateDatabaseButton.disable();
			JShell.Msg.error('只能给实验室创建数据库！');
			return;
		}
		
		JShell.Msg.confirm({
			msg:'是否确定创建实验室数据库？'
		},function(but){
			if (but != "ok") return;
			
			me.disableControl();//禁用所有的操作功能
		
			var params={
				ORGNO:record.get('CenOrg_OrgNo'),
				ORGNAME:record.get('CenOrg_CName')
			};
			
			var url='http://r.zhifang.com.cn:8808/AddDBlinkAndCreateDB';
			Ext.data.JsonP.request({
				url:url,
				timeout:300000,  
				params:params,  
				callbackKey:'jsonPCallback',  
				success:function(text){
					me.enableControl();//启用所有的操作功能
					if(text=='S'){
						JShell.Msg.alert('创建成功',null,1000);
					}else {
						JShell.Msg.error('错误信息: '+text);
					}
				},
				failure:function(result){
					me.enableControl();//启用所有的操作功能
					JShell.Msg.error('请求服务失败！');
				}
			});
		});
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			CenOrgTypeName = buttonsToolbar.getComponent('CenOrgTypeName'),
			CenOrgTypeId = buttonsToolbar.getComponent('CenOrgTypeId');

		CenOrgTypeName && CenOrgTypeName.on({
			check: function(p, record) {
				CenOrgTypeName.setValue(record ? record.get('CenOrgType_CName') : '');
				CenOrgTypeId.setValue(record ? record.get('CenOrgType_Id') : '');
				p.close();
			}
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			CenOrgTypeId = buttonsToolbar.getComponent('CenOrgTypeId'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];

		if (CenOrgTypeId && CenOrgTypeId.getValue()) {
			params.push('cenorg.CenOrgType.Id=' + CenOrgTypeId.getValue());
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
	}
});