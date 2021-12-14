/**
 * @description 实验室导入平台供货单（新平台）
 * @author longfc
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.reasale.lab.import.DtlGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.SimpleComboBox'
	],
	title: '导入供货单',
	/**关闭后是否联动*/
	IsloadData:false,
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**是否启用序号列*/
	hasRownumberer: false,

	hasDel: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'DispOrder',
		direction: 'ASC'
	}],
	/**默认每页数量*/
	defaultPageSize: 10000,
	/**分页栏下拉框数据*/
	pageSizeList: [
		[10000, 10000]
	],
	/**选择的供应商ID*/
	ReaCompID: null,
	/**选择的供应商*/
	ReaServerCompCode: null,
	/**选择的供应商所属平台编码*/
	ReaCompanyName: null,
	/**供货明细的货品平台编码*/
	GoodsNoArr: [],
	/**提取平台的供货信息*/
	saleData: {
		"saleDoc": {}, //供货总单基本信息
		"saleDtlList": [], //原平台供货明细集合
		"SaleDtlIdList": []//更新的平台供货明细Id
	},
	/**提取平台供货单信息服务*/
	getExtractUrl: '/ZFReaRestfulService.svc/RS_Client_GetBmsCenSaleDoc',
	/**更新平台供货单提取标志服务*/
	editExtractFlagUrl: '/ZFReaRestfulService.svc/RS_Client_UpdateBmsCenSaleDocExtractFlag',
	/**访问BS平台的URL*/
	BSPlatformURL:null,
	/**用户UI配置Key*/
	userUIKey: 'reasale.lab.import.DtlGrid',
	/**用户UI配置Name*/
	userUIName: "供货明细列表",
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//自定义按钮功能栏
		me.buttonToolbarItems = me.buttonToolbarItems || [{
			fieldLabel: '供应商选择',
			emptyText: '必选项',
			allowBlank: false,
			itemId: 'ReaCompName',
			xtype: 'trigger',
			triggerCls: 'x-form-search-trigger',
			enableKeyEvents: false,
			editable: false,
			labelWidth: 75,
			width: 285,
			value: me.ReaCompanyName,
			onTriggerClick: function() {
				JShell.Win.open('Shell.class.rea.client.reacenorg.CheckTree', {
					resizable: false,
					/**是否显示根节点*/
					rootVisible: false,
					/**机构类型*/
					OrgType: "0",
					ShowPlatformOrgNo:true,
					value: me.ReaCompanyName,
					listeners: {
						accept: function(p, record) {
							if(record && record.get("tid") == 0) {
								JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
							me.onCompAccept(record);
							p.close();
							me.onExtractSaleDoc();
						}
					}
				}).show();
			}
		}, {
			xtype: 'textfield',
			width: 280,
			emptyText: '供货单号',
			fieldLabel: '',
			name: 'txtSaleDocNo',
			itemId: 'txtSaleDocNo',
			listeners: {
				specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER)
						me.onExtractSaleDoc();
				}
			}
		}, {
			xtype: 'button',
			iconCls: 'button-search',
			text: '提取',
			tooltip: '查询平台供货信息',
			handler: function() {
				me.onExtractSaleDoc();
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'button-check',
			style: {
				marginLeft: "15px"
			},
			text: '确认保存',
			tooltip: '确认保存提取的供货信息',
			hidden:true,
			handler: function() {
				me.onExtractSave();
			}
		}, '-', {
			xtype: 'button',
			iconCls: 'button-reset',
			style: {
				marginLeft: "15px"
			},
			text: '清空',
			tooltip: '清空当前提取的供货信息',
			handler: function() {
				me.clearData();
			}
		}];
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'PSaleDtlID',
			sortable: false,
			text: '上级ID',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'Id',
			sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'SaleDtlNo',
			sortable: false,
			text: '供货明细单号',
			hidden: true,
			defaultRenderer: true
		}, {
			dataIndex: 'BarCodeType',
			text: '条码类型',
			width: 60,
			hidden: true,
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
			dataIndex: 'GoodsName',
			sortable: false,
			text: '货品名称',
			width: 160,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("BarCodeType");
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
			dataIndex: 'ShortCode',
			sortable: false,
			text: '货品简码',
			width: 80,
			defaultRenderer: true
		},{
			dataIndex: 'EName',
			sortable: false,
			text: '货品英文名',
			hidden: true,
			hideable: false
		},  {
			dataIndex: 'ReaGoodsNo',
			sortable: false,
			text: '货品编码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ProdGoodsNo',
			sortable: false,
			text: '厂商货品编码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'CenOrgGoodsNo',
			sortable: false,
			text: '供货商货品编码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'GoodsNo',
			text: '货品平台码',
			sortable: false,
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'LotNo',
			sortable: false,
			text: '货品批号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'InvalidDate',
			sortable: false,
			text: '有效期',
			width: 90,
			isDate: true
		}, {
			dataIndex: 'GoodsUnit',
			sortable: false,
			text: '包装单位',
			width: 80,
			defaultRenderer: true
		}, {
			dataIndex: 'UnitMemo',
			sortable: false,
			text: '包装规格',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'GoodsQty',
			sortable: false,
			text: '数量',
			align: 'right',
			align: 'center',
			type: 'float',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ContractPrice',
			sortable: false,
			text: '合同单价',
			align: 'right',
			width: 60,
			renderer: function(value, meta, record) {
				var contractPrice = value;
				if(!contractPrice) contractPrice = 0;
				var price = record.get("Price");
				if(!price) price = 0;
				var style = 'font-weight:bold;';
				var bColor = "";
				if(contractPrice < price) {
					bColor = "#f4c600";
				} else if(contractPrice > price) {
					bColor = "#dd6572";
				}
				if(bColor) {
					style = style + "background-color:" + bColor + ";color:#ffffff";
					meta.style = style;
				}
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return value;
			}
		}, {
			dataIndex: 'Price',
			sortable: false,
			text: '供货单价',
			align: 'right',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'SumTotal',
			sortable: false,
			text: '总计金额',
			align: 'right',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'TaxRate',
			sortable: false,
			text: '税率',
			align: 'right',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'ProdDate',
			sortable: false,
			text: '生产日期',
			align: 'center',
			width: 90,
			isDate: true
		}, {
			dataIndex: 'BiddingNo',
			sortable: false,
			text: '招标号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'LotSerial',
			sortable: false,
			text: '批号条码',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Memo',
			sortable: false,
			text: '备注',
			hidden: true,
			hideable: false,
			renderer: function(value, meta, record) {
				return "";
			}
		}];
		//客户端货品信息
		columns.push({
			dataIndex: 'BarcodeOperationList',
			sortable: false,
			text: '供货明细条码集合Str',
			hidden: true,
			editor: {
				readOnly: true
			},
			defaultRenderer: true
		}, {
			dataIndex: "ContrastSign",
			text: '对照标志',
			width: 55,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			renderer: function(value, meta, record) {
				var v = '';
				if(value === 'true') {
					v = '<b style="color:green">' + JShell.All.SUCCESS_TEXT + '</b>';
				}
				if(value === 'false') {
					v = '<b style="color:red">' + JShell.All.FAILURE_TEXT + '</b>';
				}
				var msg = record.get('ErrorInfo');
				if(msg) {
					meta.tdAttr = 'data-qtip="<b style=\'color:red\'>' + msg + '</b>"';
				}

				return v;
			}
		}, {
			dataIndex: 'ErrorInfo',
			text: '对照信息',
			hidden: true,
			hideable: false,
			sortable: false,
			menuDisabled: true
		}, {
			dataIndex: 'ReaGoodsID',
			text: '本地货品ID',
			hidden: true,
			hideable: false,
			sortable: false,
			menuDisabled: true
		}, {
			dataIndex: 'ReaGoodsName',
			text: '本地货品名称',
			hidden: true,
			hideable: false,
			sortable: false,
			menuDisabled: true
		}, {
			dataIndex: 'CompGoodsLinkID',
			text: '货品机构关系ID',
			hidden: true,
			hideable: false,
			sortable: false,
			menuDisabled: true
		}, {
			dataIndex: 'CenOrgGoodsNo',
			text: '供货商货品编码',
			hidden: true,
			hideable: false,
			sortable: false,
			menuDisabled: true
		});
		return columns;
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;
		if(records && records.length > 0) {
			me.onConversionSaleInfo();
		}
		me.callParent(arguments);
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function() {
		var me = this;
		
		return data;
	},
	/**查询数据*/
	onSearch: function(autoSelect) {
		var me = this;
		me.onExtractSaleDoc();
	},
	onCompAccept: function(record) {
		var me = this;
		var ComName = me.getComponent('buttonsToolbar').getComponent('ReaCompName');
		me.ReaCompID = record ? record.get('tid') : '';
		me.ReaServerCompCode = record ? record.data.value.PlatformOrgNo : '';
		var text = record ? record.get('text') : '';
		if(text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		me.ReaCompanyName = text;
		ComName.setValue(text);
	},
	/***提取供货单信息*/
	onExtractSaleDoc: function() {
		var me = this;
		var saleDocNo = me.getComponent('buttonsToolbar').getComponent('txtSaleDocNo').getValue();
		if(!saleDocNo) {
			JShell.Msg.alert('供货单号为空！', null, 2000);
			return;
		}
		if(!me.ReaCompID) {
			JShell.Msg.alert('获取供应商信息为空！', null, 2000);
			return;
		}
		//
		
	},
	/***设置供货单值*/
	onSetSaleDocNoValue: function(value) {
		var me = this;
		var saleDocNo = me.getComponent('buttonsToolbar').getComponent('txtSaleDocNo');
		saleDocNo.setValue(value);
		saleDocNo.focus();
	},
	/***提取平台供货单信息*/
	onExtractOfSaleDocNo: function() {
		var me = this;
		var saleDocNo = me.getComponent('buttonsToolbar').getComponent('txtSaleDocNo').getValue();
		if(!saleDocNo) {
			JShell.Msg.alert('供货单号为空！', null, 2000);
			return;
		}
		if(!me.ReaServerCompCode) {
			JShell.Msg.alert('获取供应商的平台编码为空！', null, 2000);
			return;
		}
		me.clearData();

		//机构所属平台编码
		var labOrgNo = JcallShell.REA.System.CENORG_CODE;

		var data1 = {
			"CompOrgNo": me.ReaServerCompCode,
			"SaleDocNo": saleDocNo,
			"LabOrgNo": labOrgNo
		};
		data1 = Ext.JSON.encode(data1);
		var params = {
			"appkey": "",
			"timestamp": "",
			"token": "",
			"version": "",
			"data": data1
		};
		var url = me.BSPlatformURL + me.getExtractUrl;
		JShell.Server.post(url, Ext.JSON.encode(params), function(response) {
			var result = Ext.JSON.decode(response.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, ''));
			if(result.success && result.data) {
				//result.data=result.data.replace(/[null]/g, '');
				var data = Ext.JSON.decode(result.data);
				if(!data) {
					JShell.Msg.error("解析提取的供货单信息出错!");
					me.onSetSaleDocNoValue("");
					return;
				}
				//重新处理供货单基本信息
				me.onConversionSaleInfo(data);
			} else {
				JShell.Msg.error(result.message);
				me.onSetSaleDocNoValue("");
			}
		}, false, null, true);
	},
	/***转换平台供货单信息*/
	onConversionSaleInfo: function(data) {},
	/**转换供货总单信息*/
	onConverSaleDoc: function(data) {
		var me = this;
		me.saleData.saleDoc = {
			"SaleDocNo": data.SaleDocNo,
			"OrderDocNo": data.OrderDocNo,
			"OtherOrderDocNo": data.OtherOrderDocNo,
			"UserName": data.UserName,
			"UrgentFlag": data.UrgentFlag,
			"UrgentFlagName": data.UrgentFlagName,
			"OperDate": data.OperDate,
			"Memo": data.Memo,
			"Source": data.Source,
			"InvoiceNo": data.InvoiceNo,
			"TotalPrice": data.TotalPrice,
			"Sender": data.Sender,
			"DeptName": data.DeptName,
			"Id": -1,
			"ReaCompID": me.ReaCompID,
			"ReaCompanyName": me.ReaCompanyName,
			"ReaServerCompCode": me.ReaServerCompCode,
			"CenSaleDocID": data.Id
		};
		//操作日期转换
		//if(data.OperDate) me.saleData.saleDoc.OperDate = JcallShell.Date.getDate(data.OperDate);
	},
	loadData: function() {
		var me = this;
		
	},
	/**供货条码操作记录封装*/
	getOperationVO: function(item) {
		var me = this;
		return operationVO = {
			"Id": -1,
			"BDocID": -1,
			"BDtlID": -1,
			"BDocNo": me.saleData.saleDoc.SaleDocNo,
			"OperTypeID": 1, //货品条码操作类型:供货						
			"SysPackSerial": "",
			"OtherPackSerial": item.MixSerial,
			"UsePackSerial": item.MixSerial,
			"LotNo": item.LotNo
		};
	},
	/**按本地供应商ID及货品平台编码获取对照的供应商与货品信关系的信息*/
	getReaGoodsOrgLink: function() {
		var me = this;
		var goodsNoStr = me.GoodsNoArr.join(",");
		var url = JShell.System.Path.ROOT + "/ReaManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByReaCompIDAndGoodsNoStr";
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'goodsNoStr=' + goodsNoStr + "&reaCompID=" + me.ReaCompID;
		setTimeout(function() {
			JShell.Server.get(url, function(result) {
				if(result.success) {
					//客户端货品与货品平台对照及赋值
					var listLink = result.value;
					me.contrastData(listLink);
				} else {
					JShell.Msg.error(result.msg);
					me.onSetSaleDocNoValue("");
				}
			});
		}, 10);
	},
	contrastData: function(listLink) {
		var me = this;
		
	},
	/**按本地供应商ID及货品平台编码获取对照的供应商与货品信关系的信息*/
	onExtractSave: function() {},
	/***更新平台供货单提取标志*/
	editPlatformBmsCenSaleDoc: function() {
		var me = this;
		var data1 = {
			"SaleDocId": me.saleData.saleDoc.CenSaleDocID,
			"SaleDtlIdList": me.saleData.SaleDtlIdList.join(",")
		};
		data1 = Ext.JSON.encode(data1);
		var params = {
			"appkey": "",
			"timestamp": "",
			"token": "",
			"version": "",
			"data": data1
		};
		var url = me.BSPlatformURL + me.editExtractFlagUrl;
		JShell.Server.post(url, Ext.JSON.encode(params), function(response) {
			var result = Ext.JSON.decode(response.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, ''));
			if(result.success) {
				me.clearData();
			} else {
				JShell.Msg.error("更新平台供货单提取标志出错!" + result.message);
			}
		}, false, null, true);
	},
	/**
	 * @description 清空货品明细
	 * @param {Object} record
	 */
	clearData: function() {
		var me = this;
		me.store.removeAll();
		me.GoodsNoArr = [];
		me.lastData = [];
		me.saleData = {
			"saleDoc": {},
			"saleDtlList": [],
			"SaleDtlIdList":[]
		};
	}
});