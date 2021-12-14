/**
 * 机构货品维护
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.goods.Grid', {
	extend: 'Shell.class.rea.client.goods.basic.Grid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button',
		'Shell.ux.form.field.SimpleComboBox',
	    'Shell.ux.form.field.BoolComboBox'
	],
	    /**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsByField',
    
		/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		Ext.override(Ext.ToolTip, {
			maxWidth: 350
		});
		//初始化检索监听
		me.initFilterListeners();

		me.on({
			itemdblclick: function(view, record) {
				me.showForm(record.get(me.PKField));
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.CENORG_ID = JShell.REA.System.CENORG_ID;
		me.CENORG_NAME = JShell.REA.System.CENORG_NAME;
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
		//默认条件
//		me.defaultWhere = "reagoods.ReaCenOrg.Id=" + me.CENORG_ID;
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-', 'add', 'edit', 'del', 'save','-',{
			xtype:'splitbutton',
            textAlign: 'left',
			iconCls:'button-edit',
			text:'批量修改',
			handler:function(btn,e){btn.overMenuTrigger = true;btn.onClick(e);},
			menu:[{
				text:'条码类型',iconCls: 'button-edit',tooltip: '勾选试剂批量修改条码类型',
				listeners:{click:function(but) {me.onChangeBarcodeType();}}
			},
			{
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
		},'-',{
			xtype:'splitbutton',
            textAlign: 'left',
			iconCls:'file-excel',
			text:'导入导出',
			handler:function(btn,e){btn.overMenuTrigger = true;btn.onClick(e);},
			menu:[{
				text:'货品导入',iconCls: 'file-excel',tooltip: 'EXCEL导入试剂',
				listeners:{click:function(but) {me.onGoodsImportExcel('Comp');}}
			},{
				text:'模板下载',iconCls: 'button-exp',tooltip: '货品模板下载',
				listeners:{click:function(but) {me.onGoodsExp();}}
			},{
				text:'勾选导出',iconCls: 'file-excel',tooltip: '导出勾选的货品信息到EXCEL',
				listeners:{click:function(but) {me.onGoodsExportExcel(1);}}
			},{
				text:'条件导出',iconCls: 'file-excel',tooltip: '导出复合条件的货品信息到EXCEL',
				listeners:{click:function(but) {me.onGoodsExportExcel(2);}}
			}]
		}];

//		items = items.concat(me.createCheckCom());

		//查询框信息
		me.searchInfo = {
			width:200,isLike:true,itemId: 'Search',
			emptyText:'货品编号/中文名/英文名/简称',
			fields:['reagoods.GoodsNo','reagoods.CName','reagoods.EName','reagoods.ShortCode']
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
		var columns = [  {
			dataIndex: 'ReaGoods_BarCodeMgr',
			text: '条码类型',
			width: 60,
			editor:{
				xtype:'uxSimpleComboBox',value:'0',hasStyle:true,
				data:[
					['0','批条码','color:green;'],
					['1','盒条码','color:orange;'],
					['2','无条码','color:black;']
				]
			},
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
		}, {
			dataIndex: 'ReaGoods_IsRegister',
			text: '有注册证',
			width: 60,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_IsPrintBarCode',
			text: '打印条码',
			width: 60,
			align: 'center',
			type: 'bool',
			isBool: true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true},
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_CName',
			text: '名称',
			width: 100,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_ReaCenOrg_CName',
			text: '机构',
			width: 100,
			defaultRenderer: true
//			editor:{}
		}, {
			dataIndex: 'ReaGoods_GoodsNo',
			text: '货品编号',
			width: 100,
			editor:{},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitName',
			text: '单位',
			width: 100,
			editor:{},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_UnitMemo',
			text: '规格',
			width: 100,
			editor:{},
			defaultRenderer: true
		},{
			xtype: 'actioncolumn',
			text: '注册证',
			align: 'center',
			width: 55,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var GoodsNo = record.get('ReaGoods_GoodsNo') + '';
					//有货品编号的可以维护注册证
					if(GoodsNo){
					    meta.tdAttr = 'data-qtip="<b>注册证编辑</b>"';
						return 'button-edit hand';
					}else{
						return 'button-actionedit hand';
					}
				},
				handler: function(grid, rowIndex, colIndex) {
					var rec = grid.getStore().getAt(rowIndex);
					var GoodsNo = rec.get('ReaGoods_GoodsNo') + '';
					//有货品编号的可以维护注册证
					if(GoodsNo){
						JcallShell.Msg.overwrite('维护注册证');
//						 me.openEditForm();
					}else{
						JcallShell.Msg.error('请先维护注册证信息');
					}
				}
			}]
		},{
			dataIndex: 'ReaGoods_Visible',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true}
		},{
			dataIndex: 'ReaGoods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		},  {
			dataIndex: 'ReaGoods_ProdEara',
			text: '产地',
			width: 100,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_Price',
			text: '参考价',
			width: 100,
			editor:{xtype:'numberfield'},
			defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_StorageType',
			text: '储藏条件',hidden:false,
			width: 100,
			editor:{},
			renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta);
				return v;
			}
		}, {
			dataIndex: 'ReaGoods_GoodsClass',
			text: '一级分类',hidden:false,
			width: 100,
			editor:{},
			defaultRenderer: true
		}, {
			text: '二级分类',
			dataIndex: 'ReaGoods_GoodsClassType',
			width: 100,hidden:false,
			editor:{},
			defaultRenderer: true
		}];
		return columns;
	},
	/**导入试剂信息*/
	onGoodsImportExcel: function(formType) {
		var me = this;
		JShell.Win.open('Shell.class.rea.client.goods.UploadPanel', {
			formtype:'add',
			resizable: false,
			formType: formType,
    		CenOrg:{Id:me.CENORG_ID,Name:me.CENORG_NAME,readOnly:true},//机构信息
    		Comp:{Id:me.CENORG_ID,Name:me.CENORG_NAME,readOnly:false},//上级供应商信息
    		Prod:{Id:me.CENORG_ID,Name:me.CENORG_NAME,readOnly:false},//厂商信息
			listeners: {
				save: function(p, records) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},

	/**显示表单*/
	showForm: function(id) {
		var me = this,
			config = {
				resizable: false,
				listeners: {
					save: function(p, records) {
						p.close();
						me.onSearch();
					},
					beforerender: function(p,  eOpts ){
//						var edit = me.getPlugin('NewsGridEditing'); 
////		                edit.completeEdit();
//		                edit.cancelEdit();
					},
					load:function(){
						var edit = me.getPlugin('NewsGridEditing'); 
//		                edit.completeEdit();
		                edit.cancelEdit();
					}
				}
			};

		if (id) {
			config.formtype = 'edit';
			config.PK = id;
		} else {
			config.formtype = 'add';
		}

		JShell.Win.open('Shell.class.rea.client.goods.Form', config).show();
	},
	/**@overwrite 返回数据处理方法*/
	changeResult: function(data) {
		if(!data || !data.list) return data;
		var list = data.list;
		for(var i = 0; i < list.length; i++) {
			list[i].Goods_IsBarCodeMgr = list[i].Goods_BarCodeMgr == '1' ? true : false;
			list[i].Goods_IsRegister = list[i].Goods_IsRegister == '1' ? true : false;
			list[i].Goods_IsPrintBarCode = list[i].Goods_IsPrintBarCode == '1' ? true : false;
		}
		data.list = list;
		return data;
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
			if(rec.get('ReaGoods_CompConfirm') || rec.get('ReaGoods_CenOrgConfirm')){
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
			 /**修改服务地址*/
            editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsByField',
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
			var CenOrgConfirm = rec.get('ReaGoods_CenOrgConfirm');
			var CompConfirm = rec.get('ReaGoods_CompConfirm');
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
    /**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			ProdName = buttonsToolbar.getComponent('ProdName'),
			ProdId = buttonsToolbar.getComponent('ProdID');
			
		ProdName && ProdName.on({
			check: function(p, record) {
				ProdName.setValue(record ? record.get('ReaCenOrg_CName') : '');
				ProdId.setValue(record ? record.get('ReaCenOrg_Id') : '');
				p.close();
			}
		});
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick: function() {
		//this.fireEvent('addclick');

		this.showForm();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		//me.fireEvent('editclick',me,records[0].get(me.PKField));

		me.showForm(records[0].get(me.PKField));
	},
	
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			LabID = buttonsToolbar.getComponent('LabID'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
		if (Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}

		me.internalWhere = params.join(' and ');

		return me.callParent(arguments);
	},
	/**显示表单*/
	showForm: function(id) {
		var me = this,
			config = {
				resizable: false,
				listeners: {
					save: function(p, records) {
						p.close();
						me.onSearch();
					}
				}
			};

		if (id) {
			config.formtype = 'edit';
			config.PK = id;
		} else {
			config.formtype = 'add';
		}

		JShell.Win.open('Shell.class.rea.client.goods.Form', config).show();
	},
	
	/**机构修改*/
	onOrgChange:function(type){
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		
		var orgField = '';
		var orgTypeName = '';
		switch(type){
			case 1 : 
				orgField = 'CenOrg';
				orgTypeName = '实验室';
				break;
			case 2 : 
				orgField = 'Comp';
				orgTypeName = '供应商';
				break;
			case 3 : 
				orgField = 'Prod';
				orgTypeName = '厂商';
				break;
		}
		if(!orgField) return;
		
		JShell.Win.open('Shell.class.rea.cenorg.CheckGrid',{
			resizable: false,
			hasClearButton:false,//是否带清除按钮
			listeners: {
				accept:function(p,record){
					var msg = '您确定将【' + orgTypeName + '】批量改为【' + record.get('CenOrg_CName') + '】吗？';
					
					JShell.Msg.confirm({
						msg:msg
					},function(but) {
						if (but != "ok") return;
						var orgId = record.get(p.PKField);
						p.close();
						me.updateOrg(orgField,records,orgId);
					});
				}
			}
		}).show();
	},
	/**批量修改机构*/
	updateOrg:function(orgField,records,orgId){
		var me = this,
			len = records.length;
			
		me.delErrorCount = 0;
		me.delCount = 0;
		me.delLength = records.length;

		me.showMask(me.saveText); //显示遮罩层
		for(var i=0;i<len;i++){
			var id = records[i].get(me.PKField);
			me.updateOneOrg(i,id,orgField,orgId);
		}
	},
	/**修改一个机构*/
	updateOneOrg:function(index,id,orgField,orgId){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.editUrl);
			
		var entity = {Id:id};
		entity[orgField] = {Id:orgId};
		
		var params = JShell.JSON.encode({
			entity:entity,
			fields:'Id,'+ orgField + '_Id'
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
					if (me.delErrorCount == 0){
						me.onSearch();
					}else{
						JShell.Msg.error(me.delErrorCount + '条数据发生错误!');
					}
				}
			});
		}, 100 * index);
	},
	/**试剂模板下载*/
	onGoodsExp:function(){
		JcallShell.Msg.overwrite('试剂模板下载');
		return;
		var url = JShell.System.Path.UI + '/models/rea/goods/' + JShell.REA.Goods.EXCEL + '?v=' + new Date().getTime();
		window.open(url);
	},
	/**
	 * 试剂导出
	 * @param {Object} type 导出类型：勾选导出(1)，条件导出(2)
	 */
	onGoodsExportExcel: function(type) {
		if(type=='1'){
			JcallShell.Msg.overwrite('勾选导出');
		}else{
			JcallShell.Msg.overwrite('条件导出');

		}
		return;
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
				downloadUrl += '?isUpLoadFile=1&operateType=0&downFileName=平台货品数据&fileName=' + fileName.split('\/')[2];
				downloadUrl = encodeURI(downloadUrl);
				window.open(downloadUrl);
            },
            failure:function(form,action){
            	me.hideMask();
				JShell.Msg.error(action.result.ErrorInfo);
			}
		});
	},
	/**保存*/
	onSaveClick:function(){
		var me = this,
			records = me.store.data.items;
			
		var isError = false;
		var changedRecords = me.store.getModifiedRecords(),//获取修改过的行记录
			len = changedRecords.length;
			
		if(len == 0){
			JShell.Msg.alert("没有变更，不需要保存！");
			return;
		}
		
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			me.updateOne(i,changedRecords[i]);
		}
	},/**修改信息*/
	updateOne:function(i,record){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var params = Ext.JSON.encode({
			entity:{
				Id:record.get('ReaGoods_Id'),
				BarCodeMgr:record.get('ReaGoods_BarCodeMgr'),
				IsRegister:record.get('ReaGoods_IsRegister')? 1 : 0,
				IsPrintBarCode:record.get('ReaGoods_IsPrintBarCode')? 1 : 0,
				CName:record.get('ReaGoods_CName'),
				GoodsNo:record.get('ReaGoods_GoodsNo'),
				UnitName:record.get('ReaGoods_UnitName'),
				UnitMemo:record.get('ReaGoods_UnitMemo'),
				Visible:record.get('ReaGoods_Visible')? 1 : 0,
				ProdEara:record.get('ReaGoods_ProdEara'),
				Price:record.get('ReaGoods_Price'),
				StorageType:record.get('ReaGoods_StorageType'),
				GoodsClass:record.get('ReaGoods_GoodsClass'),
				GoodsClassType:record.get('ReaGoods_GoodsClassType')
			},
			fields:'Id,BarCodeMgr,IsRegister,IsPrintBarCode,CName,GoodsNo,UnitName,UnitMemo,Visible,ProdEara,Price,StorageType,GoodsClass,GoodsClassType'
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
	showMemoText:function(value, meta){
		var me=this	;
        var val=value.replace(/(^\s*)|(\s*$)/g, ""); 	
		val = val.replace(/\\r\\n/g, "<br />");
        val = val.replace(/\\n/g, "<br />");
		var v = "" + value;
		var index1=v.indexOf("</br>");
		if(index1>0)v=v.substring(0,index1);
		if(v.length > 0)v = (v.length > 20 ? v.substring(0, 20) : v);
		if(value.length>20){
			v= v+"...";
		}
        var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
        meta.tdAttr = 'data-qtip="' + qtipValue + '"';
        return v
	}
});