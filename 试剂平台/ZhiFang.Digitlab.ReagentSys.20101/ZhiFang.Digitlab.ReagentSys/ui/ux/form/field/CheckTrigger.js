/**
 * 选择文本框
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.ux.form.field.CheckTrigger',{
    extend:'Ext.form.field.Trigger',
    alias:'widget.uxCheckTrigger',
	
	fieldLabel:'',
	triggerCls:'x-form-search-trigger',
	enableKeyEvents:false,
	editable:false,
	
	/**弹出的类名*/
	className:'',
	/**参数*/
	classConfig:{},
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		//支持双击input框弹出组件窗体
		me.inputEl.on({
			dblclick:function(p,data){
				me.onTriggerClick();
			}
		});
	},
	
	onTriggerClick:function(){
		var me = this;
		
		var bo = me.fireEvent('beforetriggerclick',me);
		if(bo === false){
			return;
		}
		
		if(!me.className){
			JShell.Msg.warning('请配置className参数!');
			return;
		}
		
		JShell.Win.open(me.className,Ext.apply(me.classConfig,{
			resizable:false,
			listeners:{
				accept:function(p,record){
					me.fireEvent('check',p,record);
				}
			}
		})).show();
	},
	
	initComponent:function(){
		var me = this;
		
		me.addEvents('check');
		
		me.callParent(arguments);
	}
});