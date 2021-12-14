/**
 * 中心项目字典表
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.core.itemallitem.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	requires: [
	    'Ext.ux.CheckColumn',
	    'Shell.ux.form.field.CheckTrigger'
	],
	
	title: '中心项目字典表 ',
	width: 800,
	height: 500,
  	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchItemAllItemByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateItemAllItemByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/DictionaryService.svc/ST_UDTO_DelItemAllItem',
	 /**是否启用修改按钮*/
	hasEdit:false,
	/**默认加载*/
	defaultLoad: true,
	
	/**查询栏参数设置*/
	searchToolbarConfig:{},
	/**复制按钮点击次数*/
    copyTims:0,
    autoSelect: false,
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
	},
	initComponent: function() {
		var me = this;
		//数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		var columns = [{
			text:'',dataIndex:'',width:10,
			sortable:true,menuDisabled:true,align: 'left',
			renderer: function(value, meta, record) {
				var v = "",Color='';
				var UseFlag =record.get('TestItem_Visible');
				//禁用
				if(UseFlag=='0'){
					Color = '<span style="padding:0px;color:gray; border:solid 3px gray"></span>'
				}
				//启用 并且对照状态=已对照
				if(UseFlag=='1'){
                    Color = '<span style="padding:0px;color:white;"></span>'
				}
				v = Color;
//				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		},{
			text:'项目编号',dataIndex:'TestItem_Id',width:100,
			isKey:true,sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'中文名称',dataIndex:'TestItem_CName',minWidth:150,
			flex:1,sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'英文名称',dataIndex:'TestItem_EName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'TestItem_ShortName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简码',dataIndex:'TestItem_ShortCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'三甲价格',dataIndex:'TestItem_MarketPrice',width:100,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'内部价格',dataIndex:'TestItem_GreatMasterPrice',width:100,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'执行价格',dataIndex:'TestItem_Price',width:100,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'是否使用',dataIndex:'TestItem_Visible',width:100,
			hidden:true,sortable:false,menuDisabled:true,defaultRenderer:true
		}];
		
		return columns;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
			
		buttonToolbarItems.unshift('add','edit',
		{text:'禁用',tooltip:'禁用',iconCls:'button-cancel',
		    name: 'btnDisable',itemId: 'btnDisable',
			handler:function(){
				me.onChangeUseField(false);
			}
		},{text:'启用',tooltip:'启用',iconCls:'button-accept',
			name: 'btnEnable ',itemId: 'btnEnable',
			handler:function(){
				me.onChangeUseField(true);
			}
     	});
		return buttonToolbarItems;
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		items.push(me.createDefaultButtonToolbarItems());
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());

		return items;
	},
		/**默认按钮栏*/
	createDefaultButtonToolbarItems:function(){
		var me = this,
		items = [];
		//查询框信息
		me.searchInfo = {width:125,emptyText:'编号/名称/简称',isLike:true,
			itemId: 'search',fields:['testitem.CName','testitem.ShortName','testitem.Id']};
			items = ['refresh','-',
			{
				type: 'search',
				info: me.searchInfo
			},'-',{text:'复制',tooltip:'复制',iconCls:'button-copy',
			name: 'btnCopy ',itemId: 'btnCopy',
			handler:function(){
			}
		},{fieldLabel:'实验室ID',hidden:true,xtype:'textfield',
			name:'SelectClienteleId',itemId:'SelectClienteleId'
	    },{
			fieldLabel: '',xtype: 'uxCheckTrigger',emptyText: '实验室',
			width:135,labelSeparator:'',hidden:true,
			labelWidth: 55,labelAlign: 'right',
			name: 'SelectClienteleName',itemId: 'SelectClienteleName',
			className: 'Shell.class.weixin.dict.lab.dictitem.CheckGrid',
			classConfig: {
				title:'实验室选择',
				/**是否单选*/
	            checkOne:false
			},
			listeners: {
				check: function(p, record) {
					me.onClienteleAccept(record);
					p.close();
				}
			}
		},{
			text:'保存',tooltip:'保存',iconCls:'button-save',
			itemId:'btnSave',hidden: true,
			handler:function(){
				var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
				var SelectClienteleId = buttonsToolbar2.getComponent('SelectClienteleId');
				var SelectClienteleName = buttonsToolbar2.getComponent('SelectClienteleName');
				var Arr=[];
				var strVal=SelectClienteleId.getValue();
				if(strVal){
					Arr=strVal.split(",")
				}
				var ItemNoList=me.getLabTestItem();
				if(Arr.length==0){
		        	JShell.Msg.error('请选择目标实验室');
		        	return;
		        }
				var msg='确定把当前勾选的数据复制到目标实验室吗?';
				if(ItemNoList.length==0){
					msg='确定把当前中心表所有数据复制到目标实验室吗?'
				}
				var isall=true;
				if(ItemNoList.length >0){
					isall=false;
				}
				JShell.Msg.confirm({
					msg:msg
				},function(but){
					if (but == "ok") {
						me.onCopyClick(Arr,ItemNoList,isall);
					}
				});
			}
		}];
	    return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'top',
			itemId: 'buttonsToolbar2',
			items: items
		});
	},
	/** 
	 * 复制保存按钮点击处理方法
	 * LabCodeList 实验室项
	 * ItemNoList  要复制的项目
	 * */
	onCopyClick:function(LabCodeList,ItemNoList,isall){
		var me = this;
		JShell.Win.open('Shell.class.weixin.dict.core.itemallitem.MsgFrom', {
			resizable: false,
			formtype:'add',
			maximizable:false,//是否带最大化功能
			LabCodeList:LabCodeList,
			ItemNoList:ItemNoList,
			IsAll:isall,
			listeners: {
				save: function(p,entity) {
					p.close();
//                  me.onSaveInfo(entity,hasAllLabCode);
				}
			}
		}).show();
	},
		/**获取查询框内容*/
	getSearchWhere: function(value) {
		var me = this;
		//查询栏不为空时先处理内部条件再查询
		var searchInfo = me.searchInfo,
			isLike = searchInfo.isLike,
			fields = searchInfo.fields || [],
			len = fields.length,
			where = [];

		for (var i = 0; i < len; i++) {
			if(i == 'testitem.Id'){
				if(!isNaN(value)){
					where.push("testitem.Id=" + value);
				}
				continue;
			}
			if (isLike) {
				where.push(fields[i] + " like '%" + value + "%'");
			} else {
				where.push(fields[i] + "='" + value + "'");
			}
		}
		return where.join(' or ');
	},
	/**初始化监听*/
	initFilterListeners: function() {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
		var btnSave = buttonsToolbar.getComponent('btnSave');	
		var SelectClienteleName = buttonsToolbar.getComponent('SelectClienteleName');
		var btnCopy = buttonsToolbar.getComponent('btnCopy');
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
	},
	onClienteleAccept: function(records) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
	    var ClienteleId = buttonsToolbar.getComponent('SelectClienteleId');
		var ClienteleName = buttonsToolbar.getComponent('SelectClienteleName');
        var id='',name='';
        var Arr=[],ArrName=[];
        if(records){
        	for(var i =0; i<records.length; i++ ){
	        	id = records[i] ? records[i].get('CLIENTELE_Id') : '';
	        	name = records[i] ? records[i].get('CLIENTELE_CNAME') : '';
	        	Arr.push(id);
	        	ArrName.push(name);
	        }
        }
        
        ClienteleId.setValue(Arr);
		ClienteleName.setValue(ArrName);
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this;
	    me.fireEvent('onEditClick', me);
	},
	onAddClick:function(){
		var me =this;
		me.fireEvent('onAddClick', me);
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		//状态为已对照的 不允许删除
        if(records[0].get('TestItem_ControlStatus')=='1'){
            JShell.Msg.error(records[0].get('TestItem_ItemNo')+'已存在对照关系,不能删除！');
			return;
        }
        
		JShell.Msg.del(function(but) {
			if (but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;

			me.showMask(me.delText); //显示遮罩层
			for (var i in records) {
				var id = records[i].get('TestItem_Id');
				me.delOneById(i, id);
			}
		});
	},
	/**获取列表选中行*/
	getLabTestItem:function(){
		var me =this;
		var Id='',Arr=[];
		var	records = me.getSelectionModel().getSelection();
		if (records.length > 0) {
			 for(var i =0; i<records.length; i++ ){
	        	Id=records[i] ? records[i].get('TestItem_Id') : '';
	            if(Id)Arr.push(Id);
	        }
		}
		return Arr;
	},
	/**批量修改使用字段值*/
	onChangeUseField: function(IsUse) {
		var me = this,
			records = me.getSelectionModel().getSelection(),
			len = records.length;
		if(len == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		me.showMask(me.saveText); //显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		for(var i = 0; i < len; i++) {
			var rec = records[i];
			var id = rec.get(me.PKField);
			me.updateOneByIsUse(i, id, IsUse);
		}
	},
	updateOneByIsUse: function(index, id, IsUse) {
		var me = this;
		var url = (me.editUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
	    IsUse = IsUse ? 1 : 0;
		var params = {
			entity: {
				Id: id,
				Visible: IsUse
			},
			fields: 'Id,Visible'
		};

		setTimeout(function() {
			JShell.Server.post(url, Ext.JSON.encode(params), function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if(data.success) {
					if(record) {
						record.set(me.DelField, true);
						record.commit();
					}
					me.saveCount++;
				} else {
					me.saveErrorCount++;
					if(record) {
						record.set(me.DelField, false);
						record.commit();
					}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength) {
					me.hideMask(); //隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		}, 100 * index);
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			arr = [],
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			search = null,ClienteleId=null;
        if(!buttonsToolbar) return;
        var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID) || -1;

		var url = (me.selectUrl.slice(0, 4) == 'http' ? '' :
			JShell.System.Path.ROOT) + me.selectUrl;
        if(buttonsToolbar2) {
//			ClienteleId = buttonsToolbar2.getComponent('ClienteleId').getValue();
			search = buttonsToolbar2.getComponent('search').getValue();
		}
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
//		url+='&LabCode='+ClienteleId;
		
	
		me.internalWhere='';
		//默认条件
		if (me.defaultWhere && me.defaultWhere != '') {
			arr.push(me.defaultWhere);
		}
		//内部条件
		if (me.internalWhere && me.internalWhere != '') {
			arr.push(me.internalWhere);
		}
		//外部条件
		if (me.externalWhere && me.externalWhere != '') {
			arr.push(me.externalWhere);
	    }
		if(search) {
			if(me.internalWhere) {
				arr.push(' and (' + me.getSearchWhere(search) + ')') ;
			} else {
				arr.push(me.getSearchWhere(search));
			}
		}
	    if(arr.length > 0) {
			me.internalWhere = arr.join(' and ');
		} else {
			me.internalWhere = '';
		}
		var where = arr.join(") and (");
		if (where) where = "(" + where + ")";

		if (where) {
			url += '&where=' + JShell.String.encode(where);
		}
		return url;
	}
});