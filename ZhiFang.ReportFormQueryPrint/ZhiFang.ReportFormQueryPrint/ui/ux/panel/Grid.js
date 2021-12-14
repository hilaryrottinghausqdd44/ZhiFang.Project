/**
 * 列表类
 * @author Jcall
 * @version 2014-08-08
 */
Ext.define('Shell.ux.panel.Grid',{
	extend:'Shell.ux.grid.Panel',
	alias:'widget.uxgrid',
	
	/**是否批量删除*/
	isBatchDel:false,
	/**删除成功标志字段*/
	stateField:'state',
	/**删除状态信息字段*/
	stateInfoField:'stateInfo',
	/**是否有选泽框列*/
	multiSelect:false,
	
	/**启用数据列排序功能*/
	columnSortable:true,
	/**启用数据列移动功能*/
	columnDraggable:true,
	/**启用数据列隐藏功能*/
	columnHideable:true,
	
	/**重写初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.addEvents('addClick','editClick','showClick');
		
		if(me.multiSelect === true){
		    me.selType = 'checkboxmodel';
		}
		
		me.callParent(arguments);
	},
	
	/**重写创建数据列*/
	createColumns:function(){
		var me = this,
			columns = me.callParent(arguments) || [];
			
//		me.plugins = [Ext.create('Ext.grid.plugin.CellEditing',{clicksToEdit:2})];
//		var length = columns.length;
//		for(var i=0;i<length;i++){
//			columns[i] = Ext.applyIf(columns[i],{
//				editor:{readOnly:true}
//			});
//		}
			
		if(!me.delUrl || me.delUrl == '') return columns;
			
		if(me.isBatchDel) return columns;
		
		columns.push({
			dataIndex:me.stateField,
			width:40,
			sortable:false,
			draggable:false,
			hideable:false,
			renderer:function(value,meta,record){
				if(value === true){value = "<b style='color:green'>成功</b>";}
				if(value === false){value = "<b style='color:red'>失败</b>";}
				
                meta.tdAttr = 'data-qtip="' + (record.get(me.stateInfoField) || '') + '"';
                return value;
            }
		});
		
		return columns;
	},
	
	/**重写新增功能*/
	onAddClick:function(but){
		var me = this,
			closeAction = me.openWinCloseAction,
			url = but.iframeUrl,
			className = but.className,
			classConfig = but.classConfig || {};
			
		if(className){
			var win = Shell.util.Win.open(className,Ext.apply(classConfig,{formtype:'add'}));
			win.on({
				save:function(form){
					win.close();
					me.load(null,true);
				}
			});
		}else{
			me.fireEvent('addClick',me,but);
		}
	},
	/**重写修改功能*/	
	onEditClick:function(but){
		var me = this,
			closeAction = me.openWinCloseAction,
			url = but.iframeUrl,
			className = but.className,
			classConfig = but.classConfig || {},
			records = me.getSelectionModel().getSelection();
			
		if(records.length != 1){
			me.showInfo('请选择一条记录进行操作！');
			return;
		}
		
		var id = records[0].get(me.PKColumn);
		if(className){
			var win = Shell.util.Win.open(className,Ext.apply(classConfig,{formtype:'edit',PK:id}));
			win.on({
				save:function(form){
					win.close();
					me.load(null,true);
				}
			});
		}else{
			me.fireEvent('editClick',me,but,id,records[0]);
		}
	},
	/**重写查看功能*/
	onShowClick:function(but){
		var me = this,
			closeAction = me.openWinCloseAction,
			url = but.iframeUrl,
			className = but.className,
			classConfig = but.classConfig || {},
			records = me.getSelectionModel().getSelection();
			
		if(records.length != 1){
			me.showInfo('请选择一条记录进行操作！');
			return;
		}
		
		var id = records[0].get(me.PKColumn);
		if(className){
			Shell.util.Win.open(className,Ext.apply(classConfig,{formtype:'show',PK:id}));
		}else{
			me.fireEvent('showClick',me,but,id,records[0]);
		}
	},
	/**重写删除功能*/
	onDelClick:function(but){
		var me = this,
			records = me.getSelectionModel().getSelection(),
			length = records.length,
			ids = [];
		
		if(length == 0){
			me.showInfo('请勾选需要删除的记录!');
			return;
		}
		
		me.confirmDel(function(button){
			if(button == "ok"){
				for(var i=0;i<length;i++){
					ids.push(records[i].get(me.PKColumn));
				}
				me.onDel(ids);
			}
		});
	},
	/**删除数据*/
	onDel:function(ids){
		var me = this,
			delUrl = Shell.util.Path.rootPath + me.delUrl,
			length = ids.length,
			count = 0;
			
		if(!me.multiSelect){//单选删除
			var url = delUrl + '?id=' + ids[0];
			me.getToServer(url,function(text){
				var result = Ext.JSON.decode(text);
				if(result.success){
					me.load(null,true);
				}else{
					me.showError('删除失败！');
				}
			},false);
		}else{
			for(var i=0;i<length;i++){
				var url = delUrl + '?id=' + ids[i];
				var id = ids[i];
				me.getToServer(url,function(text){
					var result = Ext.JSON.decode(text);
					var record = me.store.findRecord(me.PKColumn,id);
					if(result.success){
						if(record){record.set(me.stateField,true);}
						count++;
						if(count == length){me.load(null,true);}
					}else{
						if(record){record.set(me.stateField,false);}
					}
				},false);
			}
		}
	},
	/**重写高级查询*/
	onSearchClick:function(but){
		if(this.searchaWin){
			this.searchaWin.show();
			return;
		}
		
		var me = this,
			url = but.iframeUrl,
			className = but.className,
			classConfig = but.classConfig || {};
			
		Ext.apply(classConfig,{
			listeners:{accept:function(form,where){
				me.internalWhere = where;
				me.load(null,true);
			}},
			closeAction:'hide'
		});
			
		me.searchaWin = Shell.util.Win.open(className,classConfig || {});
	}
});