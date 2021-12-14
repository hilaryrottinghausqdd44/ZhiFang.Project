/**
 * 统计-试剂库存
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.statistics.ReagentStockPanel', {
	extend: 'Shell.class.rea.statistics.ReagentStockLineChart',
	requires: ['Shell.ux.form.field.CheckTrigger'],
	
	title: '试剂库存统计',
	width:1200,
	height:800,
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchCenQtyDtlByHQL?isPlanish=true',
	/**默认条件*/
	defaultWhere:'',
	
	/**机构类型*/
    ORGTYPE:null,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();
		me.onSearchBClick();
	},
	initComponent: function() {
		var me = this;
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	createDockedItems:function(){
		var me = this;
		var items = [];
		
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
			}
		}, {
			fieldLabel: '实验室主键ID',
			itemId: 'Lab_Id',
			xtype:'textfield',
			hidden: true
		});
		
		items.push('-','searchb');
		
		var dockedItems = Ext.create('Shell.ux.toolbar.Button',{
			dock:'top',
			itemId:'buttonsToolbar',
			items:items
		});
		
		return dockedItems;
	},
	/**刷新按钮点击处理方法*/
	onRefreshClick:function(){
		this.onSearch();
	},
	/**查询*/
	onSearchBClick:function(){
		this.externalWhere = '';
		this.onSearch();
	},
	load:function(){
		var me = this,
			url = JShell.System.Path.ROOT + me.selectUrl;
			
		var where = [];
		if(me.defaultWhere) where.push(me.defaultWhere);
		if(me.internalWhere) where.push(me.internalWhere);
		if(me.externalWhere) where.push(me.externalWhere);
		
		if(where.length > 0){
			url += '&where=' + where.join(' and ');
		}
		
		//试剂名称,低库存报警数量,高库存报警数量,库存数量,总计金额
		var fields = [
			'CenQtyDtl_GoodsName','CenQtyDtl_LowQty','CenQtyDtl_HeightQty',
			'CenQtyDtl_GoodsQty','CenQtyDtl_SumTotal'
		];
		url += '&fields=' + fields.join(',');
			
		JShell.Server.get(url,function(data){
			var obj = {list:data.value.list};
			me.changeData(obj);
		});
	},
	onSearch:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
			
		if(buttonsToolbar){
			Lab_Id = buttonsToolbar.getComponent('Lab_Id').getValue(),
			params = [];
			if(Lab_Id){
				params.push('cenqtydtl.Lab.Id=' + Lab_Id);
			}
			
			if(me.ORGTYPE){
				var type = me.ORGTYPE.toLocaleUpperCase();
				if(type == 'COMP'){
					params.push('cenqtydtl.Comp.Id=' + JShell.REA.System.CENORG_ID);
				}else if(TYPE == 'PROD'){
					params.push('cenqtydtl.Prod.Id=' + JShell.REA.System.CENORG_ID);
				}
			}
			me.internalWhere = params.join(' and ');
		}
		
		me.load();
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;

		var checkList = ['Lab_CName'];

		for (var i in checkList) {
			me.initCheckTriggerListeners(checkList[i]);
		}
	},
	/**下拉框监听*/
	initCheckTriggerListeners: function(name) {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar');
		
		if (!buttonsToolbar) return;
			
		var com = buttonsToolbar.getComponent(name);

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
	}
});