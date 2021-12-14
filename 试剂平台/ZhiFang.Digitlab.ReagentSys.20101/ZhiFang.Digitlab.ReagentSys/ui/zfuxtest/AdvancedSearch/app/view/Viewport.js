//alert(1);
Ext.define('ZhiFang.view.Viewport', {
    extend: 'Ext.container.Viewport',
    id: 'viewport',
    initComponent: function() {
    	var me=this;
    	me.layout = 'border';
    	me.border = false;
    	me.padding = '0 2 0 2';
    	me.items = [{
    		region: 'north',
    		xtype: 'toolbar',
    		padding: '0 20 0 0',
            height: 30,
            items: [
            	'->',
//            	{xtype: 'button',id:'image',text: '图片',iconCls: 'image-add'},
//            	{xtype: 'button',id:'label',text: '纯文本',iconCls: 'label-add'},
//                {xtype: 'button',id:'button',text: '按钮',iconCls: 'button-add'},
                '-',
                {xtype: 'button',id:'show',text: '浏览',iconCls: 'show'},
                {xtype: 'button',id:'save',text: '保存',iconCls: 'save'},
                {xtype: 'button',id:'edit',text: '修改',iconCls: 'edit'},
                {xtype: 'button',id:'delete',text: '删除',iconCls: 'delete'}
            ]
    	},{
    		region: 'center',
            layout: 'border',
            border: false,
            items:[{
            	region: 'center',
	            xtype: 'panel',
	            id:'FormDisplayArea',
	            title: '展示区域',
	            frame: true,
	            autoScroll: true,
	            layout:'fit',
            	items:[{
            		//frame: true,
            		xtype: 'formView'
            	}]
            },{
	            region: 'south',
	            xtype: 'formItemParamsView',
	            frame: true,
	            collapsible: true,
	            split: true,
	            sortableColumns: true,//不可排序
	            height: 200
	    	}]
    	},{
            region: 'east',
            xtype: 'formParamsView',
            frame: true,
            collapsible: true,
            split: true,
            width: 480

    	}];
        this.callParent(arguments);
    }
});