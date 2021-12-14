/**
 * 地步信息栏
 */
Ext.ns('Ext.main');
Ext.define('Ext.main.MainBottomPanel',{
	extend:'Ext.panel.Panel',
	alias:'widget.mainbottompanel',
	//=========================可配参数========================
	/**
	 * 版权信息
	 * @type String
	 */
	copyrightInfo:'',//'默认版权信息',
	/**
	 * 用户信息样式
	 * @type String
	 */
	userInfoStyle:'color:white;fontWeight:bold;textAlign:left;',
	/**
	 * 版权信息样式
	 * @type String
	 */
	copyrightInfoStyle:'color:white;fontWeight:bold;textAlign:left;',
	//=========================内部渲染========================
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		
		var changeTime = function(){
			var time = getSystemTime();
			time && me.setServerTime(time);
		};
		window.setInterval(changeTime,1000);
		changeTime();
	},
	/**
	 * 初始化配置
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.layout = "border";
		me.bodyStyle = "background-color:#0C82B3;";
		me.items = me.createItems();
		me.callParent(arguments);
	},
	/**
	 * 创建内部组件
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		var items = [{
			region:'west',
			xtype:'image',
			cls:'main-user-16',
			margin:'4 0 5 20'
		},{
			region:'west',
			xtype:'label',
			itemId:'userInfo',
			style:me.userInfoStyle,
			width:240,
			padding:4
		},{
			region:'west',
			xtype:'label',
			itemId:'serverTime',
			style:me.userInfoStyle,
			width:260,
			padding:4
		},{
			region:'east',
			xtype:'label',
			itemId:'copyrightInfo',
			style:me.copyrightInfoStyle,
			width:80,
			padding:4,
			text:me.copyrightInfo
		},{
			region:'center',
			xtype:'container'
		}];
		return items;
	},
	//=========================对外公开方法========================
	/**
	 * 设置用户信息
	 * @public
	 */
	setUserInfo:function(userName){
		var me = this;
		var userInfo = me.getComponent('userInfo');
		var text = "当前用户：" + userName;
		userInfo.setText(text);
	},
	/**
	 * 设置版权信息
	 * @public
	 */
	setCopyrightInfo:function(value){
		var me = this;
		var copyrightInfo = me.getComponent('copyrightInfo');
		copyrightInfo.setText(value);
	},
	/**
	 * 设置当前时间
	 * @private
	 * @param {} date
	 */
	setServerTime:function(date){
		var me = this,
			serverTime = me.getComponent('serverTime'),
			type = Ext.typeOf(date),
			text = '';
			
		if(type == 'date'){
			text += '系统时间：' + getDateString(date,true,true);
		}else if(type == 'string' && type != ''){
			text += '系统时间：' + date;
		}
		
		serverTime.setText(text);
	}
});