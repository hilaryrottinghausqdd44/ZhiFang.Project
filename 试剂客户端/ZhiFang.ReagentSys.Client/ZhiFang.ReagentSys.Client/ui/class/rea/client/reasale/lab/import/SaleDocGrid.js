/**
 * @description 实验室导入平台供货单（新平台）
 * @author longfc
 * @version 2018-02-27
 */
Ext.define('Shell.class.rea.client.reasale.lab.import.SaleDocGrid', {
	extend: 'Shell.class.rea.client.basic.CheckPanel',

	requires: [
		'Shell.ux.form.field.DateArea',
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '实验室导入平台供货单',

	/**数据库不独立部署:获取数据服务路径*/
	//selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocByHQL?isPlanish=true',
	/**数据库独立部署:获取数据服务路径*/
	selectUrl: '/ZFReaRestfulService.svc/RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocByHQL?isPlanish=true',

	/**默认加载*/
	defaultLoad: false,
	/**是否多选行*/
	checkOne: true,

	/**选择的供应商ID*/
	ReaCompID: null,
	/**选择的供应商*/
	ReaServerCompCode: null,
	/**选择的供应商所属平台编码*/
	ReaCompanyName: null,
	/**客户端供货单及明细状态Key*/
	StatusKey: "ReaBmsCenSaleDocAndDtlStatus",
	/**客户端供货单及明细数据标志Key*/
	IOFlagKey: "ReaBmsCenSaleDocIOFlag",

	/**排序字段*/
	defaultOrderBy: [{
		property: 'ReaBmsCenSaleDoc_DataAddTime',
		direction: 'DESC'
	}],
	/**用户UI配置Key*/
	userUIKey: 'reasale.lab.import.SaleDocGrid',
	/**用户UI配置Name*/
	userUIName: "供货列表",
	BUTTON_CAN_CLICK:true,
	
	setCheckboxModel: function() {
		var me = this;
		//复选框
		me.multiSelect = true;
		me.selType = 'checkboxmodel';
	},
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//系统运行参数"数据库是否独立部署"
		JcallShell.REA.RunParams.getRunParamsValue("ReaDataBaseIsDeploy", false, function(data) {
			//数据库是否独立部署: 1: 是;2: 否;
			var isDeploy = "" + JcallShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
			if (isDeploy == "1") {
				me.selectUrl = '/ZFReaRestfulService.svc/RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocByHQL?isPlanish=true';
				//访问BS平台的URL
				JcallShell.REA.RunParams.getRunParamsValue("BSPlatformURL", false, function(data2) {
					
				});
			} else if (isDeploy == "2") {
				me.selectUrl = '/ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocByHQL?isPlanish=true';
			}
			me.onSearch();
		});
	},
	initComponent: function() {
		var me = this;

		//查询框信息
		me.searchInfo = {
			width: 280,
			isLike: true,
			itemId: 'search',
			emptyText: '供货单号',
			fields: ['reabmscensaledoc.SaleDocNo']
		};
		
		JShell.REA.StatusList.getStatusList(me.StatusKey, false, true, null);
		JShell.REA.StatusList.getStatusList(me.IOFlagKey, false, true, null);
		
		if (!me.checkOne) me.setCheckboxModel();
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaBmsCenSaleDoc_DataAddTime',
			text: '供货日期',
			align: 'center',
			width: 95,
			isDate: true,
			hasTime: false
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_CompanyName',
			text: '供货商名称',
			width: 95,
			hideable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_LabcName',
			text: '订货方名称',
			width: 95,
			hideable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_Status',
			text: '单据状态',
			width: 90,
			renderer: function(value, meta) {
				var v = value;
				if (JShell.REA.StatusList.Status[me.StatusKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.StatusKey].Enum[value];
				var bColor = "";
				if (JShell.REA.StatusList.Status[me.StatusKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.StatusKey].BGColor[value];
				var fColor = "";
				if (JShell.REA.StatusList.Status[me.StatusKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.StatusKey].FColor[value];
				var style = 'font-weight:bold;';
				if (bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if (fColor) {
					style = style + "color:" + fColor + ";";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_SaleDocNo',
			text: '供货单号',
			width: 130,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_OrderDocNo',
			text: '订货单号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_TotalPrice',
			text: '总价',
			width: 100,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '操作',
			align: 'center',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-show hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					me.onShowOperation(rec);
				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '提取',
			align: 'center',
			width: 40,
			hideable: false,
			sortable: false,
			menuDisabled: true,
			items: [{
				iconCls: 'button-import hand',
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					//数据库是否独立部署: 1: 是;2: 否;
					var isDeploy = "" + JcallShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
					if (isDeploy == "1") {
						me.onExtractSaleDocOfDeploy(rec, function(result) {
							//me.onSearch();							
						});
					} else if (isDeploy == "2") {
						me.onExtractSaleDocBySaleDocId(rec);
					}
				}
			}]
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_UrgentFlag',
			text: '紧急标志',
			align: 'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_UrgentFlag['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_IOFlag',
			text: '数据标志',
			align: 'center',
			width: 75,
			renderer: function(value, meta) {
				var v = value;
				if (JShell.REA.StatusList.Status[me.IOFlagKey].Enum != null)
					v = JShell.REA.StatusList.Status[me.IOFlagKey].Enum[value];
				var bColor = "";
				if (JShell.REA.StatusList.Status[me.IOFlagKey].BGColor != null)
					bColor = JShell.REA.StatusList.Status[me.IOFlagKey].BGColor[value];
				var fColor = "";
				if (JShell.REA.StatusList.Status[me.IOFlagKey].FColor != null)
					fColor = JShell.REA.StatusList.Status[me.IOFlagKey].FColor[value];
				var style = 'font-weight:bold;';
				if (bColor) {
					style = style + "background-color:" + bColor + ";";
				}
				if (fColor) {
					style = style + "color:" + fColor + ";";
				}
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = style;
				return v;
			}
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_CompID',
			text: '供货商主键ID',
			hidden: true,
			//hideable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_ReaServerCompCode',
			text: '供货商平台编码',
			width: 100,
			hideable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_LabcID',
			text: '订货方主键ID',
			hidden: true,
			hideable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaBmsCenSaleDoc_ReaServerLabcCode',
			text: '订货方平台编码',
			width: 100,
			hideable: false,
			defaultRenderer: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	initButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-'];
		//订货方
		items.push({
			fieldLabel: '供应商选择',
			emptyText: '必选项',
			allowBlank: false,
			itemId: 'ReaCompName',
			name: 'ReaCompName',
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
					ShowPlatformOrgNo: true,
					value: me.ReaCompanyName,
					listeners: {
						accept: function(p, record) {
							if (record && record.get("tid") == 0) {
								JShell.Msg.alert('不能选择所有机构根节点', null, 2000);
								return;
							}
							me.onCompAccept(record);
							p.close();
						}
					}
				}).show();
			}
		});
		items.push({
			type: 'search',
			info: me.searchInfo
		}, '-', {
			xtype: 'button',
			iconCls: 'button-search',
			text: '查询',
			tooltip: '查询操作',
			handler: function() {
				me.onSearch();
			}
		});
		items.push('->', 'accept');
		me.buttonToolbarItems = items;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.getView().update();

		var labOrgNo = JShell.REA.System.CENORG_CODE;
		if (!labOrgNo) {
			var error = me.errorFormat.replace(/{msg}/, "获取机构平台编码信息为空!");
			me.getView().update(error);
			return false;
		};
		if (!labOrgNo) labOrgNo = '';
		//供应商自己录入的供货单(数据来源为供应商:1) 并且 供货单状态为审核通过,及数据标志为"未提取"或"部分提取" reabmscensaledoc.Source=1 and 
		me.defaultWhere = "reabmscensaledoc.Status=4 and (reabmscensaledoc.IOFlag=0 or reabmscensaledoc.IOFlag=2)";
		//供货单的订货方为当前机构的所属机构平台编码
		me.defaultWhere = me.defaultWhere + " and reabmscensaledoc.ReaServerLabcCode='" + labOrgNo + "'";

		me.store.proxy.url = me.getLoadUrl(); //查询条件
		me.disableControl(); //禁用 所有的操作功能
		if (!me.defaultLoad) return false;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var search = buttonsToolbar.getComponent('search');
		var where = [];
		if (me.ReaServerCompCode) {
			where.push("reabmscensaledoc.ReaServerCompCode='" + me.ReaServerCompCode + "'");
		}
		if (search) {
			var value = search.getValue();
			if (value) {
				var searchHql = me.getSearchWhere(value);
				if (searchHql) {
					searchHql = "(" + searchHql + ")";
					where.push(searchHql);
				}
			}
		}
		me.internalWhere = where.join(" and ");

		var url = me.callParent(arguments);
		//数据库是否独立部署: 1: 是;2: 否;
		var isDeploy = "" + JcallShell.REA.RunParams.Lists["ReaDataBaseIsDeploy"].Value;
		if (isDeploy == "1") {
			var labcCode = JcallShell.REA.System.CENORG_CODE;
			if (!labcCode) labcCode = "";
			var compCode = me.ReaServerCompCode;
			if (!compCode) compCode = "";
			var platformUrl = JShell.REA.RunParams.Lists["BSPlatformURL"].Value;
			url = url + "&platformUrl=" + platformUrl + "&labcCode=" + labcCode + "&compCode=" + compCode;
		}

		return url;
	},
	/**@description 供货商选择*/
	onCompAccept: function(record) {
		var me = this;
		var id = record ? record.get('tid') : '';
		var text = record ? record.get('text') : '';
		var platformOrgNo = record ? record.data.value.PlatformOrgNo : '';

		if (text && text.indexOf("]") >= 0) {
			text = text.split("]")[1];
			text = Ext.String.trim(text);
		}
		me.ReaCompID = id;
		me.ReaCompanyName = text;
		me.ReaServerCompCode = platformOrgNo;
		me.getComponent('buttonsToolbar').getComponent('ReaCompName').setValue(text);
		if(id&&id.length>0){
			me.onSearch();
		}
	},
	/**@description 操作记录查看*/
	onShowOperation: function(record) {
		var me = this;
		if (!record) {
			var records = me.getSelectionModel().getSelection();
			if (records.length != 1) {
				JcallShell.Msg.error(JcallShell.All.CHECK_ONE_RECORD);
				return;
			}
			record = records[0];
		}
		//临时,已撤销申请,已撤消审核,可以修改
		var id = record.get("ReaBmsCenOrderDoc_Id");
		var config = {
			title: '订单操作记录',
			resizable: true,
			width: 428,
			height: 390,
			PK: id,
			className: 'ReaBmsCenSaleDocAndDtlStatus' //类名
		};
		var win = JcallShell.Win.open('Shell.class.rea.client.reareqoperation.Panel', config);
		win.show();
	},
	/**@description 通过选择供货单提取供货单(实验室与平台在同一数据库)*/
	onExtractSaleDocBySaleDocId: function(rec) {
		var me = this;
		if (!me.BUTTON_CAN_CLICK) return;
		
		var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_UDTO_UpdateReaBmsCenSaleDocOfExtract";
		var id = rec.get("ReaBmsCenSaleDoc_Id");
		var reaServerCompCode = rec.get("ReaBmsCenSaleDoc_ReaServerCompCode");
		var saleDocNo = rec.get("ReaBmsCenSaleDoc_SaleDocNo");
		var reaServerLabcCode = JcallShell.REA.System.CENORG_CODE;
		if (!reaServerLabcCode) reaServerLabcCode = "";
		var params = {
			"saleDocId": id,
			"saleDocNo": saleDocNo,
			"reaServerCompCode": reaServerCompCode,
			"reaServerLabcCode": reaServerLabcCode
		};
		
		me.BUTTON_CAN_CLICK = false; //不可点击	
		me.showMask("供货提取中...");
		JcallShell.Action.delay(function() {
			JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
				me.hideMask();
				me.BUTTON_CAN_CLICK = true;
				if (data.success) {
					me.onSearch();
				} else {
					JShell.Msg.error(data.msg);
				}
			});
		}, null, 100);
	},
	/**提取平台供货单到客户端(客户端与平台不在同一数据库)*/
	onExtractSaleDocOfDeploy: function(rec, callback) {
		var me = this;
		if (!me.BUTTON_CAN_CLICK) return;
		
		var url = JShell.System.Path.ROOT + "/ZFReaRestfulService.svc/RS_UDTO_AddSaleDocAndDtlOfPlatformExtract";
		var id = rec.get("ReaBmsCenSaleDoc_Id");
		var reaServerCompCode = rec.get("ReaBmsCenSaleDoc_ReaServerCompCode");
		var saleDocNo = rec.get("ReaBmsCenSaleDoc_SaleDocNo");
		var reaServerLabcCode = JcallShell.REA.System.CENORG_CODE;
		if (!reaServerLabcCode) reaServerLabcCode = "";
		var platformUrl = JShell.REA.RunParams.Lists["BSPlatformURL"].Value;
		var params = {
			"platformUrl": platformUrl,
			"saleDocId": id,
			"saleDocNo": saleDocNo,
			"compCode": reaServerCompCode,
			"labcCode": reaServerLabcCode
		};
		
		me.BUTTON_CAN_CLICK = false; //不可点击
		me.showMask("供货提取中...");
		JcallShell.Action.delay(function() {
			JShell.Server.post(url, JShell.JSON.encode(params), function(data) {
				me.hideMask();
				me.BUTTON_CAN_CLICK = true;
				if (data.success) {
					console.log(data);
					me.fireEvent('onExtractAfter', me, data, rec);
					if (callback) {
						callback(data);
					} else {
						me.onSearch();
					}
				} else {
					JShell.Msg.error(data.msg);
				}
			});
		}, null, 100);
	}
});
