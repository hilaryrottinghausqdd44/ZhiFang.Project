/**
 * 血袋接收:血袋接收信息
 * @author longfc
 * @version 2020-03-17
 */
Ext.define('Shell.class.blood.nursestation.bloodprohandover.handover.DtlGrid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '血袋接收信息',

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagOperationAndDtlOfHandoverByHQL?isPlanish=true',
	/**默认查询条件:1:领用;2:接收;3:回收;*/
	defaultWhere: "bloodbagoperation.BagOperTypeID=2",
	/**默认加载*/
	defaultLoad: false,
	//发血单号
	PK: null,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否启用刷新按钮*/
	hasRefresh: false,
	/**是否启用查询框*/
	hasSearch: false,

	/**排序字段*/
	defaultOrderBy: [{
		property: 'BloodBagOperation_Id',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'bloodprohandover.handover.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "血袋接收信息",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '血袋接收登记Id',
			dataIndex: 'BloodBagOperation_Id',
			width: 60,
			isKey: true,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '献血编号',
			dataIndex: 'BloodBagOperation_BBagCode',
			width: 135,
			defaultRenderer: true
		}, {
			text: '产品码',
			dataIndex: 'BloodBagOperation_PCode',
			width: 135,
			defaultRenderer: true
		},{
			text: '惟一码',
			dataIndex: 'BloodBagOperation_BloodBOutItem_B3Code',
			width: 135,
			hidden:true,
			defaultRenderer: true
		},{
			text: '血制品',
			dataIndex: 'BloodBagOperation_Bloodstyle_CName',
			width: 120,
			defaultRenderer: true
		}, {
			text: '接收科室',
			dataIndex: 'BloodBagOperation_DeptCName',
			width: 115,
			defaultRenderer: true
		}, {
			text: '接收人',
			dataIndex: 'BloodBagOperation_BagOper',
			width: 95,
			defaultRenderer: true
		}, {
			text: '接收时间',
			dataIndex: 'BloodBagOperation_BagOperTime',
			width: 135,
			isDate: true,
			hasTime: true,
			defaultRenderer: true
		}, {
			text: '运送人',
			dataIndex: 'BloodBagOperation_Carrier',
			width: 95,
			defaultRenderer: true
		}];
		columns.push({
			xtype: 'actioncolumn',
			text: '修改',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			items: [{
				getClass: function(v, meta, record) {
					var id = record.get("BloodBagOperation_Id");
					if (id && id.length > 0) {
						meta.tdAttr = 'data-qtip="<b>修改</b>"';
						return 'button-edit hand';
					} else {
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onEditBagOper(rec);
				}
			}]
		}, {
			text: '血袋外观',
			dataIndex: 'BloodBagOperation_BloodAppearance_BDict_CName',
			width: 90,
			defaultRenderer: true
		}, {
			text: '血袋完整性',
			dataIndex: 'BloodBagOperation_BloodIntegrity_BDict_CName',
			width: 90,
			flex: 1,
			defaultRenderer: true
		});
		return columns;
	},
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		if (!me.PK) {
			var error = me.errorFormat.replace(/{msg}/, "请选择发血单后再操作!");
			me.getView().update(error);
			return false;
		}

		me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var params = me.getInternalWhere();
		//内部条件
		if (params && params.length > 0) me.internalWhere = params.join(" and ");
		var url = me.callParent(arguments);
		return url;
	},
	getInternalWhere: function() {
		var me = this;
		var params = [];
		if (me.PK) {
			params.push("bloodbagoperation.BloodBOutForm='" + me.PK + "'");
		}
		return params;
	},
	onEditBagOper: function(rec) {
		var me = this;
		var bagOperationID = rec.get("BloodBagOperation_Id");
		var maxWidth = 360;//document.body.clientWidth;
		var height1 =240;// document.body.clientHeight * 0.45;
		var islayui=false;
		if(islayui){
			var src = JShell.System.Path.ROOT +
				"/ui/layui/views/bloodtransfusion/nursestation/bloodprohandover/index.html?BagOperationID=" + bagOperationID;
			var html = '<iframe frameborder=0 width="100%" height="100%" allowtransparency="true" scrolling=auto src="' + src +
				'"></iframe>';
			JShell.Win.open('Shell.ux.panel.AppPanel', {
				title: "接收登记修改",
				layout: 'fit', //设置布局模式为fit，能让frame自适应窗体大小
				resizable: true,
				width: maxWidth,
				height: height1,
				modal: true,
				border: 0,
				frame: false,
				html: html,
				listeners: {
					close: function() {
						me.onSearch();
					}
				}
			}).show();
		}else{
			var win=JShell.Win.open('Shell.class.blood.nursestation.bloodprohandover.edit.Form', {
				title: "接收登记修改",
				resizable: true,
				width: maxWidth,
				height: height1,
				modal: true,
				PK:bagOperationID,
				formtype:'edit',
				listeners: {
					save: function(p,id) {
						p.close();
						me.onSearch();
					},
					close: function() {
						me.onSearch();
					}
				}
			}).show();
			win.isEdit(bagOperationID);
		}
	}
});
