Ext.onReady(function(){
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	
	var panel = {
		xtype:'form',
		layout:'absolute',
		items:[{
			xtype:'textfield',fieldLabel:'服务地址',
			allowBlank:false,emptyText:'必填项,服务全路径',
			name:'UrlPath',itemId:'UrlPath',
			x:10,y:10,width:480
		},{
			xtype:'filefield',fieldLabel:'程序文件',
			allowBlank:false,
			emptyText:'请选择EXCEL文件',
			buttonConfig:{iconCls:'search-img-16',text:'选择'},
			name:'CodeFile',itemId:'CodeFile',
			x:10,y:40,width:480
		}],
		dockedItems:[{
			xtype:'toolbar',
			dock:'bottom',
			items:['->',{
				xtype:'button',text:'保存',
				iconCls:'build-button-save',
				handler:function(){this.ownerCt.ownerCt.save();}
			}]
		}],
		save:function(){
			var me = this;
			if (!me.getForm().isValid()) return;
			if (me.getForm().isValid()) {
				var values = me.getForm().getValues();
				var url= values.UrlPath;
	            me.getForm().submit({
	                url:url,
	                waitMsg:"提交中...",
	                success: function (form,action) {
	                	var result = action.result;
	                	if(result.success){
	                    	alertInfo('提交成功!');
	                	}else{
	                		alertError(result.ErrorInfo);
	                	}
	                },
	                failure:function(form,action){
						alertError('提交请求服务失败!');
					}
	            });
	        }
		}
	};
	
	//总体布局
	Ext.create('Ext.container.Viewport',{
		padding:2,
		layout:'fit',
		items:panel
	});
});