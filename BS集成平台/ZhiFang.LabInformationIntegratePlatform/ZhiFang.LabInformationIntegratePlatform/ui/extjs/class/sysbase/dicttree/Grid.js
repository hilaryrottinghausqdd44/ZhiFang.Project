/**
 * 类型树列表
 * @author 
 * @version 2016-06-22
 */
Ext.define('Shell.class.sysbase.dicttree.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '类型树列表 ',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/SingleTableService.svc/UDTO_SearchBDictTreeByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ServerWCF/SingleTableService.svc/UDTO_UpdateBDictTreeByField',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/SingleTableService.svc/UDTO_DelBDictTree',

	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,

	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize: 50,

	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
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
	/**对外公开:允许外部调用应用时传入树节点值(如IDS=123,232)*/
	IDS: "",
	/**获取树的最大层级数*/
	LEVEL: "",
	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'BDictTree_DispOrder',
		direction: 'ASC'
	}],

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
			emptyText: '机构名称',
			isLike: true,
			fields: ['bdicttree.CName']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: 'GUID值',
			dataIndex: 'BDictTree_Id',
			width: 180,
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '名称',
			dataIndex: 'BDictTree_CName',
			width: 150,
			defaultRenderer: true
		}, {
			text: '简称',
			dataIndex: 'BDictTree_SName',
			width: 90,
			defaultRenderer: true
		}, {
			text: '快捷码',
			dataIndex: 'BDictTree_Shortcode',
			width: 90,
			defaultRenderer: false
		}, {
			xtype: 'checkcolumn',
			text: '是否使用',
			dataIndex: 'BDictTree_IsUse',
			width: 60,
			align: 'center',
			sortable: false,
			menuDisabled: true,
			stopSelection: false,
			type: 'boolean'
		}, {
			text: '备注',
			dataIndex: 'BDictTree_Memo',
			width: 120,
			defaultRenderer: true
		}, {
			text: '次序',
			dataIndex: 'BDictTree_DispOrder',
			width: 60,
			defaultRenderer: true,
			type: 'int'
		}, {
			text: '时间戳',
			dataIndex: 'BDictTree_DataTimeStamp',
			hidden: true,
			hideable: false
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
			var IsUse = rec.get('BDictTree_IsUse');
			me.updateOneByIsUse(i, id, IsUse);
		}
	},
	updateOneByIsUse: function(index, id, IsUse) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		var params = Ext.JSON.encode({
			entity: {
				Id: id,
				IsUse: IsUse
			},
			fields: 'Id,IsUse'
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
		var me = this;
		me.openOrgForm();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(!records || records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		var id = records[0].get(me.PKField);
		me.openOrgForm(id);
	},
	/**打开表单*/
	openOrgForm: function(id) {
		var me = this;
		var config = {
			ParentID: me.OrgId, //上级机构ID
			ParentName: me.OrgName, //上级机构名称
			showSuccessInfo: false,
			resizable: false,
			formtype: 'add',
			IDS: me.IDS,
			/**获取树的最大层级数*/
			LEVEL: me.LEVEL,
			treeShortcodeWhere: me.treeShortcodeWhere,
			listeners: {
				save: function(win) {
					me.onSearch();
					win.close();
				}
			}
		};
		if(id) {
			config.formtype = 'edit';
			config.PK = id;
		}
		JShell.Win.open('Shell.class.sysbase.dicttree.Form', config).show();
	},
	/**根据上级机构ID加载数据*/
	loadByParentId: function(id, name) {
		var me = this;
		me.OrgId = id;
		me.OrgName = name;
		me.defaultWhere = 'bdicttree.ParentID=' + id;
		me.onSearch();
	}
});