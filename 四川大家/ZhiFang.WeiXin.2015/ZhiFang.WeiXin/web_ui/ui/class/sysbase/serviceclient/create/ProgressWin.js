/**
 * 进度面板
 * @author Jcall
 * @version 2016-08-27
 */
Ext.define('Shell.class.sysbase.serviceclient.create.ProgressWin',{
    extend: 'Ext.panel.Panel',
    title:'进度面板',
    
    width:180,
	height:300,
    
    autoScroll:true,
    
    /**布局方式*/
	layout:'anchor',
	/**每个组件的默认属性*/
    defaults:{
    	anchor:'100%',
    	xtype:'label'
    },
    initComponent:function(){
    	var me = this;
    	
    	me.dockedItems = [{
    		xtype:'toolbar',
			dock:'bottom',
			itemId:'buttonsToolbar',
			items:['->',{
				text:'关闭',
				itemId:'close',
				iconCls:'button-cancel',
				tooltip:'关闭',
				handler:function(){me.close();},
				hidden:true
			}]
		}];
		
		me.callParent(arguments);
    },
    
	/**开始*/
	onBegin:function(value){
		var me = this;
		value = value || '开始';
		me.add({
			html:'<div style="color:black;font-weight:bold;text-align:center;">' + value + '</div>'
		});
	},
	/**结束*/
	onEnd:function(value){
		var me = this;
		value = value || '结束';
		me.add({
			html:'<div style="color:black;font-weight:bold;text-align:center;">' + value + '</div>'
		});
		//显示按钮栏
		me.onShowToolbar();
	},
	
	/**显示遮罩*/
	onShowInfo:function(value){
		var me = this;
		me.add({
			html:'<div style="color:green;font-weight:bold;text-align:center;">' + value + '</div>'
		});
	},
	/**隐藏遮罩*/
	onHideError:function(value){
		var me = this;
		me.add({
			html:'<div style="color:red;font-weight:bold;text-align:center;">' + value + '</div>' + 
				'<div style="color:red;font-weight:bold;text-align:center;">操作终止</div>'
		});
		//显示按钮栏
		me.onShowToolbar();
	},
	/**显示按钮栏*/
	onShowToolbar:function(){
		var me = this,
			buttonsToolbar = me.getComponent('buttonsToolbar'),
			close = buttonsToolbar.getComponent('close');
			
		close.show();
	}
});