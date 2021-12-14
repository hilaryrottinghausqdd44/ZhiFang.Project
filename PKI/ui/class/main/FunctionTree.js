/**
 * 报告结果
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.main.FunctionTree',{
    extend:'Ext.tree.Panel',
	
	/**可以修改*/
	canEidt:true,
	
	title:'功能树',
	width:300,
	height:500,
	
	selectOrgUrl:JShell.System.Path.ROOT + '/ui/server/getFunctionTree.js',
	
	afterRender:function(){
		var me = this ;
		me.callParent(arguments);
		
		me.store.on({
			beforeload:function(){
				me.mk = me.mk || new Ext.LoadMask(me.getEl(),{msg:'数据加载中...',removeMask:true});
				me.mk.show();//显示遮罩层
			},
			load:function(){
				if(me.mk){me.mk.hide();}//隐藏遮罩层
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.dockedItems = me.createDockedItems();
		me.store = me.createStore();
		me.callParent(arguments);
	},
	createDockedItems:function(){
		var me = this;
		
		var items = [{
			iconCls:'button-refresh',
			itemId:'refresh',tooltip:'刷新数据',
			handler:function(){me.onRefreshClick();}
		},'-',{
			iconCls:'button-arrow-in',
			itemId:'minus',tooltip:'全部收缩',
			handler:function(){me.onMinusClick();}
		},{
			iconCls:'button-arrow-out',
			itemId:'plus',tooltip:'全部展开',
			handler:function(){me.onPlusClick();}
		},'->',{
			iconCls:'button-left',
			tooltip:'<b>收缩面板</b>',
			handler:function(){me.collapse();}
		}];
		
		return [{
			xtype:'toolbar',
			dock:'top',
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
				text:'所有功能',
				iconCls:'button-show',
				id:0,
				leaf:false,
				expanded:true
			},
			proxy: {
				type:'ajax',url:me.selectOrgUrl,
				extractResponseData:function(response){
					var data = Ext.JSON.decode(response.responseText);
					
					if(data.success == false || data.success == 'false'){
						var html = '<div style="text-align:center;color:red;"><b>' + 
							data.ErrorInfo + "</b></div>";
						me.getView().update(html);
					}
					
					return response;
				}
			},
			defaultLoad:true
		});
	},
	/**获取数据字段*/
	getStoreFields:function(){
		var me = this,
			fields = ['text','expanded','leaf','icon','iconCls','url','tid','pid','className'];
			
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
	load:function(){
		this.onRefreshClick();
	}
});
	