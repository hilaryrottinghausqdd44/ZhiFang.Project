layui.extend({
	uxutil:'ux/util'
}).use(['uxutil','form','laydate'],function(){
	var $ = layui.$,
		uxutil = layui.uxutil,
		form = layui.form,
		laydate = layui.laydate;
		
	//新增服务
	var ADD_INFO_URL = uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_AddSAccountRegister';
	//账号申请表查询服务
	var GET_SACCOUNT_REGISTER_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchSAccountRegisterByHQL?isPlanish=true';
    //省份查询
    var GET_PROVINCEL_LIST_URL= uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBProvinceByHQL?isPlanish=true';
	 //城市查询
    var GET_CITY_LIST_URL= uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBCityByHQL?isPlanish=true';
	//区县查询
    var GET_COUNTY_LIST_URL= uxutil.path.ROOT + '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBCountyByHQL?isPlanish=true';
	
	//数据提交
	form.on('submit(submit)', function(data){
		//根据账号OR手机号OR身份证去账号申请表查询是否存在非打回状态的数据。如果不存在再请求新增服务。
		isValid(data.field.MobileTel,function(msg){
			if(msg){
				layer.msg(msg,{ icon: 5, anim: 6 ,time:2000});
				return false;
			}else{
				//根据身份证号取出生日期(有可能焦点未离开就保存)
				data.field.Birthday = getBirthdayByIdNumber($("[name='IdNumber']").val());
				if (data.field.ishospitalm == 1){
					if (data.field.CityID == ""  || data.field.ProvinceID == "") {
						layer.msg("省份、城市不可为空");
						return false;
					}
					//data.field.HospitalName = "";
				} else {
					if (data.field.HospitalName == "") {
						layer.msg("医院名称不可为空且必须是全称！");
						return false;
					}
				}
				delete data.field.ishospitalm;
				
                 //申請數據處理
		        data.field.Birthday = uxutil.date.toServerDate(data.field.Birthday);
		        data.field.StatusId = 0;
		        data.field.StatusName = '申请中';
		        data.field.CityID = Number(data.field.CityID);		
		        data.field.CountyID = Number(data.field.CountyID);
		        data.field.ProvinceID = Number(data.field.ProvinceID);
				onSubmit(data.field);
			}
		});
		return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
	});
	//数据提交
	form.on('submit(reset)', function(data){
		form.reset();
	});
    form.on('select(CountyID)', function (data) {
		$('#CountyName').val($("#CountyID").children('option:selected')[0].text);
	});
	form.on('select(CityID)', function (data) {
		$('#CityName').val($("#CityID").children('option:selected')[0].text);
		Countylist($("#CityID").children('option:selected')[0].value);
	});
	form.on('select(ProvinceID)', function (data) {
		$('#ProvinceName').val($("#ProvinceID").children('option:selected')[0].text);
	});
	//身份证失去焦点后自动填写出生日期
	$("[name='IdNumber']").blur(function res() {
	    $("#Birthday").val(getBirthdayByIdNumber($("[name='IdNumber']").val()));
    });
	function onSubmit(params) {
		var layerIndex = layer.load();//开启保护层
		uxutil.server.ajax({
			type:'post',
			url:ADD_INFO_URL,
			data:JSON.stringify({
				entity:params
			})
		},function(data){
			layer.close(layerIndex);//关闭保护层
			if(data.success){
				layer.open({
					type: 1,
					title: false,
					closeBtn: false,
					area: '300px;',
					shade: 0.8,
					btn: ['返回登录页面'],
					btnAlign: 'c',
					moveType: 1, //拖拽模式，0或者
					content: '<div style="padding: 50px; line-height: 22px; background-color: #393D49; color: #fff; font-weight: 300;">已提交账号申请，请等待管理员审批</div>',
					success: function(layero){
						var btn = layero.find('.layui-layer-btn');
						btn.find('.layui-layer-btn0').on("click",function(){
							parent.layer.closeAll('iframe');
						});
					}
				});
			}else{
				layer.msg(data.ErrorInfo);
			}
		});
	}
	
	
    //根据身份证号取出生日期
    function getBirthdayByIdNumber(IdNumber) {  
        var birthday = "";  
        if(IdNumber != null && IdNumber != ""){  
            if(IdNumber.length == 15){  
                birthday = "19"+IdNumber.substr(6,6);  
            } else if(IdNumber.length == 18){  
                birthday = IdNumber.substr(6,8);  
            }  
            birthday = birthday.replace(/(.{4})(.{2})/,"$1-$2-");  
        }  
        return birthday;  
    }
    //根据账号OR手机号OR身份证去账号申请表查询是否存在非打回状态的数据
    function isValid(MobileTel,callback){
    	var where ="saccountregister.StatusId!=2 and  saccountregister.MobileTel='"+ MobileTel+"'";
		var url = GET_SACCOUNT_REGISTER_LIST_URL+'&fields=SAccountRegister_MobileTel,SAccountRegister_IdNumber&where='+where;
        var layerIndex = layer.load();//开启保护层
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.close(layerIndex);//关闭保护层
			if(data.success){
				var list = data.value.list || [];
				var msg = '';
				for(var i=0;i<list.length;i++){
					if(list[i].SAccountRegister_MobileTel == MobileTel)msg ='手机号码已被注册过,不能重复申请';
					if(msg)break;
				}
				callback(msg);
			}else{
				layer.msg(data.ErrorInfo);
			}
		});
	}
	
	//获取省份列表
	function ProvinceList(){
		var url = GET_PROVINCEL_LIST_URL + '&fields=BProvince_Name,BProvince_Id&where=IsUse=1';
		layer.load();
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.closeAll('loading');
			if(data.success){
				var list = (data.value || {}).list || [];
				var userList = ['<option value="">请选择</option>'];
				for(var i in list){
					userList.push('<option value="' + list[i].BProvince_Id + '">' + list[i].BProvince_Name + '</option>');
				}
				$("#ProvinceID").html(userList.join(''));
				form.render('select'); //需要渲染一下;
			}else{
				layer.msg(data.msg);
			}
		});
	}
	//城市下拉框
	function CityList(){
		var url = GET_CITY_LIST_URL + '&fields=BCity_Name,BCity_Id&where=IsUse=1';
		layer.load();
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.closeAll('loading');
			if(data.success){
				var list = (data.value || {}).list || [];
				var userList = ['<option value="">请选择</option>'];
				for(var i in list){
					userList.push('<option value="' + list[i].BCity_Id + '">' + list[i].BCity_Name + '</option>');
				}
				$("#CityID").html(userList.join(''));
				form.render('select'); //需要渲染一下;
			}else{
				layer.msg(data.msg);
			}
		});
	}
	
	//区县-下拉框加载
	function Countylist(where){
		var url = GET_COUNTY_LIST_URL + '&fields=BCounty_Name,BCounty_Id';
		if (where) {
			url += '&where=IsUse=1 and bcounty.BCity.Id=' + where;
		} else {
			url += '&where=IsUse=1';
		}
		layer.load();
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.closeAll('loading');
			if(data.success){
				var list = (data.value || {}).list || [];
				var userList = ['<option value="">请选择</option>'];
				for(var i in list){
					userList.push('<option value="' + list[i].BCounty_Id + '">' + list[i].BCounty_Name + '</option>');
				}
				$("#CountyID").html(userList.join(''));
				form.render('select'); //需要渲染一下;
			}else{
				layer.msg(data.msg);
			}
		});
	}

	//初始化
	function init(){
		//执行一个laydate实例
	    laydate.render({
	        elem: '#Birthday' //指定元素
	    });
	    //城市-下拉框加载
	    CityList();
	    //区县-下拉框加载
	    Countylist();
	    //省份-下拉框加载
	    ProvinceList();
	}
	//初始化
	init();
    
});