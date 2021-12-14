/**
 * 订货明细列表-基础
 * @author Jcall
 * @version 2017-05-13
 */
Ext.define('Shell.class.rea.order.basic.DtlGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '订货明细列表-基础',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDtlByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelBmsCenOrderDtl',
	/**新增数据服务路径*/
	addUrl: '/ReagentSysService.svc/ST_UDTO_AddBmsCenOrderDtl',
	/**修改数据服务路径*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateBmsCenOrderDtlByField',
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,

	/**排序字段*/
	defaultOrderBy:[
		{property:'BmsCenOrderDtl_DataAddTime',direction:'ASC'},
		{property:'BmsCenOrderDtl_GoodsName',direction:'ASC'}
	],
	
	/**默认选中*/
	autoSelect:false,
	
	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用保存按钮*/
	hasSave: true,
	
	/**主单信息*/
	DocInfo:{
		OrderDocID:'',//订货单主单ID
		OrderDocNo:'',//订货单主单No
		CompId:'',//供应商ID
		CenOrgId:''//机构ID
	},
	
	/**可编辑*/
	canEdit:false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		if(buttonsToolbar){buttonsToolbar.hide();}
		
		me.store.on({
			update:function(store,record){
				me.onPriceOrGoodsQtyChanged(record);
			},
			datachanged:function(store){
				me.store.each(function(record){
					me.onPriceOrGoodsQtyChanged(record);
					record.commit();
				});
			}
		});
		
		me.on({
			beforeedit:function(editor, e) {
				return me.canEdit;
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BmsCenOrderDtl_GoodsName',
			text: '产品名称',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_ProdGoodsNo',
			text: '产品编号',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex:'BmsCenOrderDtl_GoodsQty',sortable: false,
			text:'<b style="color:blue;">数量</b>',
			width:80,type:'int',align:'right',
			editor:{xtype:'numberfield',minValue:0,allowBlank:false}
		},{
			dataIndex:'BmsCenOrderDtl_Price',sortable: false,
			text:'<b style="color:blue;">单价</b>',
			width:80,type:'float',align:'right',
			editor:{xtype:'numberfield',minValue:0,decimalPrecision:3,allowBlank:false}
		},{
			dataIndex: 'BmsCenOrderDtl_SumTotal',sortable: false,
			text: '总计金额',
			align:'right',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_GoodsUnit',sortable: false,
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_UnitMemo',sortable: false,
			text: '包装规格',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_TaxRate',sortable: false,
			text: '税率',
			align:'right',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_ProdDate',sortable: false,
			text: '生产日期',
			align:'center',
			width: 90,
			type:'date',
			isDate: true
		},{
			dataIndex: 'BmsCenOrderDtl_BiddingNo',sortable: false,
			text: '招标号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_Id',sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'BmsCenOrderDtl_GoodsSerial',sortable: false,
			text: '产品条码',hidden: true,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_LotSerial',sortable: false,
			text: '批号条码',hidden: true,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_PackSerial',sortable: false,
			text: '包装单位条码',hidden: true,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenOrderDtl_MixSerial',sortable: false,
			text: '混合条码',hidden: true,
			width: 100,
			defaultRenderer: true
		}];
		
		return columns;
	},
	/**新增勾选*/
	onAddClick:function(){
		var me = this;
		if(!me.DocInfo.CenOrgId){
			JShell.Msg.warning('CenOrgId参数不存在!');
		}
		var defaultWhere = 'goods.CenOrgConfirm=1 and goods.CompConfirm=1 and goods.Comp.Id=' + 
			me.DocInfo.CompId + 'and goods.CenOrg.Id=' + me.DocInfo.CenOrgId;
			
		JShell.Win.open('Shell.class.rea.goods.CheckGrid',{
			defaultWhere:defaultWhere,
			listeners:{
				accept:function(p,records){
					me.onAccept(p,records);
				}
			}
		}).show();
	},
	/**保存*/
	onSaveClick:function(){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
			
		if(len == 0){
			JShell.Msg.alert("没有变更，不需要保存！");
			return;
		}
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			me.updateOne(i,records[i]);
		}
	},
	/**确定保存*/
	onAccept:function(p,records){
		var me = this,
			len = records.length,
			arr = [];
			
		for(var i=0;i<len;i++){
			var rec = records[i];
			arr.push({
				BmsCenOrderDoc:{Id:me.DocInfo.OrderDocID},
				OrderDtlNo:'1',
				OrderDocNo:me.DocInfo.OrderDocNo,
				Goods:{Id:rec.get('Goods_Id')},
				ProdGoodsNo:rec.get('Goods_ProdGoodsNo'),
				Prod:{Id:rec.get('Goods_Prod_Id')},
				ProdOrgName:rec.get('Goods_ProdOrgName'),
				GoodsName:rec.get('Goods_CName'),
				GoodsUnit:rec.get('Goods_UnitName'),
				UnitMemo:rec.get('Goods_UnitMemo'),
				GoodsQty:1,
				Price:rec.get('Goods_Price'),
				IOFlag:0
			});
		}
		//p.close();
		me.saveLength = len;
		me.saveCount = 0;
		me.showMask(me.saveText);//显示遮罩层
		for(var i=0;i<len;i++){
			me.addOne(i,Ext.JSON.encode({entity:arr[i]}));
		}
	},
	/**保存一条数据*/
	addOne:function(index,params){
		var me = this;
		var url = JShell.System.Path.ROOT + me.addUrl;
		
		setTimeout(function(){
			JShell.Server.post(url,params,function(data){
				me.saveCount++;
				if(me.saveCount == me.saveLength){
					me.hideMask();//隐藏遮罩层
					me.onSearch();
				}
			});
		},100 * index);
	},
	/**修改信息*/
	updateOne:function(i,record){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var params = Ext.JSON.encode({
			entity:{
				Id:record.get('BmsCenOrderDtl_Id'),
				GoodsQty:record.get('BmsCenOrderDtl_GoodsQty'),
				Price:record.get('BmsCenOrderDtl_Price')
			},
			fields:'Id,GoodsQty,Price'
		});
		JShell.Server.post(url,params,function(data){
			if(data.success){
				me.saveCount++;
				if(record){
					record.set(me.DelField,true);
					record.commit();
				}
			}else{
				me.saveErrorCount++;
				if(record){
					record.set(me.DelField,false);
					record.commit();
				}
			}
			if(me.saveCount + me.saveErrorCount == me.saveLength){
				me.hideMask();//隐藏遮罩层
				if(me.saveErrorCount == 0){
					me.onSearch();
				}else{
					JShell.Msg.error("保存信息有误！");
				}
			}
		},false);
	},
	/**总金额自动计算*/
	onPriceOrGoodsQtyChanged:function(record){
		var me = this;
		var Price = record.get('BmsCenOrderDtl_Price');
		var GoodsQty = record.get('BmsCenOrderDtl_GoodsQty');
		
		var SumTotal = parseFloat(Price) * parseInt(GoodsQty);
		TotalPrice = SumTotal ? SumTotal.toFixed(2) : 0;
		record.set('BmsCenOrderDtl_SumTotal',SumTotal);
	},
	/**
	 * 刷新数据
	 * @param {Object} DocInfo
	 * @example {Object} {SaleDocID,SaleDocNo,CompId,CenOrgId}
	 */
	onSearchByDocInfo:function(DocInfo){
		var me = this;
		me.DocInfo = DocInfo;
		me.defaultWhere = 'bmscenorderdtl.BmsCenOrderDoc.Id=' + DocInfo.OrderDocID;
		me.canEdit = true;
		me.getComponent('buttonsToolbar').show();
		this.load(null, true);
	},
	/**只看模式*/
	onSearchOnlyRead:function(id){
		var me = this;
		me.defaultWhere = 'bmscenorderdtl.BmsCenOrderDoc.Id=' + id;
		me.canEdit = false;
		me.getComponent('buttonsToolbar').hide();
		this.load(null, true);
	}
});