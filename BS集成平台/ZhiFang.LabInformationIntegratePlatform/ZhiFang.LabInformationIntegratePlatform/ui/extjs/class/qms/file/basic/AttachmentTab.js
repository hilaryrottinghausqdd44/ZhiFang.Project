/**
 * tab页签显示附件
 * @author liangyl	
 * @version 2018-05-21
 */
Ext.define('Shell.class.qms.file.basic.AttachmentTab', {
	extend: 'Ext.tab.Panel',
	header: true,
	activeTab: 0,
	title: '查看附件',
	/**服务附件服务路径*/
	selectAttachmentUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileAttachmentByHQL',
	PK:null,
	itemArr:[],
    /**PDF预览0下载按钮显示，1不显示*/
	DOWNLOAD:'',
	/**PDF预览0打印按钮显示，1不显示*/
	PRINT:'',
	/**1 使用内置pdf预览,0 不使用内置浏览器，不支持控制pdf下载，打印按钮，*/
    BUILTIN:'1',
	/**文件下载服务路径*/
	downloadUrl: '/ServerWCF/CommonService.svc/QMS_UDTO_FFileAttachmentDownLoadFiles',
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		me.itemArr=[];
		me.items =[];
		me.initFilterListeners();
		me.loadData();
		me.items = me.createItems(me.itemArr);
		me.callParent(arguments);
	},
	createItems: function(itemArr) {
		var me = this;
		return itemArr;
	},
	initFilterListeners:function(){
		var me=this;
		JShell.Function.Dwonload = function(ele, e) {
			var comtab = me.getActiveTab(me.items.items[0]);
			me.onDwonload(comtab.TabAttachmentID);
		};
	},
	/**创建页签*/
	loadData:function(){
		var me =this;
		me.loadAttachmentData(function(data){
			if(data && data.value){
				var list =data.value.list;
			    var len =list.length;
			    for(var i=0;i<len;i++){
			    	var FileName=list[i].FFileAttachment_FileName;
			    	var Id=list[i].FFileAttachment_Id;
			    	var suffix='',beforeV='';
                    var index = FileName.lastIndexOf(".");  
                    suffix = FileName.substring(index, FileName.length);
                    beforeV = FileName.substring(0, index);
                    var url = JShell.System.Path.getRootUrl(me.downloadUrl);
		                url += '?operateType=1&id=' + Id;
			    	var num=i+1;			    	
			    	me.itemArr.push({
			    		xtype:'panel',
			    		title:'附件'+num,
			    		itemId:'Attachment'+i,
			    		TabAttachmentID:Id,
			    		suffix:suffix,
			    		dockedItems: [{
					        xtype: 'toolbar',
					        dock: 'top',
					        
					        layout: {
							    type: 'hbox',
							    align: 'middle ',
							    pack: 'center'
							},
					        items: [{
					        	xtype: 'label',
						        style: "font-weight:bold;color:blue;",
						        margin: '0 0 5 5',
						        align: 'center',
					            text: FileName
					        }]
					    }],
					    listeners:{
						    afterrender:function(com,  eOpts ){
					    		var url = JShell.System.Path.getRootUrl(me.downloadUrl);
				                    url += '?operateType=1&id=' + com.TabAttachmentID;
					    		me.showPdf(url,com,com.suffix);
							},
							tabchange: function(tabPanel, newCard, oldCard, eOpts) {
								var url = JShell.System.Path.getRootUrl(me.downloadUrl);
				                    url += '?operateType=1&id=' + Id;
				                me.showPdf(url,com,com.suffix)
							}
					    }
			    	});
			    }
			}
		});
	},
	/**查看PDF内容
	 *如果是火狐浏览器，pdf需要下载
	 * */
    showPdf : function(url,tabPanel,suffix){
    	var me=this;
    	var a='%22';
    	var html = '<div style=%22text-align:center;font-weight:bold;color:red;margin:10px;' + a + '>文件还未获取</div>';
    	suffix=suffix.toLowerCase();
    	var firefox=me.isBrowser();
	    var fileUrl = JShell.System.Path.ROOT+"/ui/pdfjs/web/viewer.html?file=";
        var openUrl = fileUrl+ encodeURIComponent(url)+ '&print='+me.PRINT+'&download='+me.DOWNLOAD;
    	if( suffix=='.pdf' || suffix=='.jpg'|| suffix=='.txt'
    	|| suffix=='.png' || suffix=='.jpeg'|| suffix=='.gif'
    	|| suffix=='.bmp' ){
    		if(url && suffix=='.pdf'){
    			if((!me.DOWNLOAD && !me.PRINT) && me.BUILTIN=='1'){//使用内置pdf预览
    				html = '<iframe ' +
						'height="100%" width="100%" frameborder="0" ' +
						'style="background:#FFFFF0;overflow:hidden;overflow-x:hidden;overflow-y:hidden;' +
						'height:100%;width:100%;position:absolute;' +
						'top:0px;left:0px;right:0px;bottom:0px;" '+ 'src=' + url+ '>' +
					'</iframe>';
    			}else{//使用第三方PDF.js 解析pdf ，可控制pdf 打印，下载按钮
    				var IeVersion = me.getIEVersion();
	    		    if(!IeVersion){
    		    	    html = '<div style=%22text-align:center;font-weight:bold;color:red;margin:10px;' + a + '>请使用IE8以上或者其他浏览器</div>' ;
	    		   }else{
		    		   	html = '<iframe ' +
							'height="100%" width="100%" frameborder="0" ' +
							'style="background:#FFFFF0;overflow:hidden;overflow-x:hidden;overflow-y:hidden;' +
							'height:100%;width:100%;position:absolute;' +
							'top:0px;left:0px;right:0px;bottom:0px;" '+ 'src=' + openUrl+ '>' +
						'</iframe>';
	    		   }
    			}
	    	}else{
	    		  html = '<iframe ' +
					'height="100%" width="100%" frameborder="0" ' +
					'style="background:#FFFFF0;overflow:hidden;overflow-x:hidden;overflow-y:hidden;' +
					'height:100%;width:100%;position:absolute;' +
					'top:0px;left:0px;right:0px;bottom:0px;" '+ 'src=' + url+ '>' +
				'</iframe>';
	    	}
    	}else{
    		html='<div class="col-sm-12" style="padding:40px;">' +
				"<center><button class='hand vertical-center' " +
				" style='padding:5px 10px;background-color:green;'" +
				" onclick='JShell.Function.Dwonload(this,event)'>" +
				"<a style='color:#ffffff;'>下载查看</a>" +
				"</button></center>" +
			'</div>';
    	}
    	tabPanel.update(html);
    },
    /**判断是否是火狐浏览器*/
    isBrowser:function(){
    	var me =this;
    	var browser=true;
    	if(isFirefox=navigator.userAgent.indexOf("Firefox")>0){ 
    		browser=false;
		}  
	    return browser;
    },
	
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if(me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if(me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
	},
	
	/**获取附件信息*/
	loadAttachmentData: function(callback) {
		var me = this;
		if(!me.PK)return;
		var url = JShell.System.Path.getRootUrl(me.selectAttachmentUrl);
		var fields = [
			'FFileAttachment_Id', 'FFileAttachment_FileName', 'FFileAttachment_FileSize',
			'FFileAttachment_CreatorName', 'FFileAttachment_DataAddTime'
		];
		url += "?isPlanish=true&fields=" + fields.join(",");
		var where = 'ffileattachment.IsUse=1 and ffileattachment.FFile.Id=' + me.PK;
		url += '&where=' + where+'&sort=[{"property":"FFileAttachment_DispOrder","direction":"ASC"}]';
		JShell.Server.get(url, function(data) {
			callback(data);
		}, false);
	},
	onDwonload: function(id) {
		var me = this;
		var url = JShell.System.Path.getRootUrl(me.downloadUrl);
		url += '?operateType=1&id=' + id;
		window.open(url);
	},
    /**判断是否是IE浏览器*/
    getIEVersion:function (){
     	var num=true;
        var userAgent = navigator.userAgent; //取得浏览器的userAgent字符串
        var isIE =  navigator.userAgent.indexOf("MSIE")>0//判断是否IE浏览器
        if(isIE){
            var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
            reIE.test(userAgent);
            var fIEVersion = parseFloat(RegExp["$1"]);
            if(fIEVersion<9){
             	num=false;
            }
        }
        return num;
    }
});