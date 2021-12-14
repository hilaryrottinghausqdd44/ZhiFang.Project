/**
 * 互动列表
 * @author Jcall
 * @version 2015-07-02
 */
Ext.define('Shell.class.sysbase.interaction.List', {
	extend: 'Ext.panel.Panel',
	
	title: '互动列表 ',
	width: 800,
	height: 500,
	
	/**附属主体名*/
    PrimaryName:null,
    /**附属主体数据ID*/
	PrimaryID:null,
	
  	/**获取数据服务路径*/
	selectUrl:'/BaseService.svc/ST_UDTO_SearchFInteractionByHQL?isPlanish=true',
	/**修改服务地址*/
	addUrl:'/BaseService.svc/ST_UDTO_UpdateFInteractionByField',
	/**删除数据服务路径*/
	delUrl:'/BaseService.svc/ST_UDTO_DelFInteraction',
  	
	/**显示成功信息*/
	showSuccessInfo: false,
	/**消息框消失时间*/
	hideTimes: 3000,
	
	/**默认加载*/
	defaultLoad: false,
	/**默认每页数量*/
	defaultPageSize:100,
	/**当前页数*/
	PageNumber:1,
	
	/**开启加载数据遮罩层*/
	hasLoadMask: true,
	/**加载数据提示*/
	loadingText: JShell.Server.LOADING_TEXT,
	/**保存数据提示*/
	saveText: JShell.Server.SAVE_TEXT,
	/**内容自动显示*/
	autoScroll:true,
	bodyStyle:'background:#F0F0F0',

	afterRender: function() {
		var me = this;
		me.callParent(arguments);
	},
	initComponent: function() {
		var me = this;
		
		me.callParent(arguments);
	},
	onLoadData:function(){
		var me = this;
		var url = JShell.System.Path.getUrl(me.selectUrl);
		var params = me.getParams();
		
		url += '&' + params.join('&');
		
		me.showMask(me.saveText); //显示遮罩层
		JShell.Server.get(url,function(data){
			me.hideMask();//隐藏遮罩
			if(data.success){
				me.onInsertData(data.value);//添加数据
			}else{
				JShell.Msg.error(data.msg,1000);
			}
		});
	},
	onInsertData:function(data){
		var me = this,
			list = (data || {}).list || [],
			len = list.length;
			
		if(len == 0){
			JShell.Msg.alert('没有数据',null,300);
			return;
		}
		
		//me.PageNumber++;
		
		for(var i=len-1;i>=0;i--){
			me.insertContent(list[i]);
		}
	},
	insertContent:function(value){
		var me = this;
		var parent = document.getElementById(me.getId() + '-body');
		var isSender = value.FInteraction_Sender_Id == 
			JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
		
		var className = 'Interaction_Div' + (isSender ? '_Right' : '_Left');
		
		var Div = document.createElement('div');
		Div.className = className;
		
	    //发起人姓名
    	var SenderNameSpan = document.createElement('span');
    	
    	if(!isSender){
    		var name = value.FInteraction_Sender_CName + '&nbsp;&nbsp;' + 
    			JShell.Date.toString(value.FInteraction_DataAddTime);
    		SenderNameSpan.innerHTML = name;
    	}else{
    		SenderNameSpan.innerHTML = '&nbsp;';
    	}
    	
    	Div.appendChild(SenderNameSpan);
	    
	    //发起人头像
	    var SenderImg = document.createElement('img');
	    SenderImg.src = JShell.System.Path.ROOT + '/ui/class/sysbase/interaction/images/DefaultHeadImg.jpg';
		Div.appendChild(SenderImg);
		
		//交流的内容
		var ContentDiv = document.createElement('div');
	    ContentDiv.innerHTML = value.FInteraction_Contents;
	    Div.appendChild(ContentDiv);
	    
	    parent.appendChild(Div);
	    
	    
//	    var me = this;
//		var parent = document.getElementById(me.getId() + '-body');
//		var isSender = value.FInteraction_Sender_Id == 
//			JShell.System.Cookie.get(JShell.System.Cookie.map.USERID);
//		
//		var className = 'Interaction_Div' + (isSender ? '_Right' : '_Left');
//		
//		//容器
//		var Div = document.createElement('div');
//		Div.className = className;
//		
//	    //发起人姓名
//	    var SenderNameDiv = document.createElement('div');
//  	if(!isSender){
//  		var name = value.FInteraction_Sender_CName + '&nbsp;&nbsp;' + 
//  			JShell.Date.toString(value.FInteraction_DataAddTime);
//  		SenderNameDiv.innerHTML = name;
//  	}else{
//  		SenderNameDiv.innerHTML = '&nbsp;';
//  	}
//  	SenderNameDiv.className = className + '_Name';
//  	Div.appendChild(SenderNameDiv);
//	    
//	    //发起人头像
//	    var SenderImg = document.createElement('img');
//	    SenderImg.src = JShell.System.Path.ROOT + '/ui/class/sysbase/interaction/images/DefaultHeadImg.jpg';
//		SenderImg.className = className + '_Img';
//		Div.appendChild(SenderImg);
//		
//		//交流的内容
//		var ContentDiv = document.createElement('div');
//	    ContentDiv.innerHTML = value.FInteraction_Contents;
//	    ContentDiv.className = className + '_Content';
//	    Div.appendChild(ContentDiv);
//	    
//	    parent.appendChild(Div);
	},
	getParams:function(){
		var me = this,
			params = [];
			
		var where = "finteraction.IsUse=true and finteraction.PrimaryName='" + me.PrimaryName + 
			"' and finteraction.PrimaryID='" + me.PrimaryID + "'";
		params.push('where=' + where);
		
		var fields = 'FInteraction_Sender_Id,FInteraction_Sender_CName,FInteraction_Contents,FInteraction_DataAddTime';
		params.push('fields=' + fields);
		
		var sort = '[{"property":"FInteraction_DataAddTime","direction":"DESC"}]';
		params.push('sort=' + sort);
		
		params.push('limit=' + me.defaultPageSize);
		params.push('page=' + me.PageNumber);//start
		
		return params;
	},
	/**显示遮罩*/
	showMask: function(text) {
		var me = this;
		if (me.hasLoadMask) {
			me.body.mask(text);
		} //显示遮罩层
		me.disableControl(); //禁用所有的操作功能
	},
	/**隐藏遮罩*/
	hideMask: function() {
		var me = this;
		if (me.hasLoadMask) {
			me.body.unmask();
		} //隐藏遮罩层
		me.enableControl(); //启用所有的操作功能
	},
	/**启用所有的操作功能*/
	enableControl: function(bo) {
		var me = this,
			enable = bo === false ? false : true,
			toolbars = me.dockedItems.items || [],
			length = toolbars.length,
			items = [];

		for (var i = 0; i < length; i++) {
			if (toolbars[i].xtype == 'header') continue;
			if (toolbars[i].isLocked) continue;
			var fields = toolbars[i].items.items;
			items = items.concat(fields);
		}

		var iLength = items.length;
		for (var i = 0; i < iLength; i++) {
			items[i][enable ? 'enable' : 'disable']();
		}
		if (bo) {
			me.defaultLoad = true;
		}
	},
	/**禁用所有的操作功能*/
	disableControl: function() {
		this.enableControl(false);
	}
});