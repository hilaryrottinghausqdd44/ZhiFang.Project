/**
 * @description 注册证预警
 * @author liangyl
 * @version 2018-08-17
 */
Ext.define('Shell.class.rea.client.registerwarning.basic.QtyGrid', {
	extend: 'Shell.class.rea.client.basic.GridPanel',
	requires: [
		'Shell.ux.form.field.BoolComboBox',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '效期预警',
	width: 800,
	height: 500,

	/**导出数据服务路径*/
	downLoadExcelUrl: '/ReaManageService.svc/RS_UDTO_DownLoadGetExportExcelReaBmsQtyDtlByGroupType',
	/**查询数据*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL?isPlanish=true',
	/**查询预警数据*/
	selectAlertUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaAlertInfoSettingsByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'ReaGoodsRegister_RegisterInvalidDate',
		direction: 'DESC'
	}],

	/**默认加载数据*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**效期预警类型:2:效期已过期报警;3:效期将过期报警*/
	qtyType: null,
	/**预警类型:注册证效期已过期预警*/
	AlertTypeId: '5',
	AlertTypeList: [],
	
	initComponent: function() {
		var me = this;
		me.defaultWhere = "reagoodsregister.Visible=1";
		//数据列
		me.columns = me.createGridColumns();
		me.decreaseUserUI();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'ReaGoodsRegister_GoodsNo',
			text: '货品编码',
			width: 100,
			renderer: function(value, meta, record) {
				var bgColor = record.get("ReaGoodsRegister_Color");
				if(bgColor) {
					meta.style = 'background-color:' + bgColor + ';color:#ffffff;';
				}
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsRegister_GoodsLotNo',
			text: '货品批号',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoodsRegister_RegisterNo',
			text: '注册证编号',
			width: 95,
			editor: {}
		}, {
			dataIndex: 'ReaGoodsRegister_RegisterDate',
			text: '注册日期',
			width: 95,
			type: 'date',
			isDate: true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			}
		}, {
			dataIndex: 'ReaGoodsRegister_RegisterInvalidDate',
			text: '<b style="color:blue;">注册证有效期</b>',
			width: 95,
			type: 'date',
			//isDate:true,
			editor: {
				xtype: 'datefield',
				format: 'Y-m-d'
			},
			renderer: function(value, meta, record) {
				if(value) value = Ext.util.Format.date(value, 'Y-m-d');
				var bgColor = record.get("ReaGoodsRegister_Color");
				if(bgColor) {
					meta.style = 'background-color:' + bgColor + ';color:#ffffff;';
				}
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return value;
			}
		}, {
			dataIndex: 'ReaGoodsRegister_CName',
			text: '名称',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaGoodsRegister_EName',
			text: '英文名称',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaGoodsRegister_ShortCode',
			text: '货品代码',
			width: 100,
			editor: {}
		}, {
			dataIndex: 'ReaGoodsRegister_FactoryName',
			text: '厂家',
			width: 120,
			editor: {}
		}, {
			dataIndex: 'ReaGoodsRegister_CompanyName',
			text: '供应商',
			width: 120,
			editor: {}
		}, {
			dataIndex: 'ReaGoodsRegister_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			text: '原件路径',
			dataIndex: 'ReaGoodsRegister_RegisterFilePath',
			width: 120,
			hidden: true,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '预览',
			align: 'center',
			width: 35,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if(record.get("ReaGoodsRegister_RegisterFilePath"))
						return 'button-search hand';
					else
						return '';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.IsSearchForm = false;
					var record = grid.getStore().getAt(rowIndex);
					me.openRegisterFile(record);
				}
			}]
		}, {
			xtype: 'actioncolumn',
			text: '下载',
			align: 'center',
			width: 35,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if(record.get("ReaGoodsRegister_RegisterFilePath"))
						return 'button-down hand';
					else
						return '';
				},
				handler: function(grid, rowIndex, colIndex) {
					me.IsSearchForm = false;
					var record = grid.getStore().getAt(rowIndex);
					me.onDownLoadRegisterFile(record);
				}
			}]
		}, {
			dataIndex: 'ReaGoodsRegister_Color',
			text: '预警颜色',
			hidden: true,
			width: 100,
			defaultRenderer: true
		}];

		return columns;
	},
	/**加载数据前*/
	onBeforeLoad: function() {
		var me = this;
		me.store.removeAll();
		me.store.proxy.url = me.getLoadUrl(); //查询条件
		if(!me.internalWhere) {
			var msg = me.msgFormat.replace(/{msg}/, "请选择有效的天数后再获取");
			me.getView().update(msg);
			return false;
		}
		me.disableControl(); //禁用 所有的操作功能
		if(!me.defaultLoad) return false;
	},
	/**获取内部条件*/
	getInternalWhere: function() {
		var me = this;
		return "";
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere = me.getInternalWhere();
		var url = me.callParent(arguments);
		return url;
	},
	/**显示操作记录信息*/
	openShowOpForm: function(id) {
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.98;
		JShell.Win.open('Shell.class.rea.client.qtyoperation.ShowGrid', {
			height: height,
			width: maxWidth,
			SUB_WIN_NO: '2',
			GoodsID: id
		}).show();
	},
	/**根据传入天数计算日期范围*/
	calcDateArea: function(day) {
		var me = this;
		if(!day) day = 0;
		var edate = JcallShell.System.Date.getDate();
		var sdate = Ext.Date.add(edate, Ext.Date.DAY, day);
		var dateArea = {
			start: sdate,
			end: edate
		};
		return dateArea;
	},
	getWhereHql:function(){
		var me = this,
			arr = [];
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		if (where) {
			where= JShell.String.encode(where);
		}
		return where;
	},
	/**导出EXCEL文件*/
	onDownLoadExcel: function() {
		var me = this,
			operateType = '0'
		var buttonsToolbar = me.getComponent('buttonsToolbar');

		var url = JShell.System.Path.ROOT + me.downLoadExcelUrl;

		var params = [];
		params.push("qtyType=" + me.qtyType);
		params.push("groupType=" + me.groupType);
		params.push("operateType=" + operateType);
		params.push("where=" + me.getWhereHql());
		params.push("sort=" + "");
		params.push("page=" + 0);
		params.push("limit=" + 0);
		url += "?" + params.join("&");
		window.open(url);
	},
	/*查看注册文件**/
	openRegisterFile: function(record) {
		var me = this;
		var id = "";
		if(record != null) {
			id = record.get('ReaGoodsRegister_Id');
		}
		var url = JShell.System.Path.getRootUrl("/ReaSysManageService.svc/ST_UDTO_ReaGoodsRegisterPreviewPdf");
		url += '?operateType=1&id=' + id;
		window.open(url);
	},
	/*下载注册文件**/
	onDownLoadRegisterFile: function(record) {
		var me = this;
		var id = "";
		if(record != null) {
			id = record.get('ReaGoodsRegister_Id');
		}
		var url = JShell.System.Path.getRootUrl("/ReaSysManageService.svc/ST_UDTO_ReaGoodsRegisterPreviewPdf");
		url += '?operateType=0&id=' + id;
		window.open(url);
	},
	/**获取预警颜色信息*/
	getAlertByAlertType: function(callback) {
		var me = this;
		var url = JShell.System.Path.ROOT + me.selectAlertUrl;
		url += "&fields=ReaAlertInfoSettings_AlertColor,ReaAlertInfoSettings_StoreLower,ReaAlertInfoSettings_StoreUpper&where=reaalertinfosettings.AlertTypeId=" + me.AlertTypeId;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				callback(data);
			} else {
				JShell.Msg.error(data.msg);
			}
		}, false);
	}
});