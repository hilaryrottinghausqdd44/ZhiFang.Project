/**
 * 供货总单列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.DocGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
	title: '供货总单列表',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDoc',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
	
	/**是否是查看列表*/
	isShow:false,
	
	/**默认打印模板类型*/
	defaultModelType:'1',
	
	/**排序字段*/
	defaultOrderBy:[{property:'BmsCenSaleDoc_OperDate',direction:'DESC'}],
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent: function() {
		var me = this;
		me.addEvents('addclick','editclick');
		//加载条码模板组件
		//me.SaleModel = me.SaleModel || Ext.create('Shell.class.rea.sale.SaleModel');
		//查询框信息
		me.searchInfo = {
			width: 220,
			emptyText: '供货单号/订货方',
			isLike: true,
			fields: ['bmscensaledoc.SaleDocNo', 'bmscensaledoc.Lab.CName']
		};
		//自定义按钮功能栏
		if(me.isShow){
			//自定义按钮功能栏
			me.buttonToolbarItems = ['refresh','-',
//			{
//				fieldLabel:'模板类型',xtype:'uxSimpleComboBox',
//				itemId:'ModelType',allowBlank:false,value:me.defaultModelType,
//				width:200,labelWidth:55,labelAlign:'right',
//				data:me.SaleModel.getModelList()
//			},{
//				xtype:'splitbutton',
//	            textAlign: 'left',
//				iconCls:'button-print',
//				text:'条码打印',
//				handler:function(btn,e){btn.overMenuTrigger = true;btn.onClick(e);},
//				menu:[{
//					text:'直接打印',iconCls:'button-print',
//					listeners:{click:function(but) {me.onSalePrint(1);}}
//				},{
//					text:'浏览打印',iconCls:'button-print',
//					listeners:{click:function(but) {me.onSalePrint(2);}}
//				},{
//					text:'维护打印',iconCls:'button-print',
//					listeners:{click:function(but) {me.onSalePrint(3);}}
//				},{
//					text:'设计打印',iconCls:'button-print',
//					listeners:{click:function(but) {me.onSalePrint(4);}}
//				}]
//			},
			'->', {
				type: 'search',
				info: me.searchInfo
			}];
		}else{
			//自定义按钮功能栏
			me.buttonToolbarItems = ['refresh','add', 'edit', 'del', '-', '->', {
				type: 'search',
				info: me.searchInfo
			}];
		}
		
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BmsCenSaleDoc_OperDate',
			text: '操作时间',
			align:'center',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenSaleDoc_SaleDocNo',
			text: '供货单号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_Lab_CName',
			text: '订货方',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_Comp_CName',
			text: '供货方',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_UserName',
			text: '操作人员',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_UrgentFlag',
			text: '紧急标志',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_UrgentFlag['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'BmsCenSaleDoc_Status',
			text: '单据状态',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_Status['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'BmsCenSaleDoc_IOFlag',
			text: '提取标志',
			align:'center',
			width: 60,
			renderer: function(value, meta) {
				var v = JShell.REA.Enum.BmsCenSaleDoc_IOFlag['E' + value] || '';
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				meta.style = 'background-color:' + JcallShell.REA.Enum.Color['E' + value] || '#FFFFFF';
				return v;
			}
		},{
			dataIndex: 'BmsCenSaleDoc_PrintTimes',
			text: '打印次数',
			align:'center',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_Memo',
			text: '备注',
			width: 200,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDoc_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'BmsCenSaleDoc_DataTimeStamp',
			text: '时间戳',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDoc_Comp_Id',
			text: '供货方ID',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDoc_Lab_Id',
			text: '订货方ID',
			hidden: true,
			hideable: false
		}];
		
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		this.fireEvent('addclick',this);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection();
			
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		this.fireEvent('editclick',me,records[0]);
	},
	/**供货单打印*/
	onSalePrint:function(type){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;
			
		if(len == 0){
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		if(type != 1 && len > 1){
			JShell.Msg.error('非直接打印，只能选择一个供货单进行操作！');
			return;
		}
		
		//加载Lodop组件
		me.Lodop = me.Lodop || Ext.create('Shell.lodop.Lodop');
		
		var LODOP = me.Lodop.getLodop();
		if(!LODOP) return;
		
		//模板类型
		var ModelType = me.getComponent('buttonsToolbar').getComponent('ModelType').getValue();
		//Lodop打印内容字符串数组
		var LodopStr = [];
		
		var result = null;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			//LODOP = me.SaleModel.getModelContent(ModelType,LODOP);
			var strLodop = me.SaleModel.getModelContent(ModelType);
			eval(strLodop);
			if(type == 1){//直接打印
				result = LODOP.PRINT();
			}else if(type == 2){//预览打印
				result = LODOP.PREVIEW();
			}else if(type == 3){//维护打印
				result = LODOP.PRINT_SETUP();
			}else if(type == 4){//设计打印
				result = LODOP.PRINT_DESIGN();
			}
		}
	}
});