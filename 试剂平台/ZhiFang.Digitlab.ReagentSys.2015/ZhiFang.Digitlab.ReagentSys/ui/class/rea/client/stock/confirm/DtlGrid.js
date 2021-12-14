/**
 * 验货单明细列表
 * @author liangyl
 * @version 2017-12-07
 */
Ext.define('Shell.class.rea.client.stock.confirm.DtlGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '验货单明细列表',
    requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	/**获取数据服务路径*/
	selectUrl: '/ReaSysManageService.svc/ST_UDTO_SearchReaDtlConfirmVOOfStoreInByHQL?isPlanish=true',
    /**新增客户端入库及入库明细*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaBmsInDocAndDtl',
	/**扫码服务*/
	scanCodeUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaGoodsScanCodeVOOfReaBmsInByCompIDAndSerialNo',
	
	/**默认加载*/
	defaultLoad: false,
	/**后台排序*/
	remoteSort: false,
	/**带分页栏*/
	hasPagingtoolbar: true,
	/**是否启用序号列*/
	hasRownumberer: true,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
    /**验收单ID*/
    BmsCenSaleDocConfirmID:null,
    /**浮动窗体对象*/
	WinInfo:null,
	/**浮动窗体是否已打开*/
	IsLoadWinInfo:false,
	 /**浮动货品窗体对象*/
	WinDtlPanel:null,
	/**浮动窗体是否已打开*/
	IsLoadWinDtlPanel:false,
    /**排序字段*/
	defaultOrderBy: [{property: 'BmsCenSaleDtlConfirm_LotNo',direction: 'ASC'},
		{property: 'BmsCenSaleDtlConfirm_DataAddTime',direction: 'ASC'},
		{property: 'BmsCenSaleDtlConfirm_GoodsName',direction: 'ASC'}
	],
	/**默认选中*/
	autoSelect: false,
	/**供应商ID*/
	ReaCompID:null,
	/**是否严格模式，严格1,非严格模式’0*/
	IsStrictMode:'0',
//	/**扫码模式(严格模式:strict,混合模式：mixing)*/
//	CodeScanningMode: "mixing",
	/**默认每页数量*/
	defaultPageSize: 500,
	/**带分页栏*/
	hasPagingtoolbar: false,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//隐藏货架工具栏
		var buttonsToolbar=me.getComponent('buttonsToolbar2');
        buttonsToolbar.hide();
        me.on({
        	itemclick:function(v, record) {
        		JShell.Action.delay(function(){
        			me.showWinInfo(record);
				},null,100);
			},
			select:function(RowModel, record){
				JShell.Action.delay(function(){
				    me.showWinInfo(record);
				},null,100);
			}
        });

	},
	
	initComponent: function() {
		var me = this;
		me.addEvents('onStoreInClick');
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
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr',
			text: '条码类型',hidden: true,sortable: false,width: 60,
			renderer: function(value, meta) {
				var v = "";
				if(value == "0") {
					v = "批条码";
					meta.style = "color:green;";
				} else if(value == "1") {
					v = "盒条码";
					meta.style = "color:orange;";
				} else if(value == "2") {
					v = "无条码";
					meta.style = "color:black;";
				}
				meta.tdAttr = 'data-qtip="<b>' + v + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName',
			text: '产品名称',sortable: false,width: 150,
			renderer: function(value, meta, record) {
				var v = "";
				var barCodeMgr = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr");
				if(!barCodeMgr) barCodeMgr = "";
				if(barCodeMgr == "0") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">批</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "1") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">盒</span>&nbsp;&nbsp;';
				} else if(barCodeMgr == "2") {
					barCodeMgr = '<span style="padding:1px;color:red; border:solid 1px red">无</span>&nbsp;&nbsp;';
				}
				v = barCodeMgr + value;
				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		}, {
			dataIndex: 'StorageCName',sortable: false,width: 100,
			text: '库房',defaultRenderer: true
		},{
			dataIndex: 'PlaceCName',text: '货架',sortable: false,width: 100,
			text: '货架',defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount',text: '验收数量',
			width: 65,sortable: false,type: 'int',
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_InCount',text: '已入库数量',
			width: 80,sortable: false,defaultRenderer: true
		},{
			dataIndex: 'StorageID',text: '库房ID',sortable: false,
			width: 80,hidden:true,defaultRenderer: true
		},{
			dataIndex: 'PlaceID',text: '货架ID',sortable: false,
			width: 80,hidden:true,defaultRenderer: true
		},  {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdGoodsNo',
			text: '厂商产品编号',hidden: true,sortable: false,
			width: 80,defaultRenderer: true
		},{
			dataIndex: 'GoodsQtyCount',
			text: '可入库数量',sortable: false,hidden:false,
			width: 100,defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo',
			sortable: false,text: '<b style="color:blue;">产品批号</b>',
			width: 80,
			editor: {
				allowBlank: false,
				listeners: {
					render: function(field, eOpts) {
						field.getEl().on('dblclick', function(p, el, e) {
							me.IsShowDtlInfo = false;
							me.onChooseLotNo();
						});
					}
				}
			},defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_InvalidDate',
			text: '有效期至',width: 80,
			type: 'date',sortable: false,
			isDate: true,defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price',
			sortable: false,text: '单价',width: 70,
			type: 'float',defaultRenderer: true
		},  {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal',
			sortable: false,text: '总计金额',
			width: 65,defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsUnit',
			sortable: false,text: '包装单位',
			hidden: false,width: 60,defaultRenderer: true
		},  {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_UnitMemo',
			sortable: false,text: '包装规格',
			width: 80,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_TaxRate',
			sortable: false,hidden: true,text: '税率',
			align: 'right',width: 60,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ProdDate',
			sortable: false,hidden: true,text: '生产日期',
			align: 'center',width: 90,type: 'date',
			isDate: true,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BiddingNo',
			sortable: false,text: '招标号',
			width: 80,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Id',
			sortable: false,text: '验收单明细id',
			hidden: true,hideable: false
		},{
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsSerial',
			sortable: false,text: '产品条码',
			width: 100,defaultRenderer: true
		},  {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_PackSerial',
			sortable: false,text: '包装单位条码',
			hidden: true,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotSerial',
			sortable: false,text: '批号条码',
			hidden: true,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_MixSerial',
			sortable: false,text: '混合条码',
			hidden: true,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Prod_Id',
			sortable: false,text: '供应商ID',hidden:true,
			width: 100,hidden:true,defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Prod_CName',
			sortable: false,hidden:true,
			text: '供应商',width: 100,defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr',
			sortable: false,hidden:true,
			text: '验货明细条码关系',width: 100,
		    defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtlConfirmVO_ReaBmsInDtlLinkListStr',
			sortable: false,hidden:true,
			text: '已入库扫码记录集合',width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList',
			sortable: false,hidden:true,
			text: '当次扫码记录集合',width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr',
			sortable: false,hidden:true,
			text: '是否是盒条码',width: 100,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsID',
			text: '产品ID',sortable: false,hidden:true,
			width: 150,defaultRenderer: true
		},{
			dataIndex: 'SerialNo',
			sortable: false,hidden:true,
			text: '选中的盒条码',width: 100,defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsQty',
			text: '本次入库数量',sortable: false,hidden:true,
			width: 150,defaultRenderer: true
		}, {
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_OrderGoodsID',
			sortable: false,
			text: '供应商与货品关系ID',
			hidden: true,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SaleDocConfirmNo',
			sortable: false,
			text: '验收单号',
			hidden: true,
			defaultRenderer: true
		}];
		//严格模式本次入库数量不可编辑
		if(me.IsStrictMode=='1'){
			columns.splice(7,0,{
                dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_DefalutGoodsQty',
				text: '本次入库数量',type:'int',	sortable: false,
				width: 80,defaultRenderer: true
			});
		}else{
			columns.splice(7,0,{
				dataIndex: 'BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_DefalutGoodsQty',
				text: '<b style="color:blue;">本次入库数量</b>',
				type:'int',	sortable: false,editor:{xtype:'numberfield',
					listeners: {
						change : function(com,newValue,oldValue,eOpts ){
							var	records = me.getSelectionModel().getSelection();
							if (records.length == 0) {
								JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
								return;
							}
							me.setSumTotal(newValue,records[0]);
						}
					}
				},
				width: 80,defaultRenderer: true
			});
		}
		return columns;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());
		return items;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems:function(){
		var me = this;
		var items = {
			xtype:'toolbar',
			dock:'top',
			itemId:'buttonsToolbar2',
			items:[]
		};
		return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];

		buttonToolbarItems.push('refresh','-',{
			name: 'txtScanCode',itemId: 'txtScanCode',
			emptyText: '条码号扫码',labelSeparator:'',
			labelWidth:0,width: 135,hidden:false,labelAlign: 'right',
		    xtype:'textfield',
		    enableKeyEvents:true,
		    listeners:{
            	specialkey: function(field, e) {
					if(e.getKey() == Ext.EventObject.ENTER) {
						if(!field.getValue()) {
							var info ="请输入条码号!";
			              	JShell.Msg.alert(info, null, 2000);
			              	return ;
						}
						me.onScanCode(field, e);
					}
				}
	        }
	    },'-',{
	    	fieldLabel: '库房',emptyText: '库房',
			name: 'StorageName',itemId: 'StorageName',
			labelWidth: 35,width: 165,labelAlign: 'right',
		    xtype: 'uxCheckTrigger',className: 'Shell.class.rea.client.shelves.storage.CheckGrid',
			classConfig: {
				title: '库房选择',
			    /**是否单选*/
				checkOne:true,
				width:300
			},
			listeners: {
				check: function(p, record) {
					var buttonsToolbar=me.getComponent('buttonsToolbar');
			        var StorageID = buttonsToolbar.getComponent('StorageID');
					var StorageName = buttonsToolbar.getComponent('StorageName');
					StorageID.setValue(record ? record.get('ReaStorage_Id') : '');
					StorageName.setValue(record ? record.get('ReaStorage_CName') : '');
					var id=record ? record.get('ReaStorage_Id') : '';
				    var name=record ? record.get('ReaStorage_CName') : '';
					me.onlyStorage(id,name);
					me.changeLoadPlace(id,name);
					p.close();
				}
//				change:function(com,  newValue,  oldValue,  eOpts){
//					
//					
////					
////					me.onlyStorage(com);
//				}
			}
		}, {
			xtype:'textfield',itemId:'StorageID',name:'StorageID',fieldLabel:'库房ID',hidden:true
		},'-',{text:'入库',tooltip:'入库',iconCls:'button-save',
		    handler: function() {
		    	me.fireEvent('onStoreInClick',me);
		    }
		});

		return buttonToolbarItems;
	},
	/***
	 *对选择行只设置库房
	 * 
	 * */
    onlyStorage:function(StorageID,StorageCName){
    	var me=this;
    	var records = me.getSelectionModel().getSelection();
		if (records.length == 0) {
//			JShell.Msg.error('请选择需要设置库房的数据');
			return;
		}
		var len=records.length;
//		var StorageCName=com.StorageCName;
//		var StorageID=com.StorageID;
		for(var i =0 ;i<len;i++){
			records[i].set('StorageCName',StorageCName);
			records[i].set('StorageID',StorageID);
		}
    },
	/**选中库房加载货架*/
    changeLoadPlace:function(StorageID,StorageCName){
    	var me =this;
    	me.hideMask();
    	var buttonsToolbar=me.getComponent('buttonsToolbar2');
    	buttonsToolbar.removeAll();
    	if(!StorageID){
    		me.NOPlaceTip(buttonsToolbar);
    		buttonsToolbar.hide();
    	}else{
    		var arr=[];
    		me.getPlaceById(StorageID,function(data){
    			if(data && data.value){
    			   if(data.value.list.length==0) {
	    			   	me.NOPlaceTip(buttonsToolbar);
    			   }
				   for(var i=0 ;i<data.value.list.length;i++){
				        var PlaceCName=data.value.list[i].ReaPlace_CName;
				        var PlaceID=data.value.list[i].ReaPlace_Id;
						var btn={
							xtype:'button',
							itemId:'btn'+i,
							text:PlaceCName,
							tooltip:PlaceCName,
							enableToggle:false,
							StorageCName:StorageCName,
							StorageID:StorageID,
							PlaceID:PlaceID,
							PlaceCName:PlaceCName
				        };
						buttonsToolbar.add(btn,'-');
				   }
				}else{
					me.NOPlaceTip(buttonsToolbar);
				}
    		});
    		buttonsToolbar.show();
    		for(var i = 0; i < buttonsToolbar.items.length; i++) {
		    //'-' 不处理
				if(buttonsToolbar.items.items[i].itemId){
					buttonsToolbar.items.items[i].on({
						click:function(com, e,eOpts ){
							me.cleartogglebuttonsToolbar(buttonsToolbar,com);
							com.toggle(true);
							me.setRecStoragePlace(com);
							me.getSelectionModel().deselectAll();
						}
					});
				}
			}
    	}
    },
     /**
     *不选中的按钮清空选中状态     */
	cleartogglebuttonsToolbar:function(buttonsToolbar,com){
		for(var i = 0; i < buttonsToolbar.items.length; i++) {
			if(buttonsToolbar.items.items[i].itemId){
				if(com.itemId != buttonsToolbar.items.items[i].itemId){    
					buttonsToolbar.items.items[i].toggle(false);
				}
			}
		}
	},
    /**勾选产品赋值*/
    setRecStoragePlace:function(com){
    	var me=this;
    	var records = me.getSelectionModel().getSelection();
		if (records.length == 0) {
			JShell.Msg.error('请选择需要设置库房货架的数据');
			return;
		}
		var len=records.length;
		var StorageCName=com.StorageCName;
		var StorageID=com.StorageID;
		var PlaceID=com.PlaceID;
		var PlaceCName=com.PlaceCName;
		for(var i =0 ;i<len;i++){
			records[i].set('StorageCName',StorageCName);
			records[i].set('StorageID',StorageID);
			records[i].set('PlaceID',PlaceID);
			records[i].set('PlaceCName',PlaceCName);
		}
    },
     /**没有货架提示*/
	NOPlaceTip:function(buttonsToolbar){
		var  me=this;
		var label={
			xtype:'label',
			text:'没有货架',
			style:'color: #FF0000',
			margins: '0 0 0 10'  
        };
		buttonsToolbar.add(label);
	},
    /**根据库房id获取货架*/
	getPlaceById:function(id,callback){
		var me = this;
		var url = JShell.System.Path.ROOT + '/ReaSysManageService.svc/ST_UDTO_SearchReaPlaceByHQL?isPlanish=true';
		url += '&fields=ReaPlace_Id,ReaPlace_CName&where=reaplace.ReaStorage.Id='+id;
		url +='&sort=[{"property":"ReaPlace_DispOrder","direction":"ASC"}]'
		JShell.Server.get(url,function(data){
			if(data.success){
				callback(data);
			}else{
				JShell.Msg.error(data.msg);
			}
		},false);
	},
	/**
	 * 显示货品信息
	 * @param {Object} record
	 */
	showWinInfo:function(record){
		var me=this;
		var BarCodeMgr = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr');	
		if(!me.IsLoadWinDtlPanel){
			me.onShowDtlInfo(record);
			me.IsLoadWinDtlPanel=true;
		}else{
	        var winWidth = me.getWidth();
		    var left=winWidth-280;
		    var top=(me.WinDtlPanel.height-40);
		    var info=me.changeDtlInfo(record);
		    me.WinDtlPanel.initData(info);
		    me.WinDtlPanel.setPosition( left, top);
			me.WinDtlPanel.show();
		}
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
	},
	 /**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me=this, result = {},list = [],arr=[];
		for(var i=0;i<data.list.length;i++){	
            //添加可入库数量
			var AcceptCount= data.list[i].BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount;
			var InCount= data.list[i].BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_InCount;
            if(!InCount) InCount=0;
            if(!AcceptCount) AcceptCount=0;
            var GoodsQtyCount=Number(AcceptCount)-Number(InCount);
            
			var obj1={
				GoodsQtyCount:GoodsQtyCount
			};
			var obj2 = Ext.Object.merge(data.list[i], obj1);
			arr.push(obj2);

		}
		result.list = arr;
		return data;
	},
	changeDefaultWhere:function(){
		var me=this;
		//defaultWhere追加上状态=已验收和部分入库的约束
		if(me.defaultWhere){
			var index = me.defaultWhere.indexOf('bmscensaledtlconfirm.Status in(1,2) and bmscensaledtlconfirm.AcceptCount>0 ');
			if(index == -1){
				me.defaultWhere += ' and bmscensaledtlconfirm.Status in(1,2) and bmscensaledtlconfirm.AcceptCount>0';
			}
		}else{
			me.defaultWhere = 'bmscensaledtlconfirm.Status in(1,2) and bmscensaledtlconfirm.AcceptCount>0';
		}
	},
	 /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,params = [];
		//改变默认条件
		me.changeDefaultWhere();
		me.internalWhere = '';
		//验收单ID
		if(me.BmsCenSaleDocConfirmID) {
			params.push("bmscensaledtlconfirm.BmsCenSaleDocConfirm.Id='" + me.BmsCenSaleDocConfirmID + "'");
		}else{
			params.push("bmscensaledtlconfirm.BmsCenSaleDocConfirm.Id is null");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},
	/**货品扫码*/
	onScanCode: function(field, e) {
		var me = this;
		var barCode = field.getValue();
		var indexOf = -1; //条码所在验收明细列表的行索引
		var curRecord = null; //条码所在的行记录
		var dtlConfirmLinkList = null; //当前条码为盒条码时的条码明细关系
		var LinkListList= null; //当前已入库的盒条码
		var num=0;
		me.store.each(function(rec) {
			indexOf=indexOf+1;
			var barCodeMgr = rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr");
			var LotSerial = rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotSerial");			
			switch(barCodeMgr) {
				case "0": //批条码
					if(LotSerial == barCode) {
						curRecord = rec;
						num=indexOf;
						return false;
					}
					break;
				case "1": 		
				    var dtlConfirmLinkStr = rec.get("BmsCenSaleDtlConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr");
					//已入库盒条码明细
					var LinkListStr=rec.get('BmsCenSaleDtlConfirmVO_ReaBmsInDtlLinkListStr');
					if(dtlConfirmLinkStr) dtlConfirmLinkList = Ext.JSON.decode(dtlConfirmLinkStr);
					if(LinkListStr)LinkListList= Ext.JSON.decode(LinkListStr);
					var serialNo = "";
					if(dtlConfirmLinkList) {
						Ext.Array.each(dtlConfirmLinkList, function(model) {
							serialNo = model["UsePackSerial"];
							if(serialNo == barCode) {
								num=indexOf;
								return false;
							}
						});
					}
					if(serialNo == barCode) {
						curRecord = rec;
						num=indexOf;
						return false;
					}
            		break;
				default:
					break;
			}
		});
		if(curRecord) {
			//校验本次入库数量不能大于可入库数量
			var isExect = me.GoodsQtyCheck(curRecord);
			if(!isExect) return; //me.onScanCodeInfo(barCode);
			indexOf = -1;
			var barCodeMgr = curRecord.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr");
			switch(barCodeMgr) {
				case "0": //批条码
					me.onBatchBarCode(curRecord,num);
					break;
				case "1": //盒条码
					me.onBoxBarCode(barCode, curRecord, dtlConfirmLinkList,num,LinkListList);
					break;
				default:
					me.onBatchBarCode(curRecord,num);
					break;
			}
		} else {
			me.onScanCodeInfo(barCode);
		}
	},
	/***
	 * @description 货品扫码时货品存在,条码类型为批条码处理
	 * @param {Object} record
	 */
	onBatchBarCode: function(record,num) {
		var me = this;
		var GoodsQty = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_DefalutGoodsQty');
		if(GoodsQty) GoodsQty =Number(GoodsQty);
	    GoodsQty = GoodsQty + 1;
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_DefalutGoodsQty', GoodsQty);
		me.setSumTotal(GoodsQty,record);
		me.getSelectionModel().select(num);
	},
	getLinkListListSerial:function(LinkListList,barCode){
		var me =this;
		var isExect = false;
		//如果当前扫码的条码已入库,直接提示并返回	
		Ext.Array.each(LinkListList, function(model, index) {
            if(model["UsePackSerial"] || model["SysPackSerial"]){
				serialNo = !model["UsePackSerial"] ? model["SysPackSerial"] : model["UsePackSerial"];
			}	
			if(serialNo == barCode) {
			    me.gettxtScanCode().setValue("");
		        me.gettxtScanCode().focus();
				isExect=true;
				var info = "条码为:" + barCode + "已入库,请不要重复扫码!";
				JShell.Msg.alert(info, null, 2000);
				return false;
			}
		});
		return isExect;
	},
	/***
	 * 根据本次入库数量计算总计金额
	 */
	setSumTotal:function(GoodsQty,record){
		var me = this;
		var Price=record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price');
		if(!Price)Price=0;
		var SumTotal = Number(GoodsQty)*Number(Price);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal', SumTotal);
	   	me.gettxtScanCode().setValue("");
		me.gettxtScanCode().focus();
//      me.showWinInfo(record);
	},
	/***
	 * 获取本次入库明细的总计金额
	 */
	getSumTotal:function(){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
		var SumTotal =0;	
		for(var i=0;i<len;i++){
			var rec = records[i];
			var total=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal');
			if(!total) total=0;
			SumTotal+=Number(total);
		}
		return SumTotal;
    },
	
	/***
	 * @description 货品扫码时货品存在,条码类型为盒条码处理*
	 * @param {Object} record 条码所在的行记录
	 * @param {Object} dtlConfirmLinkList 当前条码为盒条码时的条码明细关系
	 */
	onBoxBarCode: function(barCode, record, dtlConfirmLinkList,num,LinkListList) {
		var me = this;
		var GoodsQty = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_DefalutGoodsQty');
		var indexOf = -1;
		var fag=false
		//如果当前扫码的条码已入库,直接提示并返回	
		fag = me.getLinkListListSerial(LinkListList,barCode);
		if(fag) return;
		if(dtlConfirmLinkList) {
			var serialNo = "";
			Ext.Array.each(dtlConfirmLinkList, function(model, index) {
				if(model["UsePackSerial"] || model["SysPackSerial"]){
					serialNo = !model["UsePackSerial"] ? model["SysPackSerial"] : model["UsePackSerial"];
				}	
				if(serialNo == barCode) {
					indexOf = index;
					var obj={
				     	SysPackSerial:!model["SysPackSerial"] ? '' : model["SysPackSerial"],
				     	OtherPackSerial:!model["OtherPackSerial"] ? '' : model["OtherPackSerial"],
				     	UsePackSerial:model["UsePackSerial"]
				    };
				    me.getCurReaGoodsScanCode(obj,record,barCode);
					return false;
				}
			});
		}
		if(indexOf < 0) {
			JShell.Msg.alert("没有找到该条码信息!", null, 2000);
			me.getSelectionModel().select(num);
			return ;
		}	
		if(indexOf>-1){
			me.showWinInfo(record);
			GoodsQty = GoodsQty + 1;
			record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_DefalutGoodsQty', GoodsQty);
			var dtlConfirmLinkStr = "";
			if(dtlConfirmLinkList) dtlConfirmLinkStr = Ext.JSON.encode(dtlConfirmLinkList);
			me.setSumTotal(GoodsQty,record);
			me.getSelectionModel().select(num);
		}
    },
    /**
	 * 调用服务扫码
	 */
	onScanCodeInfo:function(barCode){
		var me =this;
		me.getSelectionModel().deselectAll();
		//调用服务
		me.onScanCodeUrl(barCode);
	},
    //获取当次扫码信息
    getCurReaGoodsScanCode : function(obj,record,barCode){
    	var me =this;
    	var CodeArr=[];
        CodeArr = record.get('BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList') ? record.get('BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList'):[];
        if(CodeArr.length>1){
        	CodeArr=Ext.JSON.decode(CodeArr);
            for( var i =0; i<CodeArr.length;i++){
        	 	if(CodeArr[i].UsePackSerial!=barCode){
				    CodeArr.push(obj); 
        	 	}
        	}
        }else{
        	 CodeArr.push(obj);
        }
	    var Arr=  Ext.encode(CodeArr);
        record.set('BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList',Arr);
    },

	/**
	 * 校验本次入库数量不能大于可入库数量
	 */
	GoodsQtyCheck:function(curRecord){
		var me=this;
		var isExect=true;
		//本次入库数量
		var DefalutGoodsQty=curRecord.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_DefalutGoodsQty');
		
		
		//可入库数量
		var GoodsQtyCount=curRecord.get('GoodsQtyCount');
        
        console.log('本次入库数量'+DefalutGoodsQty+'可入库数量'+GoodsQtyCount);

        if(DefalutGoodsQty)DefalutGoodsQty=Number(DefalutGoodsQty);
        if(GoodsQtyCount)GoodsQtyCount=Number(GoodsQtyCount);
		if(DefalutGoodsQty>GoodsQtyCount || DefalutGoodsQty==GoodsQtyCount){
			isExect=false;
		    curRecord.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_DefalutGoodsQty',GoodsQtyCount);
			JShell.Msg.alert('本次入库数量不能大于可入库数量', null, 2000);
		}
		return isExect;
	},
    /**
	 * @description 货品扫码,条码不存在验收明细中,调用服务处理
	 * @param {Object} barCode
	 */
	onScanCodeUrl: function(barCode) {
		var me = this;
		var url = (me.scanCodeUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.scanCodeUrl;
		var params = "?reaCompID=" + me.ReaCompID + "&serialNo=" + barCode+"&docConfirmID="+me.BmsCenSaleDocConfirmID;
		url = url + params;
		JShell.Server.get(url, function(data) {
			if(data.success) {
				if(data.value) {
					var info = data.value;
					if(info.BoolFlag == false) {
						JShell.Msg.error(info.ErrorInfo);
					} else {
						me.onScanCodeUrlAfter(info, barCode);
					}
				} else {
					JShell.Msg.error("货品扫码调用条码规则解码失败!" + data.msg);
				}
			} else {
				JShell.Msg.error("获取条码信息出错:" + data.msg);
			}
		});
	},
		/***
	 * @description 货品扫码调用服务后,获取到条码货品信息后的处理
	 * @param {Object} barCodeInfo
	 * @param {Object} barCode
	 */
	onScanCodeUrlAfter: function(barCodeInfo, barCode) {
		var me = this;
		
		var reaBarCodeVOList = barCodeInfo.ReaBarCodeVOList;
		if(reaBarCodeVOList.length <= 0) return;

		var callback = function(reaBarCodeVO) {
			if(!reaBarCodeVO) return;
			//先判断该条码的货品是否存在于验收明细列表中
			var record = null;
			me.store.each(function(rec) {
				var orderGoodsID = rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_OrderGoodsID");
				if(reaBarCodeVO.ReaGoodsOrgLinkID == orderGoodsID) {
					record = rec;
					return false;
				}
			});
			//货品存在验收明细中,但条码不存在验收的条码明细中
			if(record){
				me.onScanCodeUrlAfterOfBoxAndExistDtl(record, reaBarCodeVO, barCode);
			}else{
				JShell.Msg.error('没有找到该条码信息');
				return false; 
			}
		}
		if(reaBarCodeVOList.length > 1)
			me.onChooseReaBarCodeVO(reaBarCodeVOList, callback);
		else
			callback(reaBarCodeVOList[0]);
	},
	/***
	 * @description 货品扫码调用服务处理后,条码类型为盒条码,货品存在验收明细中,但条码不存在验收的条码明细中
	 * @param {Object} record
	 * @param {Object} reaBarCodeVO
	 * @param {Object} barCode
	 */
	onScanCodeUrlAfterOfBoxAndExistDtl: function(record, reaBarCodeVO, barCode) {
		var me = this;
		var curReaGoodsScanCodeList = record.get("BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList");
		if(curReaGoodsScanCodeList) curReaGoodsScanCodeList = JcallShell.JSON.decode(curReaGoodsScanCodeList);
		var indexOf = -1;
		if(curReaGoodsScanCodeList) {
			Ext.Array.each(curReaGoodsScanCodeList, function(model, index) {
				//使用盒条码或系统内部盒条码
				if(model["UsePackSerial"] == barCode || model["SysPackSerial"] == barCode) {
					indexOf = index;
					return false;
				}
			});
		}
		if(indexOf >= 0) return;

		//当前扫码值不存在该货品的记录行里
		if(!curReaGoodsScanCodeList) curReaGoodsScanCodeList = [];
		
		//扫码方式的值
		var scanCode = me.getScanCodeValue();
		reaBarCodeVO["BDocID"] = me.BmsCenSaleDocConfirmID; //验收单Id
		reaBarCodeVO["BDocNo"] = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SaleDocConfirmNo'); //验收单号
		reaBarCodeVO["BDtlID"] = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Id');
//		var operationVO = me.getBarcodeOperationVO(reaBarCodeVO, scanCode);
//		curReaGoodsScanCodeList.push(operationVO);

        var obj={
	     	SysPackSerial:!model["SysPackSerial"] ? '' : model["SysPackSerial"],
	     	OtherPackSerial:!model["OtherPackSerial"] ? '' : model["OtherPackSerial"],
	     	UsePackSerial:model["UsePackSerial"]
	    };
	    me.getCurReaGoodsScanCode(obj,record,barCode);
				    
 
 	    var GoodsQty = record.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_DefalutGoodsQty');
		if(GoodsQty) GoodsQty =Number(GoodsQty);
	    GoodsQty = GoodsQty + 1;
		me.setSumTotal(GoodsQty,record);
		record.set('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_DefalutGoodsQty', GoodsQty);

//		me.getSelectionModel().select(num);
	},
	
	/**@description 货品扫码输入框*/
	gettxtScanCode: function() {
		var me = this;
		var txtScanCode = me.getComponent("buttonsToolbar").getComponent("txtScanCode");
		return txtScanCode;
	},
	
	/**验货明细入库校验*/
	isVerification:function(){
		var me=this,
			records = me.store.data.items,
			isExect=true,
			len = records.length;
		if(len == 0){
			isExect=false;
		    return isExect;
		} 
		//验证
		for(var i=0;i<len;i++){
			var rec = records[i];
			var DefalutGoodsQty=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_DefalutGoodsQty');
			var GoodsQtyCount=rec.get('GoodsQtyCount');
			var BarCodeMgr=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr');
            var StorageID=rec.get('StorageID');
            var ReaGoodsName=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName');
            var ScanCodeList=rec.get('BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList');
            var LotNo=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo');
            if(!DefalutGoodsQty) DefalutGoodsQty=0;
            if(!GoodsQtyCount) GoodsQtyCount=0;

            //库房不能为空
			if(!StorageID){
				JShell.Msg.error('库房不能为空');
				isExect=false;
		        return isExect;
			}
			 //批号不能为空
			if(!LotNo){
				JShell.Msg.error('产品批号不能为空');
				isExect=false;
		        return isExect;
			}
			//本次入库数量+已入库数量不能大于验收数量
			if(Number(DefalutGoodsQty)>Number(GoodsQtyCount)){
				JShell.Msg.error('本次入库数量不能大于可入库数量');
				isExect=false;
		        return isExect;
			}
			//本次入库数量0
			if(Number(DefalutGoodsQty)==0){
				JShell.Msg.error('本次入库数量不能为0');
				isExect=false;
		        return isExect;
			}
			//盒条码入库明细条码号不能为空(严格模式)
			if(BarCodeMgr=='1' && me.IsStrictMode=='1' && !ScanCodeList){
				JShell.Msg.error('产品名称:'+ReaGoodsName+'的盒条码号还未扫码');
				isExect=false;
		        return isExect;
			}
		}
		return isExect;
	},
	/**取到验货单明细Id*/
	getDtlConfirmID:function(){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
		var strId ='';	
		for(var i=0;i<len;i++){
			var rec = records[i];
			var BmsCenSaleDtlConfirmId = rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Id');
		    if(i>0){
		    	strId+=",";
		    }
		    strId+=BmsCenSaleDtlConfirmId;
		}
		return strId;
	},
	
    /**获取入库明细信息*/
	getReaBmsInDtl:function(bmsindoc){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
		//入库明细
		var SaleDtlConfirmArr=[];
		var ReaBmsInDtlLink=[];
		var dtAddList=[];
		for(var i=0;i<len;i++){
		    var rec = records[i];
			var StorageCName = rec.get('StorageCName');
			var StorageID = rec.get('StorageID');
			var PlaceCName = rec.get('PlaceCName');
			var PlaceID = rec.get('PlaceID');
			var GoodsQty=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_DefalutGoodsQty');
			var Price=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price');
			var GoodsUnit=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsUnit');
            var SumTotal=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal');
		    var LotNo=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo');
		    var TaxRate=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_TaxRate');
		    var GoodsName=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName');
		    var GoodsID=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsID');
		    var BarCodeMgr=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_BarCodeMgr');
		    var BmsCenSaleDtlConfirmId=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Id');
            var LotSerial=rec.get('BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotSerial');
            var obj={
            	InDocNo:bmsindoc.InDocNo,
				SaleDtlConfirmID:BmsCenSaleDtlConfirmId,
				GoodsCName:GoodsName,
				GoodsUnit:GoodsUnit,
				GoodsQty:GoodsQty,
				Price:Price,
				SumTotal:SumTotal,
				TaxRate:TaxRate,
				StorageID:StorageID,
				StorageName:StorageCName,
				ReaCompanyID:bmsindoc.CompanyID,
				CompanyName:bmsindoc.CompanyName,
				LotNo:LotNo,
				LotSerial:LotSerial
            }
            //货架
            if(PlaceID){
            	obj.PlaceID=PlaceID;
				obj.PlaceName=PlaceCName;
            }
            //货品ID
		    obj.ReaGoods = {  
		    	Id:GoodsID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
		    };
		    var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
			var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
			if(userId){
				obj.CreaterID=userId;
				obj.CreaterName = userName;
			}
		    SaleDtlConfirmArr.push(obj);
		    ReaBmsInDtlLink=[];
		    //扫码明细
		    var ScanCodeList=rec.get('BmsCenSaleDtlConfirmVO_CurReaGoodsScanCodeList');
		    if(ScanCodeList.length>0){
		    	ReaBmsInDtlLink =Ext.JSON.decode(ScanCodeList);
		    }
			var entity = {
				BarCodeMgr:BarCodeMgr
			};
			if(SaleDtlConfirmArr.length>0){
				entity.ReaBmsInDtl = obj;
			}
			entity.ReaBmsInDtlLinkList = ReaBmsInDtlLink;
			dtAddList.push(entity);
		}
		return dtAddList;
	},
	
    /**显示货品明细列表*/
	ShowDtlPanel: function(grid, info) {
		var me = this;
		var winHeight = me.getHeight();
		var winWidth = me.getWidth();
		var pos = me.getPosition();
		var	config = {
			title: "货品信息",
			resizable: false,
			maximizable: false,
			modal: false,
			closable: true, //关闭功能
			draggable: true, //移动功能
			floating: true, //浮动模式
			width: 280,
			height: 350,
			alwaysOnTop: true,
			id:'win',itemId:'win',
			closeAction:'hide',
			listeners: {
				close: function(p, eOpts) {
				}
			}
		};
		me.WinDtlPanel = JShell.Win.open('Shell.class.rea.client.stock.confirm.DtlInfo', config);
		var winPostion=[pos[0]+winWidth-me.WinDtlPanel.width-10,winHeight-(me.WinDtlPanel.height-40)];
	    me.WinDtlPanel.initData(info);
	    me.WinDtlPanel.showAt(winPostion);
	},
	/**货品扫码显示货品浮动窗体信息*/
	onShowDtlInfo: function(rec) {
		var me = this;
		var info =me.changeDtlInfo(rec);
		//重置消息框的消失隐藏时间
//		me.hideTimes = 5000;
		me.ShowDtlPanel(me, info);
		me.fireEvent('onScanCodeShowDtl', me, info);
	},
	changeDtlInfo:function(rec){
		var me=this;
		var info = {
			"CName": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName") : "",
			"EName": rec ? rec.get("BmsCenSaleDtlConfirmVO_ReaGoodsEName") : "",
			"SName": rec ? rec.get("BmsCenSaleDtlConfirmVO_ReaGoodsSName") : "",
			"Unit": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_GoodsUnit") : "",
			"UnitMemo": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_UnitMemo") : "",
			"LotNo": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo") : "",
			"InvalidDate": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_InvalidDate") : "",
			"AcceptCount": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_AcceptCount") : "",
			"InCount": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_InCount") : "",
			"Price": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_Price") : "",
			"SumTotal": rec ? rec.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_SumTotal") : ""
		};
		return info;
	},
	/**@description 选择产品批号*/
	onChooseLotNo: function() {
		var me = this;
		var selected = me.getSelectionModel().getSelection();
		if(!selected || selected.length <= 0) return;
		var record = selected[0];
		var LotNo = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo");
		var ReaGoodsID = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsID");
		var ReaGoodsName = record.get("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_ReaGoodsName");
//		var maxWidth = document.body.clientWidth * 0.68;
//		var height = document.body.clientHeight * 0.78;
		var config = {
			resizable: true,
//			width: maxWidth,
//			height: height,
			GoodsID: ReaGoodsID,
			GoodsCName: ReaGoodsName,
			CurLotNo: LotNo,
			listeners: {
				accept: function(p, rec) {
					me.IsShowDtlInfo = true;
					if(rec) {
						record.set("BmsCenSaleDtlConfirmVO_BmsCenSaleDtlConfirm_LotNo", rec.get("ReaGoodsLot_LotNo"));
						record.commit();
					}
					p.close();
				}
			}
		};
		var win = JShell.Win.open('Shell.class.rea.client.stock.confirm.LotCheckGrid', config);
		win.show();
	}
});