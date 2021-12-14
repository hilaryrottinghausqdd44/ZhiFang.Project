//非构建类--通用型组件或控件--图标状态集合显示组件
Ext.Loader.setConfig({enabled: true});
Ext.Loader.setPath('Ext.zhifangux', getRootPath()+'/ui/zhifangux');
Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.Status', {
    extend:'Ext.panel.Panel',
    alias:'widget.status',
    requires:['Ext.zhifangux.ImgDiv2'],
    layout:'absolute',
    margin:0,
    border:true,
    serverUrl: '', //默认服务路径
    frame:false,
    pad:'16 0 0 0',
    id:'',
    postion:16,
    //公共属性
    arrowSrc:'../ui/css/images/extjs-icons/bullet_go.png',//小箭头图片的路径
	height1:148,
	width:800,
	size:26,
	//公共方法
    Load:function(value){
	    var me=this; 
	    me.getAppList(value);
    },
    //公共方法
    SetPadding:function(){
	    var me=this;
	    me.pad=me.size/2+8+' 0 0 0';
    },
    //公共方法
    SetWidth:function(width){
	    var me=this;
	    me.width=width;
    },
    //公共方法
    SetSize:function(value){
	    var me=this;
	    me.size=value;
    },
    //获取数据
    getAppList: function (id) {
        var me = this,
		data = null;
        //后台操作
        Ext.Ajax.request({
            async: false, //非异步
            url:me.serverUrl,
            params:{  
                id: id
            },  
            method: 'GET',
            timeout: 2000,
            success: function (response, opts) {
                var result = Ext.JSON.decode(response.responseText);
                if (result.success) {
                    data = result.ResultDataValue;
                   
                } else {
                    Ext.Msg.alert('提示', '获取应用列表信息失败！');
                }
            },
            failure: function (response, options) {
                Ext.Msg.alert('提示', '获取应用列表信息请求失败！');
            }
        });
       
        return data;
    },
    
  //添加所有的应用
    addAllApp: function (id) {
        var me = this;
        var data = me.getAppList(id);
        var count = data.count; //总数
        var list = data; //应用列表
        var panel = Ext.getCmp('showPanel');
        for (var i =0 ; i<list.length;i++ )
        {
    	    var app = null;
            app = me.createImg(list[i]);
            panel.add(app);
            if (i<list.length-1){
        	    var jt=null;
                jt=me.createjt(list[i]);
        	    panel.add(jt);
            }
         }
    },
  
    /**
	 * 常量设置
	 * @private
	 */
    initConstants:function(){
		var me = this;
		me.SetPadding();
    },
    /**
	 * 生成图片
	 * @private
	 * @return {}
	 */
    createImg:function(record){
    	var me = this;
    	var image = {
    	    layout:'absolute',
    	    panelType:2,
			imgUrl: record.image,
			disabled: record.stamp == 0 ? true : false,	 
            name:record.name,
            explain:record.explain,
            size:me.size
    	};
        var app1 = Ext.create('Ext.zhifangux.ImgDiv2', image);
    	image.listeners = { 	
    	};
    	return app1;
    },
    
    /**
	 * 生成箭头
	 * @private
	 * @return {}
	 */
    createjt:function(record){
    	var me = this;
    	var image = {
    	    xtype:'image',
    	    src:me.arrowSrc,
    		padding:me.pad
    	};
       image.listeners = { 	
    	   addListener:function(){
           }
       };
    	return image;
    },
    /**
	 * 生成面板
	 * @private
	 * @return {}
	 */
    createpanel:function(){
    	var me = this;
		var panel=Ext.create('Ext.panel.Panel', {
			id: 'showPanel',
			height:me.height1,
			width:me.width,
		    
	        layout: 'column',
			title: '',
			border:false
		});
    	return panel;
    },
    /**
     * 组装组件内容
     * @private
     */
    setAppItems:function(){
    	var me = this;
    	var items = [];
    	
    	var panel =me.createpanel();
    	if (panel)
    		items.push(panel);

    	me.items = items;
    },
    /**
     * 注册事件
     * @private
     */
    addAppEvents:function(){
    	var me = this;
    },
    /**
     * 初始化组件
     */
    initComponent:function(){
    	var me = this;
    	//常量设置
    	me.initConstants();
    	//注册事件
		me.addAppEvents();
    	//组装组件内容
    	me.setAppItems();
    	me.addAllApp();
    	this.callParent(arguments);
    }
});