/**
 * 供应商货品选择
 * @author longfc
 * @version 2018-11-14
 */
Ext.define('Shell.class.rea.client.goodsorglink.supply.choose.LeftGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],

	title: '待选货品列表',
	width: 530,
	height: 620,
	/**是否带清除按钮*/
	hasClearButton: false,
	/**是否带确认按钮*/
	hasAcceptButton: false,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?isPlanish=true',
	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaGoodsOrgLink_CenOrgGoodsNo',
		direction: 'ASC'
	}, {
		property: 'ReaGoodsOrgLink_DispOrder',
		direction: 'ASC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'goodsorglink.supply.choose.LeftGrid',
	/**用户UI配置Name*/
	userUIName: "供应商货品待选择列表",
	
	initComponent: function() {
		var me = this;
		me.addEvents('onBeforeSearch');
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'search',
			emptyText: '部门名称/货品名称/英文名/简称/货品编码',
			fields: ['reagoodsorglink.ReaGoods.DeptName', 'reagoodsorglink.ReaGoods.CName', 'reagoodsorglink.ReaGoods.EName', 'reagoodsorglink.ReaGoods.SName', 'reagoodsorglink.ReaGoods.ReaGoodsNo']
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
			dataIndex: 'ReaGoodsOrgLink_ReaGoodsGoodsClass',
			text: '一级分类',
			width: 75,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_GoodsClassType',
			text: '二级分类',
			width: 75,
			hidden: true,
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_ReaGoodsNo',
			text: '货品编码',
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_BarCodeMgr',
			text: '条码类型',
			hidden: true,
			width: 50,
			renderer: function(value, meta) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_CName',
			text: '货品名称',
			flex: 1,
			width: 140,
			minWidth: 140,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("ReaGoodsOrgLink_BarCodeType");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				if(value.indexOf('"')>=0)value=value.replace(/\"/g, " ");
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_PinYinZiTou',
			text: '拼音字头',
			hidden: true,
			width: 65,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_EName',
			text: '英文名',
			//hidden: true,
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_SName',
			text: '简称',
			hidden: true,
			width: 45,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_ProdOrgName',
			text: '厂家名称',
			hidden: true,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_UnitName',
			text: '单位',
			//hidden: true,
			width: 40,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_UnitMemo',
			text: '货品规格',
			//hidden: true,
			width: 90,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_Price',
			text: '价格',
			width: 55,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_DispOrder',
			text: '优先级',
			hidden: true,
			width: 55,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_ProdGoodsNo',
			text: '厂商货品编码',
			hidden: true,
			width: 90,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrgGoodsNo',
			text: '供货商货品编码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_GoodsNo',
			text: '货品平台编号',
			width: 90,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_BiddingNo',
			text: '招标号',
			hidden: true,
			width: 80,
			defaultRenderer: true
		}, {
			text: '有效开始',
			dataIndex: 'ReaGoodsOrgLink_BeginTime',
			isDate: true,
			width: 80,
			hidden: true,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '有效截止',
			dataIndex: 'ReaGoodsOrgLink_EndTime',
			hidden: true,
			width: 80,
			sortable: false,
			hideable: true,
			renderer: function(curValue, meta, record, rowIndex, colIndex, s, view) {
				var bgColor = "";
				var value = curValue;
				if(value) {
					var Sysdate = JShell.System.Date.getDate();
					value = Ext.util.Format.date(value, 'Y-m-d');
					Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
					Sysdate = JShell.Date.getDate(Sysdate);
					var RegisterInvalidDate = value;
					RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
					var days = parseInt((RegisterInvalidDate - Sysdate) / 1000 / 60 / 60 / 24);

					if(days < 0) {
						bgColor = "red";
						value = "已失效";
					} else if(days >= 0 && days <= 30) {
						bgColor = "#e97f36";
						value = "30天内到期";
					} else if(days > 30) {
						bgColor = "#568f36";
					}

				} else {
					if(record.get("ReaGoodsOrgLink_BeginTime")) {
						bgColor = "#568f36";
						value = "长期有效";
					} else {
						bgColor = "#e97f36";
						value = "无有效期";
					}
				}
				if(curValue) curValue = Ext.util.Format.date(curValue, 'Y-m-d');
				meta.tdAttr = 'data-qtip="' + curValue + '"';
				if(bgColor) meta.style = 'background-color:' + bgColor + ';color:#ffffff;';
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_RegistDate',
			text: '注册日期',
			hidden: true,
			width: 40,
			sortable: false,
			defaultRenderer: true
		}, {
			text: '注册证有效期',
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_RegistNoInvalidDate',
			isDate: true,
			width: 80,
			hidden: true,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_RegistNo',
			text: '注册证编号',
			hidden: true,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_Id',
			text: '货品主键ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoodsOrgLink_CenOrg_Id',
			text: '机构主键ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoodsOrgLink_IsPrintBarCode',
			text: '是否打印条码',
			hidden: true,
			hideable: false,
			renderer: function(value, meta, record) {
				var v = "";
				if(value == "0") {
					v = "否";
					meta.style = "color:orange;";
				} else if(value == "1") {
					v = "是";
					meta.style = "color:green;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_BarCodeType',
			text: '条码类型',
			hidden: true,
			hideable: false,
			renderer: function(value, meta) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_GoodsSort',
			text: '货品序号',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'ReaGoodsOrgLink_ReaGoods_IsNeedPerformanceTest',
			text: '性能验证',hidden: true,width:100,hideable: false
		}];

		return columns;
	},
	initButtonToolbarItems: function() {
		var me = this;
		me.callParent(arguments);
		me.buttonToolbarItems.unshift({
			emptyText: '条码类型',
			labelWidth: 0,
			width: 80,
			hasStyle: true,
			xtype: 'uxSimpleComboBox',
			itemId: 'BarCodeMgr',
			hasStyle: true,
			data: [
				['', '请选择', 'color:green;'],
				['0', '批条码', 'color:green;'],
				['1', '盒条码', 'color:orange;'],
				['2', '无条码', 'color:black;']
			],
			value: '',
			listeners: {
				select: function(com, records, eOpts) {
					me.onSearch();
				}
			}
		}, {
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
		}, {
			fieldLabel: '',
			labelWidth: 0,
			width: 85,
			name: 'BDict_CName',
			itemId: 'BDict_CName',
			xtype: 'uxCheckTrigger',
			emptyText: '厂商选择',
			className: 'Shell.class.sysbase.dict.CheckGrid',
			classConfig: {
				title: '厂商选择',
				defaultWhere: "bdict.BDictType.DictTypeCode='ProdOrg'"
			},
			listeners: {
				check: function(p, record) {
					me.onCompAccept(p, record);
				}
			}
		}, {
			fieldLabel: '厂商主键ID',
			itemId: 'BDict_Id',
			name: 'BDict_Id',
			xtype: 'textfield',
			hidden: true
		});
		me.buttonToolbarItems.push('->', {
			iconCls: 'button-check',
			text: '全部选择',
			tooltip: '将当前页货品全部选择',
			handler: function() {
				me.onAcceptClick();
			}
		});
	},
	onGoodsClass: function(p, record, classType) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var classTypeCom = buttonsToolbar.getComponent(classType);
		classTypeCom.setValue(record ? record.get('ReaGoodsClassVO_CName') : '');
		p.close();
		me.onSearch();
	},
	onCompAccept: function(p, record) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var Id = buttonsToolbar.getComponent('BDict_Id');
		var CName = buttonsToolbar.getComponent('BDict_CName');
		Id.setValue(record ? record.get('BDict_Id') : '');
		CName.setValue(record ? record.get('BDict_CName') : '');
		p.close();
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
			goodsClass = buttonsToolbar.getComponent('GoodsClass').getValue(),
			goodsClassType = buttonsToolbar.getComponent('GoodsClassType').getValue(),
			dictCName = buttonsToolbar.getComponent('BDict_CName').getValue(),
			search = buttonsToolbar.getComponent('search'),
			barCodeMgr = buttonsToolbar.getComponent('BarCodeMgr');
		if(barCodeMgr) {
			var value = barCodeMgr.getValue();
			if(value) {
				where.push("reagoodsorglink.ReaGoods.BarCodeMgr=" + value);
			}
		}
		if(goodsClass) {
			where.push("reagoodsorglink.ReaGoods.GoodsClass='" + goodsClass + "'");
		}
		if(goodsClassType) {
			where.push("reagoodsorglink.ReaGoods.GoodsClassType='" + goodsClassType + "'");
		}
		if(dictCName) {
			where.push("reagoodsorglink.ReaGoods.ProdOrgName='" + dictCName + "'");
		}
		if(search) {
			var value = search.getValue();
			var searchHql = me.getSearchWhere(value);
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
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.getInternalWhere();
		return me.callParent(arguments);
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