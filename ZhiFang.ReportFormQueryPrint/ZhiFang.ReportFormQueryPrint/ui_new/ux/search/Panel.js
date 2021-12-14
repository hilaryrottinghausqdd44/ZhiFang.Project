/**
 * 高级查询面板
 * @author Jcall
 * @version 2014-08-06
 */
Ext.define('Shell.ux.search.Panel',{
	extend:'Shell.ux.form.Panel',
	
	mixins:['Shell.ux.PanelController'],
	
	/**开启右键菜单*/
	hasContextMenu:false,
	
	bodyPadding:5,
	
	/**重写渲染完毕执行*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		me.getEl().on('keypress',function(key){
			if(key.keyCode == Ext.EventObject.ENTER){
				me.onAcceptClick();
			}
		});
		//开启右键快捷菜单设置
		me.onContextMenu();
	},
	/**重写初始化面板属性*/
	initComponent:function(){
		var me = this;
		me.addEvents('accept','reset','cancel');
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
	/**创建挂靠*/
	createDockedItems:function(){
		var me = this,
			toolbars = me.toolbars || [{dock:'bottom',buttons:['->','accept','reset','cancel']}],
			length = toolbars.length,
			dockedItems = [];
		
		for(var i=0;i<length;i++){
			dockedItems.push({
				autoScroll:true,
				dock:toolbars[i].dock || 'top',
				xtype:'uxbuttonstoolbar',
				buttons:toolbars[i].buttons,
				listeners:{
					click:function(but,type){
						me.onButtonClick(but,type);
					}
				}
			});
		}
			
		return dockedItems;
	},
	onAcceptClick:function(but){
		var me = this,
			where = me.getWhere();
		
		me.fireEvent('accept',me,where);
	},
	onResetClick:function(but){
		this.getForm().reset();
	},
	onCancelClick:function(but){
		this.close();
	},
	/**获取条件串*/
	getWhere:function(){
		var me = this,
			fields = me._thisfield,
			length = fields.length,
			whereArr = [],
			value = '',
			field;
			
		if(!me.isValid()) return null;//数据格式有误
		
		for(var i=0;i<length;i++){
			field = fields[i];
			value = field.getValue(true);
			if((value != null && value != '') || value === false){
				var where = me.getFieldWhere(field,value);
				if(where && where != ''){
					whereArr.push(where);
				}
			}
		}
		
		return whereArr.join(' and ');
	},
	/**获取组件的条件串*/
	getFieldWhere:function(field,value){
		var me = this,
			type = field.xtype,
			name = field.whereField,
			where = '';
			
		if(!name) return null;
			
		if(type == 'uxdatearea'){//日期区间
			where = me.getUxdateareaWhere(name,value);
		}else if(type == 'uxnumberarea'){//数字区间
			where = me.getUxnumberarea(name,value);
		}else if(type == 'uxtextfield'){//模糊匹配
			where = name + " like '%" + value + "%'";
		}else if(type == 'uxcheckbox' || type == 'uxcheckboxfield'){//是否勾选
			where = name + "=" + value;
		}else if(type == 'uxradiogroup' || type == 'uxcheckboxgroup' || 
				type == 'uxcombobox' || type == 'uxcombo' || 
				type == 'uxcombogridbox' || type == 'uxcombogrid'){
			//单选多选
			var t = Ext.typeOf(value);
			if(t == 'array'){
				where = name + " in('" + value.join("','") + "')";
			}else if(t == 'string' || t == 'number' || t == 'boolean'){
				where = name + "='" + value + "'";
			}
		}
		
		where = where.length > 0 ? "(" + where + ")" : "";
		
		return where;
	},
	/**获取日期区间条件串*/
	getUxdateareaWhere:function(name,value){
		var start = value.start,
			end = value.end,
			w = [],
			where = "";
			
		if(start){
			start = Shell.util.Date.toString(start,true);
			w.push(name + ">='" + start + "'");
		}
		if(end){
			end = Shell.util.Date.getNextDate(end);
			end = Shell.util.Date.toString(end,true);
			w.push(name + "<'" + end + "'");
		}
		where = w.length > 0 ? w.join(" and ") : "";
		
		return where;
	},
	/**获取数字区间条件串*/
	getUxnumberarea:function(name,value){
		var start = value.start,
			end = value.end,
			w = [],
			where = "";
			
		if(start){
			w.push(name + ">=" + start);
		}
		if(end){
			w.push(name + "<=" + end);
		}
		where = w.length > 0 ? w.join(" and ") : "";
		
		return where;
	}
});