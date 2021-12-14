/**
 * @author guohx	
 * @version 2020-04-21
 */
Ext.define('Shell.class.labStar.projectContrast.contrast.App',{
	extend:'Ext.panel.Panel',
	title:'历史对比',
	layout:'border',
    bodyPadding:1,
    bodyStyle:'background-color:#fff;',
	obresponse:'',
	parameter:'',
	selecturl:Shell.util.Path.rootPath+'/ServiceWCF/ReportFormService.svc/LabStarResultMhistory',
	/**渲染完后处理*/
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		//加载结果数据
		me.loadObresponseData(function(error){
			if(me.obresponse && me.obresponse.length > 0){
				me.createApps();
			}else{
				me.update('<div style="text-align:center;padding:40px 10px;font-weight:bold;">' + error + '</div>');
			}
		});
	},
	initComponent:function(){
		var me = this;
		//me.items = me.createApps();
		me.callParent(arguments);
	},
	//加载结果数据
	loadObresponseData:function(callback){
		var me = this;
		var url = location.search;//获取url中"?"符后的字串  		
		var str = url.substr(1);
		me.parameter = str;
		me.selecturl += "?"+str;
		Ext.Ajax.defaultPostHeader = 'application/json';
		Ext.Ajax.request({
			method: 'get',
			async: false,
		    url: me.selecturl,
			success: function(response2){
			    var response2 = Ext.decode(response2.responseText);
				var obresponse2 = response2.ResultDataValue ? JSON.parse(response2.ResultDataValue) : '';
				if(response2.success==true){
				    me.obresponse=obresponse2;
				}else{
					me.obresponse = null;
				}
				callback("没有找到数据！");
			},
			error : function(response){
				callback("服务错误！");
			}
		});
	},
	
	/**创建内部组件*/
	createApps:function(){
		var me = this;
		me.ProjectContrast = Ext.create('Shell.class.labStar.projectContrast.contrast.ProjectContrast',{
			itemId:'ProjectContrast',
			obresponse:me.obresponse,
			region:'center',
			//height:330, //border: false,
			autoScroll: true, split: true,
			collapsible: false, animCollapse: false
			
		});
		me.HistoryCompare = Ext.create('Shell.class.labStar.projectContrast.contrast.HistoryCompare',{
			itemId:'HistoryCompare',
			region:'south',
			height:350, //border: false,
			autoScroll: true, split: true,
			collapsible: false, animCollapse: false
			
		});
		
		me.add([me.ProjectContrast,me.HistoryCompare]);
	},
	
});

