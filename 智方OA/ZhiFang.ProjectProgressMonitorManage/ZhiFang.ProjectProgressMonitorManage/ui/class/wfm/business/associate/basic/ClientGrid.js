/**
 * 用户列表
 * @author liangyl	
 * @version 2017-03-29
 */
Ext.define('Shell.class.wfm.business.associate.basic.ClientGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '用户列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePClientByField',
	/**删除数据服务路径*/
	delUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_DelPClient',

	/**默认加载*/
	defaultLoad: true,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认选中数据，默认第一行，也可以默认选中其他行，也可以是主键的值匹配*/
	autoSelect: false,
	defaultOrderBy: [{ property: 'PClient_ClientNo', direction: 'ASC' }],

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '区域',
			dataIndex: 'PClient_ClientAreaName',
			width: 50,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '医院名称',
			dataIndex: 'PClient_Name',
			flex: 1,minWidth:100,
			sortable: false,
			defaultRenderer: true
		},{
		    text: '重复', dataIndex: 'PClient_IsRepeat',
			width:35,	align: 'center',
			isBool: true,
			type: 'bool',
			sortable:false,
			defaultRenderer:true
		},{
			text:'使用',dataIndex:'PClient_IsUse',
			width:45,align:'center',sortable:false,	isBool: true,
			type: 'bool',defaultRenderer:true
		},{
			text: '客户主键ID',
			dataIndex: 'PClient_Id',
			isKey: true,
			hidden: true,
			hideable: false
		}, {
			text: '省份',
			dataIndex: 'PClient_ProvinceName',
			hidden: true,
			hideable: false
		}];

		return columns;
	},

	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			ProvinceID = null,
			ClientTypeID = null,

			HospitalTypeID = null,
			HospitalLevelID = null,
			search = null,
			search2 = null,
			params = [];

		me.internalWhere = '';

		if(buttonsToolbar) {
			ProvinceID = buttonsToolbar.getComponent('ProvinceID').getValue();
			ClientTypeID = buttonsToolbar.getComponent('ClientTypeID').getValue();
			HospitalTypeID = buttonsToolbar.getComponent('HospitalTypeID').getValue();
			HospitalLevelID = buttonsToolbar.getComponent('HospitalLevelID').getValue();

			search = buttonsToolbar.getComponent('search').getValue();
			search2 = buttonsToolbar.getComponent('search2').getValue();
		}
		//省份
		if(ProvinceID) {
			params.push("pclient.ProvinceID='" + ProvinceID + "'");
		}
		//医院类别
		if(HospitalTypeID) {
			params.push("pclient.HospitalTypeID='" + HospitalTypeID + "'");
		}
		//客户类别
		if(ClientTypeID) {
			params.push("pclient.ClientTypeID='" + ClientTypeID + "'");
		}
		//医院等级
		if(HospitalLevelID) {
			params.push("pclient.HospitalLevelID='" + HospitalLevelID + "'");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}

		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere ="("+ me.getSearchWhere(search)+ ')';
			}
		}
		if(search2) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearch2Where(search2) + ')';
			} else {
				me.internalWhere = me.getSearch2Where(search2);
			}
		}
		return me.callParent(arguments);
	},
	/**获取查询框内容*/
	getSearch2Where: function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo2,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for(var i = 0; i < len; i++) {
			if(isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	},
	/**创建功能按钮栏*/
	createButtontoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			/**布局方式*/
			layout: 'absolute',
			/** 每个组件的默认属性*/
			defaults: {
				width: 145,
				labelWidth: 0
			},
			itemId: 'buttonsToolbar',
			items: items
		});
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems: function() {
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		buttonToolbarItems.push({
			x: 10,
			y: 5,
			labelWidth: -1,
			emptyText: '省份选择',
			labelWidth: 0,
			xtype: 'uxCheckTrigger',
			itemId: 'ProvinceName',
			fieldLabel: '',
			className: 'Shell.class.sysbase.country.province.CheckGrid',
			classConfig: {
				defaultWhere: "bprovince.BCountry.Id=5742820397511247346"
			}
		}, {
			x: 165,
			y: 5,
			labelWidth: -1,
			emptyText: '客户类型',
			xtype: 'uxCheckTrigger',
			itemId: 'ClientTypeName',
			fieldLabel: '',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '客户类型选择',
				defaultWhere: "pdict.PDictType.Id='" + JShell.WFM.GUID.DictType.ClientType.value + "'"
			}
		});

		buttonToolbarItems.push({
			x: 10,
			y: 30,
			labelWidth: 0,
			emptyText: '医院类别',
			xtype: 'uxCheckTrigger',
			itemId: 'HospitalTypeName',
			fieldLabel: '',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '医院类别选择',
				defaultWhere: "pdict.PDictType.Id='" + JShell.WFM.GUID.DictType.HospitalType.value + "'"
			}
		}, {
			x: 165,
			y: 30,
			labelWidth: 0,
			emptyText: '医院等级',
			xtype: 'uxCheckTrigger',
			itemId: 'HospitalLevelName',
			fieldLabel: '',
			className: 'Shell.class.wfm.dict.CheckGrid',
			classConfig: {
				title: '医院等级选择',
				defaultWhere: "pdict.PDictType.Id='" + JShell.WFM.GUID.DictType.HospitalLevel.value + "'"
			}
		});

		//查询框信息
		me.searchInfo = {
			x: 10,
			y: 55,
			emptyText: '名称/增值税开户行',
			isLike: true,
			itemId: 'search',
			fields: ['pclient.Name', 'pclient.VATBank']
		};
		me.searchInfo2 = {
			x: 165,
			y: 55,
			emptyText: '增值税税号/增值税帐号',
			isLike: true,
			itemId: 'search2',
			fields: ['pclient.VATNumber', 'pclient.VATAccount']
		};
		buttonToolbarItems.push({
			type: 'search',
			info: me.searchInfo
		}, {
			type: 'search',
			itemId: 'search2',
			info: me.searchInfo2
		});

		buttonToolbarItems.push({
			xtype: 'textfield',
			itemId: 'ClientTypeID',
			fieldLabel: '客户类型ID',
			hidden: true
		}, {
			xtype: 'textfield',
			itemId: 'ProvinceID',
			fieldLabel: '省份主键ID',
			hidden: true
		}, {
			xtype: 'textfield',
			itemId: 'HospitalTypeID',
			fieldLabel: '医院类别ID',
			hidden: true
		}, {
			xtype: 'textfield',
			itemId: 'HospitalLevelID',
			fieldLabel: '医院等级ID',
			hidden: true
		});

		return buttonToolbarItems;
	},
	/**省份监听*/
	doProvinceListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			CName = buttonsToolbar.getComponent('ProvinceName'),
			Id = buttonsToolbar.getComponent('ProvinceID');

		if(!CName) return;

		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('BProvince_Name') : '');
				Id.setValue(record ? record.get('BProvince_Id') : '');
				me.onSearch();
				p.close();
			}
		});
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		me.doProvinceListeners();
		//字典监听
		var dictList = [
			'HospitalType', 'ClientType', 'HospitalLevel'
		];
		for(var i = 0; i < dictList.length; i++) {
			me.doDictListeners(dictList[i]);
		}
	},
	/**字典监听*/
	doDictListeners: function(name) {
		var me = this;

		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var CName = buttonsToolbar.getComponent(name + 'Name');
		var Id = buttonsToolbar.getComponent(name + 'ID');
		if(!CName) return;
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('PDict_CName') : '');
				Id.setValue(record ? record.get('PDict_Id') : '');
				me.onSearch();
				p.close();
			}
		});
	}

});