/**
 * 【功能】
 * 可编辑行列光标定位功能,可以配置快捷键处理光标定位，配置
 * 【可配参数】
 * specialkeyArr 键盘快捷键,
 * 组合键可以是Ctrl和Shift键,参数ctrlKey、shiftKey
 * 方向：上(up)下(down)左(left)右(right)，参数type
 * 替换键:用以将一个键替换为另一个键的功能，参数replaceKey
 * @example
 * specialkeyArr:[
 *	{key:Ext.EventObject.ENTER,replaceKey:Ext.EventObject.TAB},//回车键替换为TAB功能
 *	{key:Ext.EventObject.UP,type:'up'},//上箭头
 *	{key:Ext.EventObject.DOWN,type:'down'},//下箭头
 *	{key:Ext.EventObject.LEFT,type:'left',ctrlKey:true},//左箭头
 *	{key:Ext.EventObject.RIGHT,type:'right',ctrlKey:true}//右箭头
 * ]
 * 【事件】
 * cellAvailable 单元格数据校验，返回true/false
 * me.fireEvent('cellAvailable',editor,e)
 */
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.BasicGridPanel',{
	extend:'Ext.grid.Panel',
	type:'gridpanel',
	alias:'widget.zhifanguxbasicgridpanel',
	/**是否循环定位光标*/
	isCycle:false,
	/**数据行上会否循环*/
	isRowCycle:false,
	/**数据列上会否循环*/
	isColCycle:false,
	/**需要光标定位的行下标*/
	rowIdx:null,
	/**需要光标定位的列下标*/
	colIdx:null,
	/**是快捷键操作*/
	isSpecialkey:false,
	/**需要监听的键盘快捷键及方向,方向分为上(up)下(down)左(left)右(right)*/
	specialkeyArr:[
		{key:Ext.EventObject.ENTER,type:'down'},//回车键
		{key:Ext.EventObject.UP,type:'up'},//上箭头
		{key:Ext.EventObject.DOWN,type:'down'},//下箭头
		{key:Ext.EventObject.LEFT,type:'left',ctrlKey:true},//左箭头
		{key:Ext.EventObject.RIGHT,type:'right',ctrlKey:true}//右箭头
	],
	/**
	 * 渲染完后
	 * @private
	 */
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.on({
			validateedit:function(editor,e){
				//存在的问题:获取到当前最新的值,e.value是旧值;
				var bo = false;
				//单元格数据校验
				bo = me.fireEvent('cellAvailable',editor,e) === false ? false : true;
				
				if(me.rowIdx != null && me.colIdx != null){
					if(me.isSpecialkey){
						me.cellEdit.startEditByPosition({row:me.rowIdx,column:me.colIdx});
					}
					me.rowIdx  = null;
					me.colIdx = null;
					me.isSpecialkey = false;
				}
				return bo;
			}
		});
	},
	/**
	 * 初始化组件属性
	 * @private
	 */
	initComponent:function(){
		var me = this;
		
		if(me.isCycle){
			me.isRowCycle = true;
			me.isColCycle = true;
		}
		//单元格数据校验监听
		me.addEvents('cellAvailable');
		//单元格编辑
		me.cellEdit = me.cellEdit || (me.plugins = Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:1}));//单元格编辑
		//创建可编辑框监听
		me.createCellEditListeners();
		me.callParent(arguments);
	},
	/**
	 * 创建编辑列的监听
	 * @private
	 * @param {} com
	 */
	createCellEditListeners:function(){
		var me = this,
			columns = me.columns;
			
		for(var i in columns){
			var column = columns[i];
			if(column.editor){
				//键盘监听
				column.editor.listeners = column.editor.listeners || {};
				column.editor.listeners.specialkey = function(textField,e){
					me.doSpecialkey(textField,e);
				};
				column.hasEditor = true;
			}else if(column.columns){//第二层,用于组合抬头
				for(var  j in column.columns){
					var c = column.columns[j];
					if(c.editor){
						//键盘监听
						c.editor.listeners = c.editor.listeners || {};
						c.editor.listeners.specialkey = function(textField,e){
							me.doSpecialkey(textField,e);
						};
						c.hasEditor = true;
					}
				}
			}
		}
	},
	/**
	 * 键盘事件监听处理
	 * @private
	 * @param {} textField
	 * @param {} e
	 */
	doSpecialkey:function(textField,e){
		var me = this;
		var info = me.getKeyInfo(e);
		
		if(info){
			me.isSpecialkey = true;//快捷键操作
			e.stopEvent();//禁用
			me.changeRowIdxAndCelIdx(textField,info.type);
			textField.blur();
		}
	},
	/**
	 * 根据键盘按钮对象获取快捷键信息
	 * @private
	 * @param {} e
	 * @return {}
	 */
	getKeyInfo:function(e){
		var me = this,
			arr = me.specialkeyArr,
			key = e.getKey();
			
		var info = null;
		for(var i in arr){
			var ctrlKey = arr[i].ctrlKey ? true : false;
			var shiftKey = arr[i].shiftKey ? true : false;
			if(arr[i].key == key && ctrlKey == e.ctrlKey && shiftKey == e.shiftKey){
				if(arr[i].replaceKey){
					e.keyCode = arr[i].replaceKey;
					info = null;break;
				}else{
					info = arr[i];break;
				}
			}
		}
		return info;
	},
	/**
	 * 光标定位行列下标赋值
	 * @private
	 * @param {} field
	 * @param {} type 方向
	 */
	changeRowIdxAndCelIdx:function(field,type){
		var me = this,
			context = field.ownerCt.editingPlugin.context,
			rowIdx = context.rowIdx,
			colIdx = context.colIdx;
		
		me.rowIdx = rowIdx;
		me.colIdx = colIdx;
		if(type == 'up'){
			me.rowIdx = me.getNextRowIndex(rowIdx,false);
		}else if(type == 'down'){
			me.rowIdx = me.getNextRowIndex(rowIdx,true);
		}else if(type == 'left'){
			me.colIdx = me.getNextColIndex(colIdx,false);
		}else if(type == 'right'){
			me.colIdx = me.getNextColIndex(colIdx,true);
		}
	},
	/**
	 * 获取下一个行下标
	 * @private
	 * @param {} rowIdx 行下标
	 * @param {} isDown 是否向下定位光标
	 */
	getNextRowIndex:function(rowIdx,isDown){
		var me = this,
			count = me.store.getCount(),
			nRowIdx = rowIdx;
		
		if(count == 0) return null;	
			
		isDown ? nRowIdx++ : nRowIdx--;
		
		if(me.isRowCycle){//循环
			nRowIdx = nRowIdx % count;
			nRowIdx = nRowIdx < 0 ? nRowIdx + count : nRowIdx;
		}else{
			if(nRowIdx == count){nRowIdx = count - 1;}//超过最大值
			if(nRowIdx == -1){nRowIdx = 0;}//超过最小值
		}
		return nRowIdx;
	},
	/**
	 * 获取下一个列下标
	 * @private
	 * @param {} colIdx 列下标
	 * @param {} isRight 是否向右定位光标
	 */
	getNextColIndex:function(colIdx,isRight){
		var me = this,
			columns = me.columns,
			length = columns.length,
			nColIdx = colIdx;
			
		if(isRight){
			for(var i=colIdx+1;i<length;i++){
				if(columns[i].hasEditor){
					return i;
				}
			}
			if(me.isColCycle){
				for(var i=0;i<colIdx;i++){
					if(columns[i].hasEditor){
						return i;
					}
				}
			}
		}else{
			for(var i=colIdx-1;i>=0;i--){
				if(columns[i].hasEditor){
					return i;
				}
			}
			if(me.isColCycle){
				for(var i=length-1;i>colIdx;i--){
					if(columns[i].hasEditor){
						return i;
					}
				}
			}
		}
		
		return nColIdx;
	}
});