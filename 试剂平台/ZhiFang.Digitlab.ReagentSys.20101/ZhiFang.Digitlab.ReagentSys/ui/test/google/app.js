Ext.onReady(function(){	
	Ext.QuickTips.init();//初始化后就会激活提示功能
	Ext.Loader.setConfig({enabled: true});//允许动态加载
	var count = 0;
	var panel2 = {
		xtype:'tabpanel',
		title:'google测试',
		items:[{
			xtype:'panel',
			title:'面板1',
			html:'asdasdasd',
			bbar:[{text:'保存'}]
		},{
			xtype:'panel',
			title:'面板2',
			html:'asaaaaaa',
			bbar:[{text:'更新'}]
		}],
		bbar:[{
			text:'新增面板',handler:function(but){
				var o = but.ownerCt.ownerCt;
				count++;
				var p = o.add({
					title:'新增面板' + count,
					html:"<html><body><iframe id='tabpanel_"+count+"' src='test.html' style='height:100%;width:100%' frameborder='no'></iframe></body></html>",
					bbar:[{text:'按钮'+count}]
				});
				o.setActiveTab(p);
			}
		},{
			text:'新增页面',handler:function(but){
				var o = but.ownerCt.ownerCt;
				count++;
				var p = o.add({
					title:'新增面板' + count,
					//html:"<html><body><iframe id='tabpanel_"+count+"' src='test2.html' style='height:100%;width:100%' frameborder='no'></iframe></body></html>",
					bbar:[{
						text:'按钮'+count,
						handler:function(but){
							var o = but.ownerCt.ownerCt;
							var html = "<html><body><iframe id='tabpanel_"+count+"' src='test.html' style='height:100%;width:100%' frameborder='no'></iframe></body></html>";
							o.update(html);
						}
					}],
					listeners:{
						afterlayout:function(con,layout){
							if(!con.hasLayout){
								con.hasLayout= true;
								var html = "<html><body><iframe id='tabpanel_"+count+"' src='test.html' style='height:100%;width:100%' frameborder='no'></iframe></body></html>";
								con.update(html);
							}
						}
					}
				});
				o.setActiveTab(p);
			}
		},{
			text:'弹出窗口',
			handler:function(but){
				window.open("test.html","弹出窗口");
			}
		},{
			text:'弹出iframe2',
			handler:function(but){
				window.open("test2.html","弹出窗口");
			}
		}]
	};
	
	var panel = {
		xtype:'panel',
		title:'google测试',
		//html:"<html><body><iframe id='tabpanel_1' src='test.html' style='height:100%;width:100%' frameborder='no'></iframe></body></html>"
		listeners:{
			afterlayout:function(con,layout){
				if(!con.hasLayout){
					con.hasLayout= true;
					var html = "<html><body><iframe id='tabpanel' src='test.html' style='height:100%;width:100%' frameborder='no'></iframe></body></html>";
					con.update(html);
				}
			}
		}
	};
	//总体布局
	var viewport = Ext.create('Ext.container.Viewport',{
		layout:'fit',
		items:[panel]
	});
	Ext.panel.Panel
});