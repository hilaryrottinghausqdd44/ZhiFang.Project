/**
 * 模块操作功能
 * @author liangyl	
 * @version 2019-11-18
 */
Ext.define('Shell.class.sysbase.module.OperGrid', {
    extend: 'Shell.ux.grid.Panel',
    title: '模块操作功能',
    requires: ['Ext.ux.CheckColumn'],

    width: 780,
    height: 440,
   /**获取数据服务路径*/
	selectUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL?isPlanish=true',
	/**修改服务地址*/
	editUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_UpdateRBACModuleOperByField',
	/**删除数据服务路径*/
	delUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_DelRBACModuleOper',

	addUrl:'/ServerWCF/RBACService.svc/RBAC_UDTO_AddRBACModuleOper',
    /**默认加载*/
    defaultLoad: true,    
    /**是否启用刷新按钮*/
	hasRefresh:true,
	/**是否启用新增按钮*/
	hasAdd:true,
	/**是否启用修改按钮*/
	hasEdit:false,
	/**是否启用删除按钮*/
	hasDel:true,
	/**是否启用保存按钮*/
	hasSave:true,
	/**是否启用查询框*/
	hasSearch:false,
	/**是否启用序号列*/
	hasRownumberer: false,
	/**默认每页数量*/
	defaultPageSize: 200,
	/**带分页栏*/
	hasPagingtoolbar: false,
    defaultOrderBy: [{ property: 'RBACModuleOper_DispOrder', direction: 'ASC' }],
    
   	/**模块ID*/
    RBACModuleID:null,
    
	/**复选框*/
	multiSelect: true,
	selType: 'checkboxmodel',
	
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
		me.on({
			nodata:function(){
				me.getView().update('');
				me.store.removeAll();
			},
			beforeclose : function ( panel,eOpts ){
				var edit = panel.getPlugin('NewsGridEditing');  
                edit.cancelEdit();  
			}
		});
    },
    
    initComponent: function () {
        var me = this;
        //数据列
        me.columns = me.createGridColumns();
        me.plugins = Ext.create('Ext.grid.plugin.CellEditing', {
			clicksToEdit: 1,
			pluginId:'NewsGridEditing'
		});
        me.callParent(arguments);
    },
    /**创建数据列*/
    createGridColumns: function () {
        var me = this;
		var columns = 
	    [{text: '主键ID',dataIndex: 'RBACModuleOper_Id',isKey: true,hidden: true,sortable: false, defaultRenderer: true},
        { menuDisabled: false, text: '用户代码', dataIndex: 'RBACModuleOper_UseCode',sortable: false,
			width:100,hidden: true,defaultRenderer: true
        },{
            menuDisabled: false, text: '标准代码', dataIndex: 'RBACModuleOper_StandCode',sortable: false,
			width:100,editor: {xtype: 'textfield',allowBlank: false,
			listeners:{
			    	change:function(file,value,eOpt){
			    		var records = me.getSelectionModel().getSelection();
						if (records.length == 0) {
							JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
							return;
						}
						records[0].set('RBACModuleOper_StandCode',value);
                        me.getView().refresh();
			    	}
		    }},
			defaultRenderer: true
        },{
            menuDisabled: false, text: '开发商代码', dataIndex: 'RBACModuleOper_DeveCode',sortable: false,
			width:100,hidden: true,defaultRenderer: true
        },{
            menuDisabled: false, text: '模块操作名称', dataIndex: 'RBACModuleOper_CName',sortable: false,
			width:150,
			editor: {xtype: 'textfield',allowBlank: false,
			listeners:{
			    	change:function(file,value,eOpt){
			    		var records = me.getSelectionModel().getSelection();
						if (records.length == 0) {
							JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
							return;
						}
						records[0].set('RBACModuleOper_CName',value);
                        me.getView().refresh();
			    	}
		    }},defaultRenderer: true
        },{
            menuDisabled: false, text: '模块操作描述', dataIndex: 'RBACModuleOper_Comment',sortable: false,
			width:150,
			editor: {xtype: 'textfield',allowBlank: false},defaultRenderer: true
        },{
			xtype:'checkcolumn',text:'是否使用',dataIndex:'RBACModuleOper_IsUse',
			width:70,align:'center',sortable:false,menuDisabled:true,
			stopSelection:false,type:'boolean'
		},{
            menuDisabled: false, text: '显示次序', dataIndex: 'RBACModuleOper_DispOrder',sortable: false,
			width:70,
			editor: {xtype:'numberfield',value:0,allowBlank: false},defaultRenderer: true
	    }, {
			dataIndex: 'ErrorInfo',text: '错误信息',hidden: true,hideable: false,sortable: false,menuDisabled: true
		}];
        return columns;
   },
    /**获取带查询参数的URL*/
	getLoadUrl: function() {
		var me = this,
			search = null,
			params = [];
		//模块Id 
		if(me.RBACModuleID) {
			params.push("rbacmoduleoper.RBACModule.Id=" + me.RBACModuleID + "");
		}
		if(params.length > 0) {
			me.internalWhere = params.join(' and ');
		} else {
			me.internalWhere = '';
		}
		return me.callParent(arguments);
	},	
    /**增加一行*/
	createAddRec: function(num) {
		var me = this;
		var obj = {
			RBACModuleOper_Id:'',
			RBACModuleOper_UseCode:'',
			RBACModuleOper_StandCode:'',
			RBACModuleOper_DeveCode:'',
			RBACModuleOper_CName:'',
			RBACModuleOper_Comment:'',
			RBACModuleOper_IsUse:true,
			RBACModuleOper_DispOrder:num+1
		};
		me.store.insert(num, obj);
	},
	/**@overwrite 新增按钮点击处理方法*/
	onAddClick:function(){
		var me = this;
		me.createAddRec(me.getStore().getCount());
	},
	/**删除按钮点击处理方法*/
	onDelClick: function() {
		var me = this,
			records = me.getSelectionModel().getSelection();
		
		if (records.length == 0) {
			JShell.Msg.error(JShell.All.CHECK_MORE_RECORD);
			return;
		}
		JShell.Msg.del(function(but) {
			if (but != "ok") return;

			me.delErrorCount = 0;
			me.delCount = 0;
			me.delLength = records.length;

			me.showMask(me.delText); //显示遮罩层
			
			for (var i in records) {
				var id = records[i].get(me.PKField);
				if(!id){
					if (records[i]) {
						records[i].set(me.DelField, true);
						me.store.remove(records[i]);
						records[i].commit();
					}
					me.delCount++;
					if (me.delCount + me.delErrorCount == me.delLength) {
						me.hideMask(); //隐藏遮罩层
						if (me.delErrorCount == 0){
//							me.onSearch();
						}else{
							JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
						}
					}
				}else{
					me.delOneById(i, id,records[i]);
				}
			}
		});
	},
	/**删除一条数据*/
	delOneById: function(index, id) {
		var me = this;
		var url = (me.delUrl.slice(0, 4) == 'http' ? '' : JShell.System.Path.ROOT) + me.delUrl;
		url += (url.indexOf('?') == -1 ? '?' : '&') + 'id=' + id;

		setTimeout(function() {
			JShell.Server.get(url, function(data) {
				var record = me.store.findRecord(me.PKField, id);
				if (data.success) {
					if (record) {
						record.set(me.DelField, true);
						me.store.remove(record);
						record.commit();
					}
					me.delCount++;
				} else {
					me.delErrorCount++;
					if (record) {
						record.set(me.DelField, false);
						record.set('ErrorInfo', data.msg);
						record.commit();
					}
				}
				if (me.delCount + me.delErrorCount == me.delLength) {
					me.hideMask(); //隐藏遮罩层
					if (me.delErrorCount == 0){
//						me.onSearch();
					}else{
						JShell.Msg.error('存在失败信息，具体错误内容请查看数据行的失败提示！');
					}
				}
			});
		}, 100 * index);
	},
	/**保存勾选数据*/
	onSaveClick:function(){
		var me = this,
			records=me.store.getModifiedRecords(),//获取修改过的行记录
			len = records.length;
			
		if(len == 0) return;
		var IsValid = me.IsValid();
		if(!IsValid)return;
		me.showMask(me.saveText);//显示遮罩层
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = len;
		
		for(var i=0;i<len;i++){
			var rec = records[i];
			var id = rec.get(me.PKField);
	
			if(id){//修改
				me.updateOne(rec,id);
			}else{
				//新增
				me.addOne(rec,id);
			}
		}
	},
	
	getEntity : function(rec,id){
		var me = this;
		var entity = {
			StandCode: rec.get('RBACModuleOper_StandCode'),
            CName: rec.get('RBACModuleOper_CName'),
			Comment: rec.get('RBACModuleOper_Comment'),
            IsUse: rec.get('RBACModuleOper_IsUse') ? 1 : 0
		};
		if(me.RBACModuleID)entity.RBACModule = {Id:me.RBACModuleID,DataTimeStamp:[0,0,0,0,0,0,0,0]};
		if(id)entity.Id = rec.get('RBACModuleOper_Id');
		if(rec.get('RBACModuleOper_DispOrder'))entity.DispOrder=rec.get('RBACModuleOper_DispOrder');
		return entity;
	},
	/**添加一条关系*/
	addOne:function(rec,id){
		var me = this;
		var url = JShell.System.Path.ROOT + me.addUrl;
		var entity = me.getEntity(rec,id);
		JShell.Server.post(url,Ext.JSON.encode({entity:entity}),function(data){
			if(data.success){
				me.saveCount++;
			
				rec.set(me.PKField, data.value.id || '');
				rec.commit();
			}else{
				me.saveErrorCount++;
			}
			me.onSaveEnd();
		});
	},
	/**更新一条数据*/
	updateOne:function(rec,id){
		var me = this;
		var url = JShell.System.Path.ROOT + me.editUrl;
		var entity = me.getEntity(rec,id);
		var fields ='Id,StandCode,CName,Comment,IsUse';
		JShell.Server.post(url,Ext.JSON.encode({entity:entity,fields:fields}),function(data){
			if(data.success){
				me.saveCount++;
				rec.commit();
			}else{
				me.saveErrorCount++;
			}
			me.onSaveEnd();
		});
	},
	onSaveEnd:function(){
		var me = this;
		if (me.saveCount + me.saveErrorCount == me.saveLength) {
			me.hideMask(); //隐藏遮罩层
			if (me.saveErrorCount == 0){
				JShell.Msg.alert(JShell.All.SUCCESS_TEXT,null,1000);
				me.onSearch();
			}else{
				JShell.Msg.error('存在失败信息，请重新保存！');
			}
		}
	},
	/**数据校验*/
	IsValid: function() {
		var me = this,
			records = me.store.getModifiedRecords(),
			len = records.length,
			isExec = true;
		
		var items = me.store.data.items,
		    itemsLen = items.length,
		    isExist=true;

		for(var i=0;i<len;i++){
			var Name =records[i].get('RBACModuleOper_CName');
			var StandCode =records[i].get('RBACModuleOper_StandCode');

			if(Name)Name.replace(/^\s\s*/, '' ).replace(/\s\s*$/, '' );
			if(StandCode)StandCode.replace(/^\s\s*/, '' ).replace(/\s\s*$/, '' );
			
			if(!Name) {
				JShell.Msg.error('模块操作名称不能为空!');
				isExec = false;
				return;
			}
			var str = Name+StandCode;
			if(!str){
				isExist = false;
				return;
			} 
			var  ExistNum =0;
			for(var j=0;j<itemsLen;j++){
				var Name2 =items[j].data.RBACModuleOper_CName;
			    var StandCode2 =items[j].data.RBACModuleOper_StandCode;
			    if(Name2)Name2.replace(/^\s\s*/, '' ).replace(/\s\s*$/, '' );
			    if(StandCode2)StandCode2.replace(/^\s\s*/, '' ).replace(/\s\s*$/, '' );
                var str2 = Name2+StandCode2;       
		    	if(str2 ==str){
		    		ExistNum+=1;
		    	}
		    }
			if(ExistNum >1){
				JShell.Msg.error('已存在相同的功能权限,不能保存!');
				isExist=false;
		        return;
		    }
		}	
	      
		return isExec;
	}
});