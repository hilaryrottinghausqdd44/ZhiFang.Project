/**
 * 下级机构试剂列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.goods.confirm.children.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '下级机构试剂列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchGoodsByHQL?isPlanish=true',
	/**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateGoodsByField',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**默认加载数据*/
	defaultLoad: false,
	/**排序字段*/
	//defaultOrderBy:[{property:'Goods_DispOrder',direction:'ASC'}],

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.on({
			itemdblclick: function(view, record) {
				me.showForm(record.get(me.PKField));
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('toMaxClick','toMinClick');
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();

		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [{
			itemId:'toMaxClick',
			iconCls:'button-right',
			text:'放大',
			tooltip:'<b>放大</b>',
			handler:function(){
				this.hide();
				me.fireEvent('toMaxClick',me);
				this.ownerCt.getComponent('toMinClick').show();
			}
		},{
			itemId:'toMinClick',
			iconCls:'button-left',
			text:'还原',
			tooltip:'<b>还原</b>',
			hidden:true,
			handler:function(){
				this.hide();
				me.fireEvent('toMinClick',me);
				this.ownerCt.getComponent('toMaxClick').show();
			}
		},'-','refresh', '-', {
			text:'实验室确认',
			tooltip:'实验室对供应商试剂进行确认',
			iconCls:'button-save',
			handler:function(){me.onCenOrgConfirm(true);}
		}, {
			text:'取消确认',
			tooltip:'实验室取消对供应商试剂的确认',
			iconCls:'button-save',
			handler:function(){me.onCenOrgConfirm(false);}
		}];
		
		//查询框信息
		me.searchInfo = {
			width: 180,
			isLike: true,
			itemId: 'Search',
			emptyText: '中文名/英文名/厂商产品编码',
			fields: ['goods.CName', 'goods.EName', 'goods.ProdGoodsNo']
		};
		items.push('-', '->', {
			type: 'search',
			info: me.searchInfo
		});

		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'Goods_BarCodeMgr',
			text: '条码类型',
			width: 60,
			renderer:function(value, meta) {
				var v = "";
				if(value == "0"){
					v = "批条码";
					meta.style = "color:green;";
				}else if (value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				}else if (value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}

				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			dataIndex: 'Goods_IsRegister',
			text: '有注册证',
			width: 60,
			align: 'center',
			type: 'bool',
			isBool: true
		}, {
			dataIndex: 'Goods_IsPrintBarCode',
			text: '打印条码',
			width: 60,
			align: 'center',
			type: 'bool',
			isBool: true
		}, {
			dataIndex: 'Goods_GoodsNo',
			text: '产品编码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_CName',
			text: '中文名',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_EName',
			text: '英文名',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_ProdGoodsNo',
			text: '厂商产品编码',
			width: 100,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '注册证',
			align: 'center',
			width: 50,
			style: 'font-weight:bold;color:white;background:orange;',
			sortable:false,
			hideable: false,
			items: [{
				//iconCls:'button-search hand',
				tooltip:'查看该产品注册证',
				getClass: function(v, meta, record) {
					if(record.get("Goods_IsRegister")){
						return 'button-search hand';
					}else{
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var record = grid.getStore().getAt(rowIndex);
					me.onShowRegisterGrid(record);
				}
			}]
		}, {
			dataIndex: 'Goods_CompConfirm',
			text: '供应商确认',
			width: 80,
			isBool: true,
			type:'bool',
			align:'center'
		}, {
			dataIndex: 'Goods_CenOrgConfirm',
			text: '实验室确认',
			width: 80,
			isBool: true,
			type:'bool',
			align:'center'
		}, {
			dataIndex: 'Goods_Comp_CName',
			text: '供应商',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_Prod_CName',
			text: '厂商',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_CompGoodsNo',
			text: '供应商产品编码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_ProdGoodsNo',
			text: '厂商产品编码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_UnitName',
			text: '单位',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_UnitMemo',
			text: '规格',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_GoodsClass',
			text: '一级分类',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_ShortCode',
			text: '代码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_Price',
			text: '价格',
			width: 100,
			align: 'right',
			xtype:'numbercolumn',
			format:'0.00'
		}, {
			dataIndex: 'Goods_GoodsDesc',
			text: '备注',
			width: 100,
			defaultRenderer: true
		}, {
			text: '最近时间',
			dataIndex: 'Goods_DataUpdateTime',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'Goods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'Goods_Comp_Id',
			text: '供应商ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'Goods_IsRegister',
			text: '是否有注册证',
			isBool: true,
			type:'bool',
			hidden: true,
			hideable: false
		}];

		return columns;
	},
	
	/**显示表单*/
	showForm: function(id) {
		JShell.Win.open('Shell.class.rea.goods.Form', {
			resizable: false,
			formtype:'show',
			PK:id
		}).show();
	},
	/**实验室试剂确认*/
	onCenOrgConfirm:function(bo){
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		var msg = bo ? "是否进行实验室试剂确认？" : "是否取消实验室试剂确认？"
		var confirm = bo ? 1 : 0;
		
		JShell.Msg.confirm({
			msg:msg
		},function(but) {
			if (but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;

			me.showMask(me.saveText); //显示遮罩层
			for (var i in records) {
				var id = records[i].get(me.PKField);
				me.confirmOneById(i, id, confirm);
			}
		});
	},
	/**确认一条数据*/
	confirmOneById: function(index, id, confirm) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.editUrl);
		
		var params = JShell.JSON.encode({
			entity:{
				Id:id,
				CenOrgConfirm:confirm
			},
			fields:'Id,CenOrgConfirm'
		});
		setTimeout(function() {
			JShell.Server.post(url,params,function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.delCount++;
				} else {
					me.delErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.delErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**查看注册证*/
	onShowRegisterGrid:function(record){
		var me = this;
		
		JShell.Win.open('Shell.class.rea.goods.register.search.SearchGrid',{
			CenOrgId:record.get('Goods_Comp_Id'),
			ProdGoodsNo:record.get('Goods_ProdGoodsNo')
		}).show();
	}
});