/**
 * 组套内项目
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.LabGroupItemGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: ['Ext.ux.CheckColumn'],
	
	title: '组套内项目表 ',
	width: 800,
	height: 500,
	
  	/**获取数据服务路径*/
	selectUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndLabCode?isPlanish=true',
	/**修改服务地址*/
	editUrl2:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_UpdateBDoctorAccountByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/WeiXinAppService.svc/ST_UDTO_DelBDoctorAccount',
	/**新增服务地址*/
    addUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_AddBLabTestItemVO',
    /**修改服务地址*/
    editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBLabTestItemByFieldVO', 
 
	/**默认加载*/
	defaultLoad: false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**项目编号*/
	ItemNo:null,
	/**实验室id*/
    ClienteleID:null,
    /**默认每页数量*/
	defaultPageSize: 500,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
		/**序号列宽度*/
	rowNumbererWidth: 30,
	formtype:'edit',
	autoSelect: false,
	canEdit : false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.on({
			beforeedit:function(editor, e) {
				return me.canEdit;
			}
		});
	},
	getPriceData:function(data){
		var me =this;
		
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var labMarketPrice = buttonsToolbar.getComponent('labMarketPrice');
		var labGreatMasterPrice = buttonsToolbar.getComponent('labGreatMasterPrice');
		var labPrice = buttonsToolbar.getComponent('labPrice');
		var list=data.list;
		var Market=0,GreatMaster=0,Price2=0;
		var MarketPrice=0,GreatMasterPrice=0,Price=0;
		for(var i=0; i<list.length;i++){
			Market=list[i].BLabGroupItemVO_BLabTestItemVO_MarketPrice;
			GreatMaster=list[i].BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice;
			Price2=list[i].BLabGroupItemVO_Price;

			if(!Market) Market=0;
			if(!GreatMaster) GreatMaster=0;
			if(!Price2) Price2=0;
            MarketPrice+=Number(Market);
            GreatMasterPrice+=Number(GreatMaster);
            Price+=Number(Price2);
		}
	   
		labMarketPrice.setValue(MarketPrice);	
		labGreatMasterPrice.setValue(GreatMasterPrice);
		labPrice.setValue(Price);
		
	},
	initComponent: function() {
		var me = this;
		me.addEvents('update','changePrice','changeGreatMasterPrice');
		me.addEvents('editClick');
		//数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = [];
		buttonToolbarItems.push({
	        xtype: 'label',
	        text: '组套内项目',
	        style: "font-weight:bold;color:blue;",
	        margin: '0 0 10 10'
		},'-',{text:'编辑',tooltip:'编辑',iconCls:'button-edit',
			itemId:'Edit',disabled:true,
			handler:function(){
				me.fireEvent('editClick', me);
//				me.onEditClick();
			}
		},{text:'删除',tooltip:'删除',iconCls:'button-del',
			itemId:'btnDel',disabled:true,
			handler:function(){
				me.onDelClick();
			}
		},'-',{
          xtype: 'displayfield',fieldLabel: '三甲价格',
          name: 'labMarketPrice',itemId:'labMarketPrice',
	      style: "font-weight:bold;color:red;",
	      fieldStyle:'font-weight:bold;color:red; padding-top:0px;',
          value: '0',labelWidth: 70,labelAlign: 'right'
      },{
          xtype: 'displayfield',fieldLabel: '内部价格',
          name: 'labGreatMasterPrice',itemId:'labGreatMasterPrice',
	      style: "font-weight:bold;color:red;", 
	      fieldStyle:'font-weight:bold;color:red; padding-top:0px;',
          value: '0',labelWidth: 70,labelAlign: 'right'
       }, {
          xtype: 'displayfield',fieldLabel: '执行价格',
          name: 'labPrice',itemId:'labPrice',
          fieldStyle:'font-weight:bold;color:red; padding-top:0px;',
	      style: "font-weight:bold;color:red;", //margin: '0 0 10 10',
          value: '0',labelWidth: 70,labelAlign: 'right'
       });
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		
		var columns = [{
			text:'项目编号',dataIndex:'BLabGroupItemVO_BLabTestItemVO_ItemNo',width:100,hidden:false,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'项目名称',dataIndex:'BLabGroupItemVO_BLabTestItemVO_CName',width:150,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'三甲价格',dataIndex:'BLabGroupItemVO_BLabTestItemVO_MarketPrice',width:100,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = Number(value) || 0;
                v = v.toFixed(4);
                v = Ext.isNumber(v) ? v : parseFloat(String(v).replace('.', '.'));
                v = isNaN(v) ? '' : String(v).replace('.', '.');
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text:'内部价格',dataIndex:'BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice',width:100,
			sortable:false,menuDisabled:true,
//			editor:{xtype:'numberfield',decimalPrecision: 4,minValue:0,
//			    listeners:{
//			    	blur :function( com,  newValue,  oldValue,  eOpts ){
//			    		me.fireEvent('changeGreatMasterPrice',me);
//			    	}
//			    }
//			},
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = Number(value) || 0;
                v = v.toFixed(4);
                v = Ext.isNumber(v) ? v : parseFloat(String(v).replace('.', '.'));
                v = isNaN(v) ? '' : String(v).replace('.', '.');
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		},{
			text:'执行价格',dataIndex:'BLabGroupItemVO_Price',width:100,
			sortable:false,menuDisabled:true,
			editor:{xtype:'numberfield',decimalPrecision: 4,minValue:0,
			    listeners:{
			    	blur :function( com,  newValue,  oldValue,  eOpts ){
						me.fireEvent('changePrice',me);
			    	}
			    }
			},
			renderer : function(value, meta, record, rowIndex, colIndex) {
				var v = Number(value) || 0;
                v = v.toFixed(4);
                v = Ext.isNumber(v) ? v : parseFloat(String(v).replace('.', '.'));
                v = isNaN(v) ? '' : String(v).replace('.', '.');
				if (v) meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}];
		return columns;
	},
	/**获取列表数据行*/
	getRecDatas:function(){
		var me = this,
		    records = me.store.data.items,
		    len = records.length;
		    
	    return records;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onEditClick:function(){
		var me = this;	
		var maxWidth = document.body.clientWidth * 0.99;
		var height = document.body.clientHeight * 0.68; 
		JShell.Win.open('Shell.class.weixin.dict.lab.dictitem.comitem.ItemPanel', {
			SUB_WIN_NO:'1',//内部窗口编号
			resizable: false,
			formtype:me.formtype,
			/**项目编号*/
			ItemNo:me.ItemNo,
			/**实验室id*/
		    ClienteleID:me.ClienteleID,
		    minHeight:450,
		    height:height,
		    RecDatas:me.getRecDatas(),
			listeners: {
				onAcceptClick:function(p,list){
					me.getRecDatasList(list);
//					me.store.add(list);      
					me.changePrice();
					me.fireEvent('update', me);
					p.close();
				}
			}
		}).show();
	},
	
	getRecDatasList:function(list){
		var me =this;
		me.store.removeAll();
		var len =list.length;
		var arr =[];
		for(var i =0 ;i<len;i++){
			var obj={
				BLabGroupItemVO_BLabTestItemVO_ItemNo:list[i].get('BLabGroupItemVO_BLabTestItemVO_ItemNo'),
				BLabGroupItemVO_BLabTestItemVO_CName:list[i].get('BLabGroupItemVO_BLabTestItemVO_CName'),
				BLabGroupItemVO_BLabTestItemVO_MarketPrice:list[i].get('BLabGroupItemVO_BLabTestItemVO_MarketPrice'),
				BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice:list[i].get('BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice'),
				BLabGroupItemVO_Price:list[i].get('BLabGroupItemVO_Price')
			}
			arr.push(obj);
		}
		me.store.add(arr);  
	},
    /**项目价格改变 */
   changePrice2 : function(DiscountVal){
   	   var me =this;
   	    me.store.each(function(record) {
   	    	var Price=record.get('BLabGroupItemVO_BLabTestItemVO_MarketPrice');
   	    	if(!Price){
   	    		PriceVal=0;
   	    	}else{
   	    		PriceVal=Number(Price);
   	    	}
   	    	var val=DiscountVal*PriceVal;
   	    	val=val.toFixed(4);
	        record.set('BLabGroupItemVO_Price', val);
	        record.commit();
	    });
   },
   
     /**项目折扣率改变
      * 项目价格=项目折扣率*三甲价格
      * */
   changeDiscountPrice: function(DiscountVal){
   	    var me =this;
   	    me.store.each(function(record) {
   	    	var Price=record.get('BLabGroupItemVO_BLabTestItemVO_MarketPrice');
   	    	if(!Price){
   	    		PriceVal=0;
   	    	}else{
   	    		PriceVal=Number(Price);
   	    	}
   	    	var val=DiscountVal*PriceVal;
   	    	val=val.toFixed(4);
	        record.set('BLabGroupItemVO_Price', val);
	        record.commit();
	    });
   },
   /**内部价格改变 */
   changeGreatMasterPrice : function(DiscountVal){
   	    var me =this;
   	    me.store.each(function(record) {
   	    	var Price=record.get('BLabGroupItemVO_BLabTestItemVO_MarketPrice');
   	    	if(!Price){
   	    		PriceVal=0;
   	    	}else{
   	    		PriceVal=Number(Price);
   	    	}
   	    	var val=DiscountVal*PriceVal;
   	    	val=val.toFixed(4);
	        record.set('BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice', val);
	        record.commit();
	    });
   },
   
    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
	    //根据BLabTestItem编号
	    if(!me.ItemNo)return;
	    if(!me.ClienteleID)return;
	    url +='&pitemNo='+me.ItemNo+'&LabCode='+me.ClienteleID;
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";
		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}
		
		return url;
	},
	/**改变价格*/
	changePrice:function(){
		var me=this;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
		var labMarketPrice = buttonsToolbar.getComponent('labMarketPrice');
		var labGreatMasterPrice = buttonsToolbar.getComponent('labGreatMasterPrice');
		var labPrice = buttonsToolbar.getComponent('labPrice');
		
		var Price=me.sumPrice();
		var MarketPrice=(Price.MarketPrice).toFixed(4);
		var GreatMasterPrice=(Price.GreatMasterPrice).toFixed(4);
		var PriceVal=(Price.Price).toFixed(4);

        MarketPrice = Ext.isNumber(MarketPrice) ? MarketPrice : parseFloat(String(MarketPrice).replace('.', '.'));
        MarketPrice = isNaN(MarketPrice) ? '' : String(MarketPrice).replace('.', '.');
                
        GreatMasterPrice = Ext.isNumber(GreatMasterPrice) ? GreatMasterPrice : parseFloat(String(GreatMasterPrice).replace('.', '.'));
        GreatMasterPrice = isNaN(GreatMasterPrice) ? '' : String(GreatMasterPrice).replace('.', '.');
    
        PriceVal = Ext.isNumber(PriceVal) ? PriceVal : parseFloat(String(PriceVal).replace('.', '.'));
        PriceVal = isNaN(PriceVal) ? '' : String(PriceVal).replace('.', '.');
        
		labMarketPrice.setValue(MarketPrice);	
		labGreatMasterPrice.setValue(GreatMasterPrice);
		labPrice.setValue(PriceVal);
	},
	/**统计价格*/
	sumPrice:function(){
		var me =this;
		var MarketPrice=0;GreatMasterPrice=0,Price=0;
		var	records = me.store.data.items;
		var count=0,len = records.length;
		for(var i=0;i<len;i++){
			Market=records[i].get('BLabGroupItemVO_BLabTestItemVO_MarketPrice');
			GreatMaster=records[i].get('BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice');
			Price2=records[i].get('BLabGroupItemVO_Price');
			if(!Market) Market=0;
			if(!GreatMaster) GreatMaster=0;
			if(!Price2) Price2=0;
            MarketPrice+=Number(Market);
            GreatMasterPrice+=Number(GreatMaster);
            Price+=Number(Price2);
		}
		var obj={
			MarketPrice:MarketPrice,
			GreatMasterPrice:GreatMasterPrice,
			Price:Price
		};
		return obj;
	},
		
    /**获取明细信息*/
	getEntity:function(PItemNo){
		var me = this,
		records = me.store.data.items,
		len = records.length;
		var dtAddList=[];
		for(var i=0;i<len;i++){
		    var rec = records[i];
			var obj = {
				LabCode:me.ClienteleID,			
				ItemNo:records[i].get('BLabGroupItemVO_BLabTestItemVO_ItemNo'),
				CName:records[i].get('BLabGroupItemVO_BLabTestItemVO_CName'),
				PItemNo:PItemNo
			}
			if(records[i].get('BLabGroupItemVO_Price')){
				obj.Price=records[i].get('BLabGroupItemVO_Price');
			}
			dtAddList.push(obj);
		}
		return dtAddList;
	},
    /**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true,
			toolbars = me.dockedItems.items || [],
			len = toolbars.length,
			items = [];

		for (var i = 0; i < len; i++) {
			if(toolbars[i].itemId!='Edit')continue;
			if (toolbars[i].xtype == 'header') continue;
			if (toolbars[i].isLocked) continue;
			var fields = toolbars[i].items.items;
			items = items.concat(fields);
		}

		var iLength = items.length;
		for (var i = 0; i < iLength; i++) {
			items[i][enable ? 'enable' : 'disable']();
		}
		if (bo) {
			me.defaultLoad = true;
		}
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
        for (var i in records) {
			me.store.remove(records[i]); 
		}
        me.fireEvent('onDelClick',me);
	},
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;

		me.enableControl(); //启用所有的操作功能
		if (me.errorInfo) {
			var msg = me.msgFormat.replace(/{msg}/, JShell.Server.NO_DATA);
			me.getView().update(msg);
			me.errorInfo = null;
		} else {
			if (!records || records.length <= 0) {
				var msg = me.msgFormat.replace(/{msg}/, JShell.Server.NO_DATA);
				me.getView().update(msg);
			}
		}

		if (!records || records.length <= 0) {
			me.fireEvent('nodata', me);
			return;
		}
		me.fireEvent('load', me);
		//默认选中处理
		me.doAutoSelect(records, me.autoSelect);
	},
		/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me=this,list=[],result={};
		var arr=[];	var count=0;
		if(data && data.value){
			count=data.value.count;
			list=data.value.list;
			len=list.length;
			for(var i=0;i<len;i++){
				var GreatMasterPrice = list[i].BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice;
				var	obj={
					BLabTestItem_LabCode:list[i].BLabTestItem_LabCode,
					BLabGroupItemVO_BLabTestItemVO_ItemNo:list[i].BLabGroupItemVO_BLabTestItemVO_ItemNo,
					BLabGroupItemVO_BLabTestItemVO_CName:list[i].BLabGroupItemVO_BLabTestItemVO_CName,
					BLabGroupItemVO_BLabTestItemVO_MarketPrice:list[i].BLabGroupItemVO_BLabTestItemVO_MarketPrice,
					BLabGroupItemVO_BLabTestItemVO_GreatMasterPrice:GreatMasterPrice,
					BLabGroupItemVO_Price:list[i].BLabGroupItemVO_Price
				};
				arr.push(obj);
			}
		}
		result.count=count;
		result.list = arr;	
		return result;
	}
	
	
});