/**
 * 数字区间组件
 * @author Jcall
 * @version 2014-08-04
 */
Ext.define('Shell.ux.form.field.NumberArea',{
	extend:'Ext.container.Container',
	alias:'widget.uxnumberarea',
	
	/**组件宽度*/
	width:220,
	/**文字宽度*/
	labelWidth:60,
	/**对齐方式*/
	labelAlign:'left',
    
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
			start = {itemId:'start',labelAlign:me.labelAlign},
			end = {itemId:'end',fieldLabel:'-',labelSeparator:'',labelWidth:2,margin:'0 0 0 2'},
			startWidth = endWidth = (me.width - me.labelWidth - 4) / 2;
			
		start.xtype = end.xtype = 'numberfield';
		start.height = end.height = 23;
		
		start.labelWidth = me.labelWidth;
		start.width = me.labelWidth + startWidth;
		end.width = endWidth + 2;
		
		if(me.fieldLabel){start.fieldLabel = me.fieldLabel};
		
		start.listeners = {blur:function(){me.isValid();}};
		end.listeners = {blur:function(){me.isValid();}};
			
		return [start,end];
	},
	/**赋值,格式:{start:number,end:number}*/
	setValue:function(value){
		var me = this,
			start = me.getComponent('start'),
			end = me.getComponent('end');
			
		if(Ext.typeOf(value) == 'object'){
			var sT = Ext.typeOf(value.start) == 'number' || value.start == null,
				eT = Ext.typeOf(value.end) == 'number' || value.end == null;
				
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
        	
        if(st == null || en == null){
        	end.clearInvalid();
        	return true;
        }
	        	
        if(st > en){
        	end.markInvalid(['开始数字不能大于结束数字!']);
        	return false;
        }else{
        	end.clearInvalid();
        	return true;
        }
	}
});