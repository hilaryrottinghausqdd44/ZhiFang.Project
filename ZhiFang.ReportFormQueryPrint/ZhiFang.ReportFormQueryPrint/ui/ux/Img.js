/**
 * 图片组件
 * @author Jcall
 * @version 2014-08-06
 */
Ext.define('Shell.ux.Img',{
	extend:'Ext.Img',
	alias:'widget.uximage',
	
	afterRender:function(){
		var me = this;
		me.on({
			click:{
				element:'el',
				fn:function(e,t,eOpts){
					me.fireEvent('click',me,e,t);
				}
			},
			dblclick:{
				element:'el',
				fn:function(e,t,eOpts){
					me.fireEvent('dblclick',me,e,t);
				}
			}
		});
	},
	initComponent:function(){
		var me = this;
		me.addEvents('click','dblclick');
		me.callParent(arguments);
	}
});