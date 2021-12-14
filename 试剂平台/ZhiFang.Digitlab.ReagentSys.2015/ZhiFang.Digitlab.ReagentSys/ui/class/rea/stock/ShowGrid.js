/**
 * 库存列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.stock.ShowGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '库存列表',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchCenQtyDtlByHQL?isPlanish=true',
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
//	multiSelect: true,
//	selType: 'checkboxmodel',
//	hasDel: true,
	
	/**显示实验室选项*/
	showLab:true,
	/**显示供应商选项*/
	showComp:true,
	/**显示厂商选项*/
	showProd:true,
	/**实验室选项默认值*/
	defaultLabValue:{},
	/**供应商选项默认值*/
	defaultCompValue:{},
	/**厂商选项默认值*/
	defaultProdValue:{},
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('addclick','editclick');
		//查询栏
		me.buttonToolbarItems = me.createButtonToolbarItems();
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'CenQtyDtl_LabName',
			text: '实验室',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtl_CompanyName',
			text: '供应商',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtl_ProdOrgName',
			text: '厂商(品牌)',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtl_GoodsName',
			text: '产品名称',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtl_ProdGoodsNo',
			text: '产品编号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtl_GoodsUnit',
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtl_UnitMemo',
			text: '规格',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtl_GoodsQty',
			text: '数量',
			width: 80,
			renderer: function(value, meta,record) {
				var v = parseInt(value || 0);
				var min = parseInt(record.get('CenQtyDtl_LowQty'));
				var max = parseInt(record.get('CenQtyDtl_HeightQty'));
				
				if(v <= min){
					meta.style = 'background-color:red;';
				}else if(v >= max){
					meta.style = 'background-color:orange;';
				}
				
				if (value) meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				
				return value;
			}
		},{
			dataIndex: 'CenQtyDtl_LotNo',
			text: '批号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtl_ProdDate',
			text: '生产日期',
			hidden: true,
			hideable: false,
			width: 80,
			isDate:true
		},{
			dataIndex: 'CenQtyDtl_InvalidDate',
			text: '失效期',
			width: 80,
			isDate:true
		},{
			dataIndex: 'CenQtyDtl_LowQty',
			text: '低库存报警数量',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtl_HeightQty',
			text: '高库存报警数量',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtl_SumTotal',
			text: '库存总计金额',
			width: 90,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtl_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	},
	/**创建查询栏*/
	createButtonToolbarItems:function(){
		var me = this,
			items = [];
		
		//实验室
		items.push({
			labelWidth:50,width:160,labelAlign:'right',
			fieldLabel: '实验室',
			itemId: 'Lab_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				defaultWhere:"cenorg.CenOrgType.ShortCode='3'",
				title:'实验室选择'
			},
			hidden:!me.showLab,
			value:me.defaultLabValue.Name
		}, {
			fieldLabel: '实验室主键ID',
			itemId: 'Lab_Id',
			xtype:'textfield',
			hidden: true,
			value:me.defaultLabValue.Id
		});
		//供应商
		items.push({
			labelWidth:50,width:160,labelAlign:'right',
			fieldLabel: '供应商',
			itemId: 'Comp_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				defaultWhere:"cenorg.CenOrgType.ShortCode='2'",
				title:'实验室选择'
			},
			hidden:!me.showComp,
			value:me.defaultCompValue.Name
		}, {
			fieldLabel: '供应商主键ID',
			itemId: 'Comp_Id',
			xtype:'textfield',
			hidden: true,
			value:me.defaultCompValue.Id
		});
		//品牌
		items.push({
			labelWidth:40,width:150,labelAlign:'right',
			fieldLabel: '品牌',
			itemId: 'Prod_CName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.cenorg.CheckGrid',
			classConfig:{
				defaultWhere:"cenorg.CenOrgType.ShortCode='1'",
				title:'实验室选择'
			},
			hidden:!me.showProd,
			value:me.defaultProdValue.Name
		}, {
			fieldLabel: '厂商主键ID',
			itemId: 'Prod_Id',
			xtype:'textfield',
			hidden: true,
			value:me.defaultProdValue.Id
		});
		//试剂
		items.push({
			labelWidth:40,width:150,labelAlign:'right',
			fieldLabel: '试剂',
			itemId: 'Goods_CName',
			xtype: 'uxCheckTrigger',
			className:'Shell.class.rea.goods.CheckGrid',
			classConfig:{
				checkOne:true,
				title:'试剂选择'
			}
		}, {
			fieldLabel: '试剂主键ID',
			itemId: 'Goods_Id',
			xtype:'textfield',
			hidden: true
		});
		//查询
		items.push('-',{
			width: 150,
			labelWidth: 60,
			fieldLabel: '库存状态',
			xtype: 'uxSimpleComboBox',
			itemId: 'FlagType',
			value: 0,
			data: me.getFlagTypeList()
		},'-','searchb',{
			xtype:'button',
			text:'查看图表',
			tooltip:'查看图表',
			iconCls:'button-search',
			handler:function(){
				me.showChart();
			}
		});
			
		return items;
	},
	/**查询*/
	onSearchBClick:function(){
		this.onSearch();
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;

		var checkList = ['Lab_CName', 'Comp_CName'];

		for (var i in checkList) {
			me.initCheckTriggerListeners(checkList[i]);
		}
		
		var Prod = me.getComponent('buttonsToolbar').getComponent('Prod_CName');
		var ProdId = me.getComponent('buttonsToolbar').getComponent('Prod_Id');
		var GoodsCName = me.getComponent('buttonsToolbar').getComponent('Goods_CName');
		var GoodsId = me.getComponent('buttonsToolbar').getComponent('Goods_Id');
		Prod.on({
			check: function(p, record) {
				ProdId.setValue(record ? record.get('CenOrg_Id') : '');
				Prod.setValue(record ? record.get('CenOrg_CName') : '');
				
				var prodIdValue = ProdId.getValue();
				if(!prodIdValue){
					GoodsCName.setValue('');
					GoodsId.setValue('');
				}
				
				p.close();
			}
		});
		GoodsCName.on({
			check: function(p, record) {
				GoodsId.setValue(record ? record.get('Goods_Id') : '');
				GoodsCName.setValue(record ? record.get('Goods_CName') : '');
				p.close();
			},
			beforetriggerclick:function(p){
				var prodIdValue = ProdId.getValue();
				if(prodIdValue){
					p.classConfig = GoodsCName.classConfig ||{};
					p.classConfig.defaultWhere = 'goods.Prod.Id=' +prodIdValue;
				}else{
					p.classConfig = GoodsCName.classConfig ||{};
					p.classConfig.defaultWhere = '';
					JShell.Msg.error('请先选择一个品牌!');
					return false;
				}
				return true;
			}
		});
	},
	/**下拉框监听*/
	initCheckTriggerListeners: function(name) {
		var me = this,
			com = me.getComponent('buttonsToolbar').getComponent(name);

		if (!com) return;

		var idName = name.split('_')[0] + '_Id';
		com.on({
			check: function(p, record) {
				var Id = me.getComponent('buttonsToolbar').getComponent(idName);
				Id.setValue(record ? record.get('CenOrg_Id') : '');
				com.setValue(record ? record.get('CenOrg_CName') : '');
				p.close();
			}
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Lab_Id = buttonsToolbar.getComponent('Lab_Id').getValue(),
			Comp_Id = buttonsToolbar.getComponent('Comp_Id').getValue(),
			Prod_Id = buttonsToolbar.getComponent('Prod_Id').getValue(),
			Goods_Id = buttonsToolbar.getComponent('Goods_Id').getValue(),
			FlagType = buttonsToolbar.getComponent('FlagType').getValue(),
			params = [];

		if(Lab_Id){
			params.push('cenqtydtl.Lab.Id=' + Lab_Id);
		}
		if(Comp_Id){
			params.push('cenqtydtl.Comp.Id=' + Comp_Id);
		}
		if(Prod_Id){
			params.push('cenqtydtl.Prod.Id=' + Prod_Id);
		}
		if(Goods_Id){
			params.push('cenqtydtl.Goods.Id=' + Goods_Id);
		}
		if(FlagType){
			params.push(FlagType);
		}
		
		if(params.length > 0){
			me.internalWhere = params.join(' and ');
		}else{
			me.internalWhere = '';
		}
		
		return me.callParent(arguments);
	},
	/**获取库存状态列表*/
	getFlagTypeList:function(){
		var list = [
			[0,'全部'],
			['(cenqtydtl.GoodsQty>=cenqtydtl.HeightQty or cenqtydtl.GoodsQty<=cenqtydtl.LowQty)','异常'],
			['cenqtydtl.GoodsQty>=cenqtydtl.HeightQty*1.1','>=1.1H'],
			['cenqtydtl.GoodsQty>=cenqtydtl.HeightQty','>=1.0H'],
			['cenqtydtl.GoodsQty>=cenqtydtl.HeightQty*0.9','>=0.9H'],
			['cenqtydtl.GoodsQty<=cenqtydtl.LowQty*0.9','<=0.9L'],
			['cenqtydtl.GoodsQty<=cenqtydtl.LowQty','<=1.0L'],
			['cenqtydtl.GoodsQty<=cenqtydtl.LowQty*1.1','<=1.1L']
		];
		return list;
	},
	showChart:function(){
		var me = this;
		
		me.getLoadUrl();
		
		var where = [];
		if(me.defaultWhere) where.push(me.defaultWhere);
		if(me.internalWhere) where.push(me.internalWhere);
		if(me.externalWhere) where.push(me.externalWhere);
		
		var defaultWhere = where.join(' and ');
		
		var panel = JShell.Win.open('Shell.class.rea.statistics.ReagentStockPanel',{
			createDockedItems:function(){return null;},
			defaultWhere:defaultWhere
		});
		
		JShell.Action.delay(function(){panel.show();},100);
	}
});