Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	var data = [];
	for(var i=0;i<20;i++){
		data.push({name:'张三'+i,age:15});
	}
	
	var store = Ext.create('Ext.data.Store',{
		fields:['name','age'],
		data:data
	});
	
	var cellEdit = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1});
	
	var grid = Ext.create('Ext.grid.Panel',{
		plugins:cellEdit,
		cellEdit:cellEdit,
		isCycle:true,
		title:'编辑列表单元格监听测试',
		store:store,
		rowIdx:null,
		colIdx:null,
		isSpecialkey:false,//是否键盘操作
		listeners:{
			edit:function(editor,e){
				var me = grid;
				if(me.isSpecialkey && me.rowIdx != null && me.colIdx != null){
					me.cellEdit.startEditByPosition({row:me.rowIdx,column:me.colIdx});
				}
				me.rowIdx  = null;
				me.colIdx = null;
				me.isSpecialkey = false;
			}
		},
		getNextRowIndex:function(rowIdx,isDown){
			var me = this,
				count = me.store.getCount(),
				nRowIdx = rowIdx;
			
			if(count == 0) return null;	
				
			if(isDown){
				if(nRowIdx+1 == count){
					if(me.isCycle){nRowIdx = 0;}
				}else{
					nRowIdx++;
				}
			}else{
				if(nRowIdx == 0){
					if(me.isCycle){nRowIdx = count-1;}
				}else{
					nRowIdx--;
				}
			}
			
			return nRowIdx;
		},
		changeRowIdxAndCelIdx:function(field,isDown){
			var me = this,
				context = field.ownerCt.editingPlugin.context,
				rowIdx = context.rowIdx,
				colIdx = context.colIdx;
			
			var nRowIdx = me.getNextRowIndex(rowIdx,isDown);
			me.rowIdx = nRowIdx;
			me.colIdx = colIdx;
		},
		doSpecialkey:function(textField,e){
			e.stopEvent();
			var me = this;
			if(e.getKey() == Ext.EventObject.ENTER){
				me.changeRowIdxAndCelIdx(textField,true);
			}else if(e.getKey() == Ext.EventObject.UP){
				me.changeRowIdxAndCelIdx(textField,false);
			}else if(e.getKey() == Ext.EventObject.DOWN){
				me.changeRowIdxAndCelIdx(textField,true);
			}else if(e.getKey() == Ext.EventObject.CAPS_LOCK){
				me.changeRowIdxAndCelIdx(textField,true);
			}
		},
		columns:[{
			text:'姓名',dataIndex:'name',editor:{
				listeners:{
					specialkey:function(textField,e){
						grid.isSpecialkey = true;
						grid.doSpecialkey(textField,e);
						textField.blur();
					}
				}
			}
		},{
			text:'年龄',dataIndex:'age'//,editor:{}
		}]
	});
	
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[grid]
	});
});