/**
 * 待拆分供货单明细列表
 */
Ext.define('Shell.class.rea.sale.basic.SplitDtlGrid',{
	extend:'Shell.class.rea.sale.basic.DtlGrid',
	title:'待拆分供货单明细列表',
	
	/**拆分服务地址*/
	SplitUrl:'/ReagentService.svc/RS_UDTO_CopyBmsCenSaleDocBySaleDocID',
	/**默认加载*/
	defaultLoad: true,
	/**默认选中*/
	autoSelect:false,
	/**带分页栏*/
	hasPagingtoolbar:false,
	/**复选框*/
	multiSelect: false,
	selType: 'rowmodel',
	
	DocId:'',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar){buttonsToolbar.show();}
	},
	initComponent:function(){
		var me = this;
		
		//自定义按钮功能栏
		me.buttonToolbarItems = ['refresh','-','save'];
		
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		me.defaultWhere = 'bmscensaledtl.BmsCenSaleDoc.Id=' + me.DocId;
		  
		var columns = [{
			dataIndex: 'BmsCenSaleDtl_Goods_BarCodeMgr',
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
			dataIndex: 'BmsCenSaleDtl_GoodsName',
			text: '产品名称',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex:'BmsCenSaleDtl_GoodsQty',sortable: false,
			text:'原单数量',
			width:80,type:'int',align:'right',
			defaultRenderer: true
		},{
			dataIndex:'SplitNum',sortable: false,
			text:'<b style="color:blue;">拆分数量</b>',
			width:80,type:'int',align:'right',
			editor:{xtype:'numberfield',minValue:0,allowBlank:false}
		},{
			dataIndex: 'BmsCenSaleDtl_ShortCode',
			text: '产品简码',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_GoodsNo',
			text: '产品编号',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_ProdGoodsNo',
			text: '厂商产品编号',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex:'BmsCenSaleDtl_LotNo',sortable: false,
			text:'产品批号',
			width:100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_InvalidDate',sortable: false,
			text:'有效期',
			width:100,type:'date',isDate:true,
			defaultRenderer: true
		},{
			dataIndex:'BmsCenSaleDtl_GoodsQty',sortable: false,
			text:'数量',
			width:80,type:'int',align:'right',
			defaultRenderer: true
		},{
			dataIndex:'BmsCenSaleDtl_Price',sortable: false,
			text:'单价',
			width:80,type:'float',align:'right',
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_SumTotal',sortable: false,
			text: '总计金额',
			align:'right',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_GoodsUnit',sortable: false,
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_UnitMemo',sortable: false,
			text: '包装规格',
			width: 100,
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
			type:'date',
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
		}];
		
		return columns;
	},
	/**保存拆分结果*/
	onSaveClick:function(){
		var me = this,
			url = JShell.System.Path.ROOT + me.SplitUrl;
			records = me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length,
			errorInfo = '',
			DtlList = [];
		
		if(len == 0){
			JShell.Msg.alert("没有变更，不需要保存！",null,1000);
			return;
		}
		
		for(var i=0;i<len;i++){
			var GoodsQty = records[i].get('BmsCenSaleDtl_GoodsQty'),
				SplitNum = records[i].get('SplitNum');
			if(SplitNum > GoodsQty){
				errorInfo = '拆分的数量不能大于原单数量';
			}
			DtlList.push({
				Id:records[i].get(me.PKField),
				GoodsQty:SplitNum
			});
		}
		
		if(errorInfo){
			JShell.Msg.error(errorInfo);
			return;
		}
		
		var params = {
			saleDocID:me.DocId,
			listSaleDtl:DtlList
		};
		
		JShell.Server.post(url,Ext.JSON.encode(params),function(data){
			if(data.success){
				me.fireEvent('save',me,me.DocId);
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	}
});
