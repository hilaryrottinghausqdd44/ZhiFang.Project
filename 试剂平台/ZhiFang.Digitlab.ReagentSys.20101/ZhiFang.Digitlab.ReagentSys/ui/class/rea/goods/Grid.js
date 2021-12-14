/**
 * 产品列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.goods.Grid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '产品列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchGoodsByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelGoods',
	/**修改服务地址*/
    editUrl:'/ReagentSysService.svc/ST_UDTO_UpdateGoodsByField',

	/**是否启用刷新按钮*/
	hasRefresh: true,
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用查询框*/
	hasSearch: true,

	/**默认加载数据*/
	defaultLoad: true,
	/**排序字段*/
	//defaultOrderBy:[{property:'Goods_DispOrder',direction:'ASC'}],

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
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

		//自定义按钮功能栏
		me.buttonToolbarItems = me.createButtonToolbarItems();

		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;

		var columns = [{
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
		}, {
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
			dataIndex: 'Goods_CenOrg_CName',
			text: '机构',
			width: 100,
			defaultRenderer: true
		}, {
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
			dataIndex: 'Goods_Visible',
			text: '启用',
			width: 50,
			align: 'center',
			type: 'bool',
			isBool: true
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
			dataIndex: 'Goods_RegistNo',
			text: '注册证',
			width: 100,
			defaultRenderer: true
		}, {
			text: '注册日期',
			dataIndex: 'Goods_RegistDate',
			width: 90,
			isDate: true
		},{
			dataIndex: 'Goods_RegistNoInvalidDate',
			text: '注册证到期日期',
			width: 110,
			isDate: true
		}, {
			text: '最近时间',
			dataIndex: 'Goods_DataUpdateTime',
			width: 130,
			isDate: true,
			hasTime: true
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
		},{
			dataIndex: 'Goods_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-', 'add', 'edit', 'del', '-', {
			xtype: 'button',
			iconCls: 'file-excel',
			text: '试剂导入导出',
			tooltip: 'EXCEL导入导出试剂',
			menu: [{
				iconCls: 'file-excel',
				text: '实验室试剂导入',
				tooltip: 'EXCEL导入实验室试剂',
				handler: function() {
					me.onGoodsImportExcel('Lab');
				}
			}, {
				iconCls: 'file-excel',
				text: '供应商试剂导入',
				tooltip: 'EXCEL导入供应商试剂',
				handler: function() {
					me.onGoodsImportExcel('Comp');
				}
			}, {
				iconCls: 'file-excel',
				text: '厂商试剂导入',
				tooltip: 'EXCEL导入厂商试剂',
				handler: function() {
					me.onGoodsImportExcel('Prod');
				}
			},{
				iconCls: 'button-exp',
				text: '试剂模板',
				tooltip: '试剂模板下载',
				handler: function() {
					me.onGoodsExp();
				}
			},{
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
		}, '-',{
			xtype:'splitbutton',
            textAlign:'left',
			iconCls:'button-edit',
			text:'批量修改',
			handler:function(btn,e){btn.overMenuTrigger = true;btn.onClick(e);},
			menu:[{
				text:'修改实验室',iconCls:'button-edit',
				listeners:{click:function(but) {me.onOrgChange(1);}}
			},{
				text:'修改供应商',iconCls:'button-edit',
				listeners:{click:function(but) {me.onOrgChange(2);}}
			},{
				text:'修改厂商',iconCls:'button-edit',
				listeners:{click:function(but) {me.onOrgChange(3);}}
			}]
		}];

		items = items.concat(me.createCheckCom());

		//查询框信息
		me.searchInfo = {
			width: 180,
			isLike: true,
			itemId: 'Search',
			emptyText: '中文名/英文名/厂商产品编码',
			fields: ['goods.CName', 'goods.EName', 'goods.ProdGoodsNo']
		};
		items.push('-', '->', {
			type: 'search',
			info: me.searchInfo
		});

		return items;
	},
	/**实验室、供应商、厂商查询*/
	createCheckCom: function() {
		var me = this;
		var items = [{
			fieldLabel: '机构',
			itemId: 'LabName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.cenorg.CheckGrid',
			width: 170,
			labelWidth: 40,
			labelAlign: 'right'
		}, {
			fieldLabel: '厂商主键ID',
			itemId: 'LabID',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '供应商',
			itemId: 'CompName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.cenorg.CheckGrid',
			classConfig: {
				defaultWhere: "cenorg.CenOrgType.ShortCode='1' or cenorg.CenOrgType.ShortCode='2'",
				title: '供应商选择'
			},
			width: 180,
			labelWidth: 50,
			labelAlign: 'right'
		}, {
			fieldLabel: '供应商主键ID',
			itemId: 'CompID',
			xtype: 'textfield',
			hidden: true
		}, {
			fieldLabel: '厂商',
			itemId: 'ProdName',
			xtype: 'uxCheckTrigger',
			className: 'Shell.class.rea.cenorg.CheckGrid',
			classConfig: {
				defaultWhere: "cenorg.CenOrgType.ShortCode='1'",
				title: '厂商选择'
			},
			width: 170,
			labelWidth: 40,
			labelAlign: 'right'
		}, {
			fieldLabel: '厂商主键ID',
			itemId: 'ProdID',
			xtype: 'textfield',
			hidden: true
		}];

		return items;
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
	onGoodsImportExcel: function(formType) {
		var me = this;
		JShell.Win.open('Shell.class.rea.goods.UploadPanel', {
			formtype:'add',
			resizable: false,
			formType: formType,
			listeners: {
				save: function(p, records) {
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			LabName = buttonsToolbar.getComponent('LabName'),
			LabId = buttonsToolbar.getComponent('LabID'),
			CompName = buttonsToolbar.getComponent('CompName'),
			CompId = buttonsToolbar.getComponent('CompID'),
			ProdName = buttonsToolbar.getComponent('ProdName'),
			ProdId = buttonsToolbar.getComponent('ProdID');

		LabName && LabName.on({
			check: function(p, record) {
				LabName.setValue(record ? record.get('CenOrg_CName') : '');
				LabId.setValue(record ? record.get('CenOrg_Id') : '');
				p.close();
			}
		});
		CompName && CompName.on({
			check: function(p, record) {
				CompName.setValue(record ? record.get('CenOrg_CName') : '');
				CompId.setValue(record ? record.get('CenOrg_Id') : '');
				p.close();
			}
		});
		ProdName && ProdName.on({
			check: function(p, record) {
				ProdName.setValue(record ? record.get('CenOrg_CName') : '');
				ProdId.setValue(record ? record.get('CenOrg_Id') : '');
				p.close();
			}
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			LabID = buttonsToolbar.getComponent('LabID'),
			CompID = buttonsToolbar.getComponent('CompID'),
			ProdID = buttonsToolbar.getComponent('ProdID'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];

		if (LabID && LabID.getValue()) {
			params.push('goods.CenOrg.Id=' + LabID.getValue());
		}
		if (CompID && CompID.getValue()) {
			params.push('goods.Comp.Id=' + CompID.getValue());
		}
		if (ProdID && ProdID.getValue()) {
			params.push('goods.Prod.Id=' + ProdID.getValue());
		}
		if (Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}

		if (params.length > 0) {
			me.internalWhere = params.join(' and ');
		}

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

		JShell.Win.open('Shell.class.rea.goods.Form', config).show();
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
		var url = JShell.System.Path.UI + '/models/rea/goods/' + JShell.REA.Goods.EXCEL + '?v=' + new Date().getTime();
		window.open(url);
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