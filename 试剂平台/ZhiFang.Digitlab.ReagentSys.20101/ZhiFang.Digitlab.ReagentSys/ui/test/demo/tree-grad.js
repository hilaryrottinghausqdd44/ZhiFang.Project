Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	//获取随机字符串
	var getRandomStr = function(len){
        var x = "123456789poiuytrewqasdfghjklmnbvcxzQWERTYUIPLKJHGFDSAZXCVBNM";
        var str = "";
        for (var i = 0; i < len; i++) {
            str += x.charAt(Math.ceil(Math.random() * 100000000) % x.length);
        }
        return str;
    };
    //说去随机数字
    var getRandom = function(min, max){
        return parseInt(Math.random() * (max - min) + min);
    };
	//获取随机的列表数据
	var getListData = function(num){
		var data = [];
		var count = num || 100;//列表数据量
		for(var i=0;i<count;i++){
			data.push({
				name:getRandomStr(8),
				code:getRandomStr(12),
				type:getRandom(1,10)
			});
		}
		return data;
	};
	
	var store = Ext.create('Ext.data.Store',{
		fields:['name','code','type'],
		data:getListData(10)
	});
	var checkbox = Ext.create('Ext.form.Checkbox',{
    	xtype:'checkbox',
    	itemId:'removeRecord',
		boxLabel:'拖动后消除记录'
    });
	var grid = Ext.create('Ext.grid.Panel',{
		region:'west',title:'列表',width:325,
		collapsible:true,split:true,
		enableDragDrop:true,stripeRows:true,
        viewConfig:{
            plugins:{
                ddGroup:'GridExample',
                ptype:'gridviewdragdrop',
                enableDrop:false
            }
        },
        store:store,
        columns:[
        	{text:'名称',dataIndex:'name'},
        	{text:'编码',dataIndex:'code'},
        	{text:'类型',dataIndex:'type'}
        ],
        selModel:Ext.create('Ext.selection.RowModel',{singleSelect:true}),
        tbar:[checkbox]
    });
    //表单
    var form = Ext.create('Ext.form.Panel',{
        region:'center',title:'信息表单',
        bodyStyle:'padding: 10px;background-color:#DFE8F6',
        labelWidth:100,margins:'0 0 0 3',
        items:[
        	{xtype:'textfield',fieldLabel:'名称',name:'name'},
        	{xtype:'textfield',fieldLabel:'编码',name:'code'},
        	{xtype:'textfield',fieldLabel:'类型',name:'type'}
		]
    });
	var panel = Ext.create('Ext.panel.Panel',{
		xtype:'panel',
		header:false,
		layout:'border',
		items:[grid,form]
	});
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
	
	var formPanelDropTargetEl =  form.body.dom;

    var formPanelDropTarget = Ext.create('Ext.dd.DropTarget',formPanelDropTargetEl,{
        ddGroup:'GridExample',
        notifyEnter:function(ddSource,e,data){
            form.body.stopAnimation();
            form.body.highlight();
        },
        notifyDrop:function(ddSource,e,data){
            var selectedRecord = ddSource.dragData.records[0];
            form.getForm().loadRecord(selectedRecord);
            var bo = checkbox.getValue();
            bo && ddSource.view.store.remove(selectedRecord);
            return true;
        }
    });
});