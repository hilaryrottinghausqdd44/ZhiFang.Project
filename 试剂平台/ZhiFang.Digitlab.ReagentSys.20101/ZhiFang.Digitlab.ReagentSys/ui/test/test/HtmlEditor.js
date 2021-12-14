Ext.ns('Ext.zhifangux');
Ext.define('Ext.zhifangux.HtmlEditor',{
	extend:'Ext.form.HtmlEditor',
	alias:'widget.zhifangux_htmleditor',
	initComponent:function(){
		var me = this;
		me.callParent(arguments);
		//添加图片上传功能图标
		me.toolbar.add('-',{
			itemId: 'updatefile',
            cls: Ext.baseCSSPrefix + 'btn-icon',
            iconCls: 'search-img-16',//baseCSSPrefix + 'edit-forecolor',
            overflowText: '',
            tooltip: '<h1>插入图片</h1><a>插入图片到编辑器</a>',
            tabIndex:-1,
            handler:function(but){
            	me.openUpdateFileWin();
            }
		});
	},
	openUpdateFileWin:function(){
		var me = this;
		var maxHeight = document.body.clientHeight*0.98;
        var maxWidth = document.body.clientWidth*0.98;
		var win = Ext.create('Ext.form.Panel',{
			maxWidth:maxWidth,
            maxHeight:maxHeight,
            autoScroll:true,
            modal:true,//模态
            floating:true,//漂浮
            closable:true,//有关闭按钮
            draggable:true,//可移动
			title:'插入图片',
			width: 400,
            height: 150,
            bodyPadding:10,
            items:[{
            	xtype:'filefield',
                fieldLabel:'选择文件',
                width:370,labelWidth:55,
                name:'file',
                buttonConfig:{iconCls:'search-img-16',text:''}
            },{
            	xtype:'label',itemId:'info',width:380
            }],
            dockedItems:[{
            	xtype:'toolbar',
            	dock:'bottom',
            	itemId:'bottomtoolbar',
            	items:['->',{
            		xtype:'button',
					text:'插入',
					iconCls:'build-button-save',
					handler:function(but){
						var com = but.ownerCt.ownerCt;
						com.getComponent('info').setText("");
						var text = com.getComponent('file').lastValue;
						if(text == ""){
							com.getComponent('info').setText("<center>请选择一个文件!</center>");
						}else if(text.split(".").slice(-1) != "jpg"){
							com.getComponent('info').setText("<center>文件格式不对!</center>");
						}else{
							com.close();
						}
						//me.save();
					}
            	},{
            		xtype:'button',
					text:'取消',
					iconCls:'build-button-cancel',
					handler:function(but){but.ownerCt.ownerCt.close();}
            	}]
            }]
		});
		win.show();
	},
	save:function(com){
		com.submit({
            waitMsg: '图片正在插入..',
            url: "?sign=HTMLEditor", //点击插入执行的方法,将图片保存到服务器上
            success: function(form, action){
                
            },
            failure: function(form, action) {
                form.reset();
              	form.getComponent('file').enable();
              	form.getComponent('info').setText("<center>服务失败!</center>");
            }
        });
	}
});