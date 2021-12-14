/**
 * 医院区域字典
 * @author guohaixiang
 * @version 2019-12-11
 */

layui.extend({
    uxutil: 'ux/util',
    treeTable: 'ux/other/treeTable/treeTable',
    tableSelect: 'ux/other/tableSelect/tableSelect',
    areaHospitalTable:'views/system/Hospital/B_HospitalArea_New/areaHospitalTable/areaHospitalTable'
}).use(['uxutil', 'table', 'form', 'treeTable', 'areaHospitalTable','tableSelect'], function () {
	var layer = layui.layer,
		uxutil = layui.uxutil,
        treeTable = layui.treeTable,
        tableSelect = layui.tableSelect,
		areaHospitalTable = layui.areaHospitalTable,
		$ = layui.jquery;

   
	//表单
    var formObj = {
        form: layui.form,
        type: 'edit',
        AreaId: 0,
        PAreaId: 0,
        FormData: {},
        reset: function () { //重置
            if (formObj.type == 'edit') {
                formObj.form.val("form", formObj.FormData);
                if (formObj.FormData.BHospitalArea_IsUse == "true") {
                    if (!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                        $("#isUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                        $("#isUse").next('.layui-form-switch').children("em").html("启用");
                        $("#isUse")[0].checked = true;
                    }
                } else {
                    if ($("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                        $("#isUse").next('.layui-form-switch').removeClass('layui-form-onswitch');
                        $("#isUse").next('.layui-form-switch').children("em").html("禁用");
                        $("#isUse")[0].checked = false;
                    }
                }
            } else {
                $("#layForm")[0].reset();
                if (!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                    $("#isUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                    $("#isUse").next('.layui-form-switch').children("em").html("启用");
                    $("#isUse")[0].checked = true;
                }
            }
        },
		empty: function() { //清空
            $("#layForm")[0].reset();
            if (!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                $("#isUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                $("#isUse").next('.layui-form-switch').children("em").html("启用");
                $("#isUse")[0].checked = true;
            }
		},
        enableSaveBtn: function () {
            if ($('#save').hasClass('layui-btn-disabled')) {
                $("#save").removeProp("disabled");
                $('#save').removeClass('layui-btn-disabled');
            }
        }
	};
    //获得参数
    function getParams() {
        var params = location.search.split("?")[1].split("&");
        //参数赋值
        for (var j in formObj) {
            for (var i = 0; i < params.length; i++) {
                if (j.toUpperCase() == params[i].split("=")[0].toUpperCase()) {
                    formObj[j] = decodeURIComponent(params[i].split("=")[1]);
                }
            }
        }
    }; 
    getParams();
   
    var Obj = {
    elem: '#areaHospitalTable',
    title: '医院字典',
    height: 'full-119',
    id: 'areaHospitalTable'
    };
    if (formObj.type == 'edit') {
        areaHospitalTable.render(Obj, formObj.AreaId);
        //获得区域信息
        $.ajax({
            type: 'get',
            dataType: 'json',
            contentType: "application/json",
            ansyc: false,
            url: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalAreaById?isPlanish=true&fields=BHospitalArea_Id,BHospitalArea_Name,BHospitalArea_Code,BHospitalArea_CenterHospitalID,BHospitalArea_CenterHospitalName,BHospitalArea_PHospitalAreaID,BHospitalArea_PHospitalAreaName,BHospitalArea_PHospitalAreaCode,BHospitalArea_DispOrder,BHospitalArea_IsUse,BHospitalArea_Comment,BHospitalArea_DeptID,BHospitalArea_DeptName,BHospitalArea_HospitalAreaLevelName' +
                '&id=' + formObj.AreaId,
            success: function (res) {
                if (res.success) {
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                    formObj.form.val("form", data);
                    formObj.FormData = data;
                } else {
                    layer.msg("获得区域信息失败！", { icon: 5, anim: 6 });
                }
            }
        });
    } else {
        areaHospitalTable.render(Obj);
    }

	//监听form表单
	formObj.form.on('submit(save)', function(data) {
        window.event.preventDefault();
        var loadIndex = layer.load();//开启加载层
		var fields = "";//发送修改的字段
		var postData = {};//发送的数据
		//判断复选框是否选中 未选中的话手动添加字段
        if (!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
            data.field["BHospitalArea_IsUse"] = "false"
        } else {
            data.field["BHospitalArea_IsUse"] = "true"
        }
		for(k in data.field){
			if(k == "BHospitalArea_AreaTypeID" && data.field[k] == ""){
				postData[k.split("_")[1]] = null;
				fields += k.split("_")[1]+",";
			}else if(k == "BHospitalArea_CenterHospitalID" && data.field[k] == ""){
				postData[k.split("_")[1]] = null;
				fields += k.split("_")[1]+",";
			}else if(k == "BHospitalArea_PHospitalAreaID" && data.field[k] == ""){
				postData[k.split("_")[1]] = null;
                fields += k.split("_")[1] + ",";
            } else if (k == "BHospitalArea_DeptID" && data.field[k] == "") {
                postData[k.split("_")[1]] = null;
                fields += k.split("_")[1] + ",";
            }else {
				postData[k.split("_")[1]] = data.field[k];
				fields += k.split("_")[1]+",";
			}			
		}
		fields = fields.slice(0,fields.length-1);
		if(formObj.type == 'edit') {
			$.ajax({
				type:'post',
				dataType:'json',
				contentType: "application/json",
				data:JSON.stringify({entity:postData,fields:fields}),
                url: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_UpdateBHospitalAreaByField',
                success: function (res) {
                    layer.close(loadIndex);//关闭加载层
					if(res.success){
						layer.msg("编辑成功！",{icon: 6,anim:0});
					}else{
						layer.msg("编辑失败！",{icon: 5,anim:6});
					}
				}
			});
		} else if(formObj.type == 'add') {
			delete postData.Id;
			$.ajax({
				type:'post',
				dataType:'json',
				contentType: "application/json",
				data:JSON.stringify({entity:postData}),
                url: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_AddBHospitalArea',
                success: function (res) {
                    layer.close(loadIndex);//关闭加载层
					if(res.success){
                        layer.msg("新增成功！", { icon: 6, anim: 0 });
                        areaHospitalTable.AreaId = res.ResultDataValue.replace(/[^0-9]/ig, ""); 
					}else{
						layer.msg("新增失败！",{icon: 5,anim:6});
					}
				}
			});
		}
	});
	//重置添加、修改不同操作
	$('#cancel').on('click', function() {
		if(formObj.type == 'edit') {
			formObj.reset();
		} else if(formObj.type == 'add') {
			formObj.empty();
		}
	});
    
    //区域-下拉框加载
    PHospitalAreaList();
    function PHospitalAreaList(checkrowdata) {
        var me = this;
        var url="";
        if(!checkrowdata){
        	 url = uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalAreaByHQL?isPlanish=true&sort=[{property:"BHospitalArea_DispOrder",direction:"ASC"}]' + '&where=IsUse=1' +
            '&fields=BHospitalArea_Name,BHospitalArea_Id,BHospitalArea_Code';
        }else{
        	url = uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalAreaFiltrationById?isPlanish=true&id='+checkrowdata.BHospitalArea_Id +
            '&fields=BHospitalArea_Name,BHospitalArea_Id,BHospitalArea_Code';
        }
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data) {
                var value = data[uxutil.server.resultParams.value];
                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                        value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                        value = value.replace(/\\"/g, '&quot;');
                        value = value.replace(/\\/g, '\\\\');
                        value = value.replace(/&quot;/g, '\\"');
                        value = eval("(" + value + ")");
                    } else {
                        value = value + "";
                    }
                }
                //if (!value) return;  
                if(!value){
                	$("#PHospitalAreaID").empty();
                	formObj.form.render('select');//需要渲染一下;
                	return;
                }
                var tempAjax = "<option value=''>请选择</option>";
                for (var i = 0; i < value.list.length; i++) {
                    var aid = formObj.AreaId;
                    if (formObj.type!="add") {
                        aid = formObj.PAreaId;
                    }
                    if (value.list[i].BHospitalArea_Id == aid) {
                        tempAjax += "<option data-code='" + value.list[i].BHospitalArea_Code + "' selected value='" + value.list[i].BHospitalArea_Id + "'>" + value.list[i].BHospitalArea_Name + "</option>";
                        $('#PHospitalAreaName').val(value.list[i].BHospitalArea_Name);
                        $('#PHospitalAreaCode').val(value.list[i].BHospitalArea_Code); 
                    } else {
                        tempAjax += "<option data-code='" + value.list[i].BHospitalArea_Code + "' value='" + value.list[i].BHospitalArea_Id + "'>" + value.list[i].BHospitalArea_Name + "</option>";
                    }
                    $("#PHospitalAreaID").empty();
                    $("#PHospitalAreaID").append(tempAjax);
                    
                }
                formObj.form.render('select');//需要渲染一下;
            } else {
                layer.msg(data.msg);
            }
        });
    };
 	formObj.form.on('select(PHospitalAreaID)', function(data){
	  $('#PHospitalAreaName').val($("#PHospitalAreaID").children('option:selected')[0].text);
	  $('#PHospitalAreaCode').val($("#PHospitalAreaID").children('option:selected')[0].dataset.code); 
	}); 
	//中心医院字段
	 CenterHospitallist();
    function CenterHospitallist() {
        var me = this;
        var url =  uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalByHQL?isPlanish=true'+
            '&fields=BHospital_Name,BHospital_Id' + '&where=IsUse=1';

        tableSelect.render({
            width:40,
            elem: '#CenterHospitalName',	//定义输入框input对象 必填
            checkedKey: 'BHospital_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'Name',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '医院名称',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                
                cols: [[
                    { type: 'radio' },
                    {
                        type: 'numbers', title: '序号', width: 45,
                    },
                    {
                        field: 'BHospital_Id',
                        width: 160,
                        title: '主键ID',
                        hide: true
                    },
                    {
                        field: 'BHospital_Name',
                        title: '医院名称',
                        minWidth: 130
                    }
                ]],
                response: function () {
                    return {
                        statusCode: true, //成功状态码
                        statusName: 'code', //code key
                        msgName: 'msg ', //msg key
                        dataName: 'data' //data key
                    }
                },
                parseData: function (res) { //res即为原始返回的数据
                    if (!res) return;
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                    return {
                        "code": res.success ? 0 : 1, //解析接口状态
                        "msg": res.ErrorInfo, //解析提示文本
                        "count": data.count || 0, //解析数据长度
                        "data": data.list || []
                    };
                },
                text: {
                    none: '暂无相关数据'
                }
            },
            done: function (elem, data) {
                if (data.data.length > 0) {
                    $('#CenterHospitalID').val(data.data[0].BHospital_Id);
                    $('#CenterHospitalName').val(data.data[0].BHospital_Name);
                }
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                //拿到data[]后 就按照业务需求做想做的事情啦~比如加个隐藏域放ID...
            }
        });       
    };
    //部门字段
    Deptlist();
    function Deptlist() {
        var me = this;
        var url = uxutil.path.ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL?isPlanish=true' + '&where=IsUse=1' +
            '&fields=HRDept_Id,HRDept_CName';
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data) {
                var value = data[uxutil.server.resultParams.value];
                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                        value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                        value = value.replace(/\\"/g, '&quot;');
                        value = value.replace(/\\/g, '\\\\');
                        value = value.replace(/&quot;/g, '\\"');
                        value = eval("(" + value + ")");
                    } else {
                        value = value + "";
                    }
                }
                if (!value) return;
                var tempAjax = "<option value=''>请选择</option>";
                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].HRDept_Id + "'>" + value.list[i].HRDept_CName + "</option>";
                    $("#DeptID").empty();
                    $("#DeptID").append(tempAjax);

                }
                formObj.form.render('select');//需要渲染一下;
            } else {
                layer.msg(data.msg);
            }
        });
    };
    formObj.form.on('select(DeptID)', function (data) {
        $('#DeptName').val($("#DeptID").children('option:selected')[0].text);
    }); 
});