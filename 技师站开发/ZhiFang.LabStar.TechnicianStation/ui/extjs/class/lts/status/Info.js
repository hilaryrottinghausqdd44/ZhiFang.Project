/**
 * 状态说明
 * @author liangyl
 * @version 2021-04-02
 */
Ext.define('Shell.class.lts.status.Info', {
	extend: 'Ext.panel.Panel',
	title: '其他-状态说明',
	width:300,
	height:170,
    bodyPadding:10,
	autoScroll:true,
    /**加载数据提示*/
	lodingText:JShell.Server.LOADING_TEXT,
   //状态样色
	ColorMap:{
	},
   //悬浮内容样式
    TipsTemplet:'',
	 //颜色条模板
    ColorLineTemplet:'',
    //主状态样式模板
    MainStatusTemplet:'',
     //打印图标样式
    PrintTemplet:'',
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		
	},
	
	initComponent: function() {
		var me = this;
		//初始化Html页面
		me.initHtml();

		me.callParent(arguments);
	},
	
	initHtml:function(){
		var me = this,
		    html = ['<div class="col-sm-12"  style="padding-bottom: 10px;">' +
		    me.PrintTemplet + '打印'+
//		    '<img style="height:10px;width:10px;" src="'+JShell.System.Path.ROOT+'/ui/extjs/css/images/buttons/print.png"/>'+
//			me.ColorLineTemplet.replace(/{color}/g,me.ColorMap.打印.color) + '打印'+
		'</div>'+
		    '<div class="col-sm-12" style="padding-bottom: 10px;">' +
		    me.MainStatusTemplet.replace(/{color}/g,me.ColorMap.危急值.color).replace(/{text}/g,me.ColorMap.危急值.iconText) + '危急值'+
		'</div>'+
		'<div class="col-sm-12" style="padding-bottom: 10px;">' +
		    '仪器审核:<br/>'+
		    me.ColorLineTemplet.replace(/{color}/g,me.ColorMap.仪器通过.color) + me.ColorMap.仪器通过.text+
			me.ColorLineTemplet.replace(/{color}/g,me.ColorMap.仪器警告.color) + me.ColorMap.仪器警告.text+
			me.ColorLineTemplet.replace(/{color}/g,me.ColorMap.仪器异常.color) + me.ColorMap.仪器异常.text+
		'</div>'+
		'<div class="col-sm-12" style="padding-bottom: 10px;">' +
		   me.ColorLineTemplet.replace(/{color}/g,me.ColorMap.阳性保卡.color) + '阳性保卡'+
		'</div>'];
		//更新HTML内容
		me.update(html.join(''));
		

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