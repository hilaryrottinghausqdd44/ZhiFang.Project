/**
 * 输血检验项目维护
 * @author longfc
 * @version 2020-04-08
 */
Ext.define('Shell.class.sysbase.bloodbtestitem.Grid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: ['Ext.ux.CheckColumn'],

	title: '输血检验项目列表 ',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBTestItemByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBTestItemByField',
	/**删除数据服务路径*/
	delUrl: '/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBTestItem ',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize: 100,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: true,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'BloodBTestItem_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'bloodbtestitem.Grid',
	/**用户UI配置Name*/
	userUIName: "输血检验项目列表",
	
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
			width: 220,
			emptyText: '名称/简称',
			isLike: true,
			fields: ['bloodbtestitem.CName', 'bloodbtestitem.SName']
		};		
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var me = this;
		var columns = [{
			text: '项目编码',
			dataIndex: 'BloodBTestItem_Id',
			isKey: true,
			width: 100
		},{
			text: '名称',
			dataIndex: 'BloodBTestItem_CName',
			width: 150,
			defaultRenderer: true
		}, {
			text: '简称',
			dataIndex: 'BloodBTestItem_SName',
			width: 90,
			defaultRenderer: true
		},  {
			text: '拼音字头',
			dataIndex: 'BloodBTestItem_PinYinZiTou',
			width: 90,
			defaultRenderer: true
		},{
			text: '快捷码',
			dataIndex: 'BloodBTestItem_ShortCode',
			width: 90,
			defaultRenderer: true
		}, {
			text: 'HisDispCode',
			dataIndex: 'BloodBTestItem_HisDispCode',
			width: 90,
			defaultRenderer: true
		},{
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'BloodBTestItem_Visible',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			xtype: 'checkcolumn',
			text: '医嘱结果录入项',
			dataIndex: 'BloodBTestItem_IsResultItem',
			width: 130,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			xtype: 'checkcolumn',
			text: '为输血前评估项',
			dataIndex: 'BloodBTestItem_IsPreTrransfusionEvaluationItem',
			width: 130,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '显示次序',
			dataIndex: 'BloodBTestItem_DispOrder',
			width: 60,
			defaultRenderer: true,
			type: 'int'
		}];

		return columns;
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if (len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for (var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			
			me.updateOne(i, id, rec);
		}
	},
	updateOne: function(index, id, rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var visible = rec.get('BloodBTestItem_Visible');
		var isResultItem = rec.get('BloodBTestItem_IsResultItem');
		var isPreTrransfusionEvaluationItem = rec.get('BloodBTestItem_IsPreTrransfusionEvaluationItem');
		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				Visible: visible,
				IsResultItem: isResultItem,
				IsPreTrransfusionEvaluationItem: isPreTrransfusionEvaluationItem
			},
			fields: 'Id,Visible,IsResultItem,IsPreTrransfusionEvaluationItem'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		var me = this;
		me.fireEvent('addclick', me);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if (!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		me.fireEvent('editclick', me, records[0]);
	}
});
