/**
 * 预警设置
 * @author liangyl	
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.alertInfo.Grid',{
    extend:'Shell.class.rea.client.basic.GridPanel',
	requires: [
	    'Shell.ux.form.field.SimpleComboBox',
	    'Ext.ux.CheckColumn'
	],
	
	title: '预警列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaAlertInfoSettingsByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaAlertInfoSettings',
	/**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaAlertInfoSettingsByField',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**默认加载数据*/
	defaultLoad: true,
    /**预警分类类型Key*/
	AlertType: "AlertType",
    /**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
		/**默认每页数量*/
	defaultPageSize: 500,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**用户UI配置Key*/
	userUIKey: 'alertInfo.Grid',
	/**用户UI配置Name*/
	userUIName: "预警列表",
	
	initComponent: function() {
		var me = this;
		JShell.REA.StatusList.getStatusList(me.AlertType, false, false, null);
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaAlertInfoSettings_AlertTypeCName',
			text: '预警分类',width: 150,defaultRenderer: true
		}, {
			dataIndex: 'ReaAlertInfoSettings_AlertColor',
			text: '预警颜色',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaAlertInfoSettings_StoreLower',
			text: '下限值',width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaAlertInfoSettings_StoreUpper',
			text: '上限值',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'ReaAlertInfoSettings_Visible',
			text: '启用',width: 50,align:'center',
			type:'bool',isBool:true,defaultRenderer: true
		},{
			dataIndex: 'ReaAlertInfoSettings_DispOrder',
			text: '显示次序',width: 65,defaultRenderer: true
		},{
			dataIndex: 'ReaAlertInfoSettings_Memo',
			text: '备注',flex: 1,defaultRenderer: true
		},{
			dataIndex: 'ReaAlertInfoSettings_Id',text: '主键ID',
			hidden: true,hideable: false,isKey: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaAlertInfoSettings_AlertTypeId',hidden:true,
			text: '类型id',width: 100,defaultRenderer: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var dataList=JShell.REA.StatusList.Status[me.AlertType].List;
		dataList.splice(0,0,['','全部','font-weight:bold;text-align:center;color:#121212']);
		var items = ['refresh','-',{
			fieldLabel: '预警分类',xtype: 'uxSimpleComboBox',
			name: 'ReaAlertInfoSettings_AlertTypeId ',
			itemId: 'ReaAlertInfoSettings_AlertTypeId',
			labelWidth: 60,labelAlign: 'right',hasStyle: true,
			data: dataList,value:'',
			width: 200,
			listeners:{
				change:function(com,newValue,oldValue,eOpts ){
					me.onSearch();
				}
			}
		},'-','add','del'];
		return items;
	},
	 /**@overwrite 新增按钮点击处理方法*/
    onAddClick: function () {
        this.fireEvent('addclick', this);
    },
    /**@overwrite 编辑按钮点击处理方法*/
    onEditClick: function () {
        this.fireEvent('editclick', this);
    },
     /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,params = [];
		var buttonsToolbar = me.getComponent("buttonsToolbar");
        if(!buttonsToolbar)return;
        var AlertTypeId= buttonsToolbar.getComponent("ReaAlertInfoSettings_AlertTypeId").getValue();
		me.internalWhere = '';

		if(AlertTypeId){
			params.push('reaalertinfosettings.AlertTypeId='+AlertTypeId);
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
				me.internalWhere = "("+me.getSearchWhere(search)+")";
			}
		}
		return me.callParent(arguments);
	}
});