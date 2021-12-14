/**
 * 产品列表-供应商
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.rea.goods.GridComp', {
	extend: 'Shell.class.rea.goods.Grid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.toolbar.Button'
	],
	initComponent: function() {
		var me = this;
		me.CENORG_ID = JShell.REA.System.CENORG_ID;
		me.CENORG_NAME = JShell.REA.System.CENORG_NAME;
		//默认条件
		me.defaultWhere = "goods.CenOrg.Id=" + me.CENORG_ID;
		me.callParent(arguments);
	},
	/**创建功能 按钮栏*/
	createButtonToolbarItems: function() {
		var me = this;
		var items = ['refresh', '-', 'add', 'edit', 'del', '-',{
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
		},'-',{
			xtype:'splitbutton',
            textAlign: 'left',
			iconCls:'file-excel',
			text:'导入导出',
			handler:function(btn,e){btn.overMenuTrigger = true;btn.onClick(e);},
			menu:[{
				text: '试剂模板下载',iconCls: 'button-exp',tooltip: '试剂模板下载',
				handler: function() {me.onGoodsExp();}
			},'-',{
				text: '试剂导入',iconCls: 'file-excel',tooltip: 'EXCEL导入试剂',
				handler: function() {me.onGoodsImportExcel('Comp');}
			}, {
				text: '多厂家试剂导入',iconCls: 'file-excel',tooltip: 'EXCEL导入试剂，多个厂家试剂一起导入',
				handler: function() {me.onGoodsImportExcel('Comp',true);}
			}, '-',{
				text: '试剂勾选导出',iconCls: 'file-excel',tooltip: '导出勾选的试剂信息到EXCEL',
				handler: function() {me.onGoodsExportExcel(1);}
			},{
				text: '试剂条件导出',iconCls: 'file-excel',tooltip: '导出复合条件的试剂信息到EXCEL',
				handler: function() {me.onGoodsExportExcel(2);}
			}]
		}, '-',{
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
		}, '-'];

		items = items.concat(me.createCheckCom());
		
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
	/**导入试剂信息*/
	onGoodsImportExcel: function(formType,noProd) {
		var me = this;
		
		var config = {
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
		};
		if(noProd){
			config.Prod = {Id:'',Name:'',readOnly:false,hidden:true,allowBlank:true};
		}
		
		JShell.Win.open('Shell.class.rea.goods.UploadPanel', config).show();
	},
	/**实验室、供应商、厂商查询*/
	createCheckCom: function() {
		var me = this;
		var items = [{
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
			config.CenOrg = {Id:'',Name:'',readOnly:true};//机构信息
		} else {
			config.formtype = 'add';
			config.CenOrg = {Id:me.CENORG_ID,Name:me.CENORG_NAME,readOnly:true};//机构信息
    		config.Comp = {Id:me.CENORG_ID,Name:me.CENORG_NAME,readOnly:false};//上级供应商信息
    		config.Prod = {Id:me.CENORG_ID,Name:me.CENORG_NAME,readOnly:false};//厂商信息
		}

		JShell.Win.open('Shell.class.rea.goods.Form', config).show();
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