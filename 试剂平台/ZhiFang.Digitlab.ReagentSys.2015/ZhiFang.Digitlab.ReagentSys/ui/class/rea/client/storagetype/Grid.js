/**
 * 入库类型维护
 * @author liangyl	
 * @version 2017-11-08
 */
Ext.define('Shell.class.rea.client.storagetype.Grid',{
    extend:'Shell.ux.grid.Panel',
    requires: [
		'Shell.ux.form.field.BoolComboBox'
	],
    title:'入库类型维护',
    width:800,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/SingleTableService.svc/ST_UDTO_SearchBStorageTypeByHQL?isPlanish=true',
    /**删除数据服务路径*/
	delUrl: '/SingleTableService.svc/ST_UDTO_DelBStorageType',
	 /**修改服务地址*/
    editUrl:'/SingleTableService.svc/ST_UDTO_UpdateBStorageTypeByField',
   
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
	/**是否启用保存按钮*/
	hasSave: true,
	
	/**默认加载数据*/
	defaultLoad:true,
	/**排序字段*/
	defaultOrderBy:[{property:'BStorageType_Name',direction:'ASC'}],
	
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,

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
			width:135,emptyText:'类型名称/快捷码',isLike:true,itemId:'Search',
			fields:['bstoragetype.Name','bstoragetype.Shortcode']
		};	
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
			dataIndex: 'BStorageType_Name',
			text: '类型名称',
			width: 150,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'BStorageType_SName',
			text: '简称',
			width: 150,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'BStorageType_Shortcode',
			text: '快捷码',width: 100,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'BStorageType_Comment',
			text: '描述',
			width: 180,
			editor:{},
			renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta);
				return v;
			}
		},{
			dataIndex: 'BStorageType_IsUse',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true},
			defaultRenderer: true
		},
		{
			dataIndex: 'BStorageType_Id',
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

		JShell.Win.open('Shell.class.rea.client.storagetype.Form', config).show();
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
			me.updateOne(i,changedRecords[i]);
		}
	},/**修改信息*/
	updateOne:function(i,record){
		var me = this;
		var url = (me.editUrl.slice(0,4) == 'http' ? '' : JShell.System.Path.ROOT) + me.editUrl;
		
		var params = Ext.JSON.encode({
			entity:{
				Id:record.get('BStorageType_Id'),
				Name:record.get('BStorageType_Name'),
				Shortcode:record.get('BStorageType_Shortcode'),
				SName:record.get('BStorageType_SName'),
				PinYinZiTou:record.get('BStorageType_PinYinZiTou'),
				Comment:record.get('BStorageType_Comment'),
				IsUse:record.get('BStorageType_IsUse')? 1 : 0
			},
			fields:'Id,Name,Shortcode,SName,PinYinZiTou,Comment,IsUse'
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
	}
});