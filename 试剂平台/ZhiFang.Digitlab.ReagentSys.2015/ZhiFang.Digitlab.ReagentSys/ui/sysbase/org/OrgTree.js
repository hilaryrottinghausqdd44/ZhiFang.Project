/**
 * 部门树
 * @author Jcall
 * @version 2015-07-07
 */
Ext.define('Shell.sysbase.org.OrgTree',{
	extend:'Ext.tree.Panel',
	
	/**按钮栏位置top/bottom*/
	toolbarDock:'top',
	/**默认加载数据时启用遮罩层*/
	hasLoadMask:true,
	/**可以修改*/
	canEidt:true,
	
	title:'部门树',
	width:1200,
	height:600,
	/**根部门ID*/
	rootOrgId:0,
	/**跟部门名称*/
	rootOrgName:'所有部门',
	selectOrgUrl:Shell.util.Path.rootPath + '/RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree?fields=HRDept_Id',
	removeOrgUrl:Shell.util.Path.rootPath + '/RBACService.svc/RBAC_UDTO_DelHRDept',
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		me.store.on({
			beforeload:function(){
				if(me.hasLoadMask){
					me.mk = me.mk || new Ext.LoadMask(me.getEl(),{msg:'数据加载中...',removeMask:true});
					me.mk.show();//显示遮罩层
		    	}
			},
			load:function(){
				if(me.hasLoadMask && me.mk){me.mk.hide();}//隐藏遮罩层
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('addClick','afterRemove');
		me.dockedItems = me.createDockedItems();
		me.store = me.createStore();
		
		me.callParent(arguments);
	},
	createDockedItems:function(){
		var me = this;
		
		var items = [{
			iconCls:'build-button-refresh',
			itemId:'refresh',tooltip:'刷新数据',
			handler:function(){me.onRefreshClick();}
		},'-',{
			iconCls:'build-button-arrow-in',
			itemId:'minus',tooltip:'全部收缩',
			handler:function(){me.onMinusClick();}
		},{
			iconCls:'build-button-arrow-out',
			itemId:'plus',tooltip:'全部展开',
			handler:function(){me.onPlusClick();}
		}];
		
		if(me.canEidt){
			items.push('->',{
				iconCls:'build-button-add',
				itemId:'add',tooltip:'新增',
				handler:function(){me.onAddClick();}
			},{
				iconCls:'build-button-delete',
				itemId:'remove',tooltip:'删除',
				handler:function(){me.onRemoveClick();}
			});
		}
		
		return [{
			xtype:'toolbar',
			dock:me.toolbarDock,
			itemId:'topToolbar',
			items:items
		}];
	},
	/**创建数据集*/
	createStore:function(){
		var me = this;
		return Ext.create('Ext.data.TreeStore',{
			defaultRootProperty:'Tree',
			fields:me.getStoreFields(),
			root:{
				text:me.rootOrgName,
				iconCls:'orgsImg16',
				id:me.rootOrgId,tid:me.rootOrgId,
				leaf:false,expanded:true
			},
			proxy: {
				type:'ajax',url:me.selectOrgUrl,
				extractResponseData:function(response){
					var data = Ext.JSON.decode(response.responseText);
					
					if(data.success == false || data.success == 'false'){
						Shell.util.Msg.error(data.ErrorInfo);
					}
					
					if (data.ResultDataValue && data.ResultDataValue != '') {
						data = Ext.JSON.decode(data.ResultDataValue);
						if(!me.rootOrgId ||me.rootOrgId == '0'){
							data = {Tree:[data]};
						}
					}else{
						data = {Tree:[{}]};
					}
					response.responseText = Ext.JSON.encode(data.Tree[0]);
					return response;
				}
			},
			defaultLoad:true
		});
	},
	/**获取数据字段*/
	getStoreFields:function(){
		var me = this,
			fields = ['text','expanded','leaf','icon','iconCls','url','tid','pid'];
			
		return fields;
	},
	onRefreshClick:function(){
		this.store.load();
	},
	onMinusClick:function(){
		var me = this;
		me.collapseAll();
		me.getRootNode().expand();
	},
	onPlusClick:function(){
		this.expandAll();
	},
	onAddClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection() || [];
			
		if(records && records.length == 1){
			me.fireEvent('addClick',me,records[0]);
		}else{
			Shell.util.Msg.warning('请选择一个部门进行新增操作');
		}
	},
	onRemoveClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection() || [];
		
		if (records.length == 1) {
			var id = records[0].get('tid');
			if(id == me.rootOrgId){
				Shell.util.Msg.warning('不能删除部门根节点');
				return;
			}
			Ext.Msg.confirm('提示','确定要删除吗？',function(button){
				if (button == 'yes') {
					var url = me.removeOrgUrl + '?id=' + id;
					Shell.util.Server.get(url,function(data){
						if(data.success){
							me.onRefreshClick();
							me.fireEvent('afterRemove',me);
						}else{
							Shell.util.Msg.error(data.msg);
						}
					});
				}
			});
		} else {
			Shell.util.Msg.warning('请选择一个部门');
		}
	},
	load:function(){
		this.onRefreshClick();
	}
});