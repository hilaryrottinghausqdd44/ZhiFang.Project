/**
 * 客户列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.wfm.client.Grid',{
    extend: 'Shell.ux.model.IsUseGrid',
    requires: [
		'Shell.ux.form.field.CheckTrigger'
	],
	title: '客户列表',
	
	/**获取数据服务路径*/
	selectUrl: '/SingleTableService.svc/ST_UDTO_SearchPClientByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl: '/SingleTableService.svc/ST_UDTO_UpdatePClientByField',
	/**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_DelPClient',
	/**导出客户数据服务路径*/
	downlUrl: '/SingleTableService.svc/ST_UDTO_ExportExcelPClient',

    /**默认排序字段*/
	defaultOrderBy: [{
		property: 'PClient_ClientNo',
		direction: 'DESC'
	}],
	/**默认加载数据*/
	defaultLoad: true,
	/**默认选中数据*/
	autoSelect: true,
		/**后台排序*/
	remoteSort: true,
	/**是否使用字段*/
	IsUseField: 'PClient_IsUse',
	
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		me.initFilterListeners();
		me.on({
			itemdblclick:function(view,record){
				me.openEditForm(record.get(me.PKField));
			}
		});
		
	},
	initComponent: function() {
		var me = this;
		//创建功能按钮栏Items
		me.buttonToolbarItems = me.createButtonToolbarItems();
		me.callParent(arguments);
	},
	/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		
		if (me.hasButtontoolbar) items.push(me.createButtontoolbar());
		if (me.hasPagingtoolbar) items.push(me.createPagingtoolbar());
		items.push(me.createDefaultButtonToolbarItems());

		return items;
	},
	/**默认按钮栏*/
	createDefaultButtonToolbarItems:function(){
		var me = this;
		var items = {
			xtype:'uxButtontoolbar',
			dock:'top',
			itemId:'buttonsToolbar2',
			items:['refresh','-','add','edit','save',
			{
			text:'导出',iconCls:'file-excel',itemId:'Excel',tooltip:'导出',  hidden:false,
			handler:function(){
				me.onDownExcel();
			}
		},'-',{
				width:180,labelWidth:60,labelAlign:'right',emptyText:'客户类型',
				xtype:'uxCheckTrigger',itemId:'ClientTypeName',fieldLabel:'客户类型',
				className:'Shell.class.wfm.dict.CheckGrid',
					classConfig:{
					title:'客户类型选择',
					defaultWhere:"pdict.BDictType.Id='" + JShell.WFM.GUID.DictType.ClientType.value + "'"
				}
			},{
				xtype:'textfield',itemId:'ClientTypeID',fieldLabel:'客户类型ID',hidden:true
			},{
				width:180,labelWidth:60,labelAlign:'right',emptyText:'医院类别',
				xtype:'uxCheckTrigger',itemId:'HospitalTypeName',fieldLabel:'医院类别',
				className:'Shell.class.wfm.dict.CheckGrid',
				classConfig:{
					title:'医院类别选择',
					defaultWhere:"pdict.BDictType.Id='" + JShell.WFM.GUID.DictType.HospitalType.value + "'"
				}
			},{
				width:180,labelWidth:60,labelAlign:'right',emptyText:'医院等级',
				xtype:'uxCheckTrigger',itemId:'HospitalLevelName',fieldLabel:'医院等级',
				className:'Shell.class.wfm.dict.CheckGrid',
				classConfig:{
					title:'医院等级选择',
					defaultWhere:"pdict.BDictType.Id='" + JShell.WFM.GUID.DictType.HospitalLevel.value + "'"
				}
			},{
				xtype:'textfield',itemId:'HospitalTypeID',fieldLabel:'医院类别ID',hidden:true
			},{
				xtype:'textfield',itemId:'HospitalLevelID',fieldLabel:'医院等级ID',hidden:true
			},{
				width:55,boxLabel:'重复',itemId:'IsRepeat',
			    xtype:'checkbox',checked:true,style: {marginLeft: '20px' }
		    },{
				width:60,boxLabel:'不使用',itemId:'IsNotUser',
			    xtype:'checkbox',checked:false
		    },{
				width:70,boxLabel:'合约用户',itemId:'IsContract',
			    xtype:'checkbox',checked:false
		    }]
		};
		
	return items;
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = me.buttonToolbarItems || [];
		//查询框信息
		me.searchInfo = {
			width:160,emptyText:'客户名称',isLike:true,itemId:'search',
			fields:['pclient.Name']
		};
		buttonToolbarItems.unshift({
			xtype:'textfield',itemId:'LicenceID',width:170,labelWidth:60,emptyText:'授权编号',fieldLabel:'授权编号'
		},{
			xtype:'textfield',itemId:'UserNO',width:170,labelWidth:60,emptyText:'用户编号',fieldLabel:'用户编号'
		},{
		width:160,labelWidth:40,labelAlign:'right',
		xtype:'uxCheckTrigger',itemId:'ClientAreaName',fieldLabel:'区域',
		className:'Shell.class.wfm.dict.CheckGrid',emptyText:'区域',
			classConfig:{
				title:'地理区域选择',
				defaultWhere:"pdict.BDictType.Id='" + JShell.WFM.GUID.DictType.ClientArea.value + "'"
			}
		},{
			xtype:'textfield',itemId:'ClientAreaID',fieldLabel:'区域主键ID',hidden:true
		},{
			width:160,labelWidth:40,labelAlign:'right',emptyText:'省份',
			xtype:'uxCheckTrigger',itemId:'ProvinceName',fieldLabel:'省份',
			className:'Shell.class.sysbase.country.province.CheckApp'
		},{
			xtype:'textfield',itemId:'ProvinceID',fieldLabel:'省份主键ID',hidden:true
		},'-',{
			xtype:'trigger',
			triggerCls:'x-form-search-trigger',
			enableKeyEvents:true,
			itemId:'searchBman',
			width:120,emptyText:'业务员',
			onTriggerClick:function(){
				me.onSearch();
			},
			listeners:{
	            keyup:{
	                fn:function(field,e){
	                	if(e.getKey() == Ext.EventObject.ESC){
	                		me.onSearch();
	                	}else if(e.getKey() == Ext.EventObject.ENTER){
	                		me.onSearch();
	                	}
	                }
	            }
	        }
		},'->',{
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns: function() {
		var me = this;
		var columns = [{
			text:'区域',dataIndex:'PClient_ClientAreaName',width:70,
			sortable:false,defaultRenderer:true
		},
		{
			text:'医院名称',dataIndex:'PClient_Name',width:200,
			sortable:true,defaultRenderer:true
		},{
			text:'授权编码',dataIndex:'PClient_LicenceCode',width:110,
			sortable:false,defaultRenderer:true
		},{
			text:'用户编号',dataIndex:'PClient_ClientNo',width:110,
			sortable:true,defaultRenderer:true
		},{
			text:'客户类型',dataIndex:'PClient_ClientTypeName',width:70,
			sortable:false,defaultRenderer:true
		},{
			text:'医院类别',dataIndex:'PClient_HospitalTypeName',width:70,
			sortable:false,defaultRenderer:true
		},{
			text:'医院等级',dataIndex:'PClient_HospitalLevelName',width:70,
			sortable:false,defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'使用',dataIndex:'PClient_IsUse',
			width:35,align:'center',sortable:false,
			stopSelection:false,type:'boolean'
		},{
			text:'重复标记',dataIndex:'PClient_IsRepeat',
			width:55,	align: 'center',
			isBool: true,
			type: 'bool',
			sortable:false,
			defaultRenderer:true
		},{
			xtype:'checkcolumn',text:'合约用户',dataIndex:'PClient_IsContract',
			width:55,align:'center',sortable:false,
			stopSelection:false,type:'boolean'
		},{
			text:'主服务器授权号',dataIndex:'PClient_LRNo1',width:120,
			sortable:false,defaultRenderer:true
		},{
			text:'备份服务器授权号',dataIndex:'PClient_LRNo2',width:120,
			sortable:false,defaultRenderer:true
		},{
			text:'业务员',dataIndex:'PClient_Bman',width:70,
			sortable:false,defaultRenderer:true
		},{
			text:'客户主键ID',dataIndex:'PClient_Id',
			isKey:true,hidden:true,hideable:false
		}];
		
		return columns;
	},
	
	onAddClick:function(){
		var me = this;
		
		JShell.Win.open('Shell.class.wfm.client.Form',{
			resizable: false,
			formtype:'add',
			listeners:{
				save:function(p){
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	onEditClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection();

		if (records.length != 1) {
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
		
		me.openEditForm(records[0].get(me.PKField));
	},
	openEditForm:function(id){
		var me = this;
		JShell.Win.open('Shell.class.wfm.client.Form',{
			resizable: false,
			formtype:'edit',
			PK:id,
			listeners:{
				save:function(p){
					p.close();
					me.onSearch();
				}
			}
		}).show();
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this;
		me.internalWhere=me.getParms();
		return me.callParent(arguments);
	},
	getParms:function(){
		var me=this;
			var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			buttonsToolbar2 = me.getComponent('buttonsToolbar2'),		
		    ClientAreaID = null,
			ProvinceID = null,
			search = null,
			HospitalTypeID=null,
			ClientTypeID=null,
			HospitalLevelID=null,
			UserNO=null,
			LicenceID=null,
			IsRepeat=false,
			IsNotUser=false,
			IsContract=false,
			searchBman=null,
			params = [];
			
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
			ProvinceID = buttonsToolbar.getComponent('ProvinceID').getValue();
			ClientAreaID = buttonsToolbar.getComponent('ClientAreaID').getValue();
			UserNO = buttonsToolbar.getComponent('UserNO').getValue();
			LicenceID = buttonsToolbar.getComponent('LicenceID').getValue();
			searchBman=buttonsToolbar.getComponent('searchBman').getValue();
		}
		if(buttonsToolbar2){
			HospitalTypeID = buttonsToolbar2.getComponent('HospitalTypeID').getValue();
			ClientTypeID = buttonsToolbar2.getComponent('ClientTypeID').getValue();
			HospitalLevelID = buttonsToolbar2.getComponent('HospitalLevelID').getValue();
			IsRepeat=buttonsToolbar2.getComponent('IsRepeat').getValue();
		    IsNotUser=buttonsToolbar2.getComponent('IsNotUser').getValue();
		    IsContract=buttonsToolbar2.getComponent('IsContract').getValue();
		}
		//区域
		if(ClientAreaID){
			params.push("pclient.ClientAreaID='" + ClientAreaID + "'");
		}
		//省份
		if(ProvinceID){
			params.push("pclient.ProvinceID='" + ProvinceID + "'");
		}
		//医院类别
		if(HospitalTypeID){
			params.push("pclient.HospitalTypeID='" + HospitalTypeID + "'");
		}
		//客户类别
		if(ClientTypeID){
			params.push("pclient.ClientTypeID='" + ClientTypeID + "'");
		}
		//医院等级
		if(HospitalLevelID){
			params.push("pclient.HospitalLevelID='" + HospitalLevelID + "'");
		}
		//用户编号
		if(UserNO){
			params.push("pclient.ClientNo='" + UserNO + "'");
		}
		//授权编码
		if(LicenceID){
			params.push("pclient.LicenceCode='" + LicenceID + "'");
		}
		//重复(打钩表示包含重复标记的记录，否则不包含。默认为打钩)
		if(IsRepeat){
			params.push("(pclient.IsRepeat=1 or pclient.IsRepeat=0)");
			
		}else{
			params.push("pclient.IsRepeat=0");
			
		}
		//不使用(打钩表示包含不使用标记的记录，否则表示不包括。默认不包括)
		if(IsNotUser){
			params.push("(pclient.IsUse=0 or pclient.IsUse=1)");
		}else{
			params.push("pclient.IsUse=1");
		}
		//合约用户
		if(IsContract){
			params.push("pclient.IsContract=1");
		}else{
			params.push("(pclient.IsContract=0 or pclient.IsContract=1 or pclient.IsContract is null)");
		}
		if(searchBman){
			params.push("(pclient.Bman like '%" + searchBman + "%')");
		}
		if(params.length > 0){
			me.internalWhere = params.join(' and ');
		}else{
			me.internalWhere = '';
		}
		
		if(search){
			if(me.internalWhere){
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			}else{
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.internalWhere;
	},
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
		me.doProvinceListeners();
		me.doClientAreaListeners();
		//字典监听
		var dictList = [
			 'HospitalType','ClientType','HospitalLevel'
		];
		for(var i = 0; i < dictList.length; i++) {
			me.doDictListeners(dictList[i]);
		}	
		var buttonsToolbar2 = me.getComponent('buttonsToolbar2');
		var IsRepeat=buttonsToolbar2.getComponent('IsRepeat');
		var IsNotUser=buttonsToolbar2.getComponent('IsNotUser');
		IsRepeat.on({
			change:function(com,  newValue,  oldValue,  eOpts ){
				me.onSearch();
			}
		});
		IsNotUser.on({
			change:function(com,  newValue,  oldValue,  eOpts ){
				me.onSearch();
			}
		});
		var IsContract=buttonsToolbar2.getComponent('IsContract');
	    IsContract.on({
			change:function(com,  newValue,  oldValue,  eOpts ){
				me.onSearch();
			}
		});
		
	},
	/**字典监听*/
	doDictListeners: function(name) {
		var me = this;
		
		var buttonsToolbar = me.getComponent('buttonsToolbar2');
		var CName = buttonsToolbar.getComponent(name + 'Name');
		var Id = buttonsToolbar.getComponent(name + 'ID');
		if(!CName) return;
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('BDict_CName') : '');
				Id.setValue(record ? record.get('BDict_Id') : '');
				me.onSearch();
				p.close();
			}
		});
	},
		/**省份监听*/
	doProvinceListeners:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			CName = buttonsToolbar.getComponent('ProvinceName'),
			Id = buttonsToolbar.getComponent('ProvinceID');
			
		if(!CName) return;
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('BProvince_Name') : '');
				Id.setValue(record ? record.get('BProvince_Id') : '');
				me.onSearch();
				p.close();
			}
		});
	},
		/**区域监听*/
	doClientAreaListeners:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			CName = buttonsToolbar.getComponent('ClientAreaName'),
			Id = buttonsToolbar.getComponent('ClientAreaID');
			
		if(!CName) return;
		
		CName.on({
			check: function(p, record) {
				CName.setValue(record ? record.get('BDict_CName') : '');
				Id.setValue(record ? record.get('BDict_Id') : '');
				me.onSearch();
				p.close();
			}
		});
	},
	onSaveClick:function(){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
			
		if(len == 0) return;
			
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			var id = rec.get(me.PKField);
			var IsUse = rec.get(me.IsUseField);
			var IsContract = rec.get('PClient_IsContract');
			me.updateOneByIsUse(i,id,IsUse,IsContract);
		}
	},
	updateOneByIsUse:function(index,id,IsUse,IsContract){
		var me = this;
		var url = JShell.System.Path.getUrl(me.editUrl);
		
		//是否使用的类型不同处理
		if(me.IsUseType == 'int'){
			IsUse = IsUse ? "1" : "0";
		}
		var IsUseField = me.IsUseField.split('_').slice(-1) + '';
		
		var params = {};
		params.entity = {Id:id};
		params.entity[IsUseField] = IsUse;
		params.entity.IsContract = IsContract;
		params.fields = 'Id,' + IsUseField+',IsContract';
		
		setTimeout(function(){
			JShell.Server.post(url,Ext.JSON.encode(params),function(data){
				var record = me.store.findRecord(me.PKField,id);
				if(data.success){
					if(record){record.set(me.DelField,true);record.commit();}
					me.saveCount++;
				}else{
					me.saveErrorCount++;
					if(record){record.set(me.DelField,false);record.commit();}
				}
				if(me.saveCount + me.saveErrorCount == me.saveLength){
					me.hideMask();//隐藏遮罩层
					if(me.saveErrorCount == 0) me.onSearch();
				}
			});
		},100 * index);
	},
	/**导出用户信息*/
	onDownExcel:function(){
		var me=this;
		var where =me.getParms();
		var url = JShell.System.Path.getRootUrl(me.downlUrl);
		url += '?operateType=0&type=PClient';
		if(where){
			url += '&where='+where;
		}
		window.open(url);
	}
});