$(function () {
	//外部参数
	var params = JcallShell.getRequestParams(true);
	//根据微信账号得到账号信息服务
	var GET_WEIXINACCOUNT_INFO_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinAppService.svc/WXAS_BA_GetWinXinAccountInfo";
	//生成医嘱单服务
	var CREATE_DOCTOR_ORDER_URL = JcallShell.System.Path.ROOT +	"/ServerWCF/ZhiFangWeiXinAppService.svc/WXAS_UDTO_SaveOSDoctorOrderForm";
	//获取采样费服务
	var SELECT_COLLOECTIONPRICE_URL = JcallShell.System.Path.ROOT + "/ServerWCF/ZhiFangWeiXinService.svc/ST_UDTO_SearchBParameterByHQL?isPlanish=true&fields=BParameter_ParaValue&where=bparameter.ParaNo='CollectionPrice'";
	//采样费用
	var COLLECTION_PRICE = '0';
	//是否勾选采样费
	var CollectionPriceChecked = true;
	
	//特推套餐选择
	$("#recommend-button").on("click",function(){
		location.href = "../package/recommend/list.html";
	});
	//固定套餐选择
	$("#fixed-button").on("click",function(){
		location.href = "../package/fixed/list.html";
	});
	//分类套餐选择
	$("#fixed-type-button").on("click",function(){
		location.href = "../package/fixed/type/index.html";
	});
	
	//生成医嘱单按钮
	$("#submit-button").on("click",function(){
		var patientInfo = JcallShell.LocalStorage.PatientOrder.getPatient(),
			list = JcallShell.LocalStorage.PatientOrder.getPackageList()
			
		var isValid = patientInfo && list.length > 0;
		
		//没有选患者
		if(!patientInfo){
			$.toptip('患者不存在，请退出重新登录！', 'error');
			return;
		}
		//患者信息-年龄、性别不存在时，需要提醒维护，然后才能开医嘱
		if(!patientInfo.SexID || !patientInfo.Birthday){
			alert("患者的出生年月和性别必须维护才能开医嘱单！");
			return;
		}
		//出生年月存在错误
		var age = jsGetAge(patientInfo.Birthday);
		if(age === null){
			alert("患者的出生年月存在错误，请重新维护！");
			return;
		}
		//没有选套餐
		if(list.length == 0){
			alert("请先选择套餐！");
			return;
		}
		
		$("#createDoctorOrderModal").modal({ backdrop: 'static', keyboard: false });
	});
	//确定生成医嘱单
	$("#createDoctorOrder-button").on("click",function(){
		createDoctorOrder();
	});
	
	//编辑按钮
	$("#edit-button").on("click",function(){
		$("#submit-div").hide();
		$("#edit-div").show();
		initContent();
	});
	
	//取消医嘱单
	$("#cancel-button").on("click",function(){
		$("#cancelDoctorOrderModal").modal({ backdrop: 'static', keyboard: false });
	});
	//确定取消医嘱单
	$("#cancelDoctorOrder-button").on("click",function(){
		$("#cancelDoctorOrderModal").modal("hide");
		JcallShell.LocalStorage.PatientOrder.removeAll();
		initContent();
	});
	
	//编辑确定按钮
	$("#ok-button").on("click",function(){
		$("#edit-div").hide();
		$("#submit-div").show();
		initContent();
	});
	//删除按钮
	$("#del-button").on("click",function(){
		var checkboxs = $("input[name='checkbox']"),
			len = checkboxs.length,
			checkedIds = [];
		for(var i=0;i<len;i++){
			var checked = $(checkboxs[i]).is(':checked');
			if(checked){
				checkedIds.push($(checkboxs[i]).attr("id"));
			}
		}
		for(var i in checkedIds){
			var id = checkedIds[i];
			JcallShell.LocalStorage.PatientOrder.removePackege(id);//删除套餐
			$("label[for='" + id + "']").remove();
		}
		changeTotalPriceHtml();//更新总价内容
	});
	//患者账号信息
	function getPatientInfo(WeiXinAccount,callback){
		var url = GET_WEIXINACCOUNT_INFO_URL + "?account=" + WeiXinAccount;
		//Id,Name,UserName,SexID,HeadImgUrl,Birthday,MobileCode,WeiXinUserID,WeiXinAccount
		JcallShell.Server.ajax({
            url: url
       	}, function (data){
       	    if (data.success) {
       			callback(data.value || null);
       		}else{
       			alert(data.msg);
       		}
        });
	}
	//更新患者账号显示内容
	function changePastientHtml(){
		var data = JcallShell.LocalStorage.PatientOrder.getPatient();
		
		var html = '';
		if(data){
			//姓名、性别、年龄、Id
			var sexName = (data.SexID == "1") ? "<b style='color:green;'>【男】</b>" : "<b style='color:green;'>【女】</b>";
			var age = jsGetAge(data.Birthday);
			html = "患者：" + data.Name + " (" + data.UserName + ") " + sexName + " " + age + "岁";
		}
			
		$("#patient-div").html(html);
	}
	//更新总价内容
	function changeTotalPriceHtml() {
	    var list = JcallShell.LocalStorage.PatientOrder.getPackageList(),
			DoctorBonusPercent = JcallShell.Cookie.get(JcallShell.Cookie.map.DoctorBonusPercent),
			CollectionPrice = CollectionPriceChecked ? parseFloat(COLLECTION_PRICE || '0') : 0,//采样费
			CollectionPriceText = parseFloat(COLLECTION_PRICE || '0').toFixed(2),//采样费文字
			totalPrice = CollectionPrice,
			BonusPrice = 0,
	    	html = "";
	    
	    if (JcallShell.Cookie.get(JcallShell.Cookie.map.DoctorAccountType) && 
	    	JcallShell.Cookie.get(JcallShell.Cookie.map.DoctorAccountType) == "2") {
	        for (var i in list) {
	            totalPrice += parseFloat(list[i].GreatMasterPrice);	           
	        }
	    }else {
	        for (var i in list) {
	            totalPrice += parseFloat(list[i].Price);
	            var BonusPercent = parseFloat(list[i].BonusPercent);
	            if (BonusPercent && !isNaN(BonusPercent)) {
	                BonusPrice += BonusPercent;
	            } else {
	                BonusPrice += parseFloat(list[i].Price) * parseFloat(DoctorBonusPercent) / 100;
	            }
	        }
	    }
	    
	    html =
	    '<div class="weui-cells weui-cells_checkbox" style="margin:0;font-size:14px;">' +
			'<label class="weui-cell weui-check__label" for="CollectionPriceCheckbox" style="margin:0;padding:0;">' +
				'<div class="weui-cell__hd">' +
					'<input type="checkbox" class="weui-check" name="CollectionPriceCheckbox" id="CollectionPriceCheckbox" ' +
					'onclick="onCollectionPriceCheckboxChange(this);"' +
					(CollectionPriceChecked ? 'checked="checked"' : '') + '>' +
					'<i class="weui-icon-checked"></i>' +
				'</div>' +
				'<div class="weui-cell__bd">' +
					'<b><span>采样费:</span><span style="color:red;">￥' + CollectionPriceText + '</span></b>' +
				'</div>' +
			'</label>' +
		'</div>' +
        '<b style="color:#169ada;">' +
        	'总价:<span style="color:red;padding-right:5px;">￥' + totalPrice.toFixed(2) + '</span>' +
        	//'积分:<span style="color:red;">' + Math.round(BonusPrice) + '</span>' +
        '</b>' +
        '</br><span style="font-size:8px;">以上信息为当前价格，总价以实际缴费时为准！</span>';
            
	    $("#price-div").html(html);
	}
	//采样费选中处理
	function onCollectionPriceCheckboxChange(checkbox){
		CollectionPriceChecked = $(checkbox).is(':checked');//是否选中
		changeTotalPriceHtml();//更新总价内容
	}
	window.onCollectionPriceCheckboxChange = onCollectionPriceCheckboxChange;
	//更改套餐列表内容
	function changePackageListHtml() {
		var list = JcallShell.LocalStorage.PatientOrder.getPackageList(),
			isHidden = $("#edit-div").is(":hidden"),
    		//templet = isHidden ? getRowTemplet() : getEditRowTemplet(),
    		templet = getRowTemplet2(!isHidden),
			defaultPic = JcallShell.System.Path.ROOT + '/Images/dajia/logo.jpg',
			html = [];
		
		html.push('<div class="weui-cells weui-cells_checkbox" style="margin:0;font-size:14px;">');
		for(var i in list) {
			var row = templet;
			var TypeIcon = "";
			if(list[i].Type == 'fixed'){
				TypeIcon = '<span style="margin-left:5px;padding:2px 5px;color:#169ada;border:1px solid #169ada;">固</span>'
			}else{
				TypeIcon = '<span style="margin-left:5px;padding:2px 5px;color:#5cb85c;border:1px solid #5cb85c;">特</span>'
			}
			row = row.replace(/{Pic}/g, list[i].Pic || defaultPic);
			row = row.replace(/{TypeIcon}/g, TypeIcon);
			row = row.replace(/{Name}/g, list[i].CName);
			row = row.replace(/{MarketPrice}/g, list[i].MarketPrice);
			row = row.replace(/{GreatMasterPrice}/g, list[i].GreatMasterPrice);
			if (JcallShell.Cookie.get(JcallShell.Cookie.map.DoctorAccountType) && JcallShell.Cookie.get(JcallShell.Cookie.map.DoctorAccountType) == "2") {
			    row = row.replace(/{Price}/g, list[i].GreatMasterPrice);
			}
			else {
			    row = row.replace(/{Price}/g, list[i].Price);
			}
			row = row.replace(/{Id}/g, list[i].Id);
			row = row.replace(/{Type}/g, list[i].Type);
			html.push(row);
		}
		html.push('</div>');
		
		$("#list").html(html.join(""));
	}
	//获取列表行模板
	function getRowTemplet() {
		var templet =
		'<div class="list-div">' +
			'<img style="float:left;margin-left:5px;" src="{Pic}"></img>' +
			'<div style="float:left;padding-left:5px;">' +
				'<div style="color:#169ada;"><b>{Name}</b></div>' +
				'<div>' +
					'<s style="color:red;">市场价:￥{MarketPrice}</s>' +						
				'</div>' +
				'<div><a>实际价:￥{Price}</a>{TypeIcon}</div>' +
			'</div>' +
			'<div style="float:left;width:100%;">' +
				'<div class="list-div-button" onclick="showInfo(\'{Id}\',\'{Type}\')">详情</div>' +
			'</div>' +
		'</div>';
			
//		'<div class="weui-cell weui-cell_swiped">' +
//			'<div class="weui-cell__bd">' +
//				'<div class="weui-cell">' +
//					'<div class="weui-cell__bd">' +
//						'<p>左滑列表</p>' +
//					'</div>' +
//					'<div class="weui-cell__ft">向左滑动试试</div>' +
//				'</div>' +
//			'</div>' +
//			'<div class="weui-cell__ft">' +
//				'<a class="weui-swiped-btn weui-swiped-btn_warn delete-swipeout" href="javascript:">删除</a>' +
//			'</div>' +
//		'</div>';
		
		return templet;
	}
	//获取列表编辑行模板
	function getEditRowTemplet() {
		var templet =
		'<label class="weui-cell weui-check__label" for="s11">' +
			'<div class="weui-cell__hd">' +
				'<input type="checkbox" class="weui-check" name="checkbox1" id="s11" checked="checked">' +
				'<i class="weui-icon-checked"></i>' +
			'</div>' +
			'<div class="weui-cell__bd">' +
				'<div class="list-div">' +
					'<img style="float:left;margin-left:5px;" src="{Pic}"></img>' +
					'<div style="float:left;padding-left:5px;">' +
						'<div style="color:#169ada;"><b>{Name}</b></div>' +
						'<div>' +
							'<s style="color:red;">市场价:￥{MarketPrice}</s>' +						
						'</div>' +
						'<div><a>实际价:￥{Price}</a>{TypeIcon}</div>' +
					'</div>' +
					'<div style="float:left;width:100%;">' +
						'<div class="list-div-button" onclick="showInfo(\'{Id}\',\'{Type}\')">详情</div>' +
					'</div>' +
				'</div>' +
			'</div>' +
		'</label>';
		
		return templet;
	}
	//获取列表编辑行模板
	function getRowTemplet2(isEdit) {
		//复选框组件
		var checkbox = isEdit ? 
		'<div class="weui-cell__hd">' +
			'<input type="checkbox" class="weui-check" name="checkbox" id="{Id}">' +
			'<i class="weui-icon-checked"></i>' +
		'</div>' : '';
		//点击看详情
		var onclick = isEdit ? '' : 'onclick="showInfo(\'{Id}\',\'{Type}\')"';
		
		var templet =
		'<label class="weui-cell weui-check__label" for="{Id}" style="margin-bottom:0;padding:0;font-weight:normal;" ' + 
			onclick + '>' + checkbox +
			'<div class="weui-cell__bd">' +
				'<div class="list-div" style="border-bottom:0;">' +
					'<img style="float:left;margin-left:5px;" src="{Pic}"></img>' +
					'<div style="float:left;padding-left:5px;">' +
						'<div style="color:#169ada;"><b>{Name}</b></div>' +
						'<div>' +
							'<s style="color:red;">市场价:￥{MarketPrice}</s>' +						
						'</div>' +
						'<div><a>实际价:￥{Price}</a>{TypeIcon}</div>' +
					'</div>' +
				'</div>' +
			'</div>' +
		'</label>';
		
		return templet;
	}
	//根据生日计算年龄的方法
	function jsGetAge(strBirthday) {
		var bDay = new Date(strBirthday),
			nDay = new Date(),
			nbDay = new Date(nDay.getFullYear(),bDay.getMonth(),bDay.getDate()),
			age = nDay.getFullYear() - bDay.getFullYear();
			
		if (bDay.getTime()>nDay.getTime()) {
			return null;
		}
		
		return nbDay.getTime() <= nDay.getTime() ? age : --age;
	}
	//生成医嘱单
	function createDoctorOrder(){
		var patientInfo = JcallShell.LocalStorage.PatientOrder.getPatient(),
			list = JcallShell.LocalStorage.PatientOrder.getPackageList(),
			len = list.length,
			OrderItem = []; 
			
		//患者信息-年龄、性别不存在时，需要提醒维护，然后才能开医嘱
		if(!patientInfo.SexID || !patientInfo.Birthday){
			alert("患者的出生年月和性别必须维护才能开医嘱单！");
			return;
		}
		
		var age = jsGetAge(patientInfo.Birthday);
		if(age === null){
			alert("患者的出生年月存在错误，请重新维护！");
			return;
		}
		
		for(var i=0;i<len;i++){
			var item = {
				ItemNo:list[i].ItemNo
			};
			if(list[i].Type == 'fixed'){
				item.ItemID= list[i].Id;
			}else if(list[i].Type == 'recommend'){
				item.RecommendationItemProductID = list[i].Id;
				item.ItemID = list[i].ItemID;
			}
			OrderItem.push(item);
		}
		
		var data = {
			entity:{
				//HospitalID:'',//医院ID
				//HospitalName:'',//医院名称
				//DoctorAccountID:null,//医生信息ID //WeiXinUserID
				DoctorWeiXinUserID:patientInfo.Id,//医生微信ID
				DoctorName:patientInfo.UserName,//医生姓名 
				DoctorOpenID:patientInfo.WeiXinAccount,//
				//DeptID: JcallShell.Cookie.get(JcallShell.Cookie.map.DEPTID),//科室ID
				//DeptName: JcallShell.Cookie.get(JcallShell.Cookie.map.DEPTNAME),//科室名称
				
				UserAccountID:patientInfo.Id,//用户账户信息ID
				UserWeiXinUserID:patientInfo.Id,//用户微信ID
				UserName:patientInfo.UserName,//用户姓名
				UserOpenID:patientInfo.WeiXinAccount,//用户OpenID
				Age:age,//年龄
				//AgeUnitID:'1',//年龄单位ID
				AgeUnitName:'岁',//年龄单位名称
				SexID:patientInfo.SexID,//性别ID
				SexName:(patientInfo.SexID == '1' ? '男' : '女'),//性别名称
				PatNo:'',//病历号

				OrderItem:OrderItem,
				CollectionFlag:CollectionPriceChecked,
				CollectionPrice:COLLECTION_PRICE
			}
		};
		data = JcallShell.JSON.encode(data);
		var url = CREATE_DOCTOR_ORDER_URL;
		
		$("#loading-div").modal({ backdrop: 'static', keyboard: false });
		JcallShell.Server.ajax({
            url: url,
            type:'post',
            showError:true,
            data:data
       	}, function (data){
       		$("#loading-div").modal("hide");
       		if(data.success){
       			onCreateDoctorOrderSuccess();
       		}else{
       			alert(data.msg);
       		}
        });
	}
	//创建医嘱单成功后处理
	function onCreateDoctorOrderSuccess(){
		JcallShell.LocalStorage.PatientOrder.removeAll();
		initContent();
		$("#createDoctorOrderModal").modal("hide");
		//跳转到医嘱单列表页面
		location.href = "../orderd/list.html";
	}
	//查看套餐信息
	function showInfo(id,type) {
		//跳转到信息页面
		location.href = "../package/" + type + "/info.html?id=" + id;
	}
	window.showInfo = showInfo;
	//获取采样费用
	function getCollectionPrice(callback){
		var url = SELECT_COLLOECTIONPRICE_URL;
		JcallShell.Server.ajax({
            url: url
       	}, function (data){
       	    if (data.success) {
       	    	var list = (data.value || {}).list || [];
       	    	if(list.length == 1){
       	    		COLLECTION_PRICE = list[0].BParameter_ParaValue || '0';
       	    	}else{
       	    		COLLECTION_PRICE = '0';
       	    	}
       			callback();
       		}else{
       			alert(data.msg);
       		}
        });
	}
	
	//初始化患者信息
	function initPatientInfo(callback){
		var WeiXinAccount = JcallShell.Cookie.get(JcallShell.Cookie.map.WEIXINACCOUNT);
		getPatientInfo(WeiXinAccount,function(data){
		    JcallShell.LocalStorage.PatientOrder.setPatient(data);
			callback();
		});
	};
	//初始化内容
    function initContent(){
    	//初始化患者信息
    	initPatientInfo(function(){
    		//更新患者账号显示内容
			changePastientHtml();
	    	//更新总价内容
	    	changeTotalPriceHtml();
	    	//更改套餐列表内容
	    	changePackageListHtml();
    	});
    }
    //初始化页面
    function initHtml(){
    	//获取采样费用
    	getCollectionPrice(function(){
    		initContent();
    	});
    }
    initHtml();
});