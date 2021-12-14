/**
 * @name：分发规则表单信息  (因必须有分发规则类型，因此在新增界面打开时会先查一遍看有没有规则类型，如果没有则新增一条默认的分发规则类型)
 * @author：liangyl
 * @version 2020-08-18
 */
layui.extend({
	uxutil:'ux/util',
	CommonSelectEnum:'modules/common/select/enum',
	CommonSelectSickType:'modules/common/select/sicktype',
	CommonSelectDept:'modules/common/select/dept',
	tableSelect: '../src/tableSelect/tableSelect'
}).use(['uxutil','form','laydate','CommonSelectEnum','tableSelect','CommonSelectSickType','CommonSelectDept'],function(){
	
	var $ = layui.$,
		form = layui.form,
		laydate = layui.laydate,
		CommonSelectEnum = layui.CommonSelectEnum,
		CommonSelectSickType = layui.CommonSelectSickType,
		CommonSelectDept = layui.CommonSelectDept,
		tableSelect = layui.tableSelect,
		uxutil = layui.uxutil;
	
		//获取采样组服务
	var GET_SAMPLING_GROUP_URL=uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSamplingGroupByHQL?isPlanish=true";
	//获取样本类型服务
	var GET_SAMPLE_TYPE_URL=uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true";
    //获取分发规则类型
    var GET_RULE_TYPE_URL =uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleTypeByHQL?isPlanish=true';
		//就诊类型
	var GET_SICK_TYPE_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeByHQL?isPlanish=true";
	//获取指定实体字段的最大号
    var GET_MAXNO_URL =  uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField?entityName=LBTranRule&entityField=DispOrder';
	//分发规则生成下一样本号
	var NEXT_SAMPLENO_URL =  uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_GetLBTranRuleNextSampleNo';
	//新增
	var	ADD_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBTranRule';
	//修改
	var	UPADTE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBTranRuleByField';
	 //外部参数
	var PARAMS = uxutil.params.get(true);
	//小组
	var SECTIONID = PARAMS.SECTIONID;
	//默认分发规则
	var DEFAULTDRULETYPEID = PARAMS.DEFAULTDRULETYPEID;
	//id
	var ID = PARAMS.ID;
	 //按钮是否可点击
	var BUTTON_CAN_CLICK = true;
	//表单状态
	var formtype  = 'add';
	
	//就诊类型
	var SickTypeID = null;
	//复位周期
	var ResetType = null;
	//外送单位
	var ClientID = null;
	//送检科室
	var DeptID = null;
	//检验日期
	var TestDelayDates = 0;
    //下拉框 -- icon 前存在icon 则点击该icon 等同于点击input
    $("input.layui-input+.layui-icon").on('click', function () {
        if (!$(this).hasClass("myDate")) {
            $(this).prev('input.layui-input')[0].click();
            return false;//不加的话 不能弹出
        }
    });
    //保存
	form.on("submit(save)",function(obj){
		onSaveClick(obj);	
	});
	$('#cancel').on('click',function(){
		parent.layer.closeAll('iframe');
	});
    //初始化下拉框
    function initselect(){
//  	var LabId="";
//  	if(uxutil.cookie.get(uxutil.cookie.map.LABID))LabId = 'and hrdeptidentity.LabID='+uxutil.cookie.get(uxutil.cookie.map.LABID);
    	var list=[
            {id:'LBTranRule_SamplingGroupID',name:'LBTranRule_SamplingGroupName',url:GET_SAMPLING_GROUP_URL,pre:'LBSamplingGroup',searchKey:''},//采样组
            {id:'LBTranRule_SampleTypeID',name:'LBTranRule_SampleTypeName',url:GET_SAMPLE_TYPE_URL,pre:'LBSampleType',searchKey:''},//样本类型
            {id:'LBTranRule_LBTranRuleType_Id',name:'LBTranRule_LBTranRuleType_CName',url:GET_RULE_TYPE_URL,pre:'LBTranRuleType',searchKey:''},//规则类型
            {id:'LBTranRule_SickTypeID',name:'LBTranRule_SickTypeName',url:GET_SICK_TYPE_URL,pre:'LBTranRule',searchKey:''}//规则类型
//          {id:'LBTranRule_ClientID',name:'LBTranRule_ClientName',url:GET_DEPT_IDENTITY_URL+"&where=hrdeptidentity.TSysCode='1001106' and hrdeptidentity.SystemCode='ZF_PRE'"+LabId,pre:'HRDeptIdentity_HRDept',searchKey:'hrdeptidentity.HRDept'}//外送单位
//          {id:'LBTranRule_DeptID',name:'LBTranRule_DeptName',url:GET_DEPT_IDENTITY_URL+"&where=hrdeptidentity.TSysCode='1001101' and hrdeptidentity.SystemCode='ZF_PRE'"+LabId,pre:'HRDeptIdentity_HRDept',searchKey:'hrdeptidentity.HRDept'}//送检单位
    	];
        for(var i=0;i<list.length;i++){
            SelectList(list[i].id,list[i].name,list[i].url,list[i].pre,list[i].searchKey);
        }
		//检验日期
		$("#LBTranRule_TestDelayDates").append(DayList());
		$('#LBTranRule_TestDelayDates').val(TestDelayDates);
		form.render('select');
		   //复位周期
		CommonSelectEnum.render({
			domId:'LBTranRule_ResetType',
			className:'TranRuleResetType',
			done:function(){
				$('#LBTranRule_ResetType').val(ResetType);
				form.render('select');
			}
		});
		 //就诊类型
		CommonSelectSickType.render({
			domId:'LBTranRule_SickTypeID',
			done:function(){
				$('#LBTranRule_SickTypeID').val(SickTypeID);
				form.render('select');
			}
		});
		//送检科室
		CommonSelectDept.render({
			domId:'LBTranRule_DeptID',
			code:'1001101',
			done:function(){
				$('#LBTranRule_DeptID').val(DeptID);
				form.render('select');
			}
		});
       //外送单位
		CommonSelectDept.render({
			domId:'LBTranRule_ClientID',
			code:'1001106',
			done:function(){
				$('#LBTranRule_ClientID').val(ClientID);
				form.render('select');
			}
		}); 
    }
    //初始化下拉选择项
    function SelectList(IdEl,NameEl,url,fieldname,searchKey) {
        if (!NameEl) return;
        var fields = ['Id','CName'];
       	url += '&fields='+fieldname+'_' + fields.join(','+fieldname+'_');
        if(!searchKey)searchKey =  fieldname.toLowerCase()+'.CName';
        tableSelect.render({
            elem: '#' + NameEl,	//定义输入框input对象 必填
            checkedKey: fieldname+'_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: searchKey,	//搜索输入框的name值 默认keyword
            searchPlaceholder: '名称/简称',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: 160,
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'numbers', title: '行号' },
                    { field: fieldname+'_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: fieldname+'_CName', minWidth: 100,flex:1, title: '名称', sort: false }
                ]],
                text: { none: '暂无相关数据' },
                response: function () {
                    return {
                        statusCode: true, //成功状态码
                        statusName: 'code', //code key
                        msgName: 'msg', //msg key
                        dataName: 'data' //data key
                    }
                },
                parseData: function (res) {//res即为原始返回的数据
                    if (!res) return;
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};                  
                    return {
                        "code": res.success ? 0 : 1, //解析接口状态
                        "msg": res.ErrorInfo, //解析提示文本
                        "count": data.count || 0, //解析数据长度
                        "data": data.list || []
                    };
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    var record = data.data[0];
                    $(elem).val(record[fieldname+"_CName"]);
                    if (IdEl) $("#" + IdEl).val(record[fieldname+"_Id"]);
					
                }else{
                	 $(elem).val("");
                    if (IdEl) $("#" + IdEl).val("");
                }
            }
        });
    }
    //检验日期下拉框数据
	function DayList() {
		var list = [],
			htmls = ['<option value="">请选择</option>'];
		for(var i=0;i<16;i++){
		 	htmls.push("<option value='" + i +"'>" + i + "</option>");
		}
		return htmls.join("");
	}
    function formatminutes(date) {
       $(".laydate-time-list li").eq(86).remove()
       $(".laydate-time-list li").css("width","50%")
       $(".laydate-time-list li ol li").css("width","100%")
    }
     /**@overwrite 获取新增的数据*/
	function getAddParams(data) {
		var entity ={
			IsUse:1,
			SectionID:SECTIONID,
			IsUseNextNo:data.LBTranRule_IsUseNextNo ? 1 :0,
			IsAutoPrint:data.LBTranRule_IsAutoPrint ? 1 :0,
			IsFollow:data.LBTranRule_IsFollow ? 1 :0,
			IsPrintProce:data.LBTranRule_IsPrintProce ? 1 :0,
			CName:data.LBTranRule_CName,
			SampleNoMin:data.LBTranRule_SampleNoMin,
			SampleNoMax:data.LBTranRule_SampleNoMax,
			SampleNoRule:data.LBTranRule_SampleNoRule,
			NextSampleNo:data.LBTranRule_NextSampleNo,
			UrgentState:data.LBTranRule_UrgentState,
			ResetType:data.LBTranRule_ResetType,
			TranWeek:data.LBTranRule_TranWeek,
			TranToWeek:data.LBTranRule_TranToWeek,
			ProceModel:data.LBTranRule_ProceModel,
			DispenseMemo:data.LBTranRule_DispenseMemo
		};
		entity.PrintCount = data.LBTranRule_PrintCount || 0;
//		if(data.LBTranRule_PrintCount)entity.PrintCount = data.PrintCount;
		if(data.LBTranRule_TestDelayDates)entity.TestDelayDates = data.LBTranRule_TestDelayDates;
		else
		    entity.TestDelayDates = 0;
		if(data.LBTranRule_DeptID)entity.DeptID = data.LBTranRule_DeptID;
        if(data.LBTranRule_ClientID)entity.ClientID = data.LBTranRule_ClientID;
        if(data.LBTranRule_SampleTypeID)entity.SampleTypeID = data.LBTranRule_SampleTypeID;
        if(data.LBTranRule_SamplingGroupID)entity.SamplingGroupID = data.LBTranRule_SamplingGroupID;
        if(data.LBTranRule_SickTypeID)entity.SickTypeID = data.LBTranRule_SickTypeID;
		if(data.LBTranRule_DispOrder)entity.DispOrder = Number(data.LBTranRule_DispOrder);
		else 
		   entity.DispOrder = 0;
		if(data.LBTranRule_SampleNoType)entity.SampleNoType = Number(data.LBTranRule_SampleNoType);
		else
		   entity.SampleNoType = 0;
        if(data.LBTranRule_LBTranRuleType_Id){ //分发规则   
        	entity.LBTranRuleType={
        		Id:data.LBTranRule_LBTranRuleType_Id,
        		DataTimeStamp:[0,0,0,0,0,0,0,0]
        	};
        }
        var sysdate = new Date();//应该取集成平台的系统服务时间
        var isBeginTimeValid  = uxutil.date.isValid(data.LBTranRule_UseTimeMin);
		var isEndTimeValid  = uxutil.date.isValid(data.LBTranRule_UseTimeMax);
		var date1 = uxutil.date.toString(sysdate,true);
		if(data.LBTranRule_UseTimeMin){
			var strBeginTime = data.LBTranRule_UseTimeMin;
			if(!isBeginTimeValid){
				strBeginTime =  date1+" "+strBeginTime;
			    entity.UseTimeMin=uxutil.date.toServerDate(strBeginTime);
			}
		}
		if(data.LBTranRule_UseTimeMax){
			var strEndTime = data.LBTranRule_UseTimeMax;
			if(!isEndTimeValid){
				strEndTime =  date1+" "+strEndTime;
			    entity.UseTimeMax=uxutil.date.toServerDate(strEndTime);
			}
		}
		var TranToWeek = '',weeks=[];
		//周一
		if(data['like1[1]'])weeks.push('1');
		if(data['like1[2]'])weeks.push('2');
		if(data['like1[3]'])weeks.push('3');
		if(data['like1[4]'])weeks.push('4');
		if(data['like1[5]'])weeks.push('5');
		if(data['like1[6]'])weeks.push('6');
		if(data['like1[7]'])weeks.push('7');
		if(weeks.length>0){
			TranToWeek = weeks.join(',');
		    entity.TranToWeek = TranToWeek;
		}
		return {entity: entity};
	};
	/**@overwrite 获取修改的数据*/
	function getEditParams(data) {
		var fields = "Id,CName,SampleNoMin,SampleNoMax,SampleNoRule,"+
		    "ResetType,NextSampleNo,IsUseNextNo,DispenseMemo,DispOrder,"+
		    "UrgentState,IsFollow,IsAutoPrint,TranToWeek,PrintCount,"+
		    "TranWeek,IsPrintProce,ProceModel,SampleNoType,TestDelayDates";
	    var entity = getAddParams(data);
	    
//      if(data["LBTranRule_PrintCount"])fields += ",PrintCount";
//      if(data["LBTranRule_TestDelayDates"])fields += ",TestDelayDates";
		if(data["LBTranRule_DeptID"])fields += ",DeptID";
		if(data["LBTranRule_SickTypeID"])fields += ",SickTypeID";
	    if(data["LBTranRule_SampleTypeID"])fields += ",SampleTypeID";
	    if(data["LBTranRule_SamplingGroupID"])fields += ",SamplingGroupID";
//		if(data["LBTranRule_SampleNoType"])fields += ",SampleNoType";
        if(data["LBTranRule_LBTranRuleType_Id"])fields += ",LBTranRuleType_Id";
	    if(data["LBTranRule_UseTimeMin"])fields += ",UseTimeMin";
		if(data["LBTranRule_UseTimeMax"])fields += ",UseTimeMax";
//		if(data["LBTranRule_DispOrder"])fields += ",DispOrder";	
	    if(data["LBTranRule_ClientID"])fields += ",ClientID";
		
		entity.fields = fields;//
		if (data["LBTranRule_Id"])
			entity.entity.Id = data["LBTranRule_Id"];
		return entity;
	};
	//保存
	function onSaveClick(obj){
		if(!BUTTON_CAN_CLICK)return;
		var url = formtype == 'add' ? ADD_URL : UPADTE_URL;
		var params = formtype == 'add' ? getAddParams(obj.field) : getEditParams(obj.field);
		if (!params) return;
		var id = params.entity.Id;
		params = JSON.stringify(params);
		//显示遮罩层
		var config1 = {
			type: "POST",
			url: url,
			data: params
		};
		var index = layer.load();
		uxutil.server.ajax(config1, function(data) {
			layer.close(index);
			//隐藏遮罩层
			if (data.success) {
				layer.msg('保存成功', {
					icon: 6, anim: 0 ,time:1000 
				}, function(){
					parent.layer.closeAll('iframe');
					parent.afterTranRuleUpdate(data);
				});
			} else {
				var msg = formtype=='add' ? '新增失败！' :'修改失败！';
				if(!data.msg)data.msg=msg;
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
	 //获取指定实体字段的最大号
    function getMaxNo(callback) {
        uxutil.server.ajax({
            url: GET_MAXNO_URL
        }, function (data) {
            if (data) {
                callback(data.ResultDataValue);
            } else {
                layer.msg(data.ErrorInfo, { icon: 5});
            }
        });
    }
    //分发规则类型数据加载
    function ruletype(callback){
    	var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_RULE_TYPE_URL,
			type:'get',
			data:{
				page:1,
				limit:1000,
				fields:'LBTranRuleType_Id,LBTranRuleType_CName'
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				var list = (data.value ||{}).list || [];
				if(list.length>0)
				    callback((data.value ||{}).list || []);
				else
				   addruletype(callback);
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
    }
  
     /**@overwrite 返回数据处理方法*/
	function changeResult(data){
	    return data; 
	};
	//时分时间还原解析(yy-mm-dd hh:mm:ss 只截取hh:mm)
	function timeResult(time){
		var value = time;
		if(uxutil.date.isValid(time)){
			var str1 = time.split(' ');
			if(str1.length>0 && str1[1]){
				var str2 = str1[1].split(':');
	            value = str2[0]+":"+str2[1]+":"+str2[2];
			}
		}
		return value;
	}
	//加载与还原
	function load(){
		//从父窗体接收数据
		var DEFAULT_DATA = parent.ChildRuleCheckData();
		var obj = {};
		for(var key in DEFAULT_DATA){
			if(key == 'LBTranRule_UseTimeMin' && DEFAULT_DATA[key]){
	            var strTime = timeResult(DEFAULT_DATA[key]);
	       	    DEFAULT_DATA[key] = strTime;
			}
			if(key == 'LBTranRule_UseTimeMax' && DEFAULT_DATA[key]){
				var endTime = timeResult(DEFAULT_DATA[key]);
	       	    DEFAULT_DATA[key] = endTime;
			}
			//TranToWeek还原
			if(key == 'LBTranRule_TranToWeek' && DEFAULT_DATA[key]){
				if(DEFAULT_DATA[key]){
					var arr = DEFAULT_DATA[key].split(',');
					for(var i in arr){
						if(arr[i]=='1')obj['like1[1]'] =  true;
						if(arr[i]=='2')obj['like1[2]'] =  true;
						if(arr[i]=='3')obj['like1[3]'] =  true;
						if(arr[i]=='4')obj['like1[4]'] =  true;
						if(arr[i]=='5')obj['like1[5]'] =  true;
						if(arr[i]=='6')obj['like1[6]'] =  true;
						if(arr[i]=='7')obj['like1[7]'] =  true;
					}
				}
			}
			if(key == 'LBTranRule_TestDelayDates' && DEFAULT_DATA[key]){
				 TestDelayDates = DEFAULT_DATA[key];
			}
			if(key == 'LBTranRule_SickTypeID' && DEFAULT_DATA[key]){
				SickTypeID =DEFAULT_DATA[key]; 
			}
			if(key == 'LBTranRule_ResetType' && DEFAULT_DATA[key]){
			   ResetType = DEFAULT_DATA[key]; 
			}
			if(key == 'LBTranRule_ClientID' && DEFAULT_DATA[key]){
				ClientID =DEFAULT_DATA[key]; //外送单位
			}
			if(key == 'LBTranRule_DeptID' && DEFAULT_DATA[key]){
			   DeptID = DEFAULT_DATA[key]; //送检科室
			}
			obj[key] = DEFAULT_DATA[key];
		}
		form.val("LBTranRule",obj);
		if(obj.LBTranRule_IsUseNextNo == 'false') {
	    	$("#LBTranRule_IsUseNextNo").removeAttr('checked');
	    }else{
	    	if (!$("#LBTranRule_IsUseNextNo").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                $("#LBTranRule_IsUseNextNo").next('.layui-form-switch').addClass('layui-form-onswitch');
                $("#LBTranRule_IsUseNextNo").next('.layui-form-switch').children("em").html("是");
                $("#LBTranRule_IsUseNextNo")[0].checked = true;
            }
	    }
	   	if(obj.LBTranRule_IsAutoPrint == 'false') {
	    	$("#LBTranRule_IsAutoPrint").removeAttr('checked');
     	}else{
	    	if (!$("#LBTranRule_IsAutoPrint").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                $("#LBTranRule_IsAutoPrint").next('.layui-form-switch').addClass('layui-form-onswitch');
                $("#LBTranRule_IsAutoPrint").next('.layui-form-switch').children("em").html("是");
                $("#LBTranRule_IsAutoPrint")[0].checked = true;
            }
	    }
     	if(obj.LBTranRule_IsPrintProce == 'false'){
	    	$("#LBTranRule_IsPrintProce").removeAttr('checked');
	    }else{
	    	if (!$("#LBTranRule_IsPrintProce").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                $("#LBTranRule_IsPrintProce").next('.layui-form-switch').addClass('layui-form-onswitch');
                $("#LBTranRule_IsPrintProce").next('.layui-form-switch').children("em").html("是");
                $("#LBTranRule_IsPrintProce")[0].checked = true;
            }
	    }
        if(obj.LBTranRule_IsFollow == 'false'){
	    	$("#LBTranRule_IsFollow").removeAttr('checked');
	    }else{
	    	if (!$("#LBTranRule_IsFollow").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                $("#LBTranRule_IsFollow").next('.layui-form-switch').addClass('layui-form-onswitch');
                $("#LBTranRule_IsFollow").next('.layui-form-switch').children("em").html("是");
                $("#LBTranRule_IsFollow")[0].checked = true;
            }
	    }
	    form.render();

	}
	//初始化
	function init(){
		 //时间选择器
		laydate.render({
		    elem: '#LBTranRule_UseTimeMin',
		    type: 'time',
		    format: "HH:mm:ss",
		    trigger: 'click'
		});
		 //时间选择器
		laydate.render({
		    elem: '#LBTranRule_UseTimeMax',
		    type: 'time',
		    format: "HH:mm:ss",
		    trigger: 'click'
		});
		if(!ID){
			formtype ='add';
            $("#LBTranRule_LBTranRuleType_Id").val(DEFAULTDRULETYPEID);
            $("#LBTranRule_LBTranRuleType_CName").val("默认规则"); 
            ResetType ='1';//复位周期默认为日
		}else{
			formtype = 'edit';
			load();//数据加载
		}
	    //下拉框初始化 
        initselect();
		form.render();
	}
	//启用
	form.on('switch(LBTranRule_IsUseNextNo)', function(data){
		if(data.elem.checked){
			nextSampleNo();
		}
	}); 
	//样本号前缀 
	$("#LBTranRule_SampleNoRule").bind("input propertychange",function(event){
		nextSampleNo();
	});
    //生成下一样本号
    function nextSampleNo(){ 
     	var SampleNoMin = $('#LBTranRule_SampleNoMin').val(),
     	    SampleNoRule = $('#LBTranRule_SampleNoRule').val();
     	if(!SampleNoMin || !SampleNoRule){
     		layer.msg('样本区间号并且样本前缀不为空才能生成下一样本号');
     		return false;
     	}
     	var entity={
			SampleNoSection:SampleNoMin,//样本号区间的起始数
			SampleNoPrefix:SampleNoRule //样本号前缀
		};
		//显示遮罩层
		var config1 = {
			type: "POST",
			url: NEXT_SAMPLENO_URL,
			data:  JSON.stringify(entity)
		};
		var index = layer.load();
		uxutil.server.ajax(config1, function(data) {
			layer.close(index);
			//隐藏遮罩层
			if(data.success){
			   var value = data.value || "";
			   $('#LBTranRule_NextSampleNo').val(value);
			} else {
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	}
	//初始化
	init();
	
});
