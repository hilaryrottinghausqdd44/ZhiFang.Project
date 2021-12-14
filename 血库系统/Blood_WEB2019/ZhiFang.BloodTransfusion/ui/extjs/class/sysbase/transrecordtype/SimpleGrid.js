/**
 * 输血过程记录分类列表
 * @author longfc
 * @version 2020-02-11
 */
Ext.define('Shell.class.sysbase.transrecordtype.SimpleGrid', {
	extend: 'Shell.ux.grid.Panel',

	title: '输血过程记录分类列表',
	width: 225,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransRecordTypeByHQL?isPlanish=true',

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
	hasRownumberer: false,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**查询栏参数设置*/
	searchToolbarConfig: {},

	defaultOrderBy: [{
		property: 'BloodTransRecordType_DispOrder',
		direction: 'ASC'
	}],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 160,
			emptyText: '编码/名称',
			isLike: true,
			fields: ['bloodtransrecordtype.TypeCode', 'bloodtransrecordtype.CName']
		};

		//数据列
		me.columns = me.createGridColumns();

		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'BloodTransRecordType_ContentTypeID',
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
				}else if(value == "4") {
					v = "不良反应选择项";
					meta.style = "color:black;";
				}else if(value == "5") {
					v = "临床处理结果";
					meta.style = "color:black;";
				}else if(value == "6") {
					v = "临床处理结果描述";
					meta.style = "color:black;";
				}

				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			text: '编码',
			dataIndex: 'BloodTransRecordType_TypeCode',
			width: 100,
			sortable: false,
			hidden: true,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '名称',
			dataIndex: 'BloodTransRecordType_CName',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
			//		},{
			//			text:'次序',dataIndex:'BloodTransRecordType_DispOrder',width:100,
			//			defaultRenderer:true,align:'center',type:'int'
		}, {
			text: '主键ID',
			dataIndex: 'BloodTransRecordType_Id',
			isKey: true,
			hidden: true,
			menuDisabled: true,
			hideable: false
		}];

		return columns;
	}
});