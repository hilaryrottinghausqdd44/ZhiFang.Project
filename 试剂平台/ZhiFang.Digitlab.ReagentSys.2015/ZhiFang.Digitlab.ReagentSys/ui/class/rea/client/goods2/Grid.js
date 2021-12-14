/**
 * 机构产品维护
 * @author liangyl
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.goods2.Grid', {
	extend: 'Shell.class.rea.client.goods2.basic.Grid',
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
	
	isRowEdit:true,
	editText:'按行编辑',
    isCycle: false,
    isRowCycle: false,
    isColCycle: false,
    rowIdx: null,
    colIdx: null,
    isSpecialkey: false,
    specialkeyArr: [{ key: Ext.EventObject.ENTER,replaceKey: Ext.EventObject.TAB
	    }, {
	        key: Ext.EventObject.LEFT, type: 'left',ctrlKey: true
	    },{
	        key: Ext.EventObject.RIGHT, type: 'right', ctrlKey: true
    }],
    
      //按相同码 ，产品  排序 
    /**默认排序字段*/
	defaultOrderBy: [{property: 'ReaGoods_CName',direction: 'ASC'}],
	/**是否启用序号列*/
	hasRownumberer: true,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		Ext.override(Ext.ToolTip, {
			maxWidth: 350
		});
		//初始化检索监听
		me.initFilterListeners();
		me.createCellEditListeners();
		var buttonsToolbar = me.getComponent("buttonsToolbar");
		if(buttonsToolbar){
			var btnMinUnit=buttonsToolbar.getComponent("btnMinUnit");
		}
        me.on({
        	select : function (com,record,index,eOpts ){
        		var GoodsNo=record.get('ReaGoods_GoodsNo');
        		if(!GoodsNo){
        			btnMinUnit.setDisabled(true);
        		}else{
        			btnMinUnit.setDisabled(false);
        		}
        	},
        	itemclick : function (com,record,item,index,e,eOpts ){
        		var GoodsNo=record.get('ReaGoods_GoodsNo');
        		if(!GoodsNo){
        			btnMinUnit.setDisabled(true);
        		}else{
        			btnMinUnit.setDisabled(false);
        		}
        	},
        	itemdblclick: function(view, record) {
				me.showForm(record.get(me.PKField));
			},
            validateedit: function(editor, e) {
                var bo = false;
               	var edit = me.getPlugin('NewsGridEditing'); 

                bo = me.fireEvent('cellAvailable', editor, e) === false ? false: true;

                if(me.rowIdx==null && me.colIdx==null){
                	me.colIdx=e.colIdx;
                	me.rowIdx=e.rowIdx;
                }
             	if (me.rowIdx != null && me.colIdx != null) {
                    if (me.isSpecialkey) {
                        edit.startEditByPosition({
                            row: me.rowIdx,
                            column: me.colIdx
                        })
                    }
                    me.rowIdx = null;
                    me.colIdx = null;
                    me.isSpecialkey = false
                }
                return bo
            }
        });
	},
	initComponent: function() {
		var me = this;
		if (me.isCycle) {
            me.isRowCycle = true;
            me.isColCycle = true
        }
		me.CENORG_ID = JShell.REA.System.CENORG_ID;
		me.CENORG_NAME = JShell.REA.System.CENORG_NAME;
		//默认条件
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-', 'add', 'edit', 'del', 'save','-',{
			text:'设置最小单位',tooltip:'设置最小单位',iconCls:'button-config',
			itemId:'btnMinUnit',
			handler:function(){
				var	records = me.getSelectionModel().getSelection(),
					len = records.length;
		
		        if (len == 0) {
		            JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
		            return;
		        }
		        var GoodsNo=records[0].get('ReaGoods_GoodsNo');
		        if(!GoodsNo){
		        	JShell.Msg.error('没有设置产品编号,不能设置最小单位');
		        	return;
		        }
				me.showConfigForm(GoodsNo);
			}
		},'-',{
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
				text:'产品导入',iconCls: 'file-excel',tooltip: 'EXCEL导入试剂',
				listeners:{click:function(but) {me.onGoodsImportExcel('Comp');}}
			},{
				text:'模板下载',iconCls: 'button-exp',tooltip: '产品模板下载',
				listeners:{click:function(but) {me.onGoodsExp();}}
			},{
				text:'勾选导出',iconCls: 'file-excel',tooltip: '导出勾选的产品信息到EXCEL',
				listeners:{click:function(but) {me.onGoodsExportExcel(1);}}
			},{
				text:'条件导出',iconCls: 'file-excel',tooltip: '导出复合条件的产品信息到EXCEL',
				listeners:{click:function(but) {me.onGoodsExportExcel(2);}}
			}]
		},'-',{
            xtype: 'radiogroup',
	        fieldLabel: '',width:180,
	        columns: 2,
	        vertical: true,
	        items: [
	            { boxLabel: '行编辑模式', name: 'rb', inputValue: '1' , checked: true},
	            { boxLabel: '列编辑模式', name: 'rb', inputValue: '2'}
	        ],
	        listeners:{
	        	change:function(com,newValue,oldValue,eOpts ){
	        		var val=newValue.rb;
	        		if(val=='1'){
	        			me.specialkeyArr=[ {
					        key: Ext.EventObject.ENTER,
	                        replaceKey: Ext.EventObject.TAB
					    }];
	        		}else{
	        			me.specialkeyArr=[{
					        key: Ext.EventObject.ENTER,
					        type: 'down'
					    }];
	        		}
	        	}
	        }
	    }];
		//查询框信息
		me.searchInfo = {
			width:200,isLike:true,itemId: 'Search',
			emptyText:'产品编号/中文名/英文名/简称',
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
		var columns = [{
			dataIndex: 'ReaGoods_GoodsSort',text: '产品序号',hidden:true,
			width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_GoodsNo',text: '产品编号',
			width: 100,editor:{xtype:'textfield'},defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_CName',text: '产品名称',width: 100,
			editor:{xtype:'textfield'},defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_SName',text: '简称',width: 100,
			editor:{xtype:'textfield'},defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_ProdEara',text: '产地',
			width: 100,editor:{xtype:'textfield'},defaultRenderer: true
		},  {
			dataIndex: 'ReaGoods_UnitMemo',text: '规格',
			width: 100,editor:{xtype:'textfield'},defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_UnitName',text: '包装单位',
			width: 100,editor:{xtype:'textfield'},defaultRenderer: true
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
		}, {
			text: '测试数',
			dataIndex: 'ReaGoods_TestCount',
			width: 100,hidden:false,
			editor:{xtype:'numberfield'},
			defaultRenderer: true
		}, {
			text: '适用机型',
			dataIndex: 'ReaGoods_SuitableType',
			width: 100,hidden:false,
			editor:{},
			defaultRenderer: true
		},{
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
			dataIndex: 'ReaGoods_IsPrintBarCode',text: '是否打印条码',
			width: 80,align: 'center',type: 'bool',isBool: true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true},
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_IsRegister',text: '有无注册证',width: 80,
			align: 'center',type: 'bool',isBool: true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true},defaultRenderer: true
		}, {
			dataIndex: 'ReaGoods_GonvertSort',text: '单位序号',
			width: 100,	editor:{xtype:'numberfield',minValue:0,maxValue :5},
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_GonvertQty',text: '换算系数',
			width: 100,editor:{xtype:'numberfield'},defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_LinkGroupCode',text: '相识产品码',hidden:true,
			width: 100,defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_Visible',text: '是否启用',width: 65,
			align:'center',type:'bool',isBool:true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true}
		},{
			dataIndex: 'ReaGoods_Id',text: '主键ID',
			hidden: true,hideable: false,isKey: true
		},{
			dataIndex: 'ReaGoods_StorageType',
			text: '储藏条件',hidden:false,
			width: 100,
			editor:{},
			renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta);
				return v;
			}
		},{
			dataIndex: 'ReaGoods_ReaCenOrg_CName',text: '机构',
			width: 100,hidden:true,sortable: false,
			defaultRenderer: true
//			editor:{}
		},{
			dataIndex: 'ReaGoods_Price',text: '参考价格',
			width: 100,editor:{xtype:'numberfield'},
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_RegistNo',text: '注册号',
			width: 100,editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_RegistDate',text: '注册日期',
			width: 90,editor:{xtype:'datefield',format:'Y-m-d'},
			type:'date',isDate:true,defaultRenderer: true
		},{
			dataIndex: 'ReaGoods_RegistNoInvalidDate',text: '注册证有效期',
			width: 90,editor:{xtype:'datefield',format:'Y-m-d'},
			type:'date',//isDate:true,//defaultRenderer: true
			renderer: function(value, meta, record, rowIndex, colIndex, s, v) {
				if(value) {
					var Sysdate = JShell.System.Date.getDate();
					value = Ext.util.Format.date(value, 'Y-m-d');
					var BGColor = "";
					Sysdate = Ext.util.Format.date(Sysdate, 'Y-m-d');
					Sysdate = JShell.Date.getDate(Sysdate);
					var RegisterInvalidDate = value;
					RegisterInvalidDate = JShell.Date.getDate(RegisterInvalidDate);
					var days = parseInt((RegisterInvalidDate - Sysdate) / 1000 / 60 / 60 / 24);
					if(days < 0) {
						BGColor = "red";
					} else if(days >= 0 && days <= 30) {
						BGColor = "#e97f36";
					} else if(days > 30) {
						BGColor = "#568f36";
					}
					if(BGColor)
						meta.style = 'background-color:' + BGColor + ';color:#ffffff;';
				}
				return value;
			}
		},{
			xtype: 'actioncolumn',text: '注册证',
			align: 'center',width: 55,
			style:'font-weight:bold;color:white;background:orange;',
			hideable: false,
			items: [{
				getClass: function(v, meta, record) {
					var GoodsNo = record.get('ReaGoods_GoodsNo') + '';
					var isRegister= record.get('ReaGoods_IsRegister') + '';
					//有产品编号的可以维护注册证
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
					var Id = rec.get('ReaGoods_Id') + '';
					var GoodsCName = rec.get('ReaGoods_GoodsCName') + '';
					//有产品编号的可以维护注册证
					if(GoodsNo){
//						JcallShell.Msg.overwrite('维护注册证');
						 me.showRegisterForm(Id,GoodsCName);
					}else{
						JcallShell.Msg.error('请先维护注册证信息');
					}
				}
			}]
		}];
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
		return columns;
	},
	/**导入试剂信息*/
	onGoodsImportExcel: function(formType) {
		var me = this;
		JShell.Win.open('Shell.class.rea.client.goods2.UploadPanel', {
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
	showForm: function(id,GoodsCName) {
		var me = this,
			config = {
				resizable: false,
				listeners: {
					load:function(){
						var edit = me.getPlugin('NewsGridEditing'); 
		                edit.cancelEdit();
					},
					close:function(){
						me.onSearch();
					}
				}
			};

		if (id) {
			config.formtype = 'edit';
			config.PK = id;
			/**货品ID*/
			config.GoodsID=id;
		    /**货品名称*/
			config.GoodsCName=GoodsCName;
		} else {
			config.formtype = 'add';
		}
		JShell.Win.open('Shell.class.rea.client.goods2.AddPanel', config).show();
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
//				list.push(rec);
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
		var me = this;
		me.showForm(null,null);
				
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		me.showForm(records[0].get(me.PKField),records[0].get('ReaGoods_CName'));
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
	/**试剂模板下载*/
	onGoodsExp:function(){
		JcallShell.Msg.overwrite('试剂模板下载');
		return;
		var url = JShell.System.Path.UI + '/models/rea/goods2/' + JShell.REA.Goods.EXCEL + '?v=' + new Date().getTime();
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
				downloadUrl += '?isUpLoadFile=1&operateType=0&downFileName=平台产品数据&fileName=' + fileName.split('\/')[2];
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
		// 换算 率    要大于1
		for(var i=0;i<len;i++){
			var GonvertQty=changedRecords[i].get('ReaGoods_GonvertQty');
			if(GonvertQty && Number(GonvertQty)!=0 && (Number(GonvertQty)<1 || Number(GonvertQty)==1) ){
				isError=true;
			}
		}
		if(isError){
			JShell.Msg.alert("换算率必须大于1！");
			return;
		}
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for(var i=0;i<len;i++){
			me.updateInfo(i,changedRecords[i]);
		}
	},/**修改信息*/
	updateInfo:function(i,record){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var params = Ext.JSON.encode({
			entity:{
				Id:record.get('ReaGoods_Id'),
				BarCodeMgr:record.get('ReaGoods_BarCodeMgr'),
				IsRegister:record.get('ReaGoods_IsRegister')? 1 : 0,
				IsPrintBarCode:record.get('ReaGoods_IsPrintBarCode')? 1 : 0,
				CName:record.get('ReaGoods_CName'),
				SName:record.get('ReaGoods_SName'),
				GoodsNo:record.get('ReaGoods_GoodsNo'),
				UnitName:record.get('ReaGoods_UnitName'),
				UnitMemo:record.get('ReaGoods_UnitMemo'),
				Visible:record.get('ReaGoods_Visible')? 1 : 0,
				ProdEara:record.get('ReaGoods_ProdEara'),
				Price:record.get('ReaGoods_Price'),
				StorageType:record.get('ReaGoods_StorageType'),
				GoodsClass:record.get('ReaGoods_GoodsClass'),
				GoodsClassType:record.get('ReaGoods_GoodsClassType'),
				GonvertSort:record.get('ReaGoods_GonvertSort'),
				GonvertQty:record.get('ReaGoods_GonvertQty'),
				SuitableType:record.get('ReaGoods_SuitableType'),
				TestCount:record.get('ReaGoods_TestCount'),
			    RegistNo:record.get('ReaGoods_RegistNo'),
				RegistDate:JShell.Date.toServerDate(record.get('ReaGoods_RegistDate')),
				RegistNoInvalidDate:JShell.Date.toServerDate(record.get('ReaGoods_RegistNoInvalidDate'))
			},
			
			fields:'Id,BarCodeMgr,IsRegister,IsPrintBarCode,CName,SName,GoodsNo,UnitName,UnitMemo,Visible,'+
			'ProdEara,Price,StorageType,GoodsClass,'+
			'GoodsClassType,GonvertSort,GonvertQty,TestCount,'+
			'SuitableType,RegistNo,RegistDate,RegistNoInvalidDate'
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
	},
	/**新增注册证表单*/
	showRegisterForm: function(GoodsID,GoodsCName) {
		var me = this,
			config = {
				resizable: false,
				 /**货品ID*/
				GoodsID:GoodsID,
			    /**货品名称*/
				GoodsCName:GoodsCName,
				listeners: {
					save: function(p, records) {
						p.close();
						me.onSearch();
					},
					beforeclose:function( panel,  eOpts ){
						var edit = panel.getPlugin('NewsGridEditing'); 
		                edit.cancelEdit();
					},
					load:function(){
						var edit = me.getPlugin('NewsGridEditing'); 
		                edit.cancelEdit();
					}
				}
			};
		config.formtype = 'add';
		JShell.Win.open('Shell.class.rea.client.goods2.register.Grid', config).show();
	},
	createCellEditListeners: function() {
        var me = this,
        columns = me.columns;
        for (var i in columns) {
            var column = columns[i];
            if (column.editor) {
                column.editor.listeners = column.editor.listeners || {};
                column.editor.listeners.specialkey = function(textField, e) {
                    me.doSpecialkey(textField, e);
                };
                column.hasEditor = true
            } else if (column.columns) {
                for (var j in column.columns) {
                    var c = column.columns[j];
                    if (c.editor) {
                        c.editor.listeners = c.editor.listeners || {};
                        c.editor.listeners.specialkey = function(textField, e) {
                            me.doSpecialkey(textField, e);
                        };
                        c.hasEditor = true
                    }
                }
            }
        }
    },
    doSpecialkey: function(textField, e) {
        var me = this;
        textField.focus();
        var info = me.getKeyInfo(e);
        if (info) {
            me.isSpecialkey = true;
            e.stopEvent();
            me.changeRowIdxAndCelIdx(textField, info.type);
            textField.blur();
        }
    },
    getKeyInfo: function(e) {
        var me = this,
        arr = me.specialkeyArr,
        key = e.getKey();
        var info = null;
        for (var i in arr) {
            var ctrlKey = arr[i].ctrlKey ? true: false;
            var shiftKey = arr[i].shiftKey ? true: false;
            if (arr[i].key == key && ctrlKey == e.ctrlKey && shiftKey == e.shiftKey) {
                if (arr[i].replaceKey) {
                    e.keyCode = arr[i].replaceKey;
                    info = null;
                    break
                } else {
                    info = arr[i];
                    break
                }
            }
        }
        return info
    },
    changeRowIdxAndCelIdx: function(field, type) {
        var me = this,
        context = field.ownerCt.editingPlugin.context,
        rowIdx = context.rowIdx,
        colIdx = context.colIdx;
        me.rowIdx = rowIdx;
        me.colIdx = colIdx;
        if (type == 'up') {
            me.rowIdx = me.getNextRowIndex(rowIdx, false,false);
        } else if (type == 'down') {
        	me.rowIdx = me.getNextRowIndex(rowIdx, true,true,colIdx);
        } else if (type == 'left') {
            me.colIdx = me.getNextColIndex(colIdx, false);
        } else if (type == 'right') {
            me.colIdx = me.getNextColIndex(colIdx, true);
        }
     
    },
    getNextRowIndex: function(rowIdx, isDown,isRowCycle,colIdx) {
        var me = this,
        count = me.store.getCount(),
        nRowIdx = rowIdx;
        if (count == 0) return null;
        isDown ? nRowIdx++:nRowIdx--;
        if (isRowCycle) {
            nRowIdx = nRowIdx % count;
            nRowIdx = nRowIdx < 0 ? nRowIdx + count: nRowIdx
        } else {
            if (nRowIdx == count) {
                nRowIdx = count - 1
            }
            if (nRowIdx == -1) {
                nRowIdx = 0
            }
        }
       
        if(count>0 && me.rowIdx==count-1){
         	me.colIdx = me.getNextColIndex(colIdx, true)
        }
        return nRowIdx
    },
    getNextColIndex: function(colIdx, isRight) {
        var me = this,
        columns = me.columns,
        length = columns.length,
        nColIdx = colIdx;
        if (isRight) {
            for (var i = colIdx + 1; i < length; i++) {
                if (columns[i].hasEditor) {
                    return i
                }
            }
            if (me.isColCycle) {
                for (var i = 0; i < colIdx; i++) {
                    if (columns[i].hasEditor) {
                        return i
                    }
                }
            }
        } else {
            for (var i = colIdx - 1; i >= 0; i--) {
                if (columns[i].hasEditor) {
                    return i
                }
            }
            if (me.isColCycle) {
                for (var i = length - 1; i > colIdx; i--) {
                    if (columns[i].hasEditor) {
                        return i
                    }
                }
            }
        }
        return nColIdx
    },
 
	 /**设置相同码*/
	showConfigForm: function(GoodsNo) {
		var me = this,
			config = {
				resizable: false,
//				idList:idList,
                GoodsNo:GoodsNo,
				listeners: {
					save: function(p) {
						p.close();
						me.onSearch();
					}
				}
			};
		JShell.Win.open('Shell.class.rea.client.goods2.MinUnitGrid', config).show();
	}
});