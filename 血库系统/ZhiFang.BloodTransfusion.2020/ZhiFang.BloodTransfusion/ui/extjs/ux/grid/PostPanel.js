/**
 * POST请求列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.grid.PostPanel',{
    extend:'Shell.ux.grid.Panel',
    alias:'widget.uxGridPostPanel',
    
    /**请求参数*/
    postParams:{},
    
    /**创建数据集*/
	createStore:function(){
		var me = this;
		return Ext.create('Ext.data.Store',{
			fields:me.getStoreFields(),
			pageSize:me.defaultPageSize,
			remoteSort:me.remoteSort,
			sorters:me.defaultOrderBy,
			proxy:{
				type:'ajax',
				actionMethods: {read: 'POST'},
				limitParam: undefined,
                pageParam: undefined,
                startParam: undefined,
				url:'',
				reader:{type:'json',totalProperty:'count',root:'list'},
				extractResponseData:function(response){
					var data = JShell.Server.toJson(response.responseText);
					if(data.success){
						var info = data.value;
						if(info){
							var type = Ext.typeOf(info);
							if(type == 'object'){
								info = info;
							}else if(type == 'array'){
								info.list = info;
								info.count = info.list.length;
							}else{
								info = {};
							}
							
							data.count = info.count || 0;
							data.list = info.list || [];
						}
						me.fireEvent('changeResult',me,data);
					}else{
						me.errorInfo = data.msg;
					}
					response.responseText = Ext.JSON.encode(data);
					
			    	return response;
				}
			},
			listeners:{
				beforeload:function(){return me.onBeforeLoad();},
			    load:function(store,records,successful){
			    	me.onAfterLoad(records,successful);
			    }
			}
		});
	},
	/**获取带查询参数的URL*/
	getLoadUrl:function(){
		var me = this,
			arr = [];
			
		var url = (me.selectUrl.slice(0,4) == 'http' ? '' : 
			me.getPathRoot()) + me.selectUrl;
		
		return url;
	},
	/**查询数据*/
	onSearch:function(){
		var me = this,
			collapsed = me.getCollapsed();
		
		me.defaultLoad = true;
		
		//收缩的面板不加载数据,展开时再加载，避免加载无效数据
		if(collapsed){
			me.isCollapsed = true;
			return;
		}
		
		me.disableControl();//禁用 所有的操作功能
		
		me.showMask(me.loadingText);//显示遮罩层
		
		var url = me.getLoadUrl();
		var params = Ext.JSON.encode(me.postParams);
		JShell.Server.post(url,params,function(data){
			me.hideMask();//隐藏遮罩层
			if(data.success){
				var obj = data.value || {};
				var list = obj.list || [];
				me.store.loadData(list);
				
				if(list.length == 0){
					var msg = me.msgFormat.replace(/{msg}/,JShell.Server.NO_DATA);
					JShell.Action.delay(function(){me.getView().update(msg);},200);
				}else{
					if(me.autoSelect){
						me.doAutoSelect(list.length - 1);
					}
				}
			}else{
				var msg = me.errorFormat.replace(/{msg}/,data.msg);
				JShell.Action.delay(function(){me.getView().update(msg);},200);
			}
		});
	}
});