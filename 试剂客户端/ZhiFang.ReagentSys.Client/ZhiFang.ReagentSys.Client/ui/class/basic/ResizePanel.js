/**
 * 可变大小面板
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.basic.ResizePanel',{
    extend:'Ext.panel.Panel',
    header:false,
    layout:'fit',
    /**功能栏位置*/
    toolbarDock:'top',
    title:'',
    afterRender:function(){
    	var me = this;
    	me.callParent(arguments);
    	
    	new Ext.KeyMap(me.getEl(),{  
			key:Ext.EventObject.ESC,
			fn:function(k,e){
				if(me.isMax){
					me.onMinimize();
					setTimeout(function(){me.focus();},500);
				}else{
					me.onMaximize();
					setTimeout(function(){me.focus();},500);
				}
			},
			scope:this,
			defaultEventAction: "stopEvent"
		}).enable();
		setTimeout(function(){me.focus();},500);
    },
    initComponent:function(){
    	var me = this;
    	me.dockedItems = me.createDockedItems();
    	me.callParent(arguments);
    },
    createDockedItems:function(){
    	var me = this,
    		isFloating = me.isFloating(),
    		items = [];
    		
    	items = [{
			xtype:'label',
			style:'margin:2px;color:#04408c;font-weight:bold',
			text:me.title
		},'->',{
			xtype:'tool',
			type:'refresh',
			tooltip:'<b>刷新数据</b>',
			handler:function(){me.onRefresh(true);}
		}];
    		
    	if(isFloating){
    		items.push({
    			xtype:'tool',
    			type:'close',
    			itemId:'close',
    			tooltip:'<b>关闭</b>',
    			handler:function(){me.close();}
    		});
    	}else{
    		items.push({
				xtype:'tool',
				type:'maximize',
				itemId:'maximize',
				tooltip:'<b>最大化</b>',
				handler:function(){me.onMaximize();}
			},{
				xtype:'tool',
				type:'minimize',
				itemId:'minimize',
				hidden:true,
				tooltip:'<b>还原(ESC)</b>',
				handler:function(){me.onMinimize();}
			});
    	}
    	
    	return [{
    		xtype:'toolbar',
    		itemId:'buttonToolbar',
    		style:'padding:2px',
    		dock:me.toolbarDock,
    		items:items
    	}];
    },
    onRefresh:function(){
    	JShell.Msg.warning('请重写刷新数据方法onRefresh!');
    },
    onMaximize:function(){
    	var me = this,
    		buttonToolbar = me.getComponent('buttonToolbar'),
    		maximize = buttonToolbar.getComponent('maximize'),
    		minimize = buttonToolbar.getComponent('minimize'),
    		size = Ext.getBody().getSize(),
    		ownerCt = me.ownerCt,
    		index = ownerCt.items.indexOf(me);
    		
    	me.isMax = true;
    		
    	maximize.hide();
    	minimize.show();
    	me.resourceOwnerCt = ownerCt;
    	me.resourceIndex = index;
    	
    	me.myX = me.x;
    	me.myY = me.y;
    	if(me.x != null) me.x = 1;
    	if(me.y != null) me.y = 1;
    	me.setPosition(me.x,me.y);
    	
    	JShell.Window.setSize(size.width - 5,size.height - 5);
    	JShell.Window.removeAll();
    	JShell.Window.add(me);
    	JShell.Window.show();
    	JShell.Window.closeFun = function(){return me.onMinimize();};
    },
    onMinimize:function(){
    	var me = this,
    		buttonToolbar = me.getComponent('buttonToolbar'),
    		maximize = buttonToolbar.getComponent('maximize'),
    		minimize = buttonToolbar.getComponent('minimize'),
    		ownerCt = me.resourceOwnerCt,
    		index = me.resourceIndex;
    		
    	me.isMax = false;
    	
    	if(me.myX != null) me.x = me.myX;
    	if(me.myY != null) me.y = me.myY;
    	me.setPosition(me.x,me.y);
    		
    	minimize.hide();
    	maximize.show();
    	
    	ownerCt.insert(index,me);
    	JShell.Window.hide();
    }
});