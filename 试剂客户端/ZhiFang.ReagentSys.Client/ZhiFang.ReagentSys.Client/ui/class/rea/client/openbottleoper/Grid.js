/**
 * 开瓶管理-开瓶管理
 * @author longfc	
 * @version 2021-01-25
 */
Ext.define('Shell.class.rea.client.openbottleoper.Grid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.picker.DateTime',
		'Shell.ux.form.field.DateTime',
	],

	title: '开瓶管理',
	width: 205,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaOpenBottleOperDocByHQL?isPlanish=true',
	/**更新数据服务路径*/
	editUrl: '/ReaManageService.svc/ST_UDTO_UpdateReaOpenBottleOperDocByField',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize: 50,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: false,
	
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: false,

	defaultOrderBy: [{
		property: 'ReaOpenBottleOperDoc_BOpenDate',
		direction: 'DESC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//me.defaultWhere = "reaopenbottleoperdoc.IsUseCompleteFlag=0";
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
				text: '供货商',
				dataIndex: 'ReaOpenBottleOperDoc_ReaBmsQtyDtl_CompanyName',
				width: 90,
				hidden:true
			},{
				text: '货品名称',
				dataIndex: 'ReaOpenBottleOperDoc_ReaGoods_CName',
				width: 90
			},{
				text: '货品批号',
				dataIndex: 'ReaOpenBottleOperDoc_ReaBmsQtyDtl_LotNo',
				width: 80
			},{
				text: '开瓶时间',
				dataIndex: 'ReaOpenBottleOperDoc_BOpenDate',
				width: 130,
				isDate: true,
				hasTime: true
			}, {
				text: '开瓶后有效期',
				dataIndex: 'ReaOpenBottleOperDoc_InvalidBOpenDate',
				width: 130,
				isDate: true,
				hasTime: true
			},
			{
				xtype: 'checkcolumn',
				text: '是否使用完',
				dataIndex: 'ReaOpenBottleOperDoc_IsUseCompleteFlag',
				width: 80,
				align: 'center',
				stopSelection: false,
				type: 'boolean'
			},
			{
				text: '使用完时间',
				dataIndex: 'ReaOpenBottleOperDoc_UseCompleteDate',
				width: 130,
				isDate: true,
				hasTime: true
			},
			{
				xtype: 'checkcolumn',
				text: '是否作废',
				dataIndex: 'ReaOpenBottleOperDoc_IsObsolete',
				width: 70,
				align: 'center',
				stopSelection: false,
				type: 'boolean'
			},
			{
				text: '作废时间',
				dataIndex: 'ReaOpenBottleOperDoc_ObsoleteTime',
				width: 130,
				isDate: true,
				hasTime: true
			},
			{
				text: '出库总单ID',
				dataIndex: 'ReaOpenBottleOperDoc_OutDocID',
				hidden: true
			},
			{
				text: '出库明细单ID',
				dataIndex: 'ReaOpenBottleOperDoc_OutDtlID',
				hidden: true
			},
			{
				text: '库存ID',
				dataIndex: 'ReaOpenBottleOperDoc_QtyDtlID',
				hidden: true
			},
			{
				text: '货品ID',
				dataIndex: 'ReaOpenBottleOperDoc_GoodsID',
				hidden: true
			}, {
				text: '主键ID',
				dataIndex: 'ReaOpenBottleOperDoc_Id',
				isKey: true,
				hidden: true,
				hideable: false
			}
		];

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

		var entity = {
			Id: id,
			IsUseCompleteFlag: rec.get("ReaOpenBottleOperDoc_IsUseCompleteFlag") ? 1 : 0,
			IsObsolete: rec.get("ReaOpenBottleOperDoc_IsObsolete") ? 1 : 0
		};
		if (rec.get("ReaOpenBottleOperDoc_OutDocID")) entity.OutDocID = rec.get("ReaOpenBottleOperDoc_OutDocID");
		if (rec.get("ReaOpenBottleOperDoc_OutDtlID")) entity.OutDtlID = rec.get("ReaOpenBottleOperDoc_OutDtlID");
		if (rec.get("ReaOpenBottleOperDoc_QtyDtlID")) entity.QtyDtlID = rec.get("ReaOpenBottleOperDoc_QtyDtlID");
		if (rec.get("ReaOpenBottleOperDoc_GoodsID")) entity.GoodsID = rec.get("ReaOpenBottleOperDoc_GoodsID");

		var params = Ext.JSON.encode({
			entity: entity,
			fields: 'Id,IsUseCompleteFlag,IsObsolete'
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
});
