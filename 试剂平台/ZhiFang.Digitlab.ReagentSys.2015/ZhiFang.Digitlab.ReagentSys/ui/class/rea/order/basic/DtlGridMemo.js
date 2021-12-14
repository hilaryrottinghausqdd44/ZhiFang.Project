/**
 * 订货明细列表
 * @author Jcall
 * @version 2018-01-02
 */
Ext.define('Shell.class.rea.order.basic.DtlGridMemo', {
	extend: 'Shell.class.rea.order.basic.ShowDtlGrid',
	title: '订货明细列表',
	
	/**获取供货单明细数据服务路径*/
	selectSaleUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL',
	
	/**默认加载数据*/
	defaultLoad: false,
	/**带功能按钮栏*/
	hasButtontoolbar: true,
	/**默认每页数量*/
	defaultPageSize: 100,
	
	/**订单主单ID*/
	OrderDocId:null,
	/**供货单明细列表*/
	SALE_DTL_LIST:[],
	
	/**查询框信息*/
	searchInfo:{
		width: 180,
		emptyText: '产品名称/产品编号',
		itemId:'search',
		isLike: true,
		fields: ['bmscenorderdtl.GoodsName', 'bmscenorderdtl.ProdGoodsNo']
	},
	
	initComponent: function() {
		var me = this;
		
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
		
		//自定义按钮功能栏
		me.buttonToolbarItems = ['refresh','-',{
			type: 'search',
			info: me.searchInfo
		},'-','save'];
		
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this,
			columns = me.callParent(arguments);
			
		columns.push({
			dataIndex:'BmsCenOrderDtl_Memo',sortable: false,
			text:'<b style="color:blue;">备注</b>',
			width:100,editor:{}
		},{
			dataIndex: 'BmsCenOrderDtl_Goods_Id',sortable: false,
			text: '产品主键ID',
			hidden: true,
			hideable: false
		});
		columns.splice(3,0,{
			dataIndex:'SaleNumber',sortable: false,
			text:'供货数',width:50,
			type:'float',align:'right'
		},{
			dataIndex:'UnSaleNumber',sortable: false,
			text:'未供数',width:50,
			type:'float',align:'right',
			renderer:function(value, meta, record, rowIndex, colIndex) {
				if(value > 0){
					meta.style = 'color:red;';
				}
				return value;
			}
		});
		columns.splice(2,0,{
			dataIndex:'BmsCenOrderDtl_CurrentQty',sortable: false,
			text:'库存数',width:50
		});
		
		return columns; 
	},
	/**保存数据*/
	onSaveClick:function(){
		var me = this,
			records = me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
		
		if(len == 0){
			JShell.Msg.alert("没有变更，不需要保存！",null,1000);
			return;
		}
		
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		me.showMask(me.saveText);//显示遮罩层
		for(var i=0;i<len;i++){
			me.updateOneDtl(records[i]);
		}
	},
	/**修改一条明细信息*/
	updateOneDtl:function(record){
		var me = this,
			url = JShell.System.Path.ROOT + me.editUrl;
			
		var entity = {
			Id:record.get('BmsCenOrderDtl_Id'),
			Memo:record.get('BmsCenOrderDtl_Memo')
		};
		var fields = [];
		for(var i in entity){
			fields.push(i);
		}
		
		var params = Ext.JSON.encode({
			entity:entity,
			fields:fields.join(',')
		});
		
		JShell.Server.post(url,params,function(data){
			if(data.success){
				me.saveCount++;
			}else{
				me.saveErrorCount++;
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				me.hideMask();//隐藏遮罩层
				if(me.saveErrorCount == 0){
					me.onSearch();
				}else{
					JShell.Msg.error("保存信息有误！");
				}
			}
		});
	},
	/**@overwrite 查询按钮点击处理方法*/
	onSearchClick: function(but, value) {
		var me = this;
		//查询栏为空时直接查询
		if (!value) {
			me.internalWhere = "";
			me.onSearch();
			return;
		}

		me.internalWhere = me.getSearchWhere(value);

		me.onSearch();
	},
	/**数据变化处理*/
	changeData:function(){
		var me = this;
			SaleNumber = 0,
			UnSaleNumber = 0;
			
		
	},
	/**获取供货单明细数据*/
	onSelectSaleDtlList:function(callback){
		var me = this,
			url = JShell.System.Path.ROOT + me.selectSaleUrl;
		
		url += '?isPlanish=true&fields=BmsCenSaleDtl_Goods_Id,BmsCenSaleDtl_GoodsQty&' +
		'where=bmscensaledtl.BmsCenSaleDoc.Status=1 and ' +
		'bmscensaledtl.BmsCenSaleDoc.BmsCenOrderDoc.Id=' + me.OrderDocId;
		
		JShell.Server.get(url,function(data){
			if(data.success){
				me.SALE_DTL_LIST = (data.value ||{}).list || [];
			}else{
				me.SALE_DTL_LIST = [];
			}
			callback();
		});
	},
	/**根据订货单ID查询数据*/
	onSearchByOrderDocId:function(OrderDocId){
		var me = this;
		
		me.OrderDocId = OrderDocId;
		me.defaultWhere = 'bmscenorderdtl.BmsCenOrderDoc.Id=' + OrderDocId;
		
		//获取供货单明细数据
		me.onSelectSaleDtlList(function(){
			me.onSearch();
		});
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me = this,
			len = data.list.length,
			dtlLen = me.SALE_DTL_LIST.length,
			GoodsID = null,
			GoodsQty = 0,
			SaleNumber = 0,
			UnSaleNumber = 0;
			
		for(var i=0;i<len;i++){
			GoodsID = data.list[i].BmsCenOrderDtl_Goods_Id;
			GoodsQty = parseFloat(data.list[i].BmsCenOrderDtl_GoodsQty);
			SaleNumber = 0;
			
			for(var j=0;j<dtlLen;j++){
				if(GoodsID == me.SALE_DTL_LIST[j].BmsCenSaleDtl_Goods_Id){
					SaleNumber += parseFloat(me.SALE_DTL_LIST[j].BmsCenSaleDtl_GoodsQty);
				}
			}
			UnSaleNumber = GoodsQty - SaleNumber;
			
			data.list[i].SaleNumber = SaleNumber;
			data.list[i].UnSaleNumber = UnSaleNumber;
		}
		
		return data;
	}
});