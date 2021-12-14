/**
 * 经销商列表
 * @author longfc
 * @version 2016-05-13
 */
Ext.define('Shell.class.pki.dealercontract.DealerGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '经销商列表',

	width: 343,

	/**获取数据服务路径*/
	selectUrl: '/BaseService.svc/ST_UDTO_SearchBDealerByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/BaseService.svc/ST_UDTO_DelBDealer',
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

	/**只读*/
	readOnly:false,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		if(!me.readOnly){
			me.on({
				itemdblclick: function(view, record) {
					me.onEditClick();
				}
			});
		}
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: '100%',
			emptyText: '经销商名称/编号',
			isLike: true,
			fields: ['bdealer.Name', 'bdealer.UseCode']
		};
		//自定义按钮功能栏
		me.buttonToolbarItems = [{
			type: 'search',
			info: me.searchInfo
		}];

		if(!me.readOnly){
			//创建挂靠功能栏
			me.dockedItems = [Ext.create('Shell.ux.toolbar.Button', {
				dock: 'bottom',
				items: ['add', 'edit', 'del', '-', 'import_excel']
			})];
			me.hasDel = true;
		}

		//数据列
		me.columns = [{
			dataIndex: 'BDealer_UseCode',
			text: '用户代码',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'BDealer_Name',
			text: '经销商名称',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BDealer_BBillingUnit_Name',
			text: '默认开票方',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'BDealer_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BDealer_BBillingUnit_Id',
			text: '开票方ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'BDealer_DataTimeStamp',
			text: '时间戳',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'BDealer_BBillingUnit_DataTimeStamp',
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
		me.openDealerForm(null,UseCode);
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
		me.openDealerForm(id,null);
	},
	/**打开表单*/
	openDealerForm: function(id,code) {
		var me = this;
		var config = {
			showSuccessInfo: false, //成功信息不显示
			resizable: false,
			formtype: 'add',
			UseCode:code,
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
		JShell.Win.open('Shell.class.pki.dealer.Form', config).show();
	},
	/**点击导入按钮处理*/
	onImportExcelClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.pki.excel.FileUpdatePanel', {
			formtype:'add',
			resizable: false,
			TableName: 'B_Dealer',
			ERROR_UNIQUE_KEY_INFO:'经销商代码有重复',
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
		url += "?EntityName=BDealer&FieldName=UseCode";
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