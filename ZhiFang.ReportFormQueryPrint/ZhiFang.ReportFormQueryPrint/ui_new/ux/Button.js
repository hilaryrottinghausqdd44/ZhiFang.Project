/**
 * 按钮类
 * @author Jcall
 * @version 2014-08-04
 * 
 * btype 按钮的类型,参照buttons对象
 * 每个按钮的css对应ui/css/buttons.css文件
 */
Ext.define('Shell.ux.Button',{
	extend:'Ext.button.Button',
	alias:'widget.uxbutton',
	
	prefix:'button-',
	
	buttons:{
		refresh:'刷新',
		
		add:'新增',
		edit:'修改',
		show:'查看',
		del:'删除',
		
		print:'打印',
		exp:'导出',
		config:'设置',
		check:'审核',
		uncheck:'取消审核',
		
		search:'高级',
		
		help:'帮助',
		
		save:'保存',
		saveas:'另存',
		reset:'重置',
		accept:'确定',
		cancel:'取消',
		back:'返回'
	},
	
	initComponent:function(){
		var me = this,
			prefix = me.prefix,
			info = me.btype ? me.buttons[me.btype] : null;
		
		if(info){
			me.itemId = me.itemId || me.btype;
			me.text = me.text != null ? me.text : info;
			me.iconCls = me.iconCls || prefix + me.btype;
			me.tooltip = '<b>' + (me.tooltip || info) + '</b>';
		}
			
		me.callParent(arguments);
	}
});