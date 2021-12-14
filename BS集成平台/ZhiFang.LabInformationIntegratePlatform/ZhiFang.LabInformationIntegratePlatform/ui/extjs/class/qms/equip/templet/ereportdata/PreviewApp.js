/**
 *预览仪器质量记录PDF文件
 * @author liangyl
 * @version 2016-08-24
 */
Ext.define('Shell.class.qms.equip.templet.ereportdata.PreviewApp', {
	extend: 'Shell.ux.panel.AppPanel',
	title: '预览仪器质量记录PDF文件',
	layout:'border',
	width: 500,
	height: 400,
	hasBtn:true,
	hasBtntoolbar:true,
	PK:'',
	ETempletId:'',
	/**获取数据服务路径*/
//	selectUrl: '/QMSReport.svc/QMS_UDTO_PreviewPdf',
	hasColse:false,
	hasSave:true,
	URL:'',
	checkviewLable:'审核意见',
	btnsaveText:'审核',
	layout: {
		type: 'border',
		regionWeights: {
			south: 1,
			center: 1
		}
	},
	startDate:null,
	endDate:null,
	afterRender: function() {
		var me = this;
		me.callParent(arguments);
//		var url=me.getUrl();
		me.showPdf(me.URL);
	},
	initComponent: function() {
		var me = this;
		me.title = me.title || "预览仪器质量记录PDF文件";
        me.items =[];	
		me.dockedItems = me.createDockedItems();
		me.callParent(arguments);
	},
		/**创建挂靠功能栏*/
	createDockedItems: function() {
		var me = this,
			items = me.dockedItems || [];
		if (me.hasBtntoolbar) items.push(me.createButtonbottomtoolbar());
		return items;
	},
	/**获取带查询条件*/
	getSearchWhereParams: function() {
		var me = this,
			whereParams = "",
			EndDate = "",
			params = [];
//	    var Sysdate=JcallShell.System.Date.getDate();
//	    var Date = JcallShell.Date.toString(Sysdate,true);
		
//	    if(Date){
//	    	params.push("&endDate="+ Date);
//	    	params.push("&beginDate="+ Date);
//	    }

	    if(Date){
	    	params.push("&endDate<"+ me.endDate);
	    	params.push("&beginDate>="+ me.startDate);
	    }
		if(params.length > 0) {
			whereParams += params.toString().replace(/,/g, '');
		}
		return whereParams;
	},
	//查看PDF内容
    showPdf : function(url,isClear){
    	var me=this;
    	var a='%22';
    	var html = '<div style=%22text-align:center;font-weight:bold;color:red;margin:10px;' + a + '>文件还未获取</div>';
    	if(url){
           html = '<iframe ' +
				'height="100%" width="100%" frameborder="0" ' +
				'style="background:#FFFFF0;overflow:hidden;overflow-x:hidden;overflow-y:hidden;' +
				'height:100%;width:100%;position:absolute;' +
				'top:0px;left:0px;right:0px;bottom:0px;" '+ 'src=' + url+ '>' +
			'</iframe>';
    	}
    	if(isClear == true) html = '';
    	me.update(html);
    },
	/**创建功能按钮栏*/
	createButtonbottomtoolbar: function() {
		var me = this,
			items = me.buttonToolbarItems || [];
		if (items.length == 0) {
		 
			if(me.hasSave){
                var maxwidth=me.width-70;
				items.push({
					xtype: 'textfield',labelWidth: 65,height:20,		
					margin:'0px 0px 10px 0px',
					width: maxwidth,name: 'checkview',itemId: 'checkview',
					labelAlign: 'right',fieldLabel:me.checkviewLable,magin: '5'
				},'-',{
					type: 'button',
					iconCls: 'button-save hand',
					text:me.btnsaveText,
					handler: function(grid, rowIndex, colIndex) {
						me.fireEvent('onSaveClick', me);
					}
				});
			}
			if(me.hasColse && !me.hasSave) {
				items.push('->');
			}
			if(me.hasColse){
				items.push({
					xtype: 'button',
					itemId: 'btnColse',
					iconCls: 'button-del',
					text: "关闭",
					tooltip: '关闭',
					handler: function() {
						me.fireEvent('onCloseClick', me);
						me.hide();
//						me.close();
					}
				})
			}
		}
		return Ext.create('Shell.ux.toolbar.Button', {
			dock: 'bottom',
			itemId: 'bottombuttonsToolbar',
			items: items
		});
	}
});