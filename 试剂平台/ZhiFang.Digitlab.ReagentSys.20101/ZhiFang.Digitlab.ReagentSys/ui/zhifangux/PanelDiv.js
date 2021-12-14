//上面图片,下面文字的DIV组件
//适用于功能图标
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.PanelDiv', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.paneldiv',

    width:200,
    height:80,
    layout:'absolute',
    cls:'hand',
    frame:true,
    padding:0,
    bodyCls:'bg-white',
	mouseoverCls:'bg-orange',
    
    initComponent:function(){
    	var me = this,
    		nameMaxLength = 8,//标题最大长度
    		xplainMaxLength = 22;//说明最大长度
    	
    	var src = me.imgUrl,//图片
    		name = me.name,//标题
    		explain = me.explain;//说明
    		
    	if(name.length > 8)
    		name = name.substring(0,7) + "...";
    	if(explain.length > 22)
    		explain = explain.substring(0,21) + "...";
    		
    	me.items = [{//图片
			xtype:'image',src:src,width:48,height:48,
			x:2,y:5
		},{//标题
			xtype:'label',text:name,
			cls:'hand',
			style:{fontSize:'16px',color:'green',fontWeight:'bold'},
			x:55,y:5
		},{//说明
			xtype:'label',text:explain,style:{color:'blue'},
			cls:'hand',
			x:55,y:35
		}];
		
		//添加事件，别的地方就能对这个事件进行监听
		this.addEvents('appClick');
		this.addEvents('appMouseover');
		this.addEvents('appMouseout');
		
		me.listeners = {
        	click:{
        		 element:'el',
        		 fn:function(){
        		 	var me = Ext.getCmp(this.id);
        		 	me.fireEvent('appClick');
        		 }
        	},
        	mouseover:{
    			element:'el',
    			fn:function(){
    				var me = Ext.getCmp(this.id);
    				if(me.mouseoverCls){
    					me.addBodyCls(me.mouseoverCls);
    				}
    				me.fireEvent('appMouseover');
    			}
    		},
    		mouseout:{
    			element:'el',
    			fn:function(){
    				var me = Ext.getCmp(this.id);
					if(me.bodyCls && me.mouseoverCls){
						me.removeBodyCls(me.mouseoverCls);
						me.addBodyCls(me.bodyCls);
					}
    				me.fireEvent('appMouseout');
    			}
    		}
        };
		
    	this.callParent(arguments);
    },
    afterRender: function() {
        var me = this;
        //提示
		if(!me.tooltip){
        	me.tooltip = Ext.create('Ext.ToolTip', {
        		title:me.name,
				html:me.explain,
				target:me.getEl()
			});
        }
        
        me.callParent(arguments);
    }
});