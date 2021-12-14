/**
 * 记录项字典选择
 * 适合左列表及右列表都为相同的实体对象(ReaGoodsOrgLink),并且右列表不需要默认加载及过滤原已选择的供货商货品
 * 右列表不调用服务获取后台数据
 * @author longfc
 * @version 2020-02-25
 */
Ext.define('Shell.class.sysbase.screcorditemlink.choose.LeftGrid', {
	extend: 'Shell.class.assist.basic.CheckPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '待选列表',
	width: 530,
	height: 620,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**是否带确认按钮*/
	hasAcceptButton: false,
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordTypeItemByLinkHQL?isPlanish=true',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'SCRecordTypeItem_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'screcordtypeitem.choose.LeftGrid',
	/**用户UI配置Name*/
	userUIName: "待选列表",
	/**关系表查询条件*/
	linkWhere: "",
	RecordTypeID: '',
	
	initComponent: function() {
		var me = this;
		me.addEvents('onBeforeSearch');
		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '名称',
			isLike: true,
			fields: ['screcordtypeitem.CName']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '编码',
			dataIndex: 'SCRecordTypeItem_Id',
			isKey: true,
			width: 90,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '名称',
			dataIndex: 'SCRecordTypeItem_CName',
			width: 100,
			flex:1,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '简称',
			dataIndex: 'SCRecordTypeItem_SName',
			width: 60,
			hidden:true,
			menuDisabled: true,
			defaultRenderer: true
		},{
			text: '次序',
			dataIndex: 'SCRecordTypeItem_DispOrder',
			width: 70,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}];

		return columns;
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
		
		me.buttonToolbarItems.push('->', {
			iconCls: 'button-check',
			text: '全部选择',
			tooltip: '将当前页全部选择',
			handler: function() {
				me.onAcceptClick();
			}
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.getInternalWhere();
		var url = me.callParent(arguments);
		if (me.linkWhere) url += "&linkWhere=" + me.linkWhere;
		return url;
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this,
			where = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = buttonsToolbar.getComponent('Search');
		if(search) {
			var value = search.getValue();
			var searchHql = "";
			if(value)searchHql = me.getSearchWhere(value);
			if(searchHql) {
				searchHql = "(" + searchHql + ")";
				where.push(searchHql);
			}
		}
		me.internalWhere = where.join(" and ");
	},
	/**获取外部传入的外部查询条件*/
	setExternalWhere: function(externalWhere) {
		var me = this;
		me.externalWhere = externalWhere;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		me.fireEvent('onBeforeSearch', me);
		//this.load(null, true, autoSelect);
		return me.callParent(arguments);
	},
	/**确定按钮处理*/
	onAcceptClick: function() {
		var me = this;
		var records = [];
		me.store.each(function(rec) {
			records.push(rec);
		});
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.fireEvent('onAccept', me, records);
	}
});