/***
 *  注册证管理
 * @author longfc
 * @version 2017-05-19
 */
Ext.define('Shell.class.rea.goods.register.basic.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	title: '注册证管理',

	defaultLoad: true,
	autoSelect: true,
	sortableColumns: false,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**是否隐藏产品批号列*/
	hiddenGoodsLotNo: false,
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchGoodsRegisterByHQL?isPlanish=true',
	defaultOrderBy: [{
		property: 'GoodsRegister_GoodsNo',
		direction: 'ASC'
	}],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		//查询框信息
		me.searchInfo = {
			width: 300,
			emptyText: '中文名称/英文名称/产品编号/注册证编号',
			isLike: false,
			itemId: "search",
			fields: ['goodsregister.CName', 'goodsregister.EName', 'goodsregister.GoodsNo', 'goodsregister.RegisterNo']
		};
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text: '中文名称',
			dataIndex: 'GoodsRegister_CName',
			flex: 1,
			minWidth: 200,
			sortable: false,
			hidden: false,
			hideable: true,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '原件',
			align: 'center',
			width: 35,
			style: 'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					if(record.get("GoodsRegister_RegisterFilePath"))
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
			text: '英文名称',
			dataIndex: 'GoodsRegister_EName',
			width: 110,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '产品编号',
			dataIndex: 'GoodsRegister_GoodsNo',
			width: 100,
			sortable: false,
			hidden: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '产品批号',
			dataIndex: 'GoodsRegister_GoodsLotNo',
			width: 90,
			hidden: me.hiddenGoodsLotNo,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '注册证编号',
			dataIndex: 'GoodsRegister_RegisterNo',
			width: 100,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: 'ID',
			dataIndex: 'GoodsRegister_Id',
			isKey: true,
			sortable: false,
			hidden: true,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '注册日期',
			dataIndex: 'GoodsRegister_RegisterDate',
			isDate: true,
			width: 80,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '有效期至',
			dataIndex: 'GoodsRegister_RegisterInvalidDate',
			//isDate: true,
			width: 80,
			sortable: false,
			hideable: true,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				if(value) {
					var Sysdate = JShell.System.Date.getDate();
					value = Ext.util.Format.date(value, 'Y-m-d');
					//					Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
					//					
					//					if(Sysdate > value) {
					//						meta.style = 'background-color:red;color:#ffffff;';
					//					}
					var BGColor = "";
					Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
					Sysdate = JShell.Date.getDate(Sysdate);
					var RegisterInvalidDate = value;
					RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
					var days = parseInt((RegisterInvalidDate - Sysdate) / 1000 / 60 / 60 / 24);
					if(days < 0) {
						BGColor = "red";
					} else if(days >= 0 && days <= 30) {
						BGColor = "#e97f36";
					} else if(days > 30) {
						BGColor = "#568f36";
					}
					if(BGColor)
						meta.style = 'background-color:' + BGColor + ';color:#ffffff;';
				}
				return value;
			}
		}, {
			text: '原件路径',
			dataIndex: 'GoodsRegister_RegisterFilePath',
			width: 120,
			hidden: true,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}];
		if(me.hasSave == true) {

		} else {

		}
		return columns;
	},
	/*查看注册文件**/
	openRegisterFile: function(record) {
		var me = this;
		var id = "";
		if(record != null) {
			id = record.get('GoodsRegister_Id');
		}
		//		var maxWidth = document.body.clientWidth * 0.98;
		//		var height = document.body.clientHeight * 0.96;
		//		var config = {
		//			PK: id,
		//			height: height,
		//			width: maxWidth,
		//			resizable: false, //可变大小
		//			closable: true, //有关闭按钮
		//			draggable: true,
		//			listeners: {
		//				close: function(win) {
		//					me.IsSearchForm = true;
		//				}
		//			}
		//		};
		//		JShell.Win.open('Shell.class.rea.goods.register.basic.PreviewPDF', config).show();
		//		
		var url = JShell.System.Path.getRootUrl("/ReagentSysService.svc/ST_UDTO_GoodsRegisterPreviewPdf");
		url += '?operateType=1&id=' + id;
		window.open(url);
	}
});