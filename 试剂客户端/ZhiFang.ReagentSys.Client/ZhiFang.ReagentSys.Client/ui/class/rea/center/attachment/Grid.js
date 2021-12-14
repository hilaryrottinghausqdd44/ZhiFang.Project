/**
 * 模块授权
 * @author liangyl
 * @version 2018-05-21
 */
Ext.define('Shell.class.rea.center.attachment.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '模块授权',
	width: 800,
	height: 500,
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/SC_UDTO_SearchSCAttachmentByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelSCAttachment',
	/**获取角色模块服务*/
	selectExportUrl: '/ReaManageService.svc/ST_UDTO_SearchExportAuthorizationFileOfPlatform',
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**默认加载数据*/
	defaultLoad: true,
	/**排序字段*/
	defaultOrderBy: [{
		property: 'SCAttachment_DataAddTime',
		direction: 'DESC'
	}],
	hasDel: true,
	/**加载数据提示*/
	loadingText: JShell.Server.LOADING_TEXT,
	defaultWhere: '',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		var LabID = JShell.System.Cookie.get(JShell.System.Cookie.map.LABID) || -1;
		me.defaultWhere = 'scattachment.LabID=' + LabID + ' and scattachment.BobjectID=' + LabID + " and scattachment.BusinessModuleCode='SServiceClient'";
		//查询框信息
		me.searchInfo = {
			width: 180,
			emptyText: '名称/说明',
			isLike: true,
			itemId: 'Search',
			fields: ['scattachment.CName', 'scattachment.Memo']
		};
		me.buttonToolbarItems = ['refresh', '-',
			{
				text: '导入',
				tooltip: '导入',
				iconCls: 'button-import',
				handler: function() {
					me.onImportExcelClick();
				}
			}, '->', {
				type: 'search',
				info: me.searchInfo
			}
		];
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'SCAttachment_DataAddTime',
			text: '上传时间',
			isDate: true,
			hasTime: true,
			width: 135,
			defaultRenderer: true
		}, {
			dataIndex: 'SCAttachment_FileName',
			text: '文件名称',
			width: 180,
			defaultRenderer: true
		}, {
			dataIndex: 'SCAttachment_Memo',
			text: '说明',
			width: 100,
			renderer: function(value, meta, record) {
				var v = me.showMemoText(value, meta);
				return v;
			}
		}, {
			xtype: 'actioncolumn',
			text: '操作记录',
			align: 'center',
			width: 100,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					return 'button-exp hand';
				},
				handler: function(grid, rowIndex, colIndex) {
					var record = grid.getStore().getAt(rowIndex);
					me.showForm(record);
				}
			}]
		}, {
			dataIndex: 'SCAttachment_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true,
			defaultRenderer: true
		}, {
			dataIndex: 'SCAttachment_LabID',
			text: 'LabID',
			hidden: true,
			hideable: false,
			defaultRenderer: true
		}, {
			dataIndex: 'SCAttachment_BobjectID',
			text: 'BobjectID',
			hidden: true,
			hideable: false,
			defaultRenderer: true
		}];

		return columns;
	},

	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];

		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		}

		return me.callParent(arguments);
	},
	/**获取查询框内容*/
	getSearchWhere: function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
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
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		var me = this;
		return data;
	},
	showMemoText: function(value, meta) {
		var me = this;
		var val = value.replace(/(^\s*)|(\s*$)/g, "");
		val = val.replace(/\\r\\n/g, "<br />");
		val = val.replace(/\\n/g, "<br />");
		var v = "" + value;
		var index1 = v.indexOf("</br>");
		if(index1 > 0) v = v.substring(0, index1);
		if(v.length > 0) v = (v.length > 20 ? v.substring(0, 20) : v);
		if(value.length > 20) {
			v = v + "...";
		}
		var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
		meta.tdAttr = 'data-qtip="' + qtipValue + '"';
		return v
	},
	/**导入授权模块信息*/
	onImportExcelClick: function() {
		var me = this;
		JShell.Win.open('Shell.class.rea.center.attachment.UploadPanel', {
			formtype: 'add',
			resizable: false,
			listeners: {
				save: function(p, records) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**查看操作记录*/
	showForm: function(record) {
		var me = this;
		var maxWidth = document.body.clientWidth * 0.69;
		var height = document.body.clientHeight * 0.62;
		var Id = record.get('SCAttachment_Id');
		var config = {
			resizable: false,
			title: '操作记录',
			hidden: true,
			height: height,
			PK: Id,
			width: 640
		};
		JShell.Win.open('Shell.class.rea.center.register.SCOperation', config).show();
	}
});