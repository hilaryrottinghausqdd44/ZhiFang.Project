$(function () {
	//修改账号信息服务地址
	var EDIT_ACCOUNT_URL = JcallShell.System.Path.ROOT + 
		"/ServerWCF/DictionaryService.svc/ST_UDTO_UpdateBWeiXinAccountByField";
	
    var currYear = (new Date()).getFullYear();
	$("#Birthday").mobiscroll({
		preset : 'date',
		theme: 'android-ics light', //皮肤样式
        display: 'modal', //显示方式 
        mode: 'scroller', //日期选择模式
		dateFormat: 'yy-mm-dd',
		lang: 'zh',
		showNow: true,
		nowText: "今天",
		maxDate:new Date(),
        startYear: currYear - 150, //开始年份
        endYear: (new Date()).getFullYear() //结束年份
	});
	$("#Birthday").on("change",function(){
		$("form").data('bootstrapValidator').resetForm().validate();
	});
	
	$("#edit-button").on("click",function(){
		$("#edit-button").hide();
		$("#save-button").show();
		onDisabled(false);
	});
	//显示修改按钮
	$("#edit-button").show();
	
	$("#save-button").on("click",function(){
		onSave(function(){
			var info = JcallShell.LocalStorage.User.getAccount(true);//账户信息
			info.BWeiXinAccount_Name = $("#Name").val();
			info.BWeiXinAccount_SexID = $("#SexID").val();
			info.BWeiXinAccount_Birthday = $("#Birthday").val();
			info.BWeiXinAccount_MobileCode = $("#MobileCode").val();
			info.BWeiXinAccount_IDNumber = $("#IDNumber").val();
			
			info = JcallShell.JSON.encode(info);//转码
			JcallShell.LocalStorage.User.setAccount(info);//初始化账户信息
			
			$("form").data('bootstrapValidator').resetForm();
			alert("保存成功");
		});
	});
	
	//是否只读模式
	function onDisabled(isDisabled){
		isDisabled = isDisabled || false;
		$("#Name").attr("disabled",isDisabled);
		$("#SexID").attr("disabled",isDisabled);
		$("#Birthday").attr("disabled",isDisabled);
		$("#MobileCode").attr("disabled",isDisabled);
		$("#IDNumber").attr("disabled",isDisabled);
	}
	//获取账户信息数据
	function initAccountData(callback){
		var info = JcallShell.LocalStorage.User.getAccount(true);//账户信息
		$("#Name").val(info.BWeiXinAccount_Name);
		$("#SexID").val(info.BWeiXinAccount_SexID);
		if(info.BWeiXinAccount_Birthday){
			$("#Birthday").val(JcallShell.Date.toString(info.BWeiXinAccount_Birthday,true));
		}
		$("#MobileCode").val(info.BWeiXinAccount_MobileCode);
		$("#IDNumber").val(info.BWeiXinAccount_IDNumber);
		$('.selectpicker').selectpicker('refresh');
	}
	//保存账户信息
	function onSave(callback){
	    if(!$("form").data('bootstrapValidator').validate().isValid()){
	    	return false;
	    }
	    
	    var info = JcallShell.LocalStorage.User.getAccount(true);//账户信息
	    var data = {
	    	entity:{
	    		Id:info.BWeiXinAccount_Id,
	    		Name:$("#Name").val(),
		    	SexID:$("#SexID").val(),
		    	Birthday:JcallShell.Date.toServerDate($("#Birthday").val()),
		    	MobileCode:$("#MobileCode").val(),
		    	IDNumber:$("#IDNumber").val()
	    	},
	    	fields:'Id,Name,SexID,Birthday,MobileCode,IDNumber'
	    };
	    
	    data = JcallShell.JSON.encode(data);
	    $("#loading-div").modal({ backdrop: 'static', keyboard: false });
	    JcallShell.Server.ajax({
			url:EDIT_ACCOUNT_URL,
			type:'post',
			showError:true,
            data:data
		},function(data){
			$("#loading-div").modal("hide");
			if(data.success){
				callback();
			}else{
				alert(data.msg);
			}
		});
	}
	//表单数据格式校验
	function onFromValidator(){
		$('form').bootstrapValidator({
			message: 'This value is not valid',
			feedbackIcons: {
				valid: 'glyphicon glyphicon-ok',
				invalid: 'glyphicon glyphicon-remove',
				validating: 'glyphicon glyphicon-refresh'
			},
			fields: {
				Name: {
					message: '姓名验证失败',
					validators: {
						notEmpty: {
							message: '姓名不能为空'
						},
						stringLength: {
							min: 1,
							max: 20,
							message: '姓名长度必须在1到20位之间'
						}
					}
				},
				SexID: {
					validators: {
						notEmpty: {
							message: '性别不能为空'
						}
					}
				},
				Birthday: {
					validators: {
						notEmpty: {
							message: '出生年月不能为空'
						}
					}
				},
				MobileCode: {
					validators: {
						stringLength: {
							min: 0,
							max: 11,
							message: '联系电话长度最大11位'
						}
					}
				},
				IDNumber: {
					validators: {
						id: {
							country: 'CN',
							message: '身份证格式错误'
						}
					}
				}
			}
		});
	}
	
	setTimeout(function(){
		initAccountData();
		onDisabled(true);
		onFromValidator();
	},100);
});