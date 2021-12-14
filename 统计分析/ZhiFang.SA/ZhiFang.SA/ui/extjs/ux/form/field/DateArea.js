/**
 * 日期区间组件
 * @author Jcall
 * @version 2014-08-06
 */
Ext.define('Shell.ux.form.field.DateArea',{
	extend:'Ext.container.Container',
	alias:'widget.uxdatearea',
	
	/**组件宽度*/
	width:254,
	/**文字宽度*/
	labelWidth:60,
	/**对齐方式*/
	labelAlign:'left',
	
	allowBlank:true,
	isLabelSeparator:true,//设置fieldLabel为" "，设置isLabelSeparator为false 即可解决宽度不同的问题
    
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.initListeners();
	},
	/**初始化监听*/
	initListeners:function(){
		var me = this,
			start = me.getComponent('start'),
			end = me.getComponent('end');
			
		start.on({
			change:function(field,nV,oV){
				var isValid = me.isValid();
				var value = me.getValue();
				if(isValid){
					me.fireEvent('change',me,field,value);
				}
			}
		});
		end.on({
			change:function(field,nV,oV){
				var isValid = me.isValid();
				var value = me.getValue();
				if(isValid){
					me.fireEvent('change',me,field,value);
				}
			}
		});
	},
	/**初始化组件属性*/
	initComponent:function(){
		var me = this;
		me.addEvents('change');
		me.layout = 'hbox';
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**创建内部组件*/
	createItems:function(){
		var me = this,
			start = me.isLabelSeparator ? {itemId:'start',labelAlign:me.labelAlign} : {itemId:'start',labelAlign:me.labelAlign,labelSeparator:''},
			end = {itemId:'end',fieldLabel:'-',labelSeparator:'',labelWidth:2,margin:'0 0 0 2'},
			startWidth = endWidth = (me.width - me.labelWidth - 4) / 2;
			
		start.xtype = end.xtype = 'datefield';
		start.format = end.format = 'Y-m-d';
		//start.height = end.height = 23;
		//回车键监听
		start.listeners = end.listeners = {
			specialkey:function(field,e){
				if(e.getKey() == Ext.EventObject.ENTER){
					var isValid = me.isValid();
					var value = field.getValue();
					if(isValid){
						me.fireEvent('enter',me,field,value);
					}
				}
			},
			blur:function(){me.isValid();}
		};
		
		start.labelWidth = me.labelWidth;
		start.width = me.labelWidth + startWidth;
		end.width = endWidth + 2;
		
		if(me.fieldLabel){start.fieldLabel = me.fieldLabel};
		
		start.allowBlank = end.allowBlank = me.allowBlank;
		
		if(me.value){
			if(me.value.start){start.value = me.value.start;}
			if(me.value.end){end.value = me.value.end;}
		}
			
		return [start,end];
	},
	/**赋值,格式:{start:date,end:date}*/
	setValue:function(value){
		var me = this,
			start = me.getComponent('start'),
			end = me.getComponent('end');
			
		if(Ext.typeOf(value) == 'object'){
			var sT = Ext.typeOf(value.start) == 'date' || value.start == null,
				eT = Ext.typeOf(value.end) == 'date' || value.end == null;
				
			start.setValue(sT ? value.start : null);
			end.setValue(eT ? value.end : null);
		}else{
			start.setValue(null);
			end.setValue(null);
		}
	},
	/**取值,格式:{start:number,end:number}*/
	getValue:function(){
		var me = this,
			start = me.getComponent('start'),
			end = me.getComponent('end'),
			st = start.getValue(),
        	en = end.getValue();
        	
        if(st == null && en == null) return null;
			
		return {start:st,end:en};
	},
	/**校验数据有效性*/
	isValid:function(){
		var me = this,
			start = me.getComponent('start'),
			end = me.getComponent('end'),
        	st = start.getValue(),
        	en = end.getValue();
        	
        if(!start.isValid() || !end.isValid()) return false;
        	
        if(st == null || en == null){
        	end.clearInvalid();
        	return true;
        }
	        	
        if(st > en){
        	end.markInvalid(['开始日期不能大于结束日期!']);
        	return false;
        }else{
        	end.clearInvalid();
        	return true;
        }
	},
	enable:function(){
		var me = this,
			start = me.getComponent('start'),
			end = me.getComponent('end');
			
		start.enable();
		end.enable();
	},
	disable:function(){
		var me = this,
			start = me.getComponent('start'),
			end = me.getComponent('end');
			
		start.disable();
		end.disable();
	}
});