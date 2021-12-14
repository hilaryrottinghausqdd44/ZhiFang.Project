/**
 * 货品维护
 * @author liangyl
 * @version 2016-11-14
 */
Ext.define('Shell.class.rea.client.goods2.AddPanel', {
	extend: 'Ext.tab.Panel',
	title: '货品维护',
	requires:['Shell.ux.toolbar.Button'],
	width: 700,
	height: 530,
	autoScroll: false,
	/**货品ID*/
	PK: null,
	/**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
	/**新增,edit 修改*/
	formtype : 'add',
	/**货品ID*/
	GoodsID:null,
    /**货品名称*/
	GoodsCName:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
		//新增时 ,其他页签不可用
		if(me.GoodsID){
		    me.BatchGrid.setDisabled(false);
			me.RegisterGrid.setDisabled(false);
		}
		me.Form.on({
			save: function(p, id) {
				var values=p.getForm().getValues();
				//货品名称
				var CName = values.ReaGoods_CName;
				/**货品ID*/
				me.GoodsID=id;
			    /**货品名称*/
				me.GoodsCName=CName;
	          
//				保存后给页签赋值 货品id和货品名称
				me.Form.PK=id;
				me.Form.isEdit(id);
				me.BatchGrid.GoodsID=id;
				me.BatchGrid.GoodsCName=CName;
			    me.RegisterGrid.GoodsID=id;
				me.RegisterGrid.GoodsCName=CName;
				
				//页签可用
			    me.BatchGrid.setDisabled(false);
				me.RegisterGrid.setDisabled(false);

			}
	    });
	    me.on({
			beforetabchange:function( tabPanel, newCard, oldCard,  eOpts ){
                me.cancelEdit();
			}
	    });
	},
	//取消编辑
	cancelEdit:function(){
		var me=this;
        var edit2 = me.BatchGrid.getPlugin('NewsGridEditing'); 
        var edit3 = me.RegisterGrid.getPlugin('NewsGridEditing'); 
        edit2.cancelEdit();
        edit3.cancelEdit();
	},
	initComponent: function() {
		var me = this;
		me.addEvents('save');
		//内部组件
		me.items = me.createItems();
		//创建挂靠功能栏
		me.dockedItems = me.createDockedItems();

		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.Form = Ext.create('Shell.class.rea.client.goods2.Form', {
			formtype: me.formtype,
			PK:me.PK,
			hasLoadMask: false,//开启加载数据遮罩层
			title: '货品信息',
			itemId: 'Form',
			hasButtontoolbar:false//带功能按钮栏
		});
		
	    me.BatchGrid = Ext.create('Shell.class.rea.client.goods2.lot.Grid', {
			title: '批次信息',
			itemId: 'BatchGrid',
			disabled:true,
			/**货品ID*/
			GoodsID:me.GoodsID,
		    /**货品名称*/
			GoodsCName:me.GoodsCName,
			/**默认每页数量*/
			defaultPageSize: 100,
			/**带分页栏*/
			hasPagingtoolbar: false,
		});
	    me.RegisterGrid = Ext.create('Shell.class.rea.client.goods2.register.Grid', {
			title: '注册证信息',
			itemId: 'RegisterGrid',
			disabled:true,
			/**默认每页数量*/
			defaultPageSize: 100,
			/**货品ID*/
			GoodsID:me.GoodsID,
		    /**货品名称*/
			GoodsCName:me.GoodsCName,
			/**带分页栏*/
			hasPagingtoolbar: false,
		});
		return [me.Form,me.BatchGrid,me.RegisterGrid];
	},
	/**创建挂靠功能栏*/
	createDockedItems:function(){
		var me = this;
		var dockedItems = {
			xtype:'uxButtontoolbar',
			dock:'bottom',
			itemId:'buttonsToolbar',
			items:['->',{
				text:'保存',
				iconCls:'button-save',
				tooltip:'保存',
				handler:function(btn){
					me.onSave(true);
				}
			}]
		};
		return dockedItems;
	},
	/**保存按钮点击处理方法*/
	onSave:function(isSubmit){
		var me = this,
			values = me.Form.getForm().getValues();
		me.cancelEdit();
		if(!me.Form.getForm().isValid()){
			me.setActiveTab(me.Form);
			return;
		}
    
        var changedBatchRecords = me.BatchGrid.store.getModifiedRecords();
       	//验证
		if(!me.BatchGrid.isValid(changedBatchRecords)){
			me.setActiveTab(me.BatchGrid);
			return;
		}
		var changedRegisterRecords = me.RegisterGrid.store.getModifiedRecords();
       	//验证
		if(!me.RegisterGrid.isValid(changedRegisterRecords)){
			me.setActiveTab(me.RegisterGrid);
			return;
		}
		me.Form.onSaveClick();
		//存在id时,保存各个页签
		if(me.GoodsID){
			//批次信息修改
			me.BatchGrid.onSaveClick();
		    //注册证信息修改
		    me.RegisterGrid.onSaveClick();
		}
	}
});