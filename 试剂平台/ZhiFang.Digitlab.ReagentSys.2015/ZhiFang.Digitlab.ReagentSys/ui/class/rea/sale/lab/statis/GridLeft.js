/**
 * 实验室试剂-采购统计-总计
 * @author Jcall
 * @version 2017-07-21
 */
Ext.define('Shell.class.rea.sale.lab.statis.GridLeft', {
	extend: 'Shell.ux.grid.Panel',
	title: '实验室试剂-采购统计计-总计',
	requires:[
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.DateArea',
	    'Shell.ux.form.field.CheckTrigger'
    ],
    
    width:535,
    height:600,
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentService.svc/RS_UDTO_BmsCenSaleDtlStat',
	/**导出Excel数据服务路径*/
	outExcelUrl: '/ReagentService.svc/RS_UDTO_BmsCenSaleDtlStatExcel',
	
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
	defaultOrderBy:[{property:'BmsCenSaleDtl_GoodsName',direction:'ASC'}],
	
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		//供应商选择
		var topToolabr2 = me.getComponent('topToolabr2'),
			CompName = topToolabr2.getComponent('CompName'),
			CompID = topToolabr2.getComponent('CompID');
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
		//产品选择
		var topToolabr2 = me.getComponent('topToolabr2'),
			GoodsName = topToolabr2.getComponent('GoodsName'),
			GoodsID = topToolabr2.getComponent('GoodsID');
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
		
		var end = new Date();
		var start = JShell.Date.getNextDate(end,-30);
		
		me.dockedItems = [{
			xtype:'uxButtontoolbar',
			itemId:'topToolabr1',
			dock:'top',
			items:['refresh','-',{
				xtype:'uxdatearea',itemId:'date',fieldLabel:'日期范围',
				labelAlign:'right',value:{start:start,end:end},
				listeners:{
					enter:function(){
						me.onSearch();
					}
				}
			}]
		},{
			xtype:'uxButtontoolbar',
			itemId:'topToolabr2',
			dock:'top',
			items:[{
				width:160,labelWidth:50,labelAlign:'right',
				xtype:'uxCheckTrigger',itemId:'CompName',fieldLabel:'供应商',
				className:'Shell.class.rea.cenorgcondition.ParentCheckGrid',
				classConfig: {
					checkOne:true,
					CenOrgId:JShell.REA.System.CENORG_ID
				}
			},{
				xtype:'textfield',itemId:'CompID',fieldLabel:'供应商主键ID',hidden:true
			},{
				width:140,labelWidth:30,labelAlign:'right',
				xtype:'uxCheckTrigger',itemId:'GoodsName',fieldLabel:'产品',
				className:'Shell.class.rea.goods.CheckGrid',
				classConfig: {
					checkOne:true,
					defaultWhere:'goods.CenOrgConfirm=1 and goods.CompConfirm=1 and goods.CenOrg.Id=' + JShell.REA.System.CENORG_ID
				}
			},{
				xtype:'textfield',itemId:'GoodsID',fieldLabel:'产品主键ID',hidden:true
			},'-','searchb','-', {
				text:'导出',
				tooltip:'按条件导出成Excel文件！',
				iconCls:'file-excel',
				itemId:'outButton',
				handler:function(){me.onOutClick();}
			}]
		}];
		
		//创建数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'BmsCenSaleDtl_Goods_ShortCode',sortable: false,
			text: '试剂简称',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_CName',sortable: false,
			text: '中文名',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_GoodsNo',sortable: false,
			text: '产品编码',
			width: 100,
			hidden:true,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_GoodsUnit',sortable: false,
			text: '包装单位',
			width: 60,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_UnitMemo',sortable: false,
			text: '包装规格',
			width: 60,
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
			dataIndex: 'BmsCenSaleDtl_Goods_Prod_CName',sortable: false,
			text: '生产厂家',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_Id',sortable: false,
			text:'试剂ID',hidden:true,hideable:false,isKey:true
		}];
		
		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			topToolabr1 = me.getComponent('topToolabr1'),
			topToolabr2 = me.getComponent('topToolabr2'),
			date = topToolabr1.getComponent('date'),
			CompID = topToolabr2.getComponent('CompID'),
			GoodsID = topToolabr2.getComponent('GoodsID');

		var url = JShell.System.Path.ROOT + me.selectUrl;
		url += '?fields=' + me.getStoreFields(true).join(',');
		url += '&labID=' + JShell.REA.System.CENORG_ID;//本实验室
		url += '&listStatus=1&isPlanish=true&groupbyType=1';
		
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
		if(CompID){
			var value = CompID.getValue();
			if(value){
				url += '&compID=' + value;
			}
		}
		if(GoodsID){
			var value = GoodsID.getValue();
			if(value){
				url += '&goodID=' + value;
			}
		}
		return url;
	},
	/**查询*/
	onSearchBClick:function(){
		this.onSearch();
	},
	/**导出供货单Excel*/
	onOutClick:function(){
		var me = this,
			url = JShell.System.Path.ROOT + me.outExcelUrl,
			topToolabr1 = me.getComponent('topToolabr1'),
			topToolabr2 = me.getComponent('topToolabr2'),
			date = topToolabr1.getComponent('date'),
			CompID = topToolabr2.getComponent('CompID'),
			GoodsID = topToolabr2.getComponent('GoodsID');
			
		//验收时间（倒序）+供货单号（正序）+货品编码（正序）
		var sort = [
			{"property":"BmsCenSaleDtl_BmsCenSaleDoc_AccepterTime","direction":"DESC"},
			{"property":"BmsCenSaleDtl_BmsCenSaleDoc_SaleDocNo","direction":"ASC"},
			{"property":"BmsCenSaleDtl_Goods_GoodsNo","direction":"ASC"}
		];
		me.UpdateForm = me.UpdateForm || Ext.create('Ext.form.Panel',{
			items:[
				{xtype:'filefield',name:'file'},
				{xtype:'textfield',name:'labID',value:JShell.REA.System.CENORG_ID},
				{xtype:'textfield',name:'listStatus',value:1},
				{xtype:'textfield',name:'isPlanish',value:1},
				{xtype:'textfield',name:'groupbyType',value:1},
				{xtype:'textfield',name:'beginDate'},
				{xtype:'textfield',name:'endDate'},
				{xtype:'textfield',name:'compID'},
				{xtype:'textfield',name:'goodID'}
			]
		});
		//清空数据
		me.UpdateForm.getForm().setValues({beginDate:'',endDate:'',compID:'',goodID:''});
		
		if(date){
			var value = date.getValue();
			var isValid = date.isValid();
			if(value && isValid){
				if(value.start){
					if(isValid){
						me.UpdateForm.getForm().setValues({'beginDate':JShell.Date.toString(value.start,true)});
					}
				}
				if(value.end){
					if(isValid){
						me.UpdateForm.getForm().setValues({'endDate':JShell.Date.toString(value.end,true)});
					}
				}
			}
		}
		if(CompID){
			var value = CompID.getValue();
			if(value){
				me.UpdateForm.getForm().setValues({'compID':value});
			}
		}
		if(GoodsID){
			var value = GoodsID.getValue();
			if(value){
				me.UpdateForm.getForm().setValues({'goodID':value});
			}
		}
		
		me.showMask("数据请求中...");
		var url = JShell.System.Path.ROOT + me.outExcelUrl;
		me.UpdateForm.getForm().submit({
			url:url,
            //waitMsg:JShell.Server.SAVE_TEXT,
            success:function (form,action) {
            	me.hideMask();
        		var fileName = action.result.ResultDataValue;
        		var downloadUrl = JShell.System.Path.ROOT + '/ReagentService.svc/RS_UDTO_DownLoadExcel';
				downloadUrl += '?isUpLoadFile=1&operateType=0&downFileName=采购统计数据&fileName=' + fileName.split('\/')[2];
				downloadUrl = encodeURI(downloadUrl);
				window.open(downloadUrl);
            },
            failure:function(form,action){
            	me.hideMask();
				JShell.Msg.error(action.result.ErrorInfo);
			}
		});
	}
});