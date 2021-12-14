/**
 * 存储库房表
 * @author liangyl	
 * @version 2017-11-08
 */
Ext.define('Shell.class.rea.client.shelves.storage.Grid',{
    extend:'Shell.ux.grid.Panel',
    requires: [
		'Shell.ux.form.field.BoolComboBox'
	],
    title:'存储库房维护',
    width:800,
    height:500,
    /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaStorageByHQL?isPlanish=true',
    /**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaStorage',
	 /**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaStorageByField',
   /**新增服务地址*/
	addUrl: '/ReaSysManageService.svc/ST_UDTO_AddReaStorage',
   /**是否启用刷新按钮*/
	hasRefresh:true,
	/**默认加载数据*/
	defaultLoad:true,
	/**排序字段*/
	defaultOrderBy:[{property:'ReaStorage_DispOrder',direction:'ASC'}],
	
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
	/**原始行数*/
	oldCount: 0,
	   /**默认每页数量*/
	defaultPageSize: 200,
	/**带分页栏*/
	hasPagingtoolbar: false,
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		Ext.override(Ext.ToolTip, {
			maxWidth: 350
		});
		//初始化检索监听
		me.initFilterListeners();
		
		me.on({
			nodata:function(){
				me.getView().update('');
			},
			itemdblclick: function(view, record) {
				me.showForm(record.get(me.PKField));
			}
		});
	},

	initComponent:function(){
		var me = this;
	    //查询框信息
		me.searchInfo = {
			width:135,emptyText:'库房名称/代码',isLike:true,itemId:'Search',
			fields:['reastorage.CName','reastorage.ShortCode']
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
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		this.showForm();
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'ReaStorage_CName',
			text: '库房名称',
			width: 120,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaStorage_ShortCode',
			text: '代码',width: 100,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaStorage_Memo',
			text: '描述',width: 120,
			editor:{},
			renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta);
				return v;
			}
		},{
			dataIndex: 'ReaStorage_DispOrder',
			text: '次序',
			width: 50,
			align:'center',
			type:'int',	
			editor:{xtype:'numberfield'},
			defaultRenderer: true
		},{
			dataIndex: 'Tab',text: '标记',hidden:true,
			width: 50,hideable: false,defaultRenderer: true
		},{
			dataIndex: 'ReaStorage_Visible',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true},
			defaultRenderer: true
		},
		{
			dataIndex: 'ReaStorage_Id',
			text: '主键ID',
			hidden: true,
			hideable: false,
			isKey: true
		}];
		
		return columns;
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
		}

		JShell.Win.open('Shell.class.rea.client.shelves.storage.Form', config).show();
	},
	/**验证*/
	IsValidate:function(){
		var me=this;
		var changedRecords = me.getStore().getModifiedRecords(),//获取修改过的行记录
			len = changedRecords.length;
        var  isExect=true;
		//验证库房名称不能为空
		for(var i=0;i<len;i++){
			if(!changedRecords[i].get('ReaStorage_CName')){
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
			JShell.Msg.error("库房名称不能为空！");
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
			CName:record.get('ReaStorage_CName'),
			ShortCode:record.get('ReaStorage_ShortCode'),
			Visible:record.get('ReaStorage_Visible')? 1 : 0,
			DispOrder:record.get('ReaStorage_DispOrder'),
			Memo:record.get('ReaStorage_Memo')
		};
		var userId = JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		var userName = JShell.System.Cookie.get(JShell.System.Cookie.map.USERNAME);
		if(userId){
			entity.CreaterID=userId;
		    entity.CreaterName = userName;
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
				Id:record.get('ReaStorage_Id'),
				CName:record.get('ReaStorage_CName'),
				ShortCode:record.get('ReaStorage_ShortCode'),
				Visible:record.get('ReaStorage_Visible')? 1 : 0,
				DispOrder:record.get('ReaStorage_DispOrder'),
				Memo:record.get('ReaStorage_Memo')
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
		if(v.length > 0)v = (v.length > 30 ? v.substring(0, 32) : v);
		if(value.length>30){
			v= v+"...";
		}
        var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
        meta.tdAttr = 'data-qtip="' + qtipValue + '"';
        return v
	},
	/**新增空行，最多有5个空行*/
	onAddRec:function(){
		var  me=this;
		var count=me.getStore().getCount();
		//未保存前只能新增5行
		var sum=me.oldCount + 5;
		var obj={
			ReaStorage_CName:'',
			ReaStorage_ShortCode:'',
			ReaStorage_Memo:'',
			ReaStorage_DispOrder:'',
			Tab:'1',
			ReaStorage_Visible: 1
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