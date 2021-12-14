/**
 * 实验室供货查看-统计
 * @author Jcall
 * @version 2017-07-13
 */
Ext.define('Shell.class.rea.sale.lab.statis.Grid', {
	extend: 'Shell.ux.grid.Panel',
	title: '实验室供货查看-统计',
	requires:[
		'Shell.ux.form.field.DateArea',
	    'Shell.ux.form.field.CheckTrigger'
    ],
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentService.svc/RS_UDTO_BmsCenSaleDtlStat?isPlanish=true',
	
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: true,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	
	/**排序字段*/
	defaultOrderBy:[{property:'BmsCenSaleDtl_BmsCenSaleDoc_OperDate',direction:'DESC'}],
	
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//产品选择
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			GoodsName = buttonsToolbar.getComponent('GoodsName'),
			GoodsID = buttonsToolbar.getComponent('GoodsID');
		if(GoodsName){
			GoodsName.on({
				check:function(p, record) {
					GoodsID.setValue(record ? record.get('Goods_Id') : '');
					GoodsName.setValue(record ? record.get('Goods_CName') : '');
					p.close();
				},
				change:function(){me.onSearch();}
			});
		}
	},
	initComponent: function() {
		var me = this;
		
		var defaultWhere = 'goods.CenOrgConfirm=1 and goods.CompConfirm=1 and goods.CenOrg.Id=' + JShell.REA.System.CENORG_ID;
		
		var end = new Date();
		var start = JShell.Date.getNextDate(end,-30);
		
		me.buttonToolbarItems = ['refresh','-',{
			xtype:'uxdatearea',itemId:'date',fieldLabel:'操作日期',
			value:{start:start,end:end},
			listeners:{
				enter:function(){
					me.onSearch();
				}
			}
		},{
			width:180,labelWidth:30,labelAlign:'right',
			xtype:'uxCheckTrigger',itemId:'GoodsName',fieldLabel:'产品',
			className:'Shell.class.rea.goods.CheckGrid',
			classConfig: {
				checkOne:true,
				defaultWhere:defaultWhere
			}
		},{
			xtype:'textfield',itemId:'GoodsID',fieldLabel:'产品主键ID',hidden:true
		},{
			width:180,labelWidth:60,labelAlign:'right',
			xtype:'textfield',itemId:'GoodsLotNo',fieldLabel:'产品批号',
			enableKeyEvents:true,
			listeners:{
	            keyup:function(field,e){
                	if(e.getKey() == Ext.EventObject.ESC){
                		field.setValue('');
                		me.onSearch();
                	}else if(e.getKey() == Ext.EventObject.ENTER){
                		me.onSearch();
                	}
                }
	        }
		},'-','searchb'];
		
		//创建数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BmsCenSaleDtl_BmsCenSaleDoc_OperDate',
			text: '操作时间',
			align:'center',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenSaleDtl_BmsCenSaleDoc_SaleDocNo',sortable: false,
			text: '供货单号',
			width: 100,
			defaultRenderer: true
		},{
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
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_SumTotal',sortable: false,
			text: '金额',
			align:'right',
			width: 80,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_BmsCenSaleDoc_Lab_CName',
			text: '订货方',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_BmsCenSaleDoc_Comp_CName',
			text: '供货方',
			width: 100,
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
			dataIndex: 'BmsCenSaleDtl_BarCodeMgr',
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
			}
		},{
			dataIndex: 'BmsCenSaleDtl_Id',sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			date = buttonsToolbar.getComponent('date'),
			GoodsID = buttonsToolbar.getComponent('GoodsID'),
			GoodsLotNo = buttonsToolbar.getComponent('GoodsLotNo');

		var url = JShell.System.Path.ROOT + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		url += '&labID=' + JShell.REA.System.CENORG_ID;//本实验室
		url += '&listStatus=1';
		
		if(date){
			var value = date.getValue();
			var isValid = date.isValid();
			if(value && isValid){
				if(value.start){
					if(isValid){
						url += '&beginDate=' + JShell.Date.toString(value.start,true);
					}
				}
				if(value.end){
					if(isValid){
						url += '&endDate=' + JShell.Date.toString(value.end,true);
					}
				}
			}
		}
		if(GoodsID){
			var value = GoodsID.getValue();
			if(value){
				url += '&goodID=' + value;
			}
		}
		if(GoodsLotNo){
			var value = GoodsLotNo.getValue();
			if(value){
				url += '&goodLotNo=' + value;
			}
		}
		return url;
	},
	/**查询*/
	onSearchBClick:function(){
		this.onSearch();
	}
});