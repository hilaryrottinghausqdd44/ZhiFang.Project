/**
 * 出入库历史列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.stock.HistoryGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '出入库历史列表',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchCenQtyDtlTempHistoryByHQL?isPlanish=true',
	/**默认加载*/
	defaultLoad: true,
	/**后台排序*/
	remoteSort: true,
	/**带功能按钮栏*/
	hasButtontoolbar:false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	
	/**使用标志默认值*/
	defaultUseFlagValue:0,
	
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
		//数据列
		me.columns = me.createGridColumns();
		//功能栏
		me.dockedItems = [Ext.create('Shell.ux.toolbar.Button',{
			dock:'top',
			itemId:'buttonsToolbar1',
			items:me.createButtonToolbarItems1()
		}),Ext.create('Shell.ux.toolbar.Button',{
			dock:'top',
			itemId:'buttonsToolbar2',
			items:me.createButtonToolbarItems2()
		})];
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'CenQtyDtlTempHistory_UseFlag',
			text: '使用标志',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.UseFlag['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'CenQtyDtlTempHistory_LabName',
			text: '实验室',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTempHistory_CompanyName',
			text: '供应商',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTempHistory_ProdOrgName',
			text: '厂商(品牌)',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTempHistory_TestEquipName',
			text: '仪器名称',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTempHistory_GoodsName',
			text: '产品名称',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTempHistory_ProdGoodsNo',
			text: '产品编号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTempHistory_GoodsUnit',
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTempHistory_UnitMemo',
			text: '规格',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTempHistory_LotNo',
			text: '批号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTempHistory_InvalidDate',
			text: '失效期',
			width: 80,
			isDate:true
		},{
			dataIndex: 'CenQtyDtlTempHistory_GoodsQty',
			text: '数量',
			width: 80,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTempHistory_InTime',
			text: '出入库时间',
			width: 80,
			isDate:true
		},{
			dataIndex: 'CenQtyDtlTempHistory_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	},
	
	/**创建查询栏*/
	createButtonToolbarItems1:function(){
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
				defaultWhere:"cenorg.CenOrgType.ShortCode='1' or cenorg.CenOrgType.ShortCode='2'",
				title:'供应商选择'
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
				title:'厂商选择'
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
		
		//仪器
		items.push({
			labelWidth:40,width:150,labelAlign:'right',
			fieldLabel: '仪器',
			itemId: 'Equip_CName',
			xtype: 'uxCheckTrigger'
		}, {
			fieldLabel: '仪器主键ID',
			itemId: 'Equip_Id',
			xtype:'textfield',
			hidden: true
		});
		//试剂
		items.push({
			labelWidth:40,width:150,labelAlign:'right',
			fieldLabel: '试剂',
			itemId: 'Goods_CName',
			xtype: 'uxCheckTrigger'
		}, {
			fieldLabel: '试剂主键ID',
			itemId: 'Goods_Id',
			xtype:'textfield',
			hidden: true
		});
		
		//试剂消耗图表按钮
		items.push({
			xtype:'button',
			text:'试剂消耗图表',
			tooltip:'试剂消耗图表',
			iconCls:'button-search',
			handler:function(){
				me.showChart();
			}
		});
		
		return items;
	},
	/**创建查询栏*/
	createButtonToolbarItems2:function(){
		var me = this,
			items = [];
		
		//使用标志
		items.push({
			width: 150,
			labelWidth: 60,
			fieldLabel: '使用类型',
			xtype: 'uxSimpleComboBox',
			itemId: 'UseFlag',
			hasStyle: true,
			value: me.defaultUseFlagValue,
			data: JShell.REA.Enum.getList('UseFlag',true,true)
		});
		//日期区间
		items.push({
			width: 165,
			labelWidth: 70,
			fieldLabel: '出入库时间',
			labelAlign:'right',
			itemId: 'BeginDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		},{
			width: 100,
			labelWidth: 5,
			fieldLabel: '-',
			labelSeparator: '',
			itemId: 'EndDate',
			xtype: 'datefield',
			format: 'Y-m-d'
		});
		//刷新
		items.push('-','searchb');
		
		//3个测试按钮
		items.push({
			xtype:'button',
			text:'仪器：cobas8000A',
			tooltip:'仪器：cobas8000A',
			iconCls:'button-search',
			handler:function(){
				var where = "cenqtydtltemphistory.TestEquipName='cobas8000A'";
				me.onTestBtnClick(where);
			}
		},{
			xtype:'button',
			text:'仪器：cobas8000B',
			tooltip:'仪器：cobas8000B',
			iconCls:'button-search',
			handler:function(){
				var where = "cenqtydtltemphistory.TestEquipName='cobas8000B'";
				me.onTestBtnClick(where);
			}
		},{
			xtype:'button',
			text:'仪器：Cobas E601',
			tooltip:'仪器：Cobas E601',
			iconCls:'button-search',
			handler:function(){
				var where = "cenqtydtltemphistory.TestEquipName='Cobas E601'";
				me.onTestBtnClick(where);
			}
		});
			
		return items;
	},
	/**查询*/
	onSearchBClick:function(){
		this.externalWhere = '';
		this.onSearch();
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;

		var checkList = ['Lab_CName', 'Comp_CName', 'Prod_CName'];

		for (var i in checkList) {
			me.initCheckTriggerListeners(checkList[i]);
		}
	},
	/**下拉框监听*/
	initCheckTriggerListeners: function(name) {
		var me = this,
			com = me.getComponent('buttonsToolbar1').getComponent(name);

		if (!com) return;

		var idName = name.split('_')[0] + '_Id';
		com.on({
			check: function(p, record) {
				var Id = me.getComponent('buttonsToolbar1').getComponent(idName);
				Id.setValue(record ? record.get('CenOrg_Id') : '');
				com.setValue(record ? record.get('CenOrg_CName') : '');
				p.close();
			}
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar1 = me.getComponent('buttonsToolbar1'),
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			Lab_Id = buttonsToolbar1.getComponent('Lab_Id').getValue(),
			Comp_Id = buttonsToolbar1.getComponent('Comp_Id').getValue(),
			Prod_Id = buttonsToolbar1.getComponent('Prod_Id').getValue(),
			UseFlag = buttonsToolbar2.getComponent('UseFlag').getValue(),
			BeginDate = buttonsToolbar2.getComponent('BeginDate').getValue(),
			EndDate = buttonsToolbar2.getComponent('EndDate').getValue(),
			params = [];
		
		if(Lab_Id){
			params.push('cenqtydtltemphistory.Lab.Id=' + Lab_Id);
		}
		if(Comp_Id){
			params.push('cenqtydtltemphistory.Comp.Id=' + Comp_Id);
		}
		if(Prod_Id){
			params.push('cenqtydtltemphistory.Prod.Id=' + Prod_Id);
		}
		
		if(UseFlag != 0){
			params.push('cenqtydtltemphistory.UseFlag=' + UseFlag);
		}
		if(BeginDate){
			params.push("cenqtydtltemphistory.InTime>='" + 
				JShell.Date.toString(BeginDate,true) + "'");
		}
		if(EndDate){
			params.push("cenqtydtltemphistory.InTime<'" + 
				JShell.Date.toString(JShell.Date.getNextDate(EndDate),true) + "'");
		}
		
		if(params.length > 0){
			me.internalWhere = params.join(' and ');
		}else{
			me.internalWhere = '';
		}
		
		return me.callParent(arguments);
	},
	/**测试按钮*/
	onTestBtnClick:function(where){
		this.externalWhere = where;
		this.onSearch();
	},
	showChart:function(){
		var me = this,
			buttonsToolbar1 = me.getComponent('buttonsToolbar1'),
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			Lab_Id = buttonsToolbar1.getComponent('Lab_Id').getValue(),
			Comp_Id = buttonsToolbar1.getComponent('Comp_Id').getValue(),
			Prod_Id = buttonsToolbar1.getComponent('Prod_Id').getValue(),
			BeginDate = buttonsToolbar2.getComponent('BeginDate').getValue(),
			EndDate = buttonsToolbar2.getComponent('EndDate').getValue();
		
		BeginDate = BeginDate ? JShell.Date.toString(BeginDate,true) : '';
		EndDate = EndDate ? JShell.Date.toString(JShell.Date.getNextDate(EndDate),true) : '';
		
		var panel = JShell.Win.open('Shell.class.rea.statistics.ConsumePanel',{
			createDockedItems:function(){return null;},
			LabId:Lab_Id,
			CompId:Comp_Id,
			ProdId:Prod_Id,
			StartDate:BeginDate,
			EndDate:EndDate
		});
		JShell.Action.delay(function(){panel.show();},100);
	}
});