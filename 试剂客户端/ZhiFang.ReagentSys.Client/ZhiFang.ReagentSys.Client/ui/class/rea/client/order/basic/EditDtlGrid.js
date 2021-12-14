/**
 * 订货明细列表
 * @author longfc
 * @version 2017-11-15
 */
Ext.define('Shell.class.rea.client.order.basic.EditDtlGrid', {
	extend: 'Shell.class.rea.client.order.basic.OrderDtlGrid',
	requires: [
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '订货明细列表',

	/**获取数据服务路径*/
	selectUrl: '/ReaManageService.svc/ST_UDTO_SearchReaBmsCenOrderDtlByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaManageService.svc/ST_UDTO_DelReaBmsCenOrderDtl',

	/**新增服务地址*/
	addUrl: '/ReaManageService.svc/ST_UDTO_AddReaBmsCenOrderDocAndDt',
	/**修改服务地址*/
	editUrl: '/ReaManageService.svc/ST_UDTO_UpdateReaBmsCenOrderDocAndDt',
	/**获取模板选择调用查询服务 */
	selectModelUrl: '/ReaManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?isGetGoodsQty=true&isPlanish=true',

	/**录入:entry/审核:check*/
	OTYPE: "entry",
	/**是否多选行*/
	checkOne: true,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.addEvents('onAddAfter', 'onDelAfter', 'onEditAfter');
		if(!me.checkOne) me.setCheckboxModel();
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			listeners: {
				edit: function(editor, e, eOpts) {
					me.fireEvent('onEditAfter', me);
				}
			}
		});
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = me.callParent(arguments);
		columns.splice(3, 0, {
			xtype: 'actioncolumn',
			text: '删除',
			align: 'center',
			style: 'font-weight:bold;color:white;background:orange;',
			width: 40,
			hideable: false,
			sortable: false,
			items: [{
				getClass: function(v, meta, record) {
					var id = record.get("ReaBmsCenOrderDtl_Id");
					if(id == "" || id == "-1") {
						meta.tdAttr = 'data-qtip="<b>删除</b>"';
						return 'button-del hand';
					} else if(me.buttonsDisabled==false){
						return 'button-del hand';
					} else{
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					JShell.Msg.confirm({
						title: '<div style="text-align:center;">删除操作确认</div>',
						msg: '请确认是否删除?',
						closable: true
					}, function(but, text) {
						if(but != "ok") return;
						var rec = grid.getStore().getAt(rowIndex);
						me.deleteOne(rec,0);
					});
				}
			}]
		});
		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh'];
		if(me.OTYPE != "upload") {
			items.push('-', {
				iconCls: 'button-add',
				itemId: "btnAdd",
				disabled: true,
				text: '新增明细',
				tooltip: '新增货品明细',
				handler: function() {
					me.onAddDtClick();
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
					TemplateType: "ReaOrderDtl"
					//defaultWhere: " reachoosegoodstemplate.SName='ReaOrderDtl'"
				},
				className: 'Shell.class.rea.client.goodstemplate.CheckGrid',
				listeners: {
					beforetriggerclick: function(p) {
						//if(!p.classConfig || !p.classConfig.OrgID) me.setGoodstemplateClassConfig();
						if(!p.classConfig || !p.classConfig.OrgID) {
							JShell.Msg.warning('获取供货商信息为空,请选择供货商后再操作!');
							return false;
						}
						me.setGoodstemplateClassConfig();
					},
					check: function(p, record) {
						p.close();
						me.loadByReaGoodsNo(record);
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
		}
		//查询框信息
		me.searchInfo = {
			width: 200,
			isLike: true,
			itemId: 'Search',
			emptyText: '货品中文名/货品平台编码',
			fields: ['reabmscenorderdtl.ReaGoodsName', 'reabmscenorderdtl.GoodsNo']
		};
		items.push('->', {
			type: 'search',
			info: me.searchInfo
		});
		return items;
	},
	/**@description 新增按钮点击处理方法*/
	onAddDtClick: function() {
		var me = this;
		if(me.ReaCompID) {
			me.showDtGridCheck();
		} else {
			JShell.Msg.alert("请选择供应商后再操作!", null, 1000);
		}
	},
	/**@description 显示新增货品明细*/
	showDtGridCheck: function() {
		var me = this;
		var linkIdStr = "";
		var arrIdStr = [],
			idStr = "";
		var defaultWhere = " reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.OrgType=0 and reagoodsorglink.CenOrg.Id=" + me.ReaCompID;
		me.store.each(function(record) {
			if(record.get("ReaBmsCenOrderDtl_CompGoodsLinkID"))
				arrIdStr.push(record.get("ReaBmsCenOrderDtl_CompGoodsLinkID"));
		});
		if(arrIdStr.length > 0) linkIdStr = arrIdStr.join(",");
		if(linkIdStr) defaultWhere = defaultWhere + "and reagoodsorglink.Id not in (" + linkIdStr + ")";
		var maxWidth = document.body.clientWidth * 0.98;
		var height = document.body.clientHeight * 0.92;

		var config = {
			resizable: true,
			width: maxWidth,
			height: height,
			leftDefaultWhere: defaultWhere,
			defaultWhere: defaultWhere,
			listeners: {
				accept: function(p, records) {
					me.onDtCheck(p, records);
				}
			}
		};
		//'Shell.class.rea.client.order.GoodsOrgLinkCheck'
		var win = JShell.Win.open('Shell.class.rea.client.order.choose.App', config);
		win.show();
	},
	/**@description 新增明细选择后处理方法*/
	onDtCheck: function(p, records) {
		var me = this;
		me.onDtAddOfNew(p, records);
	},
	/**修改时新增明细处理*/
	onDtAddOfNew: function(p, records) {
		var me = this,
			len = records.length,
			arr = [];
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var obj = {
				ReaBmsCenOrderDtl_Id: "",
				//ReaBmsCenOrderDtl_BiddingNo: rec.get('ReaGoodsOrgLink_BiddingNo'),
				ReaBmsCenOrderDtl_ReaGoodsID: rec.get('ReaGoodsOrgLink_ReaGoods_Id'),
				ReaBmsCenOrderDtl_CompGoodsLinkID: rec.get('ReaGoodsOrgLink_Id'),
				ReaBmsCenOrderDtl_GoodSName: rec.get('ReaGoodsOrgLink_ReaGoods_SName'),
				ReaBmsCenOrderDtl_GoodEName: rec.get('ReaGoodsOrgLink_ReaGoods_EName'),
				ReaBmsCenOrderDtl_ReaGoodsName: rec.get('ReaGoodsOrgLink_ReaGoods_CName'),
				ReaBmsCenOrderDtl_GoodsUnit: rec.get('ReaGoodsOrgLink_ReaGoods_UnitName'),
				ReaBmsCenOrderDtl_UnitMemo: rec.get('ReaGoodsOrgLink_ReaGoods_UnitMemo'),
				ReaBmsCenOrderDtl_ProdOrgName: rec.get('ReaGoodsOrgLink_ReaGoods_ProdOrgName'),

				ReaBmsCenOrderDtl_BarCodeType: rec.get('ReaGoodsOrgLink_BarCodeType'),
				ReaBmsCenOrderDtl_IsPrintBarCode: rec.get('ReaGoodsOrgLink_IsPrintBarCode'),
				ReaBmsCenOrderDtl_ReaGoodsNo: rec.get('ReaGoodsOrgLink_ReaGoods_ReaGoodsNo'),
				ReaBmsCenOrderDtl_ProdGoodsNo: rec.get('ReaGoodsOrgLink_ReaGoods_ProdGoodsNo'),
				ReaBmsCenOrderDtl_CenOrgGoodsNo: rec.get('ReaGoodsOrgLink_CenOrgGoodsNo'),
				ReaBmsCenOrderDtl_GoodsNo: rec.get('ReaGoodsOrgLink_ReaGoods_GoodsNo')
			};
			//理论月用量
			var monthlyUsage = rec.get('ReaGoodsOrgLink_ReaGoods_MonthlyUsage');
			if(monthlyUsage) {
				monthlyUsage = parseFloat(monthlyUsage);
			} else {
				monthlyUsage = 0;
			}
			obj["ReaBmsCenOrderDtl_MonthlyUsage"] = monthlyUsage;
			//当前库存数
			var currentQty = rec.get('CurrentQty');
			if(currentQty) {
				currentQty = parseFloat(currentQty);
			} else {
				currentQty = 0;
			}
			obj["ReaBmsCenOrderDtl_CurrentQty"] = currentQty;

			var goodsQty = rec.get('GoodsQty');
			if(goodsQty) goodsQty = parseFloat(goodsQty);

			var Price = rec.get('ReaGoodsOrgLink_Price');
			if(Price) Price = parseFloat(Price);

			var SumTotal = Price * goodsQty;
			SumTotal = SumTotal ? SumTotal : 0;

			var GoodsSort = rec.get('ReaGoodsOrgLink_ReaGoods_GoodsSort');
			if(GoodsSort) GoodsSort = parseInt(GoodsSort);
			GoodsSort = GoodsSort ? GoodsSort : 0;

			obj["ReaBmsCenOrderDtl_ReqGoodsQty"] = goodsQty;
			obj["ReaBmsCenOrderDtl_GoodsQty"] = goodsQty;
			obj["ReaBmsCenOrderDtl_Price"] = Price;
			obj["ReaBmsCenOrderDtl_SumTotal"] = SumTotal;
			obj["ReaBmsCenOrderDtl_GoodsSort"] = GoodsSort;
			//预期库存量
			var expectedStock = rec.get('ReaBmsCenOrderDtl_ExpectedStock');
			if(expectedStock) {
				expectedStock = parseFloat(expectedStock);
			} else {
				expectedStock = currentQty + goodsQty;
			}
			obj["ReaBmsCenOrderDtl_ExpectedStock"] = expectedStock;
			arr.push(obj);
		}
		if(arr) me.store.add(arr);
		me.fireEvent('onAddAfter', me);
		p.close();
	},
	/**@description 获取明细的保存提交数据*/
	getDtSaveParams: function() {
		var me = this;
		var result = {
			TotalPrice: 0,
			dtAddList: [],
			dtEditList: []
		};
		if(me.store.getCount() <= 0) return result;
		//订单总价
		var TotalPrice = 0;
		me.store.each(function(record) {
			var id = record.get("ReaBmsCenOrderDtl_Id");
			if(!id) id = "-1";
			var entity = {
				"Id": id,
				//"BiddingNo": record.get("ReaBmsCenOrderDtl_BiddingNo"),
				"ReaGoodsID": record.get("ReaBmsCenOrderDtl_ReaGoodsID"),
				"CompGoodsLinkID": record.get("ReaBmsCenOrderDtl_CompGoodsLinkID"),
				"ReaGoodsName": record.get("ReaBmsCenOrderDtl_ReaGoodsName"),
				"GoodSName": record.get("ReaBmsCenOrderDtl_GoodSName"),
				"GoodEName": record.get("ReaBmsCenOrderDtl_GoodEName"),
				
				"GoodsUnit": record.get("ReaBmsCenOrderDtl_GoodsUnit"),
				"UnitMemo": record.get("ReaBmsCenOrderDtl_UnitMemo"),
				"BarCodeType": record.get('ReaBmsCenOrderDtl_BarCodeType'),
				"IsPrintBarCode": record.get('ReaBmsCenOrderDtl_IsPrintBarCode'),
				"ProdOrgName": record.get('ReaBmsCenOrderDtl_ProdOrgName'),
				"ReaGoodsNo": record.get("ReaBmsCenOrderDtl_ReaGoodsNo"),
				"ProdGoodsNo": record.get("ReaBmsCenOrderDtl_ProdGoodsNo"),
				"CenOrgGoodsNo": record.get("ReaBmsCenOrderDtl_CenOrgGoodsNo"),
				"GoodsNo": record.get("ReaBmsCenOrderDtl_GoodsNo")
			};
			//理论月用量
			var monthlyUsage = record.get('ReaBmsCenOrderDtl_MonthlyUsage');
			if(monthlyUsage) {
				monthlyUsage = parseFloat(monthlyUsage);
			} else {
				monthlyUsage = 0;
			}
			entity["MonthlyUsage"] = monthlyUsage;
			//当前库存数
			var currentQty = record.get('ReaBmsCenOrderDtl_CurrentQty');
			if(currentQty) {
				currentQty = parseFloat(currentQty);
			} else {
				currentQty = 0;
			}
			entity["CurrentQty"] = currentQty;
			//要求到货时间
			if(record.get("ReaBmsCenOrderDtl_ArrivalTime")) {
				entity.ArrivalTime = JShell.Date.toServerDate(record.get("ReaBmsCenOrderDtl_ArrivalTime"));
			}

			var reqGoodsQty = record.get('ReaBmsCenOrderDtl_ReqGoodsQty');
			if(reqGoodsQty) {
				reqGoodsQty = parseFloat(reqGoodsQty);
			} else {
				reqGoodsQty = 0;
			}
			entity["ReqGoodsQty"] = reqGoodsQty;

			var goodsQty = record.get('ReaBmsCenOrderDtl_GoodsQty');
			if(goodsQty) {
				goodsQty = parseFloat(goodsQty);
			} else {
				goodsQty = 0;
			}

			var price = record.get('ReaBmsCenOrderDtl_Price');
			if(price) price = parseFloat(price);
			var SumTotal = price * goodsQty;
			TotalPrice += parseFloat(SumTotal);

			entity["GoodsQty"] = goodsQty;
			entity["Price"] = price;
			entity["SumTotal"] = SumTotal;

			var GoodsSort = record.get('ReaBmsCenOrderDtl_GoodsSort');
			if(GoodsSort) GoodsSort = parseInt(GoodsSort);
			GoodsSort = GoodsSort ? GoodsSort : 0;
			entity["GoodsSort"] = GoodsSort;
			//预期库存量
			var expectedStock = record.get('ReaBmsCenOrderDtl_ExpectedStock');
			if(expectedStock) {
				expectedStock = parseFloat(expectedStock);
			} else {
				expectedStock = currentQty + goodsQty;
			}
			entity["ExpectedStock"] = expectedStock;
			if(id && id != "-1") {
				result.dtEditList.push(entity);
			} else {
				result.dtAddList.push(entity);
			}
		});
		result.TotalPrice = TotalPrice;
		return result;
	},
	/**@description 获取明细的保存提交数据*/
	getSaveParams: function(result) {
		var me = this;
		if(!result) result = me.getDtSaveParams();
		if(me.fromtype == "add") {
			me.PK = -1;
			me.Status = "0";
		}
		var entity = {
			"Id": me.PK,
			"Status": me.Status,
			TotalPrice: result.TotalPrice
		};
		var fields = "Id,Status,TotalPrice,ProdOrgName,ArrivalTime";
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
		JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
			me.hideMask(); //隐藏遮罩层
			callback(data);
		});
	},
	deleteOne: function(record,index) {
		var me = this;
		me.delErrorCount = 0;
		me.delCount = 0;
		me.delLength = 1;
		var showMask = false;
		var id = record.get(me.PKField);
		if(!id || id == "-1") {
			me.delCount++;
			me.store.remove(record);
			if((me.delCount + me.delErrorCount) == me.delLength && me.delErrorCount == 0) {
				me.fireEvent('onDelAfter', me);
			}
		} else {
			if(showMask == false) {
				showMask = true;
				me.showMask(me.delText); //显示遮罩层
			}
			me.delOneById(record, index, id);
		}
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
					if(me.delErrorCount != 0) {
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					} else {
						me.fireEvent('onDelAfter', me);
					}
				}
			});
		}, 100 * index);
	},
	/**订单选择改变或部门选择改变后更新模板选择的配置项*/
	setGoodstemplateClassConfig: function(isSearch) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar) {
			var cbo = buttonsToolbar.getComponent("cboGoodstemplate");
			if(cbo) {
				cbo.setValue("");
				cbo.changeClassConfig({
					"OrgID": me.ReaCompID
				});
				var picker=cbo.getPicker();
				if(picker){
					picker.OrgID=me.ReaCompID;
					if(isSearch==true)picker.onSearch();
				}
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
		if(!me.ReaCompID) {
			JShell.Msg.error('获取供应商信息为空!');
			return;
		}
		var tempArr = [];
		me.store.each(function(record) {
			var data = {};
			data = Ext.apply(data, record.data);
			data["ReaBmsCenOrderDtl_Id"] = "";
			tempArr.push(data);
		});
		var config = {
			resizable: true,
			formtype: "add",
			OrgID: me.ReaCompID,
			OrgName: me.ReaCompCName,
			TemplateType: "ReaOrderDtl",
			ContextJson: JcallShell.JSON.encode(tempArr),
			listeners: {
				save: function(p, id) {
					var buttonsToolbar = me.getComponent("buttonsToolbar");
					var cbo = buttonsToolbar.getComponent("cboGoodstemplate");
					if(cbo) cbo.getPicker().onSearch();
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.goodstemplate.ApplyForm', config);
		win.show();
	},
	
	/**@description 获取申请货品明细的库存数量*/
	loadByReaGoodsNo: function(record) {
		var me = this;
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		var cbo = buttonsToolbar.getComponent("cboGoodstemplate");
		if(record) cbo.setValue(record.get("ReaChooseGoodsTemplate_CName"));
		else cbo.setValue("");
		if(!record) return;
		
		var contextJson = record.get("ReaChooseGoodsTemplate_ContextJson");
		var templateId=record.get("ReaChooseGoodsTemplate_Id");
		if(contextJson) contextJson = JcallShell.JSON.decode(contextJson);
		if(!contextJson){
			contextJson = record.get("ReaChooseGoodsTemplate_ContextJson");
			contextJson=contextJson.replace(/\\'/g, '’').replace(/\\"/g, '”');;
			try{
				contextJson =JcallShell.JSON.decode(contextJson);
			}catch (e) {
				contextJson ="";
			}
		}
		
		var url = me.getModelLoadUrl(templateId);
		
		if(Ext.typeOf(contextJson)!='undefined'){
			
		  JShell.Server.get(url, function(data) {
			if(data.success) {
				var list = data.value.list;
				for(var i=0;i<contextJson.length;i++){
					for(var j=0;j<list.length;j++){
						if(contextJson[i]["ReaBmsCenOrderDtl_ReaGoodsID"] == list[j]["ReaGoodsOrgLink_ReaGoods_Id"]){
							contextJson[i]["ReaBmsCenOrderDtl_CurrentQty"]=list[j]["ReaGoodsOrgLink_ReaGoods_GoodsQty"];
							contextJson[i]["ReaBmsCenOrderDtl_Price"]=list[j]["ReaGoodsOrgLink_Price"];
							contextJson[i]["ReaBmsCenOrderDtl_MonthlyUsage"]=list[j]["ReaGoodsOrgLink_ReaGoods_MonthlyUsage"];
							contextJson[i]["ReaBmsCenOrderDtl_ReaGoodsName"]=list[j]["ReaGoodsOrgLink_ReaGoods_CName"];
							contextJson[i]["ReaBmsCenOrderDtl_GoodSName"]=list[j]["ReaGoodsOrgLink_ReaGoods_SName"];
							contextJson[i]["ReaBmsCenOrderDtl_GoodEName"]=list[j]["ReaGoodsOrgLink_ReaGoods_EName"];
							contextJson[i]["ReaBmsCenOrderDtl_GoodsUnit"]=list[j]["ReaGoodsOrgLink_ReaGoods_UnitName"];
							contextJson[i]["ReaBmsCenOrderDtl_UnitMemo"]=list[j]["ReaGoodsOrgLink_ReaGoods_UnitMemo"];
							contextJson[i]["ReaBmsCenOrderDtl_ReaGoodsNo"]=list[j]["ReaGoodsOrgLink_ReaGoods_ReaGoodsNo"];
							contextJson[i]["ReaBmsCenOrderDtl_IsPrintBarCode"]=list[j]["ReaGoodsOrgLink_IsPrintBarCode"];
							contextJson[i]["ReaBmsCenOrderDtl_BarCodeType"]=list[j]["ReaGoodsOrgLink_BarCodeType"];
							contextJson[i]["ReaBmsCenOrderDtl_ProdOrgName"]=list[j]["ReaGoodsOrgLink_ReaGoods_ProdOrgName"];
							contextJson[i]["ReaBmsCenOrderDtl_GoodsNo"]=list[j]["ReaGoodsOrgLink_ReaGoods_GoodsNo"];
							contextJson[i]["ReaBmsCenOrderDtl_CenOrgGoodsNo"]=list[j]["ReaGoodsOrgLink_CenOrgGoodsNo"];
							contextJson[i]["ReaBmsCenOrderDtl_ProdGoodsNo"]=list[j]["ReaGoodsOrgLink_ReaGoods_ProdGoodsNo"];
						}
					}
				}
				Ext.Array.each(contextJson, function(data) {
					var goodId = data["ReaBmsCenOrderDtl_ReaGoodsID"];
					var indexOf = -1;
					if(goodId) indexOf = me.store.findExact("ReaBmsCenOrderDtl_ReaGoodsID", goodId);
				//模板的货品不存在明细列表里
					if(goodId && indexOf == -1) {
						data["ReaBmsCenOrderDtl_Id"] = "";
						me.store.add(data);
					}
				});
			}
		  });
		}
	},
	/**获取带查询参数的URL*/
	getModelLoadUrl: function(templateId) {
		var me = this,
			arr = [];
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectModelUrl;
			
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=ReaGoodsOrgLink_ReaGoods_Id,ReaGoodsOrgLink_ReaGoods_ReaGoodsNo,ReaGoodsOrgLink_ReaGoods_CName,ReaGoodsOrgLink_ReaGoods_SName,ReaGoodsOrgLink_ReaGoods_EName,ReaGoodsOrgLink_ReaGoods_MonthlyUsage,ReaGoodsOrgLink_ReaGoods_ProdOrgName,ReaGoodsOrgLink_ReaGoods_UnitName,ReaGoodsOrgLink_ReaGoods_UnitMemo,ReaGoodsOrgLink_Price,ReaGoodsOrgLink_ReaGoods_ProdGoodsNo,ReaGoodsOrgLink_CenOrgGoodsNo,ReaGoodsOrgLink_ReaGoods_GoodsNo,ReaGoodsOrgLink_IsPrintBarCode,ReaGoodsOrgLink_BarCodeType,ReaGoodsOrgLink_ReaGoods_CurrentQty,ReaGoodsOrgLink_ReaGoods_GoodsQty';
		var modelWhere = " reagoodsorglink.Visible=1 and reagoodsorglink.CenOrg.OrgType=0 and reagoodsorglink.CenOrg.Id=" + me.ReaCompID;
		if(modelWhere){
			arr.push(modelWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}
		if(templateId){
			url+='&templateId='+JShell.String.encode(templateId);
		}
	
		return url;
	}

});