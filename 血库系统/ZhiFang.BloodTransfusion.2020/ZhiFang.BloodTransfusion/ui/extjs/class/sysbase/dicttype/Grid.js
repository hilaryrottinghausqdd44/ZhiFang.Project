/**
 * 字典类型列表
 * @author longfc
 * @version 2020-07-10
 */
Ext.define('Shell.class.sysbase.dicttype.Grid', {
	extend:"Shell.ux.grid.Panel",
	//extend: 'Shell.class.blood.basic.GridPanel',
	requires: ['Ext.ux.CheckColumn'],

	title: '字典类型列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDictTypeByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBDictTypeByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/SingleTableService.svc/ST_UDTO_DelBDictType',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: true,
	/**默认每页数量*/
	defaultPageSize: 50,

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

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	//	/**是否启用修改按钮*/
	//	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'BDictType_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'dicttype.Grid',
	/**用户UI配置Name*/
	userUIName: "字典类型列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '编码/名称',
			isLike: true,
			fields: ['bdicttype.DictTypeCode', 'bdicttype.CName']
		};

		//数据列
		me.columns = me.createGridColumns();
		//me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var me = this;
		var columns = [{
			text: '类型编号',
			dataIndex: 'BDictType_Id',
			isKey: true,
			width: 100,
			hideable: false
		},{
			text: '编码',
			dataIndex: 'BDictType_DictTypeCode',
			width: 160,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '名称',
			dataIndex: 'BDictType_CName',
			width: 100,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '快捷码',
			dataIndex: 'BDictType_ShortCode',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '业务编码',
			dataIndex: 'BDictType_UseCode',
			width: 80,
			hidden:true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '标准代码',
			dataIndex: 'BDictType_StandCode',
			width: 80,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '开发商代码',
			dataIndex: 'BDictType_DeveCode',
			width: 120,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '<b style="color:blue;">使用</b>',
			xtype: 'checkcolumn',
			dataIndex: 'BDictType_IsUse',
			width: 40,
			align: 'center',
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		},{
			text: '<b style="color:blue;">显示次序</b>',
			dataIndex: 'BDictType_DispOrder',
			width: 100,
			defaultRenderer: true,
			align: 'center',
			type: 'int',
			editor: {
				xtype: 'numberfield'
			}
		} ,{
			text: '创建时间',
			dataIndex: 'BDictType_DataAddTime',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			text: '备注',
			dataIndex: 'BDictType_Memo',
			width: 200,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
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
			me.updateOne(i, id, rec);
		}
	},
	updateOne: function(index,  rec) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var id = rec.get(me.PKField);
		var DispOrder = rec.get('BDictType_DispOrder');
		var IsUse = rec.get('BDictType_IsUse');
		var empID = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var empName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if (!empID) empID = -1;
		if (!empName) empName = "";
		var params = Ext.JSON.encode({
			empID:empID,
			empName:empName,
			entity: {
				Id: id,
				IsUse: IsUse,
				DispOrder: DispOrder
			},
			fields: 'Id,IsUse,DispOrder'
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
		this.fireEvent('addclick', this);
	}
});
