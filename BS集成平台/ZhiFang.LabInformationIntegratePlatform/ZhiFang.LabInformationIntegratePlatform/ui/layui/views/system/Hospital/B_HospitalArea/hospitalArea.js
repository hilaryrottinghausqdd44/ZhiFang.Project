/**
 * 医院区域字典
 * @author guohaixiang
 * @version 2019-12-11
 */

layui.extend({
    uxutil: 'ux/util',
    treeTable: 'ux/other/treeTable/treeTable',
    tableSelect: 'ux/other/tableSelect/tableSelect',
    areaHospitalTable:'views/system/Hospital/B_HospitalArea/areaHospitalTable/areaHospitalTable'
}).use(['uxutil', 'table', 'form', 'treeTable', 'areaHospitalTable','tableSelect'], function () {
	var layer = layui.layer,
		uxutil = layui.uxutil,
        treeTable = layui.treeTable,
        tableSelect = layui.tableSelect,
		areaHospitalTable = layui.areaHospitalTable,
		$ = layui.jquery;
	//表格	
	var tableObj = {
		table: layui.table,
		form: layui.form,
		fields:{
			Id:"BHospitalArea_Id",
			IsUse:'BHospitalArea_IsUse'
        },
        current: null,
        delIndex: null,
        addUrl: uxutil.path.ROOT +'/ServerWCF/LIIPCommonService.svc/ST_UDTO_AddBHospitalArea',
        selectUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalAreaByHQL?isPlanish=true&sort=[{property:"BHospitalArea_DispOrder",direction:"ASC"}]&where=IsUse=1',
        updateUrl: uxutil.path.ROOT +'/ServerWCF/LIIPCommonService.svc/ST_UDTO_UpdateBHospitalAreaByField',
        delUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_DelBHospitalArea',
        uodatePNameUrl: uxutil.path.ROOT +'/ServerWCF/LIIPCommonService.svc/ST_UDTO_UpdateBHospitalAreaByWhere',
        checkRowData: [], //选中数据
        refresh: function () {
            var searchText = $("#search")[0].value;
            tableObj.table.reload('table', {
                where: {
                    time: new Date().getTime()
                }
            });
            tableObj.checkRowData = [];
            if (searchText != "") {
                $("#search").val(searchText);
            }
        }
	};
	//表单
	var formObj = {
		form: layui.form,
		type: 'edit',
        reset: function () { //重置
            if (formObj.type == 'edit') {
                if (tableObj.checkRowData.length > 0) {
                    formObj.form.val("form", tableObj.checkRowData[tableObj.checkRowData.length - 1]);
                    if (tableObj.checkRowData[tableObj.checkRowData.length - 1].BHospitalArea_IsUse == "true") {
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
        disabledSaveBtn: function () {
            if (!$('#save').hasClass('layui-btn-disabled')) {
                $("#save").prop("disabled", "disabled");
                $('#save').addClass('layui-btn-disabled');
            }
        },
        enableSaveBtn: function () {
            if ($('#save').hasClass('layui-btn-disabled')) {
                $("#save").removeProp("disabled");
                $('#save').removeClass('layui-btn-disabled');
            }
        }
	};
	
	//初始化表格
    var insTb =  treeTable.render({
            elem: '#table',
            //height: 'full-50',
            //size: 'sm', //小尺寸的表格
            tree: {
		        iconIndex: 1,  // 折叠图标显示在第几列
		        idName: 'BHospitalArea_Id',  // 自定义id字段的名称
		        pidName: 'BHospitalArea_PHospitalAreaID',  // 自定义标识是否还有子节点的字段名称
		        //haveChildName: 'haveChild',  // 自定义标识是否还有子节点的字段名称
		        isPidData: true  // 是否是pid形式数据
		    },
            cols: [
                {
                    type: 'numbers',title:'序号', width: 45,
                }/*,
                {
                    field: tableObj.fields.Id,
                    width: 160,
                    title: '主键ID',
                    hide: true
                }*/,
                {
                    field: 'BHospitalArea_Name',
                    title: '医院区域名称',
                    minWidth: 130,
                    sort: true
                },
                /*{
                    field: 'BHospitalArea_Code',
                    title: '医院区域简码',
                    minWidth: 130,
                    sort: true
                },
                {
                    field: 'BHospitalArea_PHospitalAreaID',
                    title: '父区域ID',
                    width: 160,
                    sort: true
                },*/
                {
                    field: 'BHospitalArea_PHospitalAreaName',
                    title: '父区域名称',
                    minWidth: 130,
                    sort: true
                },
                /*{
                    field: 'BHospitalArea_PHospitalAreaCode',
                    title: '父区域简码',
                    minWidth: 130,
                    sort: true
                },
                {
                    field: 'BHospitalArea_CenterHospitalID',
                    title: '区域中心医院ID',
                    minWidth: 130,
                    sort: true
                }*/
                {
                    field: 'BHospitalArea_CenterHospitalName',
                    title: '区域中心医院名称',
                    sort: true,
                    minWidth: 130
                },
                {align: 'center', toolbar: '#toolbar', title: '操作', width: 200}
                /*,
                {
                    field: 'BHospitalArea_AreaTypeID',
                    title: '区域类型ID',
                    sort: true,
                    minWidth: 130
                },
                {
                    field: 'BHospitalArea_AreaTypeName',
                    title: '区域类型名称',
                    sort: true,
                    minWidth: 130
                },
                {
                    field: 'BHospitalArea_SName',
                    title: '简称',
                    minWidth: 70,
                    //hide: true,
                    sort: true
                },
                {
                    field: 'BHospitalArea_Shortcode',
                    title: '快捷码',
                    minWidth: 100,
                    sort: true
                },
                {
                    field: 'BHospitalArea_Comment',
                    title: '备注',
                    minWidth: 100,
                    sort: true
                },               
                {
                    field: 'BHospitalArea_PinYinZiTou',
                    title: '拼音字头',
                    minWidth: 100,
                    sort: true
                }, 
                {
                    field: 'BHospitalArea_DispOrder',
                    title: '排序字段',
                    minWidth: 100,
                    sort: true
                }*/
            ],
            
            reqData: function(data, callback) {
		        // 在这里写ajax请求，通过callback方法回调数据
		        $.get(tableObj.selectUrl, function (res) {
                    //console.log(JSON.parse(res.ResultDataValue).list);
		            callback(JSON.parse(res.ResultDataValue).list);  // 参数是数组类型
		             if (JSON.parse(res.ResultDataValue).list.length > 0) {
	                    $("#table+div .ew-tree-table-box table.layui-table tbody tr:first-child")[0].click();
	                }
		        });
		    },
            text: {
                none: '暂无相关数据'
            }
        });
    
   // 监听行单击事件
	treeTable.on('row(table)', function(obj){
	    tableObj.checkRowData = [];
        formObj.type = 'edit';
        //标注选中样式
        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
        
        editHospitalAreaLevelName(obj.data.BHospitalArea_Id,function(HospitalAreaLevelName){
       		obj.data.BHospitalArea_HospitalAreaLevelName = HospitalAreaLevelName;
        });//增加修改区域等级
        
        tableObj.checkRowData.push(obj.data);
        
        formObj.reset();//加载表格
        formObj.disabledSaveBtn();
        
         var Obj = {
            elem: '#areaHospitalTable',
            title: '医院字典',
            height: 'full-444',
            id: 'areaHospitalTable'
        };
        areaHospitalTable.render(Obj,obj.data.BHospitalArea_Id);
        
	});
	// 全部展开
    $('#btnExpandAll').click(function () {
        insTb.expandAll();
    });

    // 全部折叠
    $('#btnFoldAll').click(function () {
        insTb.foldAll();
    });

    $('#btnSearch').click(function () {
        var keywords = $('#edtSearch').val();
        if (keywords) {
            insTb.filterData(keywords);
        } else {
            insTb.clearFilter();
        }
    });

    $('#btnClearSearch').click(function () {
        insTb.clearFilter();
    });
    
    $('#formAdd').click(function () {
        PHospitalAreaList();
        formObj.type = 'add';//编辑还是新增
        formObj.reset();
        formObj.enableSaveBtn();
    });
				
	//table上面的工具栏
	treeTable.on('tool(table)', function(obj) {
		switch(obj.event) {
			case 'edit':
				//PHospitalAreaList(obj.data);
				formObj.enableSaveBtn();
                formObj.type = 'edit';//编辑还是新增
                tableObj.checkRowData = [];
                tableObj.checkRowData.push(obj.data);
				formObj.reset();
				break;
			case 'del':
                layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
                    var loadIndex = layer.load();//开启加载层
				  		$.ajax({
					  		type:"get",
					  		url:tableObj.delUrl+"?Id="+obj.data.BHospitalArea_Id,
					  		async:true,
					  		dataType:'json',
                            success: function (res) {
					  			if(res.success){
                                    layer.close(loadIndex);//关闭加载层
					  				layer.close(index);
									layer.msg("删除成功！",{icon: 6,anim:0});
                                    formObj.reset();
								  	formObj.disabledSaveBtn();
								  	formObj.empty();
								  	insTb.refresh();
					  			}else{
					  				layer.msg("删除失败！", { icon: 5, anim: 6 });
                                    tableObj.delIndex = null;
                                    layer.close(loadIndex);//关闭加载层
					  			}
					  		}
					  	});
				});
				break;
			
		};
	});
	//监听form表单
	formObj.form.on('submit(save)', function(data) {
        window.event.preventDefault();
        var loadIndex = layer.load();//开启加载层
		var fields = "";//发送修改的字段
		var postData = {};//发送的数据
		//判断复选框是否选中 未选中的话手动添加字段
		if(!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')){
			data.field[tableObj.fields.IsUse] = "false"
		}else{
			data.field[tableObj.fields.IsUse] = "true"
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
        if (postData["PHospitalAreaID"] == null) {
            postData["PHospitalAreaName"] = null;
        }
		fields = fields.slice(0,fields.length-1);
		if(formObj.type == 'edit') {
			$.ajax({
				type:'post',
				dataType:'json',
				contentType: "application/json",
				data:JSON.stringify({entity:postData,fields:fields}),
				url:tableObj.updateUrl,
                success: function (res) {
                    layer.close(loadIndex);//关闭加载层
                    if (res.success) {
                        updatePHospitalAreaName(postData);
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
				url:tableObj.addUrl,
                success: function (res) {
                    layer.close(loadIndex);//关闭加载层
					if(res.success){
						layer.msg("新增成功！",{icon: 6,anim:0});
						formObj.disabledSaveBtn();
                        formObj.empty();
                        var data = eval('(' + res.ResultDataValue + ')');
						insTb.refresh();
					}else{
						layer.msg("新增失败！",{icon: 5,anim:6});
					}
				}
			});
		}
	});
	//重置添加、修改不同操作
	$('#cancel').on('click', function() {
		if(tableObj.checkRowData.length == 0){
			formObj.empty();
			return;
		}
		if(formObj.type == 'edit') {
			formObj.reset();
		} else if(formObj.type == 'add') {
			formObj.empty();
		}
    });

    function updatePHospitalAreaName(pardata) {       
        var fields = "PHospitalAreaName";//发送修改的字段
        var postData = {};//发送的数据
        var where = "";
        if (pardata != null && pardata != {}) {
            var loadIndex = layer.load();//开启加载层
            postData["PHospitalAreaName"] = pardata.Name;
            where = "bhospitalarea.PHospitalAreaID=" + pardata.Id;
            $.ajax({
                type: 'post',
                dataType: 'json',
                contentType: "application/json",
                data: JSON.stringify({ entity: postData, fields: fields, where: where }),
                url: tableObj.uodatePNameUrl,
                success: function (res) {
                    layer.close(loadIndex);//关闭加载层                    
                    formObj.disabledSaveBtn();
                    formObj.empty();
                    insTb.refresh();
                }
            });
        }
    };
	
	function editHospitalAreaLevelName(id,callback){
		var loadIndex = layer.load();//开启加载层
		$.ajax({
			type:'get',
			url:uxutil.path.ROOT +'/ServerWCF/LIIPCommonService.svc/ST_UDTO_UpdateBHospitalAreaLevelNameTreeByID?id='+id,
			async:true,
			dataType:'json',
            success: function (res) {
                layer.close(loadIndex);//关闭加载层
				if(res.success){
					callback(res.ResultDataValue);
				}else{
				}
			}
		});
		
	};
    
    //监听浏览器窗口
    window.onresize = function () {
        $(".tableHeight").css("height", ($(window).height() - 30) + "px");//设置表单容器高度
    };

    
    
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
                    tempAjax += "<option data-code='"+ value.list[i].BHospitalArea_Code +"' value='" + value.list[i].BHospitalArea_Id + "'>" + value.list[i].BHospitalArea_Name + "</option>";
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
	
	HTypelist()
    function HTypelist() {
        var me = this;
        var url =  uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalTypeByHQL?isPlanish=true' + '&where=IsUse=1' +
            '&fields=BHospitalType_Name,BHospitalType_Id';
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
                    tempAjax += "<option value='" + value.list[i].BHospitalType_Id + "'>" + value.list[i].BHospitalType_Name + "</option>";
                    $("#AreaTypeID").empty();
                    $("#AreaTypeID").append(tempAjax);

                }
                formObj.form.render('select');//需要渲染一下;
            } else {
                layer.msg(data.msg);
            }
        });
    };
    formObj.form.on('select(AreaTypeID)', function(data){
		$('#AreaTypeName').val($("#AreaTypeID").children('option:selected')[0].text);
	}); 


	//中心医院字段
	 CenterHospitallist();
    function CenterHospitallist() {
        var me = this;
        var url =  uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalByHQL?isPlanish=true'+
            '&fields=BHospital_Name,BHospital_Id' + '&where=IsUse=1';

        tableSelect.render({
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