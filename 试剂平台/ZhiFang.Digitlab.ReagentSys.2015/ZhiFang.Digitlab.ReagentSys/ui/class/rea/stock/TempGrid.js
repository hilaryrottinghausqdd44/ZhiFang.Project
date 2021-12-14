/**
 * 出入库临时列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.stock.TempGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.SimpleComboBox'
	],
	
	title: '出入库临时列表',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchCenQtyDtlTempByHQL?isPlanish=true',
	/**迁移到历史表服务路径*/
	saveToHistoryUrl:'/ReagentService.svc/ST_UDTO_MigrationCenQtyDtlTemp',
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
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
	/**使用标志默认值*/
	defaultUseFlagValue:0,
	
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
			dataIndex: 'CenQtyDtlTemp_UseFlag',
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
			dataIndex: 'CenQtyDtlTemp_LabName',
			text: '实验室',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTemp_CompanyName',
			text: '供应商',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTemp_ProdOrgName',
			text: '厂商(品牌)',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTemp_GoodsName',
			text: '产品名称',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTemp_ProdGoodsNo',
			text: '产品编号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTemp_GoodsUnit',
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTemp_UnitMemo',
			text: '规格',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTemp_LotNo',
			text: '批号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTemp_InvalidDate',
			text: '失效期',
			width: 80,
			isDate:true
		},{
			dataIndex: 'CenQtyDtlTemp_GoodsQty',
			text: '数量',
			width: 80,
			defaultRenderer: true
		},{
			dataIndex: 'CenQtyDtlTemp_InTime',
			text: '出入库时间',
			width: 80,
			isDate:true
		},{
			dataIndex: 'CenQtyDtlTemp_Id',
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
			}
		}, {
			fieldLabel: '实验室主键ID',
			itemId: 'Lab_Id',
			xtype:'textfield',
			hidden: true
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
			}
		}, {
			fieldLabel: '供应商主键ID',
			itemId: 'Comp_Id',
			xtype:'textfield',
			hidden: true
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
			}
		}, {
			fieldLabel: '厂商主键ID',
			itemId: 'Prod_Id',
			xtype:'textfield',
			hidden: true
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
		items.push('-','searchb','-',{
			xtype:'button',
			text:'手工迁移',
			iconCls:'button-save',
			tooltip:'迁移选中的数据',
			handler:function(){
				me.onSaveToHistory();
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
			params.push('cenqtydtltemp.Lab.Id=' + Lab_Id);
		}
		if(Comp_Id){
			params.push('cenqtydtltemp.Comp.Id=' + Comp_Id);
		}
		if(Prod_Id){
			params.push('cenqtydtltemp.Prod.Id=' + Prod_Id);
		}
		
		if(UseFlag != 0){
			params.push('cenqtydtltemp.UseFlag=' + UseFlag);
		}
		if(BeginDate){
			params.push("cenqtydtltemp.InTime>='" + 
				JShell.Date.toString(BeginDate,true) + "'");
		}
		if(EndDate){
			params.push("cenqtydtltemp.InTime<'" + 
				JShell.Date.toString(JShell.Date.getNextDate(EndDate),true) + "'");
		}
		
		if(params.length > 0){
			me.internalWhere = params.join(' and ');
		}else{
			me.internalWhere = '';
		}
		
		return me.callParent(arguments);
	},
	onSaveToHistory:function(){
		var me = this,
			recs = me.getSelectionModel().getSelection(),
			len = recs.length;

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		var url = JShell.System.Path.ROOT + me.saveToHistoryUrl;
		
		var ids = [];
		for(var i=0;i<len;i++){
			ids.push(recs[i].get(me.PKField));
		}
		url += "?QtyDtlIDList=" + ids;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});