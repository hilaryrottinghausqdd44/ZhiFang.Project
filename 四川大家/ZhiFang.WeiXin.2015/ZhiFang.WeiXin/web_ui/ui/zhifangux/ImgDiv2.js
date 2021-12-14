/**
 * 上面图片,下面文字的DIV组件
 * 适用于功能图标
 * @param
 * id：组件ID
 * imgUrl：图片路径
 * name：名称
 * explain：说明，如果为空就与名称一样
 * size：图片型号（大小），例如16、32、48、64，默认32
 * panelType：//类型开关,1:普通；2、小号，默认1
 * hasClose：是否有删除图标，默认false
 * hasDrop：是否有下拉图标，默认false
 * autoSelected:：是否默认处于选中状态,默认false
 * clickSelected：点击处于选中状态，默认false
 * selectedCls：选中时的css，默认imgdiv-orange
 * unSelectedCls：未选中时的css，默认bg-white
 * closeShowCls：关闭按钮可见状态的css，默认imgdiv-close-show-16
 * closeHIdeCls：关闭按钮不可见状态的css，默认imgdiv-close-hide-16
 * dropShowCls：下拉按钮可见状态的css,默认imgdiv-drop-show-16-16
 * 
 * 对外公开事件
 * closeClick//点击删除按钮
 * dropClick//点击下拉按钮
 * imgClick//点击图片
 * 
 * 对外公开方法
 * hasSelected//选中时调用，更改背景
 * notselected//未选中，更换背景
 * setMouseoverCls//设置为悬浮背景
 * setDefaultCls//设置为默认背景
 */

Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.ImgDiv2', {
    extend: 'Ext.panel.Panel',
    alias: 'widget.imgdiv2',
    layout:'absolute',
    //cls:'hand',
    margin:4,
    border:false,
    frame:false,
    padding:0,
    
    panelType:1,//类型开关,1:普通；2、小号
    
	imgUrl: '',
	name: '',
	explain: '',
	size: 32,
	hasClose: false,
	hasDrop:false,
	autoSelected: false,
	clickSelected:false,
	
	selectedCls:'imgdiv-orange-32',
	unSelectedCls:'bg-white',
	
	closeShowCls:'imgdiv-close-show-16',
	closeHideCls:'imgdiv-close-hide-16',
	
	dropShowCls:'imgdiv-drop-show-16-16',
	
	/**
     * 【选中时调用，更改背景】
     * @public
     */
    hasSelected:function(){
    	var me = this;
    	me.setMouseoverCls();
    },
    
    /**
     * 【未选中，更换背景】
     * @public
     */
    notselected:function(){
    	var me = this;
    	me.setDefaultCls();
    },
    
    /**
     * 【设置为悬浮背景】
     * @public
     */
    setMouseoverCls:function(){
    	var me = this;
    	if(me.selectedCls && me.unSelectedCls){
			me.removeBodyCls(me.unSelectedCls);
			me.addBodyCls(me.selectedCls);
		}
    },
    
    /**
     * 【设置为默认背景】
     * @public
     */
    setDefaultCls:function(){
    	var me = this;
    	if(me.unSelectedCls && me.selectedCls){
			me.removeBodyCls(me.selectedCls);
			me.addBodyCls(me.unSelectedCls);
		}
    },
    
	/**
	 * 常量设置
	 * @private
	 */
	initConstants:function(){
		var me = this;
    	if(me.panelType == 1){
    		me.IMG_LEFT_WIDTH = 16;//图片左边的距离
	    	me.IMG_RIGHT_WIDTH = 16;//图片右边的距离
	    	me.IMG_TOP_HIGHT = 10;//图片上边的距离
	    	
	    	me.ALL_SIZE = me.size + me.IMG_LEFT_WIDTH + me.IMG_RIGHT_WIDTH;//总大小
	    		
	    	me.TEXT_LEFT_WIDTH = 5;//文字左边的距离
	    	me.TEXT_RIGHT_WIDTH = 5;//文字右边的距离
	    	me.TEXT_TOP_HIGHT = 2;//文字上边与图片的距离
	    	me.TEXT_BOTTOM_HEIGHT = 10;//文字下边的距离
	    	me.TEXT_SIZE = '11px';//文字大小
	    		
	    	me.DELETE_WIDTH = 16;//删除按钮的宽度
	    	me.DELETE_HIGHT = 16;//删除按钮的高度
	    	me.DELETE_X = me.ALL_SIZE - me.DELETE_WIDTH - 2;//删除按钮的X
	    	me.DELETE_Y = 0;//删除按钮的Y
	    	
	    	me.DROP_WIDTH = 16;//下拉按钮的宽度
	    	me.DROP_HIGHT = 16;//下拉按钮的高度
	    	me.DROP_X = me.ALL_SIZE - me.DROP_WIDTH - 2;//下拉按钮的X
	    	me.DROP_Y = me.ALL_SIZE - me.DROP_HIGHT - 2;//下拉按钮的Y
	    	
    	}else if(me.panelType == 2){
    		me.IMG_LEFT_WIDTH = 11;//图片左边的距离
    		me.IMG_RIGHT_WIDTH = 11;//图片右边的距离
    		me.IMG_TOP_HIGHT = 4;//图片上边的距离
	    	
	    	me.ALL_SIZE = me.size + me.IMG_LEFT_WIDTH + me.IMG_RIGHT_WIDTH;//总大小
	    		
	    	me.TEXT_LEFT_WIDTH = 2;//文字左边的距离
	    	me.TEXT_RIGHT_WIDTH = 2;//文字右边的距离
	    	me.TEXT_TOP_HIGHT = 2;//文字上边与图片的距离
	    	me.TEXT_BOTTOM_HEIGHT = 4;//文字下边的距离
	    	me.TEXT_SIZE = '11px';//文字大小
	    		
	    	me.DELETE_WIDTH = 16;//删除按钮的宽度
	    	me.DELETE_HIGHT = 16;//删除按钮的高度
	    	me.DELETE_X = me.ALL_SIZE - me.DELETE_WIDTH - 2;//删除按钮的X
	    	me.DELETE_Y = 0;//删除按钮的Y
	    	
	    	me.DROP_WIDTH = 7;//下拉按钮的宽度
	    	me.DROP_HIGHT = 10;//下拉按钮的高度
	    	me.DROP_X = me.ALL_SIZE - me.DROP_WIDTH - 4;//下拉按钮的X
	    	me.DROP_Y = me.ALL_SIZE - me.DROP_HIGHT - 4;//下拉按钮的Y
	    	me.dropShowCls = 'imgdiv-drop-show-7-10';
    	}
    	
    	me.width = me.ALL_SIZE;
    	me.height = me.ALL_SIZE;
    	me.style = {textAlign:'center'};
	},
	
	/**
	 * 初始化选中时的样式
	 * @private
	 */
	initAppCls:function(){
		var me = this,
			size = me.size;
		
		if(size == 32){
			me.TEXT_SIZE = '12px';//文字大小
			me.selectedCls = 'imgdiv-orange-32';
		}else if(size == 48){
			me.TEXT_SIZE = '12px';//文字大小
			me.selectedCls = 'imgdiv-orange-48';
		}else if(size == 64){
			me.TEXT_SIZE = '12px';//文字大小
			me.selectedCls = 'imgdiv-orange-64';
		}else{
			me.selectedCls = 'bg-orange';
		}
		//小号图标
		if(me.panelType == 2)
			me.selectedCls += "-s";
		
		
	},
	
	/**
	 * 生成图片
	 * @private
	 * @return {}
	 */
    createImg:function(){
    	var me = this;
    	var image = {
    		xtype: 'image',
    		id: me.id + '-img',
    		cls: 'hand',
    		src: me.imgUrl,
    		width: me.size,
    		height: me.size,
			x: me.IMG_LEFT_WIDTH,
			y: me.IMG_TOP_HIGHT
    	}
    	image.listeners = { 
    		click:{
    			element:'el',
    			fn:function(){
    				var id = this.id.substring(0,this.id.length-4);
		    		var me = Ext.getCmp(id);
    				me.fireEvent('imgClick');
    			}
    		}
    	}
    	return image;
    },
    
    /**
     * 生成文字
     * @private 
     * @return {}
     */
    createText:function(){
    	var me = this;
    		name = me.name;
    		
    	var MAXLENGTH = me.size / 16 + 2;
    	
    	if(me.hasDrop)
    		MAXLENGTH--;
    	
    	var length = name.length;
    	
    	if(length > MAXLENGTH)
    		name = name.substring(0,MAXLENGTH-1) + "...";
    		
    	var text = {
    		xtype: 'label',
    		id: me.id + '-text',
    		text: name,
			width: me.ALL_SIZE - me.TEXT_LEFT_WIDTH - me.TEXT_RIGHT_WIDTH,
			style: {fontSize:me.TEXT_SIZE},
			x: me.TEXT_LEFT_WIDTH,
			y: me.size + me.IMG_TOP_HIGHT + me.TEXT_TOP_HIGHT
    	}
    	return text;
    },
    
    /**
     * 生成关闭按钮
     * @private
     * @return {}
     */
    createCloseBut:function(){
    	var me = this;
    	var but = null;
    	//alert(me.closeHideCls);
    	if(me.hasClose){//需要关闭按钮
    		but = {
	    		xtype: 'image',
	    		id: me.id + '-close',
	    		cls: me.closeHideCls,
	    		width: me.DELETE_WIDTH,
	    		height: me.DELETE_HIGHT,
				x: me.DELETE_X,
				y: me.DELETE_Y
	    	};
	    	
	    	but.listeners = {
	    		click:{
	    			element:'el',
	    			fn:function(){
	    				var id = this.id.substring(0,this.id.length-6);
		    			var me = Ext.getCmp(id);
        		 		me.fireEvent('closeClick');
		    		}
	    		},
	    		mouseover:{
	    			element:'el',
	    			fn:function(){
	    				this.removeCls(me.closeHideCls);
	    				this.addCls(me.closeShowCls);
		    		}
	    		},
	    		mouseout:{
	    			element:'el',
	    			fn:function(){
	    				this.removeCls(me.closeShowCls);
	    				this.addCls(me.closeHideCls);
		    		}
	    		}
	    	}
    	}
    	return but; 
    },
    
    /**
     * 生成下拉按钮
     * @private
     * @return {}
     */
    createDropBut:function(){
    	var me = this;
    	var but = null;
    	if(me.hasDrop){//需要下拉按钮
    		but = {
	    		xtype: 'image',
	    		id: me.id + '-drop',
	    		cls: me.dropShowCls,
	    		width: me.DROP_WIDTH,
	    		height: me.DROP_HIGHT,
				x: me.DROP_X,
				y: me.DROP_Y
	    	};
	    	
	    	but.listeners = {
	    		click:{
	    			element:'el',
	    			fn:function(){
	    				var id = this.id.substring(0,this.id.length-5);
		    			var me = Ext.getCmp(id);
        		 		me.fireEvent('dropClick');
		    		}
	    		}
	    	}
            
    	}
    	return but;
    },
    
    /**
     * 弹出提示框
     * @private
     */
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
    },
    
    /**
     * 注册事件
     * @private
     */
    addAppEvents:function(){
    	var me = this;
    	me.addEvents('closeClick');//删除按钮点击
		me.addEvents('dropClick');//下拉按钮点击
		me.addEvents('imgClick');//点击图片
    },
    
    /**
     * 组件监听
     * @private
     */
    appListeners:function(){
    	var me = this;
    	me.listeners = {
    		mouseover:{
    			element:'el',
    			fn:function(){
    				var me = Ext.getCmp(this.id);
    				me.setMouseoverCls();//悬浮背景
    				me.fireEvent('appMouseover');
    			}
    		},
    		mouseout:{
    			element:'el',
    			fn:function(){
    				var me = Ext.getCmp(this.id);
					me.setDefaultCls();//默认背景
    				me.fireEvent('appMouseout');
    			}
    		}
    	}
    },
    
    /**
     * 组装组件内容
     * @private
     */
    setAppItems:function(){
    	var me = this;
    	var items = [];
    	//图片
    	var image = me.createImg();
    	if(image)
    		items.push(image);
    		
    	//文字
    	var text = me.createText();
    	if(text)
    		items.push(text);
    	
    	//删除按钮
    	var closeBtn = me.createCloseBut();
    	if(closeBtn)
    		items.push(closeBtn);
    	
    	//下拉按钮
    	var dropBtn = me.createDropBut();
    	if(dropBtn)
    		items.push(dropBtn);
    		
    	me.items = items;
    },
    
    /**
     * 初始化组件
     */
    initComponent:function(){
    	var me = this;
    	//常量设置
    	me.initConstants();
    	//初始化选中时的样式
		me.initAppCls();
    	//注册事件
		me.addAppEvents();
    	
    	//组装组件内容
    	me.setAppItems();
    	
    	//组件监听
    	me.appListeners();
    	
    	this.callParent(arguments);
    }
});