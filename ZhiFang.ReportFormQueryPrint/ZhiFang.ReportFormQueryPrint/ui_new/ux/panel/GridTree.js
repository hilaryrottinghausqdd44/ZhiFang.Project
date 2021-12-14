/**
 * 表格树面板类
 * @author Jcall
 * @version 2014-09-02
 */
Ext.define('Shell.ux.panel.GridTree',{
	extend:'Shell.ux.panel.Tree',
	alais:'widget.uxgridtree',
	
	/**开启单元格内容提示*/
	tooltip:false,
	
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		if(me.columns){
			me.columns = me.createColumns();
		}
		me.callParent(arguments);
	},
	/**创建数据列*/
	createColumns:function(){
		var me = this,
			columns = me.columns,
			length = columns.length,
			type;
		
		//数据列基础属性默认值
		for(var i=0;i<length;i++){
			type = columns[i].type;
			if(type == 'key'){me.PKColumn = columns[i].dataIndex;}
			
			if(type == 'datetime'){
				columns[i] = me.applyDatetimeColumn(columns[i]);
			}else if(type == 'isuse'){
				columns[i] = me.applyIsuseColumn(columns[i]);
			}else{
				columns[i] = me.applyColumn(columns[i]);
			}
		}
		
		return columns;
	},
	/**转化一般数据列*/
	applyColumn:function(column){
		var me = this,
			tooltip = me.tooltip;
		
		if(tooltip){
			column = Ext.applyIf(column,{
				renderer:function(value,meta,record){
                    meta.tdAttr = 'data-qtip="' + value + '"';
                    return value;
                }
			});
		}
		
		return column;
	},
	/**转化日期时间数据列*/
	applyDatetimeColumn:function(column){
		var me = this,
			tooltip = me.tooltip;
		
		if(tooltip){
			column = Ext.applyIf(column,{
				renderer:function(value,meta,record){
                    value = value == null ? '' : value.replace(/\//g,'-');
                    meta.tdAttr = 'data-qtip="' + value + '"';
                    return value;
                }
			});
		}else{
			column = Ext.applyIf(column,{
				renderer:function(value,meta,record){
                    value = value == null ? '' : value.replace(/\//g,'-');
                    return value;
                }
			});
		}
		
		return column;
	},
	/**转化是否使用数据列*/
	applyIsuseColumn:function(column){
		var me = this,
			tooltip = me.tooltip;
		
		if(tooltip){
			column = Ext.applyIf(column,{
				renderer:function(value,meta,record){
                    value = value.toString() == 'true' ? "<b style='color:green'>是</b>" : "<b style='color:red'>否</b>";
                    meta.tdAttr = 'data-qtip="' + value + '"';
                    return value;
                }
			});
		}else{
			column = Ext.applyIf(column,{
				renderer:function(value,meta,record){
                    value = value.toString() == 'true' ? "<b style='color:green'>是</b>" : "<b style='color:red'>否</b>";
                    return value;
                }
			});
		}
		
		return column;
	}
});