/**
 * 开票方列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.billingunit.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '开票方列表',
	width: 360,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchBBillingUnitByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/BaseService.svc/ST_UDTO_DelBBillingUnit',
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			itemdblclick: function(view, record) {
				me.onEditClick();
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('accept');
		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '开票方名称/编号',
			isLike: true,
			fields: ['bbillingunit.Name', 'bbillingunit.UseCode']
		};
		//自定义按钮功能栏
		me.buttonToolbarItems = ['add', 'edit', 'del', '-', 'import_excel', '->', {
			type: 'search',
			info: me.searchInfo
		}];
		//数据列
		me.columns = [{
			dataIndex: 'BBillingUnit_UseCode',
			text: '用户代码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BBillingUnit_Name',
			text: '开票方名称',
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'BBillingUnit_BillingUnitType',
			text: '开票方类型',
			width: 70,
			renderer: function(value, meta) {
				var v = JShell.PKI.Enum.UnitType['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.PKI.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'BBillingUnit_PaymentTerm',
			text: '付款期(天)',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'BBillingUnit_CashDeposit',
			text: '保证金(元)',
			width: 70,
			defaultRenderer: true
		}, {
			dataIndex: 'BBillingUnit_Address',
			text: '地址',
			width: 200,
			defaultRenderer: true
		}, {
			dataIndex: 'BBillingUnit_IsUse',
			text: '使用',
			width: 40,
			isBool: true,
			align: 'center',
			type: 'bool'
		}, {
			dataIndex: 'BBillingUnit_Id',
			text: '开票方ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BBillingUnit_DataTimeStamp',
			text: '开票方时间戳',
			hidden: true,
			hideable: false
		}];

		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		var UseCode = me.getUseCode();
		me.openBillingUnitForm(null, UseCode);
	},
   
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var id = records[0].get(me.PKField);
		me.openBillingUnitForm(id, null);
	},
	/**打开表单*/
	openBillingUnitForm: function(id, code) {
		var me = this;
		var config = {
			showSuccessInfo: false, //成功信息不显示
			resizable: false,
			formtype: 'add',
			UseCode: code,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		if (id) {
			config.formtype = 'edit';
			config.PK = id;
		}
		JShell.Win.open('Shell.class.pki.billingunit.Form', config).show();
	},
	/**点击导入按钮处理*/
	onImportExcelClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.pki.excel.FileUpdatePanel', {
			formtype: 'add',
			resizable: false,
			TableName: 'B_BillingUnit',
			ERROR_UNIQUE_KEY_INFO: '开票方代码有重复',
			listeners: {
				save: function() {
					me.onSearch();
				}
			}
		}).show();
	},
	 /**获取用户代码方法*/
	getUseCode: function() {
		var me = this;
		var UseCode = '';
		var UseCodeUrl = '/StatService.svc/Stat_UDTO_GetMaxNoByEntityName';
		var url = (UseCodeUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + UseCodeUrl;
		url += "?EntityName=BBillingUnit&FieldName=UseCode";
//		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.get(url, function(data) {
//			me.hideMask(); //隐藏遮罩层
			if (data.success) {
				UseCode = data.value;
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
		return UseCode;
	}
});