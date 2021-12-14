Ext.define("Shell.class.weixin.dict.core.SampleType.Grid",{
	extend:'Shell.ux.grid.IsUseGrid',
	requires: [
		'Ext.ux.CheckColumn',
		'Shell.ux.form.field.CheckTrigger'
	],
	PKField: 'SampleType_Id',
	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchSampleTypeByHQL?isPlanish=true',
	/**删除数据服务路径*/
	delUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_DelSampleType',
	/**是否启用修改按钮*/
	hasEdit: false,
	/**是否启用保存按钮*/
	hasSave: false,
	copyTims:0,
	/**默认加载*/
	defaultLoad: true,
	
/**复制按钮点击次数*/
    copyTims:0,
    
    searchInfo: {
		width: 120,
			emptyText: '编码/名称',
			isLike: true,
			itemId: 'search',
			fields: ['sampletype.CName',"sampletype.Id"]
	},
    
    
	/**查询栏参数设置*/
	searchToolbarConfig: {},
	
	initComponent:function(){
		var me = this;
		me.columns=me.createGcolumns();
		me.callParent(arguments);
	},
	
	afterRender:function(){
		var me =this;
		
		me.callParent(arguments);
		me.initFilterListeners();
	},
	
	createGcolumns:function(){
		var me =this;
		var columns=[
		{
			text:'样本编码',
			dataIndex:'SampleType_Id',
			width:100,
			isKey:true,
			hidden:false,
		},{
			text:'中文名称',
			dataIndex:'SampleType_CName',
			width:100,
		},{
			text:'简码',
			dataIndex:'SampleType_ShortCode',
			width:100,
		},{
			text: '是否显示',
				dataIndex: 'SampleType_Visible',
				sortable: false,
				width: 100,
				menuDisabled: true,
				renderer : function(value, meta) {
				var v = value + '';
				if (v == '1') {
					meta.style = 'color:green';
					v = '显示';
				} else if (v == '0') {
					meta.style = 'color:red';
					v = '不显示';
				} else {
					v == '';
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
			items.push(me.createMeButtontoolbar());
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());

		return items;
	},
	
	
	/**创建功能按钮栏*/
	createMeButtontoolbar: function() {
		var me = this;
		
		items = [
		
		{
			text:'复制',
			tooltip:'复制',
			icoCls:'buttion-copy',
			name: 'btnCopy ',
			itemId: 'btnCopy',
			handler: function() {}
		},
		{
			fieldLabel: '实验室id',
			hidden: true,
			xtype: 'textfield',
			name: 'SelectClienteleId',
			itemId: 'SelectClienteleId'
		},
		{
			xtype: 'uxCheckTrigger',
				emptyText: '实验室',
				width: 300,
				labelSeparator: '',
				hidden: true,
				labelWidth: 55,
				labelAlign: 'right',
				name: 'SelectClienteleName',
				itemId: 'SelectClienteleName',
				className: 'Shell.class.weixin.dict.core.SampleType.CheckGrid',
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
							me.onCopyClick(Arr, ItemNoList, isall);
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
	
	onClienteleAccept:function(record){
		var me =this;
		var buttonsToolbar=me.getComponent("buttonsToolbar2");
		var SelectClienteleId=buttonsToolbar.getComponent('SelectClienteleId');
		var SelectClienteleName=buttonsToolbar.getComponent('SelectClienteleName');
		var arrId=[];
		var arrName=[];
		for(var i =0;i < record.length;i++){
			arrId.push(record[i].get('CLIENTELE_Id'));
			arrName.push(record[i].get('CLIENTELE_CNAME'));
		}
		SelectClienteleId.setValue(arrId);
		SelectClienteleName.setValue(arrName);
	},
	
	initFilterListeners:function(){
		var me =this;
		var buttonsToolbar=me.getComponent("buttonsToolbar2");
		var btnSave=buttonsToolbar.getComponent('btnSave');
		var btnCopy = buttonsToolbar.getComponent('btnCopy');
		var SelectClienteleName=buttonsToolbar.getComponent('SelectClienteleName');
		
		btnCopy.on({
			click:function(btn,e){
				if(Number(me.copyTims) == 0) {
					me.copyTims = Number(me.copyTims) + 1;
					SelectClienteleName.show();
					btnSave.show();
				} else {
					me.copyTims = 0;
					SelectClienteleName.hide();
					btnSave.hide();
				}
			}
		});
	},
	
	getLabTestItem:function(){
		var me =this;
		var row = me.getSelectionModel().getSelection();
		var arrId=[];
		for(var i =0 ; i< row.length;i++){
			arrId.push(row[i].get('SampleType_Id'));
		}
		return arrId;
	},
	
	// arr实验室id
	onCopyClick:function(Arr, ItemNoList, isall){
		var me =this;
		JShell.Win.open('Shell.class.weixin.dict.core.SampleType.MsgFrom', {
			//resizable: false,
			formtype: 'add',
			maximizable: false, //是否带最大化功能
			LabCodeList: Arr,
			ItemNoList: ItemNoList,
			IsAll: isall,
			listeners: {
				save: function(p, entity) {
					p.close();
				}
			}
		}).show();
	},
});
