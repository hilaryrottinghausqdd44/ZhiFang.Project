/**
 * 订货方/供货方列表
 * @author liangyl	
 * @version 2017-09-08
 */
Ext.define('Shell.class.rea.client.cenorg.basic.Grid',{
    extend:'Shell.ux.grid.Panel',
    requires: [
		'Shell.ux.form.field.BoolComboBox'
	],
    title:'订货方/供货方列表',
    width:800,
    height:500,
    
    /**获取数据服务路径*/
    selectUrl:'/ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?isPlanish=true',
    /**删除数据服务路径*/
	delUrl: '/ReaSysManageService.svc/ST_UDTO_DelReaCenOrg',
	 /**修改服务地址*/
    editUrl:'/ReaSysManageService.svc/ST_UDTO_UpdateReaCenOrgByField',
   
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
	defaultLoad:true,
	/**排序字段*/
	defaultOrderBy:[{property:'ReaCenOrg_OrgNo',direction:'ASC'}],
	
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	hasDel: true,
    /**机构类型 0供货方，1订货方*/
	OrgType:'0',
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
			width:180,emptyText:'中文名/英文名/代码/机构编号',isLike:true,itemId:'Search',
			fields:['reacenorg.CName','reacenorg.EName','reacenorg.ShortCode','reacenorg.OrgNo']
		};		
		me.buttonToolbarItems = ['refresh','add','edit','del','save','->',{
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
			dataIndex: 'ReaCenOrg_OrgNo',
			text: '机构编号',
			width: 60,
			type:'int',
			defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_PlatformOrgNo',
			text: '平台机构编号',width: 80,
			type:'int',editor:{xtype:'numberfield'},
			defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_CName',
			text: '机构名称',
			width: 100,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_EName',
			text: '英文名称',
			width: 100,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_Memo',
			text: '备注',
			width: 100,
			editor:{},
			renderer: function(value, meta, record) {
            	var v=me.showMemoText(value, meta);
				return v;
			}
		},{
			dataIndex: 'ReaCenOrg_DispOrder',
			text: '次序',
			width: 50,
			align:'center',
			type:'int',	
			editor:{xtype:'numberfield'},
			defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_Visible',
			text: '启用',
			width: 50,
			align:'center',
			type:'bool',
			isBool:true,
			editor:{xtype:'uxBoolComboBox',value:true,hasStyle:true},
			defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_Address',
			text: '机构地址',
			width: 100,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_Contact',
			text: '联系人',
			width: 70,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_Tel',
			text: '电话',
			width: 100,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_Fox',
			text: '传真',
			width: 100,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_Email',
			text: '邮箱',
			width: 100,
			editor:{},
			defaultRenderer: true
		},{
			dataIndex: 'ReaCenOrg_WebAddress',
			text: '网址',
			width: 100,
			editor:{},
			defaultRenderer: true
		},
		{
			dataIndex: 'ReaCenOrg_Id',
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
//		this.fireEvent('addclick');
	},
	/**@overwrite 修改按钮点击处理方法*/
	onEditClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection();
			
		if(records.length != 1){
			JShell.Msg.error(JShell.All.CHECK_ONE_RECORD);
			return;
		}
//		me.fireEvent('editclick',me,records[0].get(me.PKField));
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
        	params.push('reacenorg.OrgType=' + me.OrgType);
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
			if(i == 'reacenorg.OrgNo'){
				if(!isNaN(value)){
					where.push("reacenorg.OrgNo=" + value);
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
				//机构类型
				OrgType:me.OrgType,
				listeners: {
					save: function(p, records) {
						p.close();
						me.onSearch();
					},
					beforerender: function(p,  eOpts ){
//						var edit = me.getPlugin('NewsGridEditing'); 
////		                edit.completeEdit();
//		                edit.cancelEdit();
					},
					load:function(){
						var edit = me.getPlugin('NewsGridEditing'); 
//		                edit.completeEdit();
		                edit.cancelEdit();
					}
				}
			};
        if(me.OrgType=='1'){
        	config.title='订货方信息';
        }else{
        	config.title='供货方信息';
        }
		if (id) {
			config.formtype = 'edit';
			
			config.PK = id;
		} else {
			config.formtype = 'add';
		}

		JShell.Win.open('Shell.class.rea.client.cenorg.basic.Form', config).show();
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
				Id:record.get('ReaCenOrg_Id'),
				PlatformOrgNo:record.get('ReaCenOrg_PlatformOrgNo'),
				CName:record.get('ReaCenOrg_CName'),
				EName:record.get('ReaCenOrg_EName'),
				Memo:record.get('ReaCenOrg_Memo'),
				DispOrder:record.get('ReaCenOrg_DispOrder'),
				Visible:record.get('ReaCenOrg_Visible')? 1 : 0,
				Address:record.get('ReaCenOrg_Address'),
				Contact:record.get('ReaCenOrg_Contact'),
				Tel:record.get('ReaCenOrg_Tel'),
				Fox:record.get('ReaCenOrg_Fox'),
				Email:record.get('ReaCenOrg_Email'),
				WebAddress:record.get('ReaCenOrg_WebAddress')
			},
			fields:'Id,PlatformOrgNo,CName,EName,Memo,DispOrder,Visible,Address,Contact,Tel,Fox,Email,WebAddress'
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
		if(v.length > 0)v = (v.length > 20 ? v.substring(0, 20) : v);
		if(value.length>20){
			v= v+"...";
		}
        var qtipValue = "<p border=0 style='vertical-align:top;font-size:12px; word-break:break-all;'>" + value + "</p>";
        meta.tdAttr = 'data-qtip="' + qtipValue + '"';
        return v
	}
});