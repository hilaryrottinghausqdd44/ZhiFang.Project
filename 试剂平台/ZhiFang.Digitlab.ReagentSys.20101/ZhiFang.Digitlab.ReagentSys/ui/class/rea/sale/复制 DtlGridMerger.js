/**
 * 供货明细列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.DtlGridMerger', {
	extend: 'Shell.ux.grid.Panel',
	requires:[
	    'Shell.ux.form.field.SimpleComboBox'
    ],
	title: '供货明细列表',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDtl',
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
	
	//hasButtontoolbar:false,
	hasDel:false,
//	multiSelect:false,
//	selType:'rowmodel',
	
	/**默认打印模板类型*/
	defaultModelType:'2',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
	},
	initComponent: function() {
		var me = this;
		//加载条码模板组件
		me.BarcodeModel = me.BarcodeModel || Ext.create('Shell.class.rea.sale.BarcodeModel');
		//自定义按钮功能栏
		//me.buttonToolbarItems = ['refresh','add', 'del'];
		me.buttonToolbarItems = ['refresh','-',{
			xtype:'checkbox',boxLabel:'合并',itemId:'merger',
			listeners:{
				change:function(field,newValue,oldValue){
					me.mergerData(newValue);
				}
			}
		},'-',{
			fieldLabel:'模板类型',xtype:'uxSimpleComboBox',
			itemId:'ModelType',allowBlank:false,value:me.defaultModelType,
			width:200,labelWidth:55,labelAlign:'right',
			data:me.BarcodeModel.getModelList()
		},{
			xtype:'splitbutton',
            textAlign: 'left',
			iconCls:'button-print',
			text:'条码打印',
			handler:function(btn,e){btn.overMenuTrigger = true;btn.onClick(e);},
			menu:[{
				text:'直接打印',iconCls:'button-print',
				listeners:{click:function(but) {me.onBarcodePrint(1);}}
			},{
				text:'浏览打印',iconCls:'button-print',
				listeners:{click:function(but) {me.onBarcodePrint(2);}}
			},{
				text:'维护打印',iconCls:'button-print',
				listeners:{click:function(but) {me.onBarcodePrint(3);}}
			},{
				text:'设计打印',iconCls:'button-print',
				listeners:{click:function(but) {me.onBarcodePrint(4);}}
			}]
		}];
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BmsCenSaleDtl_GoodsName',sortable: false,
			text: '产品名称',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_ShortCode',sortable: false,
			text: '产品简码',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_ProdGoodsNo',sortable: false,
			text: '产品编号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_LotNo',sortable: false,
			text: '产品批号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_InvalidDate',sortable: false,
			text: '有效期',
			width: 90,
			isDate: true
		},{
			dataIndex: 'BmsCenSaleDtl_GoodsUnit',sortable: false,
			text: '包装单位',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_UnitMemo',sortable: false,
			text: '包装规格',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_GoodsQty',sortable: false,
			text: '数量',
			align:'right',
			align:'center',
			type:'int',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_IOGoodsQty',sortable: false,
			text: '提取数量',
			align:'right',
			align:'center',
			type:'int',
			width: 60,
			renderer:function(value, meta, record, rowIndex, colIndex) {
				var bo = record.get('BmsCenSaleDtl_GoodsQty');
				var v = value || 0;
				var bo = (bo == v ? true : false);
				if(!bo){
					meta.style = 'background-color:red;color:#ffffff';
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			dataIndex: 'BmsCenSaleDtl_Price',sortable: false,
			text: '单价',
			align:'right',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_SumTotal',sortable: false,
			text: '总计金额',
			align:'right',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_TaxRate',sortable: false,
			text: '税率',
			align:'right',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_ProdDate',sortable: false,
			text: '生产日期',
			align:'center',
			width: 90,
			isDate: true
		},{
			dataIndex: 'BmsCenSaleDtl_BiddingNo',sortable: false,
			text: '招标号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_Id',sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'BmsCenSaleDtl_GoodsSerial',sortable: false,
			text: '产品条码',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_LotSerial',sortable: false,
			text: '批号条码',hidden: true,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_PackSerial',sortable: false,
			text: '包装单位条码',hidden: true,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_MixSerial',sortable: false,
			text: '混合条码',hidden: false,
			width: 200,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_BmsCenSaleDoc_Comp_OrgNo',sortable: false,
			text: '供应商名称',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDtl_BmsCenSaleDoc_SaleDocNo',sortable: false,
			text: '供货单号',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDtl_Prod_OrgNo',sortable: false,
			text: '产品品牌',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_GoodsClass',sortable: false,
			text: '一级分类',
			hidden: true,
			hideable: false
		}];
		
		return columns;
	},
	/**加载数据后*/
	onAfterLoad:function(records,successful){
		var me = this;
		
		me.enableControl();//启用所有的操作功能
		
		if(me.errorInfo){
			var error = me.errorFormat.replace(/{msg}/,me.errorInfo);
			me.getView().update(error);
			me.errorInfo = null;
		}else{
			if(!records || records.length <= 0){
				var msg = me.msgFormat.replace(/{msg}/,JShell.Server.NO_DATA);
				me.getView().update(msg);
			}
		}
		
		if(!records || records.length <= 0) return;
		
		var bo = me.getComponent('buttonsToolbar').getComponent('merger').getValue();
		me.mergerData(bo);
	},
	/**@overwrite 改变返回的数据*/
	changeResult:function(data){
		var me = this;
		me.lastData = data;
		return data;
	},
	/**合并数据*/
	mergerData:function(value){
		var me = this,
			list = Ext.clone(me.lastData.list),
			len = list.length,
			map = {},
			data = [];
			
		if(value){
			for(var i=0;i<len;i++){
				var ProdGoodsNo = list[i].BmsCenSaleDtl_ProdGoodsNo;
				var LotNo = list[i].BmsCenSaleDtl_LotNo;
				var GoodsSerial = ProdGoodsNo + '+' + LotNo;
				if(!map[GoodsSerial]){
					map[GoodsSerial] = list[i];
				}else{
					var GoodsQty = list[i].BmsCenSaleDtl_GoodsQty;
					map[GoodsSerial].BmsCenSaleDtl_GoodsQty = parseInt(GoodsQty) +
						parseInt(map[GoodsSerial].BmsCenSaleDtl_GoodsQty);
				}
			}
			var i=0;
			for(var m in map){
				data[i++] = map[m];
			}
		}else{
			data = list;
		}
		
		me.store.loadData(data);
		
		if(data.length > 0){
			me.doAutoSelect(data.length - 1);//默认选中处理
		}
	},
	/**条码打印*/
	onBarcodePrint:function(type){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;
			
		if(len == 0){
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
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
		//模板标题
		LodopStr.push(me.BarcodeModel.getModelTitle(ModelType));
		
		var allRecords = me.store.data.items,
			allLen = allRecords.length,
			count = 0;
			
		for(var i=0;i<allLen;i++){
			var rec = allRecords[i];
			
			var isChecked = false;
			for(var j=0;j<len;j++){
				if(rec.get('BmsCenSaleDtl_Id') == records[j].get('BmsCenSaleDtl_Id')){
					isChecked = true;
					break;
				}
			}
			
			//单内唯一码：有100个商品表示1-100 单据号可以使用平台唯一号，也可以是供应商编制的号
			var GoodsQty = parseInt(rec.get('BmsCenSaleDtl_GoodsQty'));
			
			for(var j=0;j<GoodsQty;j++){
				count++;//单内商品唯一码累加
				if(!isChecked) continue;//没有勾选的不打印
				//模板1
				var barcode = me.BarcodeModel.getModelContent(ModelType,{
					GoodsName:rec.get('BmsCenSaleDtl_GoodsName'),//产品名称
					ShortCode:rec.get('BmsCenSaleDtl_ShortCode'),//产品简码
					InvalidDate:JShell.Date.toString(rec.get('BmsCenSaleDtl_InvalidDate'),true),//效期
					LotNo:rec.get('BmsCenSaleDtl_LotNo'),//批号
					UnitMemo:rec.get('BmsCenSaleDtl_UnitMemo'),//产品规格
					ProdOrgNo:rec.get('BmsCenSaleDtl_Prod_OrgNo'),//品牌编号
					ProdGoodsNo:rec.get('BmsCenSaleDtl_ProdGoodsNo'),//产品码(厂商产品编号)
					CompOrgNo:rec.get('BmsCenSaleDtl_BmsCenSaleDoc_Comp_OrgNo'),//供应商编号
					SaleDocNo:rec.get('BmsCenSaleDtl_BmsCenSaleDoc_SaleDocNo'),//单据号
					GoodsClass:rec.get('BmsCenSaleDtl_Goods_GoodsClass'),//一级分类
					Count:count//单内商品唯一码
				});
				LodopStr.push(barcode);
			}
		}
		
//		for(var i=0;i<len;i++){
//			//单内唯一码：有100个商品表示1-100 单据号可以使用平台唯一号，也可以是供应商编制的号
//			var GoodsQty = parseInt(records[i].get('BmsCenSaleDtl_GoodsQty'));
//			for(var j=0;j<GoodsQty;j++){
//				count++;//单内商品唯一码累加
//				//模板1
//				var barcode = me.BarcodeModel.getModelContent(ModelType,{
//					GoodsName:records[i].get('BmsCenSaleDtl_GoodsName'),//产品名称
//					ShortCode:records[i].get('BmsCenSaleDtl_ShortCode'),//产品简码
//					InvalidDate:JShell.Date.toString(records[i].get('BmsCenSaleDtl_InvalidDate'),true),//效期
//					LotNo:records[i].get('BmsCenSaleDtl_LotNo'),//批号
//					UnitMemo:records[i].get('BmsCenSaleDtl_UnitMemo'),//产品规格
//					ProdOrgNo:records[i].get('BmsCenSaleDtl_Prod_OrgNo'),//品牌编号
//					ProdGoodsNo:records[i].get('BmsCenSaleDtl_ProdGoodsNo'),//产品码(厂商产品编号)
//					CompOrgNo:records[i].get('BmsCenSaleDtl_BmsCenSaleDoc_Comp_OrgNo'),//供应商编号
//					SaleDocNo:records[i].get('BmsCenSaleDtl_BmsCenSaleDoc_SaleDocNo'),//单据号
//					GoodsClass:records[i].get('BmsCenSaleDtl_Goods_GoodsClass'),//一级分类
//					Count:count//单内商品唯一码
//				});
//				LodopStr.push(barcode);
//			}
//		}
		
		eval(LodopStr.join(""));
		
		//LODOP.SET_PRINT_MODE("POS_BASEON_PAPER",true);//该语句可使输出以纸张边缘为基点
//		LODOP.PRINT_DESIGN();
		
		var result = null;
		if(type == 1){//直接打印
			result = LODOP.PRINT();
		}else if(type == 2){//预览打印
			result = LODOP.PREVIEW();
		}else if(type == 3){//维护打印
			result = LODOP.PRINT_SETUP();
		}else if(type == 4){//设计打印
			result = LODOP.PRINT_DESIGN();
		}
		if(result != 0){
			JShell.Msg.alert(len + "个产品,共" +　count + "个条码已发送到打印机...");
		}
	}
});