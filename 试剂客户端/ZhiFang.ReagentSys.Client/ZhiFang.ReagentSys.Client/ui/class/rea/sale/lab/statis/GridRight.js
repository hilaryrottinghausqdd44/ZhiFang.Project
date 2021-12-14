/**
 * 实验室试剂-采购统计-明细
 * @author Jcall
 * @version 2017-07-21
 */
Ext.define('Shell.class.rea.sale.lab.statis.GridRight', {
	extend: 'Shell.ux.grid.Panel',
	title: '实验室试剂-采购统计-明细',
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
	/**序号列宽度*/
	rowNumbererWidth: 35,
	
	/**排序字段*/
	defaultOrderBy:[{property:'BmsCenSaleDtl_BmsCenSaleDoc_OperDate',direction:'DESC'}],
	
	/**开始时间*/
	Start:null,
	/**结束时间*/
	End:null,
	/**试剂ID*/
	GoodsID:null,
	/**供应商ID*/
	CompID:null,
	
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//供应商选择
		var buttonsToolbar = me.getComponent('buttonsToolbar'),
			CompName = buttonsToolbar.getComponent('CompName'),
			CompID = buttonsToolbar.getComponent('CompID');
		if(CompName){
			CompName.on({
				check:function(p, record) {
					CompID.setValue(record ? record.get('CenOrgCondition_cenorg1_Id') : '');
					CompName.setValue(record ? record.get('CenOrgCondition_cenorg1_CName') : '');
					p.close();
				},
				change:function(){me.onSearch();}
			});
		}
	},
	initComponent: function() {
		var me = this;
		me.addEvents('toMaxClick', 'toMinClick');
		var defaultWhere = 'goods.CenOrgConfirm=1 and goods.CompConfirm=1 and goods.CenOrg.Id=' + JShell.REA.System.CENORG_ID;
		
		var end = new Date();
		var start = JShell.Date.getNextDate(end,-30);
		
		me.buttonToolbarItems = [{
			itemId: 'toMaxClick',
			iconCls: 'button-right',
			text: '放大',
			tooltip: '<b>放大</b>',
			handler: function() {
				this.hide();
				me.fireEvent('toMaxClick', me);
				this.ownerCt.getComponent('toMinClick').show();
			}
		}, {
			itemId: 'toMinClick',
			iconCls: 'button-left',
			text: '还原',
			tooltip: '<b>还原</b>',
			hidden: true,
			handler: function() {
				this.hide();
				me.fireEvent('toMinClick', me);
				this.ownerCt.getComponent('toMaxClick').show();
			}
		}, '-','refresh','-',{
			width:160,labelWidth:50,labelAlign:'right',hidden:true,
			xtype:'uxCheckTrigger',itemId:'CompName',fieldLabel:'供应商',
			className:'Shell.class.rea.cenorgcondition.ParentCheckGrid',
			classConfig: {
				checkOne:true,
				CenOrgId:JShell.REA.System.CENORG_ID
			}
		},{
			xtype:'textfield',itemId:'CompID',fieldLabel:'供应商主键ID',hidden:true
		},{
			width:160,labelWidth:60,labelAlign:'right',
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
			dataIndex: 'BmsCenSaleDtl_BmsCenSaleDoc_OperDate',sortable: false,
			text: '采购日期',
			align:'center',
			width: 130,
			isDate:true,
			hasTime:true
		},{
			dataIndex: 'BmsCenSaleDtl_BmsCenSaleDoc_CompanyName',sortable: false,
			text: '供应商',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_LotNo',sortable: false,
			text: '产品批号',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_GoodsQty',sortable: false,
			text: '采购数量',
			align:'right',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_SumTotal',sortable: false,
			text: '采购金额',
			align:'right',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_BmsCenSaleDoc_SaleDocNo',sortable: false,
			text: '供货单号',
			width: 150,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_BmsCenSaleDoc_Id',sortable: false,
			text: '主单主键ID',
			hidden: true,
			hideable: false
		}];
		
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			date = buttonsToolbar.getComponent('date'),
			CompID = buttonsToolbar.getComponent('CompID'),
			GoodsLotNo = buttonsToolbar.getComponent('GoodsLotNo');

		var url = JShell.System.Path.ROOT + me.selectUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		url += '&labID=' + JShell.REA.System.CENORG_ID;//本实验室
		url += '&listStatus=1';
		
		if(me.GoodsID){
			url += '&goodID=' + me.GoodsID;
		}
		if(me.Start){
			url += '&beginDate=' + JShell.Date.toString(me.Start,true);
		}
		if(me.End){
			url += '&endDate=' + JShell.Date.toString(me.End,true);
		}
		if(me.CompID){
			url += '&compID=' + me.CompID;
		}
		
		if(CompID){
			var value = CompID.getValue();
			if(value){
				url += '&compID=' + value;
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
	},
	/**
	 * @public 对外查询
	 * @param {Object} Start 开始时间
	 * @param {Object} End 结束时间
	 * @param {Object} GoodsID 试剂ID
	 * @param {Object} CompID 供应商ID
	 */
	onSearchByParams:function(Start,End,GoodsID,CompID){
		var me = this;
		
		me.Start = Start;
		me.End = End;
		me.GoodsID = GoodsID;
		me.CompID = CompID;
		
		me.onSearch();
	}
});