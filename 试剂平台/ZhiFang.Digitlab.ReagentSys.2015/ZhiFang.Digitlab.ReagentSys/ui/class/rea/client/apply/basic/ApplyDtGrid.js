/**
 * @description 申请明细列表父类
 * @author longfc
 * @version 2017-10-23
 */
Ext.define('Shell.class.rea.client.apply.basic.ApplyDtGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '申请明细',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDtlByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaBmsReqDtl',
	/**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsReqDocAndDt',
	/**修改服务(只更新明细,更新主单)地址*/
	editUrl: '/ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDtlOfCheck',
	/**是否多选行*/
	checkOne: false,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,

	/**录入:entry/审核:check/生成订单:create*/
	OTYPE: "entry",
	/**当前的申请部门*/
	CurDeptId: null,
	/**当前登录者所属部门(包含子部门)下全部的供应商货品信息*/
	ReaGoodsCenOrgList: null,
	/**编辑前的行选中集合*/
	SelectionRecords: null,
	/**获取明细后是否实时获取货品明细的库存数*/
	isLoadCurrentQty: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			nodata: function(p) {
				me.enableControl(true);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onSaveAllDt');
		me.addEvents('onDeleted');
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		if(!me.checkOne) me.setCheckboxModel();
		//数据列
		me.columns = me.createGridColumns();
		//创建数据集
		me.store = me.createStore();
		me.callParent(arguments);
	},
	setCheckboxModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		//me.getView().update();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		if(!me.PK) return false;
		me.disableControl(); //禁用 所有的操作功能		
		if(!me.defaultLoad) return false;
	},
	/**创建供应商数据列*/
	createReaCenOrgColumn: function() {
		var me = this;
		var column = {
			dataIndex: 'ReaBmsReqDtl_OrgName',
			width: 185,
			text: '<b style="color:blue;">供应商</b>',
			editor: new Ext.form.field.ComboBox({
				mode: 'local',
				editable: false,
				allowBlank: me.OTYPE == "check" ? false : true,
				displayField: 'CenOrgCName',
				valueField: 'CenOrgCName',
				listClass: 'x-combo-list-small',
				store: new Ext.data.Store({ //Simple
					autoLoad: true,
					fields: ['CenOrgId', 'CenOrgCName', 'Id', 'CenOrgGoodsNo'], //
					data: []
				}),
				listeners: {
					expand: function(field, eOpts) {
						if(me.formtype != "show") {
							var record = field.ownerCt.editingPlugin.context.record;
							if(record && field.store.getCount() <= 0) {
								if(!me.ReaGoodsCenOrgList || me.ReaGoodsCenOrgList.length == 0) me.getReaGoodsCenOrgList(true);
								if(me.ReaGoodsCenOrgList && me.ReaGoodsCenOrgList.length > 0) {
									var goodId = record.get("ReaBmsReqDtl_GoodsID");
									for(var i = 0; i < me.ReaGoodsCenOrgList.length; i++) {
										var item = me.ReaGoodsCenOrgList[i];
										if(goodId == item.GoodsId) {
											var dataList = item.ReaCenOrgVOList;
											if(dataList) {
												//dataList.unshift({"Id":'',"CName":"请选择"});
												//console.log("dataList:"+dataList);
												field.store.loadData(dataList);
											}
											break;
										}
									}
								}
							}
						}
					},
					focus: function(field, e, eOpts) {
						me.comSetReadOnly(field, e);
					},
					select: function(field, records, eOpts) {
						var record = field.ownerCt.editingPlugin.context.record;
						record.set('ReaBmsReqDtl_OrderGoodsID', records[0].get("Id"));
						record.set('ReaBmsReqDtl_ReaCenOrg_Id', records[0].get("CenOrgId"));
						record.set('ReaBmsReqDtl_OrgName', records[0].get("CenOrgCName"));
						//record.commit();
						me.getView().refresh();
					}
				}
			})
		};
		return column;
	},
	/**创建申请数量数据列*/
	createGoodsQtyColumn: function() {
		var me = this;
		var column = {
			dataIndex: 'ReaBmsReqDtl_GoodsQty',
			text: '<b style="color:blue;">申请数量</b>',
			width: 60,
			editor: {
				xtype: 'numberfield',
				allowBlank: false,
				minValue: 0,
				listeners: {
					focus: function(com, e, eOpts) {
						me.comSetReadOnly(com, e);
					},
					change: function(com, newValue) {
						var record = com.ownerCt.editingPlugin.context.record;
						record.set('ReaBmsReqDtl_GoodsQty', newValue);
						//record.commit();
						me.getView().refresh();
					}
				}
			}
		};
		return column;
	},
	/**创建当前库存数数据列*/
	createCurrentQtyColumn: function() {
		var me = this;
		var column = {
			dataIndex: 'CurrentQty',
			text: '库存数',
			width: 75,
			renderer: function(value, meta, record) {
				var goodsOtherQty = "";
				goodsOtherQty = record.get("GoodsOtherQty");
				if(goodsOtherQty) goodsOtherQty = "<p border=0 style='vertical-align:top;font-size:12px;'>同系列库存为:" + goodsOtherQty + "</p>";
				if(value) goodsOtherQty = "<p border=0 style='vertical-align:top;font-size:12px;'>当前库存数为:" + value + "<br />" + goodsOtherQty + "</p>";
				meta.tdAttr = 'data-qtip="<b>' + goodsOtherQty + '</b>"';
				return value;
			}
		};
		return column;
	},
	comSetReadOnly: function(com, e) {
		var me = this;
		var isReadOnly = false;
		if(me.formtype === "show") {
			isReadOnly = true;
		} else {
			isReadOnly = false;
		}
		com.setReadOnly(isReadOnly);
	},
	showQtipValue: function(meta, record) {
		var me = this;
		var tempStr = "";
		return meta;
	},
	/**@description 获取部门货品的全部货品与货品所属供应商信息*/
	getReaGoodsCenOrgList: function(isRefresh) {
		var me = this;
		if(isRefresh == true) me.ReaGoodsCenOrgList = null;
		if(me.ReaGoodsCenOrgList != null && me.ReaGoodsCenOrgList.length > 0) return;
		var deptId = JShell.System.Cookie.get(JShell.System.Cookie.map.DEPTID) || "";
		if(!me.CurDeptId) return;

		var url = JShell.System.Path.ROOT + '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHRDeptId?deptId=' + me.CurDeptId;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				//var list = (data.data || {}).list || [];
				me.ReaGoodsCenOrgList = data.value;
			} else {
				me.ReaGoodsCenOrgList = null;
				JShell.Msg.error(data.msg);
			}
		}, false);
	},
	/**@description 获取明细的保存提交数据*/
	getDtSaveParams: function() {
		var me = this;
		var result = {
			dtAddList: [],
			dtEditList: []
		};
		if(me.store.getCount() <= 0) return result;
		me.store.each(function(record) {
			var id = record.get("ReaBmsReqDtl_Id");
			if(!id) id = "-1";
			var goodsQty = record.get("ReaBmsReqDtl_GoodsQty");
			if(!goodsQty) goodsQty = 0;
			var entity = {
				"Id": id,
				"OrgName": record.get("ReaBmsReqDtl_OrgName"),
				"GoodsQty": goodsQty,
				"GoodsCName": record.get("ReaBmsReqDtl_GoodsCName"),
				"GoodsUnit": record.get("ReaBmsReqDtl_GoodsUnit"),
				"GoodsID": record.get("ReaBmsReqDtl_GoodsID"),
				"Memo": record.get("ReaBmsReqDtl_Memo")
			};
			if(record.get("ReaBmsReqDtl_GoodsUnitID")) entity.GoodsUnitID = record.get("ReaBmsReqDtl_GoodsUnitID");
			if(record.get("ReaBmsReqDtl_OrderGoodsID")) entity.OrderGoodsID = record.get("ReaBmsReqDtl_OrderGoodsID");

			var reaCenOrg = null;
			if(record.get("ReaBmsReqDtl_ReaCenOrg_Id")) {
				reaCenOrg = {
					"Id": record.get("ReaBmsReqDtl_ReaCenOrg_Id")
				};
			}
			if(reaCenOrg) entity.ReaCenOrg = reaCenOrg;
			if(id && id != "-1") {
				result.dtEditList.push(entity);
			} else {
				var strDataTimeStamp = "1,2,3,4,5,6,7,8";
				if(entity.ReaCenOrg) entity.ReaCenOrg.DataTimeStamp = strDataTimeStamp.split(',');
				result.dtAddList.push(entity);
			}
		});
		return result;
	},
	/**@description 获取明细的保存提交数据*/
	getSaveParams: function(result) {
		var me = this;
		if(!result) result = me.getDtSaveParams();
		if(me.fromtype == "add") {
			me.PK = -1;
			me.Status = "1";
		}
		var entity = {
			"Id": me.PK,
			"Status": me.Status
		};
		var fields = "Id,Status";
		var params = {
			entity: entity,
			"fields": fields,
			"dtAddList": result.dtAddList,
			"dtEditList": result.dtEditList
		};
		return params;
	},
	/**@description编辑新增明细的保存处理*/
	onSaveOfUpdate: function(result, callback) {
		var me = this;
		var params = me.getSaveParams(result);
		if(!params) return false;
		var url = me.formtype == 'add' ? me.addUrl : me.editUrl;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;

		JShell.Server.post(url, JcallShell.JSON.encode(params), function(data) {
			me.hideMask(); //隐藏遮罩层
			callback(data);
		});
	},
	/**@description 修改按钮点击处理方法*/
	onDeleteDtClick: function() {
		var me = this;
		var records = me.getSelectionModel().getSelection();
		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		//当前申请单状态判断

		//区分前台删除及调用后台服务删除		
		JShell.Msg.del(function(but) {
			if(but != "ok") return;
			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;
			var showMask = false;
			for(var i in records) {
				var id = records[i].get(me.PKField);
				if(!id || id == "-1") {
					me.delCount++;
					me.store.remove(records[i]);
					if((me.delCount + me.delErrorCount) == me.delLength && me.delErrorCount == 0) {
						me.fireEvent('onDeleted', me);
					}
				} else {
					if(showMask == false) {
						showMask = true;
						me.showMask(me.delText); //显示遮罩层
					}
					me.delOneById(records[i], i, id);
				}
			}
		});
	},
	/**删除一条数据*/
	delOneById: function(record, index, id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;
		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				if(data.success) {
					me.store.remove(record);
					me.delCount++;
				} else {
					me.delErrorCount++;
				}
				if(me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.delErrorCount == 0) {
						me.fireEvent('onDeleted', me);
					} else {
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					}
				}
			});
		}, 100 * index);
	},
	/**@description 获取申请货品明细的库存数量*/
	onCurrentQtyClick: function() {
		var me = this;
		me.loadCurrentQty(true, true);
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		me.callParent(arguments);
		if(records && records.length > 0) {
			me.loadCurrentQty(me.isLoadCurrentQty, false);
		}
	},
	/**@description 获取申请货品明细的库存数量*/
	loadCurrentQty: function(isLoad, isShowMsg) {
		var me = this;
		if(isLoad == false) return;

		var idStr = "",
			goodIdStr = "";
		me.store.each(function(record) {
			var goodId = record.get("ReaBmsReqDtl_GoodsID");
			var orgId = record.get("ReaBmsReqDtl_ReaCenOrg_Id");
			if(orgId) {
				idStr += (goodId + ":" + orgId + ",");
				goodIdStr += goodId + ",";
			}
		});
		if(!goodIdStr || !idStr) return;
		goodIdStr = goodIdStr.substring(0, goodIdStr.length - 1);
		idStr = idStr.substring(0, idStr.length - 1);
		var url = "/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsCurrentQtyByGoodIdStr?goodIdStr=" + goodIdStr + "&idStr=" + idStr;
		url = (url.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + url;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				//var list = (data.data || {}).list || [];
				var list = data.value;
				if(list && list.length > 0) {
					me.store.each(function(record) {
						for(var i = 0; i < list.length; i++) {
							if(record.get("ReaBmsReqDtl_GoodsID") == list[i]["CurGoodsId"]) {
								record.set("CurrentQty", list[i]["CurrentQty"]);
								record.set("GoodsOtherQty", list[i]["GoodsOtherQty"]);
								record.commit();
								break;
							}
						}
					});
				} else {
					if(isShowMsg == true) JShell.Msg.error("获取货品的库存数据为空!");
				}
			} else {
				if(isShowMsg == true) JShell.Msg.error(data.msg);
			}
		});
	}
});