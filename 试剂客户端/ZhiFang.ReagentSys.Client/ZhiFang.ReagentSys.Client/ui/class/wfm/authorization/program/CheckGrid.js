/**
 * 程序选择列表(需要过滤通讯节点下的数据)
 * @author longfc	
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.authorization.program.CheckGrid', {
	extend: 'Shell.ux.grid.CheckPanel',
	title: '程序选择',
	width: 270,
	height: 300,

	/**根据ID查询模式*/
	TypeModel: true,
	/**部门ID*/
	PK: null,

	/**获取数据服务路径*/
	selectUrl: '/PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramByHQL?isPlanish=true',
	/**根据部门ID获取数据服务路径*/
	selectUrl2: '/PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramByBDictTreeId?isPlanish=true',

	/**是否单选*/
	checkOne: true,
	/**默认加载*/
	defaultLoad: false,
	isSearchChildNode: false,

	initComponent: function() {
		var me = this;
		me.defaultWhere = me.defaultWhere || '';

		if(me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		/*需要过滤通讯节点下的数据并状态是已以布的程序*/
		me.defaultWhere += 'pgmprogram.SubBDictTree.Id!=5684872576807158459 and pgmprogram.PBDictTree.Id!=5684872576807158459 and pgmprogram.IsUse=1 and Status=3';
		//查询框信息
		me.searchInfo = {
			width: '70%',
			emptyText: '名称/SQH',
			isLike: true,
			fields: ['pgmprogram.Title', 'pgmprogram.SQH']
		};
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);

		me.buttonToolbarItems.splice(1, 0, {
			xtype: 'checkbox',
			boxLabel: '本节点',
			checked: true,
			value: false,
			inputValue: false,
			itemId: 'onlyShowDept',
			listeners: {
				change: function(field, newValue, oldValue) {
					if(newValue == true) {
						me.isSearchChildNode = false;
					} else {
						me.isSearchChildNode = true;
					}
					me.onSearch();
				}
			}
		});
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			text: '标题',
			dataIndex: 'PGMProgram_Title',
			width: 180,
			flex: 1,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: 'SQH号',
			dataIndex: 'PGMProgram_SQH',
			width: 100,
			sortable: false,
			menuDisabled: true,
			defaultRenderer: true
		}, {
			text: '编号',
			dataIndex: 'PGMProgram_No',
			hidden: false,
			width: 80,
			hideable: false,
			defaultRenderer: true
		}, {
			text: '版本号',
			dataIndex: 'PGMProgram_VersionNo',
			width: 60,
			hideable: false,
			defaultRenderer: true
		}, {
			text: '主键ID',
			dataIndex: 'PGMProgram_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}]

		return columns;
	},
	loadByDeptId: function(id) {
		var me = this;
		me.PK = id;
		me.onSearch();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],
			url = '';
		//根据ID查询模式
		if(me.TypeModel) {
			url += JShell.System.Path.getUrl(me.selectUrl2);
		} else {
			url += JShell.System.Path.getUrl(me.selectUrl);
		}

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',') + "&isSearchChildNode=" + me.isSearchChildNode;

		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";

		url += '&where=id=' + me.PK;
		if(where) {
			url += '^' + JShell.String.encode(where);
		}
		return url;
	}

});