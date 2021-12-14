/**
 * 职责列表
 * @author liangyl
 * @version 2017-11-23
 */
Ext.define('Shell.class.qms.equip.res.manage.Grid', {
	extend: 'Shell.class.qms.equip.res.manage.basic.Grid',
	title: '职责列表',
	requires: [
		'Shell.ux.form.field.CheckTrigger',
		'Shell.ux.form.field.BoolComboBox'
	],
	/**是否启用新增按钮*/
	hasAdd: true,
	/**是否启用修改按钮*/
	hasEdit: true,
	/**是否启用删除按钮*/
	hasDel: true,
	/**是否启用查看按钮*/
	hasShow: false,
	/**是否启用保存按钮*/
	hasSave: true,
	/**是否启用查询框*/
	hasSearch: true,
	
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
		
		
		me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
		//数据列
		me.columns = me.createGridColumns();
		me.callParent(arguments);
	},
	/**创建功能按钮栏Items*/
	createButtonToolbarItems:function(){
		var me = this,
			buttonToolbarItems = [];
		//查询框信息
        me.searchInfo = {
			width:135,emptyText:'职责名称/快捷码',isLike:true,itemId:'search',
			fields:['eresponsibility.CName','eresponsibility.Shortcode']
		};
		buttonToolbarItems.push('refresh','add','edit','del','save');
		
		buttonToolbarItems.push('->',{
			type: 'search',
			info: me.searchInfo
		});
		return buttonToolbarItems;
	},
	/**创建数据列*/
	createGridColumns:function(){
		var me = this;
		  
		var columns = [{
			dataIndex: 'EResponsibility_CName',
			text: '职责名称',
			width: 180,editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'EResponsibility_EName',
			text: '英文名称',
			width: 100,editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'EResponsibility_SName',
			text: '简称',
			width: 100,editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'EResponsibility_Shortcode',
			text: '快捷码',width: 100,
			editor:{},sortable:true,
			defaultRenderer: true
		},{
			dataIndex: 'EResponsibility_PinYinZiTou',
			text: '拼音字头',width: 100,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'EResponsibility_Comment',text: '描述',width: 200,
			editor:{},sortable:false,
			renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta);
				return v;
			}
		},{
			dataIndex: 'EResponsibility_DispOrder',
			text: '次序',
			width: 70,
			align:'center',
			editor:{xtype:'numberfield'},
			defaultRenderer: true
		},{
			dataIndex: 'EResponsibility_IsUse',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true},
			defaultRenderer: true
		},{
			dataIndex: 'EResponsibility_Id',text: '主键ID',
			hidden: true,hideable: false,isKey: true
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
				 maximizable: false, //是否带最大化功能
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

		JShell.Win.open('Shell.class.qms.equip.res.manage.Form', config).show();
	},
	changeDefaultWhere:function(){
		
	},
	  /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			search = null,params = [];
			
		//改变默认条件
		me.changeDefaultWhere();
			
		me.internalWhere = '';
			
		if(buttonsToolbar){
			search = buttonsToolbar.getComponent('search').getValue();
		}
		
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		if(search) {
			if(me.internalWhere) {
				me.internalWhere += ' and (' + me.getSearchWhere(search) + ')';
			} else {
				me.internalWhere = me.getSearchWhere(search);
			}
		}
		return me.callParent(arguments);
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
				Id:record.get('EResponsibility_Id'),
				CName:record.get('EResponsibility_CName'),
				Shortcode:record.get('EResponsibility_Shortcode'),
				EName:record.get('EResponsibility_EName'),
				SName:record.get('EResponsibility_SName'),
				PinYinZiTou:record.get('EResponsibility_PinYinZiTou'),
				IsUse:record.get('EResponsibility_IsUse')? 1 : 0,
				DispOrder:record.get('EResponsibility_DispOrder'),
				Comment:record.get('EResponsibility_Comment')
			},
			fields:'Id,CName,Shortcode,EName,SName,PinYinZiTou,IsUse,DispOrder,Comment'
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