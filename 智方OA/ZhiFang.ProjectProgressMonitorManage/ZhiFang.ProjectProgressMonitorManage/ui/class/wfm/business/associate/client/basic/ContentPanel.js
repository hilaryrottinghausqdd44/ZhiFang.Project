/**
 *客户信息
 * @author liangyl
 * @version 2017-03-10
 */
Ext.define('Shell.class.wfm.business.associate.client.basic.ContentPanel', {
	extend: 'Ext.panel.Panel',
	title: '客户信息',
	width: 800,
	height: 410,
	
	autoScroll:true,
	/**获取数据服务路径*/
	selectUrl: '/ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientById',
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
			url = JShell.System.Path.getRootUrl(me.selectUrl);
		
		var fields = [
			'Id','Name','SName','Shortcode','PinYinZiTou','Comment','CountryName',
			'ProvinceName','CityName','SCTypeName','Principal','LinkMan',
			'PhoneNum','PhoneNum2','Address','MailNo','Emall','Bman',
			'ClientAreaName','ClientTypename','HospitalTypeName','HospitalLevelName',
			'Url','VATNumber','VATBank','VATAccount','LicenceCode','LicenceKey1',
			'LRNo1','LicenceKey2','LRNo2'
		];
		
		fields = "PClient_" + fields.join(",PClient_");
		
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
				html = html.replace(/{Name}/g,data.value.PClient_Name || '');
				html = html.replace(/{SName}/g,data.value.PClient_SName || '');
				html = html.replace(/{Shortcode}/g,data.value.PClient_Shortcode || '');
				html = html.replace(/{PinYinZiTou}/g,data.value.PClient_PinYinZiTou || '');
				html = html.replace(/{Comment}/g,data.value.PClient_Comment || '');
				html = html.replace(/{CountryName}/g,data.value.PClient_CountryName || '');
				html = html.replace(/{ProvinceName}/g,data.value.PClient_ProvinceName || '');
				html = html.replace(/{SCTypeName}/g,data.value.PClient_SCTypeName || '');
				html = html.replace(/{Principal}/g,data.value.PClient_Principal || '');
				html = html.replace(/{LinkMan}/g,data.value.PClient_LinkMan || '');
				html = html.replace(/{PhoneNum}/g,data.value.PClient_PhoneNum || '');
				html = html.replace(/{PhoneNum2}/g,data.value.PClient_PhoneNum2 || '');
				html = html.replace(/{Address}/g,data.value.PClient_Address || '');
				html = html.replace(/{MailNo}/g,data.value.PClient_MailNo || '');
				html = html.replace(/{Emall}/g,data.value.PClient_Emall || '');
				html = html.replace(/{Bman}/g,data.value.PClient_Bman || '');
				html = html.replace(/{ClientAreaName}/g,data.value.PClient_ClientAreaName || '');
				html = html.replace(/{ClientTypename}/g,data.value.PClient_ClientTypename || '');
				html = html.replace(/{HospitalTypeName}/g,data.value.PClient_HospitalTypeName || '');
				html = html.replace(/{HospitalLevelName}/g,data.value.PClient_HospitalLevelName || '');
				html = html.replace(/{Url}/g,data.value.PClient_Url || '');
				html = html.replace(/{VATBank}/g,data.value.PClient_VATBank || '');
				html = html.replace(/{VATNumber}/g,data.value.PClient_VATNumber || '');
				html = html.replace(/{VATAccount}/g,data.value.PClient_VATAccount || '');
				html = html.replace(/{LicenceCode}/g,data.value.PClient_LicenceCode || '');
				html = html.replace(/{LicenceKey1}/g,data.value.PClient_LicenceKey1 || '');
				html = html.replace(/{LRNo1}/g,data.value.PClient_LRNo1 || '');
				html = html.replace(/{LicenceKey2}/g,data.value.PClient_LicenceKey2 || '');
				html = html.replace(/{LRNo2}/g,data.value.PClient_LRNo2 || '');
				html = html.replace(/{CityName}/g,data.value.PClient_CityName || '');
//				html = html.replace(/{CityName}/g,data.value.PClient_CityName || '');

			
			}
		}else{
			html = me.getErrorTemplet();
			html = html.replace(/{Title}/g,'客户内容');
			html = html.replace(/{Error}/g,data.msg);
		}
		
		return html;
	},
	/**获取信息HTML模板*/
	getInfoTemplet: function() {
		
		//行DIV框样式
		var rDivStyle = 'float:left;width:100%;padding:5px;margin:5px 0;border:1px solid #5cb85c;border-radius:2px;';
		//内容DIV框样式
		var sDivStyle = 'float:left;padding:5px;border:0;margin-right:10px;';
		
		var templet =
		'<div class="col-sm-12" style="text-align:center;margin-top:10px;color:#5cb85c;">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">{Name}</h4>' +
		'</div>' +
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
			    '<div style="' + sDivStyle + '"><b>区域：{ClientAreaName}</b></div>' +
				'<div style="' + sDivStyle + '">客户类型名称：{ClientTypename}</div>' +
				'<div style="' + sDivStyle + '">医院类别名称：{HospitalTypeName}</div>' +
				'<div style="' + sDivStyle + '">医院等级名称：{HospitalLevelName}</div>' +
				'<div style="' + sDivStyle + '">客户级别：{SCTypeName}</div>' +
				'<div style="' + sDivStyle + '">国家名称：{CountryName}</div>' +
				'<div style="' + sDivStyle + '">省份：{ProvinceName}</div>' +
				'<div style="' + sDivStyle + '">城市：{CityName}</div>' +
			
				'<div style="' + sDivStyle + '">简称：{SName}</b></div>' +
				'<div style="' + sDivStyle + '">快捷码：{Shortcode}</div>' +
				'<div style="' + sDivStyle + '">汉字拼音字头：{PinYinZiTou}</div>' +
				'<div style="' + sDivStyle + '">负责人：{Principal}</div>' +
				'<div style="' + sDivStyle + '">联系人：{LinkMan}</div>' +
				'<div style="' + sDivStyle + '">联系电话：{PhoneNum}</div>' +
				'<div style="' + sDivStyle + '">联系电话2：{PhoneNum2}</div>' +
				'<div style="' + sDivStyle + '">邮编：{MailNo}</div>' +
				
				'<div style="' + sDivStyle + '">电子邮件：{Emall}</div>' +
				'<div style="' + sDivStyle + '">地址：{Address}</div>' +
				'<div style="' + sDivStyle + '">业务员：{Bman}</div>' +
				'<div style="' + sDivStyle + '">网址：{Url}</div>' +


			'</div>' +
		'</div>' +
	
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
				'<div style="' + sDivStyle + '">增值税税号：{VATNumber}</div>' +
				'<div style="' + sDivStyle + '">增值税开户行：{VATBank}</div>' +
				'<div style="' + sDivStyle + '">增值税帐号：{VATAccount}</div>' +
			'</div>' +
		'</div>' +
			
		'<div class="col-sm-12">' +
			'<div style="' + rDivStyle + '">' +
                '<div style="' + sDivStyle + '">授权编码：{LicenceCode}</div>' +
				'<div style="' + sDivStyle + '">主服务器授权Key：{LicenceKey1}</div>' +
				'<div style="' + sDivStyle + '">主服务器授权申请号：{LRNo1}</div>' +
                '<div style="' + sDivStyle + '">备份服务器授权Key：{LicenceKey2}</div>' +
				'<div style="' + sDivStyle + '">备份服务器授权申请号：{LRNo2}</div>' +

			'</div>' +
		'</div>' +
		'<div class="col-sm-12">' +
			'<h4 style="border-bottom:1px solid #e0e0e0;padding-bottom:5px;">说明描述</h4>' +
			'<p style="word-break:break-all;word-wrap:break-word;">{Comment}</p>' +
		'</div>' ;
	
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