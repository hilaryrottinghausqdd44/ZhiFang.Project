/**
 * 根据选择的库房加载货架按钮
 * @author liangyl
 * @version 2018-03-09
 */
Ext.define('Shell.class.qms.equip.templet.emaintenancedata.Btn', {
    extend:'Shell.ux.panel.AppPanel',
    alias: 'widget.uxToolBtn',
    requires: [
	    'Shell.ux.toolbar.Button',
	    'Shell.ux.form.field.CheckTrigger'

	],
	margins: '0 0 1 0', 
	bodyPadding: '0 0 0 0',
	width:200,
	TxtLen:200,
    afterRender: function() {
		var me = this;
		me.callParent(arguments);
		var buttonsToolbar=me.getComponent('buttonsToolbar');
		var BtnLeft=me.getComponent('BtnLeft');
		var BtnRight=me.getComponent('BtnRight');
		BtnLeft.setVisible(false);
		BtnRight.setVisible(false);
		me.on({
			leftClick:function( com,e,target){
				buttonsToolbar.scrollBy(-100,2,false);
			},
			rightClick:function( com,e,target){
				buttonsToolbar.scrollBy(100,2,false);	
			},
			resize : function(com,  width,  height,  oldWidth,  oldHeight,  eOpts ){
		        if(me.TxtLen>width-50){
					BtnLeft.setVisible(true);
					BtnRight.setVisible(true);
				}else{
					BtnLeft.setVisible(false);
					BtnRight.setVisible(false);
				}
			    buttonsToolbar.setWidth(width-60);
			}
		});
	},
	initComponent: function() {
		var me = this;
		me.addEvents('leftClick', 'rightClick');
		//内部组件
		me.items = me.createItems();
		me.callParent(arguments);
	},
	createBtn:function(){
		var me = this;	
		var items= [];
		var bbar = Ext.create('Shell.ux.toolbar.Button', {
			width:me.width-60,
			region: 'west',//split: true,
			itemId: 'buttonsToolbar',border:false,
			autoScroll : 'auto',
            bodyStyle : 'overflow-x:hidden; overflow-y:scroll',
		    items: items
		});
		return bbar;
	},
	/**创建内部组件*/
	createItems: function() {
		var me = this;
		me.BtnLeft = Ext.create('Ext.button.Button', {
			text:'',tooltip:'向左移动',iconCls:'button-right',
			itemId: 'BtnLeft',region: 'west',width:20,//maxWidth:25,split: true,
		    hidden:true,
			listeners: {
		    	click:function(com,e,target){
		    		me.fireEvent('leftClick', com,e,target);
		    	}
		    }
		});
		
	    me.BtnRight = Ext.create('Ext.button.Button', {
			text:'',tooltip:'向右移动',iconCls:'button-left',
			itemId: 'BtnRight',region: 'center',width:18,maxWidth:20,
			hidden:true,
			listeners: {
		    	click:function(com,e,target){
		    		me.fireEvent('rightClick', com,e,target);
		    	}
		    }
		});
		
		var items=[me.BtnLeft,me.createBtn(),me.BtnRight];
//		var items=[me.createBtn()];
//		if(me.getWidth()>200){
//			items.splice(0,0,[me.BtnLeft]);
//			items.splice(2,0,[me.BtnRight]);
//		}
		return items;
	}
});