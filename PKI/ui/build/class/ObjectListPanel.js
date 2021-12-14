/**
 * 对象列表类
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.ObjectListPanel',{
	extend:'Ext.grid.Panel',
	alias:'widget.objectlistpanel',
	title:'对象列表',
	defaultLoad:false,
	/**
	 * 获取数据对象列表的服务地址
	 * @type 
	 */
	getObjectListUrl:getRootPath()+'/ConstructionService.svc/CS_BA_GetEntityList',
	
	initComponent:function(){
		var me = this;
		//创建数据集
		me.store = me.createStore();
		//创建数据列
		me.columns = me.createColumns();
		//创建挂靠
		me.dockedItems = me.createDockedItems();
		//创建监听
		me.listeners = me.createListeners();
		me.callParent(arguments);
	},
	/**
	 * 创建监听
	 * @private
	 * @return {}
	 */
	createListeners:function(){
		var me = this;
		var listensers = {
			itemclick:function(com,record){
				me.openWin(record);
			},
			select:function(com,record){
				me.openWin(record);
			}
		};
		return listensers;
	},
	/**
	 * 创建数据集
	 * @private
	 * @return {}
	 */
	createStore:function(){
		var me = this;
		var store = Ext.create('Ext.data.Store',{
			fields:me.getFields(),
			proxy:{
				type:'ajax',
				url:me.getObjectListUrl,
				extractResponseData:function(response){
					var result = Ext.JSON.decode(response.responseText);
					var data = [];
			    	if(result.ResultDataValue && result.ResultDataValue != ''){
			    		var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
				    	data = ResultDataValue;
			    	}
			    	response.responseText = Ext.JSON.encode(data);
			    	//总条数数值赋值
			    	me.setCount(data.length);
			    	return response;
				}
			},
			autoLoad:me.defaultLoad
		});
		return store;
	},
	/**
	 * 创建数据列
	 * @private
	 * @return {}
	 */
	createColumns:function(){
		var me = this;
		var columns = [{
			xtype:'rownumberer',
			text:'序号',
			width:40,
			align:'right'
		},{
			text:'对象名',
			dataIndex:'ClassName',
			width:150
		},{
			text:'中文名',
			dataIndex:'CName',
			width:150
		},{
			text:'英文名',
			dataIndex:'EName',
			width:150
		},{
			text:'简码',
			dataIndex:'ShortCode',
			width:150
		},{
			text:'描述',
			dataIndex:'Description',
			width:150
		}];
		return columns;
	},
	/**
	 * 创建挂靠
	 * @private
	 * @return {}
	 */
	createDockedItems:function(){
		var me = this;
		var top = [{
			xtype:'button',text:'更新',itemId:'loadBut',
			iconCls:'build-button-refresh',
			handler:function(button){me.load();}
		},'->',{
			xtype:'textfield',itemId:'searchText',width:160,
        	emptyText:'对象名/中文名/英文名/简码',
        	listeners:{
        		render:function(input){
			    	new Ext.KeyMap(input.getEl(),[{
				      	key:Ext.EventObject.ENTER,
				      	fn:function(){me.search();}
			     	}]);
			    }
        	}
		},{
			xtype:'button',text:'查询',iconCls:'search-img-16 ',
        	tooltip:'按照对象名、中文名、英文名、简码进行查询',
        	handler:function(button){me.search();}
		}];
		
		var bottom = ['->',{
			xtype:'label',
			itemId:'count'
		}];
		var dockedItems = [{
			xtype:'toolbar',
			itemId:'toolbar-top',
			dock:'top',
			items:top
		},{
			xtype:'toolbar',
			itemId:'toolbar-bottom',
			dock:'bottom',
			items:bottom
		}];
		return dockedItems;
	},
	/**
	 * 获取数据字段
	 * @private
	 * @return {}
	 */
	getFields:function(){
		var me = this;
		var fields = ['ClassName','CName','EName','Description','ShortCode'];
		return fields;
	},
	search:function(){
		var me = this;
		var searchText = me.getComponent('toolbar-top').getComponent('searchText');
		var value = searchText.getValue();
		me.store.filterBy(function(record){
			var ClassName = record.get('ClassName');
			var CName = record.get('CName');
			var EName = record.get('EName');
			var Description = record.get('Description');
			var ShortCode = record.get('ShortCode');
			
			var b1 = ClassName.indexOf(value)!=-1;
			var b2 = CName.indexOf(value)!=-1;
			var b3 = EName.indexOf(value)!=-1;
			var b4 = Description.indexOf(value)!=-1;
			var b5 = ShortCode.indexOf(value)!=-1;
			
			return b1 || b2 || b3 || b4 || b5;
		});
	},
	/**
	 * 总条数赋值
	 * @private
	 * @param {} count
	 */
	setCount:function(count){
		var me = this;
		var com = me.getComponent('toolbar-bottom').getComponent('count');
		var str = "总共 <b>" + count + "</b> 条数据";
		com.setText(str,false);
	},
	/**
	 * 更新数据
	 * @private
	 */
	load:function(){
		var me = this;
		me.store.load();
	},
	/**
	 * 弹出详细详细窗口
	 * @private
	 * @param {} record
	 */
	openWin:function(record){
		var me = this;
		
		var p = Ext.WindowManager.get("objectInfoWin");
		if(p){//已经打开了窗口
			//Ext.WindowManager.bringToFront(p);
			var obj = {
				ClassName:record.get('ClassName'),
				CName:record.get('CName'),
				EName:record.get('EName'),
				ShortCode:record.get('ShortCode'),
				Description:record.get('Description')
			};
			p.getForm().setValues(obj);
		}else{
			var win = me.createForm(record);
			win.show();
		}
	},
	/**
	 * 创建详细信息表单
	 * @private
	 * @param {} record
	 * @return {}
	 */
	createForm:function(record){
		var maxHeight = document.body.clientHeight*0.98;
		var maxWidth = document.body.clientWidth*0.98;
		var width = 450;
		var height = 220;
		var win = Ext.create('Ext.form.Panel',{
			id:'objectInfoWin',
			width:maxWidth > width ? width : maxWidth,
			height:maxHeight > height ? height : maxHeight,
			autoScroll:true,
    		modal:false,//模态
    		floating:true,//漂浮
			closable:true,//有关闭按钮
			resizable:true,//可变大小
			draggable:true,//可移动
			layout:'absolute',
			title:'详细信息',
			defaults:{
				xtype:'textfield',
				width:200,
				labelWidth:50,
				labelAlign:'right',
				readOnly:true
			},
			items:[{
				fieldLabel:'对象名',
				name:'ClassName',
				value:record.get('ClassName'),
				x:10,y:10
			},{
				fieldLabel:'中文名',
				name:'CName',
				value:record.get('CName'),
				x:220,y:10
			},{
				fieldLabel:'英文名',
				name:'EName',
				value:record.get('EName'),
				x:10,y:40
			},{
				fieldLabel:'简码',
				name:'ShortCode',
				value:record.get('ShortCode'),
				x:220,y:40
			},{
				fieldLabel:'描述',
				name:'Description',
				value:record.get('Description'),
				xtype:'textarea',
				height:100,
				width:410,
				x:10,y:70
			}]
		});
		return win;
	}
});