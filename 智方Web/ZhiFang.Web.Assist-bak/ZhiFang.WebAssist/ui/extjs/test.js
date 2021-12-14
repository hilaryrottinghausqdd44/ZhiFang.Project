Ext.Loader.setConfig({
	enabled:true,
	//disableCaching:false,
	disableCachingParam:'v',
	//获取当前版本参数
	getDisableCachingParamValue:function(){
		//return Ext.Date.now();
		return JShell.System.JS_VERSION;
	},
	paths:{
		'Shell':JShell.System.Path.UI,
		'Ext.ux':JShell.System.Path.ROOT + '/web_src/extjs/ux'
	}
});

Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	JShell.Window = Ext.create('Ext.window.Window',{
		layout:'fit',
		header:false,
		border:false,
		margin:0,
		padding:0,
		modal:true,
		plain:true,
		draggable:false,
		resizable:false,
		closeAction:'hide',
		close:function(){return JShell.Window.closeFun();}
	});
	
	//初始化系统时间
	JcallShell.System.Date.init(function(){
		var ClassName = JShell.Page.getParams(true).CLASSNAME;
		var panel = null;
		if(ClassName){
			panel = Ext.create(ClassName);
			//总体布局
			
		}else{
			panel = Ext.create('Ext.panel.Panel',{
				html:'<div style="margin:20px;text-align:center;color:red;font-weight:bold;">请传递ClassName参数</div>'
			});
		}
		var viewport = Ext.create('Ext.container.Viewport',{
			layout:'fit',
			//items:[panel],
			items:[{
				xtype:'panel',
				layout:'fit',
				border:false,
				bodyPadding:1,
				itemId:'panel',
				items:[panel],
				dockedItems:[{
					xtype:'toolbar',
					dock:'bottom',
					itemId:'bottomToolbar',
					items:[{
						xtype:'label',
						itemId:'SysTime',
						style:'color:rgb(4,64,140);fontWeight:bold;margin:3px 2px',
						text:''//系统时间：2015-01-01 10:12:14 星期一
					},'->',{
						xtype:'label',
						itemId:'Vesion',
						style:'color:rgb(4,64,140);fontWeight:bold;margin:3px 2px',
						text:'版本：' + JcallShell.System.JS_VERSION
					}]
				}]
			}]
		});
		
		//设置新系统时间
		function setSysTime(value){
			var me = this;
			var SysTime = viewport.getComponent('panel').getComponent('bottomToolbar').getComponent('SysTime');
			
			var v = '系统时间：' + (value || '无');
			SysTime.setText(v);
		};
		function changeSysTime(){
			var SysTime = JcallShell.Date.toString(JShell.System.Date.getDate(),false,false,true);
			setSysTime(SysTime);
			
			var timeout = setTimeout(function(){
				changeSysTime();
			},1000);
		}
		changeSysTime();
	});
});