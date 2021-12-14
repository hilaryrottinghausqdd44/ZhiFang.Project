/***
 * 根据机构查看产品注册证
 * @author Jcall
 * @version 2017-05-24
 */
Ext.define('Shell.class.rea.goods.register.search.SearchGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '注册证查看',
	
	width:1200,
	height:600,
	
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchGoodsRegisterByHQL?isPlanish=true',
	
	/**是否启用序号列*/
	hasRownumberer: true,
	/**序号列宽度*/
	rowNumbererWidth: 35,
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**默认选中数据*/
	autoSelect: false,
	/**默认加载数据*/
	defaultLoad: false,
	/**排序字段*/
	defaultOrderBy: [{property: 'GoodsRegister_GoodsNo',direction: 'ASC'}],
	
	/**机构ID*/
	CenOrgId:null,
	/**厂家产品编号*/
	ProdGoodsNo:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		if(me.CenOrgId){
			me.defaultWhere = "goodsregister.CenOrgID=" + me.CenOrgId;
			if(me.ProdGoodsNo){
				me.defaultWhere += " and goodsregister.GoodsNo='" + me.ProdGoodsNo + "'";
			}
			me.onSearch();
		}
	},
	
	initComponent: function() {
		var me = this;
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [ {
			text: '中文名称',
			dataIndex: 'GoodsRegister_CName',
			flex: 1,
			minWidth: 110,			
			sortable: false,
			hidden: false,
			hideable: true,
			defaultRenderer: true
		},{
			xtype: 'actioncolumn',
			text: '原件',
			align: 'center',
			width: 40,
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
					var record = grid.getStore().getAt(rowIndex);
					me.openRegisterFile(record);
				}
			}]
		}, {
			text: '英文名称',
			dataIndex: 'GoodsRegister_EName',
			width: 100,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '产品编号',
			dataIndex: 'GoodsRegister_GoodsNo',
			width: 120,
			sortable: false,
			hidden: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '产品批号',
			dataIndex: 'GoodsRegister_GoodsLotNo',
			width: 120,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '注册证编号',
			dataIndex: 'GoodsRegister_RegisterNo',
			width: 120,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '注册日期',
			dataIndex: 'GoodsRegister_RegisterDate',
			isDate: true,
			width: 90,
			sortable: false,
			hideable: true,
			defaultRenderer: true
		}, {
			text: '有效期至',
			dataIndex: 'GoodsRegister_RegisterInvalidDate',
			width: 90,
			sortable: false,
			hideable: true,
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				if(value) {
					var date = JShell.System.Date.getDate();
					date = Ext.util.Format.date(date, 'Y-m-d');
					value = Ext.util.Format.date(value, 'Y-m-d');
					if(date > value) {
						meta.style = 'background-color:red;color:#ffffff;';
					}
				}
				return value;
			}
		}, {
			text: 'ID',
			dataIndex: 'GoodsRegister_Id',
			isKey: true,
			sortable: false,
			hidden: true,
			hideable: true
		}, {
			text: '原件路径',
			dataIndex: 'GoodsRegister_RegisterFilePath',
			hidden: true,
			sortable: false,
			hideable: true
		}];
		
		return columns;
	},
	/*查看注册文件**/
	openRegisterFile: function(record) {
		var me = this;
		var id = record ? record.get('GoodsRegister_Id') : "";
		if(!id) return;
		
		JShell.Win.open('Shell.class.rea.goods.register.basic.PreviewPDF', {
			PK:id,
			width:'90%',
			height:'98%'
		}).show();
	}
});