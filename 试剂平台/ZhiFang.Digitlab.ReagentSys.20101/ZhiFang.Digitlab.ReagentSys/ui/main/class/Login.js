/**
 * 控制台
 */
Ext.ns('Ext.main');
Ext.define('Ext.main.Login',{
	extend:'Ext.form.Panel',
	alias:'widget.login',
	//requires: ['Ext.main.MainTreePanel', 'Ext.main.MainTabPanel','Ext.main.MainTopPanel'],
	//===================可配置参数====================
	/**
	 * 有确定按钮
	 * @type Boolean
	 */
	hasOK:true,
	/**
	 * 有取消按钮
	 * @type Boolean
	 */
	hasCancel:true,
	/**
	 * 标题
	 * @type String
	 */
	title:'用户登录',
	/**
	 * 面板宽度
	 * @type Number
	 */
	width:682,
	/**
	 * 面板高度
	 * @type Number
	 */
	height:433,
	/**
     * 登录的后台服务地址
     * @type String
     */
    loginServerUrl:getRootPath()+'/RBACService.svc/RBAC_BA_Login',
	//=================================================
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
		me.getComponent('strUserAccount').focus(true,100);
		me.setRememberUser(getCookie('100005'));
		me.setRememberPwd(getCookie('100006'));
		//按钮监听
		me.initButListeners();
	},
	/**
	 * 初始化配置
	 * @private
	 */
	initComponent:function(){
		var me = this;
		me.layout = "absolute";
		me.html = me.createHtml();
		me.items = me.createItems();
		me.listeners = me.createListeners();
        me.addEvents('login');//注册登录事件
        me.addEvents('cancel');//注册取消事件
		me.callParent(arguments);
	},
	createHtml:function(){
		var bgImgUrl ="../images/login/img.png";
		var html = "<img src='" + bgImgUrl + "'/>";
		return html;
	},
	/**
	 * 创建内部组件
	 * @private
	 * @return {}
	 */
	createItems:function(){
		var me = this;
		var items = [{
			xtype:'label',itemId:'errorInfo',
			style:'color:red;fontWeight:bold;textAlign:center;',
			width:275,x:348,y:290
		},{
			xtype:'image',src:'../images/login/userName.png',
			width:41,height:46,
			x:331,y:103
		},{
			xtype:'textfield',emptyText:'请输入账号',
			itemId:'strUserAccount',name:'strUserAccount',
			width:264,height:46,
			x:372,y:103,
			value:getCookie('100005') == 'true' ? getCookie('100003') :''
		},{
			xtype:'image',src:'../images/login/userPwd.png',
			width:41,height:46,
			x:331,y:183
		},{
			xtype:'textfield',emptyText:'请输入密码',
			itemId:'strPassWord',name:'strPassWord',
			inputType:'password',
			width:264,height:46,
			x:372,y:183
		},{
			xtype:'image',itemId:'ok',
			hidden:!me.hasOK,
			src:'../images/login/login.png',
			width:116,height:36,
			x:507,y:309,
			style:{
				cursor:'pointer'
			}
		},{
			xtype:'image',itemId:'cancel',
			hidden:!me.hasCancel,
			src:'../images/login/cancel.png',
			width:116,height:36,
			x:348,y:309,
			style:{
				cursor:'pointer'
			}
		},{
			xtype:'image',itemId:'loadingImg',src:'../images/login/loading.gif',
			width:275,x:348,y:320,hidden:true
		},{
			xtype:'label',itemId:'loadingText',text:'正在登录...',
			style:'color:green;fontWeight:bold;textAlign:center;',
			width:275,x:348,y:300,hidden:true
		}];
		
		//选择功能
		var checkboxs = [{
			xtype:'checkbox',itemId:'rememberUser',boxLabel:'记住用户名',
			width:100,x:332,y:262,
			listeners:{
				change:function(){
					me.rememberUserAndPwd();
				}
			}
		},{
			xtype:'checkbox',itemId:'rememberPwd',boxLabel:'记住密码',
			width:100,x:459,y:262,
			listeners:{
				change:function(){
					me.rememberUserAndPwd();
				}
			}
		},{
			xtype:'label',itemId:'forgetPwd',text:'忘记密码？',
			style:'cursor:pointer;color:blue;text-decoration:underline',
			width:60,x:569,y:264
		}];
		
		items = items.concat(checkboxs);
		return items;
	},
	/**
	 * 创建监听
	 * @private
	 * @return {}
	 */
	createListeners:function(){
		var me = this;
		var listeners = me.listeners || {};
		listeners.render = function(input){
	    	new Ext.KeyMap(input.getEl(),[{
		      	key:Ext.EventObject.ENTER,
		      	fn:function(){
		       		me.login();
		      	}
	     	}]);
	    }
	    return listeners;
	},
	/**
	 * 按钮的监听
	 * @private
	 */
	initButListeners:function(){
		var me = this;
		//登录按钮
		var ok = me.getComponent('ok');
		if(ok){
			ok.on({
				mousedown:{
					element:'el',
					fn:function(e,t,eOpts){
						if(e.button == 0){//左键
							me.login();
						}
					}
				}
			});
		}
		//取消按钮
		var cancel = me.getComponent('cancel');
		if(cancel){
			cancel.on({
				mousedown:{
					element:'el',
					fn:function(e,t,eOpts){
						if(e.button == 0){//左键
							me.setErrorInfo("");
							me.fireEvent('cancel');
						}
					}
				}
			});
		}
	},
	/**
	 * 保存数据
	 * @private
	 */
	login:function(){
		var me = this;
		if (!me.getForm().isValid()) return;
		
		var callback = function(){
			me.setErrorInfo("");
			me.hideLoading();
			me.fireEvent('login');
		};
		var values = me.getForm().getValues();
		//用户信息（账号，密码）
		var user = {
			strUserAccount:values.strUserAccount,
			strPassWord:values.strPassWord
		}
		me.setErrorInfo("");
		me.getComponent('ok').hide();
		me.getComponent('cancel').hide();
		me.showLoading();
		me.loginToServer(user,callback);
	},
	/**
	 * 往后台提交数据
	 * @private
	 * @param {} user
	 * @param {} callback
	 */
	loginToServer:function(user,callback){
		var me = this;
        var url = me.loginServerUrl + "?strUserAccount=" + user.strUserAccount + "&strPassWord=" + user.strPassWord;
        Ext.Ajax.defaultPostHeader = 'application/json';
        Ext.Ajax.request({
            async:false,//非异步
            url:url,
            method:'GET',
            timeout:5000,
            success:function(response,opts){
                var result = Ext.JSON.decode(response.responseText);
                if(result){
                	//从服务器上获取时间替换当前的系统时间
                	var c = function(time){if(time){ServerInfo.Time = time;}};
					getServerTime(c);
                    if(Ext.typeOf(callback) == "function"){
                        callback();//回调函数
                    }
                }else{
                    me.setErrorInfo("账号或密码错误！");
                    me.hideLoading();
                    me.showOk();
                    me.hasCancel && me.showCancel();
                }
            },
            failure:function(response,options){ 
                me.setErrorInfo("登录账号请求失败！");
                me.hideLoading();
                me.showOk();
                me.hasCancel && me.showCancel();
            }
        });
	},
	/**
	 * 设置错误信息
	 * @private
	 * @param {} text
	 */
	setErrorInfo:function(text){
		var me = this;
		var errorInfo = me.getComponent('errorInfo');
		if(text && text != ""){
			errorInfo.setText(text);
		}else{
			errorInfo.setText("");
		}
	},
	/**
	 * 设置登录等待提示可见
	 * @private
	 */
	showLoading:function(){
		var me = this;
		var loadingImg = me.getComponent('loadingImg');
		var loadingText = me.getComponent('loadingText');
		loadingImg.show();
		loadingText.show();
	},
	/**
	 * 设置登录等待提示不可见
	 * @private
	 */
	hideLoading:function(){
		var me = this;
		var loadingImg = me.getComponent('loadingImg');
		var loadingText = me.getComponent('loadingText');
		loadingImg.hide();
		loadingText.hide();
	},
	//=========================对外公开方法========================
	/**
	 * 设置登录按钮可见
	 * @public
	 */
	showOk:function(){
		var me = this;
		me.hasOK = true;
		var com = me.getComponent('ok');
		com.show();
	},
	/**
	 * 设置登录按钮不可见
	 * @public
	 */
	hideOk:function(){
		var me = this;
		me.hasOK = false;
		var com = me.getComponent('ok');
		com.hide();
	},
	/**
	 * 设置取消按钮可见
	 * @public
	 */
	showCancel:function(){
		var me = this;
		me.hasCancel = true;
		var com = me.getComponent('cancel');
		com.show();
	},
	/**
	 * 设置取消按钮不可见
	 * @public
	 */
	hideCancel:function(){
		var me = this;
		me.hasCancel = false;
		var com = me.getComponent('cancel');
		com.hide();
	},
	/**
	 * 清空密码栏
	 * @public
	 */
	clearPassWord:function(){
		var me = this;
		var strPassWord = me.getComponent('strPassWord');
		strPassWord.setValue("");
	},
	//==============================================================
	/**
	 * 是否记住用户名
	 * @public
	 * @return {}
	 */
	isRememberUser:function(){
		var me = this;
		var bo = me.getComponent('rememberUser').getValue();
		return bo;
	},
	/**
	 * 是否记住密码
	 * @public
	 * @return {}
	 */
	isRememberPwd:function(){
		var me = this;
		var bo = me.getComponent('rememberPwd').getValue();
		return bo;
	},
	/**
	 * 用户名及密码记忆处理
	 * @private
	 */
	rememberUserAndPwd:function(){
		var me = this;
		var isRememberUser = me.isRememberUser();
		var isRememberPwd = me.isRememberPwd();
		var expires = getCookieDate();//cookie失效时间
		
		var strUserAccount = me.getComponent('strUserAccount').getValue();
		strUserAccount = isRememberUser ? strUserAccount : '';
		Ext.util.Cookies.set('100003',strUserAccount,expires);

		Ext.util.Cookies.set("100005",isRememberUser,expires);
		Ext.util.Cookies.set("100006",isRememberPwd,expires);
	},
	/**
	 * 给用户名框赋值
	 * @public
	 * @param {} name
	 */
	setUserName:function(name){
		var me = this;
		var EmployeeID = me.getComponent('strUserAccount');
		EmployeeID.setValue(name);
	},
	/**
	 * 给密码框赋值
	 * @public
	 * @param {} pwd
	 */
	setPwd:function(pwd){
		var me = this;
		var EmployeePwd = me.getComponent('strPassWord');
		EmployeePwd.setValue(pwd);
	},
	/**
	 * 给记住用户名赋值
	 * @public
	 * @param {} bo
	 */
	setRememberUser:function(bo){
		var me = this;
		var com = me.getComponent('rememberUser');
		if(bo === true || bo === 'true'){
			bo = true;
		}else{
			bo = false;
		}
		com.setValue(bo);
	},
	/**
	 * 给记住密码赋值
	 * @public
	 * @param {} bo
	 */
	setRememberPwd:function(bo){
		var me = this;
		var com = me.getComponent('rememberPwd');
		if(bo === true || bo === 'true'){
			bo = true;
		}else{
			bo = false;
		}
		com.setValue(bo);
	}
});