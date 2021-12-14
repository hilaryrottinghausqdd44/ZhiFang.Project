/**
 * 条码号列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.sale.DtlBarcodeGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '条码号列表',
	
	width:185,
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlBarcodeByHQL?isPlanish=true',
	/**保存服务地址*/
    saveUrl:'/ReagentService.svc/ST_UDTO_AddBmsCenSaleDtlBarCodeList',
	/**默认加载*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**带分页栏*/
	hasPagingtoolbar:false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl:false,
	
	/**明细表ID*/
	SaleDtlId:null,
	/**需要删除的ID列表*/
	delIdList:[],
	
	plugins:Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1}),
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
//		me.store.on({
//			load:function(){
//				me.delIdList = [];
//				me.store.removeAll();
//			}
//		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('BarcodeNumberChanged');
		//自定义按钮功能栏
		me.buttonToolbarItems = ['refresh','add','-','->','save'];
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'Status',
			width: 5,
			hideable:false,
			sortable:false,
			menuDisabled:true,
			type:'bool',
			renderer:function(value,meta){
				var v = '';
				var color = value ? 'orange' : 'green';
				
		        if(v){
		        	meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
		        }
		        
		        meta.style='background-color:' + color;
		        return v;
			}
		},{
			dataIndex: 'BmsCenSaleDtlBarcode_GoodsSerial',
			text: '产品条码',
			width: 135,
			sortable:false,
			menuDisabled:true,
			defaultRenderer: true,
			editor:{
				listeners:{
					blur:function(){
						JShell.Action.delay(function(){
							me.changeBarcodeNumber();
						},null,50);
					}
				}
			}
		},{
			xtype:'actioncolumn',
			text:'删',
			align:'center',
			width:25,
			hideable:false,
			sortable:false,
			menuDisabled:true,
			items:[{
				iconCls:'button-del hand',
				handler:function(grid,rowIndex,colIndex){
					var rec = grid.getStore().getAt(rowIndex);
					var id = rec.get(me.PKField);
					me.delIdList.push(id);
					
					me.store.removeAt(rowIndex);
					me.changeBarcodeNumber();
				}
			}]
		},{
			dataIndex:'BmsCenSaleDtlBarcode_Id',
			isKey:true,
			hideable:false,
			sortable:false,
			hidden:true
		}];
		
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		me.store.add({
			BmsCenSaleDtlBarcode_GoodsSerial:'',
			Status:true
		});
	},
	/**@overwrite 保存按钮点击处理方法*/
	onSaveClick:function(){
		var me = this,
			records = me.store.data.items,
			len = records.length,
			addList = [];
			
		for(var i=0;i<len;i++){
			var id = records[i].get('BmsCenSaleDtlBarcode_Id');
			var barcode = records[i].get('BmsCenSaleDtlBarcode_GoodsSerial');
			if(!id && barcode){
				addList.push({
					GoodsSerial:barcode
				});
			}
		}
		
		if(me.delIdList.length == 0 && addList.length == 0){
			return;
		}
		
		var url = (me.saveUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.saveUrl;
		var params = {
			SaleDtlID:me.SaleDtlId,
			SaleDtlBarCodeIDList:me.delIdList.join(','),
			BmsCenSaleDtlBarCodeList:addList
		};
		params = Ext.JSON.encode(params);
		JShell.Server.post(url,params,function(data){
			if(data.success){
				//JShell.Msg.alert(JcallShell.All.SUCCESS_TEXT,null,1000);
				me.onSearch();
			}else{
				JShell.Msg.error(data.msg);
			}
		});
	},
	changeBarcodeNumber:function(){
		var me = this,
			records = me.store.data.items,
			len = records.length,
			number = 0;
			
		for(var i=0;i<len;i++){
			var GoodsSerial = records[i].get('BmsCenSaleDtlBarcode_GoodsSerial');
			if(GoodsSerial){
				number++;
			}
		}
		
		if(me.LastBarcodeNumber != number){
			me.fireEvent('BarcodeNumberChanged',me,number);
		}
	},
	/**根据明细表ID获取明细条码列表*/
	loadBySaleDtlId:function(id){
		var me = this;
		me.SaleDtlId = id;
		me.defaultWhere = 'bmscensaledtlbarcode.BmsCenSaleDtl.Id=' + id;
		me.onSearch();
	},
	/**@public 根据where条件加载数据*/
	load:function(where,isPrivate){
		var me = this;
		me.delIdList = [];
		me.callParent(arguments);
	}
});