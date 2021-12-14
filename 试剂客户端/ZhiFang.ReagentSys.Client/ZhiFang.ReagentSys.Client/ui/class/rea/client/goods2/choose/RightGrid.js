/**
 * 机构货品选择
 * 适合左列表及右列表都为相同的实体对象(ReaGoodsOrgLink),并且右列表不需要默认加载及过滤原已选择的供货商货品
 * 右列表不调用服务获取后台数据
 * @author longfc
 * @version 2018-11-14
 */
Ext.define('Shell.class.rea.client.goods2.choose.RightGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '当前选择货品列表',
	width: 530,
	height: 620,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**是否带确认按钮*/
	hasAcceptButton: true,
	/**获取数据服务路径/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?isPlanish=true*/
	selectUrl: '',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaGoods_DispOrder',
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
	userUIKey: 'goods2.choose.RightGrid',
	/**用户UI配置Name*/
	userUIName: "机构货品当前选择货品列表",
	
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
		
		me.addEvents('onAccept', 'onRemove', 'onRefresh');
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
			dataIndex: 'ReaGoods_BarCodeMgr',
			text: '条码类型',
			hidden: true,
			width: 50,
			renderer: function(value, meta) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoods_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_CName',
			text: '货品名称',
			width: 160,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaGoods_BarCodeMgr");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"')>=0)value=value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoods_UnitName',
			text: '单位',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '规格',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_GoodsClass',
			text: '一级分类',
			hidden: false,
			width: 70,
			defaultRenderer: true
		}, {
			text: '二级分类',
			dataIndex: 'ReaGoods_GoodsClassType',
			width: 70,
			hidden: false,
			defaultRenderer: true
		}, {
			text: '所属部门',
			dataIndex: 'ReaGoods_DeptName',
			width: 80,
			hidden: false,
			defaultRenderer: true
		}, {
			text: '仪器',
			dataIndex: 'ReaGoods_SuitableType',
			width: 70,
			hidden: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true,
			defaultRenderer: true
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