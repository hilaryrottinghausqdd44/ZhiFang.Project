/**
 * 输血过程记录项字典管理
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.transrecordtypeitem.Grid', {
	extend: 'Shell.class.blood.basic.GridPanel',
	requires: ['Ext.ux.CheckColumn'],

	title: '字典列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransRecordTypeItemByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransRecordTypeItemByField',
	/**删除数据服务路径*/
	delUrl: '/BloodTransfusionManageService.svc/BT_UDTO_DelBloodTransRecordTypeItem',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: false,
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

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	//	/**是否启用修改按钮*/
	//	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'BloodTransRecordTypeItem_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'transrecordtypeitem.Grid',
	/**用户UI配置Name*/
	userUIName: "字典列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '名称',
			isLike: true,
			fields: ['bloodtransrecordtypeitem.CName']
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
		var columns = [ {
			text: '记录项编码',
			dataIndex: 'BloodTransRecordTypeItem_Id',
			isKey: true,
			hideable: false
		},{
			dataIndex: 'BloodTransRecordTypeItem_BloodTransRecordType_ContentTypeID',
			text: '内容分类',
			width: 120,
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
				} else if(value == "4") {
					v = "不良反应选择项";
					meta.style = "color:black;";
				} else if(value == "5") {
					v = "临床处理结果";
					meta.style = "color:black;";
				} else if(value == "6") {
					v = "临床处理结果描述";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '名称',
			dataIndex: 'BloodTransRecordTypeItem_CName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '简称',
			dataIndex: 'BloodTransRecordTypeItem_SName',
			width: 60,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			xtype: 'checkcolumn',
			text: '使用',
			dataIndex: 'BloodTransRecordTypeItem_IsVisible',
			width: 40,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '创建时间',
			dataIndex: 'BloodTransRecordTypeItem_DataAddTime',
			width: 130,
			hidden: true,
			isDate: true,
			hasTime: true
		}, {
			text: '次序',
			dataIndex: 'BloodTransRecordTypeItem_DispOrder',
			width: 70,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}];

		return columns;
	},
	onSaveClick: function() {
		var me = this,
			records = me.store.getModifiedRecords(), //获取修改过的行记录
			len = records.length;

		if(len == 0) return;

		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;

		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			var IsVisible = rec.get('BloodTransRecordTypeItem_IsVisible');
			me.updateOneByIsVisible(i, id, IsVisible);
		}
	},
	updateOneByIsVisible: function(index, id, IsVisible) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;

		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				IsVisible: IsVisible
			},
			fields: 'Id,IsVisible'
		});

		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if(record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		this.fireEvent('addclick', this);
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var len = data.list.length;
		for(var i = 0; i < len; i++) {
			var isVisible = data.list[i].BloodTransRecordTypeItem_IsVisible;
			if(isVisible == 'True' || isVisible == 'true' || isVisible == '1' || isVisible == 1) {
				isVisible = true;
			} else {
				isVisible = false;
			}
			data.list[i].BloodTransRecordTypeItem_IsVisible = isVisible;
		}
		return data;
	}
});