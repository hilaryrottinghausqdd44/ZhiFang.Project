/**
 * 下级机构产品列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.goods.ChildrenGrid', {
	extend: 'Shell.ux.grid.Panel',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '下级机构产品列表',
	width: 800,
	height: 500,

	/**获取数据服务路径*/
	selectUrl: '/ReagentSysService.svc/ST_UDTO_SearchGoodsByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ReagentSysService.svc/ST_UDTO_DelGoods',
	/**拷贝数据服务路径*/
	copyUrl: '/ReagentService.svc/RS_UDTO_CopyGoodsByID',
	/**修改服务地址*/
	editUrl: '/ReagentSysService.svc/ST_UDTO_UpdateGoodsByField',

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
	defaultLoad: false,
	/**排序字段*/
	//defaultOrderBy:[{property:'Goods_DispOrder',direction:'ASC'}],

	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,

	/**机构信息*/
	Lab: {
		Id: '',
		Name: '',
		readOnly: false
	},

	/**机构ID*/
	CenOrgId: null,
	/**供应商ID*/
	CompId: null,

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//初始化检索监听
		me.initFilterListeners();

		me.on({
			itemdblclick: function(view, record) {
				me.editInfo(record);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.CENORG_ID = JShell.REA.System.CENORG_ID;
		me.CENORG_NAME = JShell.REA.System.CENORG_NAME;
		me.addEvents('toMaxClick', 'toMinClick');
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
			renderer: function(value, meta) {
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
			sortable: false,
			hideable: false,
			items: [{
				//iconCls:'button-search hand',
				tooltip: '查看该产品注册证',
				getClass: function(v, meta, record) {
					if(record.get("Goods_IsRegister")) {
						return 'button-search hand';
					} else {
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
			type: 'bool',
			align: 'center'
		}, {
			dataIndex: 'Goods_CenOrgConfirm',
			text: '实验室确认',
			width: 80,
			isBool: true,
			type: 'bool',
			align: 'center'
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
		}, {
			dataIndex: 'Goods_ShortCode',
			text: '代码',
			width: 100,
			defaultRenderer: true
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
		}];

		return columns;
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = [{
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
		}, '-', 'refresh', '-', 'add',{
			xtype: 'button',
			iconCls: 'button-add',
			text: '选择新增',
			tooltip: '选择本机构的试剂给下级机构',
			handler: function() {
				me.onCheckAdd();
			}
		}, 'edit', 'del', '-', {
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
			text:'操作',
			handler:function(btn,e){btn.overMenuTrigger = true;btn.onClick(e);},
			menu:[{
				text:'供应商确认',iconCls: 'button-accept',tooltip: '对供应给实验室的试剂进行确认',
				listeners:{click:function(but) {me.onCompConfirm(true);}}
			},{
				text:'取消确认',iconCls: 'button-cancel',tooltip: '取消对供应给实验室的试剂的确认',
				listeners:{click:function(but) {me.onCompConfirm(false);}}
			}]
		}, '-'];

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

		if(records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}

		//		if(records[0].get('Goods_CompConfirm') || records[0].get('Goods_CenOrgConfirm')){
		//			JShell.Msg.error('供应商和实验室双方都取消确认才能进行编辑!');
		//			return;
		//		}

		//me.fireEvent('editclick',me,records[0].get(me.PKField));

		//me.showForm(records[0].get(me.PKField));
		me.editInfo(records[0]);
	},
	onGoodsImportExcel: function(formType) {
		var me = this;
		JShell.Win.open('Shell.class.rea.goods.UploadPanel', {
			formtype: 'add',
			resizable: false,
			formType: formType,
			CenOrg: {
				Id: me.Lab.Id,
				Name: me.me.Lab.Name,
				readOnly: true
			}, //机构信息
			Comp: {
				Id: me.CENORG_ID,
				Name: me.CENORG_NAME,
				readOnly: ture
			}, //上级供应商信息
			Prod: {
				Id: me.CENORG_ID,
				Name: me.CENORG_NAME,
				readOnly: false
			}, //厂商信息
			listeners: {
				save: function(p, records) {
					p.close();
				}
			}
		}).show();
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			ProdName = buttonsToolbar.getComponent('ProdName'),
			ProdId = buttonsToolbar.getComponent('ProdID');

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
			ProdID = buttonsToolbar.getComponent('ProdID'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];

		if(ProdID && ProdID.getValue()) {
			params.push('goods.Prod.Id=' + ProdID.getValue());
		}
		if(Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}

		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		}

		return me.callParent(arguments);
	},

	/**修改信息*/
	editInfo: function(record) {
		var me = this;
		if(record.get('Goods_CompConfirm') || record.get('Goods_CenOrgConfirm')) {
			//JShell.Msg.error('供应商和实验室双方都取消确认才能进行编辑!');
			//查看
			JShell.Win.open('Shell.class.rea.goods.Form', {
				title:'试剂信息',
				PK:record.get(me.PKField)
			}).show();
		}else{
			me.showForm(record.get(me.PKField));
		}
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

		if(id) {
			config.formtype = 'edit';
			config.PK = id;
			config.CenOrg = {
				Id: '',
				Name: '',
				readOnly: true
			}; //机构信息
			config.Comp = {
				Id: '',
				Name: '',
				readOnly: true
			}; //上级供应商信息
		} else {
			config.formtype = 'add';
			config.CenOrg = {
				Id: me.Lab.Id,
				Name: me.Lab.Name,
				readOnly: true
			}; //机构信息
			config.Comp = {
				Id: me.CENORG_ID,
				Name: me.CENORG_NAME,
				readOnly: true
			}; //上级供应商信息
			config.Prod = {
				Id: me.CENORG_ID,
				Name: me.CENORG_NAME,
				readOnly: false
			}; //厂商信息
		}

		JShell.Win.open('Shell.class.rea.goods.Form', config).show();
	},
	/**选择新增*/
	onCheckAdd: function() {
		var me = this;
		JShell.Win.open('Shell.class.rea.goods.CheckCompGrid', {
			resizable: true,
			labCenOrgID: me.CenOrgId, //实验室ID
			compCenOrgID: me.CompId, //供应商ID
			listeners: {
				accept: function(p, records) {
					me.saveGoodsInfos(records || []);
					p.close();
				}
			}
		}).show();
	},
	/**保存试剂信息*/
	saveGoodsInfos: function(records) {
		var me = this,
			len = records.length,
			ids = [];

		if(len == 0) return;

		for(var i = 0; i < len; i++) {
			ids.push(records[i].get(me.PKField));
		}

		var url = JShell.System.Path.getRootUrl(me.copyUrl);
		var params = JShell.JSON.encode({
			listID: ids.join(","),
			compId: me.CompId,
			cenOrgId: me.CenOrgId
		});
		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.post(url, params, function(data) {
			me.hideMask(); //隐藏遮罩层
			if(data.success) {
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT, null, 1000);
				me.onSearch();
			} else {
				JShell.Msg.error(data.msg);
			}
		});
	},
	/**供应商试剂确认*/
	onCompConfirm: function(bo) {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if(records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}

		var msg = bo ? "是否进行供应商试剂确认？" : "是否取消供应商试剂确认？"
		var confirm = bo ? 1 : 0;

		JShell.Msg.confirm({
			msg: msg
		}, function(but) {
			if(but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;

			me.showMask(me.saveText); //显示遮罩层
			for(var i in records) {
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
			entity: {
				Id: id,
				CompConfirm: confirm
			},
			fields: 'Id,CompConfirm'
		});
		setTimeout(function() {
			JShell.Server.post(url, params, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.delCount++;
				} else {
					me.delErrorCount++;
					if(record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if(me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.delErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**查看注册证*/
	onShowRegisterGrid: function(record) {
		var me = this;

		JShell.Win.open('Shell.class.rea.goods.register.search.SearchGrid', {
			CenOrgId: record.get('Goods_Comp_Id'),
			ProdGoodsNo: record.get('Goods_ProdGoodsNo')
		}).show();
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
	}
});