/**
 * 下级机构试剂列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.goods.confirm.children.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '下级机构试剂列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchGoodsByHQL?isPlanish=true',
	/**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateGoodsByField',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**默认加载数据*/
	defaultLoad: false,
	/**排序字段*/
	//defaultOrderBy:[{property:'Goods_DispOrder',direction:'ASC'}],

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);

		me.on({
			itemdblclick: function(view, record) {
				me.showForm(record.get(me.PKField));
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('toMaxClick','toMinClick');
		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();

		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [{
			itemId:'toMaxClick',
			iconCls:'button-right',
			text:'放大',
			tooltip:'<b>放大</b>',
			handler:function(){
				this.hide();
				me.fireEvent('toMaxClick',me);
				this.ownerCt.getComponent('toMinClick').show();
			}
		},{
			itemId:'toMinClick',
			iconCls:'button-left',
			text:'还原',
			tooltip:'<b>还原</b>',
			hidden:true,
			handler:function(){
				this.hide();
				me.fireEvent('toMinClick',me);
				this.ownerCt.getComponent('toMaxClick').show();
			}
		},'-','refresh', '-', {
			xtype:'splitbutton',
            textAlign: 'left',
			iconCls:'button-edit',
			text:'批量修改',
			handler:function(btn,e){btn.overMenuTrigger = true;btn.onClick(e);},
			menu:[{
				text:'条码类型',iconCls: 'button-edit',tooltip: '勾选试剂批量修改条码类型',
				listeners:{click:function(but) {me.onChangeBarcodeType();}}
			},{
				text:'有注册证',iconCls: 'button-accept',tooltip: '勾选试剂批量修改为"有注册证"',
				listeners:{click:function(but) {me.onChangeInfo('IsRegister',1);}}
			},{
				text:'无注册证',iconCls: 'button-cancel',tooltip: '勾选试剂批量修改为"无注册证"',
				listeners:{click:function(but) {me.onChangeInfo('IsRegister',0);}}
			},{
				text:'打印条码',iconCls: 'button-accept',tooltip: '勾选试剂批量修改为"打印条码"',
				listeners:{click:function(but) {me.onChangeInfo('IsPrintBarCode',1);}}
			},{
				text:'不打印条码',iconCls: 'button-cancel',tooltip: '勾选试剂批量修改为"不打印条码"',
				listeners:{click:function(but) {me.onChangeInfo('IsPrintBarCode',0);}}
			}]
		}, '-', {
			xtype:'splitbutton',
            textAlign: 'left',
			iconCls:'button-edit',
			text:'确认操作',
			handler:function(btn,e){btn.overMenuTrigger = true;btn.onClick(e);},
			menu:[{
				text:'实验室确认',iconCls: 'button-accept',tooltip: '实验室对供应商试剂进行确认',
				listeners:{click:function(but) {me.onCenOrgConfirm(true);}}
			},{
				text:'取消确认',iconCls: 'button-cancel',tooltip: '实验室取消对供应商试剂的确认',
				listeners:{click:function(but) {me.onCenOrgConfirm(false);}}
			}]
		},'-',{
			xtype: 'button',
			iconCls: 'file-excel',
			text: '试剂导出',
			tooltip: 'EXCEL导入导出试剂',
			menu: [{
				iconCls: 'file-excel',
				text: '试剂勾选导出',
				tooltip: '导出勾选的试剂信息到EXCEL',
				handler: function() {
					me.onGoodsExportExcel(1);
				}
			},{
				iconCls: 'file-excel',
				text: '试剂条件导出',
				tooltip: '导出复合条件的试剂信息到EXCEL',
				handler: function() {
					me.onGoodsExportExcel(2);
				}
			}]
		},'-',{
			fieldLabel:'适用机型',
			itemId:'SuitableType',
			width: 180,
			labelWidth: 60,
			labelAlign: 'right',
			xtype:'trigger',
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:true,
			onTriggerClick:function(){
				me.onSearch();
			},
			listeners:{
	            keyup:{
	                fn:function(field,e){
	                	if(e.getKey() == Ext.EventObject.ESC){
	                		field.setValue('');
	                		me.onSearch();
	                	}else if(e.getKey() == Ext.EventObject.ENTER){
							me.onSearch();
	                	}
	                }
	            }
	        }
		}];
		
		//查询框信息
		me.searchInfo = {
			width:320,isLike:true,itemId: 'Search',
			emptyText:'产品编号/厂商产品编号/中文名/英文名/简称/拼音字头',
			fields:['goods.GoodsNo','goods.ProdGoodsNo','goods.CName','goods.EName','goods.ShortCode','goods.PinYinZiTou']
		};
		items.push('-', {
			type: 'search',
			info: me.searchInfo
		});

		return items;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
			dataIndex: 'Goods_Visible',
			text: '启用',
			width: 50,
			align: 'center',
			type: 'bool',
			isBool: true
		}, {
			dataIndex: 'Goods_BarCodeMgr',
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
			dataIndex: 'Goods_IsRegister',
			text: '有注册证',
			width: 60,
			align: 'center',
			type: 'bool',
			isBool: true
		}, {
			dataIndex: 'Goods_IsPrintBarCode',
			text: '打印条码',
			width: 60,
			align: 'center',
			type: 'bool',
			isBool: true
		}, {
			dataIndex: 'Goods_SuitableType',
			text: '适用机型',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_GoodsNo',
			text: '产品编码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_CName',
			text: '中文名',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_EName',
			text: '英文名',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_PinYinZiTou',
			text: '拼音字头',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_ProdGoodsNo',
			text: '厂商产品编码',
			width: 100,
			defaultRenderer: true
		}, {
			xtype: 'actioncolumn',
			text: '注册证',
			align: 'center',
			width: 50,
			style: 'font-weight:bold;color:white;background:orange;',
			sortable:false,
			hideable: false,
			items: [{
				//iconCls:'button-search hand',
				tooltip:'查看该产品注册证',
				getClass: function(v, meta, record) {
					if(record.get("Goods_IsRegister")){
						return 'button-search hand';
					}else{
						return '';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var record = grid.getStore().getAt(rowIndex);
					me.onShowRegisterGrid(record);
				}
			}]
		}, {
			dataIndex: 'Goods_CompConfirm',
			text: '供应商确认',
			width: 80,
			isBool: true,
			type:'bool',
			align:'center'
		}, {
			dataIndex: 'Goods_CenOrgConfirm',
			text: '实验室确认',
			width: 80,
			isBool: true,
			type:'bool',
			align:'center'
		}, {
			dataIndex: 'Goods_Comp_CName',
			text: '供应商',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_Prod_CName',
			text: '厂商',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_CompGoodsNo',
			text: '供应商产品编码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_ProdGoodsNo',
			text: '厂商产品编码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_UnitName',
			text: '单位',
			width: 60,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_UnitMemo',
			text: '规格',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_GoodsClass',
			text: '一级分类',
			width: 100,
			defaultRenderer: true
		},{
			dataIndex: 'Goods_ShortCode',
			text: '代码',
			width: 100,
			defaultRenderer: true
		}, {
			dataIndex: 'Goods_Price',
			text: '价格',
			width: 100,
			align: 'right',
			xtype:'numbercolumn',
			format:'0.00'
		}, {
			dataIndex: 'Goods_GoodsDesc',
			text: '备注',
			width: 100,
			defaultRenderer: true
		}, {
			text: '最近时间',
			dataIndex: 'Goods_DataUpdateTime',
			width: 130,
			isDate: true,
			hasTime: true
		}, {
			dataIndex: 'Goods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}, {
			dataIndex: 'Goods_Comp_Id',
			text: '供应商ID',
			hidden: true,
			hideable: false
		}, {
			dataIndex: 'Goods_IsRegister',
			text: '是否有注册证',
			isBool: true,
			type:'bool',
			hidden: true,
			hideable: false
		}];

		return columns;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			SuitableType = buttonsToolbar.getComponent('SuitableType'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];

		if(SuitableType && SuitableType.getValue()){
			params.push("goods.SuitableType like '%" + SuitableType.getValue() + "%'");
		}
		if (Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}

		me.internalWhere = params.join(' and ');

		return me.callParent(arguments);
	},
	/**显示表单*/
	showForm: function(id) {
		JShell.Win.open('Shell.class.rea.goods.Form', {
			resizable: false,
			formtype:'show',
			PK:id
		}).show();
	},
	/**实验室试剂确认*/
	onCenOrgConfirm:function(bo){
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		var msg = bo ? "是否进行实验室试剂确认？" : "是否取消实验室试剂确认？"
		var confirm = bo ? 1 : 0;
		
		JShell.Msg.confirm({
			msg:msg
		},function(but) {
			if (but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;

			me.showMask(me.saveText); //显示遮罩层
			for (var i in records) {
				var id = records[i].get(me.PKField);
				me.confirmOneById(i, id, confirm);
			}
		});
	},
	/**确认一条数据*/
	confirmOneById: function(index, id, confirm) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.editUrl);
		
		var params = JShell.JSON.encode({
			entity:{
				Id:id,
				CenOrgConfirm:confirm
			},
			fields:'Id,CenOrgConfirm'
		});
		setTimeout(function() {
			JShell.Server.post(url,params,function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.delCount++;
				} else {
					me.delErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if (me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.delErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**查看注册证*/
	onShowRegisterGrid:function(record){
		var me = this;
		
		JShell.Win.open('Shell.class.rea.goods.register.search.SearchGrid',{
			CenOrgId:record.get('Goods_Comp_Id'),
			ProdGoodsNo:record.get('Goods_ProdGoodsNo')
		}).show();
	},
	/**批量修改条码类型*/
	onChangeBarcodeType:function(){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			isValid = true,
			Ids = [];

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		for(var i=0;i<len;i++){
			var rec = records[i];
			Ids.push(rec.get(me.PKField));
			if(rec.get('Goods_CompConfirm') || rec.get('Goods_CenOrgConfirm')){
				isValid = false;
				break;
			}
		}
		if(!isValid){
			JShell.Msg.error('供应商和实验室双方都取消确认才能进行操作!');
		}else{
			me.onCheckBarcodeType(Ids);
		}
	},
	/**选择处理*/
	onCheckBarcodeType:function(Ids){
		var me = this;
		
		JShell.Win.open('Shell.class.rea.goods.BarcodeTypeCheckForm',{
			Ids:Ids,
			listeners:{
				save:function(p){
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**批量修改信息*/
	onChangeInfo:function(key,value){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length,
			list = [];

		if (len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		for(var i=0;i<len;i++) {
			var rec = records[i];
			var CenOrgConfirm = rec.get('Goods_CenOrgConfirm');
			var CompConfirm = rec.get('Goods_CompConfirm');
			if(!CenOrgConfirm && !CompConfirm) {
				list.push(rec.get(me.PKField));
			}else{
				JShell.Msg.error("供应商和实验室双方都取消确认才能进行该操作!");
				return;
			}
		}
		
		JShell.Msg.confirm({},function(but) {
			if (but != "ok") return;

			me.saveErrorCount = 0;
			me.saveCount = 0;
			me.saveLength = len;

			me.showMask(me.saveText); //显示遮罩层
			for (var i in list) {
				me.updateOne(i, list[i],key,value);
			}
		});
	},
	/**单个修改数据*/
	updateOne:function(index,Id,key,value){
		var me = this,
			url = JShell.System.Path.ROOT + me.editUrl;
			
		var params = {
			entity:{Id:Id},
			fields:'Id'
		};
		params.entity[key] = value;
		params.fields += ',' + key;
		
		setTimeout(function() {
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				if (data.success) {
					me.saveCount++;
				} else {
					me.saveErrorCount++;
				}
				if (me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.saveErrorCount == 0){
						JShell.Msg.alert('批量修改数据成功',null,1000);
						me.onSearch();
					}else{
						JShell.Msg.error('批量修改数据失败，请重新保存！');
					}
				}
			});
		}, 100 * index);
	},
	/**
	 * 试剂导出
	 * @param {Object} type 导出类型：勾选导出(1)，条件导出(2)
	 */
	onGoodsExportExcel: function(type) {
		this.onGoodsExportExcelByForm(type);
	},
	/**表单方式提交*/
	onGoodsExportExcelByForm:function(type){
		var me = this;
			
		me.UpdateForm = me.UpdateForm || Ext.create('Ext.form.Panel',{
			items:[
				{xtype:'filefield',name:'file'},
				{xtype:'textfield',name:'reportType',value:"1"},
				{xtype:'textfield',name:'idList'},
				{xtype:'textfield',name:'where'},
				{xtype:'textfield',name:'isHeader',value:"0"}
			]
		});
		//清空数据
		me.UpdateForm.getForm().setValues({idList:'',where:''});
		
		if(type == 1){//类型为勾选导出
			var records = me.getSelectionModel().getSelection(),
				len = records.length,
				ids = [];
				
			if (len == 0) {
				JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
				return;
			}
			
			for(var i=0;i<len;i++){
				ids.push(records[i].get(me.PKField));
				me.UpdateForm.getForm().setValues({idList:ids.join(",")});
			}
		}else if(type == 2){//类型为条件导出
			var gridUrl = me.getLoadUrl(),
				arr = gridUrl.split('&where='),
				where = '';
				
			if(arr.length == 2){
				where = JShell.String.decode(arr[1]);
			}
			
			me.UpdateForm.getForm().setValues({where:where});
		}
		
		me.showMask("数据请求中...");
		var url = JShell.System.Path.ROOT + '/ReagentService.svc/RS_UDTO_GetReportDetailExcelPath';
		me.UpdateForm.getForm().submit({
			url:url,
            //waitMsg:JShell.Server.SAVE_TEXT,
            success:function (form,action) {
            	me.hideMask();
        		var fileName = action.result.ResultDataValue;
        		var downloadUrl = JShell.System.Path.ROOT + '/ReagentService.svc/RS_UDTO_DownLoadExcel';
				downloadUrl += '?isUpLoadFile=1&operateType=0&downFileName=平台产品数据&fileName=' + fileName.split('\/')[2];
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