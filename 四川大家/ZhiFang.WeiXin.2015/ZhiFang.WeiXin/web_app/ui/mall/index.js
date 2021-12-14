$(function () {
	//外部参数
	var params = JcallShell.getRequestParams(true);
	//获取账号信息服务地址
    var GET_ACCOUNT_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppService.svc/WXAS_BA_GetPatientInformation";
    //获取医生信息服务地址
    var GET_DOCTOR_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppDoctService.svc/WXADS_BA_GetDoctorAccountInfo";
    //获取区域信息服务地址
    var GET_AREA_URL = JcallShell.System.Path.ROOT + "/ServerWCF/DictionaryService.svc/ST_UDTO_SearchClientEleAreaById";
    //账户信息获取失败提示信息
    var NO_INFO_ERROR_TEXT = '获取用户信息失败，请尝试重新登录！';
    
    //TYPE参数含义：患者(1)，医生(2)
    var TYPE = params.TYPE || 1;
    
	if(TYPE == 1){//患者
		initPatient();//初始化患者
	}else{//医生
		initDoctor();//初始化医生
	}
	
	//初始化患者
	function initPatient(){
		var loginUrl = "patient/login/login.html?v=" + new Date().getTime();
		var homeUrl = "patient/home/home.html?v=" + new Date().getTime();
		initInfo(loginUrl,homeUrl);
	}
	//初始化医生
	function initDoctor(){
		//医生账号不存在，跳转至医生账号首次绑定页面
		if(!JcallShell.Cookie.get(JcallShell.Cookie.map.DoctorId)){
			location.href = 'doctor/register/index.html?v=' + new Date().getTime();
			return;
		}
		
		//获取医生信息
		getDoctorInfo(function(data){
			var info = JcallShell.JSON.encode(data);//转码
			JcallShell.LocalStorage.User.setDoctor(info);//初始化医生信息
			//获取区域信息
			getAreaInfo(data.AreaID,function(dat){
				if(dat){
					var doctor = JcallShell.LocalStorage.User.getDoctor(true);
					doctor.AreaName = dat.AreaCName;
					JcallShell.LocalStorage.User.setDoctor(JcallShell.JSON.encode(doctor));//初始化医生信息
				}
				var loginUrl = "doctor/login/login.html?v=" + new Date().getTime();
				var homeUrl = "doctor/home/home.html?v=" + new Date().getTime();
				initInfo(loginUrl,homeUrl);
			});
		});
	}
	//初始化页面信息
	function initInfo(loginUrl,homeUrl){
		JcallShell.LocalStorage.User.setAccount('');//清空用户账户信息
		getAccountInfo(function(data){
			var info = JcallShell.JSON.encode(data);//转码
			JcallShell.LocalStorage.User.setAccount(info);//初始化账户信息
			
			//如果需要强制强制密码登录的就定位到登录页面，否则直接进入主页
			var url = data.BWeiXinAccount_LoginInputPasswordFlag ? loginUrl : homeUrl;
			url += '?v=' + JcallShell.System.JS_VERSION;
			//$("#iframe").attr("src",url);
			location.href = url;
		});
	}
	//获取账户信息
	function getAccountInfo(callback){
		JcallShell.Server.ajax({
			url:GET_ACCOUNT_URL
		},function(data){
			if(data.success){
				data.value.BWeiXinAccount_LoginInputPasswordFlag = 
					data.value.BWeiXinAccount_LoginInputPasswordFlag == "true" ? true : false;
				callback(data.value);
			}else{
				$("#info").html(NO_INFO_ERROR_TEXT);
				$("#info").show();
			}
		});
	}
	//获取医生信息
	function getDoctorInfo(callback){
		JcallShell.Server.ajax({
			url:GET_DOCTOR_URL
		},function(data){
			if(data.success){
				callback(data.value);
			}else{
				$("#info").html(NO_INFO_ERROR_TEXT);
				$("#info").show();
			}
		});
	}
	//获取区域信息
	function getAreaInfo(id,callback){
		if(!id) callback();
		
		var url = GET_AREA_URL + "?fields=ClientEleArea_AreaCName&id=" + id;
		JcallShell.Server.ajax({
			url:url
		},function(data){
			if(data.success){
				callback(data.value);
			}else{
				$("#info").html(NO_INFO_ERROR_TEXT);
				$("#info").show();
			}
		});
	}
});