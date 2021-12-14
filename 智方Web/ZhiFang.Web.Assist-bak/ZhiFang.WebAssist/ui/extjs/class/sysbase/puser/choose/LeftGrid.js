/**
 * 人员选择选择
 * @author longfc
 * @version 2020-03-26
 */
Ext.define('Shell.class.sysbase.puser.choose.LeftGrid', {
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
	selectUrl: '/ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchPUserByHQL?isPlanish=true',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'PUser_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'puser.choose.LeftGrid',
	/**用户UI配置Name*/
	userUIName: "待选列表",

	initComponent: function() {
		var me = this;
		me.addEvents('onBeforeSearch');
		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '帐号/名称',
			itemId:"Search",
			isLike: true,
			fields: ['puser.ShortCode', 'puser.CName']
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
			dataIndex: 'PUser_Id',
			isKey: true,
			width: 100,
			defaultRenderer: true
		}, {
			text: '帐号',
			dataIndex: 'PUser_ShortCode',
			width: 100,
			defaultRenderer: true
		}, {
			text: '名称',
			dataIndex: 'PUser_CName',
			width: 100,
			defaultRenderer: true
		}, {
			text: '身份类型',
			dataIndex: 'PUser_Usertype',
			width: 65,
			defaultRenderer: true
		}, {
			text: 'Code1',
			dataIndex: 'PUser_Code1',
			width: 95,
			defaultRenderer: true
		}, {
			text: 'Code2',
			dataIndex: 'PUser_Code2',
			width: 95,
			hidden: true,
			defaultRenderer: true
		}, {
			text: 'Code3',
			dataIndex: 'PUser_Code3',
			width: 95,
			hidden: true,
			defaultRenderer: true
		}, {
			text: '次序',
			dataIndex: 'PUser_DispOrder',
			width: 100,
			defaultRenderer: true,
			align: 'center',
			type: 'int'
		}];

		return columns;
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
		me.buttonToolbarItems.push({
			fieldLabel: '', //身份类型
			labelWidth: 0,
			width: 100,
			name: 'Usertype',
			itemId: 'Usertype',
			xtype: 'uxSimpleComboBox',
			value: '',
			hasStyle: true,
			data: [
				['', '全部', 'color:black;'],
				['检验技师', '检验技师', 'color:green;'],
				['医生', '医生', 'color:black;'],
				['护士', '护士', 'color:black;'],
				['护工', '护工', 'color:black;']
			],
			listeners: {
				change: function(p, newV, oldV, e) {
					me.onSearch();
				}
			}
		}, {
			fieldLabel: '', //是否绑定
			labelWidth: 0,
			width: 95,
			name: 'IsBindDoctor',
			itemId: 'IsBindDoctor',
			xtype: 'uxSimpleComboBox',
			value: '',
			hasStyle: true,
			hidden:true,
			data: [
				["", '全部', 'color:black;'],
				["puser.Doctor.Id is not null", "已绑定医生", 'color:green;'],
				["puser.Doctor.Id is null", "未绑定医生", 'color:orange;']
			],
			listeners: {
				change: function(p, newV, oldV, e) {
					me.onSearch();
				}
			}
		});
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
		return me.callParent(arguments);
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this,
			where = [];
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = buttonsToolbar.getComponent('Search');
		var usertype = buttonsToolbar.getComponent('Usertype');
		var isBindDoctor = buttonsToolbar.getComponent('IsBindDoctor');
		//var params = [];
		if (usertype) {
			var value = usertype.getValue();
			if (value) {
				where.push("puser.Usertype='" + value + "'");
			}
		}
		if (isBindDoctor) {
			var value1 = "" + isBindDoctor.getValue();
			if (value1) {
				where.push(value1);
			}
		}
		if (search) {
			var value = search.getValue();
			var searchHql = "";
			if (value) searchHql = me.getSearchWhere(value);
			if (searchHql) {
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
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.fireEvent('onAccept', me, records);
	}
});
