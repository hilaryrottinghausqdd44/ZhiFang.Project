/**
 * 选择部门树
 * @author Jcall
 * @version 2015-07-07
 */
Ext.define('Shell.sysbase.org.CheckOrgTree',{
	extend:'Ext.tree.Panel',
	
	/**默认加载数据时启用遮罩层*/
	hasLoadMask:true,
	
	title:'选择部门',
	width:400,
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
		me.addEvents('okClick');
		me.dockedItems = me.createDockedItems();
		me.store = me.createStore();
		
		me.callParent(arguments);
	},
	createDockedItems:function(){
		var me = this;
		return [{
			xtype:'toolbar',
			dock:'top',
			itemId:'topToolbar',
			items:['->',{
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
			}]
		},{
			xtype:'toolbar',
			dock:'bottom',
			itemId:'bottomToolbar',
			items:['->',{
				iconCls:'build-button-ok',
				itemId:'ok',text:'确定',tooltip:'确定',
				handler:function(){me.onOkClick();}
			}]
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
	onOkClick:function(){
		var me = this,
			records = me.getSelectionModel().getSelection() || [];
			
		if(records && records.length == 1){
			me.fireEvent('okClick',me,records[0]);
		}else{
			Shell.util.Msg.warning('请选择一个部门');
		}
	},
	load:function(){
		this.onRefreshClick();
	}
});