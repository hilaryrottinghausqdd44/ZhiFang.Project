/**
 * 货品待选列表
 * @author longfc
 * @version 2016-10-24
 */
Ext.define('Shell.class.rea.client.apply.basic.ReaGoodsCheck', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	
	title: '部门货品待选列表',
	width: 360,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/RS_UDTO_SearchListByDeptIdAndHQL?isPlanish=true',

	/**是否单选*/
	checkOne: false,
	/**是否带确认按钮*/
	hasAcceptButton: true,
	/**默认每页数量*/
	defaultPageSize: 20,
	
	/**用户UI配置Key*/
	userUIKey: 'apply.basic.ReaGoodsCheck',
	/**用户UI配置Name*/
	userUIName: "部门货品待选列表",
	
	/**当前的申请部门*/
	CurDeptId: null,

	initComponent: function() {
		var me = this;
		me.addEvents('onBeforeSearch');
		me.defaultWhere = me.defaultWhere || '';
		if (me.defaultWhere) {
			me.defaultWhere = '(' + me.defaultWhere + ') and ';
		}
		me.defaultWhere += ' readeptgoods.ReaGoods.Visible=1 and readeptgoods.Visible=1';

		//查询框信息
		me.searchInfo = {
			width: "80%", //280
			isLike: true,
			itemId: 'search',
			emptyText: '供应商/适用机型/拼音字头/中文名/简称/货品编码/英文名',
			fields: ['readeptgoods.ReaGoods.ReaCompanyName', 'readeptgoods.ReaGoods.SuitableType',
				'readeptgoods.ReaGoods.PinYinZiTou', 'readeptgoods.ReaGoods.CName', 'readeptgoods.ReaGoods.SName',
				'readeptgoods.ReaGoods.ReaGoodsNo', 'readeptgoods.ReaGoods.EName'
			]
		};
		//自定义按钮功能栏
		//me.buttonToolbarItems = ['->'];
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaDeptGoods_ReaGoods_BarCodeMgr',
			text: '条码类型',
			width: 60,
			hidden: true,
			renderer: function(value, meta) {
				if (value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if (value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if (value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}

				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaDeptGoods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_SName',
			text: '简称',
			width: 80,
			defaultRenderer: true
		},{
			dataIndex: 'ReaDeptGoods_ReaGoods_ReaGoodsNo',
			text: '货品码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_Id',
			text: '货品ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_CName',
			text: '货品名',
			width: 140,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_EName',
			text: '英文名',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_UnitName',
			text: '单位',
			width: 50,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_DispOrder',
			text: '显示次序',
			width: 50,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_UnitMemo',
			text: '规格',
			width: 80,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_MonthlyUsage',
			text: '月用量',
			width: 65,
			defaultRenderer: true
		}];
		columns.push(me.createCurrentQtyColumn());
		columns.push({
			dataIndex: 'ReaDeptGoods_ReaGoods_ReaCompanyName',
			text: '所属供应商',
			//hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_SuitableType',
			text: '适用机型',
			//hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_ShortCode',
			text: '系列码',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_ProdID',
			text: '生产厂家Id',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaDeptGoods_ReaGoods_ProdOrgName',
			text: '生产厂家',
			hidden: true,
			width: 80,
			defaultRenderer: true
		});

		return columns;
	},
	/**创建当前库存数数据列*/
	createCurrentQtyColumn: function() {
		var me = this;
		var column = {
			dataIndex: 'ReaDeptGoods_CurrentQtyVO_GoodsQty',
			text: '当前库存数',
			width: 75,
			doSort: function(state) {
				//自定义排序字段,转换为库存数
				me.store.sort({
					property: "ReaBmsQtyDtl_GoodsQty",
					direction: state
				});
			},
			renderer: function(value, meta, record) {
				return value;
			}
		};
		return column;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this;
		var items1 = me.callParent(arguments);
		items1.border = false;
		var items = me.createButtonToolbar1Items();
		items = Ext.Array.merge(items, items1);
		//items = me.callParent(arguments);
		return items;
	},
	/**默认按钮栏*/
	createButtonToolbar1Items: function() {
		var me = this;
		var items = [];
		items.push({
			emptyText: '一级分类',
			labelWidth: 0,
			width: 80,
			itemId: 'GoodsClass',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '一级分类',
				ClassType: "GoodsClass"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClass');
				}
			}
		}, {
			emptyText: '二级分类',
			labelWidth: 0,
			width: 80,
			itemId: 'GoodsClassType',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.client.goodsclass.GoodsCheck',
			classConfig: {
				title: '二级分类',
				ClassType: "GoodsClassType"
			},
			listeners: {
				check: function(p, record) {
					me.onGoodsClass(p, record, 'GoodsClassType');
				}
			}
		});
		// 需求调整：供货商筛选
		items.push(
		{
			fieldLabel: '',
			name: 'CompanyName', 
			itemId: 'CompanyName',
			xtype: 'uxCheckTrigger',
			emptyText: '供货商',
			width: 70,
			labelWidth: 0,
			labelAlign: 'right',
			className: 'Shell.class.rea.client.reacenorg.basic.CheckGrid',
			classConfig: {
				title: '供货商选择',
				checkOne:true,//单选
				defaultOrderBy: [{
					property: 'ReaCenOrg_DispOrder',
					direction: 'ASC'
				}],
				
			},
			listeners: {
				check: function(p, record) {
					me.onCompAccept(p, record);
				}
			}
		},
		{
			fieldLabel: '供货商主键ID',
			hidden: true,
			xtype: 'uxCheckTrigger',
			name: 'CompanyID',
			itemId: 'CompanyID'
		}
		
		);
		items.push({
			xtype: 'numberfield',
			itemId: 'GoodsQty',
			name: 'GoodsQty',
			emptyText: '',
			fieldLabel: "库存小于等于",
			labelWidth: 85,
			width: 135,
			minValue: 0,
			listeners: {
				specialkey: function(field, e) {
					if (e.getKey() == Ext.EventObject.ENTER) {
						me.onSearch();
					}
				}
			}
		});
		items.push({
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			border: false,
			itemId: 'buttonsToolbar1',
			items: items
		});
	},
	/**需求调整供应商选择*/
	onCompAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var CName = buttonsToolbar.getComponent('CompanyName');
		var Id = buttonsToolbar.getComponent('CompanyID');
		
		CName.setValue(record ? record.get('ReaCenOrg_CName') : '');
		Id.setValue(record ? record.get('ReaCenOrg_Id') : '');
		p.close();
		me.onSearch();
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar1');
		var classTypeCom = buttonsToolbar.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.onSearch();
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		me.fireEvent('onBeforeSearch', me);
		me.load(null, true, autoSelect);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = buttonsToolbar.getComponent('search').getValue(),
			params = [];
		var buttonsToolbar1 = me.getComponent('buttonsToolbar1'),
		// 一级分类值
			goodsClass = buttonsToolbar1.getComponent('GoodsClass').getValue(),
			// 二级分类值
			goodsClassType = buttonsToolbar1.getComponent('GoodsClassType').getValue(),
			// 库存值
			goodsQty = buttonsToolbar1.getComponent('GoodsQty').getValue(),
			// 需求调整：供应商的值
			// 使用供应商的id
			companyId = buttonsToolbar1.getComponent('CompanyID').getValue();
		//一级分类	
		if (goodsClass) {
			params.push("readeptgoods.ReaGoods.GoodsClass='" + goodsClass + "'");
		}
		//二级分类	
		if (goodsClassType) {
			params.push("readeptgoods.ReaGoods.GoodsClassType='" + goodsClassType + "'");
		}
		// 需求调整：增加供应商筛选
		if(companyId) {
			params.push("readeptgoods.ReaGoods.Id in (select link.ReaGoods.Id from ReaGoodsOrgLink link where link.CenOrg.Id =  " + companyId +")");
		}
		if (search) {
			params.push('(' + me.getSearchWhere(search) + ')');
		}
		me.internalWhere = params.join(' and ');
		var url = me.callParent(arguments);
		url += "&deptId=" + me.CurDeptId;
		if (goodsQty || goodsQty == 0) url += "&goodsQty=" + goodsQty;
		return url;
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		if (records && records.length > 0) {
			me.loadCurrentQty();
		}
	},
	/**@description 获取申请货品明细的库存数量*/
	loadCurrentQty: function() {
		var me = this;
		var idStr = "",
			goodIdStr = "";
		me.store.each(function(record) {
			var goodId = record.get("ReaDeptGoods_ReaGoods_Id");
			goodIdStr += goodId + ",";
		});
		if (!goodIdStr) return;
		goodIdStr = goodIdStr.substring(0, goodIdStr.length - 1);
		idStr = idStr.substring(0, idStr.length - 1);
		var url = "/ReaManageService.svc/ST_UDTO_SearchReaGoodsCurrentQtyByGoodIdStr?goodIdStr=" + goodIdStr + "&idStr=" +
			idStr;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		JShell.Server.get(url, function(data) {
			if (data.success) {
				var list = data.value;
				if (list && list.length > 0) {
					me.store.each(function(record) {
						for (var i = 0; i < list.length; i++) {
							if (record.get("ReaDeptGoods_ReaGoods_Id") == list[i]["CurGoodsId"]) {
								var currentQty = list[i]["GoodsQty"];
								if (!parseFloat(currentQty))
									currentQty = 0;
								record.set("ReaDeptGoods_CurrentQtyVO_GoodsQty", currentQty);
								record.commit();
								break;
							}
						}
					});
				}
			}
		});
	}
});
