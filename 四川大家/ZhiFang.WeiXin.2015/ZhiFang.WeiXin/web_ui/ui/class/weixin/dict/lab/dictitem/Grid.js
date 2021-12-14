/**
 * 区域项目表
 * @author liangyl
 * @version 2018-02-01
 */
Ext.define('Shell.class.weixin.dict.lab.dictitem.Grid', {
	extend: 'Shell.ux.grid.IsUseGrid',
	requires: [
	    'Ext.ux.CheckColumn',
	    'Shell.ux.form.field.CheckTrigger'
	],
	title: '区域项目列表 ',
	width: 800,
	height: 500,
  	/**获取数据服务路径*/
	selectUrl: '/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchOSBLabTestItemByLabCode?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_UpdateBLabTestItemByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_DelBLabTestItem',
    /**复制保存数据服务路径*/
	copyUrl:'/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_BLabGroupItemCopy',    
    /**是否启用修改按钮*/
	hasEdit:false,
	/**默认加载*/
	defaultLoad: false,
	/**查询栏参数设置*/
	searchToolbarConfig:{},
	/**复制按钮点击次数*/
    copyTims:0,
    /**实验室*/
    ClienteleID:null,
    hideTimes:1000,
    autoSelect: false,
    /**序号列宽度*/
	rowNumbererWidth: 35,
	/**被选择的数据行*/
	recArr: [],
	/**翻页提示，是否是第一次加载*/
	IsPagLoad:true,
//	defaultOrderBy:[{ property: 'BLabTestItem_ItemNo', direction: 'ASC' }],
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
		me.showSearch();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('ClienteleClick','delSelectClick');
		
		//数据列
		me.columns = me.createGridColumns();
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
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
				var UseFlag =record.get('BLabTestItem_UseFlag');
			    var IsMappingFlag =record.get('BLabTestItem_IsMappingFlag');
				//启用 并且对照状态=未对照
				if((UseFlag || UseFlag=='1') && IsMappingFlag=='false'){
                    Color = '<span style="padding:0px;color:Violet; border:solid 3px Violet"></span>&nbsp;'
				}
				//启用 并且对照状态=已对照
				if((UseFlag || UseFlag=='1') && IsMappingFlag=='true'){
                    Color = '<span style="padding:0px;color:white;"></span>&nbsp;&nbsp;&nbsp;'
				}
				//禁用
				if(!UseFlag || UseFlag=='0'){
					Color = '<span style="padding:0px;color:gray; border:solid 3px gray"></span>&nbsp;'
				}
				v = Color + value;
//				meta.tdAttr = 'data-qtip="<b>' + value + '</b>"';
				return v;
			}
		},{
			text:'项目编号',dataIndex:'BLabTestItem_ItemNo',width:85,
			sortable:true,menuDisabled:true,
			defaultRenderer:true
		},{
			text:'中文名称',dataIndex:'BLabTestItem_CName',width:150,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'三甲价格',dataIndex:'BLabTestItem_MarketPrice',width:70,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'内部价格',dataIndex:'BLabTestItem_GreatMasterPrice',width:70,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'执行价格',dataIndex:'BLabTestItem_Price',width:70,
			sortable:true,menuDisabled:true,defaultRenderer:true
		},{
			text:'咨询费',dataIndex:'BLabTestItem_BonusPercent',width:70,
			sortable:true,menuDisabled:true,editor:{xtype:'numberfield',decimalPrecision: 4,minValue:0},defaultRenderer:true
		},{
			text:'英文名称',dataIndex:'BLabTestItem_EName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简称',dataIndex:'BLabTestItem_ShortName',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},{
			text:'简码',dataIndex:'BLabTestItem_ShortCode',width:100,
			sortable:false,menuDisabled:true,defaultRenderer:true
		},
		{
			text:'是否在用',dataIndex:'BLabTestItem_UseFlag',
			width:55,align:'center',sortable:false,menuDisabled:true,
//			stopSelection:false,
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
		},
		{
			text:'对照状态',dataIndex:'BLabTestItem_IsMappingFlag',width:100,
			sortable:false,menuDisabled:true,
			renderer : function(value, meta) {
				var v = value + '';
				if (v == 'true') {
					meta.style = 'color:green';
					v = '已对照';
				} else if (v == 'false') {
					meta.style = 'color:red';
					v = '未对照';
				} else {
					v == '';
				}

				return v;
			}
		},{
			text:'主键ID',dataIndex:'BLabTestItem_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'LabCode',dataIndex:'BLabTestItem_LabCode',hidden:true,hideable:false
		}]
		
		return columns;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
			
		//查询框信息
		buttonToolbarItems.unshift('refresh','-','add','edit');
		buttonToolbarItems.push('-','del','-',{text:'禁用',tooltip:'禁用',iconCls:'button-cancel',
		    name: 'btnDisable',itemId: 'btnDisable',
			handler:function(){
				me.onChangeUseField(false);
			}
		},{text:'启用',tooltip:'启用',iconCls:'button-accept',
			name: 'btnEnable ',itemId: 'btnEnable',
			handler:function(){
				me.onChangeUseField(true);
			}
     	},'->',{text:'列表保存',tooltip:'列表,三甲价格,内部价格，咨询费保存',iconCls:'button-save',
			itemId:'btnSave2',
			handler:function(){
				me.onSaveClick();
			}
			});
		return buttonToolbarItems;
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
			if(i == 'blabtstitem.ItemNo'){
				if(!isNaN(value)){
					where.push("blabtstitem.ItemNo=" + value);
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
	/**实验室选择*/
	onClienteleAccept: function(records) {
		var me = this;
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
	    var SelectClienteleId = buttonsToolbar.getComponent('SelectClienteleId');
		var SelectClienteleName = buttonsToolbar.getComponent('SelectClienteleName');
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
        SelectClienteleId.setValue(Arr);
		SelectClienteleName.setValue(ArrName);
		
	},
	/**获取列表选中行*/
	getLabTestItem:function(){
		var me =this;
		var Id='',Arr=[];
		var	records = me.getSelectionModel().getSelection();
		if (records.length > 0) {
			for(var i =0; i<records.length; i++ ){
	        	Id=records[i] ? records[i].get('BLabTestItem_Id') : '';
	            if(Id)Arr.push(Id);
	        }
		}
		return Arr;
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this;
		
	    me.fireEvent('onEditClick', me);
	},
	onAddClick:function(){
		var me =this;
			var	buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
		ClienteleId = buttonsToolbar2.getComponent('ClienteleId').getValue();
		me.fireEvent('onAddClick',ClienteleId);
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
				UseFlag: IsUse
			},
			fields: 'Id,UseFlag'
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
		me.searchInfo = {width:105,emptyText:'编号/名称/简称',isLike:true,
			itemId: 'search',fields:['blabtestitem.CName','blabtestitem.ShortName','blabtestitem.ItemNo']};
			items = [
			{fieldLabel:'实验室ID',hidden:true,xtype:'textfield',
				name:'ClienteleId',itemId:'ClienteleId'
		    },{
				fieldLabel: '',xtype: 'uxCheckTrigger',emptyText: '实验室',
				width:125,labelSeparator:'',
				labelWidth: 55,
				labelAlign: 'right',
				name: 'ClienteleName',itemId: 'ClienteleName',
				className: 'Shell.class.weixin.dict.lab.dictitem.CheckGrid',
				listeners: {
					check: function(p, record) {
						me.onClienteleAcceptId(record);
						p.close();
					}
				}
			},{
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
			width:125,labelSeparator:'',hidden:true,
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
				var ClienteleId = buttonsToolbar2.getComponent('ClienteleId').getValue();
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
				var isall=true;
				var msg='确定把当前勾选的数据复制到目标实验室吗?';
				if(ItemNoList.length==0){
					isall=false;
					msg='确定把当前中心表所有数据复制到目标实验室吗?'
				}
				JShell.Msg.confirm({
					msg:msg
				},function(but){
					if (but == "ok") {
						me.onCopyClick(Arr,ItemNoList,isall,ClienteleId);
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

	  /**实验室选择*/
	onClienteleAcceptId: function(record) {
		var me = this;
	    var bottomToolbar = me.getComponent('buttonsToolbar2');
	    var ClienteleId = bottomToolbar.getComponent('ClienteleId');
		var btnSave = bottomToolbar.getComponent('btnSave');	
		var SelectClienteleName = bottomToolbar.getComponent('SelectClienteleName');
	    me.copyTims=0;
	    SelectClienteleName.hide();
		btnSave.hide();
		
		var ClienteleName = bottomToolbar.getComponent('ClienteleName');
        var id=record ? record.get('CLIENTELE_Id') : '';
		
		ClienteleId.setValue(id);
		ClienteleName.setValue(record ? record.get('CLIENTELE_CNAME') : '');
		me.fireEvent('ClienteleClick', id);
		if(id){
			me.onSearch();
		}else{
			me.clearData();
		}
		me.showSearch(id);
	},
	
	
	/**启用，禁用查询框*/
	showSearch:function(id){
		var me=this,
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			search = buttonsToolbar2.getComponent('search');
			btnCopy = buttonsToolbar2.getComponent('btnCopy');
	    var btnSave = buttonsToolbar2.getComponent('btnSave');	
		var SelectClienteleName = buttonsToolbar2.getComponent('SelectClienteleName');
	
        if(id){
        	search.enable();
        	btnCopy.enable();
    		btnSave.enable();
    		SelectClienteleName.enable();
        }else{
        	search.disable();
        	btnCopy.disable();
        	btnSave.disable();
    		SelectClienteleName.disable();
        }
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
			ClienteleId = buttonsToolbar2.getComponent('ClienteleId').getValue();
			search = buttonsToolbar2.getComponent('search').getValue();
		}
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'fields=' + me.getStoreFields(true).join(',');
		url+='&LabCode='+ClienteleId;
		
	
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
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me=this,list=[],result={};
		var arr=[];	var count=0;
		if(data && data.value){
			count=data.value.count;
			list=data.value.list;
			len=list.length;
			for(var i=0;i<len;i++){
				var	obj={
					BLabTestItem_LabCode:list[i].LabCode,
					BLabTestItem_ItemNo:list[i].ItemNo,
					BLabTestItem_CName:list[i].CName,
					BLabTestItem_EName:list[i].EName,
					BLabTestItem_ShortName:list[i].ShortName,
					BLabTestItem_ShortCode:list[i].ShortCode,
					BLabTestItem_DiagMethod:list[i].DiagMethod,
					BLabTestItem_Unit:list[i].Unit,
					BLabTestItem_IsCalc:list[i].IsCalc,
					BLabTestItem_Visible:list[i].Visible,
					BLabTestItem_DispOrder:list[i].DispOrder,
					BLabTestItem_Prec:list[i].Prec,
					BLabTestItem_IsProfile:list[i].IsProfile,
					BLabTestItem_Price:list[i].Price,
					BLabTestItem_MarketPrice:list[i].MarketPrice,
					BLabTestItem_GreatMasterPrice:list[i].GreatMasterPrice,
					BLabTestItem_OrderNo:list[i].OrderNo,
					BLabTestItem_IsDoctorItem:list[i].IsDoctorItem,
					BLabTestItem_IschargeItem:list[i].IschargeItem,
					BLabTestItem_IsCombiItem:list[i].IsCombiItem,
					BLabTestItem_Color:list[i].Color,
					BLabTestItem_StandCode:list[i].StandCode,
					BLabTestItem_ZFStandCode:list[i].ZFStandCode,
					BLabTestItem_UseFlag:list[i].UseFlag,
					BLabTestItem_LabSuperGroupNo:list[i].LabSuperGroupNo,
					BLabTestItem_Pic:list[i].Pic,
					BLabTestItem_BonusPercent:list[i].BonusPercent,
					BLabTestItem_CostPrice:list[i].CostPrice,
					BLabTestItem_InspectionPrice:list[i].InspectionPrice,
					BLabTestItem_IsMappingFlag:list[i].IsMappingFlag,
					BLabTestItem_Id:list[i].Id,
					BLabTestItem_LabID:list[i].LabID,
					BLabTestItem_DataAddTime:list[i].DataAddTime
				};
				arr.push(obj);
			}
		}
		result.count=count;
		result.list = arr;	
		return result;
	},
	
	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true;
		var buttonsToolbar = me.getComponent('buttonsToolbar');
			toolbars = me.dockedItems.items || [],
			len = toolbars.length,
			items = [];
		for (var i = 0; i < len; i++) {
			if(toolbars[i].itemId!='buttonsToolbar')continue;
			if (toolbars[i].xtype == 'header') continue;
			if (toolbars[i].isLocked) continue;
			var fields = toolbars[i].items.items;
			items = items.concat(fields);
		}
		var iLength = items.length;
		for (var i = 0; i < iLength; i++) {
			items[i][enable ? 'enable' : 'disable']();
		}
		if (bo) {
			me.defaultLoad = true;
		}
	},
	onRefreshClick:function(){
		var me=this;
		var	buttonsToolbar2 = me.getComponent('buttonsToolbar2'),
			ClienteleId = buttonsToolbar2.getComponent('ClienteleId').getValue();
		if(!ClienteleId) return;
		me.onSearch();
	},
	/** 
	 * 复制保存按钮点击处理方法
	 * LabCodeList 实验室项
	 * ItemNoList  要复制的项目
	 * */
	onCopyClick:function(LabCodeList,ItemNoList,isall,ClienteleId){
		var me = this;
		JShell.Win.open('Shell.class.weixin.dict.lab.dictitem.MsgFrom', {
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
	/**加载数据后*/
	onAfterLoad: function(records, successful) {
		var me = this;

		me.enableControl(); //启用所有的操作功能
		if (me.errorInfo) {
			var msg = me.msgFormat.replace(/{msg}/, JShell.Server.NO_DATA);
			me.getView().update(msg);
			me.errorInfo = null;
		} else {
			if (!records || records.length <= 0) {
				var msg = me.msgFormat.replace(/{msg}/, JShell.Server.NO_DATA);
				me.getView().update(msg);
			}
		}

		if (!records || records.length <= 0) {
			me.fireEvent('nodata', me);
			return;
		}
		me.fireEvent('load', me);
		//默认选中处理
		me.doAutoSelect(records, me.autoSelect);
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
			me.updateInfo(i,changedRecords[i]);
		}
	},/**修改信息*/
	updateInfo:function(i,record){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var entity={
			Id:record.get('BLabTestItem_Id')
		};
		var fields='Id';
		if(record.get('BLabTestItem_BonusPercent')){
			entity.BonusPercent=record.get('BLabTestItem_BonusPercent');
		    fields+=',BonusPercent';
		}
		var params = Ext.JSON.encode({
			entity:entity,
			fields:fields
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
				    JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,me.hideTimes);
				}else{
					JShell.Msg.error("保存信息有误！");
				}
			}
		},false);
	}
});