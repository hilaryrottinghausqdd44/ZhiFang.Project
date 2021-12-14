Ext.define('MyTest',{
	extend:'Ext.panel.Panel',
	alias:'widget.mytest',
	title:'测试的面板',
	width:500,
	height:250,
	bodyPadding:10,
	initComponent:function(){
		var me = this;
		me.items = [{
			xtype:'textfield',
			itemId:'fileurl',
			labelWidth:60,
			width:450,
			fieldLabel:'文件路径'
		},{
			xtype:'filefield',
			itemId:'file',
			fieldLabel:'模块图标',
			width:200,
			labelWidth:60,
			buttonConfig:{iconCls:'search-img-16',text:''},
			listeners:{change:function(field,value){me.updateFile();}}
		},{
			xtype:'button',
			text:'下载文件', 
			handler:function(but){
				var fileurl = me.getComponent('fileurl').value;
				download_file(fileurl);
			}
		}];
		me.callParent(arguments);
	},
	updateFile:function(){
		Ext.Msg.show({
			title:'提示',
			msg:'开始上传',
			progressText:'上传进度',
			width:300,  
			progress:true,  
			closable:false
		});
		
		var fn = function(v){
			if(v == 10){
				Ext.MessageBox.hide();
			}else{
				Ext.MessageBox.updateProgress(i,Math.round(10*v)+"% 完成"); 
			}
		};
		for(var i=1;i<11;i++){  
			setTimeout(f(i),i*500);//从点击时就开始计时，所以500*i表示每500ms就执行一次  
		}
	}
});