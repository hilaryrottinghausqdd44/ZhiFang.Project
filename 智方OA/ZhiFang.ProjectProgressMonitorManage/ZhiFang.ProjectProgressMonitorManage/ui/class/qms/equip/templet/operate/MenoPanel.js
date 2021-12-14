/**
 * 填表说明
 * @author liangyl
 * @version 2017-11-09
 */
Ext.define('Shell.class.qms.equip.templet.operate.MenoPanel', {
	extend: 'Ext.panel.Panel',
	title: '填表说明',
    layout:'fit',
	
	autoScroll:true,
	
	/**获取数据服务路径*/
	selectInfoUrl: '/QMSReport.svc/ST_UDTO_SearchETempletById',
	    
    /**加载数据提示*/
	lodingText:JShell.Server.LOADING_TEXT,
	
	/**信息数据*/
	InfoData: '',
	/**信息ID*/
	PK:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		if(me.PK){
			//初始化Html页面
			me.initHtml();
		}
	},
	
	initComponent: function() {
		var me = this;
		
		if(me.PK){
			me.html = 
			'<div class="loading-div">' +
				'<img src="' + JShell.System.Path.UI + '/css/images/sysbase/loading3.gif">' +
				'<div style="padding-top:10px;">页面加载中</div>' +
			'</div>';
		}
		me.callParent(arguments);
	},
	
	initHtml:function(){
		var me = this;
		
		//显示遮罩
		me.showMask(me.lodingText);
		me.loadInfoData(function(data){
			me.InfoData = data;//信息数据
			me.initHtmlContent();//初始化HTML页面
		});
	},
	/**初始化HTML页面*/
	initHtmlContent: function() {
		var me = this,
			html = [];
			
		//获取任务HTML
		html.push(me.getInfoHtml());
	
		//更新HTML内容
		me.update(html.join(''));
		
		//隐藏遮罩
		me.hideMask();
		
	},
	/**加载信息*/
	loadInfoData:function(callback){
		var me = this,
			url = JShell.System.Path.getRootUrl(me.selectInfoUrl);
			
		//备注
		var fields = [
			'Id','Comment','CName'
		];
		
		fields = "ETemplet_" + fields.join(",ETemplet_");
		
		url += "?isPlanish=true&fields=" + fields + "&id=" + me.PK;
		
		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},
	
	
	/**获取信息HTML*/
	getInfoHtml:function(){
		var me = this,
			data = me.InfoData,
			html = '';
			
		if(data.success){
			html = me.getInfoTemplet();
			if(data.value){
			
				html = html.replace(/{CName}/g,data.value.ETemplet_CName || '');
				html = html.replace(/{Comment}/g,data.value.ETemplet_Comment || '无 </br> </br> </br> </br></br> </br>');
			}
		}else{
			html = me.getErrorTemplet();
			html = html.replace(/{Error}/g,data.msg);
		}
		return html;
	},
	/**获取信息HTML模板*/
	getInfoTemplet: function() {
         var templet =
		 "<div class=' center-block alert alert-warning' style='text-align:center;margin:20px 20px;padding-top:20px;padding-bottom:20px;'>" +
          "<p  style='height:80%;  word-break:break-all;word-wrap:break-word;'>{Comment}</p>" +
        "</div>";
		return templet;
	},	
	/**获取错误信息HTML模板*/
	getErrorTemplet:function(){
		var templet = 
		'<div class="col-sm-12">' +
			'<h4>{Title}-错误信息</h4>' +
			'<p style="color:red;padding:5px;word-break:break-all;word-wrap:break-word;">{Error}</p>' +
		'</div>';
		return templet;
	},
	

	/**显示遮罩*/
	showMask:function(text){
		var me = this;
		me.body.mask(text);//显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask:function(){
		var me = this;
		if(me.body){me.body.unmask();}//隐藏遮罩层
	},
	
	/**@public 加载数据*/
	load:function(id){
		var me = this;
		me.PK = id;
    	me.initHtml();
	},
	/**清空数据*/
	clearData: function() {
		var me = this;
		me.PK = null;
		me.update('');
	}
});