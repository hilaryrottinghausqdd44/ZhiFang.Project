/**
 * 货位货架
 * @author liangyl	
 * @version 2017-11-08
 */
Ext.define('Shell.class.rea.client.shelves.place.Grid',{
    extend:'Shell.ux.grid.Panel',
    requires: [
		'Shell.ux.form.field.BoolComboBox'
	],
    title:'货架列表',
    width:800,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaPlaceByHQL?isPlanish=true',
    /**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaPlace',
	 /**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaPlaceByField',
   /**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaPlace',
	
    /**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:true,
	/**是否启用修改按钮*/
	hasEdit:true,
	/**是否启用删除按钮*/
	hasDel:true,
	/**是否启用查询框*/
	hasSearch:true,
	
	/**默认加载数据*/
	defaultLoad:false,
	/**排序字段*/
	defaultOrderBy:[{property:'ReaPlace_DispOrder',direction:'ASC'}],
	
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
	/**库房id*/
	ReaStorageID:null,
	/**库房 名称*/
	ReaStorageCName:null,
    /**默认每页数量*/
	defaultPageSize: 200,
	/**带分页栏*/
	hasPagingtoolbar: false,
	/**原始行数*/
	oldCount: 0,
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
	initComponent:function(){
		var me = this;
		
		//查询框信息
		me.searchInfo = {
			width:135,emptyText:'货位名称/代码',isLike:true,itemId:'Search',
			fields:['reaplace.CName','reaplace.ShortCode']
		};		
		me.buttonToolbarItems = ['refresh','add',{
			text:'新增空行',tooltip:'新增空行',iconCls:'button-add',
			handler:function(){
				me.onAddRec();
			}
		},'edit','del','save','->',{
			type: 'search',
			info: me.searchInfo
		}];
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'ReaPlace_CName',
			text: '货架名称',
			width: 120,editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaPlace_ShortCode',
			text: '代码',width: 80,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaPlace_Memo',
			text: '描述',
			width: 120,
			editor:{},
			renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta);
				return v;
			}
		},{
			dataIndex: 'ReaPlace_DispOrder',
			text: '次序',
			width: 50,
			align:'center',
			type:'int',	
			editor:{xtype:'numberfield'},
			defaultRenderer: true
		},{
			dataIndex: 'ReaPlace_Visible',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true},
			defaultRenderer: true
		},{
			dataIndex: 'Tab',text: '标记',hidden:true,
			width: 50,hideable: false,defaultRenderer: true
		},{
			dataIndex: 'ReaPlace_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		this.showForm();
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection();
			
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
        me.showForm(records[0].get(me.PKField));
	},
	
	/**初始化检索监听*/
	initFilterListeners: function() {
		var me = this;
	},
	/**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			Search = buttonsToolbar.getComponent('Search').getValue(),
			params = [];
        if(me.OrgType!=null && me.OrgType!=''){
        	params.push('ReaPlace.OrgType=' + me.OrgType);
        }
		
		if (Search) {
			params.push('(' + me.getSearchWhere(Search) + ')');
		}

		if (params.length > 0) {
			me.internalWhere = params.join(' and ');
		}

		return me.callParent(arguments);
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
			if(i == 'ReaPlace.OrgNo'){
				if(!isNaN(value)){
					where.push("ReaPlace.OrgNo=" + value);
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
					load:function(){
						var edit = me.getPlugin('NewsGridEditing'); 
		                edit.cancelEdit();
					}
				}
			};
        
		if (id) {
			config.formtype = 'edit';
			
			config.PK = id;
		} else {
			config.formtype = 'add';
				/**库房id*/
			config.ReaStorageID=me.ReaStorageID;
			/**库房 名称*/
			config.ReaStorageCName=me.ReaStorageCName;

		}

		JShell.Win.open('Shell.class.rea.client.shelves.place.Form', config).show();
	},
	/**验证*/
	IsValidate:function(){
		var me=this;
		var changedRecords = me.getStore().getModifiedRecords(),//获取修改过的行记录
			len = changedRecords.length;
        var  isExect=true;
		//验证库房名称不能为空
		for(var i=0;i<len;i++){
			if(!changedRecords[i].get('ReaPlace_CName')){
				isExect=false;
			}
		}
		return isExect;
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
		var IsValidate=me.IsValidate();	
		if(!IsValidate){
			JShell.Msg.error("货架名称不能为空！");
			return;
		}
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			//新增
			if(changedRecords[i].get('Tab')=='1'){
				me.oneAdd(i,changedRecords[i]);
			}else{
				me.updateOne(i,changedRecords[i]);
			}
		}
	},
	/**行新增信息*/
	oneAdd:function(i,record){
		var me = this;
		var url = (me.addUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.addUrl;
		var entity={
			CName:record.get('ReaPlace_CName'),
			ShortCode:record.get('ReaPlace_ShortCode'),
			Visible:record.get('ReaPlace_Visible')? 1 : 0,
			DispOrder:record.get('ReaPlace_DispOrder'),
			Memo:record.get('ReaPlace_Memo')
		};
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId){
			entity.CreaterID=userId;
		    entity.CreaterName = userName;
		}
		if(me.ReaStorageID){
			entity.ReaStorage = {
				Id:me.ReaStorageID,
				DataTimeStamp:[0,0,0,0,0,0,0,0]
			};
		}
		var params = Ext.JSON.encode({
			entity:entity
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
	/**修改信息*/
	updateOne:function(i,record){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var params = Ext.JSON.encode({
			entity:{
				Id:record.get('ReaPlace_Id'),
				CName:record.get('ReaPlace_CName'),
				ShortCode:record.get('ReaPlace_ShortCode'),
				Visible:record.get('ReaPlace_Visible')? 1 : 0,
				DispOrder:record.get('ReaPlace_DispOrder'),
				Memo:record.get('ReaPlace_Memo')
			},
			fields:'Id,CName,ShortCode,Visible,DispOrder,Memo'
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
		if(v.length > 0)v = (v.length > 32 ? v.substring(0, 32) : v);
		if(value.length>32){
			v= v+"...";
		}
        var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
        meta.tdAttr = 'data-qtip="' + qtipValue + '"';
        return v
	},
	/**根据库房id查询*/
	loadDataById:function(id){
		var me=this;
		me.defaultWhere = 'reaplace.ReaStorage.Id=' + id;
		me.onSearch();
	},
	/**新增空行，最多有5个空行*/
	onAddRec:function(){
		var  me=this;
		var count=me.getStore().getCount();
		//未保存前只能新增5行
		var sum=me.oldCount + 5;
		var obj={
			ReaPlace_CName:'',
			ReaPlace_ShortCode:'',
			ReaPlace_Memo:'',
			ReaPlace_DispOrder:'',
			Tab:'1',
			ReaPlace_Visible: 1
		} 
	   if(count<sum){
	   	  me.store.insert(count, obj);
	   }
	},
	/**@overwrite 改变返回的数据*/
	changeResult: function(data) {
		var me=this;
		me.oldCount=data.count;
		return data;
	}
});