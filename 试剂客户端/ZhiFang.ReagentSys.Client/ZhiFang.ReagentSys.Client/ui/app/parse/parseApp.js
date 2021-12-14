Ext.onReady(function(){	
	//显示错误信息
	var showError = function(value){
		var error = document.getElementById('error');
		error.innerHTML = "<b style='color:red'>" + value + "</b>";
	}
	//改变firame地址
	var changeIframe = function(url){
		var parseIframe = document.getElementById('parseIframe');
		parseIframe.src = url;
	}
	//执行页面跳转
	var doHref = function(){
		//地址路径
		var url = getQueryString("url");
		
//		http://localhost:80/ZhiFang.Digitlab.Service/ui/app/parse/
//		getAppHtmlPath()@@?id=4751996466650903663?strUserAccount=admin&strPassWord=123
		
//		http://localhost/ZhiFang.Digitlab.Service/ui/app/parse/parseApp.html?
//		url=getAppHtmlPath()@@?id=4751996466650903663&moduleId=5667147598401256795&
//		strUserAccount=admin&strPassWord=123
		
		if(url.indexOf('app/parse/main.html') != 1){
			var strUserAccount = getQueryString("strUserAccount");//用户名
			var strPassWord = getQueryString("strPassWord");//密码
			url += "?strUserAccount=" + strUserAccount + "&strPassWord=" + strPassWord;
			changeIframe(url);
			return;
		}
		//构建的访问路径处理
		if(url && url != ""){
			var arr = url.split("@@");
			if(arr.length == 2){
				url = eval(arr[0]) + arr[1];
			}
			
			//模块ID
			var moduleId = getQueryString("moduleId");
			if(moduleId && moduleId != ""){
				//请求的地址带上模块ID
				url += (url.indexOf("?") != -1 ? "&" : "?");
				url += "moduleId=" + moduleId;
			}
			var expires = getCookieDate();//cookie失效时间
			Ext.util.Cookies.set('000500',moduleId ,expires);//默认模块ID
		}
		
		//document.location.href = url
		//改变firame地址
		changeIframe(url);
	};
	//账户登录
	var login = function(c){
		//用户名
		strUserAccount = getQueryString("strUserAccount");
		//密码
		strPassWord = getQueryString("strPassWord");
		//登录服务地址
		var url = getRootPath()+'/RBACService.svc/RBAC_BA_Login' + 
				"?strUserAccount=" + strUserAccount + "&strPassWord=" + strPassWord;
		
		var callback = function(text){
			var result = Ext.JSON.decode(text);
			if(result){
				c();
			}else{
				showError("账号或密码错误！");
			}
		};
		getToServer(url,callback,false);
	};
	
	/**服务器信息*/
	var ServerInfo = {
		Date:null
	};
	//系统时间
	(function(){
		//从服务器上获取时间替换当前的系统时间
		var changeServerTime = function(){
			var callback = function(time){if(time){ServerInfo.Time = time;}};
			getServerTime(callback);
		};
		//一分钟一次更正当前系统时间
		window.setInterval(changeServerTime,1000*60);
		//当前系统时间自动累加
		var changeSystemTime = function(){
			if(ServerInfo.Time){
				ServerInfo.Time = new Date(ServerInfo.Time.getTime() + 1000);
			}
		};
		//每秒累加当前系统时间
		window.setInterval(changeSystemTime,1000);
		
		//第一次执行
		changeServerTime();
		changeSystemTime();
	})();
	
	//判断是否请求登录服务
	var EmployeeID = getCookie("000200");
	//alert("员工ID="+EmployeeID);
	
	if(EmployeeID && EmployeeID != ""){//员工ID存在
		doHref();
	}else{
		login(doHref);
	}
});