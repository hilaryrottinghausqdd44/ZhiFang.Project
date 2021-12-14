Ext.define("Shell.class.weixin.dict.lab.BLabSampleType.Grid",{
	extend:"Shell.ux.grid.IsUseGrid",
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.CheckTrigger'
	],
	width: 800,
	height: 500,
	PKField: 'BLabSampleType_Id',
	/**复制按钮点击次数*/
    copyTims:0,
    
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBLabSampleTypeByHQLAndControl',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_DelBLabSampleType',
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用保存按钮*/
	hasSave: false,
	/**默认加载*/
	defaultLoad: false,
	/**不加载时默认禁用功能按钮*/
	defaultDisableControl: false,
	/*禁用查询框;     */
	hasSearch:true,
	/**查询栏参数设置*/
	searchToolbarConfig: {},
	
	 searchInfo: {
		width: 120,
			width: 105,
			emptyText: '编码/名称',
			isLike: true,
			itemId: 'search',
			fields: ['blabsampletype.CName','blabsampletype.LabSampleTypeNo']
	},
	
	initComponent:function(){
		var me =this;
		me.columns=me.createGColumns();
		me.callParent(arguments);
	},
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
		me.showSearch();
	},
	
	createGColumns:function(){
		var me =this;
		var columns=[
		{
				text: 'BLabSampleType_Id',
				dataIndex: 'BLabSampleType_Id',
				width: 100,
				isKey: true,
				hidden: true,
				hideable: false
		},
		{
				text: 'labCode',
				dataIndex: 'BLabSampleType_LabCode',
				width: 100,
				hidden: true,
				hideable: false
		},
		{
			text:'样本编码',
			dataIndex:'BLabSampleType_LabSampleTypeNo',
			width:100,
			hidden:false,
		},{
			text:'中文名称',
			dataIndex:'BLabSampleType_CName',
			width:100,
		},{
			text:'简码',
			dataIndex:'BLabSampleType_ShortCode',
			width:100,
		},{
			text: '是否在用',
				dataIndex: 'BLabSampleType_UseFlag',
				sortable: false,
				width: 100,
				menuDisabled: true,
				renderer : function(value, meta) {
				var v = value + '';
				if (v == '1') {
					meta.style = 'color:green';
					v = '已启用';
				} else if (v == '0') {
					meta.style = 'color:red';
					v = '已禁用';
				} else {
					v == '';
				}
				return v;
				}
		},{
			text: '是否对照',
				dataIndex: 'BLabSampleType_isContrast',
				sortable: false,
				width: 100,
				menuDisabled: true,
				renderer : function(value, meta) {
				var v = value + '';
				if (v != '') {
					meta.style = 'color:green';
					v = '已对照';
				} else if (v == '') {
					meta.style = 'color:red';
					v = '未对照';
				} 
				return v;
				}
		}
		];
		return columns;
	},
	
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
		items = me.dockedItems || [];
		items.push(me.createDefaultButtonToolbarItems());
		if(me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if(me.hasPagingtoolbar) items.push(me.createPagingtoolbar());

		return items;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems: function() {
		var me = this,
			items = [];
		
		items = [{
				fieldLabel: '实验室ID',
				hidden: true,
				xtype: 'textfield',
				name: 'ClienteleId',
				itemId: 'ClienteleId'
			}, {
				fieldLabel: '',
				xtype: 'uxCheckTrigger',
				emptyText: '实验室',
				width: 300,
				labelSeparator: '',
				labelWidth: 55,
				labelAlign: 'right',
				name: 'ClienteleName',
				itemId: 'ClienteleName',
				className: 'Shell.class.weixin.dict.lab.BLabSampleType.CheckGrid',
				listeners: {
					check: function(p, record) {
						me.onClienteleAcceptId(record);
						p.close();
					}
				}
			},
			{
				text: '复制',
				tooltip: '复制',
				iconCls: 'button-copy',
				name: 'btnCopy ',
				itemId: 'btnCopy',
				handler: function() {}
			},
			{
				fieldLabel: '实验室ID',
				hidden: true,
				xtype: 'textfield',
				name: 'SelectClienteleId',
				itemId: 'SelectClienteleId'
			},
			{
				fieldLabel: '',
				xtype: 'uxCheckTrigger',
				emptyText: '实验室',
				width: 300,
				labelSeparator: '',
				hidden: true,
				labelWidth: 55,
				labelAlign: 'right',
				name: 'SelectClienteleName',
				itemId: 'SelectClienteleName',
				className: 'Shell.class.weixin.dict.lab.BLabSickType.CheckGrid',
				classConfig: {
					title: '实验室选择',
					/**是否单选*/
					checkOne: false
				},
				listeners: {
					check: function(p, record) {
						me.onClienteleAccept(record);
						p.close();
					}
				}
			},
			{
				text: '保存',
				tooltip: '保存',
				iconCls: 'button-save',
				itemId: 'btnSave',
				hidden: true,
				handler: function() {
					var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
					var SelectClienteleId = buttonsToolbar2.getComponent('SelectClienteleId');
					var SelectClienteleName = buttonsToolbar2.getComponent('SelectClienteleName');
					var ClienteleId = buttonsToolbar2.getComponent('ClienteleId').getValue();
					var Arr = [];
					var strVal = SelectClienteleId.getValue();
					if(strVal) {
						Arr = strVal.split(",")
					}
					var ItemNoList = me.getLabTestItem();
					if(Arr.length == 0) {
						JShell.Msg.error('请选择目标实验室');
						return;
					}
					var isall = false;
					var msg = '确定把当前勾选的数据复制到目标实验室吗?';
					if(ItemNoList.length == 0) {
						isall = true;
						msg = '确定把当前中心表所有数据复制到目标实验室吗?'
					}
					JShell.Msg.confirm({
						msg: msg
					}, function(but) {
						if(but == "ok") {
							
							me.onCopyClick(Arr, ItemNoList, isall, ClienteleId);
						}
					});
				}
			}
		];
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	
	onClienteleAcceptId:function(record){
		var me =this;
		var buttonsToolbar2 =me.getComponent("buttonsToolbar2");
		var ClienteleId =buttonsToolbar2.getComponent('ClienteleId');
		var ClienteleName =buttonsToolbar2.getComponent('ClienteleName');
		var btnSave = buttonsToolbar2.getComponent('btnSave');
		var SelectClienteleName = buttonsToolbar2.getComponent('SelectClienteleName');
		
		var id = record ? record.get('CLIENTELE_Id') : '';
		
		me.copyTims = 0;
		SelectClienteleName.hide();
		btnSave.hide();
		
		ClienteleId.setValue(id);
		ClienteleName.setValue(record ? record.get('CLIENTELE_CNAME') : '');
		
		if(id) {
			me.internalWhere=' blabsampletype.LabCode='+id;// 设置查询的内部条件
			me.onSearch();
		} else {
			me.clearData();
		}
		me.showSearch(id);
	},
	
	/**启用，禁用查询框*/
	showSearch:function(id){
		var me=this,
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			
			btnCopy = buttonsToolbar2.getComponent('btnCopy');
	    var btnSave = buttonsToolbar2.getComponent('btnSave');	
		var SelectClienteleName = buttonsToolbar2.getComponent('SelectClienteleName');
	
        if(id){
        	btnCopy.enable();
    		btnSave.enable();
    		SelectClienteleName.enable();
        }else{
        	btnCopy.disable();
        	btnSave.disable();
    		SelectClienteleName.disable();
        }
	},
	
	onClienteleAccept:function(record){
		var me = this;
		var bottomToolbar = me.getComponent('buttonsToolbar2');
		var SelectClienteleId = bottomToolbar.getComponent('SelectClienteleId');
		var SelectClienteleName = bottomToolbar.getComponent('SelectClienteleName');
		var idArry=[];
		var nameArry=[];
		
		if(record){
			for(var i =0 ; i < record.length;i++){
				var id = record[i] ? record[i].get('CLIENTELE_Id') : '';
				var name = record[i] ? record[i].get('CLIENTELE_CNAME') : '';
				idArry.push(id);
				nameArry.push(name);
			}
		}
		SelectClienteleId.setValue(idArry);
		SelectClienteleName.setValue(nameArry);
	},
	
	getLabTestItem:function(){
		var me =this;
		var row = me.getSelectionModel().getSelection();
		var arrId=[];
		for(var i =0;i<row.length;i++){
			arrId.push(row[i].get("BLabSampleType_Id"));
		}
		return arrId;
	},
	
	onCopyClick:function(LabCodeList, ItemNoList, isall, ClienteleId){
		var me = this;
		JShell.Win.open('Shell.class.weixin.dict.lab.BLabSampleType.MsgFrom', {
			resizable: false,
			formtype:'add',
			maximizable:false,//是否带最大化功能
			LabCodeList:LabCodeList,
			ItemNoList:ItemNoList,
			IsAll:isall,
			ClienteleId:ClienteleId,
			listeners: {
				save: function(p,entity) {
					p.close();
				}
			}
		}).show();
	},
	/**重写 查询按钮点击处理方法*/
	onSearchClick: function(but, value) {
		var me = this;
		if (!value) {
			me.onSearch();
			return;
		}
		me.internalWhere = me.getSearchWhere(value);
		me.onSearch();
	},
	/**初始化监听*/
	initFilterListeners: function() {
		var me = this;
		var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
		var btnSave = buttonsToolbar2.getComponent('btnSave');	
		var SelectClienteleName = buttonsToolbar2.getComponent('SelectClienteleName');
		var btnCopy = buttonsToolbar2.getComponent('btnCopy');		
		var ClienteleId = buttonsToolbar2.getComponent('ClienteleId');

		btnCopy.on({
			click:function(btn,e){
				if(Number(me.copyTims)==0){
					me.copyTims=Number(me.copyTims)+1;
					SelectClienteleName.show();
					btnSave.show();
				}else{
					me.copyTims=0;
					SelectClienteleName.hide();
					btnSave.hide();
				}
			}
		});
		SelectClienteleName.on({
			beforetriggerclick:function(btn){
				if(ClienteleId.getValue()){
					btn.classConfig = {
	                    checkOne:false,
	                    hasClearButton:true,
						defaultWhere : 'clientele.Id!='+ ClienteleId.getValue()
					};
				}
				
			}
		});
	},
	
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [];
		var bottomToolbar = me.getComponent('buttonsToolbar2');
		var ClienteleId = bottomToolbar.getComponent('ClienteleId');
		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;

		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		
		url += "&labCode=" +ClienteleId.value + '&controlType=0';

		//默认条件
		if(me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if(me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if(me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
		}
		var where = arr.join(") and (");
		if(where) where = "(" + where + ")";

		if(where) {
			url += '&where=' + JShell.String.encode(where);
		} else {
			url += "&where=";
		}

		return url;
	},
});
