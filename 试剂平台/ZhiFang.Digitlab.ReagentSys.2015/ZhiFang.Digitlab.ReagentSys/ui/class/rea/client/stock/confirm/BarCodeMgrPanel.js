/**
 * 条码明细列表
 * @author 
 * @version 2016-11-02
 */
Ext.define('Shell.class.rea.client.stock.confirm.BarCodeMgrPanel', {
	extend: 'Ext.panel.Panel',
	title: '条码明细内容',
	width: 800,
	height: 600,
	/**内容周围距离*/
	bodyPadding:'10px 10px 10px 10px',
	autoScroll:true,
   /**加载数据提示*/
	lodingText:JShell.Server.LOADING_TEXT,
	
	/**操作记录数据*/
	LogData:[{SerialNo:1000000},{SerialNo:1001000},{SerialNo:10020000}],
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		//初始化Html页面
		me.initHtml();
	},
	/**数据改变时*/
	changeData:function(LogData){
		var me=this;
		me.LogData=LogData;
		me.initHtml();
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
		me.initHtmlContent();//初始化HTML页面
			
	},
	/**初始化HTML页面*/
	initHtmlContent: function() {
		var me = this,
			html = [];
			
		html.push(me.getOperLogHtml());
		
		//更新HTML内容
		me.update(html.join(''));
		
		//隐藏遮罩
		me.hideMask();
		
	},
	/**获取操作记录HTML*/
	getOperLogHtml: function() {
		var me = this,
			data = Ext.decode(me.LogData),
			html = '';
		if(data.length>0){
			var attArr = [],
				html= '';
				
			for(var i = 0; i < data.length; i++) {
				attArr.push(me.getOneOperLogHtml(data[i]));
			}
			html = me.getOperLogTemplet();
			html = html.replace(/{OperLogList}/g, (attArr.join("") || '无'));
		}else{
			html = me.getErrorTemplet();
			html = html.replace(/{Title}/g,'条码号');
			html = html.replace(/{Error}/g,data.msg);
		}
		return html;
	},

	/**获取一条操作记录信息HTML*/
	getOneOperLogHtml:function(data){
		var me = this,
			temp = me.getOneOperLogTemplet(),
			html = '';
			
		temp = temp.replace(/{SerialNo}/g, data.GoodsBarcodeID);
		html = temp;
		return html;
	},
	/**获取一个操作记录模板*/
	getOneOperLogTemplet: function() {
		var templet =
		'<div style="padding:5px; ">' +
		   '<b >'+//style="color: #008B00"
			'{SerialNo}'+
		   '</b>'+
		'</div>';
		return templet;
	},
	/**获取内容模板*/
	getContentTemplet:function(title,name){
		var templet =
		'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">' + title + '</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">' + name + '</p>' +
		'</div>';
		return templet;
	},
	/**获取操作记录模板*/
	getOperLogTemplet: function() {
		return this.getContentTemplet('条码号','{OperLogList}');
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
	
	/**初始化下载监听*/
	initDownloadListeners:function(){
		var me = this,
			DomArray = Ext.query("[filedownload]"),
			len = DomArray.length;
			
		for(var i=0;i<len;i++){
			DomArray[i].onclick = function() {
				var id = this.getAttribute("data");
				me.onDwonload(id);
			};
		}
	},
	/**点击下载文件*/
	onDwonload:function(id){
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.downloadUrl);
		url += '?operateType=1&id=' + id;
		window.open(url);
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