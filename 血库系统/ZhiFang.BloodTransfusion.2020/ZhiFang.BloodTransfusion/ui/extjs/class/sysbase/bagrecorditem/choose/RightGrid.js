/**
 * 血袋记录项字典选择
 * @author longfc
 * @version 2020-02-25
 */
Ext.define('Shell.class.sysbase.bagrecorditem.choose.RightGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '已选择列表',
	width: 530,
	height: 620,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**是否带确认按钮*/
	hasAcceptButton: true,
	/**获取数据服务路径/BloodTransfusionManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?isPlanish=true*/
	selectUrl: '',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodBagRecordItem_DispOrder',
		direction: 'ASC'
	}],
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**后台排序*/
	remoteSort: false,
	/**是否调用服务从后台获取数据*/
	remoteLoad: false,
	/**查询框信息*/
	searchInfo: null,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**用户UI配置Key*/
	userUIKey: 'bagrecorditem.choose.RightGrid',
	/**用户UI配置Name*/
	userUIName: "当前已选择列表",
	
	//closable:true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(grid, record, item, index, e, eOpts) {
				me.onRemoveClick([record]);
			}
		});
	},
	initComponent: function() {
		var me = this;
		
		//me.addEvents('onAccept', 'onRemove', 'onRefresh');
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
			dataIndex: 'BloodBagRecordItem_BloodBagRecordType_ContentTypeID',
			text: '内容分类',
			width: 120,
			hidden:true,
			renderer: function(value, meta) {
				var v = "";
				if(value == "1") {
					v = "输血记录项";
					meta.style = "color:green;";
				} else if(value == "2") {
					v = "不良反应分类";
					meta.style = "color:orange;";
				} else if(value == "3") {
					v = "临床处理措施";
					meta.style = "color:black;";
				}else if(value == "4") {
					v = "不良反应选择项";
					meta.style = "color:black;";
				}else if(value == "5") {
					v = "临床处理结果";
					meta.style = "color:black;";
				}else if(value == "6") {
					v = "临床处理结果描述";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text: '名称',
			dataIndex: 'BloodBagRecordItem_CName',
			width: 100,
			flex:1,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '简称',
			dataIndex: 'BloodBagRecordItem_SName',
			width: 60,
			hidden:true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '次序',
			dataIndex: 'BloodBagRecordItem_DispOrder',
			width: 70,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}, {
			text: '输血过程记录项分类ID',
			dataIndex: 'BloodBagRecordItem_BloodBagRecordType_Id',
			width: 60,
			hidden:true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '输血过程记录项分类名称',
			dataIndex: 'BloodBagRecordItem_BloodBagRecordType_CName',
			width: 60,
			hidden:true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '主键ID',
			dataIndex: 'BloodBagRecordItem_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [];//'->'
		items.push({
			xtype: 'button',
			iconCls: 'button-del',
			text: '选择移除',
			hidden: true,
			tooltip: '移除列表选择的行',
			handler: function() {
				var records = me.getSelectionModel().getSelection();
				me.onRemoveClick(records);
			}
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-del',
			text: '全部移除',
			tooltip: '移除列表选择的行',
			handler: function() {
				var records = [];
				me.store.each(function(rec) {
					records.push(rec);
				});
				me.onRemoveClick(records);
			}
		});
		items.push('-', {
			iconCls: 'button-check',
			text: '确定选择',
			tooltip: '确定当前选择并退出',
			handler: function() {
				me.onAcceptClick();
			}
		});

		return items;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		//是否调用服务从后台获取数据
		if(!me.remoteLoad) return false;

		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件

		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**确定按钮处理*/
	onAcceptClick: function() {
		var me = this;
		var records = [];
		me.store.each(function(rec) {
			records.push(rec);
		});
		
		me.fireEvent('onAccept', me, records);
	},
	onRemoveClick: function(records) {
		var me = this;
		if(!records) records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.fireEvent('onRemove', me, records);
	},
	onRefreshClick: function() {
		var me = this;
		me.fireEvent('onRefresh', me);
	}
});