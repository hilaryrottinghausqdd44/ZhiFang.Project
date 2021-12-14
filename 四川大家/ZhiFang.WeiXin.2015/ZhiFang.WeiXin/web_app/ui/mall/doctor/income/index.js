$(function () {
	//获取医生咨询费用信息服务
	//var GET_DOCTOR_CHARGE_INFO = "info.json";
	var GET_DOCTOR_CHARGE_INFO = JcallShell.System.Path.ROOT + 
		"/ServerWCF/ZhiFangWeiXinAppDoctService.svc/WXADS_BA_GetDoctorChargeInfo";
	
	//信息数据
	var INFO_DATA = null;
	//余额合计
	var SURPLUS_SUM = 0;
	//已提取合计
	var OBTAIN_SUM = 0;
	
	//获取医生咨询费用信息
	function getInfoData(callback){
        $("#loading-div").modal({ backdrop: 'static', keyboard: false });
        JcallShell.Server.ajax({
            url: GET_DOCTOR_CHARGE_INFO
        }, function (data) {
            $("#loading-div").modal("hide");
            callback(data);
        });
	}
	//数据处理
	function changeData(){
		//医生患者消费单列表
		var uList = INFO_DATA.OSUserConsumerList || [],
			uLen = uList.length;
			
		for(var i=0;i<uLen;i++){
			SURPLUS_SUM += parseFloat(uList[i].Price);
		}
		
		//医生结算单列表
		var dList = INFO_DATA.OSDoctorBonusList || [],
			dLen = dList.length;
			
		for(var i=0;i<dLen;i++){
			OBTAIN_SUM += parseFloat(dList[i].Price);
		}
	}
	//更改页面内容
	function changeHtml(){
		$("#surplus-sum").html(SURPLUS_SUM);//余额合计
		$("#obtain-sum").html(OBTAIN_SUM);//已提取合计
		$("#bank-name").html(INFO_DATA.BN);//开户银行
		$("#bank-card-no").html(INFO_DATA.BA);//银行卡号
		//详细转入转出
		var listHtml = getListHtml();
		$("#list").html(listHtml);
		
		$("#edit-button").on("click",function(){
			alert("银行信息编辑");
		});
	}
	//获取详细转入转出列表HTML
	function getListHtml(){
		var html = [];
			
		//医生患者消费单列表
		var uList = INFO_DATA.OSUserConsumerList || [],
			uLen = uList.length,
			uTemplet = getRowTemplet_U();
			
		for(var i=0;i<uLen;i++){
			var row = uTemplet;
			row = row.replace(/{Code}/g, uList[i].CFCode);
			row = row.replace(/{Time}/g, uList[i].DT);
			row = row.replace(/{Price}/g, uList[i].Price);
			row = row.replace(/{DOFID}/g, uList[i].DOFID);
			html.push(row);
		}
		
		//医生结算单列表
		var dList = INFO_DATA.OSDoctorBonusList || [],
			dLen = dList.length,
			dTemplet = getRowTemplet_D();
			
		for(var i=0;i<dLen;i++){
			var row = dTemplet;
			row = row.replace(/{Code}/g, dList[i].BonusCode);
			row = row.replace(/{Time}/g, dList[i].DT);
			row = row.replace(/{Price}/g, dList[i].Price);
			html.push(row);
		}

		return html.join("");
	}
	//获取列表行模板-医生患者消费单列表
	function getRowTemplet_U() {
		var templet =
		'<div style="padding:10px;border-bottom:1px dashed #e0e0e0;">' +
			'<div>消费单号:{Code}</div>' +
			'<div style="padding:5px 0;">' +
				'<span style="color:#e0e0e0;">{Time}</span>' +
				'<span style="margin-left:10px;color:green;">转入 +{Price}</span>' +
			'</div>' +
			'<div class="info-button" onclick="showInfo(\'{DOFID}\')">详情</div>' +
		'</div>';
			
		return templet;
	}
	//获取列表行模板-医生结算单列表
	function getRowTemplet_D() {
		var templet =
		'<div style="padding:10px;border-bottom:1px dashed #e0e0e0;">' +
			'<div>结算单号:{CodeNo}</div>' +
			'<div style="padding:5px 0;">' +
				'<span style="color:#e0e0e0;">{Time}</span>' +
				'<span style="margin-left:10px;color:red;">转出 -{Price}</span>' +
			'</div>' +
		'</div>';
			
		return templet;
	}
	function showInfo(id) {
		//跳转到信息页面
		location.href = "info.html?hasbutton=1&id=" + id;
	}
	window.showInfo = showInfo;
	
	//显示错误信息
    function showError(msg) {
        $("#list").html('<div style="margin:20px 10px;color:#169ada;text-align:center;">' + msg + '</div>');
    }
	//初始化页面
    function initHtml(){
    	getInfoData(function(data){
			if (data.success == true) {
				alert(JcallShell.JSON.encode(data.value));
				INFO_DATA = data.value;
				changeData();//数据处理
                changeHtml();//更改页面内容
            } else {
                showError(data.msg);
            }
		});
    }
    initHtml();
});