/**
 * 供货单管理-明细列表-实验室专用
 * @author Jcall
 * @version 2017-08-09
 */
Ext.define('Shell.class.rea.sale.lab.manage.DtlGrid', {
	extend: 'Shell.ux.grid.Panel',
	title: '供货单管理-明细列表-实验室专用',
	
	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDtl',
	/**新增数据服务路径*/
	addUrl: '/ReagentSysService.svc/ST_UDTO_AddBmsCenSaleDtl',
	/**修改数据服务路径*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDtlByField',
	
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
	
	/**默认每页数量*/
	defaultPageSize: 500,
	/**分页栏下拉框数据*/
	pageSizeList:[[500,500]],
	/**默认选中*/
	autoSelect:true,
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
	/**加载的数据*/
	_lastData:null,
	/**按钮类型值*/
	_buttonType:null,
	
	/**排序字段*/
	defaultOrderBy:[
		{property:'BmsCenSaleDtl_DispOrder',direction:'ASC'},
		{property:'BmsCenSaleDtl_DataAddTime',direction:'ASC'},
		{property:'BmsCenSaleDtl_GoodsName',direction:'ASC'}
	],
	
	/**默认打印模板类型*/
	defaultModelType:'1',
	/**主单信息*/
	DocInfo:{},
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		
		me.store.on({
			update:function(store,record){
				me.onPriceOrGoodsQtyChanged(record);
			}
		});
	},
	initComponent: function() {
		var me = this;
		//单元格编辑插件
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
		//加载条码模板组件
		me.BarcodeModel = me.BarcodeModel || Ext.create('Shell.class.rea.sale.basic.BarcodeModel');
		//打印模板类型
		var ModelType = me.BarcodeModel.getLastModdelType() || me.defaultModelType;
		//自定义按钮功能栏
		me.buttonToolbarItems = ['refresh','-',{
			iconCls:'button-add',
			text:'新增',
			tooltip:'新增供货明细',
			itemId:'add',
			disabled:true,
			isLocked:true,
			handler:function(btn,e){
				me.onAddClick();
			}
		},'-',{
			iconCls:'button-del',
			text:'删除',
			tooltip:'删除供货明细',
			itemId:'del',
			disabled:true,
			isLocked:true,
			handler:function(btn,e){
				me.onDelClick();
			}
		},'-',{
			iconCls:'button-save',
			text:'保存',
			tooltip:'保存供货明细',
			itemId:'save',
			disabled:true,
			isLocked:true,
			handler:function(btn,e){
				me.onSaveClick();
			}
		},'-',{
			xtype:'checkbox',boxLabel:'合并',itemId:'merger',checked:false,
			listeners:{
				change:function(field,newValue,oldValue){
					me.mergerData(newValue);
				}
			}
		},'-',{
			fieldLabel:'模板类型',xtype:'uxSimpleComboBox',
			itemId:'ModelType',allowBlank:false,value:ModelType,
			width:200,labelWidth:55,labelAlign:'right',
			disabled:true,isLocked:true,
			data:me.BarcodeModel.getModelList(),
			listeners:{change:function(field,newValue){
				me.BarcodeModel.setLastModdelType(newValue);
			}}
		},{
			iconCls:'button-print',
			text:'直接打印',
			tooltip:'直接打印条码',
			itemId:'print1',
			disabled:true,
			isLocked:true,
			handler:function(btn,e){
				me.onBarcodePrint(1);
			}
		},'-',{
			iconCls:'button-print',
			text:'浏览打印',
			tooltip:'浏览打印条码',
			itemId:'print2',
			disabled:true,
			isLocked:true,
			handler:function(btn,e){
				me.onBarcodePrint(2);
			}
		},'->',{
			itemId: 'toTopClick',
			iconCls: 'button-up',
			text: '放大',
			tooltip: '<b>放大明细列表</b>',
			handler: function() {
				this.hide();
				me.fireEvent('toTopClick', me);
				this.ownerCt.getComponent('toDownClick').show();
			}
		}, {
			itemId: 'toDownClick',
			iconCls: 'button-down',
			text: '还原',
			tooltip: '<b>还原明细列表</b>',
			hidden: true,
			handler: function() {
				this.hide();
				me.fireEvent('toDownClick', me);
				this.ownerCt.getComponent('toTopClick').show();
			}
		},'-',{
			itemId: 'toRightClick',
			iconCls: 'button-left',
			//text: '缩小',
			tooltip: '<b>缩小明细详情</b>',
			handler: function() {
				this.hide();
				me.fireEvent('toRightClick', me);
				this.ownerCt.getComponent('toLeftClick').show();
			}
		}, {
			itemId: 'toLeftClick',
			iconCls: 'button-right',
			//text: '放大',
			tooltip: '<b>放大明细详情</b>',
			hidden: true,
			handler: function() {
				this.hide();
				me.fireEvent('toLeftClick', me);
				this.ownerCt.getComponent('toRightClick').show();
			}
		}];
		//数据列
		me.columns = me.createGridColumns();
		
		me.callParent(arguments);
	},
	
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
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
			dataIndex: 'BmsCenSaleDtl_Goods_IsPrintBarCode',
			text: '打印条码',
			width: 60,
			align: 'center',
			type: 'bool',
			isBool: true
		},{
			dataIndex: 'BmsCenSaleDtl_GoodsName',
			text: '产品名称',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_ShortCode',
			text: '产品简码',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_ProdGoodsNo',
			text: '产品编号',sortable: false,
			width: 100,
			defaultRenderer: true
		},{
			dataIndex:'BmsCenSaleDtl_LotNo',sortable: false,
			text:'<b style="color:blue;">产品批号</b>',
			width:100,editor:{}
		},{
			dataIndex: 'BmsCenSaleDtl_InvalidDate',sortable: false,
			text:'<b style="color:blue;">有效期</b>',
			width:100,type:'date',isDate:true,
			editor:{xtype:'datefield',format:'Y-m-d'}
		},{
			dataIndex:'BmsCenSaleDtl_GoodsQty',sortable: false,
			text:'<b style="color:blue;">数量</b>',
			width:60,type:'int',align:'right',
			editor:{xtype:'numberfield',minValue:0,allowBlank:false}
		},{
			dataIndex:'BmsCenSaleDtl_AcceptCount',sortable: false,
			text:'验收数量',width:60,type:'int',align:'right',hidden:true
		},{
			dataIndex:'BmsCenSaleDtl_Price',sortable: false,
			text:'<b style="color:blue;">单价</b>',
			width:80,type:'float',align:'right',
			editor:{xtype:'numberfield',minValue:0,decimalPrecision:3,allowBlank:false}
		},{
			dataIndex: 'BmsCenSaleDtl_SumTotal',sortable: false,
			text: '总计金额',
			align:'right',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'BmsCenSaleDtl_TempRange',sortable: false,
		    text:'<b style="color:blue;">温度范围</b>',
			width:100,editor:{}
		},{
			dataIndex: 'BmsCenSaleDtl_ProdDate',sortable: false,
		    text:'<b style="color:blue;">生产日期</b>',
			width:100,type:'date',isDate:true,
			editor:{xtype:'datefield',format:'Y-m-d'}
		},{
			dataIndex: 'BmsCenSaleDtl_MixSerial',sortable: false,
			width: 200,
			text:'<b style="color:orange;">混合条码</b>',
			editor:{selectOnFocus:true}
		},{
			dataIndex: 'BmsCenSaleDtl_ProdOrgName',sortable: false,
		    text:'<b style="color:blue;">生产厂家</b>',
			width:100,editor:{}
		},{
			dataIndex: 'BmsCenSaleDtl_BiddingNo',sortable: false,
		    text:'<b style="color:blue;">招标号</b>',
			width:100,editor:{}
		},{
			dataIndex:'BmsCenSaleDtl_RegisterNo',sortable: false,
			text:'<b style="color:green;">注册证编号</b>',
			width:100,editor:{}
		},{
			dataIndex: 'BmsCenSaleDtl_RegisterInvalidDate',sortable: false,
			text:'<b style="color:green;">注册证有效期</b>',
			width:100,type:'date',isDate:true,
			editor:{xtype:'datefield',format:'Y-m-d'}
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
			dataIndex: 'BmsCenSaleDtl_Id',sortable: false,
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},{
			dataIndex: 'BmsCenSaleDtl_DataAddTime',sortable: false,
			text: '新增时间',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_EName',sortable: false,
			text: '产品英文名',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_GoodsClass',sortable: false,
			text: '一级分类',
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
			dataIndex: 'BmsCenSaleDtl_BmsCenSaleDoc_Comp_OrgNo',sortable: false,
			text: '供应商名称',
			hidden: true,
			hideable: false
		},{
			dataIndex: 'BmsCenSaleDtl_Goods_Id',sortable: false,
			text: '产品ID',
			hidden: true,
			hideable: false
		}];
		
//		for(var i in columns){
//			if(columns[i].editor){
//				columns[i].editor.listeners = function(){
//					
//				}
//			}
//		}
		
		return columns;
	},
	/**总金额自动计算*/
	onPriceOrGoodsQtyChanged:function(record){
		var me = this;
		var Price = record.get('BmsCenSaleDtl_Price');
		var GoodsQty = record.get('BmsCenSaleDtl_GoodsQty');
		
		var SumTotal = parseFloat(Price) * parseInt(GoodsQty);
		
		record.set('BmsCenSaleDtl_SumTotal',SumTotal);
		record.set('BmsCenSaleDtl_AcceptCount',GoodsQty);
	},
	
	/**显示功能按钮*/
	onShowButtons:function(){
		var me = this,
			type = me._buttonType,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			add = buttonsToolbar.getComponent('add'),
			del = buttonsToolbar.getComponent('del'),
			save = buttonsToolbar.getComponent('save'),
			merger = buttonsToolbar.getComponent('merger'),
			ModelType = buttonsToolbar.getComponent('ModelType'),
			print1 = buttonsToolbar.getComponent('print1'),
			print2 = buttonsToolbar.getComponent('print2'),
			mergerValue = merger.getValue();
			
		if(type == 0){//条码未拆分，开启新增、删除、保存
			mergerValue ? add.disable() : add.enable();
			mergerValue ? del.disable() : del.enable();
			mergerValue ? save.disable() : save.enable();
			ModelType.disable();
			print1.disable();
			print2.disable();
			buttonsToolbar.show();
		}else if(type == 1){//条码已拆分，功能按钮全部禁用,保存按钮开启
			add.disable();
			del.disable();
			mergerValue ? save.disable() : save.enable();
			ModelType.disable();
			print1.disable();
			print2.disable();
			buttonsToolbar.show();
		}else if(type == 2){//已审核，只能打印条码
			buttonsToolbar.show();
			add.disable();
			del.disable();
			save.disable();
			ModelType.enable();
			print1.enable();
			print2.enable();
		}else{
			buttonsToolbar.hide();
		}
	},
	
	/**新增明细*/
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
					me.onGoodsAcceptToDtl(p,records);
				}
			}
		}).show();
	},
	/**新增明细保存*/
	onGoodsAcceptToDtl:function(p,records){
		var me = this,
			len = records.length,
			arr = [];
			
		for(var i=0;i<len;i++){
			var rec = records[i];
			arr.push({
				BmsCenSaleDoc:{Id:me.DocInfo.SaleDocID},//主单对象
				Prod:{Id:rec.get('Goods_Prod_Id')},//厂家对象
				Goods:{Id:rec.get('Goods_Id')},//产品对象
				//SaleDtlNo:'',//明细号
				SaleDocNo:me.DocInfo.SaleDocNo,//供货单号
				ProdGoodsNo:rec.get('Goods_ProdGoodsNo'),//厂家产品编码
				ProdOrgName:rec.get('Goods_ProdOrgName'),//厂家名称
				GoodsName:rec.get('Goods_CName'),//产品名称
				GoodsUnit:rec.get('Goods_UnitName'),//包装单位
				UnitMemo:rec.get('Goods_UnitMemo'),//产品规格
				GoodsQty:1,//数量
				Price:rec.get('Goods_Price'),//产品单价
				SumTotal:rec.get('Goods_Price'),//产品总价
				//TaxRate:0,//税率
				//LotNo:'',//批号
				//ProdDate:null,//生产日期
				//InvalidDate:null,//有效期
				BiddingNo:rec.get('Goods_BiddingNo'),//招标号
				IOFlag:0,//数据上传标志
				//GoodsSerial:rec.get('Goods_GoodsSerial'),//产品条码
				//PackSerial:rec.get('Goods_PackSerial'),//包装单位条码
				//LotSerial:rec.get('Goods_LotSerial'),//批号条码或自定义条码
				//MixSerial:rec.get('Goods_MixSerial'),//混合条码
				ShortCode:rec.get('Goods_ShortCode'),//代码
				BarCodeMgr:rec.get('Goods_BarCodeMgr'),//条码类型
				RegisterNo:rec.get('Goods_RegistNo'),//注册证编号
				RegisterInvalidDate:JShell.Date.toServerDate(rec.get('Goods_RegistNoInvalidDate')),//注册证有效期
				//TempRange:'',//温度范围
				ApproveDocNo:rec.get('Goods_ApproveDocNo'),//批准文号
				StorageType:rec.get('Goods_StorageType')//储藏条件
			});
		}
		me.saveLength = len;
		me.saveCount = 0;
		me.showMask(me.saveText);//显示遮罩层
		for(var i=0;i<len;i++){
			me.addOneDtl(i,Ext.JSON.encode({entity:arr[i]}));
		}
	},
	/**新增一条明细数据*/
	addOneDtl:function(index,params){
		var me = this,
			url = JShell.System.Path.ROOT + me.addUrl;
		
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
			Id:record.get('BmsCenSaleDtl_Id'),
			LotNo:record.get('BmsCenSaleDtl_LotNo'),
			InvalidDate:JShell.Date.toServerDate(record.get('BmsCenSaleDtl_InvalidDate')),
			GoodsQty:record.get('BmsCenSaleDtl_GoodsQty'),
			Price:record.get('BmsCenSaleDtl_Price'),
			SumTotal:record.get('BmsCenSaleDtl_SumTotal'),
			TempRange:record.get('BmsCenSaleDtl_TempRange'),
			ProdDate:JShell.Date.toServerDate(record.get('BmsCenSaleDtl_ProdDate')),
			MixSerial:record.get('BmsCenSaleDtl_MixSerial'),
			ProdOrgName:record.get('BmsCenSaleDtl_ProdOrgName'),
			BiddingNo:record.get('BmsCenSaleDtl_BiddingNo'),
			RegisterNo:record.get('BmsCenSaleDtl_RegisterNo'),
			RegisterInvalidDate:JShell.Date.toServerDate(record.get('BmsCenSaleDtl_RegisterInvalidDate'))
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
		if(!LODOP){
			JShell.Msg.error('LODOP插件不存在，请先安装!');
			return;
		}
		
		//模板类型
		var ModelType = me.getComponent('buttonsToolbar').getComponent('ModelType').getValue();
		//Lodop打印内容字符串数组
		var LodopStr = [];
		//模板标题
		LodopStr.push(me.BarcodeModel.getModelTitle(ModelType));
		
		var merger = me.getComponent('buttonsToolbar').getComponent('merger').getValue();
		if(merger){
			LodopStr.push(me.getBarcodeContentByMergerRecords(ModelType));
		}else{
			LodopStr.push(me.getBarcodeContentByCheckedRecords(ModelType));
		}
		
		eval(LodopStr.join(""));
		
		//LODOP.SET_PRINT_MODE("POS_BASEON_PAPER",true);//该语句可使输出以纸张边缘为基点
		//LODOP.PRINT_DESIGN();
		
		var result = null;
		if(type == 1){//直接打印
			result = LODOP.PRINT();
		}else if(type == 2){//预览打印
			result = LODOP.PREVIEW();
		}
		
		if(result != 0){
			JShell.Msg.alert(len + "个产品,共" +　count + "个条码已发送到打印机...");
		}
	},
	/**根据合并的数据产生条码*/
	getBarcodeContentByMergerRecords:function(ModelType){
		var me = this,
			selectRecords = me.getSelectionModel().getSelection(),
			selectLen = selectRecords.length,
			allRecords = Ext.clone(me.lastData.list),
			allLen = allRecords.length,
			list = [];
			
		for(var i=0;i<selectLen;i++){
			for(var j=0;j<allLen;j++){
				if(selectRecords[i].get('BmsCenSaleDtl_Goods_Id') == allRecords[j].BmsCenSaleDtl_Goods_Id &&
						selectRecords[i].get('BmsCenSaleDtl_LotNo') == allRecords[j].BmsCenSaleDtl_LotNo){
					list.push(allRecords[j]);
				}
			}
		}
		return me.getBarcodeContentByRecords(ModelType,list);
	},
	/**根据选中的数据产生条码*/
	getBarcodeContentByCheckedRecords:function(ModelType){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			list = [];
			
		for(var i=0;i<len;i++){
			list.push(records[i].data);
		}
			
		return me.getBarcodeContentByRecords(ModelType,list);
	},
	/**根据数据产生条码*/
	getBarcodeContentByRecords:function(ModelType,list){
		var me = this,
			len = list.length,
			content = [];
			
		for(var i=0;i<len;i++){
			var rec = list[i];
			
			//条码不存在的不打印
			if(!rec.BmsCenSaleDtl_MixSerial) continue;
			//条码打印标志判断
			if(!rec.BmsCenSaleDtl_Goods_IsPrintBarCode || rec.BmsCenSaleDtl_Goods_IsPrintBarCode == '0') continue;
			//打印的数量
			var num = parseInt(rec.BmsCenSaleDtl_GoodsQty);
			for(var j=0;j<num;j++){
				var barcode = me.BarcodeModel.getModelContent(ModelType,{
					GoodsName:rec.BmsCenSaleDtl_GoodsName || '',//产品名称
					ShortCode:rec.BmsCenSaleDtl_ShortCode || '',//产品简码
					InvalidDate:JShell.Date.toString(rec.BmsCenSaleDtl_InvalidDate,true) || '',//效期
					LotNo:rec.BmsCenSaleDtl_LotNo || '',//批号
					UnitMemo:rec.BmsCenSaleDtl_UnitMemo || '',//产品规格
					ProdOrgNo:rec.BmsCenSaleDtl_Prod_OrgNo || '',//品牌编号
					ProdGoodsNo:rec.BmsCenSaleDtl_ProdGoodsNo || '',//产品码(厂商产品编号)
					CompOrgNo:rec.BmsCenSaleDtl_BmsCenSaleDoc_Comp_OrgNo || '',//供应商编号
					SaleDocNo:rec.BmsCenSaleDtl_BmsCenSaleDoc_SaleDocNo || '',//单据号
					GoodsClass:rec.BmsCenSaleDtl_Goods_GoodsClass || '',//一级分类
					Barcode:rec.BmsCenSaleDtl_MixSerial || ''//条码
				});
				content.push(barcode);
			}
		}
		return content.join("");
	},
	
	/**加载数据后*/
	onAfterLoad:function(records,successful){
		var me = this;
		
		if(records && records.length > 0){
			var bo = me.getComponent('buttonsToolbar').getComponent('merger').getValue();
			me.mergerData(bo);
		}
		
		me.callParent(arguments);
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
			
		me.onShowButtons();
		
		if(value){
			for(var i=0;i<len;i++){
				var GoodsSerial = list[i].BmsCenSaleDtl_Goods_Id + '+' + list[i].BmsCenSaleDtl_LotNo;
				if(!map[GoodsSerial]){
					map[GoodsSerial] = list[i];
				}else{
					var GoodsQty = list[i].BmsCenSaleDtl_GoodsQty;
					map[GoodsSerial].BmsCenSaleDtl_GoodsQty = parseInt(GoodsQty) +
						parseInt(map[GoodsSerial].BmsCenSaleDtl_GoodsQty);
					map[GoodsSerial].BmsCenSaleDtl_SumTotal = 
						parseInt(map[GoodsSerial].BmsCenSaleDtl_GoodsQty) *
						parseFloat(map[GoodsSerial].BmsCenSaleDtl_Price);
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
			//选中第一行
			me.getSelectionModel().deselect(0);
			me.getSelectionModel().select(0);
		}
	},
	
	/**@public 刷新数据
	 * @param {Object} DocInfo
	 * @example {Object} {SaleDocID,SaleDocNo,CompId,CenOrgId}
	 */
	onSearchByDocInfo:function(DocInfo){
		var me = this;
		me.DocInfo = DocInfo;
		me.defaultWhere = 'bmscensaledtl.BmsCenSaleDoc.Id=' + DocInfo.SaleDocID;
		me.canEdit = true;
		
		if(DocInfo.IsSplit == '1'){
			me._buttonType = 1;
			me.onShowButtons();
		}else{
			me._buttonType = 0;
			me.onShowButtons();
		}
		
		this.load(null, true);
	},
	/**@public 只看模式
	 * @param {Object} id
	 */
	onSearchOnlyRead:function(id){
		var me = this;
		me.defaultWhere = 'bmscensaledtl.BmsCenSaleDoc.Id=' + id;
		me.canEdit = false;
		me._buttonType = 2;
		me.onShowButtons();
		this.load(null, true);
	}
});