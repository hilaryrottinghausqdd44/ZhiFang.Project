/**
 * 销售列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.pki.seller.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '销售列表',

	width: 345,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchBSellerByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/BaseService.svc/ST_UDTO_DelBSeller',
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**默认每页数量*/
	defaultPageSize: 500,
	/**是否启用序号列*/
	hasRownumberer: false,
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
		//查询框信息
		me.searchInfo = {
			width: '100%',
			emptyText: '销售名称/代码',
			isLike: true,
			fields: ['bseller.Name', 'bseller.UseCode']
		};
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			type: 'search',
			info: me.searchInfo
		}];
		//创建挂靠功能栏
		me.dockedItems = [Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			items: ['add', 'edit', 'del', '-', 'import_excel']
		})];
		//数据列
		me.columns = [{
			dataIndex: 'BSeller_UseCode',
			text: '用户代码',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BSeller_Name',
			text: '销售名称',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BSeller_AreaIn',
			text: '区域',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BSeller_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BSeller_DataTimeStamp',
			text: '时间戳',
			hidden: true,
			hideable: false
		}];

		me.callParent(arguments);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.openSellerForm();
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
		me.openSellerForm(id);
	},
	/**打开表单*/
	openSellerForm: function(id) {
		var me = this;
		var config = {
			showSuccessInfo: false, //成功信息不显示
			resizable: false,
			formtype: 'add',
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
		JShell.Win.open('Shell.class.pki.seller.Form', config).show();
	},
	/**点击导入按钮处理*/
	onImportExcelClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.pki.excel.FileUpdatePanel', {
			formtype:'add',
			resizable: false,
			TableName: 'B_Seller',
			ERROR_UNIQUE_KEY_INFO:'销售代码有重复',
			listeners: {
				save: function() {
					me.onSearch();
				}
			}
		}).show();
	}
});