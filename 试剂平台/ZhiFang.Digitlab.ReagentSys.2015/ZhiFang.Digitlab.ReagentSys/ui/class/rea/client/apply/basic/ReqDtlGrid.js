/**
 * @description 部门采购申请录入申请明细列表
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.basic.ReqDtlGrid', {
	extend: 'Shell.class.rea.client.apply.basic.ApplyDtGrid',

	title: '申请明细信息',
	width: 800,
	height: 500,

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "entry",
	/**新增明细或删除明细按钮的启用状态*/
	buttonsDisabled: true,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**默认每页数量*/
	defaultPageSize: 150,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.CurDeptId = me.CurDeptId || "";
		me.CurDeptName = me.CurDeptName || "";
		me.addEvents('onSaveAllDt');
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1
		});
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		//创建数据集
		me.store = me.createStore();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'ReaBmsReqDtl_GoodsID',
			text: '货品Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_OrderGoodsID',
			text: '货品机构关系ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsReqDtl_GoodsCName',
			text: '货品名(包装规格)',
			width: 250,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_ReaCenOrg_Id',
			text: '供应商Id',
			width: 135,
			hidden: true,
			defaultRenderer: true
		}];
		columns.push(me.createReaCenOrgColumn());
		columns.push(me.createGoodsQtyColumn());
		columns.push({
			dataIndex: 'ReaBmsReqDtl_GoodsUnitID',
			text: '包装单位Id',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_GoodsUnit',
			text: '包装单位',
			width: 65,
			sortable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'GoodsOtherQty',
			text: '货品对应同系列的库存数量',
			width: 65,
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsReqDtl_Memo',
			sortable: false,
			text: '<b style="color:blue;">备注</b>',
			width: 80,
			hidden: false,
			editor: {
				xtype: 'textarea',
				height: 60
			},
			defaultRenderer: true
		});
		columns.push(me.createCurrentQtyColumn());
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		items.push('-', {
			iconCls: 'button-add',
			itemId: "btnAdd",
			disabled: true,
			text: '新增明细',
			tooltip: '新增货品明细',
			handler: function() {
				me.onAddDtClick();
			}
		}, {
			xtype: 'button',
			iconCls: 'button-del',
			itemId: "btnDel",
			disabled: true,
			text: '删除明细',
			tooltip: '删除勾选中的明细行',
			handler: function() {
				me.onDeleteDtClick();
			}
		});
		items.push('-', {
			fieldLabel: '',
			emptyText: '模板选择',
			labelWidth: 0,
			width: 180,
			name: 'cboGoodstemplate',
			itemId: 'cboGoodstemplate',
			xtype: 'uxCheckTrigger',

			classConfig: {
				width: 385,
				height: 460,
				checkOne: true,
				TemplateType: "ReaReqDtl",
				defaultWhere: " reachoosegoodstemplate.SName='ReaReqDtl'"
			},
			className: 'Shell.class.rea.client.goodstemplate.CheckGrid',
			listeners: {
				beforetriggerclick: function(p) {
					if(!p.classConfig || !p.classConfig.DeptID) me.setGoodstemplateClassConfig();
					if(!p.classConfig || !p.classConfig.DeptID) {
						JShell.Msg.warning('获取部门信息为空,请选择部门后再操作!');
						return false;
					}
				},
				check: function(p, record) {
					p.close();
					me.onAddRecords(record);
				}
			}
		}, {
			xtype: 'button',
			iconCls: 'button-save',
			itemId: "btnSaveTemplate",
			text: '保存模板',
			tooltip: '保存模板',
			handler: function() {
				me.onSaveTemplate();
			}
		});
		//查询框信息
		me.searchInfo = {
			width: 140,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品名',
			fields: ['reabmsreqdtl.GoodsCName']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		}, '-', {
			xtype: 'button',
			iconCls: 'button-search',
			text: '同步库存',
			tooltip: '同步库存数',
			handler: function() {
				me.onCurrentQtyClick();
			}
		});
		return items;
	},
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		//		JShell.Action.delay(function() {		
		//		}, null, 1000);
		me.setButtonsDisabled(me.buttonsDisabled);
	},
	/**按钮的启用或或禁用*/
	setButtonsDisabled: function(disabled) {
		var me = this;

		me.setBtnDisabled("btnAdd", disabled);
		me.setBtnDisabled("btnDel", disabled);
		me.setBtnDisabled("btnSave", disabled);
		me.setBtnDisabled("cboGoodstemplate", disabled);
	},
	setBtnDisabled: function(itemId, disabled) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var btn = buttonsToolbar.getComponent(itemId);
			if(btn) btn.setDisabled(disabled);
		}
	},
	/**@description 显示新增货品明细*/
	showDtGridCheck: function() {
		var me = this;
		if(me.ReaGoodsCenOrgList == null || me.ReaGoodsCenOrgList.length == 0) me.getReaGoodsCenOrgList(true);
		if(me.ReaGoodsCenOrgList == null || me.ReaGoodsCenOrgList.length == 0) {
			JShell.Msg.error('获取供货方与货品的有效信息为空(可能货品已失效)！<br/>请先维护好供货方与货品信息关系后再操作！');
			return;
		}
		var dtData = [];
		var goodIdStr = "";
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		me.store.each(function(record) {
			record.commit();
			dtData.push(record);
			goodIdStr += record.get("ReaBmsReqDtl_GoodsID") + ",";
		});
		goodIdStr = goodIdStr.substring(0, goodIdStr.length - 1);
		var maxWidth = document.body.clientWidth * 0.96;
		var height = document.body.clientHeight * 0.92;
		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			Status: me.Status,
			CurDeptId: me.CurDeptId,
			GoodIdStr: goodIdStr,
			ReaGoodsCenOrgList: me.ReaGoodsCenOrgList,
			listeners: {
				close: function(p) {
					me.onSearch();
				},
				save: function(p, saveData) {
					me.onCheckSaveDt(p, saveData);
				}
			}
		};
		if(me.PK) {
			config.formtype = 'edit';
			config.PK = me.PK;
		} else {
			config.formtype = 'add';
		}
		var win = JShell.Win.open('Shell.class.rea.client.apply.basic.DtCheckPanel', config);
		win.show();
		if(dtData.length > 0)
			win.loadDtData(dtData);
	},
	/**@description 新增明细选择后处理方法*/
	onCheckSaveDt: function(p, saveData) {
		var me = this;
		//如果是全新的新增
		switch(me.formtype) {
			case "add":
				me.store.removeAll();
				if(saveData)
					me.store.loadData(saveData);
				p.close();
				break;
			default:
				if(saveData) me.onSaveOfUpdate(saveData, function(data) {
					if(data.success) {
						JShell.Msg.alert('新增货品明细保存数据成功', null, 1000);
						p.close();
						me.onSearch();
					} else {
						JShell.Msg.error('新增货品明细保存数据失败！' + data.msg);
					}
				});
				break;
		}
	},
	/**@description 新增按钮点击处理方法*/
	onAddDtClick: function() {
		var me = this;
		if(me.CurDeptId) {
			me.showDtGridCheck();
		} else {
			JShell.Msg.alert("请选择申请部门后再操作!", null, 1000);
		}
	},
	/**@description 明细保存按钮点击处理方法*/
	onSaveDtClick: function() {
		var me = this;
		var isBreak = false;
		var msgInfo = "";
		me.store.each(function(record) {
			var goodsQty = record.get("ReaBmsReqDtl_GoodsQty");
			if(!goodsQty) goodsQty = 0;
			goodsQty = parseInt(goodsQty);
			if(goodsQty <= 0) {
				isBreak = true;
				var goodName = record.get("ReaBmsReqDtl_GoodsCName");
				msgInfo = "当前货品名为" + goodName + ",其申请数量<=0!不能保存!";
				return false;
			}
		});
		if(isBreak == true) {
			JShell.Msg.error(msgInfo);
			return;
		}

		var result = me.getDtSaveParams();
		me.onSaveOfUpdate(result, function(data) {
			if(data.success) {
				JShell.Msg.alert('新增货品明细保存数据成功', null, 1000);
				me.fireEvent('onSaveAllDt', me, true);
			} else {
				JShell.Msg.error('新增货品明细保存数据失败！' + data.msg);
				me.fireEvent('onSaveAllDt', me, true);
			}
		});
	},
	/**订单选择改变或部门选择改变后更新模板选择的配置项*/
	setGoodstemplateClassConfig: function() {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var cbo = buttonsToolbar.getComponent("cboGoodstemplate");
			cbo
			if(cbo) {
				cbo.setValue("");
				cbo.changeClassConfig({
					"DeptID": me.CurDeptId,
					"defaultWhere": " reachoosegoodstemplate.SName='ReaReqDtl' and reachoosegoodstemplate.DeptID=" + me.CurDeptId
				});
			}
		}
	},
	/**@description 模板保存*/
	onSaveTemplate: function() {
		var me = this;
		if(me.store.getCount() <= 0) {
			JShell.Msg.error('当前选择的货品明细为空!');
			return;
		}
		if(!me.CurDeptId) {
			JShell.Msg.error('获取部门信息为空!');
			return;
		}
		var tempArr = [];
		me.store.each(function(record) {
			var data = {};
			data = Ext.apply(data, record.data);
			data["ReaBmsReqDtl_Id"] = "-1";
			data["CurrentQty"] = "";
			data["GoodsOtherQty"] = "";
			tempArr.push(data);
		});
		var config = {
			resizable: true,
			formtype: "add",
			DeptID: me.CurDeptId,
			DeptName: me.CurDeptName,
			TemplateType: "ReaReqDtl",
			ContextJson: JcallShell.JSON.encode(tempArr),
			listeners: {
				save: function(p, id) {
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.goodstemplate.ApplyForm', config);
		win.show();
	},
	/**明细模板选择后,申请明细列表的货品信息处理*/
	onAddRecords: function(record) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var cbo = buttonsToolbar.getComponent("cboGoodstemplate");
		if(record) cbo.setValue(record.get("ReaChooseGoodsTemplate_CName"));
		else cbo.setValue("");

		if(!record) return;

		var contextJson = record.get("ReaChooseGoodsTemplate_ContextJson");
		if(contextJson) contextJson = JcallShell.JSON.decode(contextJson);
		if(!contextJson) return;

		Ext.Array.each(contextJson, function(data) {
			var goodId = data["ReaBmsReqDtl_GoodsID"];
			var indexOf = -1;
			if(goodId) indexOf = me.store.findExact("ReaBmsReqDtl_GoodsID", goodId);
			//模板的货品不存在明细列表里
			if(goodId && indexOf == -1) {
				data["ReaBmsReqDtl_Id"] = "-1";
				data["CurrentQty"] = "";
				data["GoodsOtherQty"] = "";
				me.store.add(data);
			}
		});
	}
});